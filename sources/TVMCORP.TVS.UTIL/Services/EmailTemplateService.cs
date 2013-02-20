using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Entities;
using TVMCORP.TVS.UTIL.Utilities;
using TVMCORP.TVS.UTIL.Utilities.Camlex;

namespace TVMCORP.TVS.UTIL.Services
{
   public class EmailTemplateService
   {
       public static EmailTemplate GetTemplateByName(string url, string name)
       {
           var spList = Utility.GetListFromURL(url);
           CAMLListQuery<EmailTemplate> queryer = new CAMLListQuery<EmailTemplate>(spList);
           string CAML = Camlex.Query()
                                          .Where(x => (string)x["Title"] == name).ToString();

           return queryer.ExecuteSingleQuery(CAML);
       }
   }
}
