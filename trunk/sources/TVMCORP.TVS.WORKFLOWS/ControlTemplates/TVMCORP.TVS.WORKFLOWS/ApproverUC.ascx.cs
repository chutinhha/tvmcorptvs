using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Linq;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using System.Collections.Generic;
using TVMCORP.TVS.WORKFLOWS.Controls;
using TVMCORP.TVS.UTIL.MODELS;


using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.RESOURCES;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Core.Controls
{
    public partial class ApproverUC : UserControl
    {
        protected EmailTemplateSelector NotifyEmail
        {
            get { return notifyEmail as EmailTemplateSelector; }
        }

        protected override void OnInit(EventArgs e)
        {
            LoadApproverType();
            rblApproverType.SelectedIndexChanged += new EventHandler(rblApproverType_SelectedIndexChanged);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_uniqueID.Value))
                {
                    _uniqueID.Value = Guid.NewGuid().ToString();
                }
                this.LoadMetaData();
                this.LoadContentType();
                this.LoadListColumn();
            }
            catch (Exception)
            {
                
            }

        }
        private void LoadListColumn()
        {
            var fielsd = SPContext.Current.List.Fields.Cast<SPField>().Where(p=>!p.IsSystemField()&& !p.Hidden);
            foreach (var item in fielsd)
	        {
                MultiLookupPicker.AddItem(item.Id.ToString(), item.Title, item.Description, string.Empty);
                		 
	        }
        }
        private void LoadApproverType()
        {

            rblApproverType.Items.Add(new ListItem()
            {
                Text = ApprovalWorkflowResources.RadioListApproverType_Metadata,
                Value = ApprovalWorkflowResources.RadioListApproverType_Metadata,
                Selected = true
            });
            rblApproverType.Items.Add(new ListItem()
            {
                Text = ApprovalWorkflowResources.RadioListApproverType_SpecifyUserGroup,
                Value = ApprovalWorkflowResources.RadioListApproverType_SpecifyUserGroup
            });
            rblApproverType.Items.Add(new ListItem()
            {
                Text = ApprovalWorkflowResources.RadioListApproverType_Manager,
                Value = ApprovalWorkflowResources.RadioListApproverType_Manager
            });

        }

        private void LoadMetaData()
        {
            var cts = SPContext.Current.List.Fields.Cast<SPField>().Where(p => p.Type == SPFieldType.User).ToList();

            cboMetadata.DataSource = cts;
            cboMetadata.DataTextField = "Title";
            cboMetadata.DataValueField = "Id";

            cboMetadata.DataBind();
        }

        private void LoadContentType()
        {
            SPContentTypeId approvalWFTask = new SPContentTypeId(TMVCorpContentType.APPROVAL_WF_TASK);
            SPContentTypeId iOfficeApprovalWFTask = new SPContentTypeId(TMVCorpContentType.IOFFICE_APPROVAL_TASK);

            var cts = SPContext.Current.Web.AvailableContentTypes.Cast<SPContentType>()
                        .Where(p => p.Id.IsChildOf(approvalWFTask) ||
                                    p.Id.IsChildOf(iOfficeApprovalWFTask) ||
                                    p.Id.CompareTo(approvalWFTask) == 0 ||
                                    p.Id.CompareTo(iOfficeApprovalWFTask) == 0).ToList();
            
            ddlContentType.DataSource = cts;
            ddlContentType.DataTextField = "Name";
            ddlContentType.DataValueField = "Id";

            ddlContentType.DataBind();
        }

        public ApprovalWFApprover ApproverData
        {
            get
            {
                ApprovalWFApprover objApprovalWFApprover = new ApprovalWFApprover()
                {

                    ApprovalLevelName = txtAppLevelName.Text,

                    ExpendGroup = chkExpandGroup.Checked,
                    TaskSequenceType = rblTaskSequenceType.SelectedValue,
                    DueDate = txtDueDate.Text,
                    DurationPerTask = String.IsNullOrEmpty(txtDurationPerTask.Text)? 0 : double.Parse(txtDurationPerTask.Text),
                    EnableEmail = chkEnableEmail.Checked,
                    EnableChangeApprovers = chkEnableChangeApprovers.Checked,
                    EmailTemplate = NotifyEmail.Value,
                    //EmailTemplate = txtEmailTempUrl.Text,
                    //TemplateName = cboTemplateName.SelectedValue,
                    AllowChangeMessage = chkAllowChangeMessage.Checked,
                    AppendTitle = chkAppendTitle.Checked,
                    TaskContenType = this.ddlContentType.SelectedValue,
                    TaskTitle = this.txtTaskTitle.Text,
                    
                    TaskEvents = Session[_uniqueID.Value] as TaskEventSettings,
                };

                if (rblApproverType.SelectedValue == ApprovalWorkflowResources.RadioListApproverType_SpecifyUserGroup)
                {
                    foreach (PickerEntity entity in peSpecificUsesGroup.ResolvedEntities)
                    {
                        objApprovalWFApprover.SpecificUserGroup.Add(entity.Key);
                    };
                }
                else if (rblApproverType.SelectedValue == ApprovalWorkflowResources.RadioListApproverType_Manager)
                {
                    objApprovalWFApprover.ManagerApprove = true;
                }
                else if (rblApproverType.SelectedValue == ApprovalWorkflowResources.RadioListApproverType_Metadata)
                {
                    objApprovalWFApprover.ColumnName = cboMetadata.SelectedValue;
                }

                objApprovalWFApprover.UpdateProperties = MultiLookupPicker.SelectedIds.Cast<string>().ToList(); 
                //string strReturn = SerializationHelper.SerializeToXml<ApprovalWFApprover>(objApprovalWFApprover);
                Session[_uniqueID.Value] = null;
                return objApprovalWFApprover;
            }
            set
            {
                if (value.TaskEvents != null && Session[_uniqueID.Value] == null)
                {
                    _uniqueID.Value = Guid.NewGuid().ToString();
                    Session[_uniqueID.Value] = value.TaskEvents;
                }

                
                //ApprovalWFApprover objApprover = SerializationHelper.DeserializeFromXml<ApprovalWFApprover>(value);
                ApprovalWFApprover objApprover = value;


                txtAppLevelName.Text = objApprover.ApprovalLevelName;
                
                chkExpandGroup.Checked = objApprover.ExpendGroup;
                rblTaskSequenceType.SelectedValue = objApprover.TaskSequenceType;
                txtDueDate.Text = String.IsNullOrEmpty(objApprover.DueDate.ToString()) ? String.Empty : objApprover.DueDate.ToString();
                txtDurationPerTask.Text = String.IsNullOrEmpty(objApprover.DurationPerTask.ToString()) ? String.Empty : objApprover.DurationPerTask.ToString();
                chkEnableEmail.Checked = objApprover.EnableEmail;
                chkEnableChangeApprovers.Checked = objApprover.EnableChangeApprovers;
                chkAppendTitle.Checked = objApprover.AppendTitle;
                //cboTemplateName.SelectedValue = objApprover.TemplateName;
                NotifyEmail.Value = objApprover.EmailTemplate;
                chkAllowChangeMessage.Checked = objApprover.AllowChangeMessage;
                txtTaskTitle.Text = objApprover.TaskTitle;
                this.ddlContentType.SelectedIndex = this.ddlContentType.Items.IndexOf(
                                                        this.ddlContentType.Items.FindByValue(objApprover.TaskContenType));

                if (!string.IsNullOrEmpty(objApprover.ColumnName))
                {
                    rblApproverType.SelectedValue = ApprovalWorkflowResources.RadioListApproverType_Metadata;
                    cboMetadata.SelectedValue = objApprover.ColumnName;
                }
                else if (objApprover.SpecificUserGroup.Count > 0)
                {
                    rblApproverType.SelectedValue = ApprovalWorkflowResources.RadioListApproverType_SpecifyUserGroup;

                    ArrayList arrApprovers = new ArrayList();
                    foreach (string loginName in objApprover.SpecificUserGroup)
                    {
                        PickerEntity pe = new PickerEntity();
                        pe.Key = loginName;
                        pe = peSpecificUsesGroup.ValidateEntity(pe);
                        arrApprovers.Add(pe);
                    };

                    peSpecificUsesGroup.UpdateEntities(arrApprovers);
                }
                else if (objApprover.ManagerApprove)
                {
                    rblApproverType.SelectedValue = ApprovalWorkflowResources.RadioListApproverType_Manager;
                }

                if (objApprover.UpdateProperties != null)
                {
                    var fields = SPContext.Current.List.Fields;
                    foreach (var item in objApprover.UpdateProperties)
	                {
		                if(fields.ContainFieldId(new Guid(item))){
                            var field = fields[new Guid(item)];

                            MultiLookupPicker.AddSelectedItem(field.Id.ToString(), field.Title);
                        }
	                }
                    
                }

                ShowHideControlsDependOnApprover();
            }
        }

        private void rblApproverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideControlsDependOnApprover();
        }

        private void ShowHideControlsDependOnApprover()
        {
            if (rblApproverType.SelectedValue == ApprovalWorkflowResources.RadioListApproverType_SpecifyUserGroup)
            {
                //if (cboMetadata.Items.Count > 0) cboMetadata.SelectedIndex = 0;

                peSpecificUsesGroup.Visible = true;
                cboMetadata.Visible = false;
                lblManager.Visible = false;

            }
            else if (rblApproverType.SelectedValue == ApprovalWorkflowResources.RadioListApproverType_Manager)
            {
                //if (cboMetadata.Items.Count > 0) cboMetadata.SelectedIndex = 0;
                //peSpecificUsesGroup.CommaSeparatedAccounts = "";

                peSpecificUsesGroup.Visible = false;
                cboMetadata.Visible = false;
                lblManager.Visible = true;
            }
            else if (rblApproverType.SelectedValue == ApprovalWorkflowResources.RadioListApproverType_Metadata)
            {
                //peSpecificUsesGroup.CommaSeparatedAccounts = "";

                peSpecificUsesGroup.Visible = false;
                cboMetadata.Visible = true;
                lblManager.Visible = false;
            }
        }
    }
}
