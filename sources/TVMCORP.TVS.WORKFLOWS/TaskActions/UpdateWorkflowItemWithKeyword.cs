using System;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;
using System.Text.RegularExpressions;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UpdateWorkflowItemWithKeyword : ITaskActionHandler
	{
        #region ITaskActionHandler Members
        public void Execute(TaskActionArgs actionData)
        {
            UpdateWorkflowItemWithKeywordSettings updateWFItemSettings = actionData.GetActionData<UpdateWorkflowItemWithKeywordSettings>();

            if (!actionData.WorkflowProperties.Item.Fields.ContainFieldId(new Guid(updateWFItemSettings.FieldId)))
                return;

            SPField fieldUpdate = actionData.WorkflowProperties.Item.Fields[new Guid(updateWFItemSettings.FieldId)];
            if (!fieldUpdate.ReadOnlyField && !fieldUpdate.Hidden)
            {
                try
                {                    
                    SPListItem item = actionData.WorkflowProperties.Item;                    
                    switch (fieldUpdate.Type)
                    {
                        case SPFieldType.DateTime:
                            bool isConvertSuccessful = false;
                            DateTime updated = CovnertKeywordToDateTime(updateWFItemSettings.Value, out isConvertSuccessful);
                            if (isConvertSuccessful)
                            {
                                SPFieldDateTime fieldDate = (SPFieldDateTime)fieldUpdate;
                                item[fieldUpdate.Id] = updated;
                            }

                            break;
                        case SPFieldType.User:
                            if (updateWFItemSettings.Value.Trim().ToUpper() == "[ME]")
                            {
                                //UpdateWorkflowItemHelper.DoUpdateItem(item, fieldUpdate, actionData.WorkflowProperties.OriginatorUser.LoginName);  

                                SPFieldUser fieldUser = (SPFieldUser)fieldUpdate;
                                SPUser initiator = actionData.WorkflowProperties.OriginatorUser;

                                SPFieldUserValue userValue = new SPFieldUserValue(item.Web, initiator.ID, initiator.Name);
                                item[fieldUpdate.Id] = userValue;

                            }
                            break;

                    }

                    item[SPBuiltInFieldId.WorkflowVersion] = 1;
                    item.SystemUpdate();
                }
                catch
                {
                    Utility.LogInfo("Error update workfkow item field " + fieldUpdate.Title + " with data " + updateWFItemSettings.Value + " is error", "TVMCORP.TVS.WORKFLOWS");
                }
            }
        }

        private DateTime CovnertKeywordToDateTime(string source, out bool isConvertSuccessful)
        {
            source = source.Trim();
            isConvertSuccessful = false;
            try
            {
                string pattern = @"\[Today\]\s*[+-]*\s*\d*";

                if (Regex.IsMatch(source, pattern))
                {
                    DateTime result = DateTime.Now;

                    string dayAdd = source.Substring(7);
                    if (!string.IsNullOrEmpty(dayAdd))
                    {
                        dayAdd = dayAdd.Replace(" ", string.Empty);
                        int days = Convert.ToInt32(dayAdd);
                        result =  result.AddDays(days);
                    }
                    isConvertSuccessful = true;
                    return result;
                }
            }
            catch (Exception ex)
            {

                Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
            }

            return DateTime.MaxValue;
        }
        #endregion
    }
}
