using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class ReferencePurchase
    {
        public string No { get; set; }
        public string Title { get; set; }
        public DateTime DateRequest { get; set; }
        public string UserRequest { get; set; }
        public string DepartmentRequest { get; set; }
    }

    public class PurchaseDetail
    {
        public string No { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
    }

    public class PhieuMuaHangDataContext : DataContextBase
    {
        public DateTime DateRequest { get; set; }
        public string UserRequest { get; set; }
        public string TypeOfApproval { get; set; }

        public string Chief { get; set; }
        public string ChiefStatus { get; set; }
        public string ChiefComment { get; set; }
        public string Buyer { get; set; }
        public string BuyerStatus { get; set; }
        public string BuyerComment { get; set; }
        public string Approver { get; set; }
        public string ApproverStatus { get; set; }
        public string ApproverComment { get; set; }
        public string Accountant { get; set; }
        public string AccountantStatus { get; set; }
        public string AccountantComment { get; set; }
        public string Confirmer { get; set; }
        public string ConfirmerStatus { get; set; }
        public string ConfirmerComment { get; set; }
        public string TotalPrice { get; set; }

        public List<ReferencePurchase> ReferencePurchases { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }

        public PhieuMuaHangDataContext(SPListItem item): base(item)
        {
            if (item["DateRequest"] != null)
                DateRequest = Convert.ToDateTime(item["DateRequest"]);

            if (item["UserRequest"] != null && !string.IsNullOrEmpty(item["UserRequest"].ToString()))
                UserRequest = item["UserRequest"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1];
            else
                UserRequest = string.Empty;

            if (item["TypeOfApproval"] != null)
                TypeOfApproval = item["TypeOfApproval"].ToString();
            else
                TypeOfApproval = string.Empty;

            if (item["Chief"] != null && !string.IsNullOrEmpty(item["Chief"].ToString()))
                Chief = item["Chief"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1];
            else
                Chief = string.Empty;

            if (item["ChiefStatus"] != null)
                ChiefStatus = item["ChiefStatus"].ToString();
            else
                ChiefStatus = string.Empty;

            if (item["ChiefComment"] != null)
                ChiefComment = item["ChiefComment"].ToString();
            else
                ChiefComment = string.Empty;

            if (item["Buyer"] != null && !string.IsNullOrEmpty(item["Buyer"].ToString()))
                Buyer = item["Buyer"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1];
            else
                Buyer = string.Empty;

            if (item["BuyerStatus"] != null)
                BuyerStatus = item["BuyerStatus"].ToString();
            else
                BuyerStatus = string.Empty;

            if (item["BuyerComment"] != null)
                BuyerComment = item["BuyerComment"].ToString();
            else
                BuyerComment = string.Empty;

            if (item["Approver"] != null && !string.IsNullOrEmpty(item["Approver"].ToString()))
                Approver = item["Approver"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1];
            else
                Approver = string.Empty;

            if (item["ApproverStatus"] != null)
                ApproverStatus = item["ApproverStatus"].ToString();
            else
                ApproverStatus = string.Empty;

            if (item["ApproverComment"] != null)
                ApproverComment = item["ApproverComment"].ToString();
            else
                ApproverComment = string.Empty;

            if (item["Accountant"] != null && !string.IsNullOrEmpty(item["Accountant"].ToString()))
                Accountant = item["Accountant"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1];
            else
                Accountant = string.Empty;

            if (item["AccountantStatus"] != null)
                AccountantStatus = item["AccountantStatus"].ToString();
            else
                AccountantStatus = string.Empty;

            if (item["AccountantComment"] != null)
                AccountantComment = item["AccountantComment"].ToString();
            else
                AccountantComment = string.Empty;

            if (item["Confirmer"] != null && !string.IsNullOrEmpty(item["Confirmer"].ToString()))
                Confirmer = item["Confirmer"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1];
            else
                Confirmer = string.Empty;

            if (item["ConfirmerStatus"] != null)
                ConfirmerStatus = item["ConfirmerStatus"].ToString();
            else
                ConfirmerStatus = string.Empty;

            if (item["ConfirmerComment"] != null)
                ConfirmerComment = item["ConfirmerComment"].ToString();
            else
                ConfirmerComment = string.Empty;


            PurchaseDetails = new List<PurchaseDetail>();
            int total = 0;
            if (item["PurchaseDetail"] != null)
            {
                SPList listPurchaseDetail = Utility.GetListFromURL(Constants.PURCHASE_DETAIL_LIST_URL, item.Web);
                SPFieldLookupValueCollection purchaseDetails = item["PurchaseDetail"] as SPFieldLookupValueCollection;
                for (int i = 0; i < purchaseDetails.Count; i++ )
                {
                    var purchaseDetail = purchaseDetails[i];
                    SPListItem listItem = listPurchaseDetail.GetItemById(purchaseDetail.LookupId);
                    if (listItem != null)
                    {
                        PurchaseDetail pd = new PurchaseDetail();
                        pd.No = (i + 1).ToString();
                        pd.Title = listItem.Title;

                        if (listItem["Description"] != null)
                            pd.Description = listItem["Description"].ToString();
                        else
                            pd.Description = string.Empty;

                        int tempQuantity = 0;
                        if (listItem["Quantity"] != null)
                        {
                            pd.Quantity = listItem["Quantity"].ToString();
                            int.TryParse(pd.Quantity, out tempQuantity);
                        }
                        else
                            pd.Quantity = "0";

                        int tempPrice = 0;
                        if (listItem["Price"] != null)
                        {
                            pd.Price = listItem["Price"].ToString();
                            int.TryParse(pd.Price, out tempPrice);
                        }
                        else
                            pd.Price = "0";

                        total += tempQuantity * tempPrice;
                        PurchaseDetails.Add(pd);
                    }
                }
            }
            TotalPrice = total.ToString();

            ReferencePurchases = new List<ReferencePurchase>();
            if (item["References"] != null)
            {
                SPList listReferences = item.ParentList;
                SPFieldLookupValueCollection references = item["References"] as SPFieldLookupValueCollection;
                for (int i = 0; i < references.Count; i++)
                {
                    var reference = references[i];
                    SPListItem listItem = listReferences.GetItemById(reference.LookupId);
                    if (listItem != null)
                    {
                        ReferencePurchase rp = new ReferencePurchase();
                        rp.No = (i + 1).ToString();
                        rp.Title = listItem.Title;

                        if (listItem["DateRequest"] != null)
                            rp.DateRequest = Convert.ToDateTime(listItem["DateRequest"]);

                        if (listItem["UserRequest"] != null)
                            rp.UserRequest = listItem["UserRequest"].ToString();
                        else
                            rp.UserRequest = string.Empty;

                        if (listItem["DepartmentRequest"] != null)
                            rp.DepartmentRequest = listItem["DepartmentRequest"].ToString();
                        else
                            rp.DepartmentRequest = string.Empty;

                        ReferencePurchases.Add(rp);
                    }
                }
            }
        }
    }
}
