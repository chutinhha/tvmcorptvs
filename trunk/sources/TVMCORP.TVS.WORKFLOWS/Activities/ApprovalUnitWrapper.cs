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
using TVMCORP.TVS.WORKFLOWS.Activities;
using System.Text;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{

    public partial class ApprovalUnitWrapper : SequenceActivity
    {
        public ApprovalUnitWrapper()
        {
            InitializeComponent();
        }

        public TaskInfo TaskInfo
        {
            get { return (TaskInfo)GetValue(TaskInfoProperty); }
            set { SetValue(TaskInfoProperty, value); }
        }


        public TaskFormOption FormOption
        {
            get { return (TaskFormOption)GetValue(FormOptionProperty); }
            set { SetValue(FormOptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormOption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormOptionProperty =
            DependencyProperty.Register("FormOption", typeof(TaskFormOption), typeof(ApprovalUnitWrapper));



        // Using a DependencyProperty as the backing store for TaskInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskInfoProperty =
            DependencyProperty.Register("TaskInfo", typeof(TaskInfo), typeof(ApprovalUnitWrapper));


        public Guid workflowId = default(System.Guid);

        public SPWorkflowActivationProperties workflowProperties
        {
            get { return (SPWorkflowActivationProperties)GetValue(workflowPropertiesProperty); }
            set { SetValue(workflowPropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for workflowProperties.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty workflowPropertiesProperty =
            DependencyProperty.Register("workflowProperties", typeof(SPWorkflowActivationProperties), typeof(ApprovalUnitWrapper));

        public string TaskContentTypeId
        {
            get { return (string)GetValue(TaskContentTypeIdProperty); }
            set { SetValue(TaskContentTypeIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskContentTypeId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskContentTypeIdProperty =
            DependencyProperty.Register("TaskContentTypeId", typeof(string), typeof(ApprovalUnitWrapper));

        public ApprovalLevelInfo ApprovalInfo
        {
            get { return (ApprovalLevelInfo)GetValue(ApprovalInfoProperty); }
            set { SetValue(ApprovalInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApprovalInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApprovalInfoProperty =
            DependencyProperty.Register("ApprovalInfo", typeof(ApprovalLevelInfo), typeof(ApprovalUnitWrapper));


        private void replicatorActivity1_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            string strApprover = (string)e.InstanceData;
            ApprovalUnit approvalActivity = (ApprovalUnit)e.Activity;
            if (ApprovalInfo.ExpendGroup)
            {

                string[] strUsers = strApprover.Split(';');
                //SPUserCollection users = new SPUserCollection(workflowProperties.Web, strUsers);
                //users.GetCollection(strApprover.Split(';'));
                SPFieldUserValueCollection readers = new SPFieldUserValueCollection(workflowProperties.Web, strApprover);

                foreach (string strUser in strUsers)
                {
                    SPGroup spApprovalGroup = workflowProperties.Web.Groups[strUser];
                    if (spApprovalGroup != null)
                    {
                        foreach (SPUser userInGroup in spApprovalGroup.Users)
                        {
                            strApprover += userInGroup.LoginName + ";";
                        }
                    }
                    else
                    {
                        strApprover += strUser + ";";
                    }
                }
            }
            else approvalActivity.Approver = strApprover;

            approvalActivity.DueDate = ApprovalInfo.DueDate;
            approvalActivity.DurationPerTask = ApprovalInfo.DurationPerTask;
            approvalActivity.Message = ApprovalInfo.Message;
            approvalActivity.TaskTitle = ApprovalInfo.TaskTitle;
        }


        private void IsApprovalNotCompleted(object sender, ConditionalEventArgs e)
        {
            e.Result = (TaskOutcome == TaskApprovalStatus.Initiated ||
                        TaskOutcome == TaskApprovalStatus.Reasigned ||
                        TaskOutcome == TaskApprovalStatus.RequestChange ||
                        TaskOutcome == TaskApprovalStatus.RequestInf);
        }

        public String approvalWorkflow_TaskOutcome1 = default(System.String);
        public Int32 approvalWorkflow_PreviousTaskId2 = -1;



        public string TaskSequenceType
        {
            get { return (string)GetValue(TaskSequenceTypeProperty); }
            set { SetValue(TaskSequenceTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskSequenceType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskSequenceTypeProperty =
            DependencyProperty.Register("TaskSequenceType", typeof(string), typeof(ApprovalUnitWrapper));

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {
            TaskOutcome = TaskApprovalStatus.Initiated;
            //replicatorApprovers.ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), TaskSequenceType);
        }


        public string TaskOutcome
        {
            get { return (string)GetValue(TaskOutcomeProperty); }
            set { SetValue(TaskOutcomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskOutcome.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskOutcomeProperty =
            DependencyProperty.Register("TaskOutcome", typeof(string), typeof(ApprovalUnitWrapper));

        private void codeActivity2_ExecuteCode(object sender, EventArgs e)
        {
            //TaskOutcome = "Completed";
        }

    }
}
