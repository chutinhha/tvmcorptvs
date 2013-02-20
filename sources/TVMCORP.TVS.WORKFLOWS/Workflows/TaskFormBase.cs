using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Extensions;
using System.Collections;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using System.Web.UI.WebControls;
using TVMCORP.TVS.UTIL.MODELS;
using System.Web.UI;
using TVMCORP.TVS.UTIL.Helpers;
using TVMCORP.TVS.UTIL;


namespace TVMCORP.TVS.WORKFLOWS.Core.Workflows
{
    public class TaskFormBase : LayoutsPageBase
    {
        #region Properties
        public virtual string CurrentForm { get { return "TaskFormBase"; } }

        protected SPList CurrentTaskList
        {
            get
            {
                if (SPContext.Current.List != null)
                    return SPContext.Current.List;
                return null;
            }
        }

        protected SPListItem CurrentTaskItem
        {
            get
            {
                if (SPContext.Current.ListItem != null)
                    return SPContext.Current.ListItem;
                return null;
            }
        }

        protected SPListItem CurrentWorkflowItem
        {
            get
            {
                try
                {
                    if (CurrentTaskItem == null) return null;
                    string fileUrl = (string)CurrentTaskItem[SPBuiltInFieldId.WorkflowLink];
                    fileUrl = fileUrl.Split(',')[0];
                    return Utility.GetItemByDocumentUrl(fileUrl);
                }
                catch { return null; }
            }
        }

        protected Hashtable CurrentTaskExtendedProperties
        {
            get
            {
                if (CurrentTaskItem != null)
                    return SPWorkflowTask.GetExtendedPropertiesAsHashtable(CurrentTaskItem);
                return null;
            }
        }

        protected SPContentType CurrentTaskContentType
        {
            get
            {
                try
                {
                    if (SPContext.Current.ListItem != null)
                        return SPContext.Current.ListItem.ContentType;
                    else if (!string.IsNullOrEmpty(Request.QueryString["ContentTypeId"]))
                        return SPContext.Current.List.ContentTypes[new SPContentTypeId(Request.QueryString["ContentTypeId"])];

                    return SPContext.Current.List.ContentTypes[0];
                }
                catch { return null; }
            }
        }

        public TaskFormOption FormOption { get {

            return CurrentTaskExtendedProperties.FromHash<TaskFormOption>();
        } }

        public List<string> PropertiesToUpdated{ get {
            var obj = CurrentTaskExtendedProperties[TaskExtendProperties.UPDATED_PROPERTIES] as string;
            if (!string.IsNullOrEmpty(obj))
            {
                return SerializationHelper.DeserializeFromXml<List<string>>(obj); 
            }
             return new List<string>();
        } }

        

        protected bool IsDialog
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["IsDlg"]))
                    return false;
                try
                {
                    return Convert.ToBoolean(Convert.ToByte(Request.QueryString["IsDlg"].Split(',')[0]));
                }
                catch { return false; }
            }
        }

        protected string SourceUrl
        {
            get
            {
                return base.Request.QueryString["Source"];
            }
        }
        public string Status { get { return (string)CurrentTaskExtendedProperties[TaskExtendProperties.OWS_TASK_STATUS]; } }
        #endregion

        #region Handlers

       public void DisplayStatus(Literal lblStatus)
        {
            switch (this.Status)
            {
                case TaskApprovalStatus.Approved:
                    lblStatus.Text = "Approved";
                    break;

                case TaskApprovalStatus.Rejected:
                    lblStatus.Text = "Rejected";
                    break;


                case TaskApprovalStatus.RequestChange:
                    lblStatus.Text = "RequestChange";
                    break;

                case TaskApprovalStatus.RequestInf:
                    lblStatus.Text = "RequestInf";
                    break;

                case TaskApprovalStatus.Canceled:
                    lblStatus.Text = "Canceled";
                    break;

                
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateFormButtons();
                if (this.Request.RawUrl.Contains("ApprovalWFViewOnlyTaskForm")) return;
                if (Status == TaskApprovalStatus.Approved ||
                    Status == TaskApprovalStatus.Rejected ||
                     Status == TaskApprovalStatus.Canceled)
                {
                    Response.Redirect(this.Request.RawUrl.Replace("ApprovalWFTaskForm", "ApprovalWFViewOnlyTaskForm"));

                }
            }

            base.OnLoad(e);
        }

        private  Control FindControlRecursive(Control Root, string Id)
        {

            if (Root.ID == Id)

                return Root;

            foreach (Control Ctl in Root.Controls)
            {

                Control FoundCtl = FindControlRecursive(Ctl, Id);

                if (FoundCtl != null)

                    return FoundCtl;

            }

            return null;

        }

        public virtual void UpdateFormButtons()
        {
            var btnApprove = FindControlRecursive(this,"btnApprove");
            btnApprove.SetProperty("Text" , !string.IsNullOrEmpty(FormOption.ApproveLabel)?FormOption.ApproveLabel:"Approve");
            btnApprove.Visible = FormOption.EnableApprove;

            var btnReject = FindControlRecursive(this, "btnReject");
            btnReject.SetProperty("Text", !string.IsNullOrEmpty(FormOption.RejectLabel) ? FormOption.RejectLabel : "Reject");
            btnReject.Visible = FormOption.EnableReject;

            ShowControlButton("btnReassign", FormOption.ReassignLabel, "Reassign", FormOption.EnableReassign);
            ShowControlButton("btnRequestInf", FormOption.RequestInformationLabel, "Request Information", FormOption.EnableRequestInf);
            ShowControlButton("btnRequestChange", FormOption.RequestChangeLabel, "Change", FormOption.EnableRequestChange);
            ShowControlButton("btnHold", FormOption.OnHoldLabel, "Hold", FormOption.EnableHoldOn);
            
        }

        public void ShowControlButton(string ctrId, string title, string titleDefault, bool enableControl)
        {
            var btnRequestInf = FindControlRecursive(this, ctrId);
            if (btnRequestInf != null)
            {

                btnRequestInf.SetProperty("Text", !string.IsNullOrEmpty(FormOption.RequestInformationLabel) ? FormOption.RequestInformationLabel : titleDefault);
                btnRequestInf.Visible = enableControl;
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (IsDialog)
                ClosePopup();
            else
                SPUtility.Redirect(CurrentTaskList.DefaultViewUrl, SPRedirectFlags.Default, this.Context);
        }
        #endregion

        #region Execute Code
        protected void Back()
        {
            if (IsDialog)
                ClosePopup();
            else
                if (string.IsNullOrEmpty(SourceUrl))
                    SPUtility.Redirect(Request.RawUrl, SPRedirectFlags.Default, this.Context);
                else
                    SPUtility.Redirect(SPEncode.UrlDecodeAsUrl(SourceUrl), SPRedirectFlags.Default, this.Context);
        }

        private void ClosePopup()
        {
            Context.Response.Clear();
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.Flush();
            Context.Response.End();
        }


        protected void ChangeForm_Command(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            Response.Redirect(this.Request.RawUrl.Replace(CurrentForm, button.CommandArgument.ToString()));
        }

        #endregion
    }
}
