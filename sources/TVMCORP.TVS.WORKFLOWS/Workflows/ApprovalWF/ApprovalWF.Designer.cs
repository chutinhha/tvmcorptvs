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

namespace TVMCORP.TVS.WORKFLOWS.Workflows
{
    public sealed partial class ApprovalWF
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
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition7 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            this.onWorkflowItemChanged1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged();
            this.CompleteAllTasks = new Microsoft.SharePoint.WorkflowActions.UpdateAllTasks();
            this.CompletedTasks = new System.Workflow.Activities.CodeActivity();
            this.DocumentChangeSendEmail = new System.Workflow.Activities.CodeActivity();
            this.DocumentNotChangeByUser = new System.Workflow.Activities.WhileActivity();
            this.ifElseBranchActivity7 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity4 = new System.Workflow.Activities.IfElseBranchActivity();
            this.approvalLevel = new TVMCORP.TVS.WORKFLOWS.Activities.ApprovalLevel();
            this.updatePermissionActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.UpdatePermissionActivity();
            this.codeActivity2 = new System.Workflow.Activities.CodeActivity();
            this.ifElseActivity2 = new System.Workflow.Activities.IfElseActivity();
            this.UpdateApprovalStatus = new System.Workflow.Activities.CodeActivity();
            this.approvalLevelsReplicator = new System.Workflow.Activities.ReplicatorActivity();
            this.ifElseBranchActivity6 = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfEnableUpdatePermission = new System.Workflow.Activities.IfElseBranchActivity();
            this.sequenceActivity3 = new System.Workflow.Activities.SequenceActivity();
            this.sequenceActivity1 = new System.Workflow.Activities.SequenceActivity();
            this.WFRejectedEvent = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.SetWFRejectedEventParameter = new System.Workflow.Activities.CodeActivity();
            this.WFApprovedEvent = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.SetWFApprovedEventParameter = new System.Workflow.Activities.CodeActivity();
            this.ifElseActivity1 = new System.Workflow.Activities.IfElseActivity();
            this.SetItemApproval = new TVMCORP.TVS.WORKFLOWS.Activities.PublishItemActivity();
            this.SetItemApprovalData = new System.Workflow.Activities.CodeActivity();
            this.conditionedActivityGroup1 = new System.Workflow.Activities.ConditionedActivityGroup();
            this.DeplayOnStart = new Microsoft.SharePoint.WorkflowActions.DelayForActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfEnableContentApprovalAndDocumentAppvoed = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity5 = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfByPass = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity3 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity1 = new System.Workflow.Activities.IfElseBranchActivity();
            this.WorkfowEndedEvent = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.ContentApproval = new System.Workflow.Activities.IfElseActivity();
            this.CheckStartingCondition = new System.Workflow.Activities.IfElseActivity();
            this.WorkflowStartedEvent = new TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler();
            this.IfDelayOnStart = new System.Workflow.Activities.IfElseActivity();
            this.workflowInitData = new System.Workflow.Activities.CodeActivity();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // onWorkflowItemChanged1
            // 
            this.onWorkflowItemChanged1.AfterProperties = null;
            this.onWorkflowItemChanged1.BeforeProperties = null;
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "ApprovalWF";
            this.onWorkflowItemChanged1.CorrelationToken = correlationtoken1;
            this.onWorkflowItemChanged1.Name = "onWorkflowItemChanged1";
            this.onWorkflowItemChanged1.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.OnWorklowItemChange);
            // 
            // CompleteAllTasks
            // 
            this.CompleteAllTasks.CorrelationToken = correlationtoken1;
            this.CompleteAllTasks.Name = "CompleteAllTasks";
            activitybind1.Name = "ApprovalWF";
            activitybind1.Path = "CompleteAllTasks_TaskProperties";
            this.CompleteAllTasks.MethodInvoking += new System.EventHandler(this.CompleteAllTasks_MethodInvoking);
            this.CompleteAllTasks.SetBinding(Microsoft.SharePoint.WorkflowActions.UpdateAllTasks.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // CompletedTasks
            // 
            this.CompletedTasks.Name = "CompletedTasks";
            this.CompletedTasks.ExecuteCode += new System.EventHandler(this.DocumentChangeSendEmail_ExecuteCode);
            // 
            // DocumentChangeSendEmail
            // 
            this.DocumentChangeSendEmail.Name = "DocumentChangeSendEmail";
            this.DocumentChangeSendEmail.ExecuteCode += new System.EventHandler(this.DocumentChangeSendEmail_ExecuteCode);
            // 
            // DocumentNotChangeByUser
            // 
            this.DocumentNotChangeByUser.Activities.Add(this.onWorkflowItemChanged1);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsItemChangeFromTaskForm);
            this.DocumentNotChangeByUser.Condition = codecondition1;
            this.DocumentNotChangeByUser.Name = "DocumentNotChangeByUser";
            // 
            // ifElseBranchActivity7
            // 
            this.ifElseBranchActivity7.Name = "ifElseBranchActivity7";
            // 
            // ifElseBranchActivity4
            // 
            this.ifElseBranchActivity4.Activities.Add(this.DocumentNotChangeByUser);
            this.ifElseBranchActivity4.Activities.Add(this.DocumentChangeSendEmail);
            this.ifElseBranchActivity4.Activities.Add(this.CompletedTasks);
            this.ifElseBranchActivity4.Activities.Add(this.CompleteAllTasks);
            ruleconditionreference1.ConditionName = "Condition1";
            this.ifElseBranchActivity4.Condition = ruleconditionreference1;
            this.ifElseBranchActivity4.Name = "ifElseBranchActivity4";
            // 
            // approvalLevel
            // 
            this.approvalLevel.ApprovalData = null;
            activitybind2.Name = "ApprovalWF";
            activitybind2.Path = "AssociationObj.EndOnFirstReject";
            this.approvalLevel.Name = "approvalLevel";
            this.approvalLevel.TaskContentTypeId = null;
            activitybind3.Name = "ApprovalWF";
            activitybind3.Path = "TaskOutcome";
            activitybind4.Name = "ApprovalWF";
            activitybind4.Path = "workflowProperties";
            this.approvalLevel.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.ApprovalLevel.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.approvalLevel.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.ApprovalLevel.workflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.approvalLevel.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.ApprovalLevel.EndAtFirstRejectionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // updatePermissionActivity1
            // 
            activitybind5.Name = "ApprovalWF";
            activitybind5.Path = "_allApprovers";
            activitybind6.Name = "ApprovalWF";
            activitybind6.Path = "workflowProperties.Item";
            activitybind7.Name = "ApprovalWF";
            activitybind7.Path = "AssociationObj.KeepCurrentPermissions";
            this.updatePermissionActivity1.Name = "updatePermissionActivity1";
            activitybind8.Name = "ApprovalWF";
            activitybind8.Path = "AssociationObj.Permissions";
            this.updatePermissionActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.UpdatePermissionActivity.ItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.updatePermissionActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.UpdatePermissionActivity.KeepExistingPermissionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.updatePermissionActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.UpdatePermissionActivity.PermissionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.updatePermissionActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.UpdatePermissionActivity.ApproversProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // codeActivity2
            // 
            this.codeActivity2.Name = "codeActivity2";
            this.codeActivity2.ExecuteCode += new System.EventHandler(this.codeActivity2_ExecuteCode);
            // 
            // ifElseActivity2
            // 
            this.ifElseActivity2.Activities.Add(this.ifElseBranchActivity4);
            this.ifElseActivity2.Activities.Add(this.ifElseBranchActivity7);
            this.ifElseActivity2.Name = "ifElseActivity2";
            // 
            // UpdateApprovalStatus
            // 
            this.UpdateApprovalStatus.Name = "UpdateApprovalStatus";
            this.UpdateApprovalStatus.ExecuteCode += new System.EventHandler(this.UpdateApprovalStatus_ExecuteCode);
            activitybind9.Name = "ApprovalWF";
            activitybind9.Path = "InitiationObj.ApprovalLevels";
            // 
            // approvalLevelsReplicator
            // 
            this.approvalLevelsReplicator.Activities.Add(this.approvalLevel);
            this.approvalLevelsReplicator.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.approvalLevelsReplicator.Name = "approvalLevelsReplicator";
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TaskRejectOrApproved);
            this.approvalLevelsReplicator.UntilCondition = codecondition2;
            this.approvalLevelsReplicator.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ApprovalLevel_DataBind);
            this.approvalLevelsReplicator.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ApprovalLevelComplete);
            this.approvalLevelsReplicator.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // ifElseBranchActivity6
            // 
            this.ifElseBranchActivity6.Name = "ifElseBranchActivity6";
            // 
            // IfEnableUpdatePermission
            // 
            this.IfEnableUpdatePermission.Activities.Add(this.codeActivity2);
            this.IfEnableUpdatePermission.Activities.Add(this.updatePermissionActivity1);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsEnableUpdatePermission);
            this.IfEnableUpdatePermission.Condition = codecondition3;
            this.IfEnableUpdatePermission.Name = "IfEnableUpdatePermission";
            // 
            // sequenceActivity3
            // 
            this.sequenceActivity3.Activities.Add(this.ifElseActivity2);
            this.sequenceActivity3.Name = "sequenceActivity3";
            // 
            // sequenceActivity1
            // 
            this.sequenceActivity1.Activities.Add(this.approvalLevelsReplicator);
            this.sequenceActivity1.Activities.Add(this.UpdateApprovalStatus);
            this.sequenceActivity1.Name = "sequenceActivity1";
            // 
            // WFRejectedEvent
            // 
            this.WFRejectedEvent.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.WFRejected;
            this.WFRejectedEvent.Name = "WFRejectedEvent";
            activitybind10.Name = "ApprovalWF";
            activitybind10.Path = "WFApprovedEvent_Parameter";
            this.WFRejectedEvent.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // SetWFRejectedEventParameter
            // 
            this.SetWFRejectedEventParameter.Name = "SetWFRejectedEventParameter";
            this.SetWFRejectedEventParameter.ExecuteCode += new System.EventHandler(this.SetWFApprovedEventParameter_ExecuteCode);
            // 
            // WFApprovedEvent
            // 
            this.WFApprovedEvent.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.WFApproved;
            this.WFApprovedEvent.Name = "WFApprovedEvent";
            activitybind11.Name = "ApprovalWF";
            activitybind11.Path = "WFApprovedEvent_Parameter";
            this.WFApprovedEvent.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            // 
            // SetWFApprovedEventParameter
            // 
            this.SetWFApprovedEventParameter.Name = "SetWFApprovedEventParameter";
            this.SetWFApprovedEventParameter.ExecuteCode += new System.EventHandler(this.SetWFApprovedEventParameter_ExecuteCode);
            // 
            // ifElseActivity1
            // 
            this.ifElseActivity1.Activities.Add(this.IfEnableUpdatePermission);
            this.ifElseActivity1.Activities.Add(this.ifElseBranchActivity6);
            this.ifElseActivity1.Name = "ifElseActivity1";
            // 
            // SetItemApproval
            // 
            activitybind12.Name = "ApprovalWF";
            activitybind12.Path = "workflowProperties";
            activitybind13.Name = "ApprovalWF";
            activitybind13.Path = "SetItemApproval___ListId";
            activitybind14.Name = "ApprovalWF";
            activitybind14.Path = "SetItemApproval___ListItem";
            this.SetItemApproval.CommentText = "test";
            this.SetItemApproval.Name = "SetItemApproval";
            activitybind15.Name = "ApprovalWF";
            activitybind15.Path = "SetItemApproval_Status";
            this.SetItemApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CCICoreActivity.@__ActivationPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.SetItemApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CCICoreActivity.@__ListIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.SetItemApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CCICoreActivity.@__ListItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.SetItemApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.PublishItemActivity.StatusProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            // 
            // SetItemApprovalData
            // 
            this.SetItemApprovalData.Name = "SetItemApprovalData";
            this.SetItemApprovalData.ExecuteCode += new System.EventHandler(this.SetItemApprovalData_ExecuteCode);
            // 
            // conditionedActivityGroup1
            // 
            this.conditionedActivityGroup1.Activities.Add(this.sequenceActivity1);
            this.conditionedActivityGroup1.Activities.Add(this.sequenceActivity3);
            this.conditionedActivityGroup1.Name = "conditionedActivityGroup1";
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.DocumentApprovedRejectedOrChanged);
            this.conditionedActivityGroup1.UntilCondition = codecondition4;
            // 
            // DeplayOnStart
            // 
            this.DeplayOnStart.Days = 0D;
            this.DeplayOnStart.Hours = 0D;
            activitybind16.Name = "ApprovalWF";
            activitybind16.Path = "DeplayOnStart_Minutes";
            this.DeplayOnStart.Name = "DeplayOnStart";
            this.DeplayOnStart.SetBinding(Microsoft.SharePoint.WorkflowActions.DelayForActivity.MinutesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.SetWFRejectedEventParameter);
            this.ifElseBranchActivity2.Activities.Add(this.WFRejectedEvent);
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // IfEnableContentApprovalAndDocumentAppvoed
            // 
            this.IfEnableContentApprovalAndDocumentAppvoed.Activities.Add(this.SetItemApprovalData);
            this.IfEnableContentApprovalAndDocumentAppvoed.Activities.Add(this.SetItemApproval);
            this.IfEnableContentApprovalAndDocumentAppvoed.Activities.Add(this.ifElseActivity1);
            this.IfEnableContentApprovalAndDocumentAppvoed.Activities.Add(this.SetWFApprovedEventParameter);
            this.IfEnableContentApprovalAndDocumentAppvoed.Activities.Add(this.WFApprovedEvent);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsEnableContentApproval);
            this.IfEnableContentApprovalAndDocumentAppvoed.Condition = codecondition5;
            this.IfEnableContentApprovalAndDocumentAppvoed.Name = "IfEnableContentApprovalAndDocumentAppvoed";
            // 
            // ifElseBranchActivity5
            // 
            this.ifElseBranchActivity5.Name = "ifElseBranchActivity5";
            // 
            // IfByPass
            // 
            this.IfByPass.Activities.Add(this.conditionedActivityGroup1);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IfStartApproval);
            this.IfByPass.Condition = codecondition6;
            this.IfByPass.Name = "IfByPass";
            // 
            // ifElseBranchActivity3
            // 
            this.ifElseBranchActivity3.Name = "ifElseBranchActivity3";
            // 
            // ifElseBranchActivity1
            // 
            this.ifElseBranchActivity1.Activities.Add(this.DeplayOnStart);
            codecondition7.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IfDeplayOnStart);
            this.ifElseBranchActivity1.Condition = codecondition7;
            this.ifElseBranchActivity1.Name = "ifElseBranchActivity1";
            // 
            // WorkfowEndedEvent
            // 
            this.WorkfowEndedEvent.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.WFEnd;
            this.WorkfowEndedEvent.Name = "WorkfowEndedEvent";
            activitybind17.Name = "ApprovalWF";
            activitybind17.Path = "WorkfowEndedEvent_Parameter";
            this.WorkfowEndedEvent.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            // 
            // ContentApproval
            // 
            this.ContentApproval.Activities.Add(this.IfEnableContentApprovalAndDocumentAppvoed);
            this.ContentApproval.Activities.Add(this.ifElseBranchActivity2);
            this.ContentApproval.Name = "ContentApproval";
            // 
            // CheckStartingCondition
            // 
            this.CheckStartingCondition.Activities.Add(this.IfByPass);
            this.CheckStartingCondition.Activities.Add(this.ifElseBranchActivity5);
            this.CheckStartingCondition.Name = "CheckStartingCondition";
            // 
            // WorkflowStartedEvent
            // 
            this.WorkflowStartedEvent.EventType = TVMCORP.TVS.UTIL.MODELS.TaskEventTypes.WFStarted;
            this.WorkflowStartedEvent.Name = "WorkflowStartedEvent";
            activitybind18.Name = "ApprovalWF";
            activitybind18.Path = "WorkflowStartedEvent_Parameter";
            this.WorkflowStartedEvent.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskEventHandler.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            // 
            // IfDelayOnStart
            // 
            this.IfDelayOnStart.Activities.Add(this.ifElseBranchActivity1);
            this.IfDelayOnStart.Activities.Add(this.ifElseBranchActivity3);
            this.IfDelayOnStart.Name = "IfDelayOnStart";
            // 
            // workflowInitData
            // 
            this.workflowInitData.Name = "workflowInitData";
            this.workflowInitData.ExecuteCode += new System.EventHandler(this.InitialData);
            activitybind20.Name = "ApprovalWF";
            activitybind20.Path = "workflowId";
            // 
            // onWorkflowActivated1
            // 
            this.onWorkflowActivated1.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind19.Name = "ApprovalWF";
            activitybind19.Path = "workflowProperties";
            this.onWorkflowActivated1.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            // 
            // ApprovalWF
            // 
            this.Activities.Add(this.onWorkflowActivated1);
            this.Activities.Add(this.workflowInitData);
            this.Activities.Add(this.IfDelayOnStart);
            this.Activities.Add(this.WorkflowStartedEvent);
            this.Activities.Add(this.CheckStartingCondition);
            this.Activities.Add(this.ContentApproval);
            this.Activities.Add(this.WorkfowEndedEvent);
            this.Name = "ApprovalWF";
            this.CanModifyActivities = false;

        }

        #endregion

        private WhileActivity DocumentNotChangeByUser;

        private IfElseBranchActivity ifElseBranchActivity7;

        private IfElseBranchActivity ifElseBranchActivity4;

        private IfElseActivity ifElseActivity2;

        private TaskEventHandler WorkfowEndedEvent;

        private TaskEventHandler WorkflowStartedEvent;

        private CodeActivity SetWFRejectedEventParameter;

        private CodeActivity SetWFApprovedEventParameter;

        private TaskEventHandler WFRejectedEvent;

        private TaskEventHandler WFApprovedEvent;

        private CodeActivity codeActivity2;

        private Activities.UpdatePermissionActivity updatePermissionActivity1;

        private IfElseBranchActivity ifElseBranchActivity6;

        private IfElseBranchActivity IfEnableUpdatePermission;

        private IfElseActivity ifElseActivity1;

        private IfElseBranchActivity ifElseBranchActivity5;

        private IfElseBranchActivity IfByPass;

        private IfElseActivity CheckStartingCondition;

        private Microsoft.SharePoint.WorkflowActions.DelayForActivity DeplayOnStart;

        private IfElseBranchActivity ifElseBranchActivity3;

        private IfElseBranchActivity ifElseBranchActivity1;

        private IfElseActivity IfDelayOnStart;

        private Microsoft.SharePoint.WorkflowActions.UpdateAllTasks CompleteAllTasks;

        private CodeActivity CompletedTasks;

        private CodeActivity SetItemApprovalData;

        private Activities.PublishItemActivity SetItemApproval;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity IfEnableContentApprovalAndDocumentAppvoed;

        private IfElseActivity ContentApproval;

        private Activities.ApprovalLevel approvalLevel;

        private CodeActivity UpdateApprovalStatus;

        private ReplicatorActivity approvalLevelsReplicator;

        private SequenceActivity sequenceActivity1;

        private CodeActivity workflowInitData;

        private CodeActivity DocumentChangeSendEmail;

        private SequenceActivity sequenceActivity3;

        private ConditionedActivityGroup conditionedActivityGroup1;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged onWorkflowItemChanged1;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;









































































































































    }
}
