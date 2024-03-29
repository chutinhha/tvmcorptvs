﻿using System;
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

        private string viewUrl = string.Format(@"javascript:NewItem2(event,'{0}/{1}?ListId={2}{3}');javascript:return false;",  SPContext.Current.Web.Url, SPContext.Current.List.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url, SPContext.Current.List.ID, "&ID={0}");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //
            repeaterRequestDetail.ItemDataBound += new RepeaterItemEventHandler(repeaterRequestDetail_ItemDataBound);
            repeaterPurchaseReference.ItemDataBound += new RepeaterItemEventHandler(repeaterPurchaseReference_ItemDataBound);
            //
            InitData();          

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable RequestDetails = LoadRequestDetail();
                repeaterRequestDetail.DataSource = RequestDetails;
                repeaterRequestDetail.DataBind();

                DataTable purchaseReferences = LoadPurchaseReferences();
                repeaterPurchaseReference.DataSource = purchaseReferences;
                repeaterPurchaseReference.DataBind();
            }
        }

        void repeaterRequestDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;
            if (rowView != null)
            {
                Literal literalOrderValue = e.Item.FindControl("literalOrderValue") as Literal;
                literalOrderValue.Text = (e.Item.ItemIndex + 1).ToString();

                Literal literaltProductName = e.Item.FindControl("literaltProductName") as Literal;
                literaltProductName.Text = rowView["ProductName"].ToString();

                Label lableQuantity = e.Item.FindControl("lableQuantity") as Label;
                lableQuantity.Text = string.IsNullOrEmpty(rowView["Quantity"].ToString()) ? string.Empty : Convert.ToDouble(rowView["Quantity"]).ToString("#,###");

                Label lablePrice = e.Item.FindControl("lablePrice") as Label;
                lablePrice.Text = string.IsNullOrEmpty(rowView["Price"].ToString()) ? string.Empty : Convert.ToDouble(rowView["Price"]).ToString("#,###");

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
            hyperLinkViewDiscussion.NavigateUrl = SPContext.Current.Site.MakeFullUrl(string.Format("_layouts/TVMCORP.TVS/DiscussionResolver.aspx?List={0}&ID={1}", SPContext.Current.ListId, SPContext.Current.ListItem.ID));
        }

        private DataTable LoadRequestDetail()
        {
            DataTable dataTable = dataTable = new DataTable();
            DataColumn[] dataColumn = new DataColumn[]{
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
                    row[0] = listItem[SPBuiltInFieldId.Title].ToString();

                    if (listItem["Quantity"] != null)
                        row[1] = listItem["Quantity"].ToString();

                    if (listItem["Price"] != null)
                        row[2] = listItem["Price"].ToString();

                    if (listItem["Description"] != null)
                        row[3] = listItem["Description"].ToString();
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
            if (purchaseReferences != null && purchaseReferences.Count > 0)
            {
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
            }
            return dataTable;
        }
    }
}
