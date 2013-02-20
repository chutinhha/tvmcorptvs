using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Utilities.Camlex;
using TVMCORP.TVS.UTIL.Entities;

namespace TVMCORP.TVS.UTIL.Services
{
    public class EmailListService
    {
        public static EmailListItem GetItemByKey(string key, SPWeb web)
        {
            SPList list = Utility.GetListFromURL(Constants.EMAIL_LIST_URL, web);
            if (list == null) return null;
            CAMLListQuery<EmailListItem> query = new CAMLListQuery<EmailListItem>(list);

            string caml = Camlex.Query().Where(x => (string)x[SPBuiltInFieldId.Title] == key).ToString();
            return query.ExecuteSingleQuery(caml);
        }

        public static EmailListItem GetItemByKey(string key)
        {
            return GetItemByKey(key, null);
        }

        public static List<EmailListItem> GetAllEmailListItems(SPWeb web)
        {
            SPList list = Utility.GetListFromURL(Constants.EMAIL_LIST_URL, web);
            if (list == null) return null;
            CAMLListQuery<EmailListItem> query = new CAMLListQuery<EmailListItem>(list);
                        
            return query.ExecuteListQuery(string.Empty);
        }

        public static List<EmailListItem> GetAllEmailListItems()
        {
            return GetAllEmailListItems(null);
        }
    }
}
