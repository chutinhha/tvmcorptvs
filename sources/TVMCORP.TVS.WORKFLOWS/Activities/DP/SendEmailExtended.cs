using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WorkflowActions;

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities.DP
{
	public partial class SendEmailExtended: Activity
	{
        public static DependencyProperty RecipientTOProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientTO", typeof(ArrayList), typeof(SendEmailExtended));

        [Description("Recipient address")]
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArrayList RecipientTO
        {
            get
            {
                return ((ArrayList)(base.GetValue(SendEmailExtended.RecipientTOProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.RecipientTOProperty, value);
            }
        }

        public static DependencyProperty RecipientCCProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientCC", typeof(ArrayList), typeof(SendEmailExtended));

        [Description("Carbon copy recipient")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArrayList RecipientCC
        {
            get
            {
                return ((ArrayList)(base.GetValue(SendEmailExtended.RecipientCCProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.RecipientCCProperty, value);
            }
        }

        public static DependencyProperty SubjectProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Subject", typeof(string), typeof(SendEmailExtended));

        [Description("")]
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Subject
        {
            get
            {
                return ((string)(base.GetValue(SendEmailExtended.SubjectProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.SubjectProperty, value);
            }
        }

        public static DependencyProperty BodyProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Body", typeof(string), typeof(SendEmailExtended));

        [Description("HTML body of the message")]
      
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Body
        {
            get
            {
                return ((string)(base.GetValue(SendEmailExtended.BodyProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.BodyProperty, value);
            }
        }

        public static DependencyProperty IsMessageUrgentProperty = System.Workflow.ComponentModel.DependencyProperty.Register("IsMessageUrgent", typeof(string), typeof(SendEmailExtended));

        [Description("Determines whether message is priority is high")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string IsMessageUrgent
        {
            get
            {
                return ((string)(base.GetValue(SendEmailExtended.IsMessageUrgentProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.IsMessageUrgentProperty, value);
            }
        }
        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(SendEmailExtended));


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(SendEmailExtended.__ContextProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.__ContextProperty, value);
            }
        }

        public static DependencyProperty RecipientFromProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientFrom", typeof(string), typeof(SendEmailExtended));

        [Description("Sender address. If this value is not specified, default sharepoint sender address will be used")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RecipientFrom
        {
            get
            {
                return ((string)(base.GetValue(SendEmailExtended.RecipientFromProperty)));
            }
            set
            {
                base.SetValue(SendEmailExtended.RecipientFromProperty, value);
            }
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {

            try
            {

                //need administrative credentials to get to Web Application Properties info
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite mySite = new SPSite(__Context.Site.ID))
                    {
                        using (SPWeb myWeb = mySite.AllWebs[__Context.Web.ID])
                        {
                           
                            string to = this.RecipientTO == null ? string.Empty : Common.ParseSendTo(this.__Context, this.RecipientTO);

                            string cc = this.RecipientCC == null ? string.Empty : Common.ParseSendTo(this.__Context, this.RecipientCC);

                            to = Common.ProcessStringField(executionContext, to);

                            cc = Common.ProcessStringField(executionContext, cc);

                            string from = Common.ProcessStringField(executionContext, this.RecipientFrom);

                            string subject = Common.ProcessStringField(executionContext, this.Subject);

                            string body = Common.ProcessStringField(executionContext, this.Body);
                         
                           Common.SendMailWithAttachment(mySite, from, to, cc, subject, body, new  AttachmentInfo[0], bool.Parse(this.IsMessageUrgent));
                        }
                    }
                });

            }
            catch (Exception e)
            {
                Common.LogExceptionToWorkflowHistory(e, executionContext, base.WorkflowInstanceId);

                throw Common.WrapWithFriedlyException(e,"Error sending Email");
            }
            
            
            return base.Execute(executionContext);
        }    

		public SendEmailExtended()
		{
			InitializeComponent();
		}
	}
}
