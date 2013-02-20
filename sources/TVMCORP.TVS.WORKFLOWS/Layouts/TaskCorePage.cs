using System;
using System.Collections;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.WORKFLOWS.Pages
{
    public class TaskCorePage : LayoutsPageBase
    {
        #region Properties
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
        #endregion

        #region Handlers
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
        #endregion
    }
}
