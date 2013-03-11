using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.SharePoint;
using WordDocumentGenerator.Library;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class GeneratorFactory
    {
        private static DocumentGenerationInfo GetDocumentGenerationInfo(string docType, string docVersion, SPListItem item, object dataContext, string fileName, bool useDataBoundControls)
        {
            DocumentGenerationInfo generationInfo = new DocumentGenerationInfo();
            generationInfo.Metadata = new DocumentMetadata() { DocumentType = docType, DocumentVersion = docVersion };
            generationInfo.DataContext = dataContext;
            //generationInfo.TemplateData = File.ReadAllBytes(Path.Combine("Sample Templates", fileName));

            if (UTIL.Utilities.Utility.IsAbsoluteUri(fileName))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(fileName))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPFile file = web.GetFile(fileName);
                            generationInfo.TemplateData = file.OpenBinary();
                        }
                    }
                });
            }
            else
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(item.Web.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(item.Web.ID))
                        {
                            SPFile file = web.GetFile(fileName);
                            generationInfo.TemplateData = file.OpenBinary();
                        }
                    }
                });
            }
           
            generationInfo.IsDataBoundControls = useDataBoundControls;

            return generationInfo;
        }

        public static WordDocumentGenerator.Library.DocumentGenerator GetGenerator(SPListItem item, string templateFile)
        {
             DocumentGenerationInfo generationInfo = GetDocumentGenerationInfo("DocumentWithTableGenerator", "1.0", item, GetDataContext(item),
                                                    templateFile, false);

            return new PhieuMuaHangGenerator(generationInfo);
        }

        private static object GetDataContext(SPListItem item)
        {
            var dc = new PhieuMuaHangDataContext(item);


            return dc;
        }

    }
}
