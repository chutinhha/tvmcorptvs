using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.Util.Helpers;
using TVMCORP.TVS.Util.Utilities;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.MODELS.IPForm;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.Activities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
    public partial class ApprovalWorkflow : SequenceActivity
    {
        public ApprovalWorkflow()
        {
            InitializeComponent();
        }

        #region Dependency Properties
        public static DependencyProperty __ListIdProperty =
            DependencyProperty.Register("__ListId",
            typeof(string), typeof(ApprovalWorkflow));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string __ListId
        {
            get { return ((string)(base.GetValue(__ListIdProperty))); }
            set { base.SetValue(__ListIdProperty, value); }
        }


        public static DependencyProperty __ListItemProperty =
            DependencyProperty.Register("__ListItem",
            typeof(int), typeof(ApprovalWorkflow));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int __ListItem
        {
            get { return ((int)(base.GetValue(__ListItemProperty))); }
            set { base.SetValue(__ListItemProperty, value); }
        }

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(SPWorkflowActivationProperties),
            typeof(ApprovalWorkflow));

        [ValidationOption(ValidationOption.Required)]
        public SPWorkflowActivationProperties __ActivationProperties
        {
            get
            {
                return (SPWorkflowActivationProperties)base.GetValue(__ActivationPropertiesProperty);
            }
            set
            {
                base.SetValue(__ActivationPropertiesProperty, value);
            }
        }

        public static DependencyProperty ApprovalConfigListURLProperty =
            DependencyProperty.Register("ApprovalConfigListURL",
            typeof(string), typeof(ApprovalWorkflow));

        [Description("Approval Config List Url ")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ApprovalConfigListURL
        {
            get { return ((string)(base.GetValue(ApprovalConfigListURLProperty))); }
            set { base.SetValue(ApprovalConfigListURLProperty, value); }
        }

        public static DependencyProperty ApprovalNameProperty =
            DependencyProperty.Register("ApprovalName",
            typeof(string), typeof(ApprovalWorkflow));

        [Description("Approval EntryName")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ApprovalName
        {
            get { return ((string)(base.GetValue(ApprovalNameProperty))); }
            set { base.SetValue(ApprovalNameProperty, value); }
        }

        public static DependencyProperty StatusProperty =
            DependencyProperty.Register("Status",
            typeof(string), typeof(ApprovalWorkflow));

        [Description("Status Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Status
        {
            get { return ((string)(base.GetValue(StatusProperty))); }
            set { base.SetValue(StatusProperty, value); }
        }

        public static DependencyProperty ApprovalWorkflowParameterProperty =
        DependencyProperty.Register("ApprovalWorkflowParameter",
        typeof(ApprovalWorkflowParameter), typeof(ApprovalWorkflow));

        [Description("Approval Workflow Parameter")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.None)]
        public ApprovalWorkflowParameter ApprovalWorkflowParameter
        {
            get { return ((ApprovalWorkflowParameter)(base.GetValue(ApprovalWorkflow.ApprovalWorkflowParameterProperty))); }
            set { base.SetValue(ApprovalWorkflow.ApprovalWorkflowParameterProperty, value); }
        }

        #endregion

        #region Fields
        public IList _listTasks = new List<TaskDetail>();
        public TaskEventHandlerParameter TaskHandlerParameter = new TaskEventHandlerParameter();
        public string strLogDescription = string.Empty;
        public bool mustUpdateAllTasks = false;

        private TaskResultSettings taskResultSettings;
        private Parameter _paramenters = new Parameter();
        private ApprovalConfiguration _approvalConfiguration = new ApprovalConfiguration();
        private bool _blnStopReplicator = false;
        private int _intTaskCompleted = 0;
        private int _intTaskApproved = 0;
        private int _intTaskSignatureVerified = 0;
        private int _intTaskDataQualityCompleted = 0;
        private bool _infoPathForm = false;
        private bool _isItemDocumentType = false;
        private string _strApprovers = string.Empty;
        private string _approverFieldId = string.Empty;
        private int excludeId = 0;
        #endregion

        #region Execute Handlers
        private void InitialData_ExecuteCode(object sender, EventArgs e)
        {
            taskResultSettings = new TaskResultSettings(__ActivationProperties.Site.ID);
            _isItemDocumentType = __ActivationProperties.List.BaseTemplate == SPListTemplateType.DocumentLibrary ? true : false;
            InitialApprovalConfiguration();
        }

        private void tasksReplicatorChild_Init(object sender, ReplicatorChildEventArgs e)
        {
            (e.Activity as TaskApproval).ApprovalInfoTask = (e.InstanceData as TaskDetail);
            (e.Activity as TaskApproval).Parameter = _paramenters;
            (e.Activity as TaskApproval).WorkflowProperties = __ActivationProperties;
        }

        private void tasksReplicatorChild_Complete(object sender, ReplicatorChildEventArgs e)
        {
            string strChildStatus = (e.Activity as TaskApproval).ApprovalInfoTask.Status;
            string strChildStatusWorkflow = (e.Activity as TaskApproval).ApprovalInfoTask.StatusWorkflow;
            excludeId = (e.Activity as TaskApproval).ApprovalInfoTask.TaskId;
            if (ApprovalWorkflowParameter != null)
            {
                this.ApprovalWorkflowParameter.LastApprover = (e.Activity as TaskApproval).Parameter.Submitter;
            }

            //number of tasks is completed
            if (string.IsNullOrEmpty(strChildStatusWorkflow)) return;

            _intTaskCompleted++;

            //number of tasks is completed with signature verified status
            if (strChildStatusWorkflow == taskResultSettings.SignatureVerifiedText)
                _intTaskSignatureVerified++;

            //number of tasks is completed with data quality completed status
            if (strChildStatusWorkflow == taskResultSettings.DataQualityCompletedText)
                _intTaskDataQualityCompleted++;

            //number of tasks is completed with approve status
            if (strChildStatusWorkflow == taskResultSettings.ApprovedText)
                _intTaskApproved++;

            //the task is completed with terminate status
            if (strChildStatusWorkflow == Constants.Workflow.STATUS_CANCEL_TEXT)
            {
                _blnStopReplicator = true;
                this.Status = Constants.Workflow.STATUS_CANCEL_TEXT;
                //UpdateAllTasksNotComplete(Constants.Workflow.STATUS_CANCEL_TEXT, excludeId);
                mustUpdateAllTasks = true;
                return;
            }

            //require number approval
            if (_approvalConfiguration.UseNumberRequired)
            {
                //approved equals require aprroval number tasks
                if (_intTaskApproved == _approvalConfiguration.NumberRequired)
                {
                    _blnStopReplicator = true;
                    this.Status = taskResultSettings.ApprovedText;
                    mustUpdateAllTasks = true;
                    //UpdateAllTasksNotComplete(Constants.Workflow.STATUS_CANCEL_TEXT, excludeId);
                    return;
                }

                //signature verified equals require aprroval number tasks
                if (_intTaskSignatureVerified == _approvalConfiguration.NumberRequired)
                {
                    _blnStopReplicator = true;
                    this.Status = taskResultSettings.SignatureVerifiedText;
                    mustUpdateAllTasks = true;
                    //UpdateAllTasksNotComplete(Constants.Workflow.STATUS_CANCEL_TEXT, excludeId);
                    return;
                }

                //data quality completed  equals require aprroval number tasks
                if (_intTaskDataQualityCompleted == _approvalConfiguration.NumberRequired)
                {
                    _blnStopReplicator = true;
                    this.Status = taskResultSettings.DataQualityCompletedText;
                    mustUpdateAllTasks = true;
                    //UpdateAllTasksNotComplete(Constants.Workflow.STATUS_CANCEL_TEXT, excludeId);
                    return;
                }

                //signature verified equals all tasks
                if (_intTaskSignatureVerified == _listTasks.Count)
                {
                    _blnStopReplicator = true;
                    this.Status = taskResultSettings.SignatureVerifiedText;
                    return;
                }

                //data quality completed equals all tasks
                if (_intTaskDataQualityCompleted == _listTasks.Count)
                {
                    _blnStopReplicator = true;
                    this.Status = taskResultSettings.DataQualityCompletedText;
                    return;
                }

                //all tasks completed
                if (_intTaskCompleted == _listTasks.Count)
                {
                    _blnStopReplicator = true;
                    this.Status = taskResultSettings.RejectedText;
                    return;
                }
            }

            //not require number approval
            if (_intTaskCompleted == _listTasks.Count)
            {
                if (_intTaskApproved == _listTasks.Count)
                    this.Status = taskResultSettings.ApprovedText;
                else
                    this.Status = taskResultSettings.RejectedText;

                if (_intTaskSignatureVerified == _listTasks.Count)
                    this.Status = taskResultSettings.SignatureVerifiedText;

                if (_intTaskDataQualityCompleted == _listTasks.Count)
                    this.Status = taskResultSettings.DataQualityCompletedText;

                _blnStopReplicator = true;
            }
        }

        private void IsStopTaskReplicator(object sender, ConditionalEventArgs e)
        {
            e.Result = _blnStopReplicator;
        }

        private void logResultToHistoryList_ExecuteCode(object sender, EventArgs e)
        {
            strLogDescription = "The task(s) is completed with status: " + this.Status;
        }
        #endregion

        #region Helpers
        private void AddNewTask(string strApprover)
        {
            //not add when approver is exist in list
            var varExist = from TaskDetail taskQuery in _listTasks
                           where string.Compare(taskQuery.InitialAssignTo.Trim(), strApprover.Trim(), true) == 0
                           select taskQuery;
            if (varExist.Count<TaskDetail>() > 0) return;

            TaskDetail task = new TaskDetail();
            task.Id = Guid.NewGuid();
            task.ContentTypeId = _approvalConfiguration.ContentTypeId;
            task.InfoPathForm = _infoPathForm;
            task.TaskProperties = new SPWorkflowTaskProperties();
            task.TaskAfterProperties = new SPWorkflowTaskProperties();

            if (!_isItemDocumentType)
                _paramenters.TaskItemName = __ActivationProperties.Item.Title;
            else
                _paramenters.TaskItemName = Path.GetFileNameWithoutExtension(__ActivationProperties.Item.Name);

            task.Title = string.IsNullOrEmpty(_approvalConfiguration.TaskTitlePrefix) ? _paramenters.TaskItemName :
                _approvalConfiguration.TaskTitlePrefix.Trim() + " " + _paramenters.TaskItemName;

            task.Body = _approvalConfiguration.TaskInstruction;
            task.InitialAssignTo = strApprover;
            if (SPUtility.IsLoginValid(__ActivationProperties.Web.Site, strApprover))
            {
                SPUser myUser = __ActivationProperties.Web.Site.RootWeb.EnsureUser(strApprover);
                task.AssignToEmail = myUser.Email;
            }
            task.PercentComplete = 0f;
            _listTasks.Add(task);
        }

        private void InitialTasksParameters()
        {
            _paramenters.ListItem = __ListItem;
            _paramenters.ListId = __ListId;
            _paramenters.ApprovalConfiguation = _approvalConfiguration;
        }

        private void InitialApprovalConfiguration()
        {
            SPList appovalConfigurationList = null;
            SPListItem approvalListItem;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = GetSite())
                {
                    if (site == null) throw new Exception();

                    using (SPWeb web = GetWeb(site))
                    {
                        if (web == null) throw new Exception();

                        appovalConfigurationList = GetApprovalConfigurationTemplateList(web);
                        if (appovalConfigurationList == null) throw new Exception();

                        approvalListItem = GetApprovalConfigurationTemplateItem(appovalConfigurationList, ApprovalName);
                        if (approvalListItem == null) throw new Exception();

                        SetValueToApprovalConfigurationObject(approvalListItem);
                    }
                }
            });
        }

        private void SetValueToApprovalConfigurationObject(SPListItem item)
        {
            _approvalConfiguration.TaskConfigurationName = item[TaskConfigurationFieldIds.CogfigName].ToString();
            _approvalConfiguration.ContentTypeId = item[TaskConfigurationFieldIds.TaskContentTypeId].ToString();

            __ActivationProperties.TaskList.EnsureContentTypeInListWithoutPrivileges(_approvalConfiguration.ContentTypeId);
            //_infoPathForm = true;
            _infoPathForm = useInfoPathOrAspx(_approvalConfiguration.ContentTypeId);
            _approvalConfiguration.TaskRuleConfiguration = item.GetCustomSettings<TaskRuleSettings>(IOfficeFeatures.Infrastructure);
            _approvalConfiguration.TaskEventConfiguration = item.GetCustomSettings<TaskEventSettings>(IOfficeFeatures.Infrastructure);
            _approvalConfiguration.ExpandGroup = (bool)item[TaskConfigurationFieldIds.ExpandGroup];

            if (item[TaskConfigurationFieldIds.UseMetaDataAssignment] != null)
                _approvalConfiguration.UseMetadataAssignment = (bool)item[TaskConfigurationFieldIds.UseMetaDataAssignment];
            else
                _approvalConfiguration.UseMetadataAssignment = false;

            if (item[TaskConfigurationFieldIds.ByPassTask] != null)
                _approvalConfiguration.ByPassTask = (bool)item[TaskConfigurationFieldIds.ByPassTask];
            else
                _approvalConfiguration.ByPassTask = false;

            if (item[TaskConfigurationFieldIds.IgnoreIfNoParticipant] != null)
                _approvalConfiguration.IgnoreIfNoParticipant = (bool)item[TaskConfigurationFieldIds.IgnoreIfNoParticipant];
            else
                _approvalConfiguration.IgnoreIfNoParticipant = false;

            _approvalConfiguration.AssignmentType = (ExecutionType)Enum.Parse(typeof(ExecutionType), item[TaskConfigurationFieldIds.AssignmentType].ToString(), true);

            if (item[TaskConfigurationFieldIds.EmailTemplateUrl] != null)
                _approvalConfiguration.URLEmailTemplate = (string)item[TaskConfigurationFieldIds.EmailTemplateUrl];
            else
                _approvalConfiguration.URLEmailTemplate = "";

            if (item[TaskConfigurationFieldIds.AssignmentEmailTemplate] != null)
                _approvalConfiguration.AssignmentEmailTemplate = item[TaskConfigurationFieldIds.AssignmentEmailTemplate].ToString();

            if (item[TaskConfigurationFieldIds.ReminderEmailTemplate] != null)
                _approvalConfiguration.ReminderEmailTemplate = (string)item[TaskConfigurationFieldIds.ReminderEmailTemplate];

            if (item[TaskConfigurationFieldIds.EscalationEmailTemplate] != null)
                _approvalConfiguration.EscalationEmailTemplate = item[TaskConfigurationFieldIds.EscalationEmailTemplate].ToString();

            if (item[TaskConfigurationFieldIds.DueDateDuration] != null)
                _approvalConfiguration.DueDateDuration = GetDuration((double)item[TaskConfigurationFieldIds.DueDateDuration],
                    item[TaskConfigurationFieldIds.DueDateMeasure].ToString());

            if (item[TaskConfigurationFieldIds.ReminderDateDuration] != null)
                _approvalConfiguration.ReminderDuration = GetDuration((double)item[TaskConfigurationFieldIds.ReminderDateDuration],
                    item[TaskConfigurationFieldIds.ReminderDateMeasure].ToString());

            if (item[TaskConfigurationFieldIds.EscalationDateDuration] != null)
                _approvalConfiguration.EscalationDuration = GetDuration((double)item[TaskConfigurationFieldIds.EscalationDateDuration],
                    item[TaskConfigurationFieldIds.EscalationDateMeasure].ToString());

            if (item[TaskConfigurationFieldIds.EscalationParty] != null)
                _approvalConfiguration.EscalationPartyEmail = GetEslacationPartyEmail(item[TaskConfigurationFieldIds.EscalationParty].ToString());

            _approvalConfiguration.UseNumberRequired = (bool)item[TaskConfigurationFieldIds.UseNumberRequired];
            if (_approvalConfiguration.UseNumberRequired)
                _approvalConfiguration.NumberRequired = (int)item[TaskConfigurationFieldIds.NumberRequired];

            if (item[TaskConfigurationFieldIds.TaskContributors] != null)
                _approvalConfiguration.TaskContributors = GetListUsers(item[TaskConfigurationFieldIds.TaskContributors].ToString(), false);

            if (item[TaskConfigurationFieldIds.TaskObservers] != null)
                _approvalConfiguration.TaskObservers = GetListUsers(item[TaskConfigurationFieldIds.TaskObservers].ToString(), false);

            if (ApprovalWorkflowParameter != null && !string.IsNullOrEmpty(ApprovalWorkflowParameter.TaskInstructions))
            {
                _approvalConfiguration.TaskInstruction = ApprovalWorkflowParameter.TaskInstructions;
            }
            else
            {
                if (item[TaskConfigurationFieldIds.TaskInstructions] != null)
                    _approvalConfiguration.TaskInstruction = item[TaskConfigurationFieldIds.TaskInstructions].ToString();
                else
                    _approvalConfiguration.TaskInstruction = "";
            }

            if (item[TaskConfigurationFieldIds.TaskTitlePrefix] != null)
                _approvalConfiguration.TaskTitlePrefix = item[TaskConfigurationFieldIds.TaskTitlePrefix].ToString();
            else
                _approvalConfiguration.TaskTitlePrefix = "";

            _approvalConfiguration.AllowReassign = (bool)item[TaskConfigurationFieldIds.AllowReassign];
            _approvalConfiguration.AllowDueDateChangeOnReassignment = (bool)item[TaskConfigurationFieldIds.AllowDueDateChangeRessignment];
            _approvalConfiguration.AlloRequestInfomation = (bool)item[TaskConfigurationFieldIds.AllowRequestInfomation];
            _approvalConfiguration.AllowDueDateChangeOnRequestInformation = (bool)item[TaskConfigurationFieldIds.AllowDueDateChangeRequestInfomation];
            _approvalConfiguration.AllowPlaceOnHold = (bool)item[TaskConfigurationFieldIds.AllowPlaceHoldOn];
            _approvalConfiguration.AllowSendEEC = (bool)item[TaskConfigurationFieldIds.AllowSendEEC];

            _strApprovers = item[TaskConfigurationFieldIds.Approvers] != null ? item[TaskConfigurationFieldIds.Approvers].ToString() : string.Empty;
            _approverFieldId = item[TaskConfigurationFieldIds.ApproversFieldId] != null ? item[TaskConfigurationFieldIds.ApproversFieldId].ToString() : string.Empty;
        }

        private void BuildTaskApprovers(string approvers, string approverFieldId)
        {
            // init Aprovers
            if (ApprovalWorkflowParameter == null || string.IsNullOrEmpty(ApprovalWorkflowParameter.Approvers))
            {
                if (!_approvalConfiguration.UseMetadataAssignment)
                    _approvalConfiguration.Approvers = GetListUsers(approvers, _approvalConfiguration.ExpandGroup);
                else
                {
                    SPField field = null;
                    if (!string.IsNullOrEmpty(approverFieldId))
                    {
                        field = __ActivationProperties.Web.FindField(approverFieldId);
                    }
                    //workflow item do not contain field
                    if (field == null || !__ActivationProperties.Item.Fields.ContainFieldId(field.Id))
                    {
                        __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Workflow item does not contain the column name: " + field.Title, string.Empty);
                        throw new Exception();
                    }
                    else
                    {
                        if (__ActivationProperties.Item[field.Id] != null)
                            _approvalConfiguration.Approvers = GetListUsers(__ActivationProperties.Item[field.Id].ToString(), _approvalConfiguration.ExpandGroup);
                    }
                }
            }
            else
                _approvalConfiguration.Approvers = GetListUsers(ApprovalWorkflowParameter.Approvers);

            if (_approvalConfiguration.Approvers == null || _approvalConfiguration.Approvers.Count == 0)
            {
                return;
                //__ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The participants cannot be empty.", string.Empty);
                //throw new Exception();
            }

            //Build Tasks
            foreach (string strApprover in _approvalConfiguration.Approvers)
                AddNewTask(strApprover);

            //set type of replicator
            this.tasksReplicator.ExecutionType = _approvalConfiguration.AssignmentType;

            InitialTasksParameters();
        }

        private double GetDuration(double intDays, string strMeasure)
        {
            return (strMeasure == "Days") ? intDays : intDays * 7;
        }

        private List<string> GetListUsers(string strUsers)
        {
            List<string> listUsers = new List<string>();
            string[] arrUsers = strUsers.Split(';');
            foreach (string user in arrUsers)
                listUsers.Add(user);

            return listUsers;
        }

        private List<string> GetListUsers(string strUsers, bool blnExpandGroup)
        {
            List<string> listUsers = new List<string>();
            SPFieldLookupValueCollection userValues = new SPFieldLookupValueCollection(strUsers);
            foreach (SPFieldLookupValue userValue in userValues)
            {
                SPFieldUserValue fieldUser = new SPFieldUserValue(__ActivationProperties.Site.RootWeb, userValue.LookupId, userValue.LookupValue);
                if (fieldUser.User != null)
                {
                    listUsers.Add(fieldUser.User.LoginName);
                    continue;
                }

                if (blnExpandGroup == false)
                {
                    listUsers.Add(userValue.LookupValue);
                }
                else
                {
                    SPGroup group = __ActivationProperties.Site.RootWeb.SiteGroups.GetByID(userValue.LookupId);
                    foreach (SPUser user in group.Users)
                        listUsers.Add(user.LoginName);
                }
            }
            return listUsers;
        }

        private string GetEslacationPartyEmail(string strEscalationParty)
        {
            string strReturn = string.Empty;
            SPFieldLookupValue escalationParty = new SPFieldLookupValue(strEscalationParty);
            SPFieldUserValue escalationPartyUser = new SPFieldUserValue(__ActivationProperties.Site.RootWeb, escalationParty.LookupId, escalationParty.LookupValue);
            if (escalationPartyUser.User != null)
                strReturn = escalationPartyUser.User.Email;
            return strReturn;
        }

        private List<string> GetListTerminateStatus(string strTerminateStatus)
        {
            List<string> list = new List<string>();
            string[] statusValues = strTerminateStatus.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < statusValues.Length; i++)
                list.Add(statusValues[i]);
            return list;
        }

        public void UpdateAllTasksNotComplete(string strStatus, int excludeId)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPWorkflowTaskCollection taskCollection = __ActivationProperties.Site.WorkflowManager.GetWorkflowTasks(__ActivationProperties.Item, this.WorkflowInstanceId);
                for (int i = taskCollection.Count; i > 0; i--)
                {
                    SPWorkflowTask task = taskCollection[i - 1];
                    if ((bool)task[SPBuiltInFieldId.Completed]) continue;
                    if (task.ID != excludeId)
                    {
                        //update running tasks
                        int retry = 0;
                        bool success = false;
                        while (retry < 5 && !success)
                        {
                            retry++;
                            try
                            {
                                task[SPBuiltInFieldId.TaskStatus] = Constants.Workflow.COMPLETED_TEXT;
                                task[SPBuiltInFieldId.WorkflowOutcome] = strStatus;
                                task[SPBuiltInFieldId.Completed] = true;
                                task.SystemUpdate();
                                success = true;
                            }
                            catch { }
                        }
                    }
                }
            });
        }

        [SPDisposeCheck.SPDisposeCheckIgnore(SPDisposeCheck.SPDisposeCheckID.SPDisposeCheckID_110, "Handled")]
        private SPSite GetSite()
        {
            SPSite site = null;

            if (CCIUtility.IsAbsoluteUri(ApprovalConfigListURL))
                try
                {
                    //open Site
                    site = new SPSite(ApprovalConfigListURL);
                }
                catch
                {
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The URL " + ApprovalConfigListURL + " does not exist", string.Empty);
                }
            else
                site = __ActivationProperties.Site;

            return site;
        }

        [SPDisposeCheck.SPDisposeCheckIgnore(SPDisposeCheck.SPDisposeCheckID.SPDisposeCheckID_120, "Handled")]
        private SPWeb GetWeb(SPSite site)
        {
            SPWeb web = null;
            if (!CCIUtility.IsAbsoluteUri(ApprovalConfigListURL))
                try
                {
                    web = site.OpenWeb(ApprovalConfigListURL, false);
                }
                catch
                {
                    //input wrong URL, can't open web
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The URL " + ApprovalConfigListURL + " does not exist", string.Empty);
                }
            else
                web = site.OpenWeb();

            return web;
        }

        private SPList GetApprovalConfigurationTemplateList(SPWeb web)
        {
            SPList list = null;
            SPFolder folder = web.GetFolder(ApprovalConfigListURL);
            if (folder.ParentListId != Guid.Empty)
                list = web.Lists.GetList(folder.ParentListId, false);
            else
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The approval configuration list does not exist", string.Empty);
            return list;
        }

        private SPListItem GetApprovalConfigurationTemplateItem(SPList list, string strTemplateName)
        {
            try
            {
                StringBuilder stringBuild = new StringBuilder();
                stringBuild.Append("<Where>");
                stringBuild.Append("    <Eq>");
                stringBuild.Append("        <FieldRef Name='Title' />");
                stringBuild.Append("        <Value Type='Text'>" + strTemplateName + "</Value>");
                stringBuild.Append("    </Eq>");
                stringBuild.Append("</Where>");
                SPQuery query = new SPQuery();
                query.Query = stringBuild.ToString();
                SPListItemCollection items = list.GetItems(query);
                if (items.Count > 0)
                    return items[0];
                else
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The approval name " + ApprovalName + " does not exist", string.Empty);
            }
            catch (Exception e)
            {
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Error when load template: " + e.ToString(), string.Empty);
            }
            return null;
        }

        private bool useInfoPathOrAspx(string contentTypeId)
        {
            SPContentType contentType = __ActivationProperties.Web.FindContentType(new SPContentTypeId(contentTypeId));
            if (contentType == null) return false;
            SPContentType listContentType = __ActivationProperties.TaskList.ContentTypes[contentType.Name];
            CustomFormSettings customFormSettings = listContentType.GetCustomSettings<CustomFormSettings>(IOfficeFeatures.Infrastructure);
            SPContentTypeId ctid = new SPContentTypeId(Constants.CCI_WORKFLOW_TASK_CONTENT_TYPE_ID);

            return true;

            if (customFormSettings == null && ctid.IsParentOf(listContentType.Id))
                return true;

            if (customFormSettings == null)
                return false;

            if (customFormSettings.EditItemFormType == CCIFormType.ASPXForm)
                return true;

            if (customFormSettings.EditItemFormType == CCIFormType.InfoPath &&
                !string.IsNullOrEmpty(customFormSettings.EditItemFormXsnLocation) &&
                customFormSettings.EditItemFormXsnLocation.EndsWith(".xsn", StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;
        }
        #endregion

        private void isByPassTask_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = _isItemDocumentType && _approvalConfiguration.ByPassTask && ApprovalWorkflowParameter != null &&
                HashHelper.Compare(ApprovalWorkflowParameter.HashFile, HashHelper.Create(__ActivationProperties.Item.File, false));
        }

        private void buildTasks_ExecuteCode(object sender, EventArgs e)
        {
            BuildTaskApprovers(_strApprovers, _approverFieldId);
        }

        private void setByPassValue_ExecuteCode(object sender, EventArgs e)
        {
            this.Status = taskResultSettings.ApprovedText;
            //strLogDescription = "The task(s) is bypassed";
            strLogDescription = "The " + _approvalConfiguration.TaskConfigurationName + " is bypassed.";

            TaskHandlerParameter.WorkflowProperties = __ActivationProperties;
            TaskHandlerParameter.EventSettings = _approvalConfiguration.TaskEventConfiguration;
        }

        private void isHaveApprovers_ConditionCode(object sender, ConditionalEventArgs e)
        {
            e.Result = _approvalConfiguration.Approvers != null
                && _approvalConfiguration.Approvers.Count > 0;
        }

        private void setIgnoreTask_ExecuteCode(object sender, EventArgs e)
        {
            strLogDescription = "The " + _approvalConfiguration.TaskConfigurationName + " is ignored because there is no participant.";
            this.Status = taskResultSettings.ApprovedText;
        }

        private void isIgnoreIfNoParticipant_ConditionCode(object sender, ConditionalEventArgs e)
        {
            e.Result = _approvalConfiguration.IgnoreIfNoParticipant;
        }

        private void setTerminateLog_ExecuteCode(object sender, EventArgs e)
        {
            strLogDescription = "The workflow is terminated because there is no participant of " + _approvalConfiguration.TaskConfigurationName;
        }

        private void updateAllTasks_ExecutedCode(object sender, EventArgs e)
        {
            UpdateAllTasksNotComplete(Constants.Workflow.STATUS_CANCEL_TEXT, excludeId);
        }

        private void isMustUpdateAllTasks_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = mustUpdateAllTasks;
        }
    }
}
