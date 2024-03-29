﻿using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Reflection;
using TVMCORP.TVS.UTIL.Helpers;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;

namespace TVMCORP.TVS.Features.TVMCORP.TVS.Feature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("3b2bdbd0-81c9-435b-b5a8-d052bad2198a")]
    public class TVMCORPTVSEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
             SPWeb web = (SPWeb)properties.Feature.Parent;
             try
             {
                 SetListPermission(web);
                 //RemoveXsltListViewWebPart(web.Site.MakeFullUrl(Constants.PURCHASE_MY_ITEM_VIEW_URL), web);
                 //RemoveXsltListViewWebPart(web.Site.MakeFullUrl(Constants.PURCHASE_MY_DEPARTMENT_ITEM_VIEW_URL), web);
                 ProvisionWebParts(web, "TVMCORP.TVS.WebParts.xml");

                 ConfigLists(web);
             }
             catch (Exception ex)
             {
                 Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
             }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}

        #region Functions

        private void SetListPermission(SPWeb web)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(web.Site.ID))
                    {
                        using (SPWeb spWeb = site.OpenWeb(web.ID))
                        {
                            spWeb.AllowUnsafeUpdates = true;
                            //Purchase list
                            var purchaseList = Utility.GetListFromURL(Constants.REQUEST_LIST_URL, spWeb);
                            purchaseList.BreakRoleInheritance(false);
                            purchaseList.SetPermissions(spWeb.EnsureUser(Constants.AUTHENTICATED_USERS), SPRoleType.Contributor);
                            //Purchase Detail list
                            var RequestDetailList = Utility.GetListFromURL(Constants.REQUEST_DETAIL_LIST_URL, spWeb);
                            RequestDetailList.BreakRoleInheritance(false);
                            RequestDetailList.SetPermissions(spWeb.EnsureUser(Constants.AUTHENTICATED_USERS), SPRoleType.Contributor);
                            //Purchase Task List
                            var purchaseTaskApprovalList = Utility.GetListFromURL(Constants.REQUEST_TASK_LIST_URL, spWeb);
                            purchaseTaskApprovalList.BreakRoleInheritance(false);
                            purchaseTaskApprovalList.SetPermissions(spWeb.EnsureUser(Constants.AUTHENTICATED_USERS), SPRoleType.Reader);
                            spWeb.AllowUnsafeUpdates = true;
                            //Discussion List
                            var discussionlList = Utility.GetListFromURL(Constants.DISCUSSIONS_LIST_URL, spWeb);
                            discussionlList.BreakRoleInheritance(false);
                            discussionlList.SetPermissions(spWeb.EnsureUser(Constants.AUTHENTICATED_USERS), SPRoleType.Contributor);
                            spWeb.AllowUnsafeUpdates = true;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
            }
        }


        private void RemoveXsltListViewWebPart(string fullPageUrl, SPWeb spWeb)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(spWeb.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(spWeb.ID))
                    {
                        web.AllowUnsafeUpdates = true;
                        Microsoft.SharePoint.WebPartPages.SPLimitedWebPartManager webPartManager = web.GetLimitedWebPartManager(fullPageUrl, System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared);
                        
                        foreach (System.Web.UI.WebControls.WebParts.WebPart webPart in webPartManager.WebParts)
                        {
                            if (webPart is Microsoft.SharePoint.WebPartPages.XsltListViewWebPart)//|| webPart is ListViewWebPart)
                            {
                                //webPartManager.MoveWebPart(webPart, "Main", 0);
                                webPartManager.DeleteWebPart(webPart);
                                //webPart.Hidden = true;
                                //webPartManager.SaveChanges(webPart);
                                web.Update();
                                break;
                            }
                        }
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        private void ProvisionWebParts(SPWeb web, string xmlFile)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string xml = assembly.GetResourceTextFile(xmlFile);

                var webpartPages = SerializationHelper.DeserializeFromXml<WebpartPageDefinitionCollection>(xml);
                WebPartHelper.ProvisionWebpart(web, webpartPages);
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
            }
        }

        private void ConfigLists(SPWeb web)
        {
            try
            {
                web.AllowUnsafeUpdates = true;

                SPList listPurchase = Utility.GetListFromURL(Constants.REQUEST_LIST_URL, web);
                if (listPurchase != null)
                {
                    //SetListProperties(listPurchase, true);
                    CreateApprovalWorkflow(listPurchase, "[TVS] Approval Workflow", "Purchase Approval", "TVMCORP.TVS.XMLSettings.ApprovalWorkflowSettings_Purchase.xml");
                }
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
            }
        }

        private void SetListProperties(SPList list, bool enableContentApproval)
        {
            try
            {
                list.ParentWeb.AllowUnsafeUpdates = true;

                list.ContentTypesEnabled = true;
                list.EnableVersioning = true;

                if (enableContentApproval)
                    list.EnableModeration = true; //enable Content Approval, Approval Status column will be added to default view

                list.Update();

                if (enableContentApproval)
                {
                    SPView view = list.DefaultView;
                    view.ViewFields.Delete("_ModerationStatus");
                    view.Update();
                }
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
            }
            finally
            {
                list.ParentWeb.AllowUnsafeUpdates = false;
            }
        }

        private void CreateApprovalWorkflow(SPList list, string workflowTemplateName, string workflowName, string fileSettings)
        {
            string association = string.Empty;

            if (!string.IsNullOrEmpty(fileSettings))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string xml = assembly.GetResourceTextFile(fileSettings);

                association = SerializationHelper.SerializeToXml(SerializationHelper.DeserializeFromXml<ApprovalWFAssociationData>(xml));
            }

            list.AssociateWorkflow(workflowTemplateName, workflowName, association, "Duyệt đề nghị");
        }
        #endregion Functions
    }
}
