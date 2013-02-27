using System;
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
                 ProvisionWebParts(web);
             }
             catch (Exception ex)
             {
                 
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
        private void ProvisionWebParts(SPWeb web)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string xml = assembly.GetResourceTextFile("TVMCORP.TVS.Webparts.xml");

                var webpartPages = SerializationHelper.DeserializeFromXml<WebpartPageDefinitionCollection>(xml);
                WebPartHelper.ProvisionWebpart(web, webpartPages);
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, TVMCORPFeatures.TVS);
            }
        }
        #endregion Functions
    }
}
