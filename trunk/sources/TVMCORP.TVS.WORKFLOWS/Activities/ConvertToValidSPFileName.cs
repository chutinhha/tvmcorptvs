using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using TVMCORP.TVS.UTIL.Extensions;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    public partial class ConvertToValidSPFileName : Activity
	{
		public ConvertToValidSPFileName()
		{
			InitializeComponent();
		}

        #region Dependency Properties
        public static DependencyProperty ValidatedFileNameProperty =
            DependencyProperty.Register("ValidatedFileName",
            typeof(string),
            typeof(ConvertToValidSPFileName));

        [Description("Validated FileName")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ValidatedFileName
        {
            get { return ((string)(base.GetValue(ValidatedFileNameProperty))); }
            set { base.SetValue(ValidatedFileNameProperty, value); }
        }  

        public static DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName",
            typeof(string), 
            typeof(ConvertToValidSPFileName));

        [Description("Input FileName")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string FileName
        {
            get { return ((string)(base.GetValue(FileNameProperty))); }
            set { base.SetValue(FileNameProperty, value); }
        }  
        #endregion

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            ValidatedFileName = FileName.ConvertToValidSharePointFileName();
            return base.Execute(executionContext);
        }
	}
}
