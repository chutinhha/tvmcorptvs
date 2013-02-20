using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using Hypertek.IOffice.Common.Extensions;
using System.Workflow.ComponentModel.Compiler;
using Hypertek.IOffice.Model;
using Hypertek.IOffice.Workflow.Activities;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.Office.Server;
using Microsoft.SharePoint;
using Hypertek.IOffice.Workflow.Activities;

namespace Hypertek.IOffice.Workflow.Actions
{
    public class FindUserProfileActivity : CCICoreActivity
    {
        #region Dependency Properties
        public static DependencyProperty UsernameProperty =
          DependencyProperty.Register("Username",
          typeof(string),
          typeof(FindUserProfileActivity));

        [ValidationOption(ValidationOption.Required)]
        public string Username
        {
            get
            {
                return (string)base.GetValue(UsernameProperty);
            }
            set
            {
                base.SetValue(UsernameProperty, value);
            }
        }
        

        public static DependencyProperty JobTitleProperty =
          DependencyProperty.Register("JobTitle",
          typeof(string),
          typeof(FindUserProfileActivity));

        //[ValidationOption(ValidationOption.Required)]
        public string JobTitle
        {
            get
            {
                return (string)base.GetValue(JobTitleProperty);
            }
            set
            {
                base.SetValue(JobTitleProperty, value);
            }
        }

        public static DependencyProperty LevelProperty =
         DependencyProperty.Register("Level",
         typeof(uint),
         typeof(FindUserProfileActivity));
        public uint Level
        {
            get
            {
                return (uint)base.GetValue(LevelProperty);
            }
            set
            {
                base.SetValue(LevelProperty, value);
            }
        }

        public static DependencyProperty ReturnChainProperty =
          DependencyProperty.Register("ReturnChain",
          typeof(Boolean),
          typeof(FindUserProfileActivity));

        //[ValidationOption(ValidationOption.Required)]
        public Boolean ReturnChain
        {
            get
            {
                return (Boolean)base.GetValue(ReturnChainProperty);
            }
            set
            {
                base.SetValue(ReturnChainProperty, value);
            }
        }

        

        public static DependencyProperty ResultProperty =
          DependencyProperty.Register("Result",
          typeof(string),
          typeof(FindUserProfileActivity));

        [ValidationOption(ValidationOption.Required)]
         public string Result
        {
            get
            {
                return (string)base.GetValue(ResultProperty);
            }
            set
            {
                base.SetValue(ResultProperty, value);
            }
        }

        public static DependencyProperty ReturnValueFormatProperty =
          DependencyProperty.Register("ReturnValueFormat",
          typeof(string),
          typeof(FindUserProfileActivity));

        [ValidationOption(ValidationOption.Required)]
        public string ReturnValueFormat
        {
            get
            {
                return (string)base.GetValue(ReturnValueFormatProperty);
            }
            set
            {
                base.SetValue(ReturnValueFormatProperty, value);
            }
        }

        #endregion
        
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            try
            {
                SPServiceContext context = SPServiceContext.GetContext(__ActivationProperties.Site);
                UserProfileManager pManager = new UserProfileManager(context);
                var up = pManager.GetUserProfile("kms\truongdnnguyen");
                Result = up != null ? up.DisplayName : "User not found";

            }
            catch (Exception ex)
            {
                Result = ex.Message + ex.StackTrace;
                

            }
           
            //base.__ActivationProperties.LogToWorkflowHistory(Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment, __ActivationProperties.Web.CurrentUser, "Testing:" + JobTitle, "test");
            
            
            return ActivityExecutionStatus.Closed;
            
        }
    }
}
