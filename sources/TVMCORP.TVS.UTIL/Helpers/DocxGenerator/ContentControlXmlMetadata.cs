﻿// ----------------------------------------------------------------------
// <copyright file="ContentControlXmlMetadata.cs" author="Atul Verma">
//     Copyright (c) Atul Verma. This utility along with samples demonstrate how to use the Open Xml 2.0 SDK and VS 2010 for document generation. They are unsupported, but you can use them as-is.
// </copyright>
// ------------------------------------------------------------------------

using TVMCORP.TVS.UTIL.Helper.DocXGenerator;
namespace TVMCORP.TVS.UTIL.Helper.DocXGenerator
{
    /// <summary>
    /// This class is used only for generic document generators that generate based on Xml, XPath and data bound content controls(optional)
    /// </summary>
    public class ContentControlXmlMetadata
    {
        public string PlaceHolderName;
        public PlaceHolderType Type;        
        public string ControlTagXPath;
        public string ControlValueXPath;
    }
}
