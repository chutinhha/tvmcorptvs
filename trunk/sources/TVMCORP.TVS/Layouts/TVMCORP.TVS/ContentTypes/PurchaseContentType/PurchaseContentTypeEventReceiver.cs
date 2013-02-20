using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;

namespace TVMCORP.TVS.ContentTypes
{
    [Guid("3f414e0d-5138-48dd-b29d-a3270ae1070b")]
    public class PurchaseContentTypeEventReceiverEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// Asynchronous After event that occurs after a new item has been added to its containing object.
        /// </summary>
        /// <param name="properties"></param>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
        }
    }
}
