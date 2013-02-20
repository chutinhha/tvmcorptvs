using System;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.WORKFLOWS.Core.TaskActions;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
	public static class TaskActionFactory
	{
        public static ITaskActionHandler CreateTaskActionHandler(TaskActionTypes actionType)
        {
            switch (actionType)
            {
                case TaskActionTypes.SendEmailToStaticAddresses:
                    return new SendEmailToStaticAddresses();

                case TaskActionTypes.UpdateTaskPermission:
                    return new UpdateTaskPermission();

                case TaskActionTypes.UpdateWFPermission:
                    return new UpdateWFItemPermission();

                case TaskActionTypes.SendEmailToWorkflowItemUserColumn:
                    return new SendEmailToWorkflowItemUserColumn();
                case TaskActionTypes.SendEmailToWorkflowTaskUserColumn:
                    return new SendEmailtoWorkflowTaskUserColumn();
                case TaskActionTypes.UpdateWorkflowItemMetadata:
                    return new UpdateWorkflowItemMetadata();
                case TaskActionTypes.UpdateWorkflowTaskMetadata:
                    return new UpdateWorkflowTaskMetadata();

                //case TaskActionTypes.SendEmailWithESignMetadataToStaticAddresses:
                //    return new SendEmailWithESignMetadataToStaticAddresses();
                //case TaskActionTypes.SendEmailWithESignMetadataToWorkflowItemUserColumn:
                //    return new SendEmailWithESignMetadataToWorkflowItemUserColumn();
                //case TaskActionTypes.UpdateWorkflowItemWithESignMetadata:
                //    return new UpdateWorkflowItemWithESignMetadata();

                //case TaskActionTypes.SendEmailWithESignVariableToStaticAddresses:
                //    return new SendEmailWithESignVariableToStaticAddresses();

                //case TaskActionTypes.UpdateWorkflowItemWithEsignVariables:
                //    return new UpdateWorkflowItemWithESignVariables();

                //case TaskActionTypes.SendEmailWithESignVariableToWfItemUserColumn:
                //    return new SendEmailWithESignVariableToWorkflowItemUserColumn();
                case TaskActionTypes.UpdateExecutedDocumentMetadata:
                    return new UpdateExecutedDocumentMetadata();
                case TaskActionTypes.UpdateWorkflowItemWithKeyword:
                    return new UpdateWorkflowItemWithKeyword();
                case TaskActionTypes.UpdateTaskItemWithItemProperty:
                    return new UpdateTaskItemWithItemProperty();

                case TaskActionTypes.UpdateWorkflowItemWithTaskProperty:
                    return new UpdateWorkflowItemWithTaskProperty();
                case TaskActionTypes.CreateUnreadTask:
                    return new CreateUnreadTask();
                default:
                    throw new ArgumentOutOfRangeException("Action Type does not support: " + actionType);
            }
        }
	}
}
