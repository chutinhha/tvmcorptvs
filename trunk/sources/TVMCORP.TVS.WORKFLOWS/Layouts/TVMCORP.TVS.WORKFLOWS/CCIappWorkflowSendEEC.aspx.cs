using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.Util.Helpers;
using TVMCORP.TVS.Util.Utilities;

using TVMCORP.TVS.WORKFLOWS.MODELS;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.WORKFLOWS.Core.Pages;

namespace TVMCORP.TVS.WORKFLOWS.Layouts
{
    public partial class CCIappWorkflowSendEEC : TaskCorePage
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(Cancel_Click);
            btnSend.Click += new EventHandler(btnSend_Click);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }
       
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            Hashtable properties = CurrentTaskExtendedProperties;
            properties[TaskExtendProperties.CCI_EEC_TO] = txtTo.Text.Trim();
            if (string.IsNullOrEmpty(txtCC.Text))
                properties[TaskExtendProperties.CCI_EEC_CC] = txtCC.Text.Trim();

            properties[TaskExtendProperties.CCI_EEC_SUBJECT] = txtSubject.Text.Trim();
            properties[TaskExtendProperties.CCI_EEC_BODY] = txtBody.Text;
            properties[TaskExtendProperties.CCI_TASK_STATUS] = CurrentTaskExtendedProperties[TaskExtendProperties.CCI_EEC_TASK_STATUS];
            CurrentTaskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
            SPWorkflowTask.AlterTask(CurrentTaskItem, properties, true);
            CurrentTaskItem.SystemUpdate();
            Back();
        }

        private void loadData()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                if (validateDocumentForEEC(CurrentWorkflowItem, true))
                {
                    initialData();
                }
            });
        }

        private bool validateDocumentForEEC(SPListItem item, bool showErrorMessage)
        {
            if (item == null)
            {
                return false;
            }

            string cciUniqueIdField = Constants.EEC.CCIUniqueIdFieldName;

            bool result = false;
            if (item.Fields.ContainsField(cciUniqueIdField))
            {
                result = true;
            }
            else
            {
                if (showErrorMessage)
                    SPUtility.TransferToErrorPage("This document is not qualified for External Email Collaboration. (Missing CCIUniquedId field)");
                return false;
            }
            return result;
        }
     
        private List<SPFile> getDocumentSetFilesBySetting(EECSettings setting)
        {
            List<SPFile> files = new List<SPFile>();

            if (CurrentWorkflowItem.ContentTypeId.IsChildOf(SPBuiltInContentTypeId.DocumentSet))
            {
                if (setting != null || !string.IsNullOrEmpty(setting.DocSetIncludedFieldName))
                {
                    foreach (SPFile file in CurrentWorkflowItem.Folder.Files)
                    {
                        SPListItem item = file.Item;
                        if (item.Fields.ContainsField(setting.DocSetIncludedFieldName)
                            && item[setting.DocSetIncludedFieldName] != null
                            && item[setting.DocSetIncludedFieldName].ToString() == setting.DocSetIncludedFieldValue
                            && validateDocumentForEEC(item, false))
                        {
                            files.Add(file);
                        }
                    }

                }
                else
                {
                    foreach (SPFile file in CurrentWorkflowItem.Folder.Files)
                        files.Add(file);
                }
            }
            else
            {
                files.Add(this.CurrentWorkflowItem.File);
            }

            return files;
        }

        private string getAttachmentFileNames(List<SPFile> files)
        {
            StringBuilder sb = new StringBuilder();

            foreach (SPFile file in files)
            {
                sb.Append(file.Name + "; ");
            }
            return sb.ToString().Trim("; ".ToCharArray());
        }

        private void initialData()
        {
            string strFullName = string.Empty;
            string strFromEmail = SPContext.Current.Site.WebApplication.OutboundMailSenderAddress;
            if (!string.IsNullOrEmpty(strFromEmail) && SPUtility.GetLoginNameFromEmail(SPContext.Current.Site, strFromEmail) != null)
                strFullName = SPUtility.GetFullNameFromLogin(SPContext.Current.Site, SPUtility.GetLoginNameFromEmail(SPContext.Current.Site, strFromEmail));

            string strFromShow = (string.IsNullOrEmpty(strFullName) == true) ? "SharePoint" : strFullName;
            strFromShow += " on Behalf Of " + SPContext.Current.Web.CurrentUser.Name;
            lbFrom.Text = string.IsNullOrEmpty(SPContext.Current.Web.CurrentUser.Email) ? strFromShow : SPUtility.GetFullNameFromLogin(SPContext.Current.Site, SPContext.Current.Web.CurrentUser.LoginName);

            EECSettings settings = SPListItemExtensions.GetCustomSettings<EECSettings>(CurrentWorkflowItem, IOfficeFeatures.EEC);
            if (settings == null || settings.Enabled == false)
            {
                SPUtility.TransferToErrorPage("External Email Collaboration is not enabled.");
            }

            List<SPFile> files = getDocumentSetFilesBySetting(settings);
            if (files.Count == 0)
            {
                SPUtility.TransferToErrorPage("There is no document.");
            }

            lbAttachments.Text = getAttachmentFileNames(files);

            if (settings == null) return;

            SPList emailTemplateList = CCIUtility.GetListFromURL(settings.EmailTemplateListUrl);
            if (emailTemplateList == null) return;

            SPListItemCollection emailListItems = SPListExtensions.FindItems(emailTemplateList, "Title", settings.EmailTemplateName);
            if (emailListItems.Count == 0) return;

            SPListItem emailListItem = emailListItems[0];
            if (emailListItem == null) return;

            if (emailListItem["Subject"] != null)
            {
                txtSubject.Text = SendEmailHelper.GetEmailFieldFromTemplate(CurrentWorkflowItem, emailListItem, "Subject", SPContext.Current.Web.CurrentUser);
            }

            txtBody.Text = SendEmailHelper.GetEmailFieldFromTemplate(CurrentWorkflowItem, emailListItem, "Body", SPContext.Current.Web.CurrentUser);
        }
    }
}
