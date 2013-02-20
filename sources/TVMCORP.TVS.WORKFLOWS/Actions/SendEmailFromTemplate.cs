using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.Helpers;
//using TVMCORP.TVS.WORKFLOWS.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
	public partial class SendEmailFromTemplate: Activity
	{
        #region Dependency Properties
        public static DependencyProperty __ListIdProperty =
            DependencyProperty.Register("__ListId",
            typeof(string), typeof(SendEmailFromTemplate));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string __ListId
        {
            get { return ((string)(base.GetValue(__ListIdProperty))); }
            set { base.SetValue(__ListIdProperty, value); }
        }


        public static DependencyProperty __ListItemProperty =
            DependencyProperty.Register("__ListItem",
            typeof(int), typeof(SendEmailFromTemplate));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int __ListItem
        {
            get { return ((int)(base.GetValue(__ListItemProperty))); }
            set { base.SetValue(__ListItemProperty, value); }
        }


        public static DependencyProperty TemplateListURLProperty =
            DependencyProperty.Register("TemplateListURL",
            typeof(string), typeof(SendEmailFromTemplate));

        [Description("Template list URL")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string TemplateListURL
        {
            get { return ((string)(base.GetValue(TemplateListURLProperty))); }
            set { base.SetValue(TemplateListURLProperty, value); }
        }

        public static DependencyProperty TemplateNameProperty =
            DependencyProperty.Register("TemplateName",
            typeof(string), typeof(SendEmailFromTemplate));

        [Description("Template name")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string TemplateName
        {
            get { return ((string)(base.GetValue(TemplateNameProperty))); }
            set { base.SetValue(TemplateNameProperty, value); }
        }


        public static DependencyProperty ToProperty =
            DependencyProperty.Register("To",
            typeof(string), typeof(SendEmailFromTemplate));

        [Description("To")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string To
        {
            get { return ((string)(base.GetValue(ToProperty))); }
            set { base.SetValue(ToProperty, value); }
        }

        public static DependencyProperty CCProperty =
            DependencyProperty.Register("CC",
            typeof(string), typeof(SendEmailFromTemplate));

        [Description("CC")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]       
        [ValidationOption(ValidationOption.Optional)]
        public string CC
        {
            get { return ((string)(base.GetValue(CCProperty))); }
            set { base.SetValue(CCProperty, value); }
        }

        public static DependencyProperty VariablesProperty =
            DependencyProperty.Register("Variables",
            typeof(string), typeof(SendEmailFromTemplate));

        [Description("Variables")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Variables
        {
            get { return ((string)(base.GetValue(VariablesProperty))); }
            set { base.SetValue(VariablesProperty, value); }
        }

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(SendEmailFromTemplate));

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
        #endregion

        #region Execute Code
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPListItem sourceListItem = __ActivationProperties.GetListItem(__ListId, __ListItem);
                if (sourceListItem == null) return;

                SPList emailTemplateList = __ActivationProperties.GetListFromURL(TemplateListURL.Split(',')[0]);
                if (emailTemplateList == null) return;
                SPListItemCollection emailListItems = emailTemplateList.FindItems("Title", TemplateName);
                if (emailListItems.Count == 0) return;
                SPListItem emailListItem = emailListItems[0];

                if (emailListItem == null) return;
                try
                {
                    SendEmailHelper.SendEmailbytemplate(sourceListItem, emailListItem, To, CC, Variables);
                }
                catch (Exception e)
                {
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, __ActivationProperties.Web.CurrentUser, "Email template " + TemplateName + " could not be located. Reason: " + e.ToString(), string.Empty);
                    return;
                }
                __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, __ActivationProperties.Web.CurrentUser, "Email template:  \"" + TemplateName + "\" has been successfully sent to " + To
                    + (string.IsNullOrEmpty(CC) == false ? " and cc " + CC : string.Empty), string.Empty);
            });

            return ActivityExecutionStatus.Closed;
        }
        #endregion

    }
}
