using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL;


namespace TVMCORP.TVS.WORKFLOWS.Activities.WorkflowActions
{
    public partial class ResetToSecurityRulesPermissions : Activity
    {
        public ResetToSecurityRulesPermissions()
        {
            InitializeComponent();
        }

        #region Properties

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(ResetToSecurityRulesPermissions));

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
            typeof(string), typeof(ResetToSecurityRulesPermissions));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(ResetToSecurityRulesPermissions.ListIdProperty)));
            }
            set
            {
                base.SetValue(ResetToSecurityRulesPermissions.ListIdProperty, value);
            }
        }


        public static DependencyProperty ListItemProperty =
            System.Workflow.ComponentModel.DependencyProperty.Register("ListItem",
            typeof(int), typeof(ResetToSecurityRulesPermissions));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(ResetToSecurityRulesPermissions.ListItemProperty)));
            }
            set
            {
                base.SetValue(ResetToSecurityRulesPermissions.ListItemProperty, value);
            }
        }

        public static DependencyProperty EventTypeProperty = DependencyProperty.Register("EventType", typeof(string), typeof(ResetToSecurityRulesPermissions));

        [Description("EventType")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string EventType
        {
            get { return (string)GetValue(EventTypeProperty); }
            set { SetValue(EventTypeProperty, value); }
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

                        SecurityEventHandler handler = new SecurityEventHandler();
                        SPEventReceiverType eventType = (this.EventType == "ItemAdded") ? SPEventReceiverType.ItemAdded : SPEventReceiverType.ItemUpdated;
                        handler.HandleSecurityRules(listItem, eventType);
                    }
                }
            });

            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowCompleted, __ActivationProperties.Web.CurrentUser, "Permissions had been reset to Hypertek.IOffice Security Rules", string.Empty);
            return base.Execute(executionContext);
        }

    }
}
