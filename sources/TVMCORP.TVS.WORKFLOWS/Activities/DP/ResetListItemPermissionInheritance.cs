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
    public partial class ResetListItemPermissionInheritance : Activity
    {
        public ResetListItemPermissionInheritance()
        {
            InitializeComponent();
        }

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(ResetListItemPermissionInheritance));

        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(ResetListItemPermissionInheritance.__ContextProperty)));
            }
            set
            {
                base.SetValue(ResetListItemPermissionInheritance.__ContextProperty, value);
            }
        }


        public static DependencyProperty ListIdProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListId", typeof(string), typeof(ResetListItemPermissionInheritance));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(ResetListItemPermissionInheritance.ListIdProperty)));
            }
            set
            {
                base.SetValue(ResetListItemPermissionInheritance.ListIdProperty, value);
            }
        }

        public static DependencyProperty ListItemProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListItem", typeof(int), typeof(ResetListItemPermissionInheritance));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(ResetListItemPermissionInheritance.ListItemProperty)));
            }
            set
            {
                base.SetValue(ResetListItemPermissionInheritance.ListItemProperty, value);
            }
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            try
            {

                PermissionRequest myResetRequest = new PermissionRequest();

                myResetRequest.RequestType = PermissionActionType.Reset;
                myResetRequest.ItemId = this.ListItem;
                myResetRequest.ListID = new Guid(this.ListId);
                myResetRequest.SiteID = this.__Context.Site.ID;
                myResetRequest.WebID = this.__Context.Web.ID;
            

                WorkflowEnvironment.WorkBatch.Add(PermissionsService.Instance, myResetRequest);


                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //     {
                //         using (SPSite site = new SPSite(__Context.Site.ID))
                //         {
                //             using (SPWeb web = site.AllWebs[__Context.Web.ID])
                //             {


                //                 SPList List = web.Lists[new Guid(this.ListId)];

                //                 SPListItem listItem = List.GetItemById(this.ListItem);

                //                 if (listItem.HasUniqueRoleAssignments)
                //                 {
                //                     listItem.ResetRoleInheritance();

                //                     listItem.SystemUpdate();

                //                 }


                //             }
                //         }

                //     });
            }
            catch (Exception e)
            {
                Common.LogExceptionToWorkflowHistory(e, executionContext, this.WorkflowInstanceId);

                throw;
            }
            return base.Execute(executionContext);
        }
      
    }
}

