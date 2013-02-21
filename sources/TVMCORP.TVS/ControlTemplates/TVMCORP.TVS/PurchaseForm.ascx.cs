using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace TVMCORP.TVS.ControlTemplates.TVMCORP.TVS
{
    public partial class PurchaseForm : UserControl
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
            GetUserInfo();
            repeaterPurchaseDetail.ItemDataBound += new RepeaterItemEventHandler(repeaterPurchaseDetail_ItemDataBound);
            btnAddPurchaseDetail.Click += new EventHandler(btnAddPurchaseDetail_Click);
            btnSave.Click += new EventHandler(btnSave_Click);

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

        void btnAddPurchaseDetail_Click(object sender, EventArgs e)
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
                //Load purchase detail
                DataTable purchaseDetail = MakePurchaseDetail();
                repeaterPurchaseDetail.DataSource = purchaseDetail;
                repeaterPurchaseDetail.DataBind();
            }
        }

        #region Private Functions

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

        private DataTable MakePurchaseDetail()
        {
            DataTable dataTable = null;
            if (ViewState["PurchaseDetail"] == null)
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
                dataTable = ViewState["PurchaseDetail"] as DataTable;
                dataTable.Clear();

                foreach (RepeaterItem purchaseDetail in repeaterPurchaseDetail.Items)
                {
                    TextBox txtProductName = purchaseDetail.FindControl("txtProductName") as TextBox;
                    TextBox txtQuantity = purchaseDetail.FindControl("txtQuantity") as TextBox;
                    TextBox txtPrice = purchaseDetail.FindControl("txtPrice") as TextBox;
                    TextBox txtDescription = purchaseDetail.FindControl("txtDescription") as TextBox;
                    
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
            ViewState["PurchaseDetail"] = dataTable;

            return dataTable;
        }

        private void AddPurchase()
        {
            SPFieldLookupValueCollection purchaseDetails = new SPFieldLookupValueCollection();

            var purchaseDetailList = Utility.GetListFromURL(Constants.PURCHASE_DETAIL_LIST_URL, SPContext.Current.Web);
            foreach (RepeaterItem purchaseDetail in repeaterPurchaseDetail.Items)
            {
                TextBox txtProductName = purchaseDetail.FindControl("txtProductName") as TextBox;
                TextBox txtQuantity = purchaseDetail.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = purchaseDetail.FindControl("txtPrice") as TextBox;
                TextBox txtDescription = purchaseDetail.FindControl("txtDescription") as TextBox;
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

            var purchaseList = Utility.GetListFromURL(Constants.PURCHASE_LIST_URL, SPContext.Current.Web);
            SPListItem purchaseItem = purchaseList.AddItem();
            purchaseItem[SPBuiltInFieldId.Title] = Constants.PURCHASE_TITLE_PREFIX + literalUserRequestValue.Text;
            purchaseItem["DateRequest"] = DateTime.Now;
            purchaseItem["UserRequest"] = SPContext.Current.Web.CurrentUser;
            purchaseItem["DepartmentRequest"] = literalDepartmentRequestValue.Text;
            purchaseItem["TypeOfApproval"] = "Công nghệ thông tin";
            purchaseItem["PurchaseDetail"] = purchaseDetails;
            purchaseItem["Chief"] = ffChief.Value; //SPContext.Current.Web.EnsureUser(((PickerEntity)peChief.ResolvedEntities[0]).Key);
            purchaseItem["Buyer"] = ffBuyer.Value; //SPContext.Current.Web.EnsureUser(((PickerEntity)peBuyer.ResolvedEntities[0]).Key);
            purchaseItem["Approver"] = ffApprover.Value; //SPContext.Current.Web.EnsureUser(((PickerEntity)peApprover.ResolvedEntities[0]).Key);
            purchaseItem["Accountant"] = ffAccountant.Value; //SPContext.Current.Web.EnsureUser(((PickerEntity)peAccountant.ResolvedEntities[0]).Key);
            purchaseItem["Confirmer"] = ffConfirmer.Value; //SPContext.Current.Web.EnsureUser(((PickerEntity)peConfirmer.ResolvedEntities[0]).Key);
            purchaseItem.Update();
        }

        #endregion Private Functions
    }
}
