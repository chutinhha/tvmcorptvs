using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UpdateWFItemPermission : ITaskActionHandler
    {
        #region ITaskActionHandler Members
        public virtual void Execute(TaskActionArgs actionData)
        {
            try
            {
                UpdateWFItemPermissionSettings setting = actionData.GetActionData<UpdateWFItemPermissionSettings>();
                var item = actionData.WorkflowProperties.Item;

                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(setting.KeepExisting);
                }
                else if (!setting.KeepExisting)
                {
                    item.RemoveAllPermissions();
                }

                List<string> loginNames = new List<string>();

                if (setting.AllParticipiants)
                {
                    SPWorkflow workflowInstance = actionData.WorkflowProperties.Workflow;
                    SPWorkflowTaskCollection taskCollection = GetWorkflowTasks(workflowInstance);
                    for (int i = 0; i <= taskCollection.Count; i++)
                    {
                        var task = taskCollection[i];
                        string assignedToValue = task[SPBuiltInFieldId.AssignedTo].ToString();
                        SPFieldUserValue userField = (SPFieldUserValue)task.Fields[SPBuiltInFieldId.AssignedTo].GetFieldValue(assignedToValue);
                        SPUser user = userField.User;

                        loginNames.Add(taskCollection[i][SPBuiltInFieldId.AssignedTo].ToString());
                    }
                }

                foreach (var col in setting.Columns)
                {
                    if (item[new Guid(col)] != null)
                    {
                        var field = (SPFieldUser)item.Fields[new Guid(col)];
                        if (field.AllowMultipleValues)
                        {
                            SPFieldUserValueCollection userCollection = (SPFieldUserValueCollection)item[field.Id];
                            foreach (SPFieldUserValue user in userCollection)
                            {
                                loginNames.Add(user.User.LoginName);
                            }
                        }
                        else
                        {
                            var user = new SPFieldUserValue(item.Web, item[field.Id].ToString());
                            loginNames.Add(user.User.LoginName);
                        }
                    }
                }

                loginNames.AddRange(setting.StaticUsers);
                var role = item.Web.RoleDefinitions.Cast<SPRoleDefinition>().FirstOrDefault(p => p.Id.ToString() == setting.RoleId);

                if (role != null)
                {
                    item.SetPermissions(role.Name, loginNames);
                }
            }
            catch (Exception ex)
            {


            }

        }
        #endregion

        public static SPWorkflowTaskCollection GetWorkflowTasks(SPWorkflow workflowInstance)
        {
            SPWorkflowTaskCollection taskCollection = null;
            bool tasksPopulated = false;
            while (!tasksPopulated)
            {
                try
                {
                    taskCollection = workflowInstance.Tasks;
                    tasksPopulated = true;
                }
                catch { }
            }

            return taskCollection;
        }
    }
}
