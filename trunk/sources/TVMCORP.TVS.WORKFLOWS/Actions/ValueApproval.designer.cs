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

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
    public partial class ValueApproval
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.approvalWorkflow = new TVMCORP.TVS.WORKFLOWS.Actions.ApprovalWorkflow();
            this.customLogToWFHistoryActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.setDescriptionLog = new System.Workflow.Activities.CodeActivity();
            this.replicatorApprovalWorkflow = new System.Workflow.Activities.ReplicatorActivity();
            this.initialValueApprovalData = new System.Workflow.Activities.CodeActivity();
            // 
            // approvalWorkflow
            // 
            activitybind1.Name = "ValueApproval";
            activitybind1.Path = "__ActivationProperties";
            activitybind2.Name = "ValueApproval";
            activitybind2.Path = "__ListId";
            activitybind3.Name = "ValueApproval";
            activitybind3.Path = "__ListItem";
            activitybind4.Name = "ValueApproval";
            activitybind4.Path = "_strApprovalConfigurationListURL";
            this.approvalWorkflow.ApprovalName = "";
            activitybind5.Name = "ValueApproval";
            activitybind5.Path = "ApprovalWorkflowParameter";
            this.approvalWorkflow.Name = "approvalWorkflow";
            this.approvalWorkflow.Status = "";
            this.approvalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.ApprovalWorkflow.@__ActivationPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.approvalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.ApprovalWorkflow.ApprovalConfigListURLProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.approvalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.ApprovalWorkflow.@__ListIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.approvalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.ApprovalWorkflow.@__ListItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.approvalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.ApprovalWorkflow.ApprovalWorkflowParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // customLogToWFHistoryActivity1
            // 
            this.customLogToWFHistoryActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind6.Name = "ValueApproval";
            activitybind6.Path = "_strLogDescription";
            activitybind7.Name = "ValueApproval";
            activitybind7.Path = "Status";
            this.customLogToWFHistoryActivity1.Name = "customLogToWFHistoryActivity1";
            activitybind8.Name = "ValueApproval";
            activitybind8.Path = "__ActivationProperties";
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // setDescriptionLog
            // 
            this.setDescriptionLog.Name = "setDescriptionLog";
            this.setDescriptionLog.ExecuteCode += new System.EventHandler(this.logResultToHistoryList_ExecuteCode);
            activitybind9.Name = "ValueApproval";
            activitybind9.Path = "_listApprovalLevel";
            // 
            // replicatorApprovalWorkflow
            // 
            this.replicatorApprovalWorkflow.Activities.Add(this.approvalWorkflow);
            this.replicatorApprovalWorkflow.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.replicatorApprovalWorkflow.Name = "replicatorApprovalWorkflow";
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.untilReplicatorCondition_ExecuteCode);
            this.replicatorApprovalWorkflow.UntilCondition = codecondition1;
            this.replicatorApprovalWorkflow.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.initializedChild_ExecuteCode);
            this.replicatorApprovalWorkflow.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.completedChild_ExecuteCode);
            this.replicatorApprovalWorkflow.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // initialValueApprovalData
            // 
            this.initialValueApprovalData.Name = "initialValueApprovalData";
            this.initialValueApprovalData.ExecuteCode += new System.EventHandler(this.initialValueApprovalData_ExecuteCode);
            // 
            // ValueApproval
            // 
            this.Activities.Add(this.initialValueApprovalData);
            this.Activities.Add(this.replicatorApprovalWorkflow);
            this.Activities.Add(this.setDescriptionLog);
            this.Activities.Add(this.customLogToWFHistoryActivity1);
            this.Name = "ValueApproval";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity setDescriptionLog;

        private Activities.CustomLogToWFHistoryActivity customLogToWFHistoryActivity1;

        private ReplicatorActivity replicatorApprovalWorkflow;

        private ApprovalWorkflow approvalWorkflow;

        private CodeActivity initialValueApprovalData;














    }
}
