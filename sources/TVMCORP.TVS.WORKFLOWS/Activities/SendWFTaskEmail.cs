﻿using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.Helpers;
//using TVMCORP.TVS.WORKFLOWS.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public class SendWFTaskEmail: Activity
	{
        #region Dependency Properties

        public static DependencyProperty WorkflowPropertiesProperty =
           System.Workflow.ComponentModel.DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties),
           typeof(SendWFTaskEmail));
        [Description("Workflow properties ")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SPWorkflowActivationProperties WorkflowProperties
        {
            get
            {
                return ((SPWorkflowActivationProperties)(base.GetValue(SendWFTaskEmail.WorkflowPropertiesProperty)));
            }
            set
            {
                base.SetValue(SendWFTaskEmail.WorkflowPropertiesProperty, value);
            }
        }

        public static DependencyProperty TemplateListURLProperty =
            DependencyProperty.Register("TemplateListURL",
            typeof(string), typeof(SendWFTaskEmail));

        [Description("Template list URL")]
        [Category("Dependency Properties")]
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
            typeof(string), typeof(SendWFTaskEmail));

        [Description("Template name")]
        [Category("Dependency Properties")]
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
            typeof(string), typeof(SendWFTaskEmail));

        [Description("To")]
        [Category("Dependency Properties")]
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
            typeof(string), typeof(SendWFTaskEmail));

        [Description("CC")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string CC
        {
            get { return ((string)(base.GetValue(CCProperty))); }
            set { base.SetValue(CCProperty, value); }
        }

        public static DependencyProperty TaskListItemProperty =
          DependencyProperty.Register("TaskListItem",
          typeof(int), typeof(SendWFTaskEmail));

        [Description("ID of the task list item we are working with")]
        [Category("Dependency Properties")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TaskListItem
        {
            get { return ((int)(base.GetValue(TaskListItemProperty))); }
            set { base.SetValue(TaskListItemProperty, value); }
        }
        #endregion

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPListItem sourceListItem = WorkflowProperties.Item;
                if (sourceListItem == null) return;

                SPList emailTemplateList = WorkflowProperties.GetListFromURL(TemplateListURL.Split(',')[0]);
                if (emailTemplateList == null) return;

                SPListItemCollection emailListItems = emailTemplateList.FindItems("Title", TemplateName);
                if (emailListItems.Count == 0) return;
                SPListItem emailListItem = emailListItems[0];
                
                SPListItem taskListItem = WorkflowProperties.TaskList.GetItemById(TaskListItem);
                if (emailListItem == null) return;
                try
                {
                    SendEmailHelper.SendEmailbytemplate(sourceListItem, taskListItem, emailListItem, To, CC);
                }
                catch (Exception e)
                {
                    WorkflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, WorkflowProperties.Web.CurrentUser, "Email template " + TemplateName + " could not be located. Reason: " + e.ToString(), string.Empty);
                    return;
                }
                WorkflowProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.None, WorkflowProperties.Web.CurrentUser, "Email template " + TemplateName + " has been successfully sent to " + To
                    + (string.IsNullOrEmpty(CC) == false ? " and cc " + CC : string.Empty), string.Empty);
            });
            return ActivityExecutionStatus.Closed;
        }
	}
}
