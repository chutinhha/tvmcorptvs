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
	public partial class DeleteListItemPermissionAssignment: Activity
	{
		public DeleteListItemPermissionAssignment()
		{
			InitializeComponent();
		}

        #region Properties

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(DeleteListItemPermissionAssignment));

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
            typeof(string), typeof(DeleteListItemPermissionAssignment));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(DeleteListItemPermissionAssignment.ListIdProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssignment.ListIdProperty, value);
            }
        }


        public static DependencyProperty ListItemProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ListItem",
            typeof(int), typeof(DeleteListItemPermissionAssignment));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(DeleteListItemPermissionAssignment.ListItemProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssignment.ListItemProperty, value);
            }
        }


        public static DependencyProperty UserNameProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("UserName",
            typeof(string), typeof(DeleteListItemPermissionAssignment));

        [Description("UserName")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserName
        {
            get
            {
                return ((string)(base.GetValue(DeleteListItemPermissionAssignment.UserNameProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssignment.UserNameProperty, value);
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
                            listItem.RemovePermissions(UserName);
                        }
                    }
                }
            });
            
            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowCompleted, __ActivationProperties.Web.CurrentUser, UserName + " permissions had been removed", string.Empty);
            return base.Execute(executionContext);
        }
	}
}
