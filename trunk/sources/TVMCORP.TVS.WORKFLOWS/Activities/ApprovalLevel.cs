using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using System.Workflow.Activities;
using TVMCORP.TVS.UTIL.MODELS;
using System.Collections.Generic;
using TVMCORP.TVS.UTIL.MODELS;
using System.Linq;
using TVMCORP.TVS.UTIL;
namespace TVMCORP.TVS.WORKFLOWS.Core.Activities
{

    public partial class ApprovalLevel : SequenceActivity
    {
        public ApprovalLevel()
        {
            InitializeComponent();
        }

        public bool EndAtFirstRejection
        {
            get { return (bool)GetValue(EndAtFirstRejectionProperty); }
            set { SetValue(EndAtFirstRejectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndAtFirstRejection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndAtFirstRejectionProperty =
            DependencyProperty.Register("EndAtFirstRejection", typeof(bool), typeof(ApprovalLevel));


        public ApprovalLevelInfo ApprovalData
        {
            get { return (ApprovalLevelInfo)GetValue(ApprovalDataProperty); }
            set { SetValue(ApprovalDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApprovalData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApprovalDataProperty =
            DependencyProperty.Register("ApprovalData", typeof(ApprovalLevelInfo), typeof(ApprovalLevel));

        public Guid workflowId = default(System.Guid);

        public SPWorkflowActivationProperties workflowProperties
        {
            get { return (SPWorkflowActivationProperties)GetValue(workflowPropertiesProperty); }
            set { SetValue(workflowPropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for workflowProperties.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty workflowPropertiesProperty =
            DependencyProperty.Register("workflowProperties", typeof(SPWorkflowActivationProperties), typeof(ApprovalLevel));

        public string TaskContentTypeId
        {
            get { return (string)GetValue(TaskContentTypeIdProperty); }
            set { SetValue(TaskContentTypeIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskContentTypeId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskContentTypeIdProperty =
            DependencyProperty.Register("TaskContentTypeId", typeof(string), typeof(ApprovalLevel));





        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            workflowId = workflowProperties.WorkflowId;

        }


        private void replicatorActivity1_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            ApprovalLevelInfo approvalInfo = (ApprovalLevelInfo)e.InstanceData;
            ApprovalUnitWrapper approvalControl = (ApprovalUnitWrapper)e.Activity;

            approvalControl.ApprovalInfo = approvalInfo;
            approvalControl.TaskContentTypeId = approvalInfo.TaskContenType;

            approvalControl.TaskSequenceType = approvalInfo.TaskSequenceType;
        }

        public System.Collections.IList replicatorApprovalLevels_InitialChildData1 = default(System.Collections.IList);

        //private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        //{
        //    ApprovalWFInitiationData approvalLevels = Common.Helpers.SerializationHelper.DeserializeFromXml<ApprovalWFInitiationData>(workflowProperties.InitiationData);
        //    replicatorApprovalLevels_InitialChildData1 = approvalLevels.ApprovalLevels;
        //}

        public string TaskOutcome
        {
            get { return (string)GetValue(TaskOutcomeProperty); }
            set { SetValue(TaskOutcomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskOutcome.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskOutcomeProperty =
            DependencyProperty.Register("TaskOutcome", typeof(string), typeof(ApprovalLevel));
        public System.Collections.IList Approvers = default(System.Collections.IList);

        private void SetData_ExecuteCode(object sender, EventArgs e)
        {
            string approversName = string.Empty;

            var approvers = GetApprovers(this.ApprovalData);
            approversName = String.Join(",", approvers.ToArray());
            Approvers = approvers.Select(p => new TaskInfo()
           {
               Approver = p,
               MessageTitle = this.ApprovalData.MessageTitle,
               Message = this.ApprovalData.Message,
               MailEnable = this.ApprovalData.EnableEmail,
               TaskContentType = this.ApprovalData.TaskContenType,
               TaskTitle = this.ApprovalData.TaskTitle,
               AppendTitle = this.ApprovalData.AppendTitle,
               TaskInstruction = this.ApprovalData.TaskInstruction,
               DueDate = this.ApprovalData.DueDate,
               TaskDuration = this.ApprovalData.DurationPerTask,
               UpdatedProperties = this.ApprovalData.UpdatedProperties,
               TaskEvents = this.ApprovalData.TaskEvents,
           }).ToList();
            MultiTaskReplicator_ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), this.ApprovalData.TaskSequenceType);
            workflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowComment, "Begin approval level: " + ApprovalData.LevelName , MultiTaskReplicator_ExecutionType.ToString());
            workflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowComment, "Tasks was created and sent to : " + approversName, string.Empty);
            

        }


        private List<string> GetApprovers(ApprovalLevelInfo approvalLevelInfo)
        {
            //TOTO - Modify this function to allow get dynamic user as a setting inputed from initiation form
            
            return approvalLevelInfo.SpecificUserGroup;
        }

        private TaskInfo currentTaskInfo = null;

        private void MultiTaskReplicator_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            ApprovalUnitWrapper activity = e.Activity as ApprovalUnitWrapper;
            currentTaskInfo = e.InstanceData as TaskInfo;
            activity.TaskInfo = currentTaskInfo;

        }

        public ExecutionType MultiTaskReplicator_ExecutionType = default(System.Workflow.Activities.ExecutionType);
        public TaskFormOption SingleTaskApproval_FormOption = new TVMCORP.TVS.UTIL.MODELS.TaskFormOption();

        private void TaskRejected(object sender, ConditionalEventArgs e)
        {
            //e.Result = (TaskOutcome == TaskApprovalStatus.Approved ||
            e.Result = EndAtFirstRejection && TaskOutcome == TaskApprovalStatus.Rejected
            || MultiTaskReplicator.AllChildrenComplete;

        }

        private void ApprovalTaskCompleted(object sender, EventArgs e)
        {

        }

    }
}
