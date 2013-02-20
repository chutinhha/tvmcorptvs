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
    public partial class GetInfoPathFormValueInnerXml : Activity
    {
        private IPAccessHelper _ipHelper;



        public static DependencyProperty PropertyPathProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyPath", typeof(string), typeof(GetInfoPathFormValueInnerXml));
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyPath
        {
            get
            {
                return ((string)(base.GetValue(GetInfoPathFormValueInnerXml.PropertyPathProperty)));
            }
            set
            {
                base.SetValue(GetInfoPathFormValueInnerXml.PropertyPathProperty, value);
            }
        }

        public static DependencyProperty PropertyValueProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyValue", typeof(string), typeof(GetInfoPathFormValueInnerXml));
       
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyValue
        {
            get
            {
                return ((string)(base.GetValue(GetInfoPathFormValueInnerXml.PropertyValueProperty)));
            }
            set
            {
                base.SetValue(GetInfoPathFormValueInnerXml.PropertyValueProperty, value);
            }
        }

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(GetInfoPathFormValueInnerXml));
        
     
        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(GetInfoPathFormValueInnerXml.__ContextProperty)));
            }
            set
            {
                base.SetValue(GetInfoPathFormValueInnerXml.__ContextProperty, value);
            }
        }

        public GetInfoPathFormValueInnerXml()
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
              
                    this.PropertyValue = this._ipHelper.GetFormValueInnerXml(this.PropertyPath);
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
