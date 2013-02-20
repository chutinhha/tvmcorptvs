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
	public partial class ChangeAllListItemPermissions: Activity
	{
		public ChangeAllListItemPermissions()
		{
			InitializeComponent();
		}

        #region Properties

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(ChangeAllListItemPermissions));

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
            typeof(string), typeof(ChangeAllListItemPermissions));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(ChangeAllListItemPermissions.ListIdProperty)));
            }
            set
            {
                base.SetValue(ChangeAllListItemPermissions.ListIdProperty, value);
            }
        }


        public static DependencyProperty ListItemProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ListItem",
            typeof(int), typeof(ChangeAllListItemPermissions));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(ChangeAllListItemPermissions.ListItemProperty)));
            }
            set
            {
                base.SetValue(ChangeAllListItemPermissions.ListItemProperty, value);
            }
        }

        public static DependencyProperty PermissionLevelProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("PermissionLevel",
            typeof(string), typeof(ChangeAllListItemPermissions));

        [Description("PermissionLevel")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PermissionLevel
        {
            get
            {
                return ((string)(base.GetValue(ChangeAllListItemPermissions.PermissionLevelProperty)));
            }
            set
            {
                base.SetValue(ChangeAllListItemPermissions.PermissionLevelProperty, value);
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

                        SPRoleDefinition permission = listItem.Web.RoleDefinitions[PermissionLevel];

                        if (!listItem.HasUniqueRoleAssignments)
                        {
                            listItem.BreakRoleInheritance(true);
                        }
                        
                        List<SPRoleAssignment> currentRoleAssigments = new List<SPRoleAssignment>();

                        //copy current role assigments and reset permission level to permission parametter
                        foreach (SPRoleAssignment ra in listItem.RoleAssignments)
                        {
                            SPRoleAssignment copyRoleAssignment = new SPRoleAssignment(ra.Member);
                            copyRoleAssignment.RoleDefinitionBindings.Add(permission);
                            currentRoleAssigments.Add(copyRoleAssignment);
                        }

                        //remove all current permissions
                        while (listItem.RoleAssignments.Count > 0)
                        {
                            listItem.RoleAssignments.Remove(0);
                        }

                        //add the copied role assigments
                        foreach (SPRoleAssignment ra in currentRoleAssigments)
                        {
                            listItem.RoleAssignments.Add(ra);
                        }
                    }
                }
            });
            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowCompleted, __ActivationProperties.Web.CurrentUser, "All permissions had been changed to " + PermissionLevel, string.Empty);            
            return base.Execute(executionContext);
        }
	}
}
