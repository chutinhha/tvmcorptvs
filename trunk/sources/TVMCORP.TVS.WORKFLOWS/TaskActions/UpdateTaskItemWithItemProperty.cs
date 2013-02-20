using System;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;


namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UpdateTaskItemWithItemProperty : ITaskActionHandler
	{
        #region ITaskActionHandler Members
        public void Execute(TaskActionArgs actionData)
        {
            UpdateTaskItemWithItemPropertySettings updateTaskItemSetting = actionData.GetActionData<UpdateTaskItemWithItemPropertySettings>();

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
                    //UpdateWorkflowItemHelper.DoUpdateItem(item, fieldUpdate, updateWFItemSettings.Value); 
                    if (updateTaskField.Type == itemField.Type)
                    {
                        taskItem[new Guid(updateTaskItemSetting.TaskFieldId)] = item[new Guid(updateTaskItemSetting.ItemFieldId)];
                    }
                    else
                    {
                        if (item[itemField.Id] != null)
                        {
                            UpdateWorkflowItemHelper.DoUpdateItem(taskItem, updateTaskField, item[itemField.Id].ToString());
                        }
                    }
                    taskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
                    taskItem.SystemUpdate();
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
