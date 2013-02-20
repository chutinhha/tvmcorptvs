using System;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;


namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
	public class UpdateWorkflowItemMetadata: ITaskActionHandler
	{
        #region ITaskActionHandler Members
        public void Execute(TaskActionArgs actionData)
        {
            UpdateWorkflowItemMetadataSettings updateWFItemSettings = actionData.GetActionData<UpdateWorkflowItemMetadataSettings>();

            if (!actionData.WorkflowProperties.Item.Fields.ContainFieldId(new Guid(updateWFItemSettings.FieldId)))
                return;

            SPField fieldUpdate = actionData.WorkflowProperties.Item.Fields[new Guid(updateWFItemSettings.FieldId)];
            if (!fieldUpdate.ReadOnlyField && !fieldUpdate.Hidden)
            {
                try
                {                    
                    SPListItem item = actionData.WorkflowProperties.Item;                    
                    UpdateWorkflowItemHelper.DoUpdateItem(item, fieldUpdate, updateWFItemSettings.Value);                    
                    item[SPBuiltInFieldId.WorkflowVersion] = 1;
                    item.SystemUpdate();
                }
                catch
                {
                    Utility.LogInfo("Error update workfkow item field " + fieldUpdate.Title + " with data " + updateWFItemSettings.Value + " is error", "TVMCORP.TVS.WORKFLOWS");
                }
            }
        }
        #endregion
    }
}
