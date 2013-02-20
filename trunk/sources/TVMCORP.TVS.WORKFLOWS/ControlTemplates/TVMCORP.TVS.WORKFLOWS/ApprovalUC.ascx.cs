using System;
using System.Linq;
using TVMCORP.TVS.UTIL.Extensions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.UTIL.Helpers;
using System.Collections.Generic;
using System.Collections;
using TVMCORP.TVS.WORKFLOWS.Controls;
using TVMCORP.TVS.UTIL.Extensions;

using Microsoft.SharePoint;

namespace TVMCORP.TVS.WORKFLOWS.Controls
{
    public partial class ApprovalUC : UserControl
    {
        
        private List<string> approverControls;
        public Guid ListID { get; set; }

        public List<string> ApproverControls
        {
            get
            {
                approverControls = this.ViewState["ApproverControls"] as List<string>;

                if (approverControls == null)
                {
                    approverControls = new List<string>();
                    approverControls.Add("ucApproverUC_1");
                    this.ViewState["ApproverControls"] = approverControls;
                }

                return approverControls;
            }
            set
            {
                this.ViewState["ApproverControls"] = value;
            }
        }

        protected EmailTemplateSelector NotifyEmail
        {
            get { return notifyEmail as EmailTemplateSelector; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (FirstLoad)
            {
                
                var list = SPContext.Current.Web.Lists.GetList(ListID, true);
                if (list != null)
                {
                    var fields = list.Fields.Cast<SPField>().Where(p => !p.IsSystemField() && p.Hidden == false).ToList();
                    ddlFields.DataSource = fields;
                    ddlFields.DataTextField = "Title";
                    ddlFields.DataValueField = "ID";
                    ddlFields.DataBind();
                }
                //LoadPermissionData();

            }

            if (this.ApproverControls == null || this.isReloadWorkflow == true) return;
            foreach (var item in ApproverControls)
            {
                AddApproverControl(item);
            }

            //Load field
          
        }

        private void LoadPermissionData()
        {
            var datasource = SPContext.Current.Web.RoleDefinitions.Cast<SPRoleDefinition>().ToList();


            rptPermissions.DataSource = datasource;
            rptPermissions.ItemDataBound += new RepeaterItemEventHandler(rptPermissions_ItemDataBound);
            rptPermissions.DataBind();
        }

        void rptPermissions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                var data = e.Item.DataItem as SPRoleDefinition;

                var ltrPermissionName = e.Item.FindControl("ltrPermissionName") as Literal;
                 ltrPermissionName.Text = data.Name;

                 var MultiLookupPicker = e.Item.FindControl("MultiLookupPicker") as GroupedItemPicker;
                 var chkAuthor = e.Item.FindControl("chkAuthor") as CheckBox;
                 var chkApprovers = e.Item.FindControl("chkApprovers") as CheckBox;



                 var SelectCandidate = e.Item.FindControl("SelectCandidate") as SPHtmlSelect;
                 var SelectResult = e.Item.FindControl("SelectResult") as SPHtmlSelect;

                MultiLookupPicker.CandidateControlId = SelectCandidate.ID;
                MultiLookupPicker.ResultControlId = SelectResult.ID;

                var fields = SPContext.Current.List.Fields.Cast<SPField>()
                                                          .Where(p => !p.Hidden && (p.Type == SPFieldType.User || p.TypeAsString == ""))
                                                          .ToList();

                foreach (var f in fields)
                {
                    MultiLookupPicker.AddItem(f.Id.ToString(), f.Title, "", "");
                }
            }

        }
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
        }

        List<ApproverUC> myApproverUC = new List<ApproverUC>();
        private void AddApproverControl(string item)
        {
            ApproverUC ucApproverUC = (ApproverUC)this.LoadControl("~/_controltemplates/TVMCORP.TVS.WORKFLOWS/ApproverUC.ascx");
            ucApproverUC.ID = item;

            phUserInfoBox.Controls.Add(ucApproverUC);
            myApproverUC.Add(ucApproverUC);
            var current = this.ApproverControls;
            if (!current.Contains(item)) current.Add(item);

            this.ApproverControls = current;
        }

        private void AddApproverControl(string item, ApprovalWFApprover objApprover)
        {
            ApproverUC ucApproverUC = (ApproverUC)this.LoadControl("~/_controltemplates/TVMCORP.TVS.WORKFLOWS/ApproverUC.ascx");
            ucApproverUC.ID = item;
           

            phUserInfoBox.Controls.Add(ucApproverUC);
            ucApproverUC.ApproverData = objApprover;
            var current = this.ApproverControls;
            if (!current.Contains(item)) current.Add(item);

            this.ApproverControls = current;
        }


        protected override object SaveViewState()
        {
            return base.SaveViewState();
        }

        private bool isReloadWorkflow = false;
        public ApprovalWFAssociationData AssociationForm
        {
            get
            {
                
                var taskEvents = Session[_uniqueID.Value] as TaskEventSettings;
                ApprovalWFAssociationData objAssData = new ApprovalWFAssociationData()
                {
                    EndOnFirstReject = chkTeminateOpt.Items[0].Selected,
                    EndOnItemDocumentChange = chkTeminateOpt.Items[1].Selected,
                    EnableContentApproval = chkTeminateOpt.Items[2].Selected,

                    DelayOnStart = String.IsNullOrEmpty(txtDelayOnStart.Text) ? 0 : int.Parse(txtDelayOnStart.Text),
                    StartNotification = chkStartingNofication.Checked,
                    EmailTemplate = NotifyEmail.Value,
                    UseMetaData = rdgUseMetadata.Checked,
                    ColumnName = cboUseMetaData.SelectedValue,
                    UseSpecificUserGroup = rdgSpecificUser.Checked,
                    EnableStartingCondition = chkEnableExectureCondition.Checked,
                    ConditionFieldId = ddlFields.SelectedValue,
                    ConditionFieldValue = txtConditionValue.Text,
                    ApproveIfByPass = chkApproveIfByPass.Checked,
                    EnableUpdatePermission = chkEnableUpdatePermission.Checked,
                    KeepCurrentPermissions = chkKeepPermissions.Checked,
                    WFEvents = taskEvents,
                    TaskFormOption = new TaskFormOption()
                    {
                        ApproveLabel = txtApproveLable.Text,
                        RejectLabel = txtRejectLabel.Text,
                        OnHoldLabel = txtOnHold.Text,
                        RequestChangeLabel = txtRequestChangeLabel.Text,
                        RequestInformationLabel = txtRequestInfLabel.Text,
                        ReassignLabel = txtReassignLabel.Text,
                        EnableHoldOn = chkEnableHoldOn.Checked,
                        EnableReassign = chkEnableReassign.Checked,
                        EnableRequestChange = chkEnableRequestChange.Checked,
                        EnableRequestInf = chkRequestInfomrmation.Checked,
                        
                    },
                    Permissions = GetPermissions(),
                };

                foreach (ApproverUC entity in myApproverUC)
                {
                    //if (objAssData.ApprovalLevels == null) objAssData.ApprovalLevels = new List<ApprovalWFApprover>();
                    //{
                    objAssData.ApproverData.Add(entity.ApproverData);
                    //}
                }

                foreach (PickerEntity entity in peSpecificUsesGroup.ResolvedEntities)
                {
                    objAssData.SpecificUserGroup.Add(entity.Key);
                }
                Session[_uniqueID.Value] = null;
                return objAssData;
                    
            }
            set
            {
                if(string.IsNullOrEmpty(_uniqueID.Value))
                _uniqueID.Value = Guid.NewGuid().ToString();
                isReloadWorkflow = true;
                myApproverUC = new List<ApproverUC>();

                if (value.WFEvents != null && Session[_uniqueID.Value]==null)
                {
                    Session[_uniqueID.Value] =value.WFEvents;
                }
                phUserInfoBox.Controls.Clear();
                ApprovalWFAssociationData objAssociationData = value;
                chkTeminateOpt.Items[0].Selected = objAssociationData.EndOnFirstReject;
                chkTeminateOpt.Items[1].Selected = objAssociationData.EndOnItemDocumentChange;
                chkTeminateOpt.Items[2].Selected = objAssociationData.EnableContentApproval;

                txtDelayOnStart.Text = objAssociationData.DelayOnStart.ToString();
                chkStartingNofication.Checked = objAssociationData.StartNotification;
                NotifyEmail.Value = objAssociationData.EmailTemplate;
                rdgUseMetadata.Checked = objAssociationData.UseMetaData;
                cboUseMetaData.SelectedValue = objAssociationData.ColumnName;
                rdgSpecificUser.Checked = objAssociationData.UseSpecificUserGroup;

                chkEnableHoldOn.Checked = objAssociationData.TaskFormOption.EnableHoldOn;
                chkEnableReassign.Checked = objAssociationData.TaskFormOption.EnableReassign;

                chkEnableRequestChange.Checked = objAssociationData.TaskFormOption.EnableRequestChange;
                chkRequestInfomrmation.Checked = objAssociationData.TaskFormOption.EnableRequestInf;
                txtApproveLable.Text = objAssociationData.TaskFormOption.ApproveLabel;
                txtRejectLabel.Text = objAssociationData.TaskFormOption.RejectLabel;
                txtReassignLabel.Text = objAssociationData.TaskFormOption.ReassignLabel;
                txtRequestInfLabel.Text = objAssociationData.TaskFormOption.RequestInformationLabel;
                txtRequestChangeLabel.Text = objAssociationData.TaskFormOption.RequestChangeLabel;
                txtOnHold.Text = objAssociationData.TaskFormOption.OnHoldLabel;
                chkEnableUpdatePermission.Checked = objAssociationData.EnableUpdatePermission;
                chkKeepPermissions.Checked = objAssociationData.KeepCurrentPermissions;
                chkEnableExectureCondition.Checked = objAssociationData.EnableStartingCondition;
                chkApproveIfByPass.Checked = objAssociationData.ApproveIfByPass;
                if (!string.IsNullOrEmpty(objAssociationData.ConditionFieldId))
                {
                    ddlFields.SelectedValue= objAssociationData.ConditionFieldId;
                    txtConditionValue.Text = objAssociationData.ConditionFieldValue;
                }

                foreach (ApprovalWFApprover strApprover in objAssociationData.ApproverData)
                {
                    AddApproverControl(getNameApproverUC(), strApprover);
                }

                if (objAssociationData.SpecificUserGroup.Count > 0)
                {
                    ArrayList arrApprovers = new ArrayList(objAssociationData.SpecificUserGroup);
                    //peSpecificUsesGroup.UpdateEntities(arrApprovers);
                    BindPeoplePicker(peSpecificUsesGroup, arrApprovers.Cast<string>().ToList());
                }
                LoadPermissionData();

                foreach (RepeaterItem item in rptPermissions.Items)
                {
                        var data = item.DataItem as SPRoleDefinition;

                        var ltrPermissionName = item.FindControl("ltrPermissionName") as Literal;
                        string name = ltrPermissionName.Text;
                        var update = value.Permissions.FirstOrDefault(p => p.Name == name);
                        if (update == null) continue;

                        var MultiLookupPicker = item.FindControl("MultiLookupPicker") as GroupedItemPicker;
                        var chkAuthor = item.FindControl("chkAuthor") as CheckBox;
                        var chkApprovers = item.FindControl("chkApprovers") as CheckBox;

                        chkAuthor.Checked = update.Ower;
                        chkApprovers.Checked = update.Approvers ;
                        foreach (var col in update.Columns)
                        {
                            MultiLookupPicker.AddSelectedItem(col.ToString() ,SPContext.Current.List.Fields[col].Title);
                        }
                        
                        
                }
            }

        }

        private List<PermissionUpdate> GetPermissions()
        {
            List<PermissionUpdate> permissions = new List<PermissionUpdate>();
            foreach (RepeaterItem item in rptPermissions.Items)
            {
                PermissionUpdate update = new PermissionUpdate();

                var data = item.DataItem as SPRoleDefinition;

                var ltrPermissionName = item.FindControl("ltrPermissionName") as Literal;
                update.Name = ltrPermissionName.Text;

                var MultiLookupPicker = item.FindControl("MultiLookupPicker") as GroupedItemPicker;
                var chkAuthor = item.FindControl("chkAuthor") as CheckBox;
                var chkApprovers = item.FindControl("chkApprovers") as CheckBox;

                update.Ower = chkAuthor.Checked;
                update.Approvers = chkApprovers.Checked;
                update.Columns = MultiLookupPicker.SelectedIds.Cast<string>().Select(p => new Guid(p)).ToList();

                var SelectCandidate = item.FindControl("SelectCandidate") as SPHtmlSelect;
                var SelectResult = item.FindControl("SelectResult") as SPHtmlSelect;

                MultiLookupPicker.CandidateControlId = SelectCandidate.ID;
                MultiLookupPicker.ResultControlId = SelectResult.ID;

                //var fields = SPContext.Current.List.Fields.Cast<SPField>()
                //                                          .Where(p => !p.Hidden && (p.Type == SPFieldType.User || p.TypeAsString == ""))
                //                                          .ToList();

                //foreach (var f in fields)
                //{
                //    MultiLookupPicker.AddItem(f.Id.ToString(), f.Title, "", "");
                //}
                permissions.Add(update);
            }
            return permissions;
        }

        private void BindPeoplePicker(PeopleEditor ppEditor, List<string> list)
        {
            string users = "";
            System.Collections.ArrayList entityArrayList = new System.Collections.ArrayList();
            PickerEntity entity = new PickerEntity();
            foreach (var item in list)
            {
                entity.Key = item;
                // this can be omitted if you're sure of what you are doing
                entity = ppEditor.ValidateEntity(entity);
                entityArrayList.Add(entity);
                users += item + ",";
            }

            //ppEditor.UpdateEntities(entityArrayList);
            if (list.Count > 0)
                ppEditor.CommaSeparatedAccounts = users.Remove(users.LastIndexOf(","), 1);
        }

        private string getNameApproverUC()
        {
            return "ucApproverUC_" + (phUserInfoBox.Controls.Count + 1).ToString();
        }
        protected void btnAddApprover_Click(object sender, EventArgs e)
        {
            //ApproverUC ucApproverUC = (ApproverUC)this.LoadControl("~/_controltemplates/TVMCORP.TVS.WORKFLOWS/ApproverUC.ascx");
            AddApproverControl(getNameApproverUC());
            //phUserInfoBox.Controls.Add(ucApproverUC);

        }

        protected void rdgUseMetadata_CheckedChanged(object sender, EventArgs e)
        {
            if (rdgUseMetadata.Checked)
            {
                rdgSpecificUser.Checked = false;
                peSpecificUsesGroup.CommaSeparatedAccounts = null;
            }
        }

        protected void rdgSpecificUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rdgSpecificUser.Checked)
            {
                rdgUseMetadata.Checked = false;
                cboUseMetaData.SelectedValue = null;
            }
        }

        public static bool FirstLoad { get; set; }
    }

}
