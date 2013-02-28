using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using Microsoft.SharePoint.WebPartPages;
using System.Collections.Generic;
using TVMCORP.TVS.UTIL;
using Microsoft.SharePoint;
using System.Collections;
using System.Linq;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.ListDefinitions.PurchaseDefinition.PurchaseFilter
{
    public partial class PurchaseFilterUserControl : UserControl
    {
        private List<XsltListViewWebPart> xsltListViewWebParts = new List<XsltListViewWebPart>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            SetCustomQuery();
        }

        protected void FindListViewWebParts(Control control, Guid listId)
        {
            XsltListViewWebPart listview = null;
            if (control is XsltListViewWebPart)
            {
                listview = control as XsltListViewWebPart;
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
            var purchaseList = Utility.GetListFromURL(Constants.PURCHASE_LIST_URL, SPContext.Current.Web);
            FindListViewWebParts(this.Page, purchaseList.ID);
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


        private List<XsltListViewWebPart> GetXsltListViewWebParts()
        {
            string pageUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            //xsltListViewWebParts = new List<XsltListViewWebPart>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        var purchaseList = Utility.GetListFromURL(Constants.PURCHASE_LIST_URL, web);

                        SPLimitedWebPartManager webPartManager = web.GetLimitedWebPartManager(pageUrl, PersonalizationScope.Shared);
                        foreach (System.Web.UI.WebControls.WebParts.WebPart webPart in webPartManager.WebParts)
                        {
                            if (webPart is XsltListViewWebPart)//|| webPart is ListViewWebPart)
                            {
                                var listViewWebPart = webPart as XsltListViewWebPart;
                                if (listViewWebPart.ListId == purchaseList.ID)
                                {
                                    xsltListViewWebParts.Add(listViewWebPart);
                                }
                            }
                        }
                    }
                }
            });
            return xsltListViewWebParts;
        }

    }
}
