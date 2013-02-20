using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Hypertek.IOffice.Workflow.TaskActions;
using Hypertek.IOffice.Model.Workflow;
using Hypertek.IOffice.Common.Helpers;
using Hypertek.IOffice.Common.Utilities;
using System;
using System.Linq;

namespace Hypertek.IOffice.Workflow.TaskActions
{
    public class UpdateWorkflowItemWithESignVariables : ITaskActionHandler
	{
        private SPListItem getReferenceItem(SPListItem item, SPField field)
        {
            return null;
        }

        public void Execute(TaskActionArgs actionData)
        {
            UpdateWorkflowItemWithESignVariablesSettings updateWFItemSettings = actionData.GetActionData<UpdateWorkflowItemWithESignVariablesSettings>();


            SPField fieldUpdate = actionData.WorkflowProperties.Item.Fields[new Guid(updateWFItemSettings.FieldId)];
            if (!fieldUpdate.ReadOnlyField && !fieldUpdate.Hidden)
            {
                try
                {
                    var value = updateWFItemSettings.Variables.Where(p => p.Name == updateWFItemSettings.VariableName).FirstOrDefault();

                    if (value != null)
                    {
                        SPListItem item = actionData.WorkflowProperties.Item;
                        
                        if (fieldUpdate.Type == SPFieldType.Lookup) { 
                            //TODO - Setting value for lookup field.
                            if (string.IsNullOrEmpty(value.Value))
                            {
                                item[new Guid(updateWFItemSettings.FieldId)] = null;
                            }
                            else
                            {

                                SPFieldLookup lookupField = fieldUpdate as SPFieldLookup;



                                using (SPSite site = new SPSite(actionData.WorkflowProperties.SiteId))
                                using (SPWeb web = site.OpenWeb(lookupField.LookupWebId))
                                {
                                    SPList lookupList = web.Lists[new Guid(lookupField.LookupList)];
                                    if (lookupList != null)
                                    {

                                        SPQuery query = new SPQuery();
                                        query.Query = "<Where>" +
                                            //"<And>" +
                                                                    "<Eq><FieldRef Name='" + lookupField.LookupField + "'/><Value Type='Text'>" + value.Value + "</Value></Eq>" +
                                            //"</And>" +
                                                       "</Where>";
                                        SPListItemCollection items = lookupList.GetItems(query);

                                        SPItem matchedItem = items.Cast<SPListItem>().FirstOrDefault();
                                        if (matchedItem != null)
                                        {
                                            SPFieldLookupValue lookupValue = new SPFieldLookupValue()
                                            {
                                                LookupId = matchedItem.ID
                                            };
                                            item[new Guid(updateWFItemSettings.FieldId)] = lookupValue;
                                        }
                                    }

                                }
                            }

                        }
                        else
                        {
                            item[new Guid(updateWFItemSettings.FieldId)] = value.Value;
                        }
                        item.SystemUpdate();
                    }
                }
                catch
                {
                    CCIUtility.LogError("Error update workfkow item field " + fieldUpdate.Title + " with data " + updateWFItemSettings.VariableName + " is error", "Hypertek.IOffice.Workflow");
                }
                CCIUtility.LogError("Exit UpdateWorkflowItemWithESignVariables", Model.IOfficeFeatures.CCIappDocuSign);
            }           
        }
	}
}
