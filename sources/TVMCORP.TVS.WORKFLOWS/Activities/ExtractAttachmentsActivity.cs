using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.Util.Helpers;
using TVMCORP.TVS.Util.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.Util.Extensions;
namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class ExtractAttachmentsActivity : Activity
    {
        #region Dependency Properties
        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(ExtractAttachmentsActivity));

        [ValidationOption(ValidationOption.Required)]
        public Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties __ActivationProperties
        {
            get
            {
                return (Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties)base.GetValue(__ActivationPropertiesProperty);
            }
            set
            {
                base.SetValue(__ActivationPropertiesProperty, value);
            }
        }

        public static DependencyProperty ListIdProperty =
            DependencyProperty.Register("ListId",
            typeof(string), typeof(ExtractAttachmentsActivity));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get { return ((string)(base.GetValue(ListIdProperty))); }
            set { base.SetValue(ListIdProperty, value); }
        }


        public static DependencyProperty ListItemProperty =
            DependencyProperty.Register("ListItem",
            typeof(int), typeof(ExtractAttachmentsActivity));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get { return ((int)(base.GetValue(ListItemProperty))); }
            set { base.SetValue(ListItemProperty, value); }
        }


        public static DependencyProperty DestinationFolderUrlProperty =
            DependencyProperty.Register("DestinationFolderUrl",
            typeof(string), typeof(ExtractAttachmentsActivity));

        [Description("Destination list url")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string DestinationFolderUrl
        {
            get { return ((string)(base.GetValue(DestinationFolderUrlProperty))); }
            set { base.SetValue(DestinationFolderUrlProperty, value); }
        }

        public static DependencyProperty ContentTypeProperty =
            DependencyProperty.Register("ContentType",
            typeof(string), typeof(ExtractAttachmentsActivity));

        [Description("Content Type")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ContentType
        {
            get { return ((string)(base.GetValue(ContentTypeProperty))); }
            set { base.SetValue(ContentTypeProperty, value); }
        }

        public static DependencyProperty ExceptionChoiceProperty =
            DependencyProperty.Register("ExceptionChoice",
            typeof(string), typeof(ExtractAttachmentsActivity));

        [Description("Exception Choice")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ExceptionChoice
        {
            get { return ((string)(base.GetValue(ExceptionChoiceProperty))); }
            set { base.SetValue(ExceptionChoiceProperty, value); }
        }

        #endregion

        #region Fields
        private bool _throwException;
        
        [NonSerialized]
        private SPListItem _sourceListItem;
        
        [NonSerialized]
        private ActivityExecutionContext _executionContext;
        #endregion

        #region Execute Code

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            SPList destinationList = null;
            SPFolder destinationFolder = null;

            _executionContext = executionContext;
            _throwException = Convert.ToBoolean(ExceptionChoice);
            _sourceListItem = GetSourceListItem();
            if (_sourceListItem == null) return ActivityExecutionStatus.Closed;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite destinationSite = GetDestinationSite())
                {
                    if (destinationSite == null) return;

                    using (SPWeb destinationWeb = GetDestinationWeb(destinationSite))
                    {
                        if (destinationWeb == null) return;

                        destinationList = GetDestinationList(destinationWeb);
                        if (destinationList == null) return;

                        destinationWeb.AllowUnsafeUpdates = true;

                        destinationFolder = GetDestinationFolder(destinationWeb, destinationList);
                        if (destinationFolder == null || !destinationFolder.Exists) return;

                        SPContentType destinationContentType = GetDestinationContentType(destinationFolder);
                        if (destinationContentType == null) return;

                        SPFolder sourceListItemAttachmentsFolder = GetSourceItemAttachmentFolder();
                        if (sourceListItemAttachmentsFolder == null) return;

                        foreach (SPFile attachedFile in sourceListItemAttachmentsFolder.Files)
                        {
                            SPListItem destDocument = CopyFile(attachedFile, destinationFolder, destinationContentType);
                            //UpdateDestDocumentProperties(destDocument);
                            string[] ignoreFields = new string[] { "Title", "ContentType", "Content Type", "Name" };
                            _sourceListItem.CopyMetadataTo(destDocument, ignoreFields);
                        }

                        destinationWeb.AllowUnsafeUpdates = false;

                        __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, __ActivationProperties.Web.CurrentUser, "The attachments have been copied into the " + destinationList.Title, string.Empty);
                    }
                }
            });

            return ActivityExecutionStatus.Closed;
        }

        private SPSite GetDestinationSite()
        {
            SPSite destinationSite = null;
            if (Utility.IsAbsoluteUri(DestinationFolderUrl))
                try
                {
                    //open Site
                    destinationSite = new SPSite(DestinationFolderUrl);
                }
                catch
                {
                    //does not exist site
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The URL " + DestinationFolderUrl + " does not exist", string.Empty);
                }
            else
                destinationSite = __ActivationProperties.Site;

            return destinationSite;
        }

        
        private SPWeb GetDestinationWeb(SPSite destinationSite)
        {
            SPWeb destinationWeb = null;
            if (!Utility.IsAbsoluteUri(DestinationFolderUrl))
                try
                {
                    destinationWeb = destinationSite.OpenWeb(DestinationFolderUrl, false);
                }
                catch
                {
                    //input wrong URL, can't open web
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The URL " + DestinationFolderUrl + " does not exist", string.Empty);
                }
            else
                destinationWeb = destinationSite.OpenWeb();
            
            return destinationWeb;
        }

        private SPList GetDestinationList(SPWeb destinationWeb)
        {
            SPList destinationList = null;
            SPFolder destinationFolder = destinationWeb.GetFolder(DestinationFolderUrl);
            if (destinationFolder.ParentListId != Guid.Empty)
                destinationList = destinationWeb.Lists.GetList(destinationFolder.ParentListId, false);
            else
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The library does not exist", string.Empty);

            return destinationList;
        }


        private SPFolder GetDestinationFolder(SPWeb destinationWeb, SPList destinationList)
        {
            SPFolder destinationFolder = destinationWeb.GetFolder(DestinationFolderUrl);
            if (!destinationFolder.Exists)
            {
                    // create folder
                    string strFolders = DestinationFolderUrl.Substring(
                        DestinationFolderUrl.ToLower().IndexOf(destinationList.RootFolder.ServerRelativeUrl.ToLower()) + 
                        destinationList.RootFolder.ServerRelativeUrl.Length + 1);
                    
                    string[] arrFolder = strFolders.Split('/');
                    
                    for (int i = 0; i < arrFolder.Length; i++)
                    {
                        string strURLCreate = destinationList.RootFolder.ServerRelativeUrl;
                        for (int j = 0; j <= i; j++)
                        {
                            strURLCreate += "/" + arrFolder[j];
                        }
                        destinationWeb.Folders.Add(strURLCreate);
                    }

                destinationFolder = destinationWeb.GetFolder(DestinationFolderUrl);
            }
            
            //In case of Forms folder
            if (destinationFolder.Properties["vti_listbasetype"] == null)
            {
                //Error when the hidden folder of document
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Cannot place files in to " + DestinationFolderUrl, string.Empty);
                return null;
            }
            
            return destinationFolder;
        }

        private SPListItem GetSourceListItem()
        {
            SPListItem ItemGet = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(__ActivationProperties.Site.ID))
                {
                    using (SPWeb web = site.AllWebs[__ActivationProperties.Web.ID])
                    {
                        try
                        {
                            SPList list = web.Lists[new Guid(ListId)];
                            ItemGet = list.Items.GetItemById(ListItem);
                        }
                        catch
                        {
                            if (_throwException)
                            {
                                string msg = "There is no item exception";
                                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, __ActivationProperties.Web.CurrentUser, msg, string.Empty);
                                throw new SPException(msg);
                            }
                            else
                                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The item does not exist", string.Empty);
                        }
                    }
                }
            });
            return ItemGet;
        }

        private SPFolder GetSourceItemAttachmentFolder()
        {
            SPFolder temp = _sourceListItem.Web.GetFolder("Lists");
            SPFolder sourceListItemFolder = temp.SubFolders[_sourceListItem.ParentList.RootFolder.Name];
            SPFolder retFolder = null;
            bool hasAttachment = false;

            try
            {
                retFolder = sourceListItemFolder.SubFolders["Attachments"].SubFolders[_sourceListItem.ID.ToString()];
                hasAttachment = (retFolder.Files.Count > 0);
            }
            catch (ArgumentException)
            {
                //log bellow
            }

            if (!hasAttachment)
            {
                string msg = "There is no attachment exception";
                if (_throwException)
                {
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, __ActivationProperties.Web.CurrentUser, msg, string.Empty);
                    throw new SPException(msg);
                }
                else
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "No attachments were extracted", string.Empty);
                
            }
            
            return retFolder;
            
        }

        private SPListItem CopyFile(SPFile sourceFile, SPFolder destFolder, SPContentType destContentType)
        {
            string filename = BuildDestinationFilename(sourceFile.Name, destFolder);
            byte[] content = sourceFile.OpenBinary();

            Hashtable fileProperties = new Hashtable();
            fileProperties["ContentType"] = destContentType.Name;
            SPFile destFile = destFolder.Files.Add(destFolder.ServerRelativeUrl + "/" + filename, content, fileProperties, true);
            return destFile.Item;
        }

        private string BuildDestinationFilename(string filename, SPFolder destFolder)
        {
            if (destFolder.FileExists(filename))
            {
                filename = string.Format("{0}_(ItemId_{1}){2}",
                    Path.GetFileNameWithoutExtension(filename),
                    _sourceListItem.ID,
                    Path.GetExtension(filename));
            }
            return filename;
        }

        private SPContentType GetDestinationContentType( SPFolder destFolder)
        {
            SPContentType getContentType;
            getContentType = destFolder.ContentTypeOrder.SingleOrDefault(ct => string.Compare(ct.Name, ContentType, true) == 0 );
            if (getContentType == null)
            {
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The " + ContentType + " content type does not exist on the destination library", string.Empty);
                return null;
            }
            return getContentType;
        }

         #endregion

    }
}
