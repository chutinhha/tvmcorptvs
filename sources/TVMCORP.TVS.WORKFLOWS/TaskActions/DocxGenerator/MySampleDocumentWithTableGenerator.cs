// ----------------------------------------------------------------------
// <copyright file="SampleDocumentWithTableGenerator.cs" author="Atul Verma">
//     Copyright (c) Atul Verma. This utility along with samples demonstrate how to use the Open Xml 2.0 SDK and VS 2010 for document generation. They are unsupported, but you can use them as-is.
// </copyright>
// ------------------------------------------------------------------------
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using WordDocumentGenerator.Library;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using System;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class Phone
    {

        public string Label { get; set; }
        public string Number { get; set; }

    }
    public class MyAddress
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }
        public List<Phone> Phones { get; set; }
    }

    /// <summary>
    /// Sample refreshable document generator for Test_Template - 2.docx(has table) template
    /// </summary>
    public class MySampleDocumentWithTableGenerator : BaseDoxGenerator
    {
        // Content Control Tags - Table tags are different. Other Tags are same so reusing the base class's code
        protected const string PhoneNumberRow = "PhoneNumberRow";
        protected const string PhoneLabel = "PhoneLabel";
        protected const string PhoneNumber = "PhoneNumber";
        protected const string MyName = "MYNAME";
        protected const string SignedDate = "SIGNEDDATE";
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDocumentWithTableGenerator"/> class.
        /// </summary>
        /// <param name="generationInfo">The generation info.</param>
        public MySampleDocumentWithTableGenerator(DocumentGenerationInfo generationInfo)
            : base(generationInfo)
        {
        }

        #endregion

        #region Overridden methods

        /// <summary>
        /// Gets the place holder tag to type collection.
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, PlaceHolderType> GetPlaceHolderTagToTypeCollection()
        {
            Dictionary<string, PlaceHolderType> placeHolderTagToTypeCollection = base.GetPlaceHolderTagToTypeCollection();

            // Handle recursive placeholders            
            placeHolderTagToTypeCollection.Add(PhoneNumberRow, PlaceHolderType.Recursive);

            // Handle non recursive placeholders
            placeHolderTagToTypeCollection.Add(PhoneLabel, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(PhoneNumber, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(MyName, PlaceHolderType.NonRecursive);
            placeHolderTagToTypeCollection.Add(SignedDate, PlaceHolderType.NonRecursive);

            return placeHolderTagToTypeCollection;
        }

        /// <summary>
        /// Non recursive placeholder found.
        /// </summary>
        /// <param name="placeholderTag">The placeholder tag.</param>
        /// <param name="openXmlElementDataContext">The open XML element data context.</param>
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
            bool bubblePlaceHolder = true;

            switch (tagPlaceHolderValue)
            {
                case PhoneLabel:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as Phone).Label.ToString();
                    content = tagValue;
                    break;
                case PhoneNumber:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as Phone).Number.ToString();
                    content = ((openXmlElementDataContext.DataContext) as Phone).Number;
                    break;
                case MyName:
                    bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as MyAddress).Name.ToString();
                    content = ((openXmlElementDataContext.DataContext) as MyAddress).Name;
                    break;

                case SignedDate:
                     bubblePlaceHolder = false;
                    tagValue = ((openXmlElementDataContext.DataContext) as MyAddress).Date.ToString();
                    content = ((openXmlElementDataContext.DataContext) as MyAddress).Date.ToString();
                    break;
            }

            if (bubblePlaceHolder)
            {
                // Use base class code as tags are already defined in base class.
                base.NonRecursivePlaceholderFound(placeholderTag, openXmlElementDataContext);
            }
            else
            {
                // Set the tag for the content control
                if (!string.IsNullOrEmpty(tagValue))
                {
                    this.SetTagValue(openXmlElementDataContext.Element as SdtElement, GetFullTagValue(tagPlaceHolderValue, tagValue));
                }

                // Set the content for the content control
                this.SetContentOfContentControl(openXmlElementDataContext.Element as SdtElement, content);
            }
        }

        /// <summary>
        /// Recursive placeholder found.
        /// </summary>
        /// <param name="placeholderTag">The placeholder tag.</param>
        /// <param name="openXmlElementDataContext">The open XML element data context.</param>
        protected override void RecursivePlaceholderFound(string placeholderTag, OpenXmlElementDataContext openXmlElementDataContext)
        {
            if (openXmlElementDataContext == null || openXmlElementDataContext.Element == null || openXmlElementDataContext.DataContext == null)
            {
                return;
            }

            string tagPlaceHolderValue = string.Empty;
            string tagGuidPart = string.Empty;
            GetTagValue(openXmlElementDataContext.Element as SdtElement, out tagPlaceHolderValue, out tagGuidPart);

            // Reuse base class code and handle only tags unavailable in base class
            bool bubblePlaceHolder = true;

            switch (tagPlaceHolderValue)
            {
                case PhoneNumberRow:
                    bubblePlaceHolder = false;

                    foreach (Phone testB in ((openXmlElementDataContext.DataContext) as MyAddress).Phones)
                    {
                        SdtElement clonedElement = this.CloneElementAndSetContentInPlaceholders(new OpenXmlElementDataContext() { Element = openXmlElementDataContext.Element, DataContext = testB });
                    }

                    openXmlElementDataContext.Element.Remove();
                    break;
            }

            if (bubblePlaceHolder)
            {
                // Use base class code as tags are already defined in base class.
                base.RecursivePlaceholderFound(placeholderTag, openXmlElementDataContext);
            }
        }

        #endregion
    }
}
