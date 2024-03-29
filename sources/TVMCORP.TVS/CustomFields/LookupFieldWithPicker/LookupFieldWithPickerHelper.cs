﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using System.Web;

namespace TVMCORP.TVS.CustomFields
{
    public class LookupFieldWithPickerHelper
    {
        public static bool IsSearchableField(SPField field)
        {
            return (field.Id==SPBuiltInFieldId.FileLeafRef || field.Hidden == false && 
                       (field.Type == SPFieldType.Counter ||
                       field.InternalName == "ContentType"
                        || field.Type == SPFieldType.Boolean
                        || field.Type == SPFieldType.Integer
                        || field.Type == SPFieldType.Currency
                        || field.Type == SPFieldType.DateTime
                        || field.Type == SPFieldType.Number
                        || field.Type == SPFieldType.Text
                        || field.Type == SPFieldType.URL
                        || field.Type == SPFieldType.User
                        || field.Type == SPFieldType.Choice
                        || field.Type == SPFieldType.MultiChoice
                        || field.Type == SPFieldType.Lookup
                        || (field.Type == SPFieldType.Calculated))
                        );
        }

        public static string GetResourceString(string key)
        {
            string resourceClass = "TVMCORP.TVS.CustomFields.LookupFieldWithPicker";
            string value = HttpContext.GetGlobalResourceObject(resourceClass, key).ToString();
            return value;
        }
    }
}
