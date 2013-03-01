using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;
using IModel = TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Utilities;
 
using TVMCORP.TVS.UTIL.MODELS;

using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Helpers;
using TVMCORP.TVS.UTIL.Services;

namespace TVMCORP.TVS.WORKFLOWS.Workflows
{
    public partial class ApprovalWFInitiation : LayoutsPageBase
    {
        private IModel.ApprovalWFAssociationData assData = null;

        
        protected override void OnInit(EventArgs e)
        {
            cdcatalog.ItemDataBound += new RepeaterItemEventHandler(cdcatalog_ItemDataBound);
            base.OnInit(e);
        }

        private void BindPeoplePicker(PeopleEditor ppEditor, List<string> list)
        {
            string users = "";
            System.Collections.ArrayList entityArrayList = new System.Collections.ArrayList();

            foreach (var item in list)
            {
                PickerEntity entity = new PickerEntity();
                entity.Key = item;
                // this can be omitted if you're sure of what you are doing
                entity = ppEditor.ValidateEntity(entity);
                entityArrayList.Add(entity);
                users += item + ";";
            }

            if (list.Count > 0)
            {
                //ppEditor.CommaSeparatedAccounts = users.Remove(users.LastIndexOf(";"), 1);
                ppEditor.UpdateEntities(entityArrayList);
            }
        }
        
        void cdcatalog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                bool isHidden = false;
                PeopleEditor peSpecificUsesGroup = e.Item.FindControl("peSpecificUsesGroup") as PeopleEditor;
                TextBox txtDueDate = e.Item.FindControl("txtDueDate") as TextBox;
                TextBox txtDuration = e.Item.FindControl("txtDuration") as TextBox;
                EncodedLiteral level = e.Item.FindControl("targetAppAdminDescription") as EncodedLiteral;
                InputFormTextBox rtfMessage = e.Item.FindControl("rtfMessage") as InputFormTextBox;
                TextBox txtMessageTitle = e.Item.FindControl("txtMessageTitle")  as TextBox;
                TextBox txtMessage = e.Item.FindControl("txtMessage")  as TextBox;
                var trEmailMessage = e.Item.FindControl("trEmailMessage");
                rtfMessage.Text = "This is text message";
                IModel.ApprovalWFApprover data = e.Item.DataItem as IModel.ApprovalWFApprover;

                if (data.EnableEmail)
                {
                    var template = data.EmailTemplate;
                    var templateItem =EmailTemplateService.GetTemplateByName(template.Url, template.Name);
                    if(templateItem!= null){
                        rtfMessage.Text = templateItem.Body;
                        txtMessageTitle.Text = templateItem.Subject;
                    }
                }

                trEmailMessage.Visible = data.AllowChangeMessage;
                txtMessage.Text = data.TaskInstruction;

                level.Text = data.ApprovalLevelName;
                if (data.DurationPerTask != 0)
                {
                    txtDuration.Text = data.DurationPerTask.ToString();
                    txtDuration.Enabled = false;
                }

               
                txtDueDate.Text = data.DueDate;

                DateTime dueDate = GetDueDate(data.DueDate);

                if(DateTime.Compare(dueDate, DateTime.MinValue) != 0)
                {
                    txtDueDate.Text = dueDate.ToShortDateString();
                    txtDueDate.Enabled = false;
                }

                if (!String.IsNullOrEmpty(data.ColumnName))
                {
                    try
                    {
                        string approver = this.workflowListItem[new Guid(data.ColumnName)] as string;

                        if (!string.IsNullOrEmpty(approver))
                        {
                            if (this.workflowListItem[new Guid(data.ColumnName)].GetType().Name == Constants.ASSIGMENT_FIELD_NAME)
                            {
                                var names = approver.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                List<string> loginAccounts = new List<string>();

                                if (names.Length > 1)
                                {
                                    for (int i = 0; i < names.Length; i = i + 2)
                                    {
                                        SPUser user = this.Web.AllUsers.GetByID(int.Parse(names[i]));
                                        loginAccounts.Add(user.LoginName);
                                    }

                                    BindPeoplePicker(peSpecificUsesGroup, loginAccounts);
                                    isHidden = true;
                                }
                            }
                            else
                            {
                                var field = this.workflowListItem.Fields[new Guid(data.ColumnName)];


                                SPFieldUserValueCollection members = this.workflowListItem[new Guid(data.ColumnName)] == null ?
                                                                        null : new SPFieldUserValueCollection(workflowListItem.Web, this.workflowListItem[new Guid(data.ColumnName)].ToString());


                                if (members != null)
                                {
                                    ArrayList entityArrayList = new ArrayList();
                                    for (int i = 0; i < members.Count; i++)
                                    {
                                        PickerEntity entity = new PickerEntity();
                                        if (members[i].User != null)
                                        {
                                            entity.Key = members[i].User.LoginName;
                                        }
                                        else
                                        {

                                            entity.Key = members[i].LookupValue;

                                        }
                                        entityArrayList.Add(entity);
                                    }

                                    peSpecificUsesGroup.UpdateEntities(entityArrayList);
                                    //isHidden = true;
                                }
                            }
                        }    
                    }
                    catch (Exception ex) { }
                }

                if (data != null && data.SpecificUserGroup.Count > 0)
                {
                    if(!data.EnableChangeApprovers) peSpecificUsesGroup.Enabled = false;
                    BindPeoplePicker(peSpecificUsesGroup, data.SpecificUserGroup);
                    //isHidden = true;
                }

                if (data != null && data.ManagerApprove)
                {
                    try
                    {
                        if (!data.EnableChangeApprovers) peSpecificUsesGroup.Enabled = false;

                        List<string> loginAccounts = new List<string>();
                        //loginAccounts.Add(IOfficeContext.Manager.LoginName);

                        BindPeoplePicker(peSpecificUsesGroup, loginAccounts);
                    }
                    catch (Exception ex) {
                        SPUtility.TransferToErrorPage(SPHttpUtility.UrlKeyValueDecode("Cannot find the manager of " + SPContext.Current.Web.CurrentUser.Name));
                    }
                }

                if (isHidden)
                {
                    peSpecificUsesGroup.Enabled = false;
                }

            }
        }

        private DateTime GetDueDate(string formular)
        {
            string[] arr = formular.Split("+ ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            DateTime current = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            if (arr.Length == 1 && arr[0].ToLower() == "[today]")
            {
                
                return current;
            }
            else
            {
                if (arr.Length == 2)
                {
                    int addDay;
                    if (int.TryParse(arr[1], out addDay))
                    {
                        current = current.AddDays(addDay);
                        return current;
                    }
                }
            }

            if (DateTime.TryParse(formular, out current))
            {
                return current;
            }

            return DateTime.MaxValue;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeParams();

            // Optionally, add code here to pre-populate your form fields.
            Guid wfID = new Guid(this.associationGuid);

            SPWorkflowAssociation association = this.workflowList.WorkflowAssociations[wfID];
            if (association == null)
            {
                
                    foreach (SPContentType item in workflowList.ContentTypes)
                    {
                        association = item.WorkflowAssociations[wfID];
                        if(association!= null) break;
                    }
            }

            this.assData = SerializationHelper.DeserializeFromXml<IModel.ApprovalWFAssociationData>(association.AssociationData);
            if (!Page.IsPostBack)
            {
                if (this.assData != null && this.assData.ApproverData.Count > 0)
                {
                    cdcatalog.DataSource = this.assData.ApproverData;
                    cdcatalog.DataBind();
                }
            }
        }

        // This method is called when the user clicks the button to start the workflow.
        private string GetInitiationData()
        {
            IModel.ApprovalWFInitiationData initData = new IModel.ApprovalWFInitiationData();

            foreach (RepeaterItem item in this.cdcatalog.Items)
            {
                PeopleEditor peSpecificUsesGroup = item.FindControl("peSpecificUsesGroup") as PeopleEditor;
                TextBox txtDueDate = item.FindControl("txtDueDate") as TextBox;
                TextBox txtDuration = item.FindControl("txtDuration") as TextBox;
                EncodedLiteral level = item.FindControl("targetAppAdminDescription") as EncodedLiteral;
                TextBox txtMessage = item.FindControl("txtMessage") as TextBox;
                TextBox txtMessageTitle = item.FindControl("txtMessageTitle") as TextBox;
                InputFormTextBox rtfMessage = item.FindControl("rtfMessage") as InputFormTextBox;

                IModel.ApprovalLevelInfo objInitiation = new IModel.ApprovalLevelInfo();
                objInitiation.LevelName = level.Text;
                foreach (PickerEntity entity in peSpecificUsesGroup.ResolvedEntities)
                {
                    objInitiation.SpecificUserGroup.Add(entity.Key);
                }
                if (peSpecificUsesGroup.ResolvedEntities.Count == 0)
                {
                    objInitiation.SpecificUserGroup = this.assData.ApproverData[item.ItemIndex].SpecificUserGroup;
                }

                objInitiation.DueDate = String.IsNullOrEmpty(txtDueDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtDueDate.Text);
                objInitiation.DurationPerTask = String.IsNullOrEmpty(txtDuration.Text) ? 0 : int.Parse(txtDuration.Text);
                objInitiation.TaskInstruction = txtMessage.Text.Trim();
                objInitiation.Message = rtfMessage.Text;
                objInitiation.MessageTitle = txtMessageTitle.Text.Trim();
                objInitiation.TaskContenType = this.assData.ApproverData[item.ItemIndex].TaskContenType;
                objInitiation.TaskTitle = this.assData.ApproverData[item.ItemIndex].TaskTitle;
                objInitiation.AppendTitle = this.assData.ApproverData[item.ItemIndex].AppendTitle;
                objInitiation.TaskSequenceType = this.assData.ApproverData[item.ItemIndex].TaskSequenceType;

                objInitiation.ExpendGroup = this.assData.ApproverData[item.ItemIndex].ExpendGroup;
                objInitiation.EnableEmail = this.assData.ApproverData[item.ItemIndex].EnableEmail;
                objInitiation.EmailTemplate = this.assData.ApproverData[item.ItemIndex].EmailTemplate;
                objInitiation.AllowChangeMessage = this.assData.ApproverData[item.ItemIndex].AllowChangeMessage;
                objInitiation.UpdatedProperties = this.assData.ApproverData[item.ItemIndex].UpdateProperties;
                objInitiation.TaskEvents = this.assData.ApproverData[item.ItemIndex].TaskEvents;
                objInitiation.ApproverSetting = this.assData.ApproverData[item.ItemIndex];
                initData.ApprovalLevels.Add(objInitiation);
            }

            string dataSer = SerializationHelper.SerializeToXml<IModel.ApprovalWFInitiationData>(initData);
            return dataSer;
        }

        protected void StartWorkflow_Click(object sender, EventArgs e)
        {
            // Optionally, add code here to perform additional steps before starting your workflow
            try
            {
                HandleStartWorkflow();
            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(SPHttpUtility.UrlKeyValueEncode("Failed to Start Workflow"));
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect("Workflow.aspx", SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current, Page.ClientQueryString);
        }

        #region Workflow Initiation Code - Typically, the following code should not be changed

        private string associationGuid;
        private SPList workflowList;
        private SPListItem workflowListItem;

        private void InitializeParams()
        {
            try
            {
                this.associationGuid = Request.Params["TemplateID"];

                // Parameters 'List' and 'ID' will be null for site workflows.
                if (!String.IsNullOrEmpty(Request.Params["List"]) && !String.IsNullOrEmpty(Request.Params["ID"]))
                {
                    this.workflowList = this.Web.Lists[new Guid(Request.Params["List"])];
                    this.workflowListItem = this.workflowList.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                }
            }
            catch (Exception)
            {
                SPUtility.TransferToErrorPage(SPHttpUtility.UrlKeyValueEncode("Failed to read Request Parameters"));
            }
        }

        private void HandleStartWorkflow()
        {
            if (this.workflowList != null && this.workflowListItem != null)
            {
                StartListWorkflow();
            }
            else
            {
                StartSiteWorkflow();
            }
        }

        private void StartSiteWorkflow()
        {
            SPWorkflowAssociation association = this.Web.WorkflowAssociations[new Guid(this.associationGuid)];
            this.Web.Site.WorkflowManager.StartWorkflow((object)null, association, GetInitiationData(), SPWorkflowRunOptions.Synchronous);
            SPUtility.Redirect(this.Web.Url, SPRedirectFlags.UseSource, HttpContext.Current);
        }

        private void StartListWorkflow()
        {
            SPWorkflowAssociation association = this.workflowList.WorkflowAssociations[new Guid(this.associationGuid)];
            this.Web.Site.WorkflowManager.StartWorkflow(workflowListItem, association, GetInitiationData());
            SPUtility.Redirect(this.workflowList.DefaultViewUrl, SPRedirectFlags.UseSource, HttpContext.Current);
        }
        #endregion
    }
}