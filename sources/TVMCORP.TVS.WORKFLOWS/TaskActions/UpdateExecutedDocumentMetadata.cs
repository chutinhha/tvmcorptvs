using System;
using System.Linq;
using System.Reflection;
using TVMCORP.TVS.UTIL.Utilities;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;


namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UpdateExecutedDocumentMetadata : ITaskActionHandler
	{
        #region ITaskActionHandler Members
        public void Execute(TaskActionArgs actionData)
        {
            UpdateExecutedDocumentMetaDataEditorSettings updateExcuteDocSettings = actionData.GetActionData<UpdateExecutedDocumentMetaDataEditorSettings>();

            SPList list = actionData.WorkflowProperties.GetListFromURL(updateExcuteDocSettings.DestinationListUrl);
            SPListItem item = list.GetItemById(updateExcuteDocSettings.DestinationItemId);

            if (!item.Fields.ContainFieldId(new Guid(updateExcuteDocSettings.FieldId)))
                return;

            SPField fieldUpdate = item.Fields[new Guid(updateExcuteDocSettings.FieldId)];
            if (!fieldUpdate.ReadOnlyField && !fieldUpdate.Hidden)
            {
                try
                {
                    UpdateWorkflowItemHelper.DoUpdateItem(item, fieldUpdate, updateExcuteDocSettings.Value);
                    item[SPBuiltInFieldId.WorkflowVersion] = 1;
                    item.SystemUpdate();
                }
                catch
                {
                    Utility.LogInfo("Error update workfkow item field " + fieldUpdate.Title + " with data " + updateExcuteDocSettings.Value + " is error", "TVMCORP.TVS.WORKFLOWS");
                }
            }
        }
        #endregion
	}
}
