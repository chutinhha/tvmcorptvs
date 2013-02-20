using System;
using System.Web;
using System.Web.UI;
using System.Linq;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Core.Controls
{
    public partial class LinkToDocumentEditor : UserControl
    {
        private CustomFormSettings _customFormSettings = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            _customFormSettings = getSettings();
        }
        
        public string LinkToWorkflowItemPropertiesText
        {
            get
            {
                try
                {
                    if (_customFormSettings != null && !string.IsNullOrEmpty(_customFormSettings.LinkEditPropertiesText))
                        return _customFormSettings.LinkEditPropertiesText;
                }
                catch
                {
                    return "Link to Document Properties";
                }
                return "Link to Document Properties";
            }
        }

        private SPContentType ItemContentType
        {
            get
            {
                if (SPContext.Current.ListItem != null)
                    return SPContext.Current.ListItem.ContentType;
                else if (!string.IsNullOrEmpty(Request.QueryString["ContentTypeId"]))
                    return SPContext.Current.List.ContentTypes[new SPContentTypeId(Request.QueryString["ContentTypeId"])];

                return SPContext.Current.List.ContentTypes[0];
            }
        }

        private CustomFormSettings getSettings()
        {
            if (_customFormSettings == null)
                _customFormSettings = ItemContentType.GetCustomSettings<CustomFormSettings>(TVMCORPFeatures.TVS);

            if (_customFormSettings == null)
                _customFormSettings = new CustomFormSettings();
            return _customFormSettings;
        }

        public string LinkToWorkflowItemProperties
        {
            get
            {
                try
                {
                    SPListItem currentItem = (SPListItem)SPContext.Current.Item;
                    SPContentTypeId currentContentTypeId = new SPContentTypeId(currentItem[SPBuiltInFieldId.ContentTypeId].ToString());
                    SPContentTypeId cmContentTypeId = new SPContentTypeId(Constants.CCI_WORKFLOW_TASK_CONTENT_TYPE_ID);
                    if (currentContentTypeId.IsChildOf(cmContentTypeId)
                        && currentItem[SPBuiltInFieldId.WorkflowListId] != null
                        && !string.IsNullOrEmpty(currentItem[SPBuiltInFieldId.WorkflowListId].ToString()))
                    {
                        Guid listId = new Guid(currentItem[SPBuiltInFieldId.WorkflowListId].ToString());
                        SPList list = SPContext.Current.Web.Lists.GetList(listId, false);
                        int itemId = Convert.ToInt32(currentItem[SPBuiltInFieldId.WorkflowItemId]);
                        SPListItem item = list.GetItemById(itemId);
                        if (_customFormSettings != null && _customFormSettings.EditPropertiesLinkType == LinkIPFormType.Edit)
                            return "EditItemWithCheckoutAlert(event, '" + item.Web.Url + "/_layouts/listform.aspx?ListId={" + listId.ToString() + "}&ID=" + item.ID + "&ContentTypeID=" + item.ContentTypeId.ToString() + "&PageType=6',0,'0','','')";
                        else

                            return "EditItem2(event, '" + item.DisplayFormUrl() + "&source=" + HttpUtility.UrlEncode(SPContext.Current.Web.Url + this.Request.Path + "?" + this.Request.QueryString.ToString()) + "')";
                    }
                }
                catch //item had been deleted
                {
                    return string.Empty;
                }

                return string.Empty;
            }
        }
       
        public string LinkToWorkflowItemDocumentText
        {
            get
            {
                try
                {
                    if (_customFormSettings != null && !string.IsNullOrEmpty(_customFormSettings.LinkEditDocumentText))
                        return _customFormSettings.LinkEditDocumentText;
                }
                catch
                {
                    return "Link to Document";
                }
                return "Link to Document";
            }
        }
       
        public string FunctionToWorkflowItemDocument
        {
            get
            {
                try
                {
                    if (_customFormSettings != null && _customFormSettings.EditDocumentLinkType == LinkIPFormType.View)
                    {
                        SPListItem currentItem = (SPListItem)SPContext.Current.Item;
                        SPContentTypeId currentContentTypeId = new SPContentTypeId(currentItem[SPBuiltInFieldId.ContentTypeId].ToString());
                        SPContentTypeId cmContentTypeId = new SPContentTypeId(Constants.CCI_WORKFLOW_TASK_CONTENT_TYPE_ID);
                        if (currentContentTypeId.IsChildOf(cmContentTypeId)
                            && currentItem[SPBuiltInFieldId.WorkflowListId] != null
                            && !string.IsNullOrEmpty(currentItem[SPBuiltInFieldId.WorkflowListId].ToString()))
                        {
                            Guid listId = new Guid(currentItem[SPBuiltInFieldId.WorkflowListId].ToString());
                            SPList list = SPContext.Current.Web.Lists.GetList(listId, false);
                            int itemId = Convert.ToInt32(currentItem[SPBuiltInFieldId.WorkflowItemId]);
                            SPListItem item = list.GetItemById(itemId);

                            if (item != null && item.File != null)
                            {
                                return "return STSNavigate('" + SPContext.Current.Web.Url + "/" + SPEncode.UrlEncode(item.File.Url) + "')";
                            }
                        }
                    }
                    else
                        return "DispEx(this,event,'TRUE','FALSE','TRUE','SharePoint.OpenDocuments.3','0','SharePoint.OpenDocuments','','','','21','0','0','0x7fffffffffffffff')";
                }
                catch
                {
                    return string.Empty;
                }
                return string.Empty;
            }
        }

        public string LinkToWorkflowItemDocument
        {
            get
            {
                try
                {
                    if (_customFormSettings != null && _customFormSettings.EditDocumentLinkType == LinkIPFormType.View)
                        return "#";

                    SPListItem currentItem = (SPListItem)SPContext.Current.Item;
                    SPContentTypeId currentContentTypeId = new SPContentTypeId(currentItem[SPBuiltInFieldId.ContentTypeId].ToString());
                    SPContentTypeId cmContentTypeId = new SPContentTypeId(Constants.CCI_WORKFLOW_TASK_CONTENT_TYPE_ID);
                    if (currentContentTypeId.IsChildOf(cmContentTypeId)
                        && currentItem[SPBuiltInFieldId.WorkflowListId] != null
                        && !string.IsNullOrEmpty(currentItem[SPBuiltInFieldId.WorkflowListId].ToString()))
                    {
                        Guid listId = new Guid(currentItem[SPBuiltInFieldId.WorkflowListId].ToString());
                        SPList list = SPContext.Current.Web.Lists.GetList(listId, false);
                        int itemId = Convert.ToInt32(currentItem[SPBuiltInFieldId.WorkflowItemId]);
                        SPListItem item = list.GetItemById(itemId);

                        if (item != null && item.File != null)
                        {
                            return SPContext.Current.Web.Url + "//" + item.File.Url;
                        }
                    }
                }
                catch // item had been deleted
                {
                    return string.Empty;
                }
                return string.Empty;
            }
        }
    }
}
