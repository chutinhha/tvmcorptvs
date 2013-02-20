using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint.Workflow;
using System.Web.UI.WebControls;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.RESOURCES;
 

namespace TVMCORP.TVS.WORKFLOWS.Workflows
{
    public partial class ApprovalWFReasignTaskForm : TaskFormBase
    {
        public override string CurrentForm
        {
            get
            {
                return "ApprovalWFReasignTaskForm";
            }
           
        }
        protected override void OnInit(EventArgs e)
        {
            
            btnReject.Click += new EventHandler(base.ChangeForm_Command);
            btnReassign.Click += new EventHandler(btnReassign_Click);
            btnRequestInf.Click += new EventHandler(ChangeForm_Command);
            btnApprove.Click += new EventHandler(ChangeForm_Command);
            
            base.OnLoad(e);
        }

        void btnReassign_Click(object sender, EventArgs e)
        {
            Hashtable properties = CurrentTaskExtendedProperties;

            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Reasigned;
            foreach (PickerEntity entity in ppTargetUser.ResolvedEntities)
            {
                properties[TaskExtendProperties.STB_ASSIGN_TO] += entity.Key + ";";

            }
            properties[TaskExtendProperties.STB_TASK_COMMENTS] = txtMessage.Text.Trim();

            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Reasigned;
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();

        }
            
        //void ChangeForm_Command(object sender, EventArgs e)
        //{
        //    LinkButton button = sender as LinkButton;
        //    Response.Redirect(this.Request.RawUrl.Replace("ReassignWFTaskForm", button.CommandArgument.ToString()));
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;



            this.ltrView.Text = String.Format("<a href='{0}?ID={1}'>{2}</a>",
                                             CurrentWorkflowItem.ParentList.DefaultDisplayFormUrl, this.CurrentWorkflowItem["ID"], ApprovalWorkflowResources.TaskForm_ViewProperties);
            this.ltrEdit.Text = String.Format("<a href='{0}?ID={1}'>{2}</a>",
                                            CurrentWorkflowItem.ParentList.DefaultEditFormUrl, this.CurrentWorkflowItem["ID"], ApprovalWorkflowResources.TaskForm_EditProperties);

            Hashtable properties = CurrentTaskExtendedProperties;

            string status = properties[TaskExtendProperties.OWS_TASK_STATUS] as string;

            DisplayStatus(ltrStatus);

            if (properties[TaskExtendProperties.STB_MESS_TO_APPROVER] != null)
            {
                txtInitMessage.Text = properties[TaskExtendProperties.STB_MESS_TO_APPROVER].ToString();
            }
        }
    }
}
