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

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class TaskApproval
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.completedTask = new System.Workflow.Activities.CodeActivity();
            this.taskApprovalActivity1 = new TVMCORP.TVS.WORKFLOWS.Activities.TaskApprovalActivity();
            this.initialData = new System.Workflow.Activities.CodeActivity();
            this.sequenceActivity1 = new System.Workflow.Activities.SequenceActivity();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            // 
            // completedTask
            // 
            this.completedTask.Name = "completedTask";
            this.completedTask.ExecuteCode += new System.EventHandler(this.completedTask_ExecuteCode);
            // 
            // taskApprovalActivity1
            // 
            activitybind1.Name = "TaskApproval";
            activitybind1.Path = "TaskInfo";
            this.taskApprovalActivity1.Name = "taskApprovalActivity1";
            activitybind2.Name = "TaskApproval";
            activitybind2.Path = "Parameter";
            activitybind3.Name = "TaskApproval";
            activitybind3.Path = "WorkflowProperties";
            this.taskApprovalActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskApprovalActivity.ParameterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.taskApprovalActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskApprovalActivity.ApprovalInfoTaskProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.taskApprovalActivity1.SetBinding(TVMCORP.TVS.WORKFLOWS.Activities.TaskApprovalActivity.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // initialData
            // 
            this.initialData.Name = "initialData";
            this.initialData.ExecuteCode += new System.EventHandler(this.initialData_ExecuteCode);
            // 
            // sequenceActivity1
            // 
            this.sequenceActivity1.Activities.Add(this.initialData);
            this.sequenceActivity1.Activities.Add(this.taskApprovalActivity1);
            this.sequenceActivity1.Activities.Add(this.completedTask);
            this.sequenceActivity1.Name = "sequenceActivity1";
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.sequenceActivity1);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.havePreviousTask);
            this.whileActivity1.Condition = codecondition1;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // TaskApproval
            // 
            this.Activities.Add(this.whileActivity1);
            this.Name = "TaskApproval";
            this.CanModifyActivities = false;

        }

        #endregion

        private SequenceActivity sequenceActivity1;
        private CodeActivity initialData;
        private CodeActivity completedTask;
        private TaskApprovalActivity taskApprovalActivity1;
        private WhileActivity whileActivity1;














    }
}
