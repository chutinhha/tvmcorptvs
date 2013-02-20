using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using System.ComponentModel;
using Microsoft.SharePoint.WorkflowActions;
using System.Workflow.ComponentModel.Compiler;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using System.Collections;

namespace TVMCORP.TVS.WORKFLOWS.Activities.DP
{
    public class SendMailWHttpFileAttachment : Activity
    {

        #region Dependency Properties

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(SendMailWHttpFileAttachment));


        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(SendMailWHttpFileAttachment.__ContextProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.__ContextProperty, value);
            }
        }

        public static DependencyProperty RecipientFromProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientFrom", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("Sender address. If this value is not specified, default sharepoint sender address will be used")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RecipientFrom
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.RecipientFromProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.RecipientFromProperty, value);
            }
        }

        public static DependencyProperty RecipientTOProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientTO", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("Recipient address")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RecipientTO
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.RecipientTOProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.RecipientTOProperty, value);
            }
        }

        public static DependencyProperty RecipientCCProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientCC", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("Carbon copy recipient")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RecipientCC
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.RecipientCCProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.RecipientCCProperty, value);
            }
        }


        public static DependencyProperty AttachmentWebUrlProperty = System.Workflow.ComponentModel.DependencyProperty.Register("AttachmentWebUrl", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("url of the file to be downloaded and attached to the email. Both http and https requests are supported")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AttachmentWebUrl
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.AttachmentWebUrlProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.AttachmentWebUrlProperty, value);
            }
        }

        public static DependencyProperty AttachmentFileNameProperty = System.Workflow.ComponentModel.DependencyProperty.Register("AttachmentFileName", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("Name of the attachment. This is very important. Extention of the file will determine which program will open the attachment.")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AttachmentFileName
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.AttachmentFileNameProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.AttachmentFileNameProperty, value);
            }
        }
        public static DependencyProperty SubjectProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Subject", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Subject
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.SubjectProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.SubjectProperty, value);
            }
        }

        public static DependencyProperty BodyProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Body", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("HTML body of the message")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Body
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.BodyProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.BodyProperty, value);
            }
        }

        public static DependencyProperty IsMessageUrgentProperty = System.Workflow.ComponentModel.DependencyProperty.Register("IsMessageUrgent", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("Determines whether message is priority is high")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string IsMessageUrgent
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.IsMessageUrgentProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.IsMessageUrgentProperty, value);
            }
        }

        public static DependencyProperty ImpersonateSystemAccountProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ImpersonateSystemAccount", typeof(string), typeof(SendMailWHttpFileAttachment));

        [Description("Determines whether the file retrieval should run under user context or should impersonate sharepoint account")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ImpersonateSystemAccount
        {
            get
            {
                return ((string)(base.GetValue(SendMailWHttpFileAttachment.ImpersonateSystemAccountProperty)));
            }
            set
            {
                base.SetValue(SendMailWHttpFileAttachment.ImpersonateSystemAccountProperty, value);
            }
        } 
        #endregion

        private void InitializeComponent()
        {
            // 
            // SendMailWAttachment
            // 
            this.Name = "SendMailWHttpFileAttachment";

        }



        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            try
            {
                Stream myContent = null;

                string url = Common.ProcessStringField(executionContext, this.AttachmentWebUrl);

                
                if (bool.Parse(ImpersonateSystemAccount))
                {   //get file using Sharepoint application pool credentials
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                {


                    myContent = Common.GetHttpFileUsingDefaultCredentials(url);

                });

                }
                else
                    myContent = Common.GetHttpFileUsingDefaultCredentials(url);


                //need administrative credentials to get to Web Application Properties info
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite mySite = new SPSite(__Context.Site.ID))
                    {
                        //replace lookup variables with actual values
                        string from = Common.ProcessStringField(executionContext,this.RecipientFrom);

                        string to = Common.ProcessStringField(executionContext,this.RecipientTO);

                        string cc = Common.ProcessStringField(executionContext,this.RecipientCC);

                        string subject =  Common.ProcessStringField(executionContext,this.Subject);

                        string body = Common.ProcessStringField(executionContext,this.Body);

                        string attachName = Common.ProcessStringField(executionContext, this.AttachmentFileName);


                        Common.SendMailWithAttachment(mySite, from, to, cc, subject, body, myContent, attachName, bool.Parse(this.IsMessageUrgent));

                    }
                });

            }
            catch (Exception e)
            {
                Common.LogExceptionToWorkflowHistory(e, executionContext, base.WorkflowInstanceId);

                throw;
            }

            return base.Execute(executionContext);
        }

    

        
    }
}
