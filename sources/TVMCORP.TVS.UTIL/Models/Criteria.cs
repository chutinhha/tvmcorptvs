using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
    public class Criteria
    {
        public string FieldId { get; set; }
        public Operators Operator { get; set; }
        public string Value { get; set; }
    }
}
