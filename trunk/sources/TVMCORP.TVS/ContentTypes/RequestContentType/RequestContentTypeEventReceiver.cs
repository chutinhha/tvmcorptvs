using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.ContentTypes
{
    [Guid("3f414e0d-5138-48dd-b29d-a3270ae1070b")]
    public class RequestContentTypeEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// Asynchronous After event that occurs after a new item has been added to its containing object.
        /// </summary>
        /// <param name="properties"></param>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            SetItemPermission(properties.Web, properties.ListId, properties.ListItemId);
        }

        #region Permission
        private void SetItemPermission(SPWeb web, Guid listId, int itemId)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(web.Site.ID))
                {
                    using (SPWeb spWeb = site.OpenWeb(web.ID))
                    {
                        SPList list = spWeb.Lists[listId];
                        SPListItem listItem = list.GetItemById(itemId);
                        listItem.RemoveAllPermissions();
                        SPFieldUserValue userValue = new SPFieldUserValue(spWeb, listItem[SPBuiltInFieldId.Author].ToString());
                        listItem.SetPermissions(userValue.User, SPRoleType.Contributor);
                        listItem.SetPermissions(spWeb.EnsureUser(Constants.AUTHENTICATED_USERS), SPRoleType.Reader);
                    }
                }
            });
        }
        #endregion Permission
    }
}
