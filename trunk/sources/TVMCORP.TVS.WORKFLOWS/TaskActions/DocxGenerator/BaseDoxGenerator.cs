using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordDocumentGenerator.Library;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class BaseDoxGenerator: DocumentGenerator
    {
        protected const string Title = "Title";

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
            var dict =  new Dictionary<string, PlaceHolderType>();
            dict.Add(Title, PlaceHolderType.NonRecursive);
            return dict;
        }
        protected override void NonRecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            if (openXmlElementDataContext == null || openXmlElementDataContext.Element == null || openXmlElementDataContext.DataContext == null)
            {
                return;
            }

            string tagPlaceHolderValue = string.Empty;
            string tagGuidPart = string.Empty;
            GetTagValue(openXmlElementDataContext.Element as SdtElement, out tagPlaceHolderValue, out tagGuidPart);

            string tagValue = string.Empty;
            string content = string.Empty;

            // Reuse base class code and handle only tags unavailable in base class
            //bool bubblePlaceHolder = true;

            switch (tagPlaceHolderValue)
            {
                case Title:
                    //bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as PhieuMuaHangDataContext).Title.ToString();
                    content = tagValue;
                    break;
            }

            //if (bubblePlaceHolder)
            //{
            //    // Use base class code as tags are already defined in base class.
            //    base.NonRecursivePlaceholderFound(placeholderTag, openXmlElementDataContext);
            //}
            //else
            //{
                // Set the tag for the content control
                if (!string.IsNullOrEmpty(tagValue))
                {
                    this.SetTagValue(openXmlElementDataContext.Element as SdtElement, GetFullTagValue(tagPlaceHolderValue, tagValue));
                }

                // Set the content for the content control
                this.SetContentOfContentControl(openXmlElementDataContext.Element as SdtElement, content);
            //}   
        }
        protected override void RecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            
        }
        protected override void RefreshCharts(DocumentFormat.OpenXml.Packaging.MainDocumentPart mainDocumentPart)
        {
            
        }
    }
}
