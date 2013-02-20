using System;
using System.ComponentModel;
using System.Linq;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Actions
{
    public partial class CoreApproval : SequenceActivity
    {
        public CoreApproval()
        {
            InitializeComponent();
        }

        #region Properties
        public static DependencyProperty __ListIdProperty =
            DependencyProperty.Register("__ListId",
            typeof(string), typeof(CoreApproval));

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
            typeof(int), typeof(CoreApproval));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int __ListItem
        {
            get { return ((int)(base.GetValue(__ListItemProperty))); }
            set { base.SetValue(__ListItemProperty, value); }
        }

        public static DependencyProperty __ActivationPropertiesProperty =
            DependencyProperty.Register("__ActivationProperties",
            typeof(Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties),
            typeof(CoreApproval));

        [ValidationOption(ValidationOption.Required)]
        public SPWorkflowActivationProperties __ActivationProperties
        {
            get { return (SPWorkflowActivationProperties)base.GetValue(__ActivationPropertiesProperty); }
            set { base.SetValue(__ActivationPropertiesProperty, value); }
        }

        public static DependencyProperty OutputFieldNameProperty =
            DependencyProperty.Register("OutputFieldName",
            typeof(string), typeof(CoreApproval));

        [Description("Field name to out put")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Optional)]
        public string OutputFieldName
        {
            get { return ((string)(base.GetValue(OutputFieldNameProperty))); }
            set { base.SetValue(OutputFieldNameProperty, value); }
        }

        public static DependencyProperty OutputTypeProperty =
            DependencyProperty.Register("OutputType",
            typeof(string), typeof(CoreApproval));

        [Description("Out put type")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Optional)]
        public string OutputType
        {
            get { return ((string)(base.GetValue(OutputTypeProperty))); }
            set { base.SetValue(OutputTypeProperty, value); }
        }

        public static DependencyProperty OutputValueProperty =
            DependencyProperty.Register("OutputValue",
            typeof(string), typeof(CoreApproval));

        [Description("Out put value")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Optional)]
        public string OutputValue
        {
            get { return ((string)(base.GetValue(OutputValueProperty))); }
            set { base.SetValue(OutputValueProperty, value); }
        }
        #endregion

        public void GenerateOutputValue(SPListItem listItem)
        {
            if (listItem == null) return;
            if (string.IsNullOrEmpty(OutputFieldName) || string.IsNullOrEmpty(OutputType))
                return;
            if (!listItem.Fields.ContainFieldName(OutputFieldName))
                return;

            try
            {
                SPField field = listItem.Fields.Cast<SPField>().FirstOrDefault(f => string.Compare(f.Title, OutputFieldName, true) == 0);
                if (field == null) return;

                if (listItem[field.Id] == null)
                    return;

                OutputType type = Model.OutputType.Text;
                if (!string.IsNullOrEmpty(OutputType))
                    type = (OutputType)Enum.Parse(typeof(OutputType), OutputType);

                SPFieldUserValue userValue = null;
                if (field.Type == SPFieldType.User)
                {
                    SPFieldUser fieldUser = (SPFieldUser)field;
                    if (fieldUser.AllowMultipleValues)
                    {
                        SPFieldUserValueCollection userValueCollection = new SPFieldUserValueCollection(__ActivationProperties.Web, listItem[field.Id].ToString());
                        userValue = userValueCollection[0];
                    }
                    else
                        userValue = new SPFieldUserValue(__ActivationProperties.Web, listItem[field.Id].ToString());
                }

                SPFieldLookupValue lookupValue = null;
                if (field.Type == SPFieldType.Lookup)
                {
                    SPFieldLookup fieldLookup = (SPFieldLookup)field;
                    if (fieldLookup.AllowMultipleValues)
                    {
                        SPFieldLookupValueCollection lookupValueCollection = new SPFieldLookupValueCollection(listItem[field.Id].ToString());
                        lookupValue = lookupValueCollection[0];
                    }
                    else
                        lookupValue = new SPFieldLookupValue(listItem[field.Id].ToString());
                }

                switch (type)
                {
                    case OutputType.DisplayName:
                        if (userValue == null || userValue.User == null) return;
                        OutputValue = userValue.User.Name;
                        return;

                    case Model.OutputType.EmailAddress:
                        if (userValue == null || userValue.User == null) return;
                        OutputValue = userValue.User.Email;
                        return;

                    case Model.OutputType.LoginName:
                        if (userValue == null || userValue.User == null) return;
                        OutputValue = userValue.User.LoginName;
                        return;

                    case Model.OutputType.LookupId:
                        if (userValue != null)
                            OutputValue = userValue.LookupId.ToString();
                        if (lookupValue != null)
                            OutputValue = lookupValue.LookupId.ToString();
                        return;

                    case Model.OutputType.LookupValue:
                        if (userValue != null)
                            OutputValue = userValue.LookupValue;
                        if (lookupValue != null)
                            OutputValue = lookupValue.LookupValue;
                        return;

                    default:
                        OutputValue = listItem[field.Id].ToString();
                        return;
                }
            }
            catch { }
        }
    }
}

