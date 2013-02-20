using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Core.Features.TVMCORP.TVS.WORKFLOWS.Core
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("5b2255b2-9455-44a2-8961-501a489a5160")]
    public class HypertekIOfficeWorkflowEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPSite;
            using (SPWeb web = site.OpenWeb())
            {
                SPContentType ct = web.ContentTypes.Cast<SPContentType>()
                                                  .Where(p => p.Name == TMVCorpContentType.IOFFICE_APPROVAL_TASK)
                                                  .FirstOrDefault();
                if(ct != null){
                    ct.EditFormUrl = "_layouts/TVMCORP.TVS.WORKFLOWS.Core/CCIappWorkflowTaskApproval.aspx";
                    ct.DisplayFormUrl = "_layouts/TVMCORP.TVS.WORKFLOWS.Core/CCIappWorkflowTaskApproval.aspx";
                    ct.NewFormUrl = "_layouts/TVMCORP.TVS.WORKFLOWS.Core/CCIappWorkflowTaskApproval.aspx";
                    ct.Update(true);
                }
                
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
    }
}
