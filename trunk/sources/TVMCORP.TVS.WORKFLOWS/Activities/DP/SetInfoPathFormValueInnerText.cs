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
using Hypertek.IOffice.Workflow.Core.Activities.DP.InfoPath;

namespace Hypertek.IOffice.Workflow.Core.Activities.DP
{
    public partial class SetInfoPathFormValueInnerText : Activity
    {
        private IPAccessHelper _ipHelper;



        public static DependencyProperty PropertyPathProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyPath", typeof(string), typeof(SetInfoPathFormValueInnerText));
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyPath
        {
            get
            {
                return ((string)(base.GetValue(SetInfoPathFormValueInnerText.PropertyPathProperty)));
            }
            set
            {
                base.SetValue(SetInfoPathFormValueInnerText.PropertyPathProperty, value);
            }
        }

        public static DependencyProperty PropertyValueProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyValue", typeof(string), typeof(SetInfoPathFormValueInnerText));
       
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyValue
        {
            get
            {
                return ((string)(base.GetValue(SetInfoPathFormValueInnerText.PropertyValueProperty)));
            }
            set
            {
                base.SetValue(SetInfoPathFormValueInnerText.PropertyValueProperty, value);
            }
        }

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(SetInfoPathFormValueInnerText));
        
     
        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(SetInfoPathFormValueInnerText.__ContextProperty)));
            }
            set
            {
                base.SetValue(SetInfoPathFormValueInnerText.__ContextProperty, value);
            }
        }

        public SetInfoPathFormValueInnerText()
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

                FormSetFieldValueRequest myRequest = new Hypertek.IOffice.Workflow.Core.Activities.DP.InfoPath.FormSetFieldValueRequest();

                myRequest.IPAccessHelper = this._ipHelper;

                myRequest.PropertyPath = this.PropertyPath;

                myRequest.PropertyValue = this.PropertyValue;

                myRequest.SetValueType = FormSetValueType.InnerText;

                WorkflowEnvironment.WorkBatch.Add(IPAccessService.Instance, myRequest);

             
            return ActivityExecutionStatus.Closed;

        }

       
       
    }
}
