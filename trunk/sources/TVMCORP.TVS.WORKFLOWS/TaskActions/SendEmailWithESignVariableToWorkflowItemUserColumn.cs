using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Hypertek.IOffice.Workflow.TaskActions;
using Hypertek.IOffice.Model.Workflow;
using Hypertek.IOffice.Common.Helpers;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Common.Utilities;
using System;

namespace Hypertek.IOffice.Workflow.TaskActions
{
    public class SendEmailWithESignVariableToWorkflowItemUserColumn : SendEmailWithESignVariableToStaticAddresses
	{
        public override void Execute(TaskActionArgs actionData)
        {
            SendEmailWithESignVariableToWfItemUserColumnSettings emailSettings = actionData.GetActionData<SendEmailWithESignVariableToWfItemUserColumnSettings>();

            if (!actionData.WorkflowProperties.Item.Fields.ContainFieldId(new Guid(emailSettings.FieldId)))
            {
                CCIUtility.LogInfo("Field id " + emailSettings.FieldId + " not exist in workflow item", "Task Action");
                return;
            }

            string emails = SendEmailHelper.GetEmailFromFieldValue(actionData.WorkflowProperties.Item, emailSettings.FieldId);
            if (string.IsNullOrEmpty(emails))
                return;

            emailSettings.EmailAddress = emails;

            base.Execute(actionData);

        }
	}
}
