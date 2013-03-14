using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.WebParts.RequestFilter
{
    [ToolboxItemAttribute(false)]
    public class RequestFilter : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public string RequestContentType{ get; set; }

        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/TVMCORP.TVS.WebParts/RequestFilter/RequestFilterUserControl.ascx";

        protected override void CreateChildControls()
        {
            RequestFilterUserControl control = Page.LoadControl(_ascxPath) as RequestFilterUserControl;
            control.RequestContentType = this.RequestContentType;
            Controls.Add(control);
        }

        public override Microsoft.SharePoint.WebPartPages.ToolPart[] GetToolParts()
        {
            CustomToolPart customToolPart = new CustomToolPart();
            customToolPart.Title = "TVM - Setting";

            Microsoft.SharePoint.WebPartPages.ToolPart[] toolParts = new Microsoft.SharePoint.WebPartPages.ToolPart[3];
            toolParts[0] = new Microsoft.SharePoint.WebPartPages.WebPartToolPart();
            toolParts[1] = new Microsoft.SharePoint.WebPartPages.CustomPropertyToolPart();
            toolParts[2] = customToolPart;


            return toolParts;
        }
    }

    public class CustomToolPart : Microsoft.SharePoint.WebPartPages.ToolPart
    {
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.DropDownList dropDownContentType;


        protected override void CreateChildControls()
        {
            RequestFilter webPart = (RequestFilter)this.ParentToolPane.SelectedWebPart;
            if (WebPartToEdit == null)
                return;

            lblTitle = new System.Web.UI.WebControls.Label();
            lblTitle.Text = "Đề nghị : ";
            Controls.Add(lblTitle);

            dropDownContentType = new System.Web.UI.WebControls.DropDownList();
            var purchaseList = Utility.GetListFromURL(Constants.PURCHASE_LIST_URL, SPContext.Current.Web);
            if (purchaseList != null)
            {
                foreach (SPContentType contentType in purchaseList.ContentTypes)
                {
                    dropDownContentType.Items.Add(new System.Web.UI.WebControls.ListItem(contentType.Name, contentType.Id.ToString()));
                }
            }
            this.Controls.Add(dropDownContentType);
            dropDownContentType.SelectedValue = webPart.RequestContentType;
            base.CreateChildControls();
        }

        public override void ApplyChanges()
        {
            RequestFilter webPart = (RequestFilter)this.ParentToolPane.SelectedWebPart;
            if (webPart != null)
                webPart.RequestContentType = dropDownContentType.SelectedItem.Value;
        }
    }
}
