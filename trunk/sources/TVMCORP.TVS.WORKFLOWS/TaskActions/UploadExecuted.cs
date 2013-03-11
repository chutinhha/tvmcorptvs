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

                            if (exportLibrary != null)
                            {
                                SPFile exportedFile = exportLibrary.RootFolder.Files.Add(exportLibrary.RootFolder.Url + "/" + item.Title + ".docx", documentBytes, true);

                                SPListItem expItem = exportedFile.Item;

                                item["Title"] = item.Title;
                                item.Update();

                                exportLibrary.Update();
                            }
                        }
                    }
                    
                });
            }
            catch (Exception ex)
            {


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
    }
}
