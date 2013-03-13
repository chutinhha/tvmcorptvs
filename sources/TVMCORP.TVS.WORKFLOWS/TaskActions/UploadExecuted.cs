using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using WordDocumentGenerator.Library;
using TVMCORP.TVS.UTIL.Utilities;
using Microsoft.Office.Word.Server.Service;
using Microsoft.Office.Word.Server.Conversions;
using Microsoft.SharePoint.Administration;
using System.IO;
using System.Threading;
using System.Web;

namespace TVMCORP.TVS.WORKFLOWS.TaskActions
{
    public class UploadExecuted : ITaskActionHandler
    {
        #region ITaskActionHandler Members
        public virtual void Execute(TaskActionArgs actionData)
        {
            try
            {
                UploadExecutedSettings setting = actionData.GetActionData<UploadExecutedSettings>();
                var item = actionData.WorkflowProperties.Item;
                DocumentGenerator generator = GeneratorFactory.GetGenerator(item, setting.TemplateFile);

                var documentBytes   = generator.GenerateDocument();

                //TODO: save to Target Lib

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(actionData.WorkflowProperties.SiteId))
                    {
                        using (SPWeb web = site.OpenWeb(actionData.WorkflowProperties.WebId))
                        {
                            SPList exportLibrary = Utility.GetListFromURL(setting.DestinationLib, web);
                            SPList tempLibrary = Utility.GetListFromURL("TempDocs", web);

                            if (exportLibrary != null && tempLibrary != null)
                            {
                                SPFile exportedFile = null;

                                //byte[] pdfBytes = ConvertDocument(web, documentBytes, item.Title, false, tempLibrary.Title, 240, true);
                                //exportedFile = exportLibrary.RootFolder.Files.Add(exportLibrary.RootFolder.Url + "/" + item.Title + ".pdf", pdfBytes, true);

                                if (setting.DocumentFormat.ToLower() == "pdf")
                                {
                                    byte[] pdfBytes = ConvertDocument(web, documentBytes, item.Title, true, tempLibrary.Title, 240, true);
                                    exportedFile = exportLibrary.RootFolder.Files.Add(exportLibrary.RootFolder.Url + "/" + item.Title + ".pdf", pdfBytes, true);
                                }
                                else
                                {
                                    //is pdf
                                    exportedFile = exportLibrary.RootFolder.Files.Add(exportLibrary.RootFolder.Url + "/" + item.Title + ".docx", documentBytes, true);
                                }

                                if (exportedFile != null)
                                {
                                    SPListItem expItem = exportedFile.Item;

                                    item["Title"] = item.Title;
                                    item.Update();

                                    exportLibrary.Update();
                                }
                            }
                        }
                    }
                    
                });
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, "UploadExecuted");

            }

        }
        #endregion

        public static SPWorkflowTaskCollection GetWorkflowTasks(SPWorkflow workflowInstance)
        {
            SPWorkflowTaskCollection taskCollection = null;
            bool tasksPopulated = false;
            while (!tasksPopulated)
            {
                try
                {
                    taskCollection = workflowInstance.Tasks;
                    tasksPopulated = true;
                }
                catch { }
            }

            return taskCollection;
        }

        private byte[] ConvertDocument(SPWeb web, byte[] docToConvert, string docFileName, bool isImmediate,
            String conversionLibName, int timeOutSecs, bool deleteDocs)
        {
            byte[] result = null;
            SPList conversionLib = web.Lists[conversionLibName];

            SPFolder folder = conversionLib.RootFolder;

            // Get the default proxy for the current Word Automation Services instance
            SPServiceContext serviceContext = SPServiceContext.GetContext(web.Site);
            WordServiceApplicationProxy wordServiceApplicationProxy =
                (WordServiceApplicationProxy)serviceContext.GetDefaultProxy(typeof(WordServiceApplicationProxy));

            ConversionJob job = new ConversionJob(wordServiceApplicationProxy);
            job.UserToken = web.CurrentUser.UserToken;
            job.Settings.UpdateFields = true;
            job.Settings.OutputSaveBehavior = SaveBehavior.AlwaysOverwrite;
            job.Settings.OutputFormat = SaveFormat.PDF;

            //String docFileName = Guid.NewGuid().ToString("D");
            docFileName = Guid.NewGuid().ToString("D");

            // we replace possible existing files on upload
            // although there is a minimal chance for GUID duplicates :-)
            SPFile docFile = folder.Files.Add(docFileName + ".docx", docToConvert, true);
            conversionLib.AddItem(docFileName + ".docx", SPFileSystemObjectType.File);

            String docFileUrl = String.Format("{0}/{1}", web.Url, docFile.Url);
            String pdfFileUrl = String.Format("{0}/{1}.pdf", web.Url, docFile.Url.Substring(0, docFile.Url.Length - 5));

            job.AddFile(docFileUrl, pdfFileUrl);

            // let's do the job :-)
            // Start-SPTimerJob "Word Automation Services"
            job.Start();

            if (isImmediate)
            {
                //Feature scope must be Web Application to use this function
                StartServiceJob("Word Automation Services Timer Job");
            }

            ConversionJobStatus cjStatus = new ConversionJobStatus(wordServiceApplicationProxy, job.JobId, null);
            // set up timeout
            TimeSpan timeSpan = new TimeSpan(0, 0, timeOutSecs);
            DateTime conversionStarted = DateTime.Now;

            int finishedConversionCount = cjStatus.Succeeded + cjStatus.Failed;
            while ((finishedConversionCount != 1) && ((DateTime.Now - conversionStarted) < timeSpan))
            {
                // wait a sec.
                Thread.Sleep(1000);
                cjStatus = new ConversionJobStatus(wordServiceApplicationProxy, job.JobId, null);
                finishedConversionCount = cjStatus.Succeeded + cjStatus.Failed;
            }

            // timeouted -> cancel conversion
            if (finishedConversionCount != 1)
            {
                job.Cancel();
            }

            // we can output the possible failed conversion error(s)
            foreach (ConversionItemInfo cii in cjStatus.GetItems(ItemTypes.Failed))
            {
                Utility.LogError(string.Format("Failed conversion. Input file: '{0}'; Output file: '{1}'; Error code: '{2}'; Error message: '{3}';",
                    cii.InputFile, cii.OutputFile, cii.ErrorCode, cii.ErrorMessage), "Document Generator");
            }

            SPFile convertedFile = web.GetFile(pdfFileUrl);
            // shouldn't be null (unless there is a conversion error)
            // but we check for sure
            if ((convertedFile != null) && (convertedFile.Exists))
            {
                Stream pdfStream = convertedFile.OpenBinaryStream();

                result = new byte[pdfStream.Length];
                pdfStream.Read(result, 0, result.Length);

                // delete result doc if requested
                if (deleteDocs)
                {
                    convertedFile.Delete();
                }
            }

            // delete source doc if requested
            if (deleteDocs)
            {
                docFile.Delete();
            }

            return result;

        }

        //Feature scope must be Web Application to use this function
        private void StartServiceJob(string serviceTypeName, string jobTypeName)
        {
            SPFarm.Local.Services.ToList().ForEach(
                svc => svc.JobDefinitions.ToList().ForEach(
                    jd =>
                    {
                        if ((jd.TypeName == jobTypeName) && ((serviceTypeName == null) || (serviceTypeName == svc.TypeName)))
                        {
                            #region [prevent Access denied]
                            /*
                            //HttpContext.Current = null;
                            var remoteAdministratorAccessDenied = SPWebService.ContentService.RemoteAdministratorAccessDenied;
                            try
                            {
                                if (remoteAdministratorAccessDenied == true)
                                {
                                    SPWebService.ContentService.RemoteAdministratorAccessDenied = false;
                                    jd.RunNow();
                                }
                                else
                                {
                                    jd.RunNow();
                                }
                            }
                            catch (Exception ex)
                            { }
                            finally
                            {
                                SPWebService.ContentService.RemoteAdministratorAccessDenied = remoteAdministratorAccessDenied;
                            }
                            */
                            #endregion[prevent Access denied]


                            jd.RunNow();
                        }
                    }));
        }

        //Feature scope must be Web Application to use this function
        private void StartServiceJob(string jobTypeName)
        {
            StartServiceJob(null, jobTypeName);
        }

    }
}
