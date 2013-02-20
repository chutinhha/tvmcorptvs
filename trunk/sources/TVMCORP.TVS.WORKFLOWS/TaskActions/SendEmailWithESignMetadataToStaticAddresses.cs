using System;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Model;
using Hypertek.IOffice.Common.Utilities;
using Hypertek.IOffice.Common.Helpers;
using Microsoft.SharePoint;
using Hypertek.IOffice.Model.Workflow;

namespace Hypertek.IOffice.Workflow.TaskActions
{
    public class SendEmailWithESignMetadataToStaticAddresses : ITaskActionHandler
	{
        public virtual void Execute(TaskActionArgs actionData)
        {
            SendEmailWithESignMetadataToStaticAddressesSettings emailSettings = actionData.GetActionData<SendEmailWithESignMetadataToStaticAddressesSettings>();

            SPListItem taskItem = null;
            if (emailSettings.TaskId > 0)
                taskItem = actionData.WorkflowProperties.TaskList.GetItemById(emailSettings.TaskId);

            SPListItem emailTemplateItem = SendEmailHelper.GetEmailTemplateItem(actionData.WorkflowProperties, emailSettings.EmailTemplateUrl, emailSettings.EmailTemplateName);
            if (emailTemplateItem == null)
            {
                CCIUtility.LogInfo("Cannot get email template name '" + emailSettings.EmailTemplateName + "' in list " + emailSettings.EmailTemplateUrl, "Task Action");
                return;
            }

            SendEmailHelper.SendEmailbytemplate(actionData.WorkflowProperties.Item, taskItem, emailSettings.ESignMetadata, emailTemplateItem, emailSettings.EmailAddress);
        }
	}
}
