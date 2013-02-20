using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using TVMCORP.TVS.UTIL.Utilities;
using Microsoft.SharePoint.Administration;

namespace TVMCORP.TVS.WORKFLOWS.FEATURES
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("2d5afacf-1098-4687-b8cc-478da85fdcc2")]
    public class TVMCORPTVSWORKFLOWSEventReceiver : WebConfigModifier
    {
        protected override void AddConfigurationToWebConfig(Microsoft.SharePoint.Administration.SPWebApplication app)
        {
          
            base.AddNodeValue("enableSessionState", "configuration/system.web/pages", "true", SPWebConfigModification.SPWebConfigModificationType.EnsureAttribute, 2);
            //<add name="Session" type="System.Web.SessionState.SessionStateModule, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
            base.AddNodeValue("add[@name='Session']", "configuration/system.webServer/modules", "<add name=\"Session\" type=\"System.Web.SessionState.SessionStateModule, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\" />", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 2);

            //<add name="IOfficeSiteMapProvider" type="Hypertek.IOffice.IOfficeNavigationProvider, Hypertek.IOffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" sourceUrl="http://ioffice.dev" depth="2" />
           // base.AddNodeValue("add[@name='IOfficeSiteMapProvider']", "configuration/system.web/siteMap/providers", "<add name=\"IOfficeSiteMapProvider\" type=\"Hypertek.IOffice.IOfficeNavigationProvider, Hypertek.IOffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f\" sourceUrl=\"http://ioffice.dev\" depth=\"5\" />", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 2);

            //<add name="IOfficeHttpModule" type="Hypertek.IOffice.IOfficeHttpModule,  Hypertek.IOffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f"/>
           // base.AddNodeValue("add[@name='IOfficeHttpModule']", "configuration/system.webServer/modules", "<add name=\"IOfficeHttpModule\" type=\"Hypertek.IOffice.IOfficeHttpModule,  Hypertek.IOffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f\"/>", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 2);

            // <controls>
            //        <add tagPrefix="asp" namespace="Hypertek.IOffice.Webpart" assembly="Hypertek.IOffice.Webpart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" />
            //</controls>
            //base.AddNodeValue("add[@namespace='Hypertek.IOffice.Webparts']", "configuration/system.web/pages/controls", "<add tagPrefix=\"asp\" namespace=\"Hypertek.IOffice.Webparts\" assembly=\"Hypertek.IOffice.Webpart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f\" />", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 2);

            //<add key="ChartImageHandler" value="storage=session;timeout=20;" />
           // base.AddNodeValue("add[@key='ChartImageHandler']", "configuration/appSettings", "<add key=\"ChartImageHandler\" value=\"storage=session;timeout=20;\" />", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 2);

            //<remove name="ChartImageHandler" />
            //<add name="ChartImageHandler" preCondition="integratedMode" verb="GET,HEAD,POST" path="ChartImg.axd" type="System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />

            //base.AddNodeValue("add[@name='ChartImageHandler']", "configuration/system.webServer/handlers", "<add name=\"ChartImageHandler\" preCondition=\"integratedMode\" verb=\"GET,HEAD,POST\" path=\"ChartImg.axd\" type=\"System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 200);
            //base.AddNodeValue("remove[@name='ChartImageHandler']", "configuration/system.webServer/handlers", "<remove name=\"ChartImageHandler\" />", SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 1);
            base.SaveWebConfig(app);

            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //{
            //    SPFarm.Local.Services.GetValue<SPWebService>().
            //                 ApplyApplicationContentToLocalServer();
            //});
        }


        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
        }

        protected override string OwnerModify
        {
            get { return "TVMCORP"; }
        }
    }
}
