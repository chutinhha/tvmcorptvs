using System;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Helpers;
using TVMCORP.TVS.UTIL.MODELS;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
	public class SendEmailToWorkflowItemUserColumn: SendEmailToStaticAddresses
	{
        public override void Execute(TaskActionArgs actionData)
        {
            SendEmailToWfItemUserColumnSettings emailSettings = actionData.GetActionData<SendEmailToWfItemUserColumnSettings>();

            if (!actionData.WorkflowProperties.Item.Fields.ContainFieldId(new Guid(emailSettings.FieldId)))
            {
                Utility.LogInfo("Field id " + emailSettings.FieldId + " not exist in workflow item" , "Task Action");
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
