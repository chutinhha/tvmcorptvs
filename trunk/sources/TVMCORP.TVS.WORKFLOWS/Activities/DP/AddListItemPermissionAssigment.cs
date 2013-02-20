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
    public partial class AddListItemPermissionAssigment : Activity
    {
        public AddListItemPermissionAssigment()
        {
            InitializeComponent();
        }

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(AddListItemPermissionAssigment));

        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(AddListItemPermissionAssigment.__ContextProperty)));
            }
            set
            {
                base.SetValue(AddListItemPermissionAssigment.__ContextProperty, value);
            }
        }





        public static DependencyProperty ListIdProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListId", typeof(string), typeof(AddListItemPermissionAssigment));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(AddListItemPermissionAssigment.ListIdProperty)));
            }
            set
            {
                base.SetValue(AddListItemPermissionAssigment.ListIdProperty, value);
            }
        }




        public static DependencyProperty ListItemProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListItem", typeof(int), typeof(AddListItemPermissionAssigment));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(AddListItemPermissionAssigment.ListItemProperty)));
            }
            set
            {
                base.SetValue(AddListItemPermissionAssigment.ListItemProperty, value);
            }
        }





        public static DependencyProperty UserNameProperty = System.Workflow.ComponentModel.DependencyProperty.Register("UserName", typeof(string), typeof(AddListItemPermissionAssigment));

        [Description("UserName")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserName
        {
            get
            {
                return ((string)(base.GetValue(AddListItemPermissionAssigment.UserNameProperty)));
            }
            set
            {
                base.SetValue(AddListItemPermissionAssigment.UserNameProperty, value);
            }
        }





        public static DependencyProperty PermissionLevelProperty = System.Workflow.ComponentModel.DependencyProperty.Register("PermissionLevel", typeof(string), typeof(AddListItemPermissionAssigment));

        [Description("PermissionLevel")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PermissionLevel
        {
            get
            {
                return ((string)(base.GetValue(AddListItemPermissionAssigment.PermissionLevelProperty)));
            }
            set
            {
                base.SetValue(AddListItemPermissionAssigment.PermissionLevelProperty, value);
            }
        }






        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {

            try
            {

               

                PermissionRequest myGrantRequest = new PermissionRequest();

                myGrantRequest.RequestType = PermissionActionType.Grant;
                myGrantRequest.ItemId = this.ListItem;
                myGrantRequest.ListID = new Guid(this.ListId);
                myGrantRequest.SiteID = this.__Context.Site.ID;
                myGrantRequest.WebID = this.__Context.Web.ID;
                myGrantRequest.User = this.UserName;
                myGrantRequest.PermissionLevel = Common.ProcessStringField(executionContext, this.PermissionLevel);


                WorkflowEnvironment.WorkBatch.Add(PermissionsService.Instance, myGrantRequest);

                
                //run in context of sharpoints system account because user might not have permissions to grant permisions (Beware of the security risk here)





                //SPSecurity.RunWithElevatedPrivileges(delegate()
                // {
                //     using (SPSite site = new SPSite(__Context.Site.ID))
                //     {
                //         using (SPWeb web = site.AllWebs[__Context.Web.ID])
                //         {

                //             SPList List = web.Lists[new Guid(this.ListId)];

                //             SPListItem listItem = List.GetItemById(this.ListItem);
                             
                //             string permission = Common.ProcessStringField(executionContext, this.PermissionLevel);

                //             if (!Common.IsUserRoleAssigned(__Context, this.ListId, this.ListItem, permission, this.UserName))
                //             {
                //                 if (!listItem.HasUniqueRoleAssignments)
                //                 {
                //                     listItem.BreakRoleInheritance(true);
                //                 }

                //                 Common.RemoveListItemLimitedPermissions(listItem);
                                 
                //                 Common.RemoveListItemPermissionEntry(listItem, this.UserName, false);

                //                 listItem = Common.SetItemPermissions(web, listItem, permission, this.UserName);

                //                 listItem.Update();

                //             }

                //         }
                //     }

                // });

            }
            catch (Exception e)
            {
                Common.LogExceptionToWorkflowHistory(e, executionContext, this.WorkflowInstanceId);

                throw;

            }
            return ActivityExecutionStatus.Closed;
        }




    }
}
