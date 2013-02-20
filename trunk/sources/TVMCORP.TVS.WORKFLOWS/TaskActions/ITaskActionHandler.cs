using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.MODELS;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
	public interface ITaskActionHandler
	{
        void Execute(TaskActionArgs actionData);
	}

    public class TaskActionArgs
    {
        private SPWorkflowActivationProperties _workflowProperties;
        private TaskActionSettings _data;

        public TaskActionArgs(TaskActionSettings actionData)
        {
            _data = actionData;
        }

        public TaskActionArgs(TaskActionSettings actionData, SPWorkflowActivationProperties workflowProperties)
        {
            _data = actionData;
            _workflowProperties = workflowProperties;
        }
        
        public T GetActionData<T>() where T : TaskActionSettings
        {
            return (T)_data;
        }

        public SPWorkflowActivationProperties WorkflowProperties
        {
            get
            {
                return _workflowProperties;
            }
        }
    }
}
