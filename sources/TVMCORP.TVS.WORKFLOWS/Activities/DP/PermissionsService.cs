using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Runtime;
using Microsoft.SharePoint;

namespace TVMCORP.TVS.WORKFLOWS.Activities.DP
{

    public enum PermissionActionType
    {
        Grant = 1,
        Revoke = 2,
        Reset = 3
    }


    [Serializable]
    public class PermissionRequest
    {
        public PermissionActionType RequestType;
        public Guid SiteID;
        public Guid WebID;
        public Guid ListID;
        public int ItemId;
        public string User;
        public string PermissionLevel;

    }
    /// <summary>
    /// processes permissions requests when workflow is ready to commit
    /// </summary>
    internal class PermissionsService : IPendingWork
    {
        internal static readonly PermissionsService Instance = new PermissionsService();

        #region PermissionRequest processing methods

        /// <summary>
        /// processes grant permission request
        /// </summary>
        /// <param name="pr"></param>
        private void ProcessGrantRequest(PermissionRequest pr)
        {
            int retryCount = 0;

            SPSecurity.RunWithElevatedPrivileges(delegate()
                 {
                     using (SPSite site = new SPSite(pr.SiteID))
                     {
                         using (SPWeb web = site.AllWebs[pr.WebID])
                         {
                             string permission = pr.PermissionLevel;

                             if (!Common.IsUserRoleAssigned(site, web, pr.ListID.ToString(), pr.ItemId, permission, pr.User))
                             {

                             setPerm:
                                 SPList List = web.Lists[pr.ListID];

                                 SPListItem listItem = List.GetItemById(pr.ItemId);

                                 if (!listItem.HasUniqueRoleAssignments)
                                 {
                                     listItem.BreakRoleInheritance(true);
                                 }


                                 Common.RemoveListItemLimitedPermissions(listItem);

                                 Common.RemoveListItemPermissionEntry(listItem, pr.User, false);

                                 listItem = Common.SetItemPermissions(web, listItem, permission, pr.User);

                                 try
                                 {
                                     listItem.SystemUpdate();

                                 }
                                 catch
                                 {
                                     //if in our workflow, we are changing the list item on which we are perfoming this operation, might need to try a couple of times
                                     if (retryCount <= 3)
                                     {
                                         retryCount++;

                                         goto setPerm;

                                     }
                                 }

                             }

                         }
                     }

                 });
        }

        /// <summary>
        /// processes revoke permission request
        /// </summary>
        /// <param name="pr"></param>
        private void ProcessRevokeRequest(PermissionRequest pr)
        {
            int retryCount = 0;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {

                using (SPSite site = new SPSite(pr.SiteID))
                {
                    using (SPWeb web = site.AllWebs[pr.WebID])
                    {
                        SPList list = web.Lists[pr.ListID];

                    revokePerm:

                        SPListItem listItem = list.Items.GetItemById(pr.ItemId);

                        Common.RemoveListItemPermissionEntry(listItem, pr.User, true);

                        try
                        {
                            listItem.SystemUpdate();
                        }
                        catch
                        {//if in our workflow, we are changing the list item on which we are perfoming this operation, might need to try a couple of times
                            if (retryCount <= 3)
                            {
                                retryCount++;

                                goto revokePerm;

                            }
                        }
                    }

                }

            });

        }


        /// <summary>
        /// processes reset permission request
        /// </summary>
        /// <param name="pr"></param>
        private void ProcessResetRequest(PermissionRequest pr)
        {
            int retryCount = 0;

            SPSecurity.RunWithElevatedPrivileges(delegate()
         {

             using (SPSite site = new SPSite(pr.SiteID))
             {
                 using (SPWeb web = site.AllWebs[pr.WebID])
                 {
                     SPList list = web.Lists[pr.ListID];


                   
                 resetPerm:

                     SPListItem listItem = list.Items.GetItemById(pr.ItemId);

                     if (listItem.HasUniqueRoleAssignments)
                     {
                         listItem.ResetRoleInheritance();

                         try
                         {
                             listItem.SystemUpdate();
                         }
                         catch
                         {
                             //if in our workflow, we are changing the list item on which we are perfoming this operation, might need to try a couple of times
                             if (retryCount <= 3)
                             {
                                 retryCount++;

                                 goto resetPerm;

                             }
                         }

                     }
                 }

             }

         });

        }


        #endregion

        #region IPendingWork Members

        public void Commit(System.Transactions.Transaction transaction, System.Collections.ICollection items)
        {
            foreach (PermissionRequest pr in items)
            {

                switch (pr.RequestType)
                {
                    case PermissionActionType.Grant:

                        this.ProcessGrantRequest(pr);

                        break;
                    case PermissionActionType.Revoke:

                        this.ProcessRevokeRequest(pr);

                        break;
                    case PermissionActionType.Reset:

                        this.ProcessResetRequest(pr);

                        break;
                    default:
                        break;
                }

            }
        }



        public void Complete(bool succeeded, System.Collections.ICollection items)
        {

        }

        public bool MustCommit(System.Collections.ICollection items)
        {
            return true;

        }

        #endregion
    }
}
