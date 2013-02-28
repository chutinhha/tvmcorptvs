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

namespace TVMCORP.TVS.ListDefinitions.PurchaseDefinition.PurchaseFilter
{
    public partial class PurchaseFilterUserControl : UserControl
    {
        //private List<XsltListViewWebPart> xsltListViewWebParts;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //UpdateFilterQuery();
            string pageUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            ChangeListViewWebPart(pageUrl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected XsltListViewWebPart FindListViewWebPart(Control control)
        {
            XsltListViewWebPart listview = null;
            if (control is XsltListViewWebPart)
            {
                listview = control as XsltListViewWebPart;
            }
            else
            {
                foreach (Control child in control.Controls)
                {
                    listview = FindListViewWebPart(child);
                    if (listview != null)
                    {
                        break;
                    }
                }
            }
            return listview;
        }

        protected void UpdateFilterQuery()
        {
            foreach (Control control in this.Page.Controls)
            {
                if (control is XsltListViewWebPart)
                {
                    var listView = control as XsltListViewWebPart;
                    if (listView.ListUrl.Contains(Constants.PURCHASE_LIST_URL))
                    {
                        SetCustomQuery(listView, GetDepartmentOfCurrentUser());
                    }
                }
            }
        }

        private void SetCustomQuery(XsltListViewWebPart listView, string command)
        {
            var query = string.Format(@"<Eq>
                                                <FieldRef Name='DepartmentRequest' />
                                                <Value Type='Text'>{0}</Value>
                                            </Eq>", command);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(listView.XmlDefinition);

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
            listView.XmlDefinition = xml.InnerXml;
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

        private void ChangeListViewWebPart(string fullPageUrl)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        web.AllowUnsafeUpdates = true;
                        SPLimitedWebPartManager webPartManager = web.GetLimitedWebPartManager(fullPageUrl, PersonalizationScope.Shared);
                        foreach (System.Web.UI.WebControls.WebParts.WebPart webPart in webPartManager.WebParts)
                        {
                            if (webPart is XsltListViewWebPart)//|| webPart is ListViewWebPart)
                            {
                                var listViewWebPart = webPart as XsltListViewWebPart;
                                if (listViewWebPart.ListUrl.Contains(Constants.PURCHASE_LIST_URL))
                                {
                                    SetCustomQuery(listViewWebPart, GetDepartmentOfCurrentUser());
                                    web.Update();
                                }
                            }
                        }
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

    }
}
