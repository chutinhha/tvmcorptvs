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
    public partial class ValueApproval : CoreApproval
    {
        public ValueApproval()
        {
            InitializeComponent();
        }

        #region Dependency Properties
        public static DependencyProperty ApprovalListIdProperty =
            DependencyProperty.Register("ApprovalListId",
            typeof(string), typeof(ValueApproval));

        [Description("Approval ListId")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ApprovalListId
        {
            get { return ((string)(base.GetValue(ApprovalListIdProperty))); }
            set { base.SetValue(ApprovalListIdProperty, value); }
        }

        public static DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount",
            typeof(string), typeof(ValueApproval));

        [Description("Amount")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Amount
        {
            get { return ((string)(base.GetValue(AmountProperty))); }
            set { base.SetValue(AmountProperty, value); }
        }

        public static DependencyProperty StatusProperty =
            DependencyProperty.Register("Status",
            typeof(string), typeof(ValueApproval));

        [Description("Status Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Status
        {
            get { return ((string)(base.GetValue(StatusProperty))); }
            set { base.SetValue(StatusProperty, value); }
        }

        public static DependencyProperty ApprovalWorkflowParameterProperty =
        DependencyProperty.Register("ApprovalWorkflowParameter",
        typeof(ApprovalWorkflowParameter), typeof(ValueApproval));

        [Description("Approval Workflow Parameter")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.None)]
        public ApprovalWorkflowParameter ApprovalWorkflowParameter
        {
            get { return ((ApprovalWorkflowParameter)(base.GetValue(ApprovalWorkflowParameterProperty))); }
            set { base.SetValue(ApprovalWorkflowParameterProperty, value); }
        }

        #endregion

        #region Constants
        private const string APPROVAL_VALUE_COLUMN = "ApprovalValue";
        private const string APPROVAL_COLUMN = "Approvals";
        private const string SKIPPED_STATUS = "Skipped";
        #endregion

        #region Fields
        private bool _blnExistApprovalValueItem = true;
        public string _strApprovalConfigurationListURL = string.Empty;
        public IList _listApprovalLevel = new List<string>();
        private int _intChildCompleted = 0;
        public string _strLogDescription = string.Empty;
        private TaskResultSettings taskResultSettings;
        #endregion

        #region Execute Code
        private void initialValueApprovalData_ExecuteCode(object sender, EventArgs e)
        {
            taskResultSettings = new TaskResultSettings(__ActivationProperties.Site.ID);
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                Guid approvalListGuiId = Guid.Empty;
                try
                {
                    approvalListGuiId = new Guid(ApprovalListId);
                }
                catch { };
                SPList approvalList = null;
                if (approvalListGuiId != Guid.Empty)
                    approvalList = __ActivationProperties.Web.Lists.GetList(approvalListGuiId, false);
                else
                    approvalList = __ActivationProperties.GetListFromURL(ApprovalListId);

                if (approvalList == null) throw new Exception();

                SPListItem approvalValueItem = GetApprovalValueItem(approvalList, Amount);
                if (approvalValueItem == null)
                {
                    _blnExistApprovalValueItem = false;
                    this.Status = SKIPPED_STATUS;
                    return;
                }

                GenerateOutputValue(approvalValueItem);

                _strApprovalConfigurationListURL = GetApprovalConfigurationListURL(approvalList, APPROVAL_COLUMN);
                SetValuesToListApprovalLevel(approvalValueItem);
            });
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

        private void untilReplicatorCondition_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            /* 
             * stop repilcator if have one of conditions below:
             * 1. is not exist aprpoval value item
             * 2. return status of child is a terminate status
             * 3. all child are completed
             */
            e.Result = !_blnExistApprovalValueItem
                || (!string.IsNullOrEmpty(this.Status) && this.Status != taskResultSettings.ApprovedText)
                || _intChildCompleted == _listApprovalLevel.Count;
        }

        private void logResultToHistoryList_ExecuteCode(object sender, EventArgs e)
        {
            _strLogDescription = "Value Approval tasks are completed with status: " + this.Status;
        }
        #endregion

        #region Helpers
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

        private SPListItem GetApprovalValueItem(SPList list, string strAmount)
        {
            try
            {
                StringBuilder stringBuild = new StringBuilder();
                stringBuild.Append("<Where>");
                stringBuild.Append("    <Lt>");
                stringBuild.Append("        <FieldRef Name=" + APPROVAL_VALUE_COLUMN + " />");
                stringBuild.Append("        <Value Type='Currency'>" + strAmount + "</Value>");
                stringBuild.Append("    </Lt>");
                stringBuild.Append("</Where>");
                stringBuild.Append("<OrderBy>");
                stringBuild.Append("    <FieldRef Name=" + APPROVAL_VALUE_COLUMN + " Ascending='False' />");
                stringBuild.Append("</OrderBy>");

                SPQuery query = new SPQuery();
                query.Query = stringBuild.ToString();
                SPListItemCollection items = list.GetItems(query);
                if (items.Count > 0)
                    return items[0];
                else
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "No value approval could be found in " + list.Title + " using the amount " + strAmount + ". Value approval is skipped", string.Empty);
            }
            catch (Exception e)
            {
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "No value approval could be found in " + list.Title + " using the amount " + strAmount, string.Empty);
                throw new Exception();
            }
            return null;
        }
        #endregion
    }
}
