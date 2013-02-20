using System;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint.Workflow;
using System.Web.UI.WebControls;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.RESOURCES;

namespace TVMCORP.TVS.WORKFLOWS.Core.Workflows
{
    public partial class ApprovalWFTaskForm : TaskFormBase
    {
        protected override void OnInit(EventArgs e)
        {
            btnApprove.Click += new EventHandler(btnApprove_Click);
            btnReject.Click += new EventHandler(btnReject_Click);
            
            btnRequestChange.Click += new EventHandler(ChangeForm_Command);
            btnReassign.Click += new EventHandler(ChangeForm_Command);
            btnRequestInf.Click += new EventHandler(ChangeForm_Command);

            updatedFieldsIterator.ControlMode = SPControlMode.Edit;
            updatedFieldsIterator.ListId = CurrentWorkflowItem.ParentList.ID;
            updatedFieldsIterator.ItemId = CurrentWorkflowItem.ID;
            string excludedFields = "";

            if (PropertiesToUpdated != null)
            {
                var fields = CurrentWorkflowItem.Fields.Cast<SPField>().Where(p => !p.Hidden && p.Type != SPFieldType.Computed &&  !this.PropertiesToUpdated.Contains(p.Id.ToString())).ToList();
                foreach (var item in fields)
                {
                    excludedFields += item.Title + ";#";
                }

                updatedFieldsIterator.ExcludeFields = excludedFields.TrimEnd(";#".ToArray());
            }
            else
            {
                updatedFieldsIterator.Visible = false;
            }
            base.OnLoad(e);
        }

        void btnRequestInf_Click(object sender, EventArgs e)
        {
            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.RequestInf;
            properties[TaskExtendProperties.STB_MESS_TO_APPROVER] = txtMessage.Text.Trim();

            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }



        public override string CurrentForm
        {
            get { return "ApprovalWFTaskForm"; }
        }
        void btnApprove_Click(object sender, EventArgs e)
        {
            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.STB_MESS_TO_APPROVER] = txtMessage.Text.Trim();
            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Approved;
            properties[TaskExtendProperties.STB_TASK_COMMENTS] = txtMessage.Text.Trim();
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();

            UpdateCurrentItem();
            Back();
        }

        private void UpdateCurrentItem()
        {
            if (updatedFieldsIterator.Visible == false) return;
           // var  item = updatedFieldsIterator.Item;
            updatedFieldsIterator.ListItem[TVSColumnIds.LastUpdatedByWF] = true;
            updatedFieldsIterator.ListItem.Update();
        }

        void btnReject_Click(object sender, EventArgs e)
        {

            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.STB_MESS_TO_APPROVER] = txtMessage.Text.Trim();
            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Rejected;
            properties[TaskExtendProperties.STB_TASK_COMMENTS] = txtMessage.Text.Trim();
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            UpdateCurrentItem();
            Back();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

             
           this.ltrView.Text = String.Format("<a href='{0}?ID={1}'>{2}</a>",
                                            CurrentWorkflowItem.ParentList.DefaultDisplayFormUrl, this.CurrentWorkflowItem["ID"], ApprovalWorkflowResources.TaskForm_ViewProperties);
            this.ltrEdit.Text = String.Format("<a href='{0}?ID={1}'>{2}</a>",
                                            CurrentWorkflowItem.ParentList.DefaultEditFormUrl, this.CurrentWorkflowItem["ID"], ApprovalWorkflowResources.TaskForm_EditProperties);
            Hashtable properties = CurrentTaskExtendedProperties;

            string status = properties[TaskExtendProperties.OWS_TASK_STATUS] as string;

            if (status == TaskApprovalStatus.Approved || status == TaskApprovalStatus.Rejected)
            {
                ltrStatus.Text = "Approved";
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            else if (status == TaskApprovalStatus.Rejected)
            {
                ltrStatus.Text = "Rejected";
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            else
            {
                ltrStatus.Text = "Pendding";
            }

            if (properties[TaskExtendProperties.STB_MESS_TO_APPROVER] != null)
            {
                txtInitMessage.Text = properties[TaskExtendProperties.STB_MESS_TO_APPROVER].ToString();
            }
        }
    }
}
