using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using WordDocumentGenerator.Library;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UploadExecuted : ITaskActionHandler
    {
        #region ITaskActionHandler Members
        public virtual void Execute(TaskActionArgs actionData)
        {
            try
            {
                UploadExecutedSettings setting = actionData.GetActionData<UploadExecutedSettings>();
                var item = actionData.WorkflowProperties.Item;
                DocumentGenerator generator = GeneratorFactory.GetGenerator(item, setting.TemplateFile);

                var document   = generator.GenerateDocument();

                
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
