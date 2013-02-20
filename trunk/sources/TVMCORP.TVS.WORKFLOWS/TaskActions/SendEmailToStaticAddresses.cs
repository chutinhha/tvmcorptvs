using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.MODELS;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
	public class SendEmailToStaticAddresses: ITaskActionHandler
	{
        #region ITaskActionHandler Members
        public virtual void Execute(TaskActionArgs actionData)
        {
            SendEmailToStaticAddressesSettings emailSettings = actionData.GetActionData<SendEmailToStaticAddressesSettings>();
            
            SPListItem taskItem = null;
            if (emailSettings.TaskId > 0)
                taskItem = actionData.WorkflowProperties.TaskList.GetItemById(emailSettings.TaskId);

            SPListItem emailTemplateItem = SendEmailHelper.GetEmailTemplateItem(actionData.WorkflowProperties, emailSettings.EmailTemplateUrl, emailSettings.EmailTemplateName);
            if (emailTemplateItem == null)
            {
                Utility.LogInfo("Cannot get email template name '" + emailSettings.EmailTemplateName + "' in list " + emailSettings.EmailTemplateUrl, "Task Action");
                return;
            }

            SPSite site = actionData.WorkflowProperties.Site;
            SPWeb rootWeb = site.RootWeb;
            SPUserCollection allUsers = rootWeb.AllUsers;
            string staticUserEmails = string.Empty;
            foreach(var userInfo in emailSettings.StaticUsers)
            {
                //TODO: get user's emails
            }

            SendEmailHelper.SendEmailbytemplate(actionData.WorkflowProperties.Item, taskItem, emailTemplateItem, emailSettings.EmailAddress + "," + staticUserEmails, emailSettings.AttachTaskLink);

        }
        #endregion
    }
}
