using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Workflows
{
    public partial class ReassignWFTaskForm : TaskFormBase
    {
        protected override void OnInit(EventArgs e)
        {
            btnApprove.Click += new EventHandler(btnApprove_Click);
            base.OnLoad(e);
        }


        void btnApprove_Click(object sender, EventArgs e)
        {
            Hashtable properties = CurrentTaskExtendedProperties;

            properties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Reasigned;
            foreach (PickerEntity entity in choiceUser.ResolvedEntities)
            {
                properties[TaskExtendProperties.CCI_ASSIGN_TO] += entity.Key + ";";

            }
            //properties[TaskExtendProperties.CCI_ASSIGN_TO] = choiceUser.ResolvedEntities

            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();

        }

        void btnReject_Click(object sender, EventArgs e)
        {

            Back();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
