using System;
using System.Linq;
using System.Reflection;
using Hypertek.IOffice.Model;
using Hypertek.IOffice.Common.Utilities;
using Microsoft.SharePoint;
using Hypertek.IOffice.Model.Workflow;

namespace Hypertek.IOffice.Workflow.TaskActions
{
    public class UpdateWorkflowItemWithESignMetadata : ITaskActionHandler
	{
        public void Execute(TaskActionArgs actionData)
        {
            UpdateWorkflowItemWithESignMetadataSettings updateWFItemSettings = actionData.GetActionData<UpdateWorkflowItemWithESignMetadataSettings>();
            Type typeESign = updateWFItemSettings.ESignMetadata.GetType();            
            PropertyInfo property = typeESign.GetProperties().FirstOrDefault(p => string.Compare(p.Name, updateWFItemSettings.ESignProperty, true) == 0);
            if (property == null)
                return;
            object value = property.GetValue(updateWFItemSettings.ESignMetadata, null);

            SPField fieldUpdate = actionData.WorkflowProperties.Item.Fields[new Guid(updateWFItemSettings.FieldId)];
            if (!fieldUpdate.ReadOnlyField && !fieldUpdate.Hidden)
            {
                try
                {
                    SPListItem item = actionData.WorkflowProperties.Item;
                    item[new Guid(updateWFItemSettings.FieldId)] = value;
                    item.SystemUpdate();
                }
                catch
                {
                    CCIUtility.LogInfo("Error update workfkow item field " + fieldUpdate.Title + " with data " + value + " is error", "Hypertek.IOffice.Workflow");
                }
            }           
        }
	}
}
