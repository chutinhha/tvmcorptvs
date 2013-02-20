using System;
using Microsoft.SharePoint;

namespace TVMCORP.TVS.UTIL.Utilities
{
    public class DisableItemEvent : SPItemEventReceiver, IDisposable
    {
        bool oldValue;

        public DisableItemEvent()
        {
            this.oldValue = base.EventFiringEnabled;
            base.EventFiringEnabled = false;
        }

        public void Dispose()
        {
            base.EventFiringEnabled = oldValue;
        }
    }
}
