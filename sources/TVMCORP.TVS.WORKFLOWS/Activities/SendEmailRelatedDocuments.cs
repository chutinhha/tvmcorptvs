using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Common.Helpers;
using Hypertek.IOffice.DocuSign.Activities;
using Hypertek.IOffice.Model.DocuSign;
using Microsoft.SharePoint;
using Hypertek.IOffice.Model.Workflow;

namespace Hypertek.IOffice.Workflow.Activities
{
	public partial class SendEmailRelatedDocuments: DocuSignBaseActivity
	{
        public static readonly DependencyProperty SendRelatedDocumentsSettingsProperty =
           DependencyProperty.Register("SendRelatedDocumentsSettings", typeof(SendRelatedDocumentsSettings), typeof(SendEmailRelatedDocuments));

        public SendRelatedDocumentsSettings SendRelatedDocumentsSettings
        {
            get { return (SendRelatedDocumentsSettings)GetValue(SendRelatedDocumentsSettingsProperty); }
            set { SetValue(SendRelatedDocumentsSettingsProperty, value); }
        }

        public static DependencyProperty SignersProperty =
           DependencyProperty.Register("Signers",
           typeof(List<Signer>), typeof(SendEmailRelatedDocuments));

        [Description("")]
        //[ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<Signer> Signers
        {
            get { return ((List<Signer>)(base.GetValue(SignersProperty))); }
            set { base.SetValue(SignersProperty, value); }
        }

     
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                SPList emailTemplateList = __ActivationProperties.GetListFromURL(this.SendRelatedDocumentsSettings.EmailTemplateListUrl);
                if (emailTemplateList == null) return;
                SPListItemCollection emailListItems = emailTemplateList.FindItems("Title", this.SendRelatedDocumentsSettings.EmailTemplateName);
                if (emailListItems.Count == 0) return;
                SPListItem emailListItem = emailListItems[0];
                
                if (emailListItem == null) return;
                List<SPFile> attachments = Document.GetRelateddocuments();
               
                    attachments = validateAttachment(attachments);
                    if (attachments == null || attachments.Count == 0)
                    {
                        base.LogComment("No related documents found");
                    }
                    else
                    {
                        SendEmailHelper.SendEmailRelatedDocuments(this.Document, emailListItem, this.Signers, attachments);
                        StringBuilder sb = new StringBuilder();
                        foreach (Signer signer in Signers)
                        {
                            sb.AppendFormat("{0};", signer.Email);
                        }
                        base.LogComment("Send related documents to : " + sb.ToString().TrimEnd(new char[] { ';' }));

                    }
                }
                catch
                {
                    base.LogError("An error occured while send email related documents as attachment");
                    return;
                }
                
            });
            return ActivityExecutionStatus.Closed;
        }

        private List<SPFile> validateAttachment(List<SPFile> attachments)
        {
            if (attachments == null) return null;
            var query = attachments.Where(p => p.Length < this.SendRelatedDocumentsSettings.AttachmentSizeLimit * 1024)
                                   .OrderBy(p => p.Length)
                                   .Take(this.SendRelatedDocumentsSettings.MaximunAttachments);


            return query.ToList();
        }
		public SendEmailRelatedDocuments()
		{
			InitializeComponent();
		}
	}
}
