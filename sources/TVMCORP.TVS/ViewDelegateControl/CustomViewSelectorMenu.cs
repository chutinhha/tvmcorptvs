using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;

namespace TVMCORP.TVS
{
    public class CustomViewSelectorMenu : ViewSelectorMenu
    {
        internal CustomViewSelectorMenu(SPContext renderContext)
        {
            base.RenderContext = renderContext;
        }
    }
}

