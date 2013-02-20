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

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities.DP
{
    public class SendMailWListItemAttachments : Activity
    {

        #region Dependency Properties

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(SendMailWListItemAttachments));


        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(SendMailWListItemAttachments.__ContextProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.__ContextProperty, value);
            }
        }

        public static DependencyProperty RecipientFromProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientFrom", typeof(string), typeof(SendMailWListItemAttachments));

        [Description("Sender address. If this value is not specified, default sharepoint sender address will be used")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RecipientFrom
        {
            get
            {
                return ((string)(base.GetValue(SendMailWListItemAttachments.RecipientFromProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.RecipientFromProperty, value);
            }
        }

        public static DependencyProperty RecipientTOProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientTO", typeof(ArrayList), typeof(SendMailWListItemAttachments));

        [Description("Recipient address")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArrayList RecipientTO
        {
            get
            {
                return ((ArrayList)(base.GetValue(SendMailWListItemAttachments.RecipientTOProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.RecipientTOProperty, value);
            }
        }

        public static DependencyProperty RecipientCCProperty = System.Workflow.ComponentModel.DependencyProperty.Register("RecipientCC", typeof(ArrayList), typeof(SendMailWListItemAttachments));

        [Description("Carbon copy recipient")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArrayList RecipientCC
        {
            get
            {
                return ((ArrayList)(base.GetValue(SendMailWListItemAttachments.RecipientCCProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.RecipientCCProperty, value);
            }
        }

        public static DependencyProperty SubjectProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Subject", typeof(string), typeof(SendMailWListItemAttachments));

        [Description("")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Subject
        {
            get
            {
                return ((string)(base.GetValue(SendMailWListItemAttachments.SubjectProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.SubjectProperty, value);
            }
        }

        public static DependencyProperty BodyProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Body", typeof(string), typeof(SendMailWListItemAttachments));

        [Description("HTML body of the message")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Body
        {
            get
            {
                return ((string)(base.GetValue(SendMailWListItemAttachments.BodyProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.BodyProperty, value);
            }
        }

        public static DependencyProperty IsMessageUrgentProperty = System.Workflow.ComponentModel.DependencyProperty.Register("IsMessageUrgent", typeof(string), typeof(SendMailWListItemAttachments));

        [Description("Determines whether message is priority is high")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string IsMessageUrgent
        {
            get
            {
                return ((string)(base.GetValue(SendMailWListItemAttachments.IsMessageUrgentProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.IsMessageUrgentProperty, value);
            }
        }


        public static DependencyProperty ListIdProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListId", typeof(string), typeof(SendMailWListItemAttachments));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(SendMailWListItemAttachments.ListIdProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.ListIdProperty, value);
            }
        }

        public static DependencyProperty ListItemProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListItem", typeof(int), typeof(SendMailWListItemAttachments));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(SendMailWListItemAttachments.ListItemProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.ListItemProperty, value);
            }
        }

        public static DependencyProperty SendIfNoAttachmentsProperty = System.Workflow.ComponentModel.DependencyProperty.Register("SendIfNoAttachments", typeof(bool), typeof(SendMailWListItemAttachments));

        [Description("Determine whether to send an email when no attachments are found")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool SendIfNoAttachments
        {
            get
            {
                return ((bool)(base.GetValue(SendMailWListItemAttachments.SendIfNoAttachmentsProperty)));
            }
            set
            {
                base.SetValue(SendMailWListItemAttachments.SendIfNoAttachmentsProperty, value);
            }
        }

        #endregion

        private void InitializeComponent()
        {
            // 
            // SendMailWAttachment
            // 
            this.Name = "SendMailWListItemAttachments";

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
                            SPList myList = myWeb.Lists[new Guid(this.ListId)];

                            SPListItem myItem = myList.Items.GetItemById(this.ListItem);


                            string to = this.RecipientTO == null ? string.Empty : Common.ParseSendTo(this.__Context, this.RecipientTO);

                            string cc = this.RecipientCC == null ? string.Empty : Common.ParseSendTo(this.__Context ,this.RecipientCC);

                            to = Common.ProcessStringField(executionContext, to);

                            cc = Common.ProcessStringField(executionContext, cc);

                            string from = Common.ProcessStringField(executionContext, this.RecipientFrom);

                            string subject = Common.ProcessStringField(executionContext, this.Subject);

                            string body = Common.ProcessStringField(executionContext, this.Body);
                     
                            AttachmentInfo[] myLIAttachments = Common.GetListItemAttachments(myItem);

                            if (myLIAttachments.Length > 0 || this.SendIfNoAttachments)
                                Common.SendMailWithAttachment(mySite, from, to, cc, subject, body, myLIAttachments, bool.Parse(this.IsMessageUrgent));
                        }
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
