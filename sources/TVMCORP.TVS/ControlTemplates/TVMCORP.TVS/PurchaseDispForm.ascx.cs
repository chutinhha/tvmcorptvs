using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL;
using System.Data;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.ControlTemplates.TVMCORP.TVS
{
    public partial class PurchaseDispForm : UserControl
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //
            repeaterPurchaseDetail.ItemDataBound+=new RepeaterItemEventHandler(repeaterPurchaseDetail_ItemDataBound);
            //
            InitData();          

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dataTable = LoadPurchaseDetail();
                repeaterPurchaseDetail.DataSource = dataTable;
                repeaterPurchaseDetail.DataBind();
            }
        }

        void repeaterPurchaseDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;
            if (rowView != null)
            {
                Literal literalOrderValue = e.Item.FindControl("literalOrderValue") as Literal;
                literalOrderValue.Text = (e.Item.ItemIndex + 1).ToString();

                Literal literaltProductName = e.Item.FindControl("literaltProductName") as Literal;
                literaltProductName.Text = rowView["ProductName"].ToString();

                Literal literaltQuantity = e.Item.FindControl("literaltQuantity") as Literal;
                literaltQuantity.Text = rowView["Quantity"].ToString();

                Literal literaltPrice = e.Item.FindControl("literaltPrice") as Literal;
                literaltPrice.Text = rowView["Price"].ToString();

                Literal literaltDescription = e.Item.FindControl("literaltDescription") as Literal;
                literaltDescription.Text = rowView["Description"].ToString();
            }
        }

        private void InitData()
        {
            literalDateRequestValue.Text = Convert.ToDateTime(SPContext.Current.ListItem["DateRequest"].ToString()).ToString("dd/MM/yyyy");
            if (SPContext.Current.ListItem["TypeOfApproval"].ToString() == ApproversGroups.CongNgheThongTin)
            {
                rdbTypeOfApproval2.Checked = true;
            }
            rdbTypeOfApproval1.Enabled = false;
            rdbTypeOfApproval2.Enabled = false;

            //Set discussion link url
            hyperLinkViewDiscussion.NavigateUrl = SPContext.Current.Site.MakeFullUrl(string.Format("_layouts/TVMCORP.TVS/DiscussionResolver.aspx?List={0}&amp;ID={1}", SPContext.Current.ListId, SPContext.Current.ListItem.ID));
        }

        private DataTable LoadPurchaseDetail()
        {
            DataTable dataTable = dataTable = new DataTable();
            DataColumn[] dataColumn = new DataColumn[]{
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
                    row[0] = listItem[SPBuiltInFieldId.Title].ToString();
                    row[1] = listItem["Quantity"].ToString();
                    row[2] = listItem["Price"].ToString();
                    row[3] = listItem["Description"].ToString();
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
    }
}
