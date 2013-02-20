using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace TVMCORP.TVS.UTIL.MODELS
{
    [XmlInclude(typeof(AutoCreationSettings))]
    
    [XmlInclude(typeof(UnreadContentNotificationSetting))]
    [Serializable]

    public class SettingBase
    {
        public string Version { get; set; }

        public SettingBase()
        {
            Version = "I-Office v1.0.0.0";
        }
    }
}
