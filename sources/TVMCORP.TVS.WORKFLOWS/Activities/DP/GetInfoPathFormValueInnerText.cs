using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint.WorkflowActions;

namespace Hypertek.IOffice.Workflow.Core.Activities.DP
{
    public partial class GetInfoPathFormValueInnerText : Activity
    {
        private IPAccessHelper _ipHelper;



        public static DependencyProperty PropertyPathProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyPath", typeof(string), typeof(GetInfoPathFormValueInnerText));
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyPath
        {
            get
            {
                return ((string)(base.GetValue(GetInfoPathFormValueInnerText.PropertyPathProperty)));
            }
            set
            {
                base.SetValue(GetInfoPathFormValueInnerText.PropertyPathProperty, value);
            }
        }

        public static DependencyProperty PropertyValueProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyValue", typeof(string), typeof(GetInfoPathFormValueInnerText));
       
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyValue
        {
            get
            {
                return ((string)(base.GetValue(GetInfoPathFormValueInnerText.PropertyValueProperty)));
            }
            set
            {
                base.SetValue(GetInfoPathFormValueInnerText.PropertyValueProperty, value);
            }
        }

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(GetInfoPathFormValueInnerText));
        
     
        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(GetInfoPathFormValueInnerText.__ContextProperty)));
            }
            set
            {
                base.SetValue(GetInfoPathFormValueInnerText.__ContextProperty, value);
            }
        }

        public GetInfoPathFormValueInnerText()
        {
            InitializeComponent();

         
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            _ipHelper = new IPAccessHelper(__Context);


            try
            {
                _ipHelper.LoadForm();
            }
            catch (Exception e)
            {
                Exception we = Common.WrapWithFriedlyException(e, "Error loading form data file. Likely causes: this file is not a valid InfoPath Form!");

                Common.LogExceptionToWorkflowHistory(we, executionContext, this.WorkflowInstanceId);

                throw we;

            }


            try
            {
              
                    this.PropertyValue = this._ipHelper.GetFormValueInnerText(this.PropertyPath);
            }
            catch (Exception e)
            {
                Exception we = Common.WrapWithFriedlyException(e, string.Format("Error getting form value where path = {0}", this.PropertyPath));

                Common.LogExceptionToWorkflowHistory(we, executionContext, this.WorkflowInstanceId);

                throw we;

            }

            return ActivityExecutionStatus.Closed;

        }

       
       
    }
}
