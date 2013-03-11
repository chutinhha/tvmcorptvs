using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordDocumentGenerator.Library;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class PhieuMuaHangGenerator : BaseDoxGenerator
    {
        protected const string DateRequest = "DateRequest";
        protected const string UserRequest = "UserRequest";
        protected const string TypeOfApproval = "TypeOfApproval";

        protected const string Chief = "Chief";
        protected const string ChiefStatus = "ChiefStatus";
        protected const string ChiefComment = "ChiefComment";
        protected const string Buyer = "Buyer";
        protected const string BuyerStatus = "BuyerStatus";
        protected const string BuyerComment = "BuyerComment";
        protected const string Approver = "Approver";
        protected const string ApproverStatus = "ApproverStatus";
        protected const string ApproverComment = "ApproverComment";
        protected const string Accountant = "Accountant";
        protected const string AccountantStatus = "AccountantStatus";
        protected const string AccountantComment = "AccountantComment";
        protected const string Confirmer = "Confirmer";
        protected const string ConfirmerStatus = "ConfirmerStatus";
        protected const string ConfirmerComment = "ConfirmerComment";
        protected const string TotalPrice = "TotalPrice";

        protected const string PurchaseDetail = "PurchaseDetail";
        protected const string PurchaseDetail_No = "PurchaseDetail_No";
        protected const string PurchaseDetail_Title = "PurchaseDetail_Title";
        protected const string PurchaseDetail_Description = "PurchaseDetail_Description";
        protected const string PurchaseDetail_Quantity = "PurchaseDetail_Quantity";
        protected const string PurchaseDetail_Price = "PurchaseDetail_Price";

        protected const string ReferencePurchase = "ReferencePurchase";
        protected const string ReferencePurchase_No = "ReferencePurchase_No";
        protected const string ReferencePurchase_Title = "ReferencePurchase_Title";
        protected const string ReferencePurchase_DateRequest = "ReferencePurchase_DateRequest";
        protected const string ReferencePurchase_UserRequest = "ReferencePurchase_UserRequest";
        protected const string ReferencePurchase_DepartmentRequest = "ReferencePurchase_DepartmentRequest";

        public PhieuMuaHangGenerator(DocumentGenerationInfo generationInfo) : base(generationInfo)
        {

        }

        #region Overridden methods

        /// <summary>
        /// Gets the place holder tag to type collection.
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, PlaceHolderType> GetPlaceHolderTagToTypeCollection()
        {
            Dictionary<string, PlaceHolderType> placeHolderTagToTypeCollection = base.GetPlaceHolderTagToTypeCollection();

            // Handle recursive placeholders            
            placeHolderTagToTypeCollection.Add(PurchaseDetail, PlaceHolderType.Recursive);
            placeHolderTagToTypeCollection.Add(ReferencePurchase, PlaceHolderType.Recursive);

            // Handle non recursive placeholders
            placeHolderTagToTypeCollection.Add(DateRequest, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(UserRequest, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(TypeOfApproval, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(Chief, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ChiefStatus, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ChiefComment, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(Buyer, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(BuyerStatus, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(BuyerComment, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(Approver, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ApproverStatus, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ApproverComment, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(Accountant, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(AccountantStatus, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(AccountantComment, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(Confirmer, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ConfirmerStatus, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ConfirmerComment, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(TotalPrice, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(PurchaseDetail_No, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(PurchaseDetail_Title, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(PurchaseDetail_Description, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(PurchaseDetail_Quantity, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(PurchaseDetail_Price, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ReferencePurchase_No, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ReferencePurchase_Title, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ReferencePurchase_DateRequest, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ReferencePurchase_UserRequest, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(ReferencePurchase_DepartmentRequest, PlaceHolderType.NonRecursive);
            return placeHolderTagToTypeCollection;
        }

        /// <summary>
        /// Non recursive placeholder found.
        /// </summary>
        /// <param name="placeholderTag">The placeholder tag.</param>
        /// <param name="openXmlElementDataContext">The open XML element data context.</param>
        protected override void NonRecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            if (openXmlElementDataContext == null || openXmlElementDataContext.Element == null || openXmlElementDataContext.DataContext == null)
            {
                return;
            }

            string tagPlaceHolderValue = string.Empty;
            string tagGuidPart = string.Empty;
            GetTagValue(openXmlElementDataContext.Element as SdtElement, out tagPlaceHolderValue, out tagGuidPart);

            string tagValue = string.Empty;
            string content = string.Empty;

            // Reuse base class code and handle only tags unavailable in base class
            bool bubblePlaceHolder = true;

            switch (tagPlaceHolderValue)
            {
                case DateRequest:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).DateRequest.ToString("dd/MM/yyyy");
                    content = tagValue;
                    break;
                case UserRequest:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).UserRequest;
                    content = tagValue;
                    break;
                case TypeOfApproval:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).TypeOfApproval;
                    content = tagValue;
                    break;
                case Chief:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).Chief;
                    content = tagValue;
                    break;
                case ChiefStatus:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ChiefStatus;
                    content = tagValue;
                    break;
                case ChiefComment:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ChiefComment;
                    content = tagValue;
                    break;
                case Buyer:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).Buyer;
                    content = tagValue;
                    break;
                case BuyerStatus:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).BuyerStatus;
                    content = tagValue;
                    break;
                case BuyerComment:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).BuyerComment;
                    content = tagValue;
                    break;
                case Approver:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).Approver;
                    content = tagValue;
                    break;
                case ApproverStatus:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ApproverStatus;
                    content = tagValue;
                    break;
                case ApproverComment:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ApproverComment;
                    content = tagValue;
                    break;
                case Accountant:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).Accountant;
                    content = tagValue;
                    break;
                case AccountantStatus:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).AccountantStatus;
                    content = tagValue;
                    break;
                case AccountantComment:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).AccountantComment;
                    content = tagValue;
                    break;
                case Confirmer:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).Confirmer;
                    content = tagValue;
                    break;
                case ConfirmerStatus:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ConfirmerStatus;
                    content = tagValue;
                    break;
                case ConfirmerComment:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ConfirmerComment;
                    content = tagValue;
                    break;

                case PurchaseDetail_No:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PurchaseDetail).No;
                    content = tagValue;
                    break;
                case PurchaseDetail_Title:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PurchaseDetail).Title;
                    content = tagValue;
                    break;
                case PurchaseDetail_Description:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PurchaseDetail).Description;
                    content = tagValue;
                    break;
                case PurchaseDetail_Quantity:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PurchaseDetail).Quantity;
                    content = tagValue;
                    break;
                case PurchaseDetail_Price:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PurchaseDetail).Price;
                    content = tagValue;
                    break;
                case TotalPrice:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).TotalPrice;
                    content = tagValue;
                    break;

                case ReferencePurchase_No:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as ReferencePurchase).No;
                    content = tagValue;
                    break;
                case ReferencePurchase_Title:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as ReferencePurchase).Title;
                    content = tagValue;
                    break;
                case ReferencePurchase_DateRequest:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as ReferencePurchase).DateRequest.ToString("dd/MM/yyyy");
                    content = tagValue;
                    break;
                case ReferencePurchase_UserRequest:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as ReferencePurchase).UserRequest;
                    content = tagValue;
                    break;
                case ReferencePurchase_DepartmentRequest:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as ReferencePurchase).DepartmentRequest;
                    content = tagValue;
                    break;
            }

            if (bubblePlaceHolder)
            {
                // Use base class code as tags are already defined in base class.
                base.NonRecursivePlaceholderFound(placeholderTag, openXmlElementDataContext);
            }
            else
            {
                // Set the tag for the content control
                if (!string.IsNullOrEmpty(tagValue))
                {
                    this.SetTagValue(openXmlElementDataContext.Element as SdtElement, GetFullTagValue(tagPlaceHolderValue, tagValue));
                }

                // Set the content for the content control
                this.SetContentOfContentControl(openXmlElementDataContext.Element as SdtElement, content);
            }
        }

        /// <summary>
        /// Recursive placeholder found.
        /// </summary>
        /// <param name="placeholderTag">The placeholder tag.</param>
        /// <param name="openXmlElementDataContext">The open XML element data context.</param>
        protected override void RecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            if (openXmlElementDataContext == null || openXmlElementDataContext.Element == null || openXmlElementDataContext.DataContext == null)
            {
                return;
            }

            string tagPlaceHolderValue = string.Empty;
            string tagGuidPart = string.Empty;
            GetTagValue(openXmlElementDataContext.Element as SdtElement, out tagPlaceHolderValue, out tagGuidPart);

            // Reuse base class code and handle only tags unavailable in base class
            bool bubblePlaceHolder = true;

            switch (tagPlaceHolderValue)
            {
                case PurchaseDetail:
                    bubblePlaceHolder = false;

                    foreach (PurchaseDetail purchaseDetail in ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).PurchaseDetails)
                    {
                        SdtElement clonedElement = this.CloneElementAndSetContentInPlaceholders(new OpenXmlElementDataContext() { Element = openXmlElementDataContext.Element, DataContext = purchaseDetail });
                    }

                    openXmlElementDataContext.Element.Remove();
                    break;
                case ReferencePurchase:
                    bubblePlaceHolder = false;

                    foreach (ReferencePurchase referencePurchase in ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).ReferencePurchases)
                    {
                        SdtElement clonedElement = this.CloneElementAndSetContentInPlaceholders(new OpenXmlElementDataContext() { Element = openXmlElementDataContext.Element, DataContext = referencePurchase });
                    }

                    openXmlElementDataContext.Element.Remove();
                    break;
            }

            if (bubblePlaceHolder)
            {
                // Use base class code as tags are already defined in base class.
                base.RecursivePlaceholderFound(placeholderTag, openXmlElementDataContext);
            }
        }

        #endregion
    }
}
