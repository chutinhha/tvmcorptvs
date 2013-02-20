using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using Hypertek.IOffice.Common.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Hypertek.IOffice.Common.Extensions;
using Hypertek.IOffice.Common.Utilities;
using System.Workflow.Activities;

namespace Hypertek.IOffice.Workflow.Activities
{
    public partial class GenerateContractNumber : SequenceActivity
    {
        public GenerateContractNumber()
        {
            InitializeComponent();
        }


        public static DependencyProperty ContactNumberGeneratedProperty =
            DependencyProperty.Register("ContactNumberGenerated",
            typeof(string), typeof(GenerateContractNumber));

        [Description("The property use to store contract number")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ContactNumberGenerated
        {
            get { return ((string)(base.GetValue(ContactNumberGeneratedProperty))); }
            set { base.SetValue(ContactNumberGeneratedProperty, value); }
        }

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(GenerateContractNumber));

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

        public static DependencyProperty __ListIdProperty =
            DependencyProperty.Register("__ListId",
            typeof(string), typeof(GenerateContractNumber));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string __ListId
        {
            get { return ((string)(base.GetValue(__ListIdProperty))); }
            set { base.SetValue(__ListIdProperty, value); }
        }

        public static DependencyProperty ContactNumberFieldNameProperty =
            DependencyProperty.Register("ContactNumberFieldName",
            typeof(string), typeof(GenerateContractNumber));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Optional)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ContactNumberFieldName
        {
            get { return ((string)(base.GetValue(ContactNumberFieldNameProperty))); }
            set { base.SetValue(ContactNumberFieldNameProperty, value); }
        }

        public static DependencyProperty __ListItemProperty =
            DependencyProperty.Register("__ListItem",
            typeof(int), typeof(GenerateContractNumber));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int __ListItem
        {
            get { return ((int)(base.GetValue(__ListItemProperty))); }
            set { base.SetValue(__ListItemProperty, value); }
        }


        private void CNGeneration_ExecuteCode(object sender, EventArgs e)
        {
            CCIUtility.Debug("Execute Contract Number Generation");
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                string contractNumber = string.Empty;
                try
                {
                    using (SPSite site = new SPSite(__ActivationProperties.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(__ActivationProperties.Web.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            SPListItem sourceListItem = web.Lists.GetList(new Guid(__ListId), false).GetItemById(__ListItem);

                            if (sourceListItem == null) return;

                            ContactNumberGenerated = ContractNumberGeneratorHelper.GenerateContractNumber(sourceListItem.ContentType);

                            if (!string.IsNullOrEmpty(ContactNumberFieldName) && 
                                !string.IsNullOrEmpty(ContactNumberGenerated) && sourceListItem.Fields.ContainsField(ContactNumberFieldName))
                            {
                                web.AllowUnsafeUpdates = true;
                                sourceListItem[ContactNumberFieldName] = ContactNumberGenerated;
                                sourceListItem.SystemUpdate();
                                //__ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowComment, __ActivationProperties.Web.CurrentUser, "Contract Number generated", ContactNumberGenerated);
                                web.AllowUnsafeUpdates = false;
                            }
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                    //if (string.IsNullOrEmpty(ContactNumberGenerated))
                    //{
                    //    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowComment, __ActivationProperties.Web.CurrentUser, "Unable to generate contract number", string.Empty);
                    //}
                }
                catch (Exception ex)
                {
                    CCIUtility.LogError(ex.Message + ex.StackTrace, "Hypertek.IOffice.Workflow");
                    __ActivationProperties.LogToWorkflowHistory(SPWorkflowHistoryEventType.WorkflowError, __ActivationProperties.Web.CurrentUser, "Error occur during generate contract number", string.Empty);
                }
            });
        }

        private void IsCNGenerated(object sender, ConditionalEventArgs e)
        {
            e.Result = !string.IsNullOrEmpty(ContactNumberGenerated);
        }

    }
    public class GenerateContractNumberV1 : GenerateContractNumber
    {
    }
}
