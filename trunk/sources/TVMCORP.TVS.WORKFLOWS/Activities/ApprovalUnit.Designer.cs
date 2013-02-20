using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using TVMCORP.TVS.WORKFLOWS.Activities;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public sealed partial class ApprovalUnit
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference2 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            this.TaskRejected = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.TaskApproved = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.IfRejected = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfApproved = new System.Workflow.Activities.IfElseBranchActivity();
            this.OnApprovalTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.ExecuteTaskEvent = new System.Workflow.Activities.IfElseActivity();
            this.SetTaskEventParameter = new System.Workflow.Activities.CodeActivity();
            this.ApprovalTaskMessageLog = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.OnCompleteApprovalTask = new Microsoft.SharePoint.WorkflowActions.CompleteTask();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            this.TaskCreatedEvent = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.ApprovalTaskCreated = new Microsoft.SharePoint.WorkflowActions.OnTaskCreated();
            this.CreateApprovalTask = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.InitData = new System.Workflow.Activities.CodeActivity();
            // 
            // TaskRejected
            // 
            this.TaskRejected.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.TaskRejected;
            this.TaskRejected.Name = "TaskRejected";
            activitybind1.Name = "ApprovalUnit";
            activitybind1.Path = "TaskRejected_Parameter";
            this.TaskRejected.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // TaskApproved
            // 
            this.TaskApproved.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.TaskApproved;
            this.TaskApproved.Name = "TaskApproved";
            activitybind2.Name = "ApprovalUnit";
            activitybind2.Path = "TaskApproved_Parameter";
            this.TaskApproved.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // IfRejected
            // 
            this.IfRejected.Activities.Add(this.TaskRejected);
            ruleconditionreference1.ConditionName = "TaskRejected";
            this.IfRejected.Condition = ruleconditionreference1;
            this.IfRejected.Name = "IfRejected";
            // 
            // IfApproved
            // 
            this.IfApproved.Activities.Add(this.TaskApproved);
            ruleconditionreference2.ConditionName = "TaskApproved";
            this.IfApproved.Condition = ruleconditionreference2;
            this.IfApproved.Name = "IfApproved";
            // 
            // OnApprovalTaskChanged
            // 
            activitybind3.Name = "ApprovalUnit";
            activitybind3.Path = "onTaskChanged1_AfterProperties1";
            activitybind4.Name = "ApprovalUnit";
            activitybind4.Path = "onTaskChanged1_BeforeProperties1";
            correlationtoken1.Name = "createTask";
            correlationtoken1.OwnerActivityName = "ApprovalUnit";
            this.OnApprovalTaskChanged.CorrelationToken = correlationtoken1;
            this.OnApprovalTaskChanged.Executor = null;
            this.OnApprovalTaskChanged.Name = "OnApprovalTaskChanged";
            activitybind5.Name = "ApprovalUnit";
            activitybind5.Path = "TaskId";
            this.OnApprovalTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChanged1_Invoked);
            this.OnApprovalTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.OnApprovalTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.OnApprovalTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // ExecuteTaskEvent
            // 
            this.ExecuteTaskEvent.Activities.Add(this.IfApproved);
            this.ExecuteTaskEvent.Activities.Add(this.IfRejected);
            this.ExecuteTaskEvent.Name = "ExecuteTaskEvent";
            // 
            // SetTaskEventParameter
            // 
            this.SetTaskEventParameter.Name = "SetTaskEventParameter";
            this.SetTaskEventParameter.ExecuteCode += new System.EventHandler(this.SetTaskEventParameter_ExecuteCode);
            // 
            // ApprovalTaskMessageLog
            // 
            this.ApprovalTaskMessageLog.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind6.Name = "ApprovalUnit";
            activitybind6.Path = "ApprovalTaskMessageLog_HistoryDescription";
            activitybind7.Name = "ApprovalUnit";
            activitybind7.Path = "approvalTask_TaskOutcome";
            this.ApprovalTaskMessageLog.Name = "ApprovalTaskMessageLog";
            activitybind8.Name = "ApprovalUnit";
            activitybind8.Path = "WorkflowProperties";
            this.ApprovalTaskMessageLog.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.ApprovalTaskMessageLog.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.ApprovalTaskMessageLog.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // OnCompleteApprovalTask
            // 
            this.OnCompleteApprovalTask.CorrelationToken = correlationtoken1;
            this.OnCompleteApprovalTask.Name = "OnCompleteApprovalTask";
            activitybind9.Name = "ApprovalUnit";
            activitybind9.Path = "TaskId";
            activitybind10.Name = "ApprovalUnit";
            activitybind10.Path = "approvalTask_TaskOutcome";
            this.OnCompleteApprovalTask.MethodInvoking += new System.EventHandler(this.approvalTask_MethodInvoking);
            this.OnCompleteApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.OnCompleteApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.OnApprovalTaskChanged);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsTaskNotCompleted);
            this.whileActivity1.Condition = codecondition1;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // TaskCreatedEvent
            // 
            this.TaskCreatedEvent.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.TaskCreated;
            this.TaskCreatedEvent.Name = "TaskCreatedEvent";
            activitybind11.Name = "ApprovalUnit";
            activitybind11.Path = "TaskCreatedEvent_Parameter";
            this.TaskCreatedEvent.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            // 
            // ApprovalTaskCreated
            // 
            activitybind12.Name = "ApprovalUnit";
            activitybind12.Path = "ApprovalTaskCreated_AfterProperties";
            this.ApprovalTaskCreated.CorrelationToken = correlationtoken1;
            this.ApprovalTaskCreated.Executor = null;
            this.ApprovalTaskCreated.Name = "ApprovalTaskCreated";
            this.ApprovalTaskCreated.TaskId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ApprovalTaskCreated.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.ApprovalTaskCreated_Invoked);
            this.ApprovalTaskCreated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskCreated.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // CreateApprovalTask
            // 
            activitybind13.Name = "ApprovalUnit";
            activitybind13.Path = "TaskContentTypeId";
            this.CreateApprovalTask.CorrelationToken = correlationtoken1;
            activitybind14.Name = "ApprovalUnit";
            activitybind14.Path = "CreatedTaskId";
            this.CreateApprovalTask.Name = "CreateApprovalTask";
            this.CreateApprovalTask.SpecialPermissions = null;
            activitybind15.Name = "ApprovalUnit";
            activitybind15.Path = "TaskId";
            activitybind16.Name = "ApprovalUnit";
            activitybind16.Path = "taskProperties";
            this.CreateApprovalTask.MethodInvoking += new System.EventHandler(this.createTaskWithContentType1_MethodInvoking);
            this.CreateApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.CreateApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.CreateApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.ContentTypeIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.CreateApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.ListItemIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // InitData
            // 
            this.InitData.Name = "InitData";
            this.InitData.ExecuteCode += new System.EventHandler(this.InitData_ExecuteCode);
            // 
            // ApprovalUnit
            // 
            this.Activities.Add(this.InitData);
            this.Activities.Add(this.CreateApprovalTask);
            this.Activities.Add(this.ApprovalTaskCreated);
            this.Activities.Add(this.TaskCreatedEvent);
            this.Activities.Add(this.whileActivity1);
            this.Activities.Add(this.OnCompleteApprovalTask);
            this.Activities.Add(this.ApprovalTaskMessageLog);
            this.Activities.Add(this.SetTaskEventParameter);
            this.Activities.Add(this.ExecuteTaskEvent);
            this.Name = "ApprovalUnit";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity SetTaskEventParameter;

        private TaskEventHandler TaskRejected;

        private TaskEventHandler TaskApproved;

        private IfElseBranchActivity IfRejected;

        private IfElseBranchActivity IfApproved;

        private IfElseActivity ExecuteTaskEvent;

        private TaskEventHandler TaskCreatedEvent;

        private CodeActivity InitData;

        private Microsoft.SharePoint.WorkflowActions.OnTaskCreated ApprovalTaskCreated;

        private CustomLogToWFHistoryActivity ApprovalTaskMessageLog;

        private Microsoft.SharePoint.WorkflowActions.CompleteTask OnCompleteApprovalTask;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged OnApprovalTaskChanged;

        private WhileActivity whileActivity1;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType CreateApprovalTask;



























































































































    }
}
