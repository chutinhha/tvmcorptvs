using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordDocumentGenerator.Library;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions.DocxGenerator
{
    public class BaseDoxGenerator: DocumentGenerator
    {
        public  BaseDoxGenerator(DocumentGenerationInfo info) : base( info){
        
        }

        protected override void IgnorePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            
        }
        protected override void ContainerPlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            
        }
        protected override Dictionary<string, PlaceHolderType> GetPlaceHolderTagToTypeCollection()
        {
            return new Dictionary<string, PlaceHolderType>();
        }
        protected override void NonRecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            
        }
        protected override void RecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            
        }
        protected override void RefreshCharts(DocumentFormat.OpenXml.Packaging.MainDocumentPart mainDocumentPart)
        {
            
        }
    }
}
