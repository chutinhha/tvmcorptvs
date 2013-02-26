using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Models;
using Microsoft.SharePoint.WebControls;

namespace TVMCORP.TVS.ControlTemplates.TVMCORP.TVS
{
    public partial class PurchaseEditForm : UserControl
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
            linkButtonAdd.Click += new EventHandler(AddPurchaseDetail);
            repeaterPurchaseDetail.ItemDataBound +=new RepeaterItemEventHandler(repeaterPurchaseDetail_ItemDataBound);
            btnSave.Click += new EventHandler(btnSave_Click);
            rdbTypeOfApproval1.AutoPostBack = true;
            rdbTypeOfApproval2.AutoPostBack = true;
            rdbTypeOfApproval1.CheckedChanged += new EventHandler(ChangeApprovalSettings);
            rdbTypeOfApproval2.CheckedChanged += new EventHandler(ChangeApprovalSettings);
            //
            InitData();
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

            UpdatePurchase();

            this.Page.Response.Clear();
            this.Page.Response.Write(
            string.Format(System.Globalization.CultureInfo.InvariantCulture, @"<script type='text/javascript'> window.frameElement.commonModalDialogClose(1, '{0}');</script>", ""));
            this.Page.Response.End();
        }

        void AddPurchaseDetail(object sender, EventArgs e)
        {
            DataTable dataTable = MakePurchaseDetail();
            repeaterPurchaseDetail.DataSource = dataTable;
            repeaterPurchaseDetail.DataBind();
        }

        void repeaterPurchaseDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;
            if (rowView != null)
            {
                HiddenField hiddenFieldId = e.Item.FindControl("hiddenFieldId") as HiddenField;
                hiddenFieldId.Value = rowView["ID"].ToString();

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

                if (SPContext.Current.ListItem["Status"] != null && SPContext.Current.ListItem["Status"].ToString() == "0")
                {
                    txtProductName.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtPrice.Enabled = false;
                    txtDescription.Enabled = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load approval
                string typeOfApproval = SPContext.Current.ListItem["TypeOfApproval"].ToString();
                LoadApprovalSettings(typeOfApproval);
                hiddenTypeOfApproval.Value = typeOfApproval;
                //Load purchase detail
                DataTable purchaseDetail = LoadPurchaseDetail();
                repeaterPurchaseDetail.DataSource = purchaseDetail;
                repeaterPurchaseDetail.DataBind();
            }
        }

        #region Private Functions

        private void UpdatePurchase()
        {
            SPFieldLookupValueCollection purchaseDetails = new SPFieldLookupValueCollection();
            var purchaseDetailList = Utility.GetListFromURL(Constants.PURCHASE_DETAIL_LIST_URL, SPContext.Current.Web);
            foreach (RepeaterItem purchaseDetail in repeaterPurchaseDetail.Items)
            {
                HiddenField hiddenFieldId = purchaseDetail.FindControl("hiddenFieldId") as HiddenField;
                TextBox txtProductName = purchaseDetail.FindControl("txtProductName") as TextBox;
                TextBox txtQuantity = purchaseDetail.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = purchaseDetail.FindControl("txtPrice") as TextBox;
                TextBox txtDescription = purchaseDetail.FindControl("txtDescription") as TextBox;

                if (!string.IsNullOrEmpty(hiddenFieldId.Value))
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text))
                    {
                        var spFieldLookupValue = UpdatePurchaseDetail(purchaseDetailList, int.Parse(hiddenFieldId.Value), txtProductName.Text, txtQuantity.Text, txtPrice.Text, txtDescription.Text);
                        purchaseDetails.Add(spFieldLookupValue);
                    }
                    else
                    {
                        SPListItem purchaseDetailItem = purchaseDetailList.GetItemById(int.Parse(hiddenFieldId.Value));
                        purchaseDetailItem.Delete();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text))
                    {
                        SPListItem purchaseDetailItem = purchaseDetailList.AddItem();
                        purchaseDetailItem[SPBuiltInFieldId.Title] = txtProductName.Text;
                        purchaseDetailItem["Quantity"] = txtQuantity.Text;
                        purchaseDetailItem["Price"] = txtPrice.Text;
                        purchaseDetailItem["Description"] = txtDescription.Text;
                        purchaseDetailItem.Update();
                        SPFieldLookupValue spFieldLookupValue = new SPFieldLookupValue(purchaseDetailItem.ID, purchaseDetailItem.Title);
                        purchaseDetails.Add(spFieldLookupValue);
                    }
                }
            }

            SPContext.Current.ListItem["TypeOfApproval"] = hiddenTypeOfApproval.Value;
            SPContext.Current.ListItem["PurchaseDetail"] = purchaseDetails;
            if (peChief.Enabled)
                SPContext.Current.ListItem["Chief"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peChief.ResolvedEntities[0]).Key); //ffChief.Value; //

            if (peBuyer.Enabled)
                SPContext.Current.ListItem["Buyer"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peBuyer.ResolvedEntities[0]).Key); //ffBuyer.Value; //

            if (peApprover.Enabled)
                SPContext.Current.ListItem["Approver"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peApprover.ResolvedEntities[0]).Key); //ffApprover.Value; //

            if (peAccountant.Enabled)
                SPContext.Current.ListItem["Accountant"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peAccountant.ResolvedEntities[0]).Key); //ffAccountant.Value; //

            if (peConfirmer.Enabled)
                SPContext.Current.ListItem["Confirmer"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peConfirmer.ResolvedEntities[0]).Key); //ffConfirmer.Value; //

            SPContext.Current.ListItem.Update();
        }

        private SPFieldLookupValue UpdatePurchaseDetail(SPList purchaseDetailList, int id, string productName, string quantity, string price, string description)
        {
            try
            {
                SPFieldLookupValue spFieldLookupValue = null;
                SPListItem purchaseDetailItem = purchaseDetailList.GetItemById(id);
                if (purchaseDetailItem != null)
                {
                    purchaseDetailItem[SPBuiltInFieldId.Title] = productName;
                    purchaseDetailItem["Quantity"] = quantity;
                    purchaseDetailItem["Price"] = price;
                    purchaseDetailItem["Description"] = description;
                    purchaseDetailItem.Update();
                    spFieldLookupValue = new SPFieldLookupValue(purchaseDetailItem.ID, purchaseDetailItem.Title);
                }
                return spFieldLookupValue;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        private void InitData()
        {
            literalDateRequestValue.Text = Convert.ToDateTime(SPContext.Current.ListItem["DateRequest"].ToString()).ToString("dd/MM/yyyy");
            if (SPContext.Current.ListItem["TypeOfApproval"].ToString() == ApproversGroups.CongNgheThongTin)
            {
                rdbTypeOfApproval1.Checked = true;
            }
            //rdbTypeOfApproval1.Enabled = false;
            //rdbTypeOfApproval2.Enabled = false;
        }

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
                            if (SPContext.Current.ListItem["Chief"] != null)
                            {
                                SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Chief"].ToString());
                                peChief.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                            }
                            else
                                peChief.CommaSeparatedAccounts = setting.TruongBoPhan;

                            if (!setting.AllowToChangeTruongBoPhan && !string.IsNullOrEmpty(setting.TruongBoPhan))
                            {
                                peChief.Enabled = false;
                                //peChief.AllowTypeIn = false;
                            }

                            if (SPContext.Current.ListItem["Buyer"] != null)
                            {
                                SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Buyer"].ToString());
                                peBuyer.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                            }
                            else
                                peBuyer.CommaSeparatedAccounts = setting.NguoiMuaHang;

                            if (!setting.AllowToChangeNguoiMuaHang && !string.IsNullOrEmpty(setting.NguoiMuaHang))
                            {
                                peBuyer.Enabled = false;
                                //peBuyer.AllowTypeIn = false;
                            }

                            if (SPContext.Current.ListItem["Approver"] != null)
                            {
                                SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Approver"].ToString());
                                peApprover.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                            }
                            else
                                peApprover.CommaSeparatedAccounts = setting.NguoiDuyet;

                            if (!setting.AllowToChangeNguoiDuyet && !string.IsNullOrEmpty(setting.NguoiDuyet))
                            {
                                peApprover.Enabled = false;
                                //peApprover.AllowTypeIn = false;
                            }

                            if (SPContext.Current.ListItem["Accountant"] != null)
                            {
                                SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Accountant"].ToString());
                                peAccountant.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                            }
                            else
                                peAccountant.CommaSeparatedAccounts = setting.PhongKeToan;

                            if (!setting.AllowToChangePhongKeToan && !string.IsNullOrEmpty(setting.PhongKeToan))
                            {
                                peAccountant.Enabled = false;
                                //peAccountant.AllowTypeIn = false;
                            }

                            if (SPContext.Current.ListItem["Confirmer"] != null)
                            {
                                SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Confirmer"].ToString());
                                peConfirmer.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                            }
                            else
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

        private DataTable MakePurchaseDetail()
        {
            DataTable dataTable = new DataTable();
            DataColumn[] dataColumn = new DataColumn[]{
                new DataColumn("ID"),
                new DataColumn("ProductName"),
                new DataColumn("Quantity"),
                new DataColumn("Price"),
                new DataColumn("Description")
            };
            dataTable.Columns.AddRange(dataColumn);
            
            foreach (RepeaterItem purchaseDetail in repeaterPurchaseDetail.Items)
            {
                HiddenField hiddenFieldId = purchaseDetail.FindControl("hiddenFieldId") as HiddenField;
                TextBox txtProductName = purchaseDetail.FindControl("txtProductName") as TextBox;
                TextBox txtQuantity = purchaseDetail.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = purchaseDetail.FindControl("txtPrice") as TextBox;
                TextBox txtDescription = purchaseDetail.FindControl("txtDescription") as TextBox;

                DataRow row = dataTable.NewRow();
                row[0] = hiddenFieldId.Value;
                row[1] = txtProductName.Text;
                row[2] = txtQuantity.Text;
                row[3] = txtPrice.Text;
                row[4] = txtDescription.Text;
                dataTable.Rows.Add(row);
            }

            DataRow emptyRow = dataTable.NewRow();
            emptyRow[0] = string.Empty;
            emptyRow[1] = string.Empty;
            emptyRow[2] = string.Empty;
            emptyRow[3] = string.Empty;
            emptyRow[4] = string.Empty;
            dataTable.Rows.Add(emptyRow);
            ViewState["PurchaseDetail"] = dataTable;

            return dataTable;
        }

        private DataTable LoadPurchaseDetail()
        {
            DataTable dataTable = dataTable = new DataTable();
            DataColumn[] dataColumn = new DataColumn[]{
                new DataColumn("ID"),
                new DataColumn("ProductName"),
                new DataColumn("Quantity"),
                new DataColumn("Price"),
                new DataColumn("Description")
            };
            dataTable.Columns.AddRange(dataColumn);

            var purchaseDetailList = Utility.GetListFromURL(Constants.PURCHASE_DETAIL_LIST_URL, SPContext.Current.Web);
            SPFieldLookupValueCollection purchaseDetails = SPContext.Current.ListItem["PurchaseDetail"] as SPFieldLookupValueCollection;
            foreach (var purchaseDetail in purchaseDetails)
            {
                SPListItem listItem = purchaseDetailList.GetItemById(purchaseDetail.LookupId);
                if (listItem != null)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = listItem[SPBuiltInFieldId.ID].ToString();
                    row[1] = listItem[SPBuiltInFieldId.Title].ToString();
                    row[2] = listItem["Quantity"].ToString();
                    row[3] = listItem["Price"].ToString();
                    row[4] = listItem["Description"].ToString();
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        #endregion Private Functions
    }
}
