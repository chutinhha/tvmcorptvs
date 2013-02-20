using System;
using System.Collections;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.Core.Pages;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.Utilities;

namespace TVMCORP.TVS.WORKFLOWS.Layouts
{
    public partial class CCIappWorkflowTaskChange : TaskCorePage
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(Cancel_Click);
            btnSendResponse.Click += new EventHandler(btnSendResponse_Click);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        protected void btnSendResponse_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.CCI_TASK_STATUS] = Constants.Workflow.STATUS_FINISHED_TEXT;
            properties[TaskExtendProperties.CCI_COMMENT] = txtComment.Text;
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        private void loadData()
        {
            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION] != null)
                txtInstruction.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION];
           
            if (CurrentTaskItem[SPBuiltInFieldId.TaskDueDate] != null)
                lblDueBy.Text = Convert.ToDateTime(CurrentTaskItem[SPBuiltInFieldId.TaskDueDate]).ToShortDateString();

            hplReassign.NavigateUrl = Request.RawUrl.Replace(CCIappWorkflowTaskView.CHANGE, CCIappWorkflowTaskView.REASSIGN) + "&Source=" + SPEncode.UrlEncode(Request.RawUrl);
        }
    }
}
