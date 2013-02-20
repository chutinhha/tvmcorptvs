using System;
using System.ComponentModel;
using System.Collections;
using System.Workflow.ComponentModel;
using System.Workflow.Activities;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint;
using System.Linq;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;
using System.Collections.Generic;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities
{
    public sealed partial class ApprovalUnit : SequenceActivity
    {
        public ApprovalUnit()
        {
            InitializeComponent();
        }

        #region "Reference"

        //public Guid TaskId
        //{
        //    get { return (Guid)GetValue(TaskIdProperty); }
        //    set { SetValue(TaskIdProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TaskId.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TaskIdProperty =
        //    DependencyProperty.Register("TaskId", typeof(Guid), typeof(ApprovalUnit));


        ////public int PreviousTaskId = -1;


        //public int PreviousTaskId
        //{
        //    get { return (int)GetValue(PreviousTaskIdProperty); }
        //    set { SetValue(PreviousTaskIdProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PreviousTaskId.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PreviousTaskIdProperty =
        //    DependencyProperty.Register("PreviousTaskId", typeof(int), typeof(ApprovalUnit));

        //public string Approver
        //{
        //    get { return (string)GetValue(ApproverProperty); }
        //    set { SetValue(ApproverProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Approver.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ApproverProperty =
        //    DependencyProperty.Register("Approver", typeof(string), typeof(ApprovalUnit));


        //public string EmailTitle
        //{
        //    get { return (string)GetValue(EmailTitleProperty); }
        //    set { SetValue(EmailTitleProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for EmailTitle.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty EmailTitleProperty =
        //    DependencyProperty.Register("EmailTitle", typeof(string), typeof(ApprovalUnit));

        //public string EmailBody
        //{
        //    get { return (string)GetValue(EmailBodyProperty); }
        //    set { SetValue(EmailBodyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for EmailBody.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty EmailBodyProperty =
        //    DependencyProperty.Register("EmailBody", typeof(string), typeof(ApprovalUnit));


        //public SPWorkflowActivationProperties WorkflowProperties
        //{
        //    get { return (SPWorkflowActivationProperties)GetValue(WorkflowPropertiesProperty); }
        //    set { SetValue(WorkflowPropertiesProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for WorkflowProperties.  This enables animation, styling, binding, etc...

        //public static readonly DependencyProperty WorkflowPropertiesProperty =
        //    DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties), typeof(ApprovalUnit));


        //public Guid workflowId = default(System.Guid);

        ////public Guid taskId = default(System.Guid);
        //public SPWorkflowTaskProperties taskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        //protected bool isCompleted = false;

        //private void createTaskWithContentType1_MethodInvoking(object sender, EventArgs e)
        //{
        //    //if (PreviousTaskId <= 0)
        //    //{
        //    //    TaskId = Guid.NewGuid();

        //    //    taskProperties.AssignedTo = Approver;
        //    //    string workflowPropertiesInitiationData = WorkflowProperties.InitiationData;
        //    //    ApprovalWFInitiationData initData = Common.Helpers.SerializationHelper.DeserializeFromXml<ApprovalWFInitiationData>(workflowPropertiesInitiationData);
        //    //    taskProperties.ExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION] = initData.;
        //    //    //taskProperties.ExtendedProperties[TaskExtendProperties.CCI_PREVIOUS_APPROVER] = WorkflowProperties.Item["Author"];

        //    //    DateTime dueDate = DateTime.Now.AddDays(initData.DurationPerTask);
        //    //    if (dueDate >= initData.DueDate) dueDate = initData.DueDate;
        //    //    taskProperties.DueDate = dueDate;


        //    //}
        //    //else
        //    //{
        //    //    TaskId = Guid.NewGuid();
        //    //    SPListItem previousTaskItem = WorkflowProperties.TaskList.GetItemById(PreviousTaskId);
        //    //    var progs = SPWorkflowTask.GetExtendedPropertiesAsHashtable(previousTaskItem);

        //    //    taskProperties.AssignedTo = progs[TaskExtendProperties.CCI_ASSIGN_TO].ToString();

        //    //}

        //    //WorkflowProperties.Web.EnsureUser(taskProperties.AssignedTo);
        //    //string strEmailTitle = this.EmailTitle;

        //    //if (WorkflowProperties.Item["Title"] != null)
        //    //{
        //    //    strEmailTitle += (" " + WorkflowProperties.Item["Title"]);
        //    //}

        //    //taskProperties.Title = strEmailTitle;
        //    //taskProperties.EmailBody = this.EmailBody;

        //    //var ct = this.WorkflowProperties.Web.AvailableContentTypes.Cast<SPContentType>()
        //    //    .Where(p => p.Id.IsChildOf(new SPContentTypeId(this.TaskContentTypeId)))
        //    //    .FirstOrDefault();

        //    //createTaskWithContentType1_ContentTypeId1 = ct.Id.ToString();
        //    //WorkflowProperties.TaskList.EnsureContentTypeInList(TaskContentTypeId);
        //    ////workflowProperties.Item[SPBuiltInFieldId.ContentType] = ct.Id;
        //    //// workflowProperties.Item.SystemUpdate();
        //}

        //public static DependencyProperty createTaskWithContentType1_ContentTypeId1Property = DependencyProperty.Register("createTaskWithContentType1_ContentTypeId1", typeof(System.String), typeof(ApprovalUnit));
        //public String createTaskWithContentType1_ContentTypeId1 = default(System.String);

        //private void IsTaskNotCompleted(object sender, ConditionalEventArgs e)
        //{
        //    e.Result = !isCompleted;
        //}

        //public SPWorkflowTaskProperties onTaskChanged1_AfterProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        //public SPWorkflowTaskProperties onTaskChanged1_BeforeProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();


        ////public static DependencyProperty TaskItemIdProperty = DependencyProperty.Register("TaskItemId", typeof(System.Int32), typeof(ApprovalUnit));

        ////[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        ////[BrowsableAttribute(true)]
        ////[CategoryAttribute("Misc")]
        ////public Int32 TaskItemId
        ////{
        ////    get
        ////    {
        ////        return ((int)(base.GetValue(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskItemIdProperty)));
        ////    }
        ////    set
        ////    {
        ////        base.SetValue(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskItemIdProperty, value);
        ////    }
        ////}

        //private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        //{
        //    TaskOutcome = onTaskChanged1_AfterProperties1.ExtendedProperties[TaskExtendProperties.OWS_TASK_STATUS] as string;
        //    isCompleted = TaskOutcome == TaskApprovalStatus.Approved ||
        //                   TaskOutcome == TaskApprovalStatus.Reasigned ||
        //                    TaskOutcome == TaskApprovalStatus.Rejected;


        //    logToHistoryListActivity1_HistoryDescription1 = "Log là đây: " + TaskOutcome;
        //}





        //public string TaskOutcome
        //{
        //    get { return (string)GetValue(TaskOutcomeProperty); }
        //    set { SetValue(TaskOutcomeProperty, value); }
        //}

        ////// Using a DependencyProperty as the backing store for TaskOutcome.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TaskOutcomeProperty =
        //    DependencyProperty.Register("TaskOutcome", typeof(string), typeof(ApprovalUnit));
        ////public Guid createTaskWithContentType1_TaskId = default(System.Guid);
        //public String logToHistoryListActivity1_HistoryDescription1 = default(System.String);


        //private void updateTask1_MethodInvoking(object sender, EventArgs e)
        //{

        //    if (TaskOutcome == TaskApprovalStatus.Approved)
        //    {
        //        Hashtable props = new Hashtable();
        //        props.Add("Status", "Completed");

        //        updateTask1_TaskProperties1.ExtendedProperties["Status"] = "Completed";

        //    }
        //    else if (TaskOutcome == TaskApprovalStatus.Rejected)
        //    {
        //        Hashtable props = new Hashtable();
        //        props.Add("Status", "Rejected");

        //        updateTask1_TaskProperties1.ExtendedProperties["Status"] = "Rejected";
        //        //var taskItem = this.WorkflowProperties.TaskList.GetItemById(this.TaskItemId);
        //        //if (taskItem != null)
        //        //{
        //        //    taskItem[SPBuiltInFieldId.TaskStatus] = "Rejected";
        //        //    taskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
        //        //    taskItem.SystemUpdate();
        //        //}
        //        isCompleted = true;

        //    }
        //    else if (TaskOutcome == TaskApprovalStatus.Reasigned)
        //    {
        //        Hashtable props = new Hashtable();
        //        props.Add("Status", "Reassign");

        //        updateTask1_TaskProperties1.ExtendedProperties["Status"] = "Reassign";
        //        //var taskItem = this.WorkflowProperties.TaskList.GetItemById(this.TaskItemId);
        //        //if (taskItem != null)
        //        //{
        //        //    taskItem[SPBuiltInFieldId.TaskStatus] = "Reassign";
        //        //    taskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
        //        //    taskItem.SystemUpdate();
        //        //}
        //        isCompleted = true;
        //    }

        //}

        //public Int32 CreatedTaskId = default(System.Int32);

        //private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        //{
        //    //PreviousTaskId = CreatedTaskId;
        //}

        #endregion

        #region "Dependency Properties"

        public TaskFormOption FormOption
        {
            get { return (TaskFormOption)GetValue(FormOptionProperty); }
            set { SetValue(FormOptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormOption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormOptionProperty =
            DependencyProperty.Register("FormOption", typeof(TaskFormOption), typeof(ApprovalUnit));



        public bool AppendTitle
        {
            get { return (bool)GetValue(AppendTitleProperty); }
            set { SetValue(AppendTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppendTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppendTitleProperty =
            DependencyProperty.Register("AppendTitle", typeof(bool), typeof(ApprovalUnit));



        public bool EnableEmail
        {
            get { return (bool)GetValue(EnableEmailProperty); }
            set { SetValue(EnableEmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableEmail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableEmailProperty =
            DependencyProperty.Register("EnableEmail", typeof(bool), typeof(ApprovalUnit));



        public string EmailTitle
        {
            get { return (string)GetValue(EmailTitleProperty); }
            set { SetValue(EmailTitleProperty, value); }
        }


        public string TaskDescription
        {
            get { return (string)GetValue(TaskDescriptionProperty); }
            set { SetValue(TaskDescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskDescription.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskDescriptionProperty =
            DependencyProperty.Register("TaskDescription", typeof(string), typeof(ApprovalUnit));


        // Using a DependencyProperty as the backing store for EmailTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmailTitleProperty =
            DependencyProperty.Register("EmailTitle", typeof(string), typeof(ApprovalUnit));



        public string EmailBody
        {
            get { return (string)GetValue(EmailBodyProperty); }
            set { SetValue(EmailBodyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmailBody.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmailBodyProperty =
            DependencyProperty.Register("EmailBody", typeof(string), typeof(ApprovalUnit));




        public string Approver
        {
            get { return (string)GetValue(ApproverProperty); }
            set { SetValue(ApproverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Approver.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApproverProperty =
            DependencyProperty.Register("Approver", typeof(string), typeof(ApprovalUnit));

        public DateTime DueDate
        {
            get { return (DateTime)GetValue(DueDateProperty); }
            set { SetValue(DueDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DueDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DueDateProperty =
            DependencyProperty.Register("DueDate", typeof(DateTime), typeof(ApprovalUnit));

        public int DurationPerTask
        {
            get { return (int)GetValue(DurationPerTaskProperty); }
            set { SetValue(DurationPerTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DurationPerTask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationPerTaskProperty =
            DependencyProperty.Register("DurationPerTask", typeof(int), typeof(ApprovalUnit));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ApprovalUnit));

        public string TaskTitle
        {
            get { return (string)GetValue(TaskTitleProperty); }
            set { SetValue(TaskTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskTitleProperty =
            DependencyProperty.Register("TaskTitle", typeof(string), typeof(ApprovalUnit));

        public SPWorkflowActivationProperties WorkflowProperties
        {
            get { return (SPWorkflowActivationProperties)GetValue(WorkflowPropertiesProperty); }
            set { SetValue(WorkflowPropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WorkflowProperties.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty WorkflowPropertiesProperty =
            DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties), typeof(ApprovalUnit));



        public string TaskOutcome
        {
            get { return (string)GetValue(TaskOutcomeProperty); }
            set { SetValue(TaskOutcomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskOutcome.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskOutcomeProperty =
            DependencyProperty.Register("TaskOutcome", typeof(string), typeof(ApprovalUnit));

        public int PreviousTaskId
        {
            get { return (int)GetValue(PreviousTaskIdProperty); }
            set { SetValue(PreviousTaskIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviousTaskId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousTaskIdProperty =
            DependencyProperty.Register("PreviousTaskId", typeof(int), typeof(ApprovalUnit));

        #endregion



        #region "CreateTaskWithContentType Activities "

        public static DependencyProperty TaskContentTypeIdProperty = DependencyProperty.Register("TaskContentTypeId", typeof(System.String), typeof(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String TaskContentTypeId
        {
            get
            {
                return ((string)(base.GetValue(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskContentTypeIdProperty)));
            }
            set
            {
                base.SetValue(TVMCORP.TVS.WORKFLOWS.Core.Activities.ApprovalUnit.TaskContentTypeIdProperty, value);
            }
        }

        public Int32 CreatedTaskId = default(System.Int32);

        public static readonly DependencyProperty TaskIdProperty =
            DependencyProperty.Register("TaskId", typeof(Guid), typeof(ApprovalUnit));

        public Guid TaskId
        {
            get { return (Guid)GetValue(TaskIdProperty); }
            set { SetValue(TaskIdProperty, value); }
        }

        public SPWorkflowTaskProperties taskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();


        private void createTaskWithContentType1_MethodInvoking(object sender, EventArgs e)
        {
            taskProperties.SendEmailNotification = false;
            taskProperties.HasCustomEmailBody = true;
            taskProperties.EmailBody = EmailBody;
            taskProperties.Title = TaskTitle;

            DateTime dueDateTask = DateTime.Now.AddDays(DurationPerTask);
            string wfAssociationData = WorkflowProperties.AssociationData;
            ApprovalWFAssociationData assData = SerializationHelper.DeserializeFromXml<ApprovalWFAssociationData>(wfAssociationData);
            if (dueDateTask >= DueDate) dueDateTask = DueDate;
            taskProperties.DueDate = dueDateTask;
            taskProperties.Title = GetTaskTitle();

            var hashProps = FormOption.ToHashtable();
            taskProperties.ExtendedProperties[TaskExtendProperties.UPDATED_PROPERTIES] = SerializationHelper.SerializeToXml<List<string>>(this.UpdatedProperties);

            if (PreviousTaskId <= 0)
            {
                TaskId = Guid.NewGuid();
                taskProperties.AssignedTo = Approver.Replace("i:0#.w|", string.Empty);
                taskProperties.ExtendedProperties[TaskExtendProperties.STB_MESS_TO_APPROVER] = Message;
                taskProperties.Description = TaskDescription;
            }
            else
            {
                TaskId = Guid.NewGuid();
                SPListItem previousTaskItem = WorkflowProperties.TaskList.GetItemById(PreviousTaskId);
                var progs = SPWorkflowTask.GetExtendedPropertiesAsHashtable(previousTaskItem);

                string assignto = progs[TaskExtendProperties.STB_ASSIGN_TO].ToString();
                assignto = assignto.Trim(new char[] { ' ', ';' });
                string message = progs[TaskExtendProperties.STB_TASK_COMMENTS].ToString();
                taskProperties.AssignedTo = assignto;
                taskProperties.Description = message;
                taskProperties.ExtendedProperties[TaskExtendProperties.STB_MESS_TO_APPROVER] = message;
            }
            taskProperties.ExtendedProperties.UpdateWith(hashProps);

            string assignToName = string.Empty;
            if (!string.IsNullOrEmpty(taskProperties.AssignedTo) && taskProperties.AssignedTo.Contains("|"))
                assignToName = taskProperties.AssignedTo.Split('|')[1];
            else
                assignToName = taskProperties.AssignedTo;


            WorkflowProperties.TaskList.EnsureContentTypeInList(TaskContentTypeId);
            var principal = WorkflowProperties.Site.FindUserOrSiteGroup(assignToName);
            if (WorkflowProperties.Site.RootWeb.SiteGroups.Cast<SPGroup>().Any(p => p.Name == assignToName))
            {
                try
                {
                    List<SPUser> users = new List<SPUser>();
                    SPGroup spGroup = WorkflowProperties.Site.RootWeb.SiteGroups[principal.Name];
                    if (spGroup != null)
                    {
                        var listUsers = spGroup.Users.Cast<SPUser>().ToList();

                        //test
                        var authenticatedUsersObj = listUsers.Where(s => s.LoginName == "NT AUTHORITY\\authenticated users").ToList();
                        if (authenticatedUsersObj.Count > 0)
                        {
                            users = WorkflowProperties.Site.RootWeb.AllUsers.Cast<SPUser>().Where(p => p.Name != "Authenticated Users").ToList();
                        }
                        else
                        {
                            users.AddRange(listUsers);
                        }

                        var grouped = users.GroupBy(p => p.LoginName);

                        string emails = string.Empty;
                        foreach (var group in grouped)
                        {
                            var user = group.First();
                            emails += user.Email + ",";
                        }

                        ApproverEmail = emails.Trim(',');
                    }

                }
                catch { }
            }
            else
            {

                var assignedTo = WorkflowProperties.Web.EnsureUser(assignToName);
                if (assignedTo != null)
                {
                    ApproverEmail = assignedTo.Email;
                }
            }

        }


        public string ApproverEmail
        {
            get { return (string)GetValue(ApproverEmailProperty); }
            set { SetValue(ApproverEmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApproverEmail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApproverEmailProperty =
            DependencyProperty.Register("ApproverEmail", typeof(string), typeof(ApprovalUnit));



        private string GetTaskTitle()
        {
            string title = TaskTitle;
            try
            {
                if (AppendTitle)
                {
                    if (WorkflowProperties.Item[SPBuiltInFieldId.Title] != null)
                    {
                        title = TaskTitle + WorkflowProperties.Item[SPBuiltInFieldId.Title].ToString();
                    }
                    else
                    {
                        if (WorkflowProperties.Item[SPBuiltInFieldId.Name] != null)
                        {
                            title = TaskTitle + WorkflowProperties.Item[SPBuiltInFieldId.Name].ToString();
                        }
                        else
                        {
                            title = TaskTitle + "none title";
                        }
                    }

                }
            }
            catch (Exception)
            { }

            return title;
        }

        #endregion

        #region "OnTaskChanged Activities "

        public SPWorkflowTaskProperties onTaskChanged1_AfterProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChanged1_BeforeProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        private string taskOwner = string.Empty;
        private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        {

            TaskOutcome = onTaskChanged1_AfterProperties1.ExtendedProperties[TaskExtendProperties.OWS_TASK_STATUS] as string;
            isCompleted = TaskOutcome == TaskApprovalStatus.Approved ||
                           TaskOutcome == TaskApprovalStatus.Reasigned ||
                            TaskOutcome ==TaskApprovalStatus.Rejected;
            taskOwner = e.Identity;
        }
        #endregion


        #region "While Activity Code Condition"
        protected bool isCompleted = false;
        private void IsTaskNotCompleted(object sender, ConditionalEventArgs e)
        {
            e.Result = !isCompleted;
        }
        #endregion

        private bool isDocChanged = false;
        private void approvalTask_MethodInvoking(object sender, EventArgs e)
        {
            PreviousTaskId = CreatedTaskId;
            taskCompleted = true;
            //string outcomeMessage = "";
            switch (TaskOutcome)
            {
                case TaskApprovalStatus.Approved:
                    approvalTask_TaskOutcome = "Approved";
                    ApprovalTaskMessageLog_HistoryDescription = "Task was approved by " + taskOwner ;
                    break;

                case TaskApprovalStatus.Reasigned:
                    approvalTask_TaskOutcome = "Reasigned";
                    var assignTo = onTaskChanged1_AfterProperties1.ExtendedProperties[TaskExtendProperties.STB_ASSIGN_TO].ToString();

                    ApprovalTaskMessageLog_HistoryDescription = taskOwner + " has been reasigned this task to " + assignTo;
                    break;

                case TaskApprovalStatus.Rejected:
                    approvalTask_TaskOutcome = "Rejected";

                    ApprovalTaskMessageLog_HistoryDescription = "Task was rejected by " + taskOwner + " with comment \"" + onTaskChanged1_AfterProperties1.ExtendedProperties[TaskExtendProperties.STB_TASK_COMMENTS].ToString();
                    break;
            }


        }

        public String approvalTask_TaskOutcome = default(System.String);
        public String ApprovalTaskMessageLog_HistoryDescription = default(System.String);

        private void IsEndOnDocumentChanged(object sender, ConditionalEventArgs e)
        {
            e.Result = Association.EndOnItemDocumentChange;
        }

        private void DocumentChanged_Invoked(object sender, ExternalDataEventArgs e)
        {
            approvalTask_TaskOutcome = TaskApprovalStatus.Canceled;
        }

        private void CancelTask_MethodInvoking(object sender, EventArgs e)
        {
            CancelTask_TaskOutcome = "Canceled";
        }

        public String CancelTask_TaskOutcome = default(System.String);

        private bool taskCompleted = false;
        private void IsTaskCompletedOrDocumentChanged(object sender, ConditionalEventArgs e)
        {
            e.Result = isDocChanged || taskCompleted;
        }

        private void SendTaskEmail_ExecuteCode(object sender, EventArgs e)
        {

        }


        public TaskEventSettings TaskEvents
        {
            get { return (TaskEventSettings)GetValue(TaskEventsProperty); }
            set { SetValue(TaskEventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskEvents.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskEventsProperty =
            DependencyProperty.Register("TaskEvents", typeof(TaskEventSettings), typeof(ApprovalUnit));



        public SPWorkflowTaskProperties ApprovalTaskCreated_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void ApprovalTaskCreated_Invoked(object sender, ExternalDataEventArgs e)
        {
            var taskListItem = WorkflowProperties.TaskList.GetItemById(ApprovalTaskCreated_AfterProperties.TaskItemId);
            if (this.EnableEmail)
            {
                var sourceListItem = WorkflowProperties.Item;
                //var taskListItem = WorkflowProperties.TaskList.GetItemById(ApprovalTaskCreated_AfterProperties.TaskItemId);

                if (!string.IsNullOrEmpty(ApproverEmail) && ApproverEmail.IsValidEmailAddress())
                {
                    SendEmailHelper.SendEmailbytemplate(sourceListItem, taskListItem, ApproverEmail, EmailTitle, EmailBody, false, true);
                }
            }

            TaskCreatedEvent_Parameter = new TaskEventHandlerParameter()
            {
                EventSettings = this.TaskEvents,

                WorkflowProperties = WorkflowProperties,
                TaskId = CreatedTaskId
            };

            //Set permission for task item
        }

        private ApprovalWFAssociationData Association;


        public List<string> UpdatedProperties
        {
            get { return (List<string>)GetValue(UpdatedPropertiesProperty); }
            set { SetValue(UpdatedPropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdatedProperties.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdatedPropertiesProperty =
            DependencyProperty.Register("UpdatedProperties", typeof(List<string>), typeof(ApprovalUnit));



        private void InitData_ExecuteCode(object sender, EventArgs e)
        {
            Association = SerializationHelper.DeserializeFromXml<ApprovalWFAssociationData>(WorkflowProperties.AssociationData);
        }

        public TaskEventHandlerParameter TaskCreatedEvent_Parameter = new TVMCORP.TVS.UTIL.MODELS.TaskEventHandlerParameter();
        public TaskEventHandlerParameter TaskApproved_Parameter = new TVMCORP.TVS.UTIL.MODELS.TaskEventHandlerParameter();
        public TaskEventHandlerParameter TaskRejected_Parameter = new TVMCORP.TVS.UTIL.MODELS.TaskEventHandlerParameter();

        private void SetTaskEventParameter_ExecuteCode(object sender, EventArgs e)
        {
            TaskApproved_Parameter = new TaskEventHandlerParameter() { 
                EventSettings = this.TaskEvents,
                WorkflowProperties = this.WorkflowProperties,
                TaskId = CreatedTaskId
            };
            TaskRejected_Parameter = new TaskEventHandlerParameter() {
                EventSettings = this.TaskEvents,
                WorkflowProperties = this.WorkflowProperties,
                TaskId = CreatedTaskId
            };
        }


    }
}
