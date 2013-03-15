using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;
using System.Collections.Generic;
using System.Xml;
using Microsoft.SharePoint.WebControls;

namespace TVMCORP.TVS.WebParts.RequestFilter
{
    public partial class RequestFilterUserControl : UserControl
    {

        public string RequestContentType { get; set; }

        private List<Microsoft.SharePoint.WebPartPages.XsltListViewWebPart> xsltListViewWebParts = new List<Microsoft.SharePoint.WebPartPages.XsltListViewWebPart>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SPRibbon ribbon = SPRibbon.GetCurrent(this.Page);
            if (ribbon != null)
            {
                ribbon.TrimById("Ribbon.ListItem.New");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            SetCustomQuery();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCustomQuery();
            if (!IsPostBack)
            {
                var requestList = Utility.GetListFromURL(Constants.REQUEST_LIST_URL, SPContext.Current.Web);
                if (requestList != null)
                {
                    string url = string.Format(@"javascript:NewItem2(event,'{0}/{1}?ContentTypeId={2}&IsDlg=1');javascript:return false;", SPContext.Current.Web.Url, requestList.Forms[PAGETYPE.PAGE_NEWFORM].Url, RequestContentType);
                    linkButtonAdd.OnClientClick = url;
                }
            }
        }

        protected void FindListViewWebParts(Control control, Guid listId)
        {
            Microsoft.SharePoint.WebPartPages.XsltListViewWebPart listview = null;
            if (control is Microsoft.SharePoint.WebPartPages.XsltListViewWebPart)
            {
                listview = control as Microsoft.SharePoint.WebPartPages.XsltListViewWebPart;
                if (listview.ListId == listId)
                {
                    xsltListViewWebParts.Add(listview);
                }
            }
            else
            {
                foreach (Control child in control.Controls)
                {
                    FindListViewWebParts(child, listId);
                    if (listview != null && listview.ListId == listId)
                    {
                        xsltListViewWebParts.Add(listview);
                    }
                }
            }
        }

        private void SetCustomQuery()
        {
            var query = string.Format(@"<Eq>
                                            <FieldRef Name='DepartmentRequest' />
                                            <Value Type='Text'>{0}</Value>
                                        </Eq>", GetDepartmentOfCurrentUser());
            var requestList = Utility.GetListFromURL(Constants.REQUEST_LIST_URL, SPContext.Current.Web);
            if (requestList != null)
            {
                FindListViewWebParts(this.Page, requestList.ID);
                if (xsltListViewWebParts.Count > 0)
                {
                    foreach (var item in xsltListViewWebParts)
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(item.XmlDefinition);

                        XmlElement viewXml = xml["View"];
                        XmlNode viewQuery = viewXml.SelectSingleNode("//Query");
                        XmlNode where = viewQuery.SelectSingleNode("//Where");

                        if (where == null)
                        {
                            where = xml.CreateElement("Where");
                            viewQuery.AppendChild(where);
                        }

                        if (where.ChildNodes.Count == 1)
                        {
                            where.InnerXml = string.Format("<And>{0}{1}</And>", where.FirstChild.OuterXml, query);
                        }
                        else
                        {
                            where.InnerXml = query;
                        }
                        item.XmlDefinition = xml.InnerXml;
                    }
                }
            }
        }

        private string GetDepartmentOfCurrentUser()
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
                                output = userItem["Department"] == null ? string.Empty : userItem["Department"].ToString();
                            }
                        }
                    }
                });
            }
            catch { throw; }

            return output;
        }
    }
}
