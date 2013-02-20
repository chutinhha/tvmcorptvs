using System.ComponentModel;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;

using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class TaskEventHandler : Activity
    {
        #region Properties
        public static DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter",
            typeof(TaskEventHandlerParameter), typeof(TaskEventHandler));
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TaskEventHandlerParameter Parameter
        {
            get
            {
                return ((TaskEventHandlerParameter)(base.GetValue(ParameterProperty)));
            }
            set
            {
                base.SetValue(TaskEventHandler.ParameterProperty, value);
            }
        }

        public static DependencyProperty EventTypeProperty =
           DependencyProperty.Register("EventType",
           typeof(TaskEventTypes), typeof(TaskEventHandler));

        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TaskEventTypes EventType
        {
            get { return ((TaskEventTypes)(base.GetValue(EventTypeProperty))); }
            set { base.SetValue(EventTypeProperty, value); }
        }
        #endregion

        public TaskEventHandler()
        {
            InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            TaskEventSetting taskEvent = null;
            if (this.Parameter.EventSettings != null && this.Parameter.EventSettings.Events != null)
            { 
                taskEvent = this.Parameter.EventSettings.Events.FirstOrDefault(t => t.Type == EventType);
                ExecuteActions(taskEvent);
            }
            return base.Execute(executionContext);
        }

        private void ExecuteActions(TaskEventSetting taskEvent)
        {
            if (taskEvent == null) return;

            foreach (TaskActionSettings taskAction in taskEvent.Actions)
            {
                ITaskActionHandler action = TaskActionFactory.CreateTaskActionHandler(taskAction.Type);
                switch (taskAction.Type)
                {
                    case TaskActionTypes.UpdateTaskItemWithItemProperty:
                        ((UpdateTaskItemWithItemPropertySettings)taskAction).TaskId = this.Parameter.TaskId;
                        break;
                    case TaskActionTypes.UpdateWorkflowItemWithTaskProperty:
                        ((UpdateWorkflowItemWithTaskPropertySettings)taskAction).TaskId = this.Parameter.TaskId;
                        break;

                    case TaskActionTypes.SendEmailToStaticAddresses:
                    case TaskActionTypes.SendEmailToWorkflowItemUserColumn:
                    case TaskActionTypes.SendEmailToWorkflowTaskUserColumn:
                        ((SendEmailToStaticAddressesSettings)taskAction).TaskId = this.Parameter.TaskId;
                        break;

                    //case TaskActionTypes.SendEmailWithESignMetadataToStaticAddresses:
                    //case TaskActionTypes.SendEmailWithESignMetadataToWorkflowItemUserColumn:
                    //    ((SendEmailWithESignMetadataToStaticAddressesSettings)taskAction).ESignMetadata = this.Parameter.ESignMetadata;
                    //    break;

                    case TaskActionTypes.UpdateWorkflowTaskMetadata:
                        ((UpdateWorkflowTaskMetadataSettings)taskAction).TaskId = this.Parameter.TaskId;
                        break;

                    //case TaskActionTypes.UpdateWorkflowItemWithESignMetadata:
                    //    ((UpdateWorkflowItemWithESignMetadataSettings)taskAction).ESignMetadata = this.Parameter.ESignMetadata;
                    //    break;

                    //case TaskActionTypes.SendEmailWithESignVariableToStaticAddresses:
                    //    ((SendEmailWithESignVariableToStaticAddressesSettings)taskAction).Variables = this.Parameter.Variables;
                    //    ((SendEmailWithESignVariableToStaticAddressesSettings)taskAction).ESignMetadata = this.Parameter.ESignMetadata;
                    //    break;

                    //case TaskActionTypes.SendEmailWithESignVariableToWfItemUserColumn:
                    //    ((SendEmailWithESignVariableToWfItemUserColumnSettings)taskAction).Variables = this.Parameter.Variables;
                    //    ((SendEmailWithESignVariableToWfItemUserColumnSettings)taskAction).ESignMetadata = this.Parameter.ESignMetadata;
                    //    break;
                    //case TaskActionTypes.UpdateWorkflowItemWithEsignVariables:
                    //    ((UpdateWorkflowItemWithESignVariablesSettings)taskAction).Variables = this.Parameter.Variables;
                    //    ((UpdateWorkflowItemWithESignVariablesSettings)taskAction).ESignMetadata = this.Parameter.ESignMetadata;
                    //    break;
                    case TaskActionTypes.UpdateExecutedDocumentMetadata:
                        ((UpdateExecutedDocumentMetaDataEditorSettings)taskAction).DestinationListUrl = this.Parameter.DestinationListUrl;
                        ((UpdateExecutedDocumentMetaDataEditorSettings)taskAction).DestinationItemId = this.Parameter.DestinationItemId;
                        break;
                    case TaskActionTypes.UpdateWFPermission:

                        break;
                    case TaskActionTypes.UpdateTaskPermission:
                        ((UpdateTaskPermissionSettings)taskAction).TaskId = this.Parameter.TaskId;
                        break;
                }

                TaskActionArgs taskArg = new TaskActionArgs(taskAction, this.Parameter.WorkflowProperties);
                try
                {
                    action.Execute(taskArg);
                }
                catch (System.Exception)
                {

                    Utility.LogInfo("There is an error occur when execute action", TVMCORPFeatures.TVS);
                    //throw;
                }
               
            }
        }
    }
}
