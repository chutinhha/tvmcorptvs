using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using System.Collections.Generic;
using TVMCORP.TVS.UTIL.Models;
using TVMCORP.TVS.UTIL.Utilities.Camlex;

namespace TVMCORP.TVS.ControlTemplates.TVMCORP.TVS
{
    public partial class PurchaseNewForm : UserControl
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //
            SPRibbon ribbon = SPRibbon.GetCurrent(this.Page);
            if (ribbon != null)
            {
                ribbon.TrimById("Ribbon.ListForm.Edit.Commit");
            }
            //
            repeaterRequestDetail.ItemDataBound += new RepeaterItemEventHandler(repeaterRequestDetail_ItemDataBound);
            linkButtonAdd.Click += new EventHandler(AddRequestDetail);
            btnSave.Click += new EventHandler(btnSave_Click);
            rdbTypeOfApproval1.AutoPostBack = true;
            rdbTypeOfApproval2.AutoPostBack = true;
            rdbTypeOfApproval1.CheckedChanged +=new EventHandler(ChangeApprovalSettings);
            rdbTypeOfApproval2.CheckedChanged += new EventHandler(ChangeApprovalSettings);
            //
            GetUserInfo();
            //PurchaseHelper.GetReferences(purchaseReferences);
        }

        void ChangeApprovalSettings(object sender, EventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad != null)
            {
                if (rad.Checked)
                {
                    hiddenTypeOfApproval.Value = rad.Text;
                    LoadApprovalSettings(rad.Text);
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
                return;

            AddPurchase();

            this.Page.Response.Clear();
            this.Page.Response.Write(
            string.Format(System.Globalization.CultureInfo.InvariantCulture, @"<script type='text/javascript'> window.frameElement.commonModalDialogClose(1, '{0}');</script>", ""));
            this.Page.Response.End();
        }

        void AddRequestDetail(object sender, EventArgs e)
        {
            DataTable dataTable = MakeRequestDetail();
            repeaterRequestDetail.DataSource = dataTable;
            repeaterRequestDetail.DataBind();
        }

        void repeaterRequestDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;
            if (rowView != null)
            {
                Literal literalOrderValue = e.Item.FindControl("literalOrderValue") as Literal;
                literalOrderValue.Text = (e.Item.ItemIndex + 1).ToString();

                TextBox txtProductName = e.Item.FindControl("txtProductName") as TextBox;
                txtProductName.Text = rowView["ProductName"].ToString();

                TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                txtQuantity.Text = rowView["Quantity"].ToString();

                TextBox txtPrice = e.Item.FindControl("txtPrice") as TextBox;
                txtPrice.Text = rowView["Price"].ToString();

                TextBox txtDescription = e.Item.FindControl("txtDescription") as TextBox;
                txtDescription.Text = rowView["Description"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Init data
                rdbTypeOfApproval1.Text = ApproversGroups.HanhChinh;
                rdbTypeOfApproval2.Text = ApproversGroups.CongNgheThongTin;
                hiddenTypeOfApproval.Value = ApproversGroups.HanhChinh;
                LoadApprovalSettings(ApproversGroups.HanhChinh);
                //Load purchase detail
                DataTable RequestDetail = MakeRequestDetail();
                repeaterRequestDetail.DataSource = RequestDetail;
                repeaterRequestDetail.DataBind();
            }
        }

        #region Private Functions

        private void LoadApprovalSettings(string typeOfApprover)
        {
            peChief.Enabled = true;
            peBuyer.Enabled = true;
            peApprover.Enabled = true;
            peAccountant.Enabled = true;
            peConfirmer.Enabled = true;
            ListApproversSettingsCollection settingsCollection = SPContext.Current.List.GetCustomSettings<ListApproversSettingsCollection>(TVMCORPFeatures.TVS);
            if (settingsCollection != null && settingsCollection.Settings != null)
            {
                foreach (var setting in settingsCollection.Settings)
                {
                    if (setting != null)
                    {
                        if (setting.ApproversGroup == typeOfApprover)
                        {
                            peChief.CommaSeparatedAccounts = setting.TruongBoPhan;
                            if (!setting.AllowToChangeTruongBoPhan && !string.IsNullOrEmpty(setting.TruongBoPhan))
                            {
                                peChief.Enabled = false;
                                //peChief.AllowTypeIn = false;
                            }

                            peBuyer.CommaSeparatedAccounts = setting.NguoiMuaHang;
                            if (!setting.AllowToChangeNguoiMuaHang && !string.IsNullOrEmpty(setting.NguoiMuaHang))
                            {
                                peBuyer.Enabled = false;
                                //peBuyer.AllowTypeIn = false;
                            }

                            peApprover.CommaSeparatedAccounts = setting.NguoiDuyet;
                            if (!setting.AllowToChangeNguoiDuyet && !string.IsNullOrEmpty(setting.NguoiDuyet))
                            {
                                peApprover.Enabled = false;
                                //peApprover.AllowTypeIn = false;
                            }

                            peAccountant.CommaSeparatedAccounts = setting.PhongKeToan;
                            if (!setting.AllowToChangePhongKeToan && !string.IsNullOrEmpty(setting.PhongKeToan))
                            {
                                peAccountant.Enabled = false;
                                //peAccountant.AllowTypeIn = false;
                            }

                            peConfirmer.CommaSeparatedAccounts = setting.NguoiXacNhan;
                            if (!setting.AllowToChangeNguoiXacNhan && !string.IsNullOrEmpty(setting.NguoiXacNhan))
                            {
                                peConfirmer.Enabled = false;
                                //peConfirmer.AllowTypeIn = false;
                            }
                        }
                    }
                }
            }
        }

        private string GetUserInfo()
        {
            string output = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPListItemCollection userItems = web.Lists.TryGetList(web.SiteUserInfoList.Title).GetItems();

                            SPListItem userItem = web.Lists.TryGetList(web.SiteUserInfoList.Title).GetItemById(SPContext.Current.Web.CurrentUser.ID);
                            if (userItem != null)
                            {
                               literalDateRequestValue.Text = DateTime.Now.ToString("dd/MM/yyyy");
                               literalUserRequestValue.Text = userItem["Title"].ToString();
                               literalDepartmentRequestValue.Text = userItem["Department"] == null ? string.Empty : userItem["Department"].ToString();
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }

            return output;
        }

        private DataTable MakeRequestDetail()
        {
            DataTable dataTable = null;
            if (ViewState["RequestDetail"] == null)
            {
                dataTable = new DataTable();
                DataColumn[] dataColumn = new DataColumn[]{
                    new DataColumn("ProductName"),
                    new DataColumn("Quantity"),
                    new DataColumn("Price"),
                    new DataColumn("Description")
                };
                dataTable.Columns.AddRange(dataColumn);
            }
            else
            {
                dataTable = ViewState["RequestDetail"] as DataTable;
                dataTable.Clear();

                foreach (RepeaterItem RequestDetail in repeaterRequestDetail.Items)
                {
                    TextBox txtProductName = RequestDetail.FindControl("txtProductName") as TextBox;
                    TextBox txtQuantity = RequestDetail.FindControl("txtQuantity") as TextBox;
                    TextBox txtPrice = RequestDetail.FindControl("txtPrice") as TextBox;
                    TextBox txtDescription = RequestDetail.FindControl("txtDescription") as TextBox;
                    
                    DataRow row = dataTable.NewRow();
                    row[0] = txtProductName.Text;
                    row[1] = txtQuantity.Text;
                    row[2] = txtPrice.Text;
                    row[3] = txtDescription.Text;
                    dataTable.Rows.Add(row);
                }
            }

            DataRow emptyRow = dataTable.NewRow();
            emptyRow[0] = string.Empty;
            emptyRow[1] = string.Empty;
            emptyRow[2] = string.Empty;
            emptyRow[3] = string.Empty;
            dataTable.Rows.Add(emptyRow);
            ViewState["RequestDetail"] = dataTable;

            return dataTable;
        }

        private void AddPurchase()
        {
            SPFieldLookupValueCollection RequestDetails = new SPFieldLookupValueCollection();

            var RequestDetailList = Utility.GetListFromURL(Constants.REQUEST_DETAIL_LIST_URL, SPContext.Current.Web);
            foreach (RepeaterItem RequestDetail in repeaterRequestDetail.Items)
            {
                TextBox txtProductName = RequestDetail.FindControl("txtProductName") as TextBox;
                TextBox txtQuantity = RequestDetail.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = RequestDetail.FindControl("txtPrice") as TextBox;
                TextBox txtDescription = RequestDetail.FindControl("txtDescription") as TextBox;
                if (!string.IsNullOrEmpty(txtProductName.Text))
                {
                    SPListItem RequestDetailItem = RequestDetailList.AddItem();
                    RequestDetailItem[SPBuiltInFieldId.Title] = txtProductName.Text;
                    RequestDetailItem["Quantity"] = txtQuantity.Text.Replace(",", string.Empty);
                    RequestDetailItem["Price"] = txtPrice.Text.Replace(",", string.Empty);
                    RequestDetailItem["Description"] = txtDescription.Text;
                    RequestDetailItem.Update();
                    SPFieldLookupValue spFieldLookupValue = new SPFieldLookupValue(RequestDetailItem.ID, RequestDetailItem.Title);
                    RequestDetails.Add(spFieldLookupValue);
                }
            }

            var purchaseList = Utility.GetListFromURL(Constants.REQUEST_LIST_URL, SPContext.Current.Web);
            SPListItem purchaseItem = SPContext.Current.ListItem;
            purchaseItem[SPBuiltInFieldId.Title] = ffTitle.Value;//Constants.PURCHASE_TITLE_PREFIX + literalUserRequestValue.Text;
            purchaseItem["DateRequest"] = DateTime.Now;
            purchaseItem["UserRequest"] = SPContext.Current.Web.CurrentUser;
            purchaseItem["DepartmentRequest"] = literalDepartmentRequestValue.Text;
            purchaseItem["TypeOfApproval"] = hiddenTypeOfApproval.Value;
            purchaseItem["RequestDetail"] = RequestDetails;
            purchaseItem["References"] = ffReferences.Value; //PurchaseHelper.GetMultipleItemSelectionValues(purchaseReferences);
            if(peChief.IsValid && peChief.ResolvedEntities.Count > 0)
                purchaseItem["Chief"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peChief.ResolvedEntities[0]).Key); //ffChief.Value; //
            if (peBuyer.IsValid && peBuyer.ResolvedEntities.Count > 0)
                purchaseItem["Buyer"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peBuyer.ResolvedEntities[0]).Key); //ffBuyer.Value; //
            if (peApprover.IsValid && peApprover.ResolvedEntities.Count > 0)
                purchaseItem["Approver"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peApprover.ResolvedEntities[0]).Key); //ffApprover.Value; //
            if (peAccountant.IsValid && peAccountant.ResolvedEntities.Count > 0)
                purchaseItem["Accountant"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peAccountant.ResolvedEntities[0]).Key); //ffAccountant.Value; //
            if (peConfirmer.IsValid && peConfirmer.ResolvedEntities.Count > 0)
                purchaseItem["Confirmer"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peConfirmer.ResolvedEntities[0]).Key); //ffConfirmer.Value; //

            SaveButton.SaveItem(SPContext.Current, false, "");
        }

        #endregion Private Functions
    }
}
