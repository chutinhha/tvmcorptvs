using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.WORKFLOWS.Core.TaskActions
{
    public class UpdateTaskPermission : ITaskActionHandler
    {
        #region ITaskActionHandler Members
        public virtual void Execute(TaskActionArgs actionData)
        {
            try
            {
                UpdateTaskPermissionSettings setting = actionData.GetActionData<UpdateTaskPermissionSettings>();

                SPListItem taskItem = null;
                if (setting.TaskId > 0)
                    taskItem = actionData.WorkflowProperties.TaskList.GetItemById(setting.TaskId);

                if (taskItem == null )
                    return;

                if (!taskItem.HasUniqueRoleAssignments)
                {
                    taskItem.BreakRoleInheritance(setting.KeepExisting);
                }
                else if (!setting.KeepExisting)
                {
                    taskItem.RemoveAllPermissions();
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
                    if (taskItem[new Guid(col)] != null)
                    {
                        var field = (SPFieldUser)taskItem.Fields[new Guid(col)];
                        if (field.AllowMultipleValues)
                        {
                            SPFieldUserValueCollection userCollection = (SPFieldUserValueCollection)taskItem[field.Id];
                            foreach (SPFieldUserValue user in userCollection)
                            {
                                loginNames.Add(user.User.LoginName);
                            }
                        }
                        else
                        {
                            var user = new SPFieldUserValue(taskItem.Web, taskItem[field.Id].ToString());
                            loginNames.Add(user.User.LoginName);
                        }
                    }
                }

                loginNames.AddRange(setting.StaticUsers);
                var role = taskItem.Web.RoleDefinitions.Cast<SPRoleDefinition>().FirstOrDefault(p => p.Id.ToString() == setting.RoleId);

                if (role != null)
                {
                    taskItem.SetPermissions(role.Name, loginNames);
                }
            }
            catch (Exception ex)
            {
                Utility.LogError(ex);

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
