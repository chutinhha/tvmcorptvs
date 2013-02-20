using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.Util.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
    public partial class MultiEntriesApproval : CoreApproval
    {
        public MultiEntriesApproval()
        {
            InitializeComponent();
        }

        public IList _approvalEntries = new List<string>();
        private string _entryApprovalListId;

        #region Dependency Properties
        public static DependencyProperty EntriesColumnProperty =
            DependencyProperty.Register("EntriesColumn",
            typeof(string), typeof(MultiEntriesApproval));

        [Description("Column defines Approval Entries")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string EntriesColumn
        {
            get { return ((string)(base.GetValue(EntriesColumnProperty))); }
            set { base.SetValue(EntriesColumnProperty, value); }
        }

        #endregion

        private void initialMultiEntriesApprovalData_ExecuteCode(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPList currentList = __ActivationProperties.Web.Lists.GetList(new Guid(__ListId), false);

                if (currentList.Fields.ContainsField(EntriesColumn))
                {
                    SPField entriesField = currentList.Fields.GetFieldByInternalName(EntriesColumn);

                    if (entriesField.Type == SPFieldType.Lookup)
                    {
                        _entryApprovalListId = ((SPFieldLookup)entriesField).LookupList;
                    }
                    else
                    {
                        __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Error: " + entriesField.Title + " is not a lookup field.", string.Empty);
                        return;
                    }

                    GenerateOutputValue(__ActivationProperties.Item);

                    getApprovalEntryNames(currentList);
                }
                else
                {
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Error: The lookup column doesn't exist in this list", string.Empty);
                }
            });
        }

        private void getApprovalEntryNames(SPList currentList)
        {
            SPListItem currentItem = currentList.GetItemById(__ListItem);
            if (currentItem[EntriesColumn] != null)
            {
                SPFieldLookupValueCollection entries = new SPFieldLookupValueCollection(currentItem[EntriesColumn].ToString());
                if (entries != null && entries.Count > 0)
                {
                    foreach (SPFieldLookupValue entry in entries)
                    {
                        _approvalEntries.Add(entry.LookupValue);
                    }
                    return;
                }
            }
            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, __ActivationProperties.Web.CurrentUser, "Skipped: There is no Approval Entry defined.", string.Empty);
        }

        private void initializedChild_ExecuteCode(object sender, ReplicatorChildEventArgs e)
        {
            EntryApproval entryApproval = (EntryApproval)e.Activity;
            entryApproval.ApprovalKey = e.InstanceData.ToString();//entryApprovalName
            entryApproval.ApprovalListId = _entryApprovalListId;
        }
    }
}
