﻿using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL;
using System.Xml;

namespace TVMCORP.TVS.ListDefinitions.RequestDefinition.FilterWebPart
{
    [ToolboxItemAttribute(false)]
    public class FilterWebPart : WebPart
    {
        private List<Microsoft.SharePoint.WebPartPages.XsltListViewWebPart> xsltListViewWebParts = new List<Microsoft.SharePoint.WebPartPages.XsltListViewWebPart>();
        protected override void CreateChildControls()
        {
            SetCustomQuery();
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
            var purchaseList = Utility.GetListFromURL(Constants.REQUEST_LIST_URL, SPContext.Current.Web);
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
    }
}
