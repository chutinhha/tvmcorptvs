using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class DataContextBase
    {
        public string Title { get; set; }

        public DataContextBase(SPListItem item)
        {
            Title = item.Title;
        }
    }
}
