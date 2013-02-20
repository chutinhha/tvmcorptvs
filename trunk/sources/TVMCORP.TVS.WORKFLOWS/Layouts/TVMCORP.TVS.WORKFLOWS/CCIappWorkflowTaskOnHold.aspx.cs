using System;
using TVMCORP.TVS.WORKFLOWS.MODELS;

using Microsoft.SharePoint;
using System.Collections;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.WORKFLOWS.Core.Pages;

namespace TVMCORP.TVS.WORKFLOWS.Layouts
{
    public partial class CCIappWorkflowTaskOnHold : TaskCorePage
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(Cancel_Click);
            btnOnHold.Click += new EventHandler(btnOnHold_Click);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        private void btnOnHold_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.CCI_TASK_STATUS] = Constants.Workflow.STATUS_ON_HOLD_TEXT;
            properties[TaskExtendProperties.CCI_COMMENT] = txtComment.Text;
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        private void loadData()
        {
            if (CurrentTaskItem[SPBuiltInFieldId.TaskDueDate] != null)
                lblDueBy.Text = Convert.ToDateTime(CurrentTaskItem[SPBuiltInFieldId.TaskDueDate]).ToShortDateString();

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION] != null)
                txtInstruction.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION];
        }
    }
}
