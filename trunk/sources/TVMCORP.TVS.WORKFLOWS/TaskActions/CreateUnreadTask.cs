using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Extensions;
using System.Collections.Generic;
using System.Linq;
using TVMCORP.TVS.UTIL;


namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class CreateUnreadTask : ITaskActionHandler
    {
        #region ITaskActionHandler Members
        public virtual void Execute(TaskActionArgs actionData)
        {
            CreatUnreadTaskSettings setting = actionData.GetActionData<CreatUnreadTaskSettings>();
            var item = actionData.WorkflowProperties.Item;

            if (setting.UsePredefine)
            {
                UnreadContentNotificationSetting unreadSetting = item.GetCustomSettings<UnreadContentNotificationSetting>(TVMCORPFeatures.TVS);
                if (unreadSetting == null || unreadSetting.Enable == false) return;

                if (unreadSetting.EnableEmail)
                {
                    //UnreadContentReciever.SendNotificationEmail(unreadSetting.Template, item);
                }

                if (unreadSetting.EnableCreateUnreadTask)
                {
                    //UnreadContentReciever.CreateNotificationTask(unreadSetting.TitleFormula, item);
                }
            }

            
        }


        #endregion
    }
}
