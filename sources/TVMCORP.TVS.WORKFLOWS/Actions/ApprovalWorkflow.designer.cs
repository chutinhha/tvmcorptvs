using System.Workflow.Activities;
using TVMCORP.TVS.WORKFLOWS.Activities;

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
    public partial class ApprovalWorkflow
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            this.updateAllTasks = new System.Workflow.Activities.CodeActivity();
            this.delayOneMinute = new System.Workflow.Activities.DelayActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.isMustUpdateAllTasks = new System.Workflow.Activities.IfElseBranchActivity();
            this.taskApproval1 = new TVMCORP.TVS.WORKFLOWS.Activities.TaskApproval();
            this.terminateWF = new System.Workflow.ComponentModel.TerminateActivity();
            this.setTerminateLog = new System.Workflow.Activities.CodeActivity();
            this.ignoreLog = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.setIgnoreTasks = new System.Workflow.Activities.CodeActivity();
            this.checkUpdateAllTasks = new System.Workflow.Activities.IfElseActivity();
            this.customLogToWFHistoryActivity2 = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.setDescriptionLog = new System.Workflow.Activities.CodeActivity();
            this.tasksReplicator = new System.Workflow.Activities.ReplicatorActivity();
            this.terminateWorkflow = new System.Workflow.Activities.IfElseBranchActivity();
            this.isIgnoreIfNoParticipant = new System.Workflow.Activities.IfElseBranchActivity();
            this.isHaveApprovers = new System.Workflow.Activities.IfElseBranchActivity();
            this.checkApprovers = new System.Workflow.Activities.IfElseActivity();
            this.buildTasks = new System.Workflow.Activities.CodeActivity();
            this.TaskEvenHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.customLogToWFHistoryActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.setByPassValue = new System.Workflow.Activities.CodeActivity();
            this.isNormalTask = new System.Workflow.Activities.IfElseBranchActivity();
            this.isByPassTask = new System.Workflow.Activities.IfElseBranchActivity();
            this.checkByPassTask = new System.Workflow.Activities.IfElseActivity();
            this.initialData = new System.Workflow.Activities.CodeActivity();
            // 
            // updateAllTasks
            // 
            this.updateAllTasks.Name = "updateAllTasks";
            this.updateAllTasks.ExecuteCode += new System.EventHandler(this.updateAllTasks_ExecutedCode);
            // 
            // delayOneMinute
            // 
            this.delayOneMinute.Name = "delayOneMinute";
            this.delayOneMinute.TimeoutDuration = System.TimeSpan.Parse("00:00:01");
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // isMustUpdateAllTasks
            // 
            this.isMustUpdateAllTasks.Activities.Add(this.delayOneMinute);
            this.isMustUpdateAllTasks.Activities.Add(this.updateAllTasks);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isMustUpdateAllTasks_Condition);
            this.isMustUpdateAllTasks.Condition = codecondition1;
            this.isMustUpdateAllTasks.Name = "isMustUpdateAllTasks";
            // 
            // taskApproval1
            // 
            this.taskApproval1.ApprovalInfoTask = null;
            this.taskApproval1.Name = "taskApproval1";
            this.taskApproval1.Parameter = null;
            this.taskApproval1.WorkflowProperties = null;
            activitybind1.Name = "ApprovalWorkflow";
            activitybind1.Path = "strLogDescription";
            // 
            // terminateWF
            // 
            this.terminateWF.Name = "terminateWF";
            this.terminateWF.SetBinding(System.Workflow.ComponentModel.TerminateActivity.ErrorProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // setTerminateLog
            // 
            this.setTerminateLog.Name = "setTerminateLog";
            this.setTerminateLog.ExecuteCode += new System.EventHandler(this.setTerminateLog_ExecuteCode);
            // 
            // ignoreLog
            // 
            this.ignoreLog.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind2.Name = "ApprovalWorkflow";
            activitybind2.Path = "strLogDescription";
            this.ignoreLog.HistoryOutcome = "Ignored";
            this.ignoreLog.Name = "ignoreLog";
            activitybind3.Name = "ApprovalWorkflow";
            activitybind3.Path = "__ActivationProperties";
            this.ignoreLog.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.ignoreLog.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // setIgnoreTasks
            // 
            this.setIgnoreTasks.Name = "setIgnoreTasks";
            this.setIgnoreTasks.ExecuteCode += new System.EventHandler(this.setIgnoreTask_ExecuteCode);
            // 
            // checkUpdateAllTasks
            // 
            this.checkUpdateAllTasks.Activities.Add(this.isMustUpdateAllTasks);
            this.checkUpdateAllTasks.Activities.Add(this.ifElseBranchActivity2);
            this.checkUpdateAllTasks.Name = "checkUpdateAllTasks";
            // 
            // customLogToWFHistoryActivity2
            // 
            this.customLogToWFHistoryActivity2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind4.Name = "ApprovalWorkflow";
            activitybind4.Path = "strLogDescription";
            activitybind5.Name = "ApprovalWorkflow";
            activitybind5.Path = "Status";
            this.customLogToWFHistoryActivity2.Name = "customLogToWFHistoryActivity2";
            activitybind6.Name = "ApprovalWorkflow";
            activitybind6.Path = "__ActivationProperties";
            this.customLogToWFHistoryActivity2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.customLogToWFHistoryActivity2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.customLogToWFHistoryActivity2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // setDescriptionLog
            // 
            this.setDescriptionLog.Name = "setDescriptionLog";
            this.setDescriptionLog.ExecuteCode += new System.EventHandler(this.logResultToHistoryList_ExecuteCode);
            activitybind7.Name = "ApprovalWorkflow";
            activitybind7.Path = "_listTasks";
            // 
            // tasksReplicator
            // 
            this.tasksReplicator.Activities.Add(this.taskApproval1);
            this.tasksReplicator.ExecutionType = System.Workflow.Activities.ExecutionType.Parallel;
            this.tasksReplicator.Name = "tasksReplicator";
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsStopTaskReplicator);
            this.tasksReplicator.UntilCondition = codecondition2;
            this.tasksReplicator.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.tasksReplicatorChild_Init);
            this.tasksReplicator.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.tasksReplicatorChild_Complete);
            this.tasksReplicator.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // terminateWorkflow
            // 
            this.terminateWorkflow.Activities.Add(this.setTerminateLog);
            this.terminateWorkflow.Activities.Add(this.terminateWF);
            this.terminateWorkflow.Name = "terminateWorkflow";
            // 
            // isIgnoreIfNoParticipant
            // 
            this.isIgnoreIfNoParticipant.Activities.Add(this.setIgnoreTasks);
            this.isIgnoreIfNoParticipant.Activities.Add(this.ignoreLog);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isIgnoreIfNoParticipant_ConditionCode);
            this.isIgnoreIfNoParticipant.Condition = codecondition3;
            this.isIgnoreIfNoParticipant.Name = "isIgnoreIfNoParticipant";
            // 
            // isHaveApprovers
            // 
            this.isHaveApprovers.Activities.Add(this.tasksReplicator);
            this.isHaveApprovers.Activities.Add(this.setDescriptionLog);
            this.isHaveApprovers.Activities.Add(this.customLogToWFHistoryActivity2);
            this.isHaveApprovers.Activities.Add(this.checkUpdateAllTasks);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isHaveApprovers_ConditionCode);
            this.isHaveApprovers.Condition = codecondition4;
            this.isHaveApprovers.Name = "isHaveApprovers";
            // 
            // checkApprovers
            // 
            this.checkApprovers.Activities.Add(this.isHaveApprovers);
            this.checkApprovers.Activities.Add(this.isIgnoreIfNoParticipant);
            this.checkApprovers.Activities.Add(this.terminateWorkflow);
            this.checkApprovers.Name = "checkApprovers";
            // 
            // buildTasks
            // 
            this.buildTasks.Name = "buildTasks";
            this.buildTasks.ExecuteCode += new System.EventHandler(this.buildTasks_ExecuteCode);
            // 
            // TaskEvenHandler
            // 
            this.TaskEvenHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.ByPassTask;
            this.TaskEvenHandler.Name = "TaskEvenHandler";
            activitybind8.Name = "ApprovalWorkflow";
            activitybind8.Path = "TaskHandlerParameter";
            this.TaskEvenHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // customLogToWFHistoryActivity1
            // 
            this.customLogToWFHistoryActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind9.Name = "ApprovalWorkflow";
            activitybind9.Path = "strLogDescription";
            this.customLogToWFHistoryActivity1.HistoryOutcome = "ByPassed";
            this.customLogToWFHistoryActivity1.Name = "customLogToWFHistoryActivity1";
            activitybind10.Name = "ApprovalWorkflow";
            activitybind10.Path = "__ActivationProperties";
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // setByPassValue
            // 
            this.setByPassValue.Name = "setByPassValue";
            this.setByPassValue.ExecuteCode += new System.EventHandler(this.setByPassValue_ExecuteCode);
            // 
            // isNormalTask
            // 
            this.isNormalTask.Activities.Add(this.buildTasks);
            this.isNormalTask.Activities.Add(this.checkApprovers);
            this.isNormalTask.Name = "isNormalTask";
            // 
            // isByPassTask
            // 
            this.isByPassTask.Activities.Add(this.setByPassValue);
            this.isByPassTask.Activities.Add(this.customLogToWFHistoryActivity1);
            this.isByPassTask.Activities.Add(this.TaskEvenHandler);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isByPassTask_ExecuteCode);
            this.isByPassTask.Condition = codecondition5;
            this.isByPassTask.Name = "isByPassTask";
            // 
            // checkByPassTask
            // 
            this.checkByPassTask.Activities.Add(this.isByPassTask);
            this.checkByPassTask.Activities.Add(this.isNormalTask);
            this.checkByPassTask.Name = "checkByPassTask";
            // 
            // initialData
            // 
            this.initialData.Name = "initialData";
            this.initialData.ExecuteCode += new System.EventHandler(this.InitialData_ExecuteCode);
            // 
            // ApprovalWorkflow
            // 
            this.Activities.Add(this.initialData);
            this.Activities.Add(this.checkByPassTask);
            this.Name = "ApprovalWorkflow";
            this.CanModifyActivities = false;

        }


        #endregion

        private DelayActivity delayOneMinute;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity isMustUpdateAllTasks;

        private IfElseActivity checkUpdateAllTasks;

        private CodeActivity updateAllTasks;

        private System.Workflow.ComponentModel.TerminateActivity terminateWF;

        private CodeActivity setTerminateLog;

        private IfElseBranchActivity terminateWorkflow;

        private IfElseBranchActivity isIgnoreIfNoParticipant;

        private IfElseBranchActivity isHaveApprovers;

        private IfElseActivity checkApprovers;

        private CustomLogToWFHistoryActivity ignoreLog;

        private CodeActivity setIgnoreTasks;

        private CustomLogToWFHistoryActivity customLogToWFHistoryActivity1;

        private CustomLogToWFHistoryActivity customLogToWFHistoryActivity2;

        private CodeActivity setDescriptionLog;

        private TaskEventHandler TaskEvenHandler;

        private CodeActivity setByPassValue;

        private CodeActivity buildTasks;

        private IfElseBranchActivity isNormalTask;

        private IfElseBranchActivity isByPassTask;

        private IfElseActivity checkByPassTask;

        private TaskApproval taskApproval1;

        private CodeActivity initialData;

        private ReplicatorActivity tasksReplicator;































































































































































    }
}
