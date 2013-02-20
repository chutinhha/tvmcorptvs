using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.WORKFLOWS.Core.Pages;
using TVMCORP.TVS.WORKFLOWS.MODELS;

namespace TVMCORP.TVS.WORKFLOWS.Layouts
{
    public partial class CCIappWorkflowTaskDisplay : TaskCorePage
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(Cancel_Click);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        private void loadData()
        {
            if (CurrentTaskItem[SPBuiltInFieldId.TaskDueDate] != null)
                lblDueBy.Text = Convert.ToDateTime(CurrentTaskItem[SPBuiltInFieldId.TaskDueDate]).ToShortDateString();

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS] != null)
                lblStatus.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS];
            
            if (CurrentTaskItem[SPBuiltInFieldId.WorkflowOutcome] != null)
                lblOutCome.Text = (string)CurrentTaskItem[SPBuiltInFieldId.WorkflowOutcome];

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION] != null)
                txtInstruction.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION];

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_COMMENT] != null)
                txtComment.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_COMMENT];
        }
    }
}
