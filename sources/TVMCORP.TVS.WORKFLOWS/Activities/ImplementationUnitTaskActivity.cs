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
    public partial class ImplementationUnitTaskActivity : SequenceActivity
    {
        public ImplementationUnitTaskActivity()
        {
            InitializeComponent();
        }

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChanged;

        private Microsoft.SharePoint.WorkflowActions.CompleteTask completeTask;

        private WhileActivity whileActivity;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTask;
        public string createTask_ContentTypeId = default(System.String);
        public Guid createTask_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTask_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChanged_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChanged_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            this.onTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.completeTask = new Microsoft.SharePoint.WorkflowActions.CompleteTask();
            this.whileActivity = new System.Workflow.Activities.WhileActivity();
            this.createTask = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            // 
            // onTaskChanged
            // 
            activitybind1.Name = "ImplementationUnitTaskActivity";
            activitybind1.Path = "onTaskChanged_AfterProperties";
            activitybind2.Name = "ImplementationUnitTaskActivity";
            activitybind2.Path = "onTaskChanged_BeforeProperties";
            correlationtoken1.Name = "taskToken";
            correlationtoken1.OwnerActivityName = "ImplementationUnitTaskActivity";
            this.onTaskChanged.CorrelationToken = correlationtoken1;
            this.onTaskChanged.Executor = null;
            this.onTaskChanged.Name = "onTaskChanged";
            activitybind3.Name = "ImplementationUnitTaskActivity";
            activitybind3.Path = "createTask_TaskId";
            this.onTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChanged_Invoked);
            this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // completeTask
            // 
            this.completeTask.CorrelationToken = correlationtoken1;
            this.completeTask.Name = "completeTask";
            activitybind4.Name = "ImplementationUnitTaskActivity";
            activitybind4.Path = "createTask_TaskId";
            this.completeTask.TaskOutcome = "Done";
            this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // whileActivity
            // 
            this.whileActivity.Activities.Add(this.onTaskChanged);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsTaskCompleted);
            this.whileActivity.Condition = codecondition1;
            this.whileActivity.Name = "whileActivity";
            // 
            // createTask
            // 
            activitybind5.Name = "ImplementationUnitTaskActivity";
            activitybind5.Path = "createTask_ContentTypeId";
            this.createTask.CorrelationToken = correlationtoken1;
            this.createTask.ListItemId = -1;
            this.createTask.Name = "createTask";
            this.createTask.SpecialPermissions = null;
            activitybind6.Name = "ImplementationUnitTaskActivity";
            activitybind6.Path = "createTask_TaskId";
            activitybind7.Name = "ImplementationUnitTaskActivity";
            activitybind7.Path = "createTask_TaskProperties";
            this.createTask.MethodInvoking += new System.EventHandler(this.createTask_MethodInvoking);
            this.createTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.ContentTypeIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.createTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.createTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // ImplementationUnitTaskActivity
            // 
            this.Activities.Add(this.createTask);
            this.Activities.Add(this.whileActivity);
            this.Activities.Add(this.completeTask);
            this.Name = "ImplementationUnitTaskActivity";
            this.CanModifyActivities = false;

        }

        public bool _isTaskApproved = false;

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskAssignedTo { get; set; }

        private void IsTaskCompleted(object sender, ConditionalEventArgs e)
        {
            e.Result = !_isTaskApproved;
        }

        public SPWorkflowActivationProperties WorkflowProperties
        {
            get { return (SPWorkflowActivationProperties)GetValue(WorkflowPropertiesProperty); }
            set { SetValue(WorkflowPropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WorkflowProperties.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty WorkflowPropertiesProperty =
            DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties), typeof(ImplementationUnitTaskActivity));

        private void createTask_MethodInvoking(object sender, EventArgs e)
        {
            WorkflowProperties.TaskList.EnsureContentTypeInList(TMVCorpContentType.IMPLEMENTATIONUNIT_WF_TASK_CONTENTTYPE);

            createTask_TaskId = Guid.NewGuid();
            createTask_ContentTypeId = TMVCorpContentType.IMPLEMENTATIONUNIT_WF_TASK_CONTENTTYPE;

            createTask_TaskProperties.AssignedTo = TaskAssignedTo;
            createTask_TaskProperties.Description = TaskDescription;
            createTask_TaskProperties.Title = TaskTitle;
        }

        private void onTaskChanged_Invoked(object sender, ExternalDataEventArgs e)
        {
            SPTaskServiceEventArgs args = (SPTaskServiceEventArgs)e;

            if (CheckTaskStatus(args.afterProperties, TaskApprovalStatus.Approved))
            {
                _isTaskApproved = true;
            }
        }

        public bool CheckTaskStatus(SPWorkflowTaskProperties TaskProperties, string status)
        {
            return TaskProperties.ExtendedProperties[TaskExtendProperties.OWS_TASK_STATUS] != null
                && TaskProperties.ExtendedProperties[TaskExtendProperties.OWS_TASK_STATUS].ToString() == status;
        }
    }
}
