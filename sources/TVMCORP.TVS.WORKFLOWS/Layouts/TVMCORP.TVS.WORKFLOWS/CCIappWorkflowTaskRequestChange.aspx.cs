using System;
using System.Collections;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.Core.Pages;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Layouts
{
    public partial class CCIappWorkflowTaskRequestChange : TaskCorePage
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(Cancel_Click);
            btnSend.Click += new EventHandler(btnSend_Click);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            Hashtable properties = CurrentTaskExtendedProperties;
            PickerEntity entity = (PickerEntity)peditRequest.ResolvedEntities[0];
            properties[TaskExtendProperties.CCI_REQUEST_TO] = entity.EntityData[PeopleEditorEntityDataKeys.AccountName];
            properties[TaskExtendProperties.CCI_TASK_STATUS] = Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT;
            properties[TaskExtendProperties.CCI_COMMENT] = txtInstruction.Text;
            if (dtDueBy.IsValid && !dtDueBy.IsDateEmpty)
            {
                properties[TaskExtendProperties.CCI_NEW_DUEDATE] = dtDueBy.SelectedDate.ToShortDateString();
            }
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        private void loadData()
        {
            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION] != null)
                txtInstruction.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION];

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_DUEDATE_CHANGE_ON_REQUEST_INFORMATION] != null &&
                Convert.ToBoolean((string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_DUEDATE_CHANGE_ON_REQUEST_INFORMATION]))
            {
                divDueDate.Visible = true;
            }
        }
    }
}
