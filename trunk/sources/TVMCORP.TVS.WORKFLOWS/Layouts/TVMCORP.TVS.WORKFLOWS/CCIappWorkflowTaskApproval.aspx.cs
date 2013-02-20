using System;
using System.Collections;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.WORKFLOWS.Core.Pages;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Layouts
{
    public partial class CCIappWorkflowTaskApproval : TaskCorePage
    {
        private TaskResultSettings taskResultSettings; 

        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(Cancel_Click);
            btnApprove.Click += new EventHandler(btnApprove_Click);
            btnReject.Click += new EventHandler(btnReject_Click);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            taskResultSettings = new TaskResultSettings(SPContext.Current.Site.ID);
            if (!IsPostBack)
            {
                switchView();
                loadData();
            }
        }
        
        private void btnApprove_Click(object sender, EventArgs e)
        {
            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.OWS_TASK_STATUS] = "#";
            properties[TaskExtendProperties.CCI_TASK_STATUS] = taskResultSettings.ApprovedText;
            properties[TaskExtendProperties.CCI_COMMENT] = txtComment.Text;
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }
        
        private void btnReject_Click(object sender, EventArgs e)
        {
            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.OWS_TASK_STATUS] = "@";
            properties[TaskExtendProperties.CCI_TASK_STATUS] = taskResultSettings.RejectedText;
            properties[TaskExtendProperties.CCI_COMMENT] = txtComment.Text;
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        private void switchView()
        {
            //swith to resubmit view
            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_TYPE] != null &&
               Convert.ToInt16((string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_TYPE]) == 1 &&
                 !Convert.ToBoolean(CurrentTaskItem[SPBuiltInFieldId.Completed]))
            {
                SPUtility.Redirect(Request.RawUrl.Replace(CCIappWorkflowTaskView.APPROVAL, CCIappWorkflowTaskView.CHANGE), SPRedirectFlags.Default, this.Context);
            }
        
            //swith to readonly view
            string eecStatus = CurrentTaskExtendedProperties[TaskExtendProperties.CCI_EEC_TASK_BACK_STATUS] != null ? (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_EEC_TASK_BACK_STATUS] : string.Empty;
            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS] != null &&
                (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS] != Constants.Workflow.STATUS_ON_HOLD_TEXT &&
                (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS] != eecStatus)
            {
                SPUtility.Redirect(Request.RawUrl.Replace(CCIappWorkflowTaskView.APPROVAL, CCIappWorkflowTaskView.READONLY), SPRedirectFlags.Default, this.Context);
            }
        }

        private void loadData()
        {
            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS] != null &&
                (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_STATUS] == Constants.Workflow.STATUS_ON_HOLD_TEXT)
            {
                divReasonOnHold.Visible = true;
                if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_COMMENT] != null)
                    lblReaonOnHold.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_COMMENT];
            }

            if (CurrentTaskItem[SPBuiltInFieldId.TaskDueDate] != null)
                lblDueBy.Text = Convert.ToDateTime(CurrentTaskItem[SPBuiltInFieldId.TaskDueDate]).ToShortDateString();

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION] != null)
                txtInstruction.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_TASK_INSTRUCTION];

            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_REASSIGN] != null &&
                Convert.ToBoolean((string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_REASSIGN]))
            {
                hplReassign.NavigateUrl = Request.RawUrl.Replace(CCIappWorkflowTaskView.APPROVAL, CCIappWorkflowTaskView.REASSIGN) + "&Source=" + SPEncode.UrlEncode(Request.RawUrl);
            }


            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_REQUEST_INFORMATION] != null &&
              Convert.ToBoolean((string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_REQUEST_INFORMATION]))
            {
                hplRequestInformation.NavigateUrl = Request.RawUrl.Replace(CCIappWorkflowTaskView.APPROVAL, CCIappWorkflowTaskView.REQUESTCHANGE) + "&Source=" + SPEncode.UrlEncode(Request.RawUrl);
            }


            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_ON_HOLD] != null &&
              Convert.ToBoolean((string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_ON_HOLD]))
            {
                hplOnHold.NavigateUrl = Request.RawUrl.Replace(CCIappWorkflowTaskView.APPROVAL, CCIappWorkflowTaskView.ONHOLD) + "&Source=" + SPEncode.UrlEncode(Request.RawUrl);
            }

            hplEEC.Text = (string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_EEC_BUTTON_LABEL];
            if (CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_SEND_EEC] != null &&
                Convert.ToBoolean((string)CurrentTaskExtendedProperties[TaskExtendProperties.CCI_ALLOW_SEND_EEC]))
            {
                hplEEC.NavigateUrl = Request.RawUrl.Replace(CCIappWorkflowTaskView.APPROVAL, CCIappWorkflowTaskView.EEC) + "&Source=" + SPEncode.UrlEncode(Request.RawUrl);

            }
        }
    }
}
