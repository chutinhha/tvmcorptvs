using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using TVMCORP.TVS.WORKFLOWS.Actions;
using TVMCORP.TVS.WORKFLOWS.MODELS;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class TaskApprovalActivity
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
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition7 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition8 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition9 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition10 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind22 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind23 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition11 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition12 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind24 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind25 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind26 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind27 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind28 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind29 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind30 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken2 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind31 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind32 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind33 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind34 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind35 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition13 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind36 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind37 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind38 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind39 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind40 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind41 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition14 = new System.Workflow.Activities.CodeCondition();
            this.sendWFTaskEmail2 = new TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail();
            this.sendEmailToEscalationParty = new TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail();
            this.escalationDateReachedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.sendReminderEmailReplicator = new System.Workflow.Activities.ReplicatorActivity();
            this.reminderDataReachedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.sendEEC = new System.Workflow.Activities.CodeActivity();
            this.TaskEvenHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.updateOnHoldTask = new System.Workflow.Activities.CodeActivity();
            this.workflowTerminatedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.sentHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.requestedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.reassignedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.rejetedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.approvedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.ifElseBranchActivity3 = new System.Workflow.Activities.IfElseBranchActivity();
            this.isNotOnHoldEscalation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.isNotOnHold = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity1 = new System.Workflow.Activities.IfElseBranchActivity();
            this.isSendEEC = new System.Workflow.Activities.IfElseBranchActivity();
            this.isOnHold = new System.Workflow.Activities.IfElseBranchActivity();
            this.isTerminated = new System.Workflow.Activities.IfElseBranchActivity();
            this.isSent = new System.Workflow.Activities.IfElseBranchActivity();
            this.isRequested = new System.Workflow.Activities.IfElseBranchActivity();
            this.isReassigned = new System.Workflow.Activities.IfElseBranchActivity();
            this.isRejected = new System.Workflow.Activities.IfElseBranchActivity();
            this.isApproved = new System.Workflow.Activities.IfElseBranchActivity();
            this.checkOnHoldEscalation = new System.Workflow.Activities.IfElseActivity();
            this.delayForActivity2 = new Microsoft.SharePoint.WorkflowActions.DelayForActivity();
            this.checkOnHold = new System.Workflow.Activities.IfElseActivity();
            this.delayForActivity1 = new Microsoft.SharePoint.WorkflowActions.DelayForActivity();
            this.taskEventHandler = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.isEscalationNotExist = new System.Workflow.Activities.IfElseBranchActivity();
            this.isEscalationExist = new System.Workflow.Activities.IfElseBranchActivity();
            this.notExist = new System.Workflow.Activities.IfElseBranchActivity();
            this.isExist = new System.Workflow.Activities.IfElseBranchActivity();
            this.taskChangeSequence = new System.Workflow.Activities.SequenceActivity();
            this.sendAssignmentEmail = new TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail();
            this.checkEscalationDate = new System.Workflow.Activities.IfElseActivity();
            this.checkReminderDate = new System.Workflow.Activities.IfElseActivity();
            this.persistOnClose2 = new TVMCORP.TVS.WORKFLOWS.Activities.PersistOnClose();
            this.taskCompletedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.completeTask = new Microsoft.SharePoint.WorkflowActions.CompleteTask();
            this.updateTask = new Microsoft.SharePoint.WorkflowActions.UpdateTask();
            this.customLogToWFHistoryActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.setDescriptionLog = new System.Workflow.Activities.CodeActivity();
            this.whileTaskChange = new System.Workflow.Activities.WhileActivity();
            this.sendAssignEmailReplicator = new System.Workflow.Activities.ReplicatorActivity();
            this.setSecurityForTask = new System.Workflow.Activities.CodeActivity();
            this.taskCreatedHandler = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.setTaskHandlerParameter = new System.Workflow.Activities.CodeActivity();
            this.persistOnClose1 = new TVMCORP.TVS.WORKFLOWS.Activities.PersistOnClose();
            this.createTaskWithContentType = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.escalationSequence = new System.Workflow.Activities.SequenceActivity();
            this.reminderSequence = new System.Workflow.Activities.SequenceActivity();
            this.taskSequence = new System.Workflow.Activities.SequenceActivity();
            this.taskGroup = new System.Workflow.Activities.ConditionedActivityGroup();
            this.initialData = new System.Workflow.Activities.CodeActivity();
            // 
            // sendWFTaskEmail2
            // 
            this.sendWFTaskEmail2.CC = null;
            this.sendWFTaskEmail2.Name = "sendWFTaskEmail2";
            activitybind1.Name = "TaskApprovalActivity";
            activitybind1.Path = "TaskIdCreated";
            activitybind2.Name = "TaskApprovalActivity";
            activitybind2.Path = "Parameter.ApprovalConfiguation.URLEmailTemplate";
            activitybind3.Name = "TaskApprovalActivity";
            activitybind3.Path = "Parameter.ApprovalConfiguation.ReminderEmailTemplate";
            this.sendWFTaskEmail2.To = null;
            activitybind4.Name = "TaskApprovalActivity";
            activitybind4.Path = "WorkflowProperties";
            this.sendWFTaskEmail2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TaskListItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.sendWFTaskEmail2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TemplateListURLProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.sendWFTaskEmail2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TemplateNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.sendWFTaskEmail2.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // sendEmailToEscalationParty
            // 
            this.sendEmailToEscalationParty.CC = null;
            this.sendEmailToEscalationParty.Name = "sendEmailToEscalationParty";
            activitybind5.Name = "TaskApprovalActivity";
            activitybind5.Path = "TaskIdCreated";
            activitybind6.Name = "TaskApprovalActivity";
            activitybind6.Path = "Parameter.ApprovalConfiguation.URLEmailTemplate";
            activitybind7.Name = "TaskApprovalActivity";
            activitybind7.Path = "Parameter.ApprovalConfiguation.EscalationEmailTemplate";
            activitybind8.Name = "TaskApprovalActivity";
            activitybind8.Path = "Parameter.ApprovalConfiguation.EscalationPartyEmail";
            activitybind9.Name = "TaskApprovalActivity";
            activitybind9.Path = "WorkflowProperties";
            this.sendEmailToEscalationParty.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TaskListItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.sendEmailToEscalationParty.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TemplateListURLProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.sendEmailToEscalationParty.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TemplateNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.sendEmailToEscalationParty.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.ToProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.sendEmailToEscalationParty.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // escalationDateReachedHandler
            // 
            this.escalationDateReachedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.EscalationDateReached;
            this.escalationDateReachedHandler.Name = "escalationDateReachedHandler";
            activitybind10.Name = "TaskApprovalActivity";
            activitybind10.Path = "TaskHandlerParameter";
            this.escalationDateReachedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            activitybind11.Name = "TaskApprovalActivity";
            activitybind11.Path = "AssingeeEmailsList";
            // 
            // sendReminderEmailReplicator
            // 
            this.sendReminderEmailReplicator.Activities.Add(this.sendWFTaskEmail2);
            this.sendReminderEmailReplicator.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.sendReminderEmailReplicator.Name = "sendReminderEmailReplicator";
            this.sendReminderEmailReplicator.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.sendReminderEmailReplicatorChild_Init);
            this.sendReminderEmailReplicator.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            // 
            // reminderDataReachedHandler
            // 
            this.reminderDataReachedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.ReminderDateReached;
            this.reminderDataReachedHandler.Name = "reminderDataReachedHandler";
            activitybind12.Name = "TaskApprovalActivity";
            activitybind12.Path = "TaskHandlerParameter";
            this.reminderDataReachedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // sendEEC
            // 
            this.sendEEC.Name = "sendEEC";
            this.sendEEC.ExecuteCode += new System.EventHandler(this.sendEEC_ExecuteCode);
            // 
            // TaskEvenHandler
            // 
            this.TaskEvenHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskOnHold;
            this.TaskEvenHandler.Name = "TaskEvenHandler";
            activitybind13.Name = "TaskApprovalActivity";
            activitybind13.Path = "TaskHandlerParameter";
            this.TaskEvenHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // updateOnHoldTask
            // 
            this.updateOnHoldTask.Name = "updateOnHoldTask";
            this.updateOnHoldTask.ExecuteCode += new System.EventHandler(this.updateOnHoldTask_ExecuteCode);
            // 
            // workflowTerminatedHandler
            // 
            this.workflowTerminatedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.WorkflowTerminated;
            this.workflowTerminatedHandler.Name = "workflowTerminatedHandler";
            activitybind14.Name = "TaskApprovalActivity";
            activitybind14.Path = "TaskHandlerParameter";
            this.workflowTerminatedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // sentHandler
            // 
            this.sentHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskInformationSent;
            this.sentHandler.Name = "sentHandler";
            activitybind15.Name = "TaskApprovalActivity";
            activitybind15.Path = "TaskHandlerParameter";
            this.sentHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            // 
            // requestedHandler
            // 
            this.requestedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskInformationRequested;
            this.requestedHandler.Name = "requestedHandler";
            activitybind16.Name = "TaskApprovalActivity";
            activitybind16.Path = "TaskHandlerParameter";
            this.requestedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            // 
            // reassignedHandler
            // 
            this.reassignedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskReassigned;
            this.reassignedHandler.Name = "reassignedHandler";
            activitybind17.Name = "TaskApprovalActivity";
            activitybind17.Path = "TaskHandlerParameter";
            this.reassignedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            // 
            // rejetedHandler
            // 
            this.rejetedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskRejected;
            this.rejetedHandler.Name = "rejetedHandler";
            activitybind18.Name = "TaskApprovalActivity";
            activitybind18.Path = "TaskHandlerParameter";
            this.rejetedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            // 
            // approvedHandler
            // 
            this.approvedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskApproved;
            this.approvedHandler.Name = "approvedHandler";
            activitybind19.Name = "TaskApprovalActivity";
            activitybind19.Path = "TaskHandlerParameter";
            this.approvedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            // 
            // ifElseBranchActivity3
            // 
            this.ifElseBranchActivity3.Name = "ifElseBranchActivity3";
            // 
            // isNotOnHoldEscalation
            // 
            this.isNotOnHoldEscalation.Activities.Add(this.escalationDateReachedHandler);
            this.isNotOnHoldEscalation.Activities.Add(this.sendEmailToEscalationParty);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isNotOnHold_Condition);
            this.isNotOnHoldEscalation.Condition = codecondition1;
            this.isNotOnHoldEscalation.Name = "isNotOnHoldEscalation";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // isNotOnHold
            // 
            this.isNotOnHold.Activities.Add(this.reminderDataReachedHandler);
            this.isNotOnHold.Activities.Add(this.sendReminderEmailReplicator);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isNotOnHold_Condition);
            this.isNotOnHold.Condition = codecondition2;
            this.isNotOnHold.Name = "isNotOnHold";
            // 
            // ifElseBranchActivity1
            // 
            this.ifElseBranchActivity1.Name = "ifElseBranchActivity1";
            // 
            // isSendEEC
            // 
            this.isSendEEC.Activities.Add(this.sendEEC);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isSendEEC_Condition);
            this.isSendEEC.Condition = codecondition3;
            this.isSendEEC.Name = "isSendEEC";
            // 
            // isOnHold
            // 
            this.isOnHold.Activities.Add(this.updateOnHoldTask);
            this.isOnHold.Activities.Add(this.TaskEvenHandler);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isOnHold_Condition);
            this.isOnHold.Condition = codecondition4;
            this.isOnHold.Name = "isOnHold";
            // 
            // isTerminated
            // 
            this.isTerminated.Activities.Add(this.workflowTerminatedHandler);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isTerminated_Condition);
            this.isTerminated.Condition = codecondition5;
            this.isTerminated.Name = "isTerminated";
            // 
            // isSent
            // 
            this.isSent.Activities.Add(this.sentHandler);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isSent_Condition);
            this.isSent.Condition = codecondition6;
            this.isSent.Name = "isSent";
            // 
            // isRequested
            // 
            this.isRequested.Activities.Add(this.requestedHandler);
            codecondition7.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isRequested_Condition);
            this.isRequested.Condition = codecondition7;
            this.isRequested.Name = "isRequested";
            // 
            // isReassigned
            // 
            this.isReassigned.Activities.Add(this.reassignedHandler);
            codecondition8.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isReassigned_Condition);
            this.isReassigned.Condition = codecondition8;
            this.isReassigned.Name = "isReassigned";
            // 
            // isRejected
            // 
            this.isRejected.Activities.Add(this.rejetedHandler);
            codecondition9.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isRejected_Condition);
            this.isRejected.Condition = codecondition9;
            this.isRejected.Name = "isRejected";
            // 
            // isApproved
            // 
            this.isApproved.Activities.Add(this.approvedHandler);
            codecondition10.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isApproved_Condition);
            this.isApproved.Condition = codecondition10;
            this.isApproved.Name = "isApproved";
            // 
            // checkOnHoldEscalation
            // 
            this.checkOnHoldEscalation.Activities.Add(this.isNotOnHoldEscalation);
            this.checkOnHoldEscalation.Activities.Add(this.ifElseBranchActivity3);
            this.checkOnHoldEscalation.Name = "checkOnHoldEscalation";
            // 
            // delayForActivity2
            // 
            activitybind20.Name = "TaskApprovalActivity";
            activitybind20.Path = "Parameter.ApprovalConfiguation.EscalationDuration";
            this.delayForActivity2.Hours = 0D;
            this.delayForActivity2.Minutes = 0D;
            this.delayForActivity2.Name = "delayForActivity2";
            this.delayForActivity2.SetBinding(Microsoft.SharePoint.WorkflowActions.DelayForActivity.DaysProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            // 
            // checkOnHold
            // 
            this.checkOnHold.Activities.Add(this.isNotOnHold);
            this.checkOnHold.Activities.Add(this.ifElseBranchActivity2);
            this.checkOnHold.Name = "checkOnHold";
            // 
            // delayForActivity1
            // 
            activitybind21.Name = "TaskApprovalActivity";
            activitybind21.Path = "Parameter.ApprovalConfiguation.ReminderDuration";
            this.delayForActivity1.Hours = 0D;
            this.delayForActivity1.Minutes = 0D;
            this.delayForActivity1.Name = "delayForActivity1";
            this.delayForActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.DelayForActivity.DaysProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            // 
            // taskEventHandler
            // 
            this.taskEventHandler.Activities.Add(this.isApproved);
            this.taskEventHandler.Activities.Add(this.isRejected);
            this.taskEventHandler.Activities.Add(this.isReassigned);
            this.taskEventHandler.Activities.Add(this.isRequested);
            this.taskEventHandler.Activities.Add(this.isSent);
            this.taskEventHandler.Activities.Add(this.isTerminated);
            this.taskEventHandler.Activities.Add(this.isOnHold);
            this.taskEventHandler.Activities.Add(this.isSendEEC);
            this.taskEventHandler.Activities.Add(this.ifElseBranchActivity1);
            this.taskEventHandler.Name = "taskEventHandler";
            // 
            // onTaskChanged
            // 
            activitybind22.Name = "TaskApprovalActivity";
            activitybind22.Path = "ApprovalInfoTask.TaskAfterProperties";
            this.onTaskChanged.BeforeProperties = null;
            correlationtoken1.Name = "taskToken";
            correlationtoken1.OwnerActivityName = "TaskApprovalActivity";
            this.onTaskChanged.CorrelationToken = correlationtoken1;
            this.onTaskChanged.Executor = null;
            this.onTaskChanged.Name = "onTaskChanged";
            activitybind23.Name = "TaskApprovalActivity";
            activitybind23.Path = "ApprovalInfoTask.Id";
            this.onTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChanged1_Invoked);
            this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind23)));
            this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind22)));
            // 
            // isEscalationNotExist
            // 
            this.isEscalationNotExist.Name = "isEscalationNotExist";
            // 
            // isEscalationExist
            // 
            this.isEscalationExist.Activities.Add(this.delayForActivity2);
            this.isEscalationExist.Activities.Add(this.checkOnHoldEscalation);
            codecondition11.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsEscalationDateExist);
            this.isEscalationExist.Condition = codecondition11;
            this.isEscalationExist.Name = "isEscalationExist";
            // 
            // notExist
            // 
            this.notExist.Name = "notExist";
            // 
            // isExist
            // 
            this.isExist.Activities.Add(this.delayForActivity1);
            this.isExist.Activities.Add(this.checkOnHold);
            codecondition12.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsReminderDateExist);
            this.isExist.Condition = codecondition12;
            this.isExist.Name = "isExist";
            // 
            // taskChangeSequence
            // 
            this.taskChangeSequence.Activities.Add(this.onTaskChanged);
            this.taskChangeSequence.Activities.Add(this.taskEventHandler);
            this.taskChangeSequence.Name = "taskChangeSequence";
            // 
            // sendAssignmentEmail
            // 
            this.sendAssignmentEmail.CC = null;
            this.sendAssignmentEmail.Name = "sendAssignmentEmail";
            activitybind24.Name = "TaskApprovalActivity";
            activitybind24.Path = "TaskIdCreated";
            activitybind25.Name = "TaskApprovalActivity";
            activitybind25.Path = "Parameter.ApprovalConfiguation.URLEmailTemplate";
            activitybind26.Name = "TaskApprovalActivity";
            activitybind26.Path = "Parameter.ApprovalConfiguation.AssignmentEmailTemplate";
            this.sendAssignmentEmail.To = null;
            activitybind27.Name = "TaskApprovalActivity";
            activitybind27.Path = "WorkflowProperties";
            this.sendAssignmentEmail.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TaskListItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind24)));
            this.sendAssignmentEmail.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TemplateListURLProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind25)));
            this.sendAssignmentEmail.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.TemplateNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind26)));
            this.sendAssignmentEmail.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.SendWFTaskEmail.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind27)));
            // 
            // checkEscalationDate
            // 
            this.checkEscalationDate.Activities.Add(this.isEscalationExist);
            this.checkEscalationDate.Activities.Add(this.isEscalationNotExist);
            this.checkEscalationDate.Name = "checkEscalationDate";
            // 
            // checkReminderDate
            // 
            this.checkReminderDate.Activities.Add(this.isExist);
            this.checkReminderDate.Activities.Add(this.notExist);
            this.checkReminderDate.Name = "checkReminderDate";
            // 
            // persistOnClose2
            // 
            this.persistOnClose2.Name = "persistOnClose2";
            // 
            // taskCompletedHandler
            // 
            this.taskCompletedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskCompleted;
            this.taskCompletedHandler.Name = "taskCompletedHandler";
            activitybind28.Name = "TaskApprovalActivity";
            activitybind28.Path = "TaskHandlerParameter";
            this.taskCompletedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind28)));
            // 
            // completeTask
            // 
            this.completeTask.CorrelationToken = correlationtoken1;
            this.completeTask.Name = "completeTask";
            activitybind29.Name = "TaskApprovalActivity";
            activitybind29.Path = "ApprovalInfoTask.Id";
            activitybind30.Name = "TaskApprovalActivity";
            activitybind30.Path = "OutComeText";
            this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind29)));
            this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind30)));
            // 
            // updateTask
            // 
            correlationtoken2.Name = "taskToken";
            correlationtoken2.OwnerActivityName = "TaskApprovalActivity";
            this.updateTask.CorrelationToken = correlationtoken2;
            this.updateTask.Name = "updateTask";
            activitybind31.Name = "TaskApprovalActivity";
            activitybind31.Path = "ApprovalInfoTask.Id";
            activitybind32.Name = "TaskApprovalActivity";
            activitybind32.Path = "updateTaskPros";
            this.updateTask.MethodInvoking += new System.EventHandler(this.updateTask_ExecuteCode);
            this.updateTask.SetBinding(Microsoft.SharePoint.WorkflowActions.UpdateTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind31)));
            this.updateTask.SetBinding(Microsoft.SharePoint.WorkflowActions.UpdateTask.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind32)));
            // 
            // customLogToWFHistoryActivity1
            // 
            this.customLogToWFHistoryActivity1.Enabled = false;
            this.customLogToWFHistoryActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind33.Name = "TaskApprovalActivity";
            activitybind33.Path = "TaskDescription";
            activitybind34.Name = "TaskApprovalActivity";
            activitybind34.Path = "ApprovalInfoTask.Status";
            this.customLogToWFHistoryActivity1.Name = "customLogToWFHistoryActivity1";
            activitybind35.Name = "TaskApprovalActivity";
            activitybind35.Path = "WorkflowProperties";
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind35)));
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind33)));
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind34)));
            // 
            // setDescriptionLog
            // 
            this.setDescriptionLog.Enabled = false;
            this.setDescriptionLog.Name = "setDescriptionLog";
            this.setDescriptionLog.ExecuteCode += new System.EventHandler(this.logToHistory_ExecuteCode);
            // 
            // whileTaskChange
            // 
            this.whileTaskChange.Activities.Add(this.taskChangeSequence);
            codecondition13.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsNotComplete);
            this.whileTaskChange.Condition = codecondition13;
            this.whileTaskChange.Name = "whileTaskChange";
            activitybind36.Name = "TaskApprovalActivity";
            activitybind36.Path = "AssingeeEmailsList";
            // 
            // sendAssignEmailReplicator
            // 
            this.sendAssignEmailReplicator.Activities.Add(this.sendAssignmentEmail);
            this.sendAssignEmailReplicator.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.sendAssignEmailReplicator.Name = "sendAssignEmailReplicator";
            this.sendAssignEmailReplicator.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.sendAssignEmailReplicatorChild_Init);
            this.sendAssignEmailReplicator.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind36)));
            // 
            // setSecurityForTask
            // 
            this.setSecurityForTask.Name = "setSecurityForTask";
            this.setSecurityForTask.ExecuteCode += new System.EventHandler(this.setSecurityForTask_ExecuteCode);
            // 
            // taskCreatedHandler
            // 
            this.taskCreatedHandler.EventType = TVMCORP.TVS.WORKFLOWS.MODELS.TaskEventTypes.TaskCreated;
            this.taskCreatedHandler.Name = "taskCreatedHandler";
            activitybind37.Name = "TaskApprovalActivity";
            activitybind37.Path = "TaskHandlerParameter";
            this.taskCreatedHandler.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind37)));
            // 
            // setTaskHandlerParameter
            // 
            this.setTaskHandlerParameter.Name = "setTaskHandlerParameter";
            this.setTaskHandlerParameter.ExecuteCode += new System.EventHandler(this.setTaskHandlerParameter_ExecuteCode);
            // 
            // persistOnClose1
            // 
            this.persistOnClose1.Name = "persistOnClose1";
            // 
            // createTaskWithContentType
            // 
            activitybind38.Name = "TaskApprovalActivity";
            activitybind38.Path = "ApprovalInfoTask.ContentTypeId";
            this.createTaskWithContentType.CorrelationToken = correlationtoken2;
            activitybind39.Name = "TaskApprovalActivity";
            activitybind39.Path = "TaskIdCreated";
            this.createTaskWithContentType.Name = "createTaskWithContentType";
            this.createTaskWithContentType.SpecialPermissions = null;
            activitybind40.Name = "TaskApprovalActivity";
            activitybind40.Path = "ApprovalInfoTask.Id";
            activitybind41.Name = "TaskApprovalActivity";
            activitybind41.Path = "ApprovalInfoTask.TaskProperties";
            this.createTaskWithContentType.MethodInvoking += new System.EventHandler(this.createTaskWithContentType1_MethodInvoking);
            this.createTaskWithContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.ContentTypeIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind38)));
            this.createTaskWithContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind41)));
            this.createTaskWithContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind40)));
            this.createTaskWithContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.ListItemIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind39)));
            // 
            // escalationSequence
            // 
            this.escalationSequence.Activities.Add(this.checkEscalationDate);
            this.escalationSequence.Name = "escalationSequence";
            // 
            // reminderSequence
            // 
            this.reminderSequence.Activities.Add(this.checkReminderDate);
            this.reminderSequence.Name = "reminderSequence";
            // 
            // taskSequence
            // 
            this.taskSequence.Activities.Add(this.createTaskWithContentType);
            this.taskSequence.Activities.Add(this.persistOnClose1);
            this.taskSequence.Activities.Add(this.setTaskHandlerParameter);
            this.taskSequence.Activities.Add(this.taskCreatedHandler);
            this.taskSequence.Activities.Add(this.setSecurityForTask);
            this.taskSequence.Activities.Add(this.sendAssignEmailReplicator);
            this.taskSequence.Activities.Add(this.whileTaskChange);
            this.taskSequence.Activities.Add(this.setDescriptionLog);
            this.taskSequence.Activities.Add(this.customLogToWFHistoryActivity1);
            this.taskSequence.Activities.Add(this.updateTask);
            this.taskSequence.Activities.Add(this.completeTask);
            this.taskSequence.Activities.Add(this.taskCompletedHandler);
            this.taskSequence.Activities.Add(this.persistOnClose2);
            this.taskSequence.Name = "taskSequence";
            // 
            // taskGroup
            // 
            this.taskGroup.Activities.Add(this.taskSequence);
            this.taskGroup.Activities.Add(this.reminderSequence);
            this.taskGroup.Activities.Add(this.escalationSequence);
            this.taskGroup.Name = "taskGroup";
            codecondition14.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsStopTaskGroup);
            this.taskGroup.UntilCondition = codecondition14;
            // 
            // initialData
            // 
            this.initialData.Name = "initialData";
            this.initialData.ExecuteCode += new System.EventHandler(this.initialData_ExecuteCode);
            // 
            // TaskApprovalActivity
            // 
            this.Activities.Add(this.initialData);
            this.Activities.Add(this.taskGroup);
            this.Name = "TaskApprovalActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity sendEEC;

        private IfElseBranchActivity ifElseBranchActivity1;

        private CustomLogToWFHistoryActivity customLogToWFHistoryActivity1;

        private CodeActivity setDescriptionLog;

        private IfElseBranchActivity ifElseBranchActivity3;

        private IfElseBranchActivity isNotOnHoldEscalation;

        private IfElseActivity checkOnHoldEscalation;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity isNotOnHold;

        private IfElseActivity checkOnHold;

        private CodeActivity updateOnHoldTask;

        private TaskEventHandler TaskEvenHandler;

        private IfElseBranchActivity isOnHold;

        private TaskEventHandler workflowTerminatedHandler;

        private TaskEventHandler taskCompletedHandler;

        private TaskEventHandler reminderDataReachedHandler;

        private TaskEventHandler escalationDateReachedHandler;

        private IfElseBranchActivity isSendEEC;

        private TaskEventHandler reassignedHandler;

        private TaskEventHandler sentHandler;

        private IfElseBranchActivity isTerminated;

        private IfElseBranchActivity isReassigned;

        private TaskEventHandler requestedHandler;

        private TaskEventHandler rejetedHandler;

        private TaskEventHandler approvedHandler;

        private IfElseBranchActivity isSent;

        private IfElseBranchActivity isRequested;

        private IfElseBranchActivity isRejected;

        private IfElseBranchActivity isApproved;

        private IfElseActivity taskEventHandler;

        private SequenceActivity taskChangeSequence;

        private CodeActivity setTaskHandlerParameter;

        private TaskEventHandler taskCreatedHandler;

        private SendWFTaskEmail sendAssignmentEmail;

        private PersistOnClose persistOnClose2;

        private Microsoft.SharePoint.WorkflowActions.UpdateTask updateTask;

        private SendWFTaskEmail sendEmailToEscalationParty;

        private SendWFTaskEmail sendWFTaskEmail2;

        private PersistOnClose persistOnClose1;

        private CodeActivity setSecurityForTask;

        private IfElseBranchActivity isEscalationNotExist;

        private IfElseBranchActivity isEscalationExist;

        private IfElseActivity checkEscalationDate;

        private IfElseBranchActivity notExist;

        private IfElseBranchActivity isExist;

        private IfElseActivity checkReminderDate;

        private ReplicatorActivity sendReminderEmailReplicator;

        private ReplicatorActivity sendAssignEmailReplicator;

        private CodeActivity initialData;

        private Microsoft.SharePoint.WorkflowActions.DelayForActivity delayForActivity1;

        private Microsoft.SharePoint.WorkflowActions.DelayForActivity delayForActivity2;

        private SequenceActivity escalationSequence;

        private WhileActivity whileTaskChange;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskWithContentType;

        private ConditionedActivityGroup taskGroup;

        private SequenceActivity taskSequence;

        private SequenceActivity reminderSequence;

        private Microsoft.SharePoint.WorkflowActions.CompleteTask completeTask;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChanged;




















































































    }
}
