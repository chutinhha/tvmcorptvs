﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVMCORP.TVS.UTIL.Helper.DocXGenerator
{
    [System.AttributeUsage(System.AttributeTargets.Property |
                         System.AttributeTargets.Struct,
                         AllowMultiple = true)  // multiuse attribute
    ]

    public class CellAttribute : Attribute
    {
        public string Index { get; set; }
        public PlaceHolderType Type { get; set; }

        public CellAttribute() { }
        public CellAttribute(string index)
        {
            Index = index;
        }
    }
}
