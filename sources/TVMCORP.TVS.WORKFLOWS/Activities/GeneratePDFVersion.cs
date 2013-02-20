using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using Hypertek.IOffice.Common.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Common.Utilities;
using System.Workflow.Activities;
using Hypertek.IOffice.Model.Infrastructure;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using Microsoft.SharePoint.Utilities;
using Hypertek.IOffice.Model;
using DocumentFormat.OpenXml.Packaging;

namespace Hypertek.IOffice.Workflow.Activities
{
    public partial class GeneratePDFWorkflow : SequenceActivity
    {
        public GeneratePDFWorkflow()
        {
            InitializeComponent();
        }

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(GeneratePDFWorkflow));

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

        public static DependencyProperty __ListIdProperty =
            DependencyProperty.Register("__ListId",
            typeof(string), typeof(GeneratePDFWorkflow));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string __ListId
        {
            get { return ((string)(base.GetValue(__ListIdProperty))); }
            set { base.SetValue(__ListIdProperty, value); }
        }

        public static DependencyProperty PDFFileNameProperty =
           DependencyProperty.Register("PDFFileName",
           typeof(string), typeof(GeneratePDFWorkflow));

        [Description("PDF File Name Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string PDFFileName
        {
            get { return ((string)(base.GetValue(PDFFileNameProperty))); }
            set { base.SetValue(PDFFileNameProperty, value); }
        }

        public static DependencyProperty __ListItemProperty =
            DependencyProperty.Register("__ListItem",
            typeof(int), typeof(GeneratePDFWorkflow));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int __ListItem
        {
            get { return ((int)(base.GetValue(__ListItemProperty))); }
            set { base.SetValue(__ListItemProperty, value); }
        }

        #region Variables
        private string TEMP_PATH = "";
        private SPList CurrentList;
        private SPList DestinationLibrary;
        private SPFile CurrentFile;
        private SPListItem CurrentListItem;
        private CreatePDFDocumentSettings PDFDocumentSettings;
        private SPContentType DestinationContentType;
        #endregion


        private void PDFGeneration_ExecuteCode(object sender, EventArgs e)
        {
            TEMP_PATH = System.Environment.GetEnvironmentVariable("TEMP");
            if (!TEMP_PATH.EndsWith("\\")) TEMP_PATH += "\\";

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    using (SPSite site = new SPSite(__ActivationProperties.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(__ActivationProperties.Web.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            CurrentListItem = web.Lists.GetList(new Guid(__ListId), false).GetItemById(__ListItem);

                            if (CurrentListItem == null || CurrentListItem.File == null) return;

                            CurrentList = CurrentListItem.ParentList;
                            CurrentFile = CurrentListItem.File;
                            PDFDocumentSettings = CurrentListItem.ContentType.GetCustomSettings<CreatePDFDocumentSettings>(CCIappFeatureNames.CCIappInfrastructure, false);
                            DestinationLibrary = CCIUtility.GetListFromURL(PDFDocumentSettings.DestinationLibraryUrl);
                            DestinationContentType = getDestinationContentType();
                            createPDFVersion();
                            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowComment, __ActivationProperties.Web.CurrentUser, "Generate file " + PDFFileName, string.Empty);
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CCIUtility.LogError(ex.Message + ex.StackTrace, "Hypertek.IOffice.Workflow");
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Error occur during generate PDF version", string.Empty);
                }
            });
        }

        private void IsPDFGenerated(object sender, ConditionalEventArgs e)
        {
            e.Result = !string.IsNullOrEmpty(PDFFileName);
        }

        private SPContentType getDestinationContentType()
        {
            if (DestinationLibrary == null || string.IsNullOrEmpty(PDFDocumentSettings.DestinationContentTypeId)) return null;

            SPContentTypeId destinationConentTypeId = DestinationLibrary.FindContentType(PDFDocumentSettings.DestinationContentTypeId);

            if (destinationConentTypeId != SPContentTypeId.Empty)
                return DestinationLibrary.ContentTypes[destinationConentTypeId];
            else
                if (PDFDocumentSettings.EnsureContentType)
                {
                    destinationConentTypeId = DestinationLibrary.EnsureContentTypeInList(PDFDocumentSettings.DestinationContentTypeId);
                    return destinationConentTypeId != SPContentTypeId.Empty ? DestinationLibrary.ContentTypes[destinationConentTypeId] : null;
                }
                else
                    return null;
        }

        private void createPDFVersion()
        {
            object oMissing = System.Reflection.Missing.Value;
            Word.ApplicationClass wordApp = new Word.ApplicationClass();
            wordApp.Visible = false;
            Word.Document doc = null;
            try
            {
                SPFile workingField = CurrentFile;

                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(CurrentList.ParentWeb.ID))
                    {
                        workingField = web.GetFile(CurrentFile.Url);
                    }
                }

                Object filename = TEMP_PATH + workingField.Name;
                CCIUtility.LogInfo(filename.ToString(), "DEBUG");
                //int size = 10 * 1024;
                //using (Stream stream = CurrentFile.OpenBinaryStream())
                //{
                //    using (FileStream fs = new FileStream(filename.ToString(), FileMode.Create, FileAccess.Write))
                //    {
                //        byte[] buffer = new byte[size];
                //        while (stream.Read(buffer, 0, buffer.Length) > 0)
                //        {
                //            fs.Write(buffer, 0, buffer.Length);
                //        }
                //    }
                //}

                Stream stream = workingField.OpenBinaryStream();
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                var writer = File.OpenWrite(TEMP_PATH + workingField.Name);
                writer.Write(data, 0, data.Length);
                writer.Close();

                object outputFileName;
                object fileFormat = Word.WdSaveFormat.wdFormatPDF;
                string ext = Path.GetExtension(workingField.Name);

                switch (ext)
                {
                    case ".docx":
                        outputFileName = TEMP_PATH + workingField.Name.Replace(".docx", ".pdf");
                        break;
                    case ".dotx":
                        outputFileName = TEMP_PATH + workingField.Name.Replace(".dotx", ".pdf");
                        break;
                    default:
                        outputFileName = TEMP_PATH + workingField.Name.Replace(".doc", ".pdf");
                        break;
                }

                if (ext == ".docx")
                {
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filename.ToString(), true))
                    {
                        MainDocumentPart mainPart = wordDoc.MainDocumentPart;
                        if (mainPart.DocumentSettingsPart != null)
                        {
                            mainPart.DeletePart(mainPart.DocumentSettingsPart);
                        }
                        mainPart.Document.Save();
                    }
                }

                doc = wordApp.Documents.Open(ref filename, ref oMissing,
                                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                            ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                doc.Activate();
                doc.SaveAs(ref outputFileName,
                        ref fileFormat, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                addToPDFList(outputFileName);
            }
            catch (Exception ex)
            {
                CCIUtility.LogInfo(ex.Message, "Create PFD");
            }
            finally
            {
                if (doc != null)
                {
                    object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                    ((Word._Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
                    doc = null;
                }

                if (wordApp != null)
                {
                    ((Word._Application)wordApp).Quit(ref oMissing, ref oMissing, ref oMissing);
                    wordApp = null;
                }

                try
                {
                    if (File.Exists(TEMP_PATH + CurrentFile.Name))
                    {
                        File.Delete(TEMP_PATH + CurrentFile.Name);
                    }

                    if (File.Exists(TEMP_PATH + Path.GetFileNameWithoutExtension(CurrentFile.Name) + ".pdf"))
                    {
                        File.Delete(TEMP_PATH + Path.GetFileNameWithoutExtension(CurrentFile.Name) + ".pdf");
                    }
                }
                catch (Exception)
                {
                    CCIUtility.LogError("Couldn't delete generated pdf file", CCIappFeatureNames.CCIappInfrastructure);
                }
            }
        }

        private void addToPDFList(object outputFileName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(CurrentList.ParentWeb.ID))
                    {
                        web.AllowUnsafeUpdates = true;
                        SPDocumentLibrary lib = (SPDocumentLibrary)web.GetListFromUrl(DestinationLibrary.DefaultViewUrl);
                        Stream streamTempFile = new FileStream(outputFileName.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
                        string filename = CurrentFile.Item.GetFormulaValue(PDFDocumentSettings.TitleFormula);
                        filename = SPEncode.HtmlDecode(filename) + ".pdf";
                        filename = filename.ConvertToValidSharePointFileName();

                        SPFile PDFFile = lib.RootFolder.Files.Add(filename, streamTempFile, true);
                        streamTempFile.Close();
                        copyMetadata(PDFFile.Item);
                        PDFFile.Item.SystemUpdate();
                        web.AllowUnsafeUpdates = false;

                        PDFFileName = filename;
                    }
                }
            });
        }

        private void copyMetadata(SPListItem item)
        {
            if (DestinationContentType != null)
                item.Properties["ContentTypeId"] = DestinationContentType.Id.ToString();

            if (PDFDocumentSettings.AllowCopyMetadata)
            {
                string[] ignoreFields = { "ContentType", "Content Type", "Name" };
                CurrentListItem.CopyMetadataTo(item, DestinationContentType, ignoreFields);
            }

            if (PDFDocumentSettings.CopySourceCNToParentCN)
                copyParentContractNumber(CurrentListItem, item);
        }

        private void copyParentContractNumber(SPListItem sourceItem, SPListItem destinationItem)
        {
            try
            {
                string contractNumberFieldName = sourceItem.Web.GetFeaturePropertyValue(Constants.Infrastructure.INFRASTRUCTURE_FEATURE_ID, Constants.Infrastructure.CONTRACT_NUMBER_FIELD_NAME);
                if (string.IsNullOrEmpty(contractNumberFieldName)
                    || !sourceItem.Fields.ContainsField(contractNumberFieldName)
                    || sourceItem[contractNumberFieldName] == null)
                    return;

                string parentContractNumberFieldName = sourceItem.Web.GetFeaturePropertyValue(Constants.Infrastructure.INFRASTRUCTURE_FEATURE_ID, Constants.Infrastructure.PARENT_CONTRACT_NUMBER_FIELD_NAME);
                if (string.IsNullOrEmpty(parentContractNumberFieldName) || !destinationItem.Fields.ContainsField(parentContractNumberFieldName))
                    return;

                destinationItem[parentContractNumberFieldName] = sourceItem[contractNumberFieldName];
                destinationItem.SystemUpdate();
            }
            catch (Exception ex)
            {
                CCIUtility.LogError(ex.Message + ex.StackTrace, "Corridor .app Workflow");
            }
        }

    }
   
}
