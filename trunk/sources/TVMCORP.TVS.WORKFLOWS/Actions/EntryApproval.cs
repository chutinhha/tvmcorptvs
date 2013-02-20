using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
    public partial class EntryApproval : CoreApproval
    {
        public EntryApproval()
        {
            InitializeComponent();
        }

        #region Dependency Properties
        public static DependencyProperty ApprovalListIdProperty =
            DependencyProperty.Register("ApprovalListId",
            typeof(string), typeof(EntryApproval));

        [Description("Approval ListId")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ApprovalListId
        {
            get { return ((string)(base.GetValue(ApprovalListIdProperty))); }
            set { base.SetValue(ApprovalListIdProperty, value); }
        }

        public static DependencyProperty ApprovalKeyProperty =
            DependencyProperty.Register("ApprovalKey",
            typeof(string), typeof(EntryApproval));

        [Description("Approval Key")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ApprovalKey
        {
            get { return ((string)(base.GetValue(ApprovalKeyProperty))); }
            set { base.SetValue(ApprovalKeyProperty, value); }
        }

        public static DependencyProperty StatusProperty =
            DependencyProperty.Register("Status",
            typeof(string), typeof(EntryApproval));

        [Description("Status Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Status
        {
            get { return ((string)(base.GetValue(StatusProperty))); }
            set { base.SetValue(StatusProperty, value); }
        }
        #endregion

        #region Constants
        private const string APPROVAL_KEY_COLUMN = "Title";
        private const string APPROVAL_COLUMN = "Approvals";
        private const string SKIPPED_STATUS = "Skipped";
        #endregion

        #region Fields
        private bool _blnExistEntryApprovalItem = true;
        public string _strApprovalConfigurationListURL = string.Empty;
        public IList _listApprovalLevel = new List<string>();
        private int _intChildCompleted = 0;
        public string _strLogDescription = string.Empty;
        private TaskResultSettings taskResultSettings; 
        #endregion

        #region Handlers Code
        private void initialEntryApprovalData_ExecuteCode(object sender, EventArgs e)
        {
            taskResultSettings = new TaskResultSettings(__ActivationProperties.Site.ID);
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPList approvalList = __ActivationProperties.Web.Lists.GetList(new Guid(ApprovalListId), false);

                SPListItem approvalValueItem = GetEntryApprovalItem(approvalList, ApprovalKey);
                if (approvalValueItem == null)
                {
                    _blnExistEntryApprovalItem = false;
                    this.Status = SKIPPED_STATUS;
                    return;
                }

                GenerateOutputValue(approvalValueItem);
                _strApprovalConfigurationListURL = GetApprovalConfigurationListURL(approvalList, APPROVAL_COLUMN);
                SetValuesToListApprovalLevel(approvalValueItem);

            });
        }

        private void untilReplicatorCondition_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            /* 
             * stop repilcator if have one of conditions below:
             * 1. is not exist aprpoval value item
             * 2. return status of child is a terminate status
             * 3. all child are completed
             */
            e.Result = !_blnExistEntryApprovalItem
                || (!string.IsNullOrEmpty(this.Status) && this.Status != taskResultSettings.ApprovedText)
                || _intChildCompleted == _listApprovalLevel.Count;
        }

        private void initializedChild_ExecuteCode(object sender, ReplicatorChildEventArgs e)
        {
            (e.Activity as ApprovalWorkflow).ApprovalName = e.InstanceData.ToString();
        }

        private void completedChild_ExecuteCode(object sender, ReplicatorChildEventArgs e)
        {
            _intChildCompleted++;
            //return status of activity
            if (!string.IsNullOrEmpty((e.Activity as ApprovalWorkflow).Status))
                this.Status = (e.Activity as ApprovalWorkflow).Status;
        }

        private void logResultToHistoryList_ExecuteCode(object sender, EventArgs e)
        {
            _strLogDescription = "Entry Approval tasks are completed with status: " + this.Status;
        }
        #endregion

        #region Helpers
        private SPListItem GetEntryApprovalItem(SPList list, string strTitle)
        {
            try
            {
                StringBuilder stringBuild = new StringBuilder();
                stringBuild.Append("<Where>");
                stringBuild.Append("    <Eq>");
                stringBuild.Append("        <FieldRef Name=" + APPROVAL_KEY_COLUMN + " />");
                stringBuild.Append("        <Value Type='Text'>" + strTitle + "</Value>");
                stringBuild.Append("    </Eq>");
                stringBuild.Append("</Where>");

                SPQuery query = new SPQuery();
                query.Query = stringBuild.ToString();
                SPListItemCollection items = list.GetItems(query);
                if (items.Count > 0)
                    return items[0];
                else
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowComment, __ActivationProperties.Web.CurrentUser, "No entry approval could be found in " + list.Title + " using the key " + strTitle + ". Entry approval is skipped", string.Empty);
            }
            catch (Exception e)
            {
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "No entry approval could be found in " + list.Title + " using the key " + strTitle, string.Empty);
                throw new Exception();
            }
            return null;
        }

        private string GetApprovalConfigurationListURL(SPList list, string strInternalNameColumn)
        {
            SPFieldLookup fieldLookup = (SPFieldLookup)list.Fields.GetFieldByInternalName(strInternalNameColumn);
            SPList listLookup = list.ParentWeb.Lists.GetList(new Guid(fieldLookup.LookupList), false);
            return listLookup.DefaultViewUrl;
        }

        private void SetValuesToListApprovalLevel(SPListItem item)
        {
            if (item[APPROVAL_COLUMN] != null)
            {
                SPFieldLookupValueCollection lookupValues = new SPFieldLookupValueCollection(item[APPROVAL_COLUMN].ToString());
                foreach (SPFieldLookupValue lookupValue in lookupValues)
                    _listApprovalLevel.Add(lookupValue.LookupValue);
            }
        }
        #endregion
    }
}
