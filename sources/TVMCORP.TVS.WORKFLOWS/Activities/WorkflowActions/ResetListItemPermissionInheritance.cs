﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.Extensions;

namespace TVMCORP.TVS.WORKFLOWS.Activities.WorkflowActions
{
	public partial class ResetListItemPermissionInheritance: Activity
	{
		public ResetListItemPermissionInheritance()
		{
			InitializeComponent();
		}


        #region Properties

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(ResetListItemPermissionInheritance));

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
            typeof(string), typeof(ResetListItemPermissionInheritance));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(ResetListItemPermissionInheritance.ListIdProperty)));
            }
            set
            {
                base.SetValue(ResetListItemPermissionInheritance.ListIdProperty, value);
            }
        }


        public static DependencyProperty ListItemProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ListItem",
            typeof(int), typeof(ResetListItemPermissionInheritance));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(ResetListItemPermissionInheritance.ListItemProperty)));
            }
            set
            {
                base.SetValue(ResetListItemPermissionInheritance.ListItemProperty, value);
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

                        if (listItem.HasUniqueRoleAssignments)
                            listItem.ResetRoleInheritance();
                    }
                }
            });

            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowCompleted, __ActivationProperties.Web.CurrentUser, "Permissions had been reset to inherit from parent list", string.Empty);
            return base.Execute(executionContext);
        }
	}
}
