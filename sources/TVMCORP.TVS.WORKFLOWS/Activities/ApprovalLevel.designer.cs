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
using TVMCORP.TVS.WORKFLOWS.Activities;

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities
{
    public partial class ApprovalLevel
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
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.SingleTaskApproval = new TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnitWrapper();
            this.customLogToWFHistoryActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.MultiTaskReplicator = new System.Workflow.Activities.ReplicatorActivity();
            this.SetData = new System.Workflow.Activities.CodeActivity();
            // 
            // SingleTaskApproval
            // 
            this.SingleTaskApproval.ApprovalInfo = null;
            activitybind1.Name = "ApprovalLevel";
            activitybind1.Path = "ApprovalData.FormOption";
            this.SingleTaskApproval.Name = "SingleTaskApproval";
            activitybind2.Name = "ApprovalLevel";
            activitybind2.Path = "TaskContentTypeId";
            this.SingleTaskApproval.TaskInfo = null;
            activitybind3.Name = "ApprovalLevel";
            activitybind3.Path = "TaskOutcome";
            this.SingleTaskApproval.TaskSequenceType = null;
            activitybind4.Name = "ApprovalLevel";
            activitybind4.Path = "workflowProperties";
            this.SingleTaskApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnitWrapper.workflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.SingleTaskApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnitWrapper.TaskContentTypeIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.SingleTaskApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnitWrapper.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.SingleTaskApproval.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnitWrapper.FormOptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // customLogToWFHistoryActivity1
            // 
            this.customLogToWFHistoryActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.customLogToWFHistoryActivity1.HistoryDescription = "End of approval level";
            this.customLogToWFHistoryActivity1.HistoryOutcome = "";
            this.customLogToWFHistoryActivity1.Name = "customLogToWFHistoryActivity1";
            activitybind5.Name = "ApprovalLevel";
            activitybind5.Path = "workflowProperties";
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            activitybind7.Name = "ApprovalLevel";
            activitybind7.Path = "Approvers";
            // 
            // MultiTaskReplicator
            // 
            this.MultiTaskReplicator.Activities.Add(this.SingleTaskApproval);
            activitybind6.Name = "ApprovalLevel";
            activitybind6.Path = "MultiTaskReplicator_ExecutionType";
            this.MultiTaskReplicator.Name = "MultiTaskReplicator";
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TaskRejected);
            this.MultiTaskReplicator.UntilCondition = codecondition1;
            this.MultiTaskReplicator.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.MultiTaskReplicator_ChildInitialized);
            this.MultiTaskReplicator.Completed += new System.EventHandler(this.ApprovalTaskCompleted);
            this.MultiTaskReplicator.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.MultiTaskReplicator.SetBinding(System.Workflow.Activities.ReplicatorActivity.ExecutionTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // SetData
            // 
            this.SetData.Name = "SetData";
            this.SetData.ExecuteCode += new System.EventHandler(this.SetData_ExecuteCode);
            // 
            // ApprovalLevel
            // 
            this.Activities.Add(this.SetData);
            this.Activities.Add(this.MultiTaskReplicator);
            this.Activities.Add(this.customLogToWFHistoryActivity1);
            this.Name = "ApprovalLevel";
            this.CanModifyActivities = false;

        }

        #endregion

        private CustomLogToWFHistoryActivity customLogToWFHistoryActivity1;

        private CodeActivity SetData;

        private ApprovalUnitWrapper SingleTaskApproval;

        private ReplicatorActivity MultiTaskReplicator;


































































    }
}
