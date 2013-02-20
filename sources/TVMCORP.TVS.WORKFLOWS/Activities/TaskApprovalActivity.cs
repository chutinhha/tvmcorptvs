using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.Util.Helpers;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class TaskApprovalActivity : SequenceActivity
    {
        public TaskApprovalActivity()
        {
            InitializeComponent();
            disableLogToHistoryInDelayActivity();
        }

        #region Properties
        public static DependencyProperty ApprovalInfoTaskProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ApprovalInfoTask", typeof(TaskDetail),
            typeof(TaskApprovalActivity));
        [Description("Approval detail tasks")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TaskDetail ApprovalInfoTask
        {
            get
            {
                return ((TaskDetail)(base.GetValue(TaskApprovalActivity.ApprovalInfoTaskProperty)));
            }
            set
            {
                base.SetValue(TaskApprovalActivity.ApprovalInfoTaskProperty, value);
            }
        }

        public static DependencyProperty WorkflowPropertiesProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties),
            typeof(TaskApprovalActivity));
        [Description("Workflow properties ")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SPWorkflowActivationProperties WorkflowProperties
        {
            get
            {
                return ((SPWorkflowActivationProperties)(base.GetValue(TaskApprovalActivity.WorkflowPropertiesProperty)));
            }
            set
            {
                base.SetValue(TaskApprovalActivity.WorkflowPropertiesProperty, value);
            }
        }

        public static DependencyProperty ParameterProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("Parameter", typeof(Parameter),
            typeof(TaskApprovalActivity));
        [Description("Parameter Output")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Parameter Parameter
        {
            get
            {
                return ((Parameter)(base.GetValue(TaskApprovalActivity.ParameterProperty)));
            }
            set
            {
                base.SetValue(TaskApprovalActivity.ParameterProperty, value);
            }
        }
        #endregion

        #region Fields
        public SPWorkflowTaskProperties updateTaskPros = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public IList AssingeeEmailsList = new List<string>();
        public string CurrentUserName = string.Empty;
        public string TaskDescription = string.Empty;
        public Int32 TaskIdCreated = default(System.Int32);
        public string OutComeText = string.Empty;
        public string TaskListId = string.Empty;

        private bool _isApproved = false;
        private bool _isRejected = false;
        private bool _isTerminated = false;
        private bool _isReassigned = false;
        private bool _isRequested = false;
        private bool _isFinished = false;
        private bool _isSignatureVerified = false;
        private bool _isDataQualityCompleted = false;
        private bool _isOnHold = false;
        private bool _isSendEEC = false;
        //private string _emailBody = string.Empty;
        private string _actionByText = "{0} by {1}";
        private string _requestInformationText = "Requested Information of {0} by {1}";
        private string _reassignText = "Reassigned by {0} to {1}";
        private string _newAssignee = string.Empty;
        private string _beforeAssignee = string.Empty;
        private string _currentLoginName = string.Empty;
        private string _sendEECStatus = string.Empty;
        private TaskResultSettings taskResultSettings;
        #endregion

        #region Execute code
        private void createTaskWithContentType1_MethodInvoking(object sender, EventArgs e)
        {
            SPWorkflowTaskProperties taskProperties = ApprovalInfoTask.TaskProperties;            

            taskProperties.Title = ApprovalInfoTask.Title;
            taskProperties.Description = ApprovalInfoTask.Body;
            taskProperties.DueDate = ApprovalInfoTask.DueDate;

            taskProperties.AssignedTo = ApprovalInfoTask.InitialAssignTo;
            taskProperties.PercentComplete = ApprovalInfoTask.PercentComplete;

            taskProperties.ExtendedProperties[Constants.Workflow.CCI_TASK_TYPE] = ApprovalInfoTask.Type;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_TASK_INSTRUCTION] = ApprovalInfoTask.Body;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_PREVIOUS_TASK_ID] = ApprovalInfoTask.PreviousTaskId;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_ALLOW_REASSIGN] = Parameter.ApprovalConfiguation.AllowReassign;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_ALLOW_DUEDATE_CHANGE_ON_REASSIGNMENT] = Parameter.ApprovalConfiguation.AllowDueDateChangeOnReassignment;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_ALLOW_REQUEST_INFORMATION] = Parameter.ApprovalConfiguation.AlloRequestInfomation;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_ALLOW_DUEDATE_CHANGE_ON_REQUEST_INFORMATION] = Parameter.ApprovalConfiguation.AllowDueDateChangeOnRequestInformation;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_ALLOW_ON_HOLD] = Parameter.ApprovalConfiguation.AllowPlaceOnHold;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_ALLOW_SEND_EEC] = Parameter.ApprovalConfiguation.AllowSendEEC;

            taskProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_BUTTON_LABEL] = Constants.Workflow.EEC_BUTTON_LABEL_TEXT;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TASK_STATUS] = Constants.Workflow.STATUS_SEND_EEC_TEXT;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TASK_BACK_STATUS] = Constants.Workflow.STATUS_SEND_EEC_TEXT + " - Back";
            taskProperties.ExtendedProperties[TaskExtendProperties.CCI_APPROVED_TEXT] = taskResultSettings.ApprovedText;
            taskProperties.ExtendedProperties[TaskExtendProperties.CCI_REJECTED_TEXT] = taskResultSettings.RejectedText;
            taskProperties.ExtendedProperties[TaskExtendProperties.CCI_SIGNATURE_VERIFIED_TEXT] = taskResultSettings.SignatureVerifiedText;
            taskProperties.ExtendedProperties[TaskExtendProperties.CCI_DATA_QUALITY_COMPLETED_TEXT] = taskResultSettings.DataQualityCompletedText;

            EECSettings settings = WorkflowProperties.Item.GetCustomSettings<EECSettings>(IOfficeFeatures.EEC);

            if (settings == null || settings.Enabled == false)
                return;

            taskProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_BUTTON_LABEL] = string.IsNullOrEmpty(settings.EECTaskButtonLabel) ? Constants.Workflow.EEC_BUTTON_LABEL_TEXT : settings.EECTaskButtonLabel;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TASK_STATUS] = _sendEECStatus = string.IsNullOrEmpty(settings.EECTaskStatus) ? Constants.Workflow.STATUS_SEND_EEC_TEXT : settings.EECTaskStatus;
            taskProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TASK_BACK_STATUS] = string.IsNullOrEmpty(settings.EECTaskStatus) ? Constants.Workflow.STATUS_SEND_EEC_TEXT + " - Back" : settings.EECTaskStatus + " - Back";
        }

        private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        {
            _isApproved = completedWithStatus(taskResultSettings.ApprovedText, Parameter.ApprovalConfiguation.TaskRuleConfiguration.ApprovedCriteriaList);
            _isRejected = completedWithStatus(taskResultSettings.RejectedText, Parameter.ApprovalConfiguation.TaskRuleConfiguration.RejectedCriteriaList);
            _isTerminated = completedWithStatus(Constants.Workflow.STATUS_CANCEL_TEXT, Parameter.ApprovalConfiguation.TaskRuleConfiguration.TerminationCriteriaList);
            _isReassigned = completedWithStatus(Constants.Workflow.STATUS_REASSIGN_TEXT, Parameter.ApprovalConfiguation.TaskRuleConfiguration.ReassignCriteriaList);
            _isRequested = completedWithStatus(Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT, Parameter.ApprovalConfiguation.TaskRuleConfiguration.RequestInformationCriteriaList);
            _isFinished = completedWithStatus(Constants.Workflow.STATUS_FINISHED_TEXT, Parameter.ApprovalConfiguation.TaskRuleConfiguration.FinishedCriteriaList);
            _isSignatureVerified = completedWithStatus(taskResultSettings.SignatureVerifiedText, Parameter.ApprovalConfiguation.TaskRuleConfiguration.SignatureVerificationCriteriaList);
            _isDataQualityCompleted = completedWithStatus(taskResultSettings.DataQualityCompletedText, Parameter.ApprovalConfiguation.TaskRuleConfiguration.DataQualityCompletedCriteriaList);
            _isOnHold = completedWithStatus(Constants.Workflow.STATUS_ON_HOLD_TEXT, null);
            _isSendEEC = completedWithStatus(_sendEECStatus, null);

            SPPrincipal pCurrentUser = WorkflowProperties.Site.FindUserOrSiteGroup(e.Identity);
            CurrentUserName = pCurrentUser == null ? e.Identity : pCurrentUser.Name;
            _currentLoginName = e.Identity;

            _beforeAssignee = ApprovalInfoTask.TaskProperties.AssignedTo;
            ApprovalInfoTask.PercentComplete = ApprovalInfoTask.TaskAfterProperties.PercentComplete;
            ApprovalInfoTask.Status = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[SPBuiltInFieldId.TaskStatus];
            ApprovalInfoTask.TaskId = TaskIdCreated;
            if (_isApproved)
            {
                ApprovalInfoTask.StatusWorkflow = taskResultSettings.ApprovedText;
                OutComeText = string.Format(_actionByText, taskResultSettings.ApprovedText, CurrentUserName);
            }

            if (_isRejected)
            {
                ApprovalInfoTask.StatusWorkflow = taskResultSettings.RejectedText;
                OutComeText = string.Format(_actionByText, taskResultSettings.RejectedText, CurrentUserName);
            }

            if (_isTerminated)
            {
                ApprovalInfoTask.StatusWorkflow = Constants.Workflow.STATUS_CANCEL_TEXT;
                OutComeText = string.Format(_actionByText, Constants.Workflow.STATUS_CANCEL_TEXT, CurrentUserName);
            }

            if (_isReassigned)
            {
                ApprovalInfoTask.StatusWorkflow = Constants.Workflow.STATUS_REASSIGN_TEXT;
                if (ApprovalInfoTask.InfoPathForm)
                    _newAssignee = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_ASSIGN_TO];
                else
                    _newAssignee = ApprovalInfoTask.TaskAfterProperties.AssignedTo;
                SPPrincipal pNewAssignee = WorkflowProperties.Site.FindUserOrSiteGroup(_newAssignee);
                _newAssignee = pNewAssignee == null ? e.Identity : pNewAssignee.Name;
                OutComeText = string.Format(_reassignText, CurrentUserName, _newAssignee);
            }

            if (_isRequested)
            {
                ApprovalInfoTask.StatusWorkflow = Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT;
                if (ApprovalInfoTask.InfoPathForm)
                    _newAssignee = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_REQUEST_TO];
                else
                    _newAssignee = ApprovalInfoTask.TaskAfterProperties.AssignedTo;
                SPPrincipal pNewAssignee = WorkflowProperties.Site.FindUserOrSiteGroup(_newAssignee);
                _newAssignee = pNewAssignee == null ? e.Identity : pNewAssignee.Name;
                OutComeText = string.Format(_requestInformationText, _newAssignee, CurrentUserName);
            }

            if (_isFinished)
            {
                ApprovalInfoTask.StatusWorkflow = Constants.Workflow.STATUS_FINISHED_TEXT;
                OutComeText = string.Format(_actionByText, Constants.Workflow.STATUS_FINISHED_TEXT, CurrentUserName);
            }

            if (_isSignatureVerified)
            {
                ApprovalInfoTask.StatusWorkflow = taskResultSettings.SignatureVerifiedText;
                OutComeText = string.Format(_actionByText, taskResultSettings.SignatureVerifiedText, CurrentUserName);
            }

            if (_isDataQualityCompleted)
            {
                ApprovalInfoTask.StatusWorkflow = taskResultSettings.DataQualityCompletedText;
                OutComeText = string.Format(_actionByText, taskResultSettings.DataQualityCompletedText, CurrentUserName);
            }

            Parameter.TaskItem = TaskIdCreated;
            string comment = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_COMMENT];
            Parameter.Comment = string.IsNullOrEmpty(comment) ? null : comment;
            if (!string.IsNullOrEmpty(comment))
            {
                WorkflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None,
                     pCurrentUser ?? WorkflowProperties.Web.CurrentUser, "Comments: " + comment, string.Empty);
            }

            Parameter.Submitter = e.Identity;
        }

        private void IsNotComplete(object sender, ConditionalEventArgs e)
        {
            //e.Result = this.ApprovalInfoTask.PercentComplete != 1f;
            e.Result = (!_isApproved
                && !_isRejected
                && !_isTerminated
                && !_isReassigned
                && !_isRequested
                && !_isFinished
                && !_isSignatureVerified
                && !_isDataQualityCompleted);
        }

        private void IsReminderDateExist(object sender, ConditionalEventArgs e)
        {
            e.Result = Parameter.ApprovalConfiguation.ReminderDuration != 0;
        }

        private void IsEscalationDateExist(object sender, ConditionalEventArgs e)
        {
            e.Result = Parameter.ApprovalConfiguation.EscalationDuration != 0;
        }

        private void IsStopTaskGroup(object sender, ConditionalEventArgs e)
        {
            //e.Result = this.ApprovalInfoTask.PercentComplete == 1f;
            e.Result = (_isApproved
                || _isRejected
                || _isTerminated
                || _isReassigned
                || _isRequested
                || _isFinished
                || _isSignatureVerified
                || _isDataQualityCompleted);
        }

        private void sendAssignEmailReplicatorChild_Init(object sender, ReplicatorChildEventArgs e)
        {
            (e.Activity as SendWFTaskEmail).To = (string)e.InstanceData;
        }

        private void sendReminderEmailReplicatorChild_Init(object sender, ReplicatorChildEventArgs e)
        {
            (e.Activity as SendWFTaskEmail).To = (string)e.InstanceData;
        }

        private void initialData_ExecuteCode(object sender, EventArgs e)
        {
            taskResultSettings = new TaskResultSettings(WorkflowProperties.Site.ID);
            TaskListId = WorkflowProperties.TaskList.ID.ToString();

            string assignee = ApprovalInfoTask.InitialAssignTo;
            ApprovalInfoTask = applyProxy(ApprovalInfoTask);
            
            logInfoProxy(assignee);

            if (SPUtility.IsLoginValid(WorkflowProperties.Site, ApprovalInfoTask.InitialAssignTo) && !string.IsNullOrEmpty(ApprovalInfoTask.AssignToEmail))
                AssingeeEmailsList.Add(ApprovalInfoTask.AssignToEmail);
            else
            {   //might be a group
                SPGroup groupApprover = null;
                foreach (SPGroup g in WorkflowProperties.Site.RootWeb.SiteGroups)
                {
                    if (string.Compare(g.Name, ApprovalInfoTask.InitialAssignTo, true) == 0)
                    {
                        groupApprover = g;
                        break;
                    }
                }

                if (groupApprover != null)
                {
                    foreach (SPUser user in groupApprover.Users)
                    {
                        AssingeeEmailsList.Add(user.Email);
                    }
                }
            }

            //_emailBody = SendEmailHelper.GetEmailBodyFromTemplate(Parameter.Context,
            //    Parameter.ListId,
            //    Parameter.ListItem,
            //    Parameter.ApprovalConfiguation.URLEmailTemplate,
            //    Parameter.ApprovalConfiguation.AssignmentEmailTemplate);
        }

        private void logInfoProxy(string assignee)
        {
            if (string.Compare(assignee, ApprovalInfoTask.InitialAssignTo, true) != 0)
            {
                string assigneeName = getAssigneeName(assignee); ;

                string delegateUserName = getAssigneeName(ApprovalInfoTask.InitialAssignTo);

                WorkflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, WorkflowProperties.Web.CurrentUser, "The task was redirected via proxy. The owner, " + assigneeName + ", automatically delegated the task to " + delegateUserName, string.Empty);
            }
        }

        private string getAssigneeName(string assignee)
        {
            if (SPUtility.IsLoginValid(WorkflowProperties.Site, assignee))
            {
                SPUser user = WorkflowProperties.Site.RootWeb.EnsureUser(assignee);
                assignee = user.Name;
                if (assignee.Contains('\\'))
                {
                    assignee = assignee.Remove(0, assignee.Substring(0, assignee.IndexOf('\\') + 1).Length);
                }
            }
            return assignee;
        }

        private void logToHistory_ExecuteCode(object sender, EventArgs e)
        {
            this.TaskDescription = "The task is completed by: " + CurrentUserName + ", with status: " + ApprovalInfoTask.Status;
        }

        private void setSecurityForTask_ExecuteCode(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPListItem item = WorkflowProperties.TaskList.Items.GetItemById(TaskIdCreated);

                if (!item.HasUniqueRoleAssignments)
                    item.BreakRoleInheritance(true);

                if (Parameter.ApprovalConfiguation.TaskContributors != null
                    && Parameter.ApprovalConfiguation.TaskContributors.Count > 0)
                {
                    SPRoleDefinition roleContribute = item.Web.RoleDefinitions.GetByType(SPRoleType.Contributor);
                    foreach (string strUser in Parameter.ApprovalConfiguation.TaskContributors)
                        item.SetPermissions(roleContribute.Name, strUser);
                }

                if (Parameter.ApprovalConfiguation.TaskObservers != null
                    && Parameter.ApprovalConfiguation.TaskObservers.Count > 0)
                {
                    SPRoleDefinition roleContribute = item.Web.RoleDefinitions.GetByType(SPRoleType.Reader);
                    foreach (string strUser in Parameter.ApprovalConfiguation.TaskObservers)
                        item.SetPermissions(roleContribute.Name, strUser);
                }
            }
            );
        }

        private void disableLogToHistoryInDelayActivity()
        {
            Activity logToHistoryListActivity1Delay2 = delayForActivity2.GetActivityByName("logToHistoryListActivity1", true);
            if (logToHistoryListActivity1Delay2 != null) logToHistoryListActivity1Delay2.Enabled = false;
            Activity logToHistoryListActivity2Delay2 = delayForActivity2.GetActivityByName("logToHistoryListActivity2", true);
            if (logToHistoryListActivity2Delay2 != null) logToHistoryListActivity2Delay2.Enabled = false;

            Activity logToHistoryListActivity1Delay1 = delayForActivity1.GetActivityByName("logToHistoryListActivity1", true);
            if (logToHistoryListActivity1Delay1 != null) logToHistoryListActivity1Delay1.Enabled = false;
            Activity logToHistoryListActivity2Delay1 = delayForActivity1.GetActivityByName("logToHistoryListActivity2", true);
            if (logToHistoryListActivity2Delay1 != null) logToHistoryListActivity2Delay1.Enabled = false;
        }

        private bool completedWithStatus(string status, List<Criteria> criteriaList)
        {
            if (ApprovalInfoTask.InfoPathForm)
            {
                if ((string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_TASK_STATUS] == status)
                    return true;
            }
            else
            {
                if (criteriaList == null || criteriaList.Count == 0) return false;
                bool match = false;
                switch (status)
                {
                    case Constants.Workflow.STATUS_REASSIGN_TEXT:
                    case Constants.Workflow.STATUS_REQUEST_INFORMATION_TEXT:
                    case Constants.Workflow.STATUS_CANCEL_TEXT:
                        match = ApprovalInfoTask.Type == 0 && matchRule(criteriaList);
                        break;
                    case Constants.Workflow.STATUS_FINISHED_TEXT:
                        match = ApprovalInfoTask.Type == 1 && matchRule(criteriaList);
                        break;

                    default:
                        if (string.Compare(status, taskResultSettings.DataQualityCompletedText, true) == 0 ||
                            string.Compare(status, taskResultSettings.SignatureVerifiedText, true) == 0 ||
                            string.Compare(status, taskResultSettings.RejectedText, true) == 0 ||
                            string.Compare(status, taskResultSettings.ApprovedText, true) == 0)
                        {
                            match = ApprovalInfoTask.Type == 0 && matchRule(criteriaList);
                        }
                        break;
                }
                return match;
            }
            return false;
        }

        private bool matchRule(List<Criteria> criteriaList)
        {
            if (criteriaList.Count == 0)
                return false;

            SPListItem item = WorkflowProperties.TaskList.Items.GetItemById(TaskIdCreated);
            foreach (Criteria c in criteriaList)
            {
                Guid fieldId = new Guid(c.FieldId);
                if (!ApprovalInfoTask.TaskAfterProperties.ExtendedProperties.ContainsKey(fieldId)) return false;
                if (FieldValueHelper.ValidateItemProperty(item, fieldId, c, (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[fieldId]))
                    continue;
                return false;
            }
            return true;
        }

        private void updateTask_ExecuteCode(object sender, EventArgs e)
        {
            if (ApprovalInfoTask.InfoPathForm)
                return;

            if (_isReassigned)
                updateTaskPros.ExtendedProperties[Constants.Workflow.CCI_ASSIGN_TO] = _newAssignee;

            if (_isRequested)
                updateTaskPros.ExtendedProperties[Constants.Workflow.CCI_REQUEST_TO] = _newAssignee;

            updateTaskPros.AssignedTo = _beforeAssignee;
            updateTaskPros.ExtendedProperties[Constants.Workflow.CCI_TASK_STATUS] = ApprovalInfoTask.StatusWorkflow;
        }
        #endregion

        private void setTaskHandlerParameter_ExecuteCode(object sender, EventArgs e)
        {
            TaskHandlerParameter.WorkflowProperties = WorkflowProperties;
            TaskHandlerParameter.TaskId = TaskIdCreated;
            TaskHandlerParameter.EventSettings = Parameter.ApprovalConfiguation.TaskEventConfiguration;
        }

        public TaskEventHandlerParameter TaskHandlerParameter = new TaskEventHandlerParameter();

        private void isApproved_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isApproved;
        }

        private void isRejected_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isRejected;
        }

        private void isRequested_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isRequested;
        }

        private void isSent_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isFinished;
        }

        private void isReassigned_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isReassigned;
        }

        private void isTerminated_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isTerminated;
        }

        private void isOnHold_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isOnHold;
        }

        private void updateOnHoldTask_ExecuteCode(object sender, EventArgs e)
        {
            UpdateWorkflowTaskMetadataSettings updateTaskSettings = new UpdateWorkflowTaskMetadataSettings();
            updateTaskSettings.FieldId = SPBuiltInFieldId.TaskStatus.ToString();
            updateTaskSettings.TaskId = TaskIdCreated;
            updateTaskSettings.Type = TaskActionTypes.UpdateWorkflowTaskMetadata;
            updateTaskSettings.Value = Constants.Workflow.STATUS_ON_HOLD_TEXT;

            ITaskActionHandler action = TaskActionFactory.CreateTaskActionHandler(TaskActionTypes.UpdateWorkflowTaskMetadata);
            TaskActionArgs taskArg = new TaskActionArgs(updateTaskSettings, this.WorkflowProperties);
            action.Execute(taskArg);
        }

        private void isNotOnHold_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = !this._isOnHold;
        }

        private void isSendEEC_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = this._isSendEEC;
        }

        private bool ValidateDocumentForEEC(SPListItem item, bool showErrorMessage, bool updateItem)
        {
            string cciUniqueIdField = Constants.EEC.CCIUniqueIdFieldName;

            bool result = false;
            if (item.Fields.ContainsField(cciUniqueIdField))
            {
                if (updateItem)
                {

                    SPUser currentUser = WorkflowProperties.Site.GetUser(_currentLoginName);
                    string currentEmail = currentUser != null ? currentUser.Email : string.Empty;
                    string address = currentEmail;

                    bool success = false;
                    int retry = 0;
                    while (retry < 5 && !success)
                    {
                        retry++;
                        try
                        {
                            SPSecurity.RunWithElevatedPrivileges(delegate()
                            {
                                using (SPSite site = new SPSite(item.ParentList.ParentWeb.Site.ID))
                                {
                                    using (SPWeb web = site.OpenWeb(item.ParentList.ParentWeb.ID))
                                    {
                                        web.AllowUnsafeUpdates = true;
                                        SPList list = web.GetList(web.Url + "/" + item.ParentList.RootFolder.Url);
                                        SPListItem newitem = list.GetItemById(item.ID);
                                        EECDS itemSettings = new EECDS();
                                        itemSettings.SE = address;
                                        //itemSettings.WorkflowInstanceId = WorkflowProperties.WorkflowId;
                                        //itemSettings.TaskId = TaskIdCreated;

                                        EECB itemSettingsWF = new EECB();
                                        itemSettingsWF.WIId = WorkflowProperties.WorkflowId;
                                        itemSettingsWF.TId = TaskIdCreated;

                                        if (newitem.File.CheckOutType != SPFile.SPCheckOutType.None)
                                        {
                                            try
                                            {
                                                newitem.File.UndoCheckOut();
                                            }
                                            catch { }
                                        }
                                        
                                        newitem[cciUniqueIdField] = DateTime.Now.Ticks.ToString();
                                        newitem.SystemUpdate();
                                        newitem.SetCustomSettings<EECDS>(IOfficeFeatures.EEC, itemSettings);
                                        newitem.SetCustomSettings<EECB>(IOfficeFeatures.EEC, itemSettingsWF);
                                        success = true;
                                        web.AllowUnsafeUpdates = false;
                                    }
                                }
                            });
                        }
                        catch
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }
                result = true;
            }
            else
            {
                return false;
            }
            return result;
        }

        private MailAddress GetFromEmailAddress()
        {
            string from = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_FROM];
            EECSettings settings = WorkflowProperties.Item.GetCustomSettings<EECSettings>(IOfficeFeatures.EEC);
            MailAddress fromAddress;
            SPUser currentUser = WorkflowProperties.Site.GetUser(_currentLoginName);
            string currentEmail = currentUser != null ? currentUser.Email : string.Empty;
            string strFromEmail;
            if (settings != null)
                if (settings.OptionSendAddress == EmailOption.PeopleSendingEEC)
                    strFromEmail = currentEmail;
                else
                    strFromEmail = settings.SendAddress;
            else
                strFromEmail = currentEmail;

            if (!string.IsNullOrEmpty(strFromEmail))
                fromAddress = new MailAddress(strFromEmail, from);
            else
                fromAddress = new MailAddress("sharepoint@" + Environment.UserDomainName.ToLower(), from);

            return fromAddress;
        }

        private List<SPFile> GetDocumentSetFilesBySetting(EECSettings setting)
        {
            List<SPFile> files = new List<SPFile>();

            if (WorkflowProperties.Item.ContentTypeId.IsChildOf(SPBuiltInContentTypeId.DocumentSet))
            {
                List<SPFile> documentSetFiles = WorkflowProperties.Item.Folder.Files.Cast<SPFile>().ToList();

                if (setting != null && !string.IsNullOrEmpty(setting.DocSetIncludedFieldName))
                {
                    foreach (var file in documentSetFiles)
                    {
                        SPListItem item = file.Item;
                        if (item.Fields.ContainsField(setting.DocSetIncludedFieldName)
                            && item[setting.DocSetIncludedFieldName] != null
                            && item[setting.DocSetIncludedFieldName].ToString() == setting.DocSetIncludedFieldValue
                            && ValidateDocumentForEEC(item, false, false))
                        {
                            files.Add(file);
                        }
                    }

                }
                else
                    files.AddRange(documentSetFiles);
            }
            else
            {
                files.Add(WorkflowProperties.Item.File);
            }

            return files;
        }

        private void SendMail(List<SPFile> files)
        {
            string ipSubject = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_SUBJECT];
            string ipBody = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_BODY];
            string ipTo = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TO];
            string ipCc = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_CC];
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(files[0].Item.ParentList.ParentWeb.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(files[0].Item.ParentList.ParentWeb.ID))
                    {
                        SPList list = web.GetList(web.Url + "/" + files[0].Item.ParentList.RootFolder.Url);
                        MailAddress fromAddress = GetFromEmailAddress();
                        string replyAddress = GetIncomingEmailAddress();

                        List<Attachment> attachments = new List<Attachment>();
                        foreach (SPFile file in files)
                        {
                            SPFile objFile = list.GetItemById(file.Item.ID).File;
                            Attachment attachment = new Attachment(objFile.OpenBinaryStream(), objFile.Name, "application/octet-stream");
                            attachments.Add(attachment);

                        }
                        SPUser currentUser = WorkflowProperties.Site.GetUser(_currentLoginName);
                        string subject = SendEmailHelper.ParseTemplateContent(ipSubject, WorkflowProperties.Item, currentUser);
                        string body = SendEmailHelper.ParseTemplateContent(ipBody, WorkflowProperties.Item, currentUser);

                        //SendEmailHelper.SendEmailByTemplateWithAttachments(WorkflowProperties.Item, fromAddress, ipTo, replyAddress, ipCc, subject, body, attachments, currentUser);
                        EECSettings settings = WorkflowProperties.Item.GetCustomSettings<EECSettings>(IOfficeFeatures.EEC);


                        SPList emailTemplateList = WorkflowProperties.GetListFromURL(settings.EmailTemplateListUrl);
                        if (emailTemplateList == null) return;

                        SPListItemCollection emailListItems = emailTemplateList.FindItems("Title", settings.EmailTemplateName);
                        if (emailListItems.Count == 0) return;

                        SPListItem emailListItem = emailListItems[0];
                        if (emailListItem == null) return;
                        bool isSendPlanText = bool.Parse(emailListItem[new Guid("1BCADBD1-CC15-40A0-AAF7-6DE222412337")] != null ? emailListItem[new Guid("1BCADBD1-CC15-40A0-AAF7-6DE222412337")].ToString() : "false");

                        SendEmailHelper.SendEmailByTemplateWithAttachments(WorkflowProperties.Item, fromAddress, ipTo, replyAddress, ipCc, subject, body, isSendPlanText, attachments, currentUser);
                    }
                }
            });
        }

        private string GetIncomingEmailAddress()
        {
            string reply = string.Empty;
            try
            {
                EECSettings settings = WorkflowProperties.Item.GetCustomSettings<EECSettings>(IOfficeFeatures.EEC);
                if (settings != null)
                {
                    SPUser currentUser = WorkflowProperties.Site.GetUser(_currentLoginName);
                    string currentEmail = currentUser != null ? currentUser.Email : string.Empty;

                    if (settings.OptionReceiveAddress == EmailOption.PeopleSendingEEC)
                        reply = currentEmail;
                    else
                        reply = settings.ReceiveAddress;
                }

            }
            catch (Exception ex)
            {
                //CCIUtility.Debug(ex);
            }

            return reply;
        }

        private void sendEEC_ExecuteCode(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDocumentForEEC(WorkflowProperties.Item, true, true)) return;

                EECSettings settings = WorkflowProperties.Item.GetCustomSettings<EECSettings>(IOfficeFeatures.EEC);

                if (settings == null || settings.Enabled == false)
                {
                    throw new Exception("Send External Collaboration is not enabled.");
                    return;
                }

                string strToEmail = (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TO];
                if (string.IsNullOrEmpty(strToEmail))
                {
                    return;
                }

                List<SPFile> files = GetDocumentSetFilesBySetting(settings);
                SendMail(files);

                updateEECStatus();

                if (settings.CheckoutOnSending)
                {
                    foreach (SPFile file in files)
                    {
                        if (file.CheckOutType == SPFile.SPCheckOutType.None)
                        {
                            try
                            {
                                //if (settings.EnableCheckoutOverride)
                                //{
                                    if (settings.EnableAllowEECSenderOverrideEECCheckout)
                                    {
                                        CheckOutAsUser(file, _currentLoginName);

                                        if (settings.EnableAllowUserOverideEECCheckout)
                                        {
                                            SetRoleForUserGroup(settings.EECCheckoutOverrideUser, file);
                                        }
                                    }
                                    else
                                        CheckOutAsSystem(file);
                                //}
                                //else
                                //    CheckOutAsSystem(file);
                            }
                            finally
                            {
                            }

                            file.Item.Audit.WriteAuditEvent(SPAuditEventType.CheckOut,
                                Constants.EEC.CCIappEECFeatureName, _currentLoginName + ": Checked out after send");
                        }
                    }
                }

                WorkflowProperties.Item.Audit.WriteAuditEvent(SPAuditEventType.Custom, Constants.EEC.CCIappEECFeatureName, _currentLoginName + " sent document for Collaboration.");
                WorkflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, _sendEECStatus + " | EEC Transmission sent to " + (string)ApprovalInfoTask.TaskAfterProperties.ExtendedProperties[Constants.Workflow.CCI_EEC_TO], string.Empty);
            }
            catch (Exception ex)
            {
            }
        }

        private void SetRoleForUserGroup(List<string> listLoginName, SPFile file)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(file.Item.Web.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(file.Item.Web.ID))
                    {
                        SPFolder folder = web.GetFolder(file.ParentFolder.UniqueId);
                        var item = folder.GetFile(file.Name).Item;
                        web.AllowUnsafeUpdates = true;
                        item.SetPermissions("EECOverrideCheckout", listLoginName);
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        private void CheckOutAsUser(SPFile file, string loginName)
        {
            SPUser currentUser = WorkflowProperties.Site.GetUser(_currentLoginName);
            using (SPSite site = new SPSite(file.Item.Web.Site.ID, currentUser.UserToken))
            {
                using (SPWeb web = site.OpenWeb(file.Item.Web.ID))
                {
                    web.AllowUnsafeUpdates = true;
                    SPFile file2 = web.GetFile(file.UniqueId);
                    file2.CheckOut();
                    web.AllowUnsafeUpdates = false;
                }
            }
        }

        private void CheckOutAsSystem(SPFile file)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(file.Item.Web.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(file.Item.Web.ID))
                    {
                        web.AllowUnsafeUpdates = true;
                        SPFile file2 = web.GetFile(file.UniqueId);
                        //file2.CheckOut(true, DateTime.Now.ToLongDateString());
                        file2.CheckOut();
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        private void updateEECStatus()
        {
            UpdateWorkflowTaskMetadataSettings updateTaskSettings = new UpdateWorkflowTaskMetadataSettings();
            updateTaskSettings.FieldId = SPBuiltInFieldId.TaskStatus.ToString();
            updateTaskSettings.TaskId = TaskIdCreated;
            updateTaskSettings.Type = TaskActionTypes.UpdateWorkflowTaskMetadata;
            updateTaskSettings.Value = _sendEECStatus;

            ITaskActionHandler action = TaskActionFactory.CreateTaskActionHandler(TaskActionTypes.UpdateWorkflowTaskMetadata);
            TaskActionArgs taskArg = new TaskActionArgs(updateTaskSettings, this.WorkflowProperties);
            action.Execute(taskArg);
        }

        private TaskDetail applyProxy(TaskDetail taskDetail)
        {
            SPPrincipal principal = WorkflowProperties.Site.FindUserOrSiteGroup(taskDetail.InitialAssignTo);
            if (principal == null) 
                return taskDetail;
            
            CCIProxy proxy = principal.GetProxyUser(WorkflowProperties.Site);
            if (proxy != null && !string.IsNullOrEmpty(proxy.DelegateUser.LoginName))
            {
                taskDetail.InitialAssignTo = proxy.DelegateUser.LoginName;
                taskDetail.AssignToEmail = proxy.DelegateUser.Email; 

                sendCCEmail(proxy);
            }

            return taskDetail;
        }

        private void sendCCEmail(CCIProxy proxy)
        {
            string ccEmails = string.Empty;
            foreach (CCIUser user in proxy.CCUser)
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    ccEmails += user.Email + ";";
                }
            }

            if (!string.IsNullOrEmpty(ccEmails) && !string.IsNullOrEmpty(proxy.CCSubjectEmail) &&
                !string.IsNullOrEmpty(proxy.CCBodyEmail))
            {
                ccEmails = ccEmails.Trim(';');
                SPUtility.SendEmail(WorkflowProperties.Web, false, true, ccEmails, proxy.CCSubjectEmail, proxy.CCBodyEmail);
            }
        }
    }
}
