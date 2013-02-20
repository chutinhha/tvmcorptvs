using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.RESOURCES;
 

namespace TVMCORP.TVS.WORKFLOWS.Workflows
{
    public partial class ApprovalWFViewOnlyTaskForm : TaskFormBase
    {
        protected override void OnInit(EventArgs e)
        {
            btnApprove.Click += new EventHandler(btnApprove_Click);
            btnReject.Click += new EventHandler(btnReject_Click);
            btnReassign.Click += new EventHandler(btnReassign_Click);
            btnRequestInf.Click += new EventHandler(btnRequestInf_Click);
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

        void btnReassign_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.Request.RawUrl.Replace("ApprovalWFTaskForm", "ReassignWFTaskForm"));
        }

        void btnApprove_Click(object sender, EventArgs e)
        {

            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.STB_MESS_TO_APPROVER] = txtMessage.Text.Trim();
            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Approved;
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        void btnReject_Click(object sender, EventArgs e)
        {

            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Rejected;

            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack) return;



            this.ltrView.Text = String.Format("<a href='{0}?ID={1}'>{2}</a>",
                                             CurrentWorkflowItem.ParentList.DefaultDisplayFormUrl, this.CurrentWorkflowItem["ID"], ApprovalWorkflowResources.TaskForm_ViewProperties);
            this.ltrEdit.Text = String.Format("<a href='{0}?ID={1}'>{2}</a>",
                                            CurrentWorkflowItem.ParentList.DefaultEditFormUrl, this.CurrentWorkflowItem["ID"], ApprovalWorkflowResources.TaskForm_EditProperties);

            Hashtable properties = CurrentTaskExtendedProperties;


            DisplayStatus(ltrStatus);

            if (properties[TaskExtendProperties.STB_MESS_TO_APPROVER] != null)
            {
                txtInitMessage.Text = properties[TaskExtendProperties.STB_MESS_TO_APPROVER].ToString();
            }
        }

    }
}
