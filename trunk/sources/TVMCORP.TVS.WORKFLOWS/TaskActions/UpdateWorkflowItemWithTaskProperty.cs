using System;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UpdateWorkflowItemWithTaskProperty : ITaskActionHandler
	{
        #region ITaskActionHandler Members
        public void Execute(TaskActionArgs actionData)
        {
            UpdateWorkflowItemWithTaskPropertySettings updateTaskItemSetting = actionData.GetActionData<UpdateWorkflowItemWithTaskPropertySettings>();

            if (!actionData.WorkflowProperties.Item.Fields.ContainFieldId(new Guid(updateTaskItemSetting.ItemFieldId)))
                return;

            SPListItem taskItem = null;
            if (updateTaskItemSetting.TaskId > 0)
                taskItem = actionData.WorkflowProperties.TaskList.GetItemById(updateTaskItemSetting.TaskId);

            if (taskItem == null || !taskItem.Fields.ContainFieldId(new Guid(updateTaskItemSetting.TaskFieldId))) 
                return;

            SPField updateTaskField = taskItem.Fields[new Guid(updateTaskItemSetting.TaskFieldId)];
            SPField itemField = actionData.WorkflowProperties.Item.Fields[new Guid(updateTaskItemSetting.ItemFieldId)];

            if (!updateTaskField.ReadOnlyField && !updateTaskField.Hidden)
            {
                try
                {                    
                    SPListItem item = actionData.WorkflowProperties.Item;                    
                    if (updateTaskField.Type == itemField.Type)
                    {
                        item[new Guid(updateTaskItemSetting.ItemFieldId)] = taskItem[new Guid(updateTaskItemSetting.TaskFieldId)];
                    }
                    else
                    {
                        if (taskItem[updateTaskField.Id] != null)
                        {
                            UpdateWorkflowItemHelper.DoUpdateItem(item, itemField, taskItem[updateTaskField.Id].ToString());
                        }
                    }
                    item[SPBuiltInFieldId.WorkflowVersion] = 1;
                    item.SystemUpdate();
                }
                catch
                {
                   // Utility.LogInfo("Error update workfkow item field " + fieldUpdate.Title + " with data " + updateWFItemSettings.Value + " is error", "TVMCORP.TVS.WORKFLOWS");
                }
            }
        }
        #endregion
    }
}
