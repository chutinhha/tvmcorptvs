using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.Receivers.DiscussionEvents
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class DiscussionEvents : SPItemEventReceiver
    {
       /// <summary>
       /// An item is being added.
       /// </summary>
       public override void ItemAdding(SPItemEventProperties properties)
       {
           base.ItemAdding(properties);
       }

       /// <summary>
       /// An item is being updated.
       /// </summary>
       public override void ItemUpdating(SPItemEventProperties properties)
       {
           base.ItemUpdating(properties);
       }

       /// <summary>
       /// An item is being deleted.
       /// </summary>
       public override void ItemDeleting(SPItemEventProperties properties)
       {
           base.ItemDeleting(properties);
       }

       /// <summary>
       /// An item was added.
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           base.ItemAdded(properties);
       }

       /// <summary>
       /// An item was updated.
       /// </summary>
       public override void ItemUpdated(SPItemEventProperties properties)
       {
           base.ItemUpdated(properties);
       }

       /// <summary>
       /// An item was deleted.
       /// </summary>
       public override void ItemDeleted(SPItemEventProperties properties)
       {
           base.ItemDeleted(properties);
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
