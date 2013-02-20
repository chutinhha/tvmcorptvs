using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
    public class RedirectSettings
    {
        public bool Enable { get; set; }
        public bool RedirectToHP { get; set; }
        public bool ShowError { get; set; }
        public string ErrorMessage { get; set; }
        public bool RedirectToUrl { get; set; }
        public string TargetUrl { get; set; }

    }
}
