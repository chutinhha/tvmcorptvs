using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.Extensions;

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities.WorkflowActions
{
	public partial class GrantPermissionsOnItem: Activity
	{
		public GrantPermissionsOnItem()
		{
			InitializeComponent();
        }
        
        #region Properties

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(GrantPermissionsOnItem));

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
            System.Workflow.ComponentModel.DependencyProperty.Register("ListId",
            typeof(string), typeof(GrantPermissionsOnItem));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(GrantPermissionsOnItem.ListIdProperty)));
            }
            set
            {
                base.SetValue(GrantPermissionsOnItem.ListIdProperty, value);
            }
        }
        
        public static DependencyProperty ListItemProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ListItem",
            typeof(int), typeof(GrantPermissionsOnItem));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(GrantPermissionsOnItem.ListItemProperty)));
            }
            set
            {
                base.SetValue(GrantPermissionsOnItem.ListItemProperty, value);
            }
        }


        public static DependencyProperty UserNameProperty = 
            System.Workflow.ComponentModel.DependencyProperty.Register("UserName",
            typeof(string), typeof(GrantPermissionsOnItem));

        [Description("UserName")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserName
        {
            get
            {
                return ((string)(base.GetValue(GrantPermissionsOnItem.UserNameProperty)));
            }
            set
            {
                base.SetValue(GrantPermissionsOnItem.UserNameProperty, value);
            }
        }


        public static DependencyProperty PermissionLevelProperty = 
            System.Workflow.ComponentModel.DependencyProperty.Register("PermissionLevel",
            typeof(string), typeof(GrantPermissionsOnItem));

        [Description("PermissionLevel")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PermissionLevel
        {
            get
            {
                return ((string)(base.GetValue(GrantPermissionsOnItem.PermissionLevelProperty)));
            }
            set
            {
                base.SetValue(GrantPermissionsOnItem.PermissionLevelProperty, value);
            }
        }
        #endregion

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
             {
                 using (SPSite site = new SPSite(__ActivationProperties.Site.ID))
                 {
                     using (SPWeb web = site.OpenWeb(__ActivationProperties.Web.ID))
                     {
                         SPList list = web.Lists.GetList(new Guid(this.ListId), false);
                         SPListItem listItem = list.GetItemById(this.ListItem);

                         if (!site.SystemAccount.LoginName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase))
                         {
                             try
                             {
                                 listItem.RemovePermissions(UserName);
                                 listItem.SetPermissions(PermissionLevel, UserName, true);

                                 __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowCompleted, __ActivationProperties.Web.CurrentUser, PermissionLevel + " had been granted to " + UserName, string.Empty);
                             }
                             catch (Exception ex)
                             {
                                 __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, String.Format("Unable to grant {0} to {1} because of error: {2}.", PermissionLevel, UserName, ex.Message), string.Empty);
                             }
                         }                         
                     }
                 }
             });

            return base.Execute(executionContext);
        }
    }
}
