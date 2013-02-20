using System.ComponentModel;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.WORKFLOWS.TaskActions;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class CustomLogToWFHistoryActivity : Activity
    {
        public static DependencyProperty HistoryDescriptionProperty = DependencyProperty.Register("HistoryDescription", typeof(string), typeof(CustomLogToWFHistoryActivity), new PropertyMetadata(""));
        public static DependencyProperty HistoryOutcomeProperty = DependencyProperty.Register("HistoryOutcome", typeof(string), typeof(CustomLogToWFHistoryActivity), new PropertyMetadata(""));
        public static DependencyProperty EventIdProperty = DependencyProperty.Register("EventId", typeof(SPWorkflowHistoryEventType), typeof(CustomLogToWFHistoryActivity), new PropertyMetadata(SPWorkflowHistoryEventType.WorkflowComment));
        public static DependencyProperty WorkflowPropertiesProperty = DependencyProperty.Register("WorkflowProperties", typeof(SPWorkflowActivationProperties), typeof(CustomLogToWFHistoryActivity));

        [Description("Workflow properties ")]
        [Category("Dependency Properties")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SPWorkflowActivationProperties WorkflowProperties
        {
            get
            {
                return ((SPWorkflowActivationProperties)(base.GetValue(WorkflowPropertiesProperty)));
            }
            set
            {
                base.SetValue(WorkflowPropertiesProperty, value);
            }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true), ValidationOption(ValidationOption.Required)]
        public string HistoryDescription
        {
            get
            {
                return (string)base.GetValue(HistoryDescriptionProperty);
            }
            set
            {
                base.SetValue(HistoryDescriptionProperty, value);
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), ValidationOption(ValidationOption.Optional)]
        public string HistoryOutcome
        {
            get
            {
                return (string)base.GetValue(HistoryOutcomeProperty);
            }
            set
            {
                base.SetValue(HistoryOutcomeProperty, value);
            }
        }

        [Browsable(true), ValidationOption(ValidationOption.Optional), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SPWorkflowHistoryEventType EventId
        {
            get
            {
                return (SPWorkflowHistoryEventType)base.GetValue(EventIdProperty);
            }
            set
            {
                base.SetValue(EventIdProperty, value);
            }
        }

        #region Properties

        #endregion

        public CustomLogToWFHistoryActivity()
        {
            InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            WorkflowProperties.LogToWorkflowHistory(EventId, WorkflowProperties.Web.CurrentUser, this.HistoryDescription, this.HistoryOutcome);
            return ActivityExecutionStatus.Closed;
        }
    }
}
 
