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

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities
{
    public partial class ApprovalUnitWrapper
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.SingleTask = new TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit();
            this.codeActivity2 = new System.Workflow.Activities.CodeActivity();
            this.TaskLifeCycleLoop = new System.Workflow.Activities.WhileActivity();
            this.codeActivity1 = new System.Workflow.Activities.CodeActivity();
            // 
            // SingleTask
            // 
            activitybind1.Name = "ApprovalUnitWrapper";
            activitybind1.Path = "TaskInfo.AppendTitle";
            activitybind2.Name = "ApprovalUnitWrapper";
            activitybind2.Path = "TaskInfo.Approver";
            this.SingleTask.ApproverEmail = null;
            activitybind3.Name = "ApprovalUnitWrapper";
            activitybind3.Path = "TaskInfo.DueDate";
            activitybind4.Name = "ApprovalUnitWrapper";
            activitybind4.Path = "TaskInfo.TaskDuration";
            activitybind5.Name = "ApprovalUnitWrapper";
            activitybind5.Path = "TaskInfo.Message";
            activitybind6.Name = "ApprovalUnitWrapper";
            activitybind6.Path = "TaskInfo.MessageTitle";
            activitybind7.Name = "ApprovalUnitWrapper";
            activitybind7.Path = "TaskInfo.MailEnable";
            activitybind8.Name = "ApprovalUnitWrapper";
            activitybind8.Path = "FormOption";
            activitybind9.Name = "ApprovalUnitWrapper";
            activitybind9.Path = "TaskInfo.TaskInstruction";
            this.SingleTask.Name = "SingleTask";
            activitybind10.Name = "ApprovalUnitWrapper";
            activitybind10.Path = "approvalWorkflow_PreviousTaskId2";
            activitybind11.Name = "ApprovalUnitWrapper";
            activitybind11.Path = "TaskInfo.TaskContentType";
            activitybind12.Name = "ApprovalUnitWrapper";
            activitybind12.Path = "TaskInfo.TaskInstruction";
            activitybind13.Name = "ApprovalUnitWrapper";
            activitybind13.Path = "TaskInfo.TaskEvents";
            this.SingleTask.TaskId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind14.Name = "ApprovalUnitWrapper";
            activitybind14.Path = "TaskOutcome";
            activitybind15.Name = "ApprovalUnitWrapper";
            activitybind15.Path = "TaskInfo.TaskTitle";
            activitybind16.Name = "ApprovalUnitWrapper";
            activitybind16.Path = "TaskInfo.UpdatedProperties";
            activitybind17.Name = "ApprovalUnitWrapper";
            activitybind17.Path = "workflowProperties";
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskContentTypeIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.PreviousTaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskTitleProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.MessageProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.DueDateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.DurationPerTaskProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.ApproverProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.EmailBodyProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.EmailTitleProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.EnableEmailProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.AppendTitleProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.FormOptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.UpdatedPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.SingleTask.SetBinding(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskEventsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // codeActivity2
            // 
            this.codeActivity2.Name = "codeActivity2";
            this.codeActivity2.ExecuteCode += new System.EventHandler(this.codeActivity2_ExecuteCode);
            // 
            // TaskLifeCycleLoop
            // 
            this.TaskLifeCycleLoop.Activities.Add(this.SingleTask);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsApprovalNotCompleted);
            this.TaskLifeCycleLoop.Condition = codecondition1;
            this.TaskLifeCycleLoop.Name = "TaskLifeCycleLoop";
            // 
            // codeActivity1
            // 
            this.codeActivity1.Name = "codeActivity1";
            this.codeActivity1.ExecuteCode += new System.EventHandler(this.codeActivity1_ExecuteCode);
            // 
            // ApprovalUnitWrapper
            // 
            this.Activities.Add(this.codeActivity1);
            this.Activities.Add(this.TaskLifeCycleLoop);
            this.Activities.Add(this.codeActivity2);
            this.Name = "ApprovalUnitWrapper";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity codeActivity2;

        private CodeActivity codeActivity1;

        private WhileActivity TaskLifeCycleLoop;

        private ApprovalUnit SingleTask;









































































    }
}
