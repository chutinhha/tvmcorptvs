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
        private string viewUrl = string.Format(@"javascript:NewItem2(event,'{0}/{1}?ListId={2}{3}');javascript:return false;", SPContext.Current.Web.Url, SPContext.Current.List.Forms[PAGETYPE.PAGE_EDITFORM].Url, SPContext.Current.List.ID, "&ID={0}");

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
            //if (SPContext.Current.ListItem["Status"] != null && 
            //    SPContext.Current.ListItem["Status"].ToString() == Constants.DRAFT_STATUS)
            //{
            //    isLockRequest = false;
            //    btnSave.Click += new EventHandler(btnSave_Click);
            //    linkButtonAdd.Click += new EventHandler(AddRequestDetail);
            //}
            //
            btnSave.Click += new EventHandler(btnSave_Click);
            linkButtonAdd.Click += new EventHandler(AddRequestDetail);
            repeaterRequestDetail.ItemDataBound +=new RepeaterItemEventHandler(repeaterRequestDetail_ItemDataBound);
            //repeaterPurchaseReference.ItemDataBound +=new RepeaterItemEventHandler(repeaterPurchaseReference_ItemDataBound);
            rdbTypeOfApproval1.AutoPostBack = true;
            rdbTypeOfApproval2.AutoPostBack = true;
            rdbTypeOfApproval1.CheckedChanged += new EventHandler(ChangeApprovalSettings);
            rdbTypeOfApproval2.CheckedChanged += new EventHandler(ChangeApprovalSettings);
            //
            InitData();
            //
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

            UpdatePurchase();

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

        void repeaterPurchaseReference_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;
            if (rowView != null)
            {
                Literal literalOrder = e.Item.FindControl("literalOrder") as Literal;
                literalOrder.Text = (e.Item.ItemIndex + 1).ToString();

                LinkButton linkButtonReferenceTitle = e.Item.FindControl("linkButtonReferenceTitle") as LinkButton;
                linkButtonReferenceTitle.Text = rowView["Title"].ToString();
                linkButtonReferenceTitle.OnClientClick = string.Format(viewUrl, rowView["ID"]);

                Literal literalReferenceDate = e.Item.FindControl("literalReferenceDate") as Literal;
                literalReferenceDate.Text = Convert.ToDateTime(rowView["DateRequest"].ToString()).ToString("dd/MM/yyyy");

                Literal literalReferenceUser = e.Item.FindControl("literalReferenceUser") as Literal;
                SPFieldUserValue userValue = new SPFieldUserValue(SPContext.Current.Web, rowView["UserRequest"].ToString());
                literalReferenceUser.Text = userValue.User.Name;

                Literal literalReferenceDepartment = e.Item.FindControl("literalReferenceDepartment") as Literal;
                literalReferenceDepartment.Text = rowView["DepartmentRequest"].ToString();
            }
        }

        void repeaterRequestDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                txtQuantity.Text = string.IsNullOrEmpty(rowView["Quantity"].ToString()) ? string.Empty : Convert.ToDouble(rowView["Quantity"]).ToString("#,###");

                TextBox txtPrice = e.Item.FindControl("txtPrice") as TextBox;
                txtPrice.Text = string.IsNullOrEmpty(rowView["Price"].ToString()) ? string.Empty : Convert.ToDouble(rowView["Price"]).ToString("#,###");

                TextBox txtDescription = e.Item.FindControl("txtDescription") as TextBox;
                txtDescription.Text = rowView["Description"].ToString();

                //if (!isLockRequest)
                //{
                //    txtProductName.Enabled = false;
                //    txtQuantity.Enabled = false;
                //    txtPrice.Enabled = false;
                //    txtDescription.Enabled = false;
                //}
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load purchase detail
                DataTable RequestDetail = LoadRequestDetail();
                repeaterRequestDetail.DataSource = RequestDetail;
                repeaterRequestDetail.DataBind();
                //Load purchase references
                //DataTable purchaseReferences = LoadPurchaseReferences();
                //repeaterPurchaseReference.DataSource = purchaseReferences;
                //repeaterPurchaseReference.DataBind();
            }
        }

        #region Private Functions

        private void UpdatePurchase()
        {
            SPFieldLookupValueCollection RequestDetails = new SPFieldLookupValueCollection();
            var RequestDetailList = Utility.GetListFromURL(Constants.REQUEST_DETAIL_LIST_URL, SPContext.Current.Web);
            foreach (RepeaterItem RequestDetail in repeaterRequestDetail.Items)
            {
                HiddenField hiddenFieldId = RequestDetail.FindControl("hiddenFieldId") as HiddenField;
                TextBox txtProductName = RequestDetail.FindControl("txtProductName") as TextBox;
                TextBox txtQuantity = RequestDetail.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = RequestDetail.FindControl("txtPrice") as TextBox;
                TextBox txtDescription = RequestDetail.FindControl("txtDescription") as TextBox;

                if (!string.IsNullOrEmpty(hiddenFieldId.Value))
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text))
                    {
                        var spFieldLookupValue = UpdateRequestDetail(RequestDetailList, int.Parse(hiddenFieldId.Value), txtProductName.Text, txtQuantity.Text, txtPrice.Text, txtDescription.Text);
                        RequestDetails.Add(spFieldLookupValue);
                    }
                    else
                    {
                        SPListItem RequestDetailItem = RequestDetailList.GetItemById(int.Parse(hiddenFieldId.Value));
                        RequestDetailItem.Delete();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text))
                    {
                        SPListItem RequestDetailItem = RequestDetailList.AddItem();
                        RequestDetailItem[SPBuiltInFieldId.Title] = txtProductName.Text;
                        RequestDetailItem["Quantity"] = txtQuantity.Text;
                        RequestDetailItem["Price"] = txtPrice.Text;
                        RequestDetailItem["Description"] = txtDescription.Text;
                        RequestDetailItem.Update();
                        SPFieldLookupValue spFieldLookupValue = new SPFieldLookupValue(RequestDetailItem.ID, RequestDetailItem.Title);
                        RequestDetails.Add(spFieldLookupValue);
                    }
                }
            }

            SPContext.Current.ListItem[SPBuiltInFieldId.Title] = ffTitle.Value;
            SPContext.Current.ListItem["TypeOfApproval"] = hiddenTypeOfApproval.Value;
            SPContext.Current.ListItem["RequestDetail"] = RequestDetails;
            SPContext.Current.ListItem["References"] = ffReferences.Value;//PurchaseHelper.GetMultipleItemSelectionValues(purchaseReferences);

            if (peChief.IsValid && peChief.ResolvedEntities.Count > 0)
                SPContext.Current.ListItem["Chief"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peChief.ResolvedEntities[0]).Key); //ffChief.Value; //
            if (peBuyer.IsValid && peBuyer.ResolvedEntities.Count > 0)
                SPContext.Current.ListItem["Buyer"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peBuyer.ResolvedEntities[0]).Key); //ffBuyer.Value; //
            if (peApprover.IsValid && peApprover.ResolvedEntities.Count > 0)
                SPContext.Current.ListItem["Approver"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peApprover.ResolvedEntities[0]).Key); //ffApprover.Value; //
            if (peAccountant.IsValid && peAccountant.ResolvedEntities.Count > 0)
                SPContext.Current.ListItem["Accountant"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peAccountant.ResolvedEntities[0]).Key); //ffAccountant.Value; //
            if (peConfirmer.IsValid && peConfirmer.ResolvedEntities.Count > 0)
                SPContext.Current.ListItem["Confirmer"] = SPContext.Current.Web.EnsureUser(((PickerEntity)peConfirmer.ResolvedEntities[0]).Key); //ffConfirmer.Value; //

            SPContext.Current.ListItem.Update();
        }

        private SPFieldLookupValue UpdateRequestDetail(SPList RequestDetailList, int id, string productName, string quantity, string price, string description)
        {
            try
            {
                SPFieldLookupValue spFieldLookupValue = null;
                SPListItem RequestDetailItem = RequestDetailList.GetItemById(id);
                if (RequestDetailItem != null)
                {
                    RequestDetailItem[SPBuiltInFieldId.Title] = productName;
                    RequestDetailItem["Quantity"] = quantity;
                    RequestDetailItem["Price"] = price;
                    RequestDetailItem["Description"] = description;
                    RequestDetailItem.Update();
                    spFieldLookupValue = new SPFieldLookupValue(RequestDetailItem.ID, RequestDetailItem.Title);
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

            string typeOfApproval = SPContext.Current.ListItem["TypeOfApproval"].ToString();
            LoadApprovalSettings(typeOfApproval);
            hiddenTypeOfApproval.Value = typeOfApproval;

            //if (!isLockRequest)
            //{
            //    linkButtonAdd.Enabled = false;
            //    btnSave.Enabled = false;
            //    rdbTypeOfApproval1.Enabled = false;
            //    rdbTypeOfApproval2.Enabled = false;
            //    ffTitle.ControlMode = SPControlMode.Display;
            //    peChief.Enabled = false;
            //    peBuyer.Enabled = false;
            //    peApprover.Enabled = false;
            //    peAccountant.Enabled = false;
            //    peConfirmer.Enabled = false;
            //}

            //Set discussion link url
            hyperLinkViewDiscussion.NavigateUrl = SPContext.Current.Site.MakeFullUrl(string.Format("_layouts/TVMCORP.TVS/DiscussionResolver.aspx?List={0}&ID={1}", SPContext.Current.ListId, SPContext.Current.ListItem.ID));

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
                #region Load approvers setting
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
                #endregion Load approvers setting
            }
            else
            {
                #region Setting is empty
                if (SPContext.Current.ListItem["Chief"] != null)
                {
                    SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Chief"].ToString());
                    peChief.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                }

                if (SPContext.Current.ListItem["Buyer"] != null)
                {
                    SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Buyer"].ToString());
                    peBuyer.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                }

                if (SPContext.Current.ListItem["Approver"] != null)
                {
                    SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Approver"].ToString());
                    peApprover.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                }

                if (SPContext.Current.ListItem["Accountant"] != null)
                {
                    SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Accountant"].ToString());
                    peAccountant.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                }
                if (SPContext.Current.ListItem["Confirmer"] != null)
                {
                    SPFieldUserValue spFieldUserValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.ListItem["Confirmer"].ToString());
                    peConfirmer.CommaSeparatedAccounts = spFieldUserValue.User.LoginName;
                }
                #endregion Setting is empty
            }
        }

        private DataTable MakeRequestDetail()
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
            
            foreach (RepeaterItem RequestDetail in repeaterRequestDetail.Items)
            {
                HiddenField hiddenFieldId = RequestDetail.FindControl("hiddenFieldId") as HiddenField;
                TextBox txtProductName = RequestDetail.FindControl("txtProductName") as TextBox;
                TextBox txtQuantity = RequestDetail.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = RequestDetail.FindControl("txtPrice") as TextBox;
                TextBox txtDescription = RequestDetail.FindControl("txtDescription") as TextBox;

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
            ViewState["RequestDetail"] = dataTable;

            return dataTable;
        }

        private DataTable LoadRequestDetail()
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

            var RequestDetailList = Utility.GetListFromURL(Constants.REQUEST_DETAIL_LIST_URL, SPContext.Current.Web);
            SPFieldLookupValueCollection RequestDetails = SPContext.Current.ListItem["RequestDetail"] as SPFieldLookupValueCollection;
            foreach (var RequestDetail in RequestDetails)
            {
                SPListItem listItem = RequestDetailList.GetItemById(RequestDetail.LookupId);
                if (listItem != null)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = listItem[SPBuiltInFieldId.ID].ToString();
                    row[1] = listItem[SPBuiltInFieldId.Title].ToString();
                    row[2] = listItem["Quantity"] != null ? listItem["Quantity"].ToString() : string.Empty;
                    row[3] = listItem["Price"] != null ? listItem["Price"].ToString() : string.Empty;
                    row[4] = listItem["Description"] != null ? listItem["Description"].ToString() : string.Empty;
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        private DataTable LoadPurchaseReferences()
        {
            DataTable dataTable = dataTable = new DataTable();
            DataColumn[] dataColumn = new DataColumn[]{
                new DataColumn("Title"),
                new DataColumn("DateRequest"),
                new DataColumn("UserRequest"),
                new DataColumn("DepartmentRequest"),
                new DataColumn("ID")
            };
            dataTable.Columns.AddRange(dataColumn);

            var purchaseList = SPContext.Current.List;
            SPFieldLookupValueCollection purchaseReferences = SPContext.Current.ListItem["References"] as SPFieldLookupValueCollection;
            foreach (var purchaseReference in purchaseReferences)
            {
                SPListItem listItem = purchaseList.GetItemById(purchaseReference.LookupId);
                if (listItem != null)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = listItem[SPBuiltInFieldId.Title].ToString();
                    row[1] = listItem["DateRequest"].ToString();
                    row[2] = listItem["UserRequest"].ToString();
                    row[3] = listItem["DepartmentRequest"] != null ? listItem["DepartmentRequest"].ToString() : string.Empty;
                    row[4] = listItem.ID;
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        #endregion Private Functions
    }
}
