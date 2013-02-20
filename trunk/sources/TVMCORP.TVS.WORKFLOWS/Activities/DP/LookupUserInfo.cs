using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;


namespace TVMCORP.TVS.WORKFLOWS.Core.Activities.DP
{
    public partial class LookupUserInfo : Activity
    {
        public LookupUserInfo()
        {
            InitializeComponent();
        }

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(LookupUserInfo));

        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(LookupUserInfo.__ContextProperty)));
            }
            set
            {
                base.SetValue(LookupUserInfo.__ContextProperty, value);
            }
        }









    



       
        public static DependencyProperty UserNameProperty = System.Workflow.ComponentModel.DependencyProperty.Register("UserName", typeof(string), typeof(LookupUserInfo));

        [Description("UserName")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserName
        {
            get
            {
                return ((string)(base.GetValue(LookupUserInfo.UserNameProperty)));
            }
            set
            {
                base.SetValue(LookupUserInfo.UserNameProperty, value);
            }
        }
       




        public static DependencyProperty UserPropertyProperty = System.Workflow.ComponentModel.DependencyProperty.Register("UserProperty", typeof(string), typeof(LookupUserInfo));

        [Description("UserProperty")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserProperty
        {
            get
            {
                return ((string)(base.GetValue(LookupUserInfo.UserPropertyProperty)));
            }
            set
            {
                base.SetValue(LookupUserInfo.UserPropertyProperty, value);
            }
        }


        public static DependencyProperty PropertyValueVariableProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PropertyValueVariable", typeof(string), typeof(LookupUserInfo));

        [Description("PropertyValueVariable")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PropertyValueVariable
        {
            get
            {
                return ((string)(base.GetValue(LookupUserInfo.PropertyValueVariableProperty)));
            }
            set
            {
                base.SetValue(LookupUserInfo.PropertyValueVariableProperty, value);
            }
        }



        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            
            try
            {
                string err = string.Empty;

                SPList userInfo = __Context.Web.SiteUserInfoList;

                
                SPQuery q = new SPQuery();

                q.Query = "<Where><Eq><FieldRef Name='Name' /><Value Type='Text' >" + UserName + "</Value></Eq></Where>";


               


                    SPListItemCollection items = userInfo.GetItems(q);

                    if (items != null && items.Count > 0)
                    {
                        SPListItem user = items[0];

                        if (user.Fields.ContainsField(UserProperty))
                            PropertyValueVariable = user[UserProperty].ToString();
                        else
                            err = "Property not found.";
                    }
                    else
                        err = "User not found.";


                if(!string.IsNullOrEmpty(err))
                    Common.LogExceptionToWorkflowHistory(new ArgumentOutOfRangeException(err), executionContext, this.WorkflowInstanceId);;
                

             
              
            }
            catch (Exception e)
            {
                Common.LogExceptionToWorkflowHistory(e, executionContext, this.WorkflowInstanceId);

               

            }
            return ActivityExecutionStatus.Closed;
        }




    }
}
