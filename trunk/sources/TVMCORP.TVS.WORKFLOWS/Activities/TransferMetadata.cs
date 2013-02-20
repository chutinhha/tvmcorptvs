using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class TransferMetadata : Activity
	{
		public TransferMetadata()
		{
			InitializeComponent();
        }

        #region Properties
        public static DependencyProperty ListIdSourceProperty =
            DependencyProperty.Register("ListIdSource",
            typeof(string), typeof(TransferMetadata));

        [Description("ID of the source list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListIdSource
        {
            get { return ((string)(base.GetValue(ListIdSourceProperty))); }
            set { base.SetValue(ListIdSourceProperty, value); }
        }


        public static DependencyProperty ListItemSourceProperty =
            DependencyProperty.Register("ListItemSource",
            typeof(int), typeof(TransferMetadata));

        [Description("ID of the source list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItemSource
        {
            get { return ((int)(base.GetValue(ListItemSourceProperty))); }
            set { base.SetValue(ListItemSourceProperty, value); }
        }

        public static DependencyProperty ListIdDestinationProperty =
            DependencyProperty.Register("ListIdDestination",
            typeof(string), typeof(TransferMetadata));

        [Description("ID of the destination list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListIdDestination
        {
            get { return ((string)(base.GetValue(ListIdDestinationProperty))); }
            set { base.SetValue(ListIdDestinationProperty, value); }
        }

        public static DependencyProperty ListItemDestinationProperty =
            DependencyProperty.Register("ListItemDestination",
            typeof(int), typeof(TransferMetadata));

        [Description("ID of the destination list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItemDestination
        {
            get { return ((int)(base.GetValue(ListItemDestinationProperty))); }
            set { base.SetValue(ListItemDestinationProperty, value); }
        }

        public static DependencyProperty OverrideContentTypeProperty =
        DependencyProperty.Register("OverrideContentType",
        typeof(Boolean), typeof(TransferMetadata));

        [Description("ID of the destination list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean OverrideContentType
        {
            get { return ((Boolean)(base.GetValue(OverrideContentTypeProperty))); }
            set { base.SetValue(OverrideContentTypeProperty, value); }
        }

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(TransferMetadata));

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

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite site = new SPSite(__ActivationProperties.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(__ActivationProperties.Web.ID))
                    {
                        SPListItem itemSource = null;
                        SPListItem itemDestination = null;
                        try
                        {
                            SPList listSource = web.Lists.GetList(new Guid(this.ListIdSource), false);
                            itemSource = listSource.GetItemById(this.ListItemSource);
                        }
                        catch
                        {
                            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The source list item does not exist", string.Empty);
                        }
                        try
                        {
                            SPList listDestination = web.Lists.GetList(new Guid(this.ListIdDestination), false);
                            itemDestination = listDestination.GetItemById(this.ListItemDestination);
                        }
                        catch
                        {
                            __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "The destination list item does not exist", string.Empty);
                        }

                        if (itemSource != null && itemDestination != null)
                        {
                            string[] ignoreFields;
                            if (OverrideContentType)
                                ignoreFields = new string[] { "Name" };
                            else
                                ignoreFields = new string[] { "ContentType", "Content Type", "Name" };

                            itemSource.CopyMetadataTo(itemDestination, ignoreFields);
                        }
                    }
                }
            });
            return ActivityExecutionStatus.Closed;
        }
	}
}
