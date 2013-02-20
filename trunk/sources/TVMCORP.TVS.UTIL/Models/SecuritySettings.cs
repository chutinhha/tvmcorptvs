using System;
using System.Collections.Generic;
using TVMCORP.TVS.UTIL.MODELS;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
    public class SecuritySettings
    {

        public SecuritySettings()
        {
            Rules = new List<Rule>();
        }

        public List<Rule> Rules { get; set; }
        
    }
}
