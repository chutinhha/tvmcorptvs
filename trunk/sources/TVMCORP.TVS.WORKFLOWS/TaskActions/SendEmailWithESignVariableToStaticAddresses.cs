using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Hypertek.IOffice.Workflow.TaskActions;
using Hypertek.IOffice.Model.Workflow;
using Hypertek.IOffice.Common.Helpers;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Common.Utilities;

namespace Hypertek.IOffice.Workflow.TaskActions
{
    public class SendEmailWithESignVariableToStaticAddresses : ITaskActionHandler
	{
        public virtual void Execute(TaskActionArgs actionData)
        {
            SendEmailWithESignVariableToStaticAddressesSettings emailSettings = actionData.GetActionData<SendEmailWithESignVariableToStaticAddressesSettings>();

            SPListItem taskItem = null;
            if (emailSettings.TaskId > 0)
                taskItem = actionData.WorkflowProperties.TaskList.GetItemById(emailSettings.TaskId);

            SPListItem emailTemplateItem = SendEmailHelper.GetEmailTemplateItem(actionData.WorkflowProperties, emailSettings.EmailTemplateUrl, emailSettings.EmailTemplateName);
            if (emailTemplateItem == null)
            {
                CCIUtility.LogInfo("Cannot get email template name '" + emailSettings.EmailTemplateName + "' in list " + emailSettings.EmailTemplateUrl, "Task Action");
                return;
            }

            SendEmailHelper.SendEmailbytemplateEx(actionData.WorkflowProperties.Item, taskItem, emailSettings.Variables, emailSettings.ESignMetadata, emailTemplateItem, emailSettings.EmailAddress);
            actionData.WorkflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, "Email template " + emailSettings.EmailTemplateName + " has been successfully sent to " + emailSettings.EmailAddress, string.Empty);
        }
	}
}
