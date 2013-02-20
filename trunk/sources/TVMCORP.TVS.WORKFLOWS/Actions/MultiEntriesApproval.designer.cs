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
    public partial class MultiEntriesApproval
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
            this.entryApprovalWorkflow = new TVMCORP.TVS.WORKFLOWS.Actions.EntryApproval();
            this.customLogToWFHistoryActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity();
            this.replicatorMultiEntries = new System.Workflow.Activities.ReplicatorActivity();
            this.initialMultiEntriesApprovalData = new System.Workflow.Activities.CodeActivity();
            // 
            // entryApprovalWorkflow
            // 
            activitybind1.Name = "MultiEntriesApproval";
            activitybind1.Path = "__ActivationProperties";
            activitybind2.Name = "MultiEntriesApproval";
            activitybind2.Path = "__ListId";
            activitybind3.Name = "MultiEntriesApproval";
            activitybind3.Path = "__ListItem";
            this.entryApprovalWorkflow.ApprovalKey = null;
            this.entryApprovalWorkflow.ApprovalListId = null;
            this.entryApprovalWorkflow.Name = "entryApprovalWorkflow";
            this.entryApprovalWorkflow.Status = null;
            this.entryApprovalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.EntryApproval.@__ActivationPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.entryApprovalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.EntryApproval.@__ListIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.entryApprovalWorkflow.SetBinding(TVMCORP.TVS.WORKFLOWS.Actions.EntryApproval.@__ListItemProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // customLogToWFHistoryActivity1
            // 
            this.customLogToWFHistoryActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.customLogToWFHistoryActivity1.HistoryDescription = "Multi Entries Appproval workflow completed";
            this.customLogToWFHistoryActivity1.HistoryOutcome = "";
            this.customLogToWFHistoryActivity1.Name = "customLogToWFHistoryActivity1";
            activitybind4.Name = "MultiEntriesApproval";
            activitybind4.Path = "__ActivationProperties";
            this.customLogToWFHistoryActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.CustomLogToWFHistoryActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            activitybind5.Name = "MultiEntriesApproval";
            activitybind5.Path = "_approvalEntries";
            // 
            // replicatorMultiEntries
            // 
            this.replicatorMultiEntries.Activities.Add(this.entryApprovalWorkflow);
            this.replicatorMultiEntries.ExecutionType = System.Workflow.Activities.ExecutionType.Parallel;
            this.replicatorMultiEntries.Name = "replicatorMultiEntries";
            this.replicatorMultiEntries.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.initializedChild_ExecuteCode);
            this.replicatorMultiEntries.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // initialMultiEntriesApprovalData
            // 
            this.initialMultiEntriesApprovalData.Name = "initialMultiEntriesApprovalData";
            this.initialMultiEntriesApprovalData.ExecuteCode += new System.EventHandler(this.initialMultiEntriesApprovalData_ExecuteCode);
            // 
            // MultiEntriesApproval
            // 
            this.Activities.Add(this.initialMultiEntriesApprovalData);
            this.Activities.Add(this.replicatorMultiEntries);
            this.Activities.Add(this.customLogToWFHistoryActivity1);
            this.Name = "MultiEntriesApproval";
            this.CanModifyActivities = false;

        }

        #endregion

        private Activities.CustomLogToWFHistoryActivity customLogToWFHistoryActivity1;

        private ReplicatorActivity replicatorMultiEntries;

        private EntryApproval entryApprovalWorkflow;

        private CodeActivity initialMultiEntriesApprovalData;




















    }
}
