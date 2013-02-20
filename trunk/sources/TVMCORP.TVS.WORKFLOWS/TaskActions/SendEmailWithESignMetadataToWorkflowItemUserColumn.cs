using System;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Common.Utilities;
using Hypertek.IOffice.Common.Helpers;
using Hypertek.IOffice.Model.Workflow;

namespace Hypertek.IOffice.Workflow.TaskActions
{
    public class SendEmailWithESignMetadataToWorkflowItemUserColumn : SendEmailWithESignMetadataToStaticAddresses
	{
        public override void Execute(TaskActionArgs actionData)
        {
            SendEmailWithESignMetadataToWfItemUserColumnSettings emailSettings = actionData.GetActionData<SendEmailWithESignMetadataToWfItemUserColumnSettings>();

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
