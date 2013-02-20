using System;
using TVMCORP.TVS.UTIL.Extensions;

using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.MODELS;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class SendEmailtoWorkflowTaskUserColumn : SendEmailToStaticAddresses
	{
        public override void Execute(TaskActionArgs actionData)
        {
            SendEmailtoWorkflowTaskUserColumnSettings emailSettings = actionData.GetActionData<SendEmailtoWorkflowTaskUserColumnSettings>();

            SPListItem taskItem = actionData.WorkflowProperties.TaskList.GetItemById(emailSettings.TaskId);
            if (!taskItem.Fields.ContainFieldId(new Guid(emailSettings.FieldId)))
            {
                Utility.LogInfo("Field id " + emailSettings.FieldId + " not exist in workflow task", "Task Action");
                return;
            }
            string emails = SendEmailHelper.GetEmailFromFieldValue(taskItem, emailSettings.FieldId);
            if (string.IsNullOrEmpty(emails))
                return;

            emailSettings.EmailAddress = emails;
            
            base.Execute(actionData);
        }
	}
}
