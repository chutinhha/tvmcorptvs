using System;
using System.Collections;
using System.ComponentModel;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class TaskApproval : SequenceActivity
    {
        public TaskApproval()
        {
            InitializeComponent();
        }

        #region Properties
        public static DependencyProperty ApprovalInfoTaskProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ApprovalInfoTask", typeof(TaskDetail),
            typeof(TaskApproval));
        [Description("Approval detail tasks")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TaskDetail ApprovalInfoTask
        {
            get
            {
                return ((TaskDetail)(base.GetValue(TaskApproval.ApprovalInfoTaskProperty)));
            }
            set
            {
                base.SetValue(TaskApproval.ApprovalInfoTaskProperty, value);
            }
        }

        public static DependencyProperty WorkflowPropertiesProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties),
            typeof(TaskApproval));
        [Description("Workflow properties ")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SPWorkflowActivationProperties WorkflowProperties
        {
            get
            {
                return ((SPWorkflowActivationProperties)(base.GetValue(TaskApproval.WorkflowPropertiesProperty)));
            }
            set
            {
                base.SetValue(TaskApproval.WorkflowPropertiesProperty, value);
            }
        }

        public static DependencyProperty ParameterProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("Parameter", typeof(Parameter),
            typeof(TaskApproval));
        [Description("Parameter Output")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Parameter Parameter
        {
            get
            {
                return ((Parameter)(base.GetValue(TaskApproval.ParameterProperty)));
            }
            set
            {
                base.SetValue(TaskApproval.ParameterProperty, value);
            }
        }
        #endregion

        #region Fields
        private TaskResultSettings taskResultSettings;
        private int _previousTaskId = -1;
        private string _requestInformationTitle = "A change has been requested on ";
        private bool _notFinishedMainTask = true;
        private int _typeForm = 0; //0: reassigned, requested; 1: finished
        public TaskDetail TaskInfo;
        #endregion

        #region Execute Code
        private void havePreviousTask(object sender, ConditionalEventArgs e)
        {
            e.Result = _notFinishedMainTask;
        }

        private void initialData_ExecuteCode(object sender, EventArgs e)
        {
            taskResultSettings = new TaskResultSettings(WorkflowProperties.Site.ID);
            // reassigned, requested or finished
            if (_previousTaskId != -1)
            {
                createNextTask();
                return;
            }
            createFirstTask();
        }

        private void createFirstTask()
        {
            createGeneralInformationTask();
            if (Parameter.ApprovalConfiguation.DueDateDuration != 0)
                TaskInfo.DueDate = DateTime.Now.AddDays(Parameter.ApprovalConfiguation.DueDateDuration);

            TaskInfo.Title = ApprovalInfoTask.Title;
            TaskInfo.InitialAssignTo = ApprovalInfoTask.InitialAssignTo;
            TaskInfo.AssignToEmail = ApprovalInfoTask.AssignToEmail;
            TaskInfo.PreviousTaskId = _previousTaskId;
            TaskInfo.Type = 0;
            TaskInfo.Body = ApprovalInfoTask.Body;
        }

        private void createGeneralInformationTask()
        {
            TaskInfo = new TaskDetail();
            TaskInfo.Id = Guid.NewGuid();
            TaskInfo.ContentTypeId = ApprovalInfoTask.ContentTypeId;
            TaskInfo.TaskProperties = new SPWorkflowTaskProperties();
            TaskInfo.TaskAfterProperties = new SPWorkflowTaskProperties();
            TaskInfo.PercentComplete = 0f;
            TaskInfo.InfoPathForm = ApprovalInfoTask.InfoPathForm;
        }

        private void createNextTask()
        {
            createGeneralInformationTask();

            TaskInfo.Body = string.IsNullOrEmpty(Parameter.Comment)? ApprovalInfoTask.Body: Parameter.Comment;
            SPListItem taskItem = WorkflowProperties.TaskList.GetItemById(_previousTaskId);
            Hashtable hasPros = SPWorkflowTask.GetExtendedPropertiesAsHashtable(taskItem);
            TaskInfo.PreviousTaskId = _previousTaskId;

            if ((string)hasPros[Constants.Workflow.CCI_TASK_STATUS] == Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT
                && _typeForm == 0)
            {
                string newDueDate = (string)hasPros[Constants.Workflow.CCI_NEW_DUEDATE];
                if (!string.IsNullOrEmpty(newDueDate))
                    TaskInfo.DueDate = Convert.ToDateTime(newDueDate) + DateTime.Now.TimeOfDay;
                
                TaskInfo.InitialAssignTo = (string)hasPros[Constants.Workflow.CCI_REQUEST_TO];
                TaskInfo.AssignToEmail = getEmail(TaskInfo.InitialAssignTo);
                TaskInfo.Title = _requestInformationTitle + Parameter.TaskItemName;
                TaskInfo.Type = 1;
            }

            if ((string)hasPros[Constants.Workflow.CCI_TASK_STATUS] == Constants.Workflow.STATUS_REASSIGN_TEXT
                && _typeForm == 0)
            {
                string newDueDate = (string)hasPros[Constants.Workflow.CCI_NEW_DUEDATE];
                if (!string.IsNullOrEmpty(newDueDate))
                    TaskInfo.DueDate = Convert.ToDateTime(newDueDate) + DateTime.Now.TimeOfDay;
                
                TaskInfo.InitialAssignTo = (string)hasPros[Constants.Workflow.CCI_ASSIGN_TO];
                TaskInfo.AssignToEmail = getEmail(TaskInfo.InitialAssignTo);                
                TaskInfo.Title = taskItem[SPBuiltInFieldId.Title].ToString();
                TaskInfo.Type = Convert.ToInt32(hasPros[Constants.Workflow.CCI_TASK_TYPE]);
            }

            // assigned to requestee
            if ((string)hasPros[Constants.Workflow.CCI_TASK_STATUS] == Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT
              && _typeForm == 1)
            {
                if (taskItem[SPBuiltInFieldId.AssignedTo] != null)
                {
                    SPFieldUserValue value = new SPFieldUserValue(WorkflowProperties.Web, taskItem[SPBuiltInFieldId.AssignedTo].ToString());
                    TaskInfo.InitialAssignTo = value.User != null ? value.User.LoginName : value.LookupValue;
                    TaskInfo.AssignToEmail = getEmail(TaskInfo.InitialAssignTo);
                }
                if (taskItem[SPBuiltInFieldId.TaskDueDate] != null)
                    TaskInfo.DueDate = Convert.ToDateTime(taskItem[SPBuiltInFieldId.TaskDueDate].ToString());
                
                TaskInfo.Title = taskItem[SPBuiltInFieldId.Title].ToString();
                TaskInfo.Type = 0;
            }
        }

        private string getEmail(string user)
        {
            string email = string.Empty;
            if (SPUtility.IsLoginValid(WorkflowProperties.Site, user))
            {
                SPUser myUser = WorkflowProperties.Site.RootWeb.EnsureUser(user);
                email = myUser.Email;
            }
            return email;
        }

        private void completedTask_ExecuteCode(object sender, EventArgs e)
        {
            switch (TaskInfo.StatusWorkflow)
            {
                case Constants.Workflow.STATUS_CANCEL_TEXT:
                    _notFinishedMainTask = false;
                    break;
                case Constants.Workflow.STATUS_REASSIGN_TEXT:
                case Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT:
                    _notFinishedMainTask = true;
                    _previousTaskId = Parameter.TaskItem;
                    _typeForm = 0;
                    break;
                case Constants.Workflow.STATUS_FINISHED_TEXT:
                    _notFinishedMainTask = true;
                    _previousTaskId = getRootTaskIdRequested(Parameter.TaskItem);
                    _typeForm = 1;
                    break;
                default:
                    if (string.Compare(TaskInfo.StatusWorkflow, taskResultSettings.DataQualityCompletedText, true) == 0 ||
                        string.Compare(TaskInfo.StatusWorkflow, taskResultSettings.SignatureVerifiedText, true) == 0 ||
                        string.Compare(TaskInfo.StatusWorkflow, taskResultSettings.RejectedText, true) == 0 ||
                        string.Compare(TaskInfo.StatusWorkflow, taskResultSettings.ApprovedText, true) == 0)
                        _notFinishedMainTask = false;
                    break;
            }

            ApprovalInfoTask.Status = TaskInfo.Status;
            ApprovalInfoTask.StatusWorkflow = TaskInfo.StatusWorkflow;
            ApprovalInfoTask.TaskId = TaskInfo.TaskId;
        }

        private int getRootTaskIdRequested(int currentTaskId)
        {
            int rootTaskId = currentTaskId;
            if (currentTaskId == -1) return currentTaskId;
            while (true)
            {
                SPListItem taskItem = WorkflowProperties.TaskList.GetItemById(rootTaskId);
                Hashtable hasPros = SPWorkflowTask.GetExtendedPropertiesAsHashtable(taskItem);
                
                if (Convert.ToInt32(hasPros[Constants.Workflow.CCI_PREVIOUS_TASK_ID]) == -1)
                    return rootTaskId;
                
                if ((string)hasPros[Constants.Workflow.CCI_TASK_STATUS] == Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT)
                    return rootTaskId;

                rootTaskId = Convert.ToInt32(hasPros[Constants.Workflow.CCI_PREVIOUS_TASK_ID]);
            }
            return rootTaskId;
        }
        #endregion
    }
}
