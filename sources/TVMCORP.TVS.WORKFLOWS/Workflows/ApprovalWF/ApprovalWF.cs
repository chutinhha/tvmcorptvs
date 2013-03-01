using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.WORKFLOWS.Activities;
using TVMCORP.TVS.UTIL.Helpers;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Entities;
using TVMCORP.TVS.UTIL.Utilities.Camlex;

namespace TVMCORP.TVS.WORKFLOWS.Workflows
{
    public sealed partial class ApprovalWF : SequentialWorkflowActivity
    {
        public ApprovalWF()
        {
            InitializeComponent();
        }

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {

        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String approvalLevel1_TaskContentTypeId1 = default(System.String);

        private bool documentChanged = false;

        private void OnWorklowItemChange(object sender, ExternalDataEventArgs e)
        {
            //TODO - send email notification
            if (workflowProperties.Item[TVSColumnIds.LastUpdatedByWF] != null &&
                (bool)workflowProperties.Item[TVSColumnIds.LastUpdatedByWF] == true)
            {
                changeByTaskForm = true;
                workflowProperties.Item[SPBuiltInFieldId.WorkflowVersion] = 1;
                workflowProperties.Item[TVSColumnIds.LastUpdatedByWF] = false;
                workflowProperties.Item.SystemUpdate();
            }
            else
            {
                changeByTaskForm = false;
                documentChanged = true;

            }
        }

        public IList approvalLevels = default(System.Collections.IList);

        private void ApprovalLevel_DataBind(object sender, ReplicatorChildEventArgs e)
        {
            ApprovalLevel activity = e.Activity as ApprovalLevel;
            ApprovalLevelInfo data = e.InstanceData as ApprovalLevelInfo;
            data.FormOption = AssociationObj.TaskFormOption;
            data.EndAtFirstRejection = AssociationObj.EndOnFirstReject;
            activity.ApprovalData = data;

        }
        public ApprovalWFAssociationData AssociationObj;
        public ApprovalWFInitiationData InitiationObj;

        private void InitialData(object sender, EventArgs e)
        {
            this.AssociationObj = SerializationHelper.DeserializeFromXml<ApprovalWFAssociationData>(this.workflowProperties.AssociationData);
            this.InitiationObj = SerializationHelper.DeserializeFromXml<ApprovalWFInitiationData>(this.workflowProperties.InitiationData);
            this.DeplayOnStart_Minutes = (double)AssociationObj.DelayOnStart;
            WorkflowStartedEvent_Parameter.EventSettings = AssociationObj.WFEvents;
            WorkflowStartedEvent_Parameter.WorkflowProperties = this.workflowProperties;
            WorkfowEndedEvent_Parameter.EventSettings = AssociationObj.WFEvents;
            WorkfowEndedEvent_Parameter.WorkflowProperties = this.workflowProperties;

            EnsureApprovalWFColumns(workflowProperties.Item);
        }

        private void EnsureApprovalWFColumns(SPListItem list)
        {
            if (list.Fields.ContainFieldId(TVSColumnIds.LastUpdatedByWF)) return;

            var field = list.Fields.Add(list.Web.AvailableFields[TVSColumnIds.LastUpdatedByWF]);
            //TODO: doing something here to set field is visible from new form
            workflowProperties.Item[SPBuiltInFieldId.WorkflowVersion] = 1;
            workflowProperties.Item[TVSColumnIds.LastUpdatedByWF] = false;
            workflowProperties.Item.SystemUpdate();

        }



        public String TaskOutcome = TaskApprovalStatus.Initiated;

        private void UpdateApprovalStatus_ExecuteCode(object sender, EventArgs e)
        {

        }

        public String SetItemApproval___ListId = default(System.String);
        public Int32 SetItemApproval___ListItem = default(System.Int32);

        private void SetItemApprovalData_ExecuteCode(object sender, EventArgs e)
        {
            SetItemApproval___ListId = this.workflowProperties.List.ID.ToString();
            SetItemApproval___ListItem = this.workflowProperties.ItemId;

            if (TaskOutcome == TaskApprovalStatus.Approved || (this.byPassed && AssociationObj.ApproveIfByPass))
            {
                SetItemApproval_Status = SPModerationStatusType.Approved;
            }

        }

        public SPModerationStatusType SetItemApproval_Status = SPModerationStatusType.Denied;

        private void DocumentChangeSendEmail_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void DocumentApprovedRejectedOrChanged(object sender, ConditionalEventArgs e)
        {
            e.Result = documentChanged || (TaskOutcome == TaskApprovalStatus.Approved || TaskOutcome == TaskApprovalStatus.Rejected);
        }

        private void IsEnableContentApproval(object sender, ConditionalEventArgs e)
        {
            e.Result = AssociationObj.EnableContentApproval && (byPassed || TaskOutcome == TaskApprovalStatus.Approved);
        }

        private void CompleteAllTasks_MethodInvoking(object sender, EventArgs e)
        {
            CompleteAllTasks_TaskProperties.PercentComplete = 1f;
            CompleteAllTasks_TaskProperties.ExtendedProperties[TaskExtendProperties.OWS_TASK_STATUS] = TaskApprovalStatus.Canceled;

            CompleteAllTasks_TaskProperties.ExtendedProperties["Status"] = "Completed";
            CompleteAllTasks_TaskProperties.ExtendedProperties["PercentComplete"] = 1f;
            CompleteAllTasks_TaskProperties.ExtendedProperties["Outcome"] = "Canceled";
        }

        public SPWorkflowTaskProperties CompleteAllTasks_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void TaskRejectOrApproved(object sender, ConditionalEventArgs e)
        {
            if (TaskOutcome == TaskApprovalStatus.Initiated)
            {
                e.Result = false;
            }
            else
                e.Result = count >= InitiationObj.ApprovalLevels.Count || (AssociationObj.EndOnFirstReject && TaskOutcome == TaskApprovalStatus.Rejected);
        }

        private int count = 0;
        private void ApprovalLevelComplete(object sender, ReplicatorChildEventArgs e)
        {
            count++;
        }

        private void IfDeplayOnStart(object sender, ConditionalEventArgs e)
        {
            e.Result = AssociationObj.DelayOnStart > 0;
        }

        public Double DeplayOnStart_Minutes = default(System.Double);
        private bool byPassed = false;
        private void IfStartApproval(object sender, ConditionalEventArgs e)
        {
            bool result = false;

            var item = this.workflowProperties.Item;

            if (!string.IsNullOrEmpty(AssociationObj.ConditionFieldId) && item.Fields.ContainFieldId(new Guid(AssociationObj.ConditionFieldId)))
            {
                var data = item[new Guid(AssociationObj.ConditionFieldId)];
                if (data != null)
                {
                    result = data.ToString() == AssociationObj.ConditionFieldValue;
                }
            }

            e.Result = !AssociationObj.EnableStartingCondition || result;
            byPassed = !e.Result;
        }

        private void IsEnableUpdatePermission(object sender, ConditionalEventArgs e)
        {
            e.Result = AssociationObj.EnableUpdatePermission;

        }

        public System.Collections.Generic.List<String> _allApprovers = new System.Collections.Generic.List<System.String>();

        private void codeActivity2_ExecuteCode(object sender, EventArgs e)
        {
            var list = this.workflowProperties.TaskList;

            CAMLListQuery<TaskItem> query = new CAMLListQuery<TaskItem>(list);

            string caml = Camlex.Query().Where(p => (Guid)p[SPBuiltInFieldId.WorkflowInstanceID] == WorkflowInstanceId).ToString();

            var items = query.ExecuteListQuery(caml);
            foreach (var item in items)
            {
                SPFieldUserValue fv = new SPFieldUserValue(workflowProperties.Web, item.AssignedTo);
                _allApprovers.Add(fv.User.LoginName);
            }
        }

        public TaskEventHandlerParameter WFApprovedEvent_Parameter = new TVMCORP.TVS.UTIL.MODELS.TaskEventHandlerParameter();

        private void SetWFApprovedEventParameter_ExecuteCode(object sender, EventArgs e)
        {
            WFApprovedEvent_Parameter = new TaskEventHandlerParameter()
            {
                EventSettings = AssociationObj.WFEvents,
                Variables = new System.Collections.Generic.List<NameValue>(),
                WorkflowProperties = workflowProperties,
            };
        }

        public TaskEventHandlerParameter WorkflowStartedEvent_Parameter = new TVMCORP.TVS.UTIL.MODELS.TaskEventHandlerParameter();
        public TaskEventHandlerParameter WorkfowEndedEvent_Parameter = new TVMCORP.TVS.UTIL.MODELS.TaskEventHandlerParameter();
        private bool changeByTaskForm = true;
        private void IsItemChangeFromTaskForm(object sender, ConditionalEventArgs e)
        {
            e.Result = changeByTaskForm;
        }

        private void IsItemApproved(object sender, ConditionalEventArgs e)
        {
            e.Result = TaskOutcome == TaskApprovalStatus.Approved;
        }


    }
}
