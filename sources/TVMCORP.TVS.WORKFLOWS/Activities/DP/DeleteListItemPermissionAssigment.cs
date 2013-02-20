using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.ComponentModel;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Workflow.Runtime;

namespace TVMCORP.TVS.WORKFLOWS.Activities.DP
{   

    /// <summary>
    /// Deletes any roles assigned to a particular user on a list item
    /// </summary>
	public class DeleteListItemPermissionAssigment:Activity
	{
        #region Dependency Properties

        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(DeleteListItemPermissionAssigment));


        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(DeleteListItemPermissionAssigment.__ContextProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssigment.__ContextProperty, value);
            }
        }


        public static DependencyProperty ListIdProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListId", typeof(string), typeof(DeleteListItemPermissionAssigment));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(DeleteListItemPermissionAssigment.ListIdProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssigment.ListIdProperty, value);
            }
        }

        public static DependencyProperty ListItemProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListItem", typeof(int), typeof(DeleteListItemPermissionAssigment));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(DeleteListItemPermissionAssigment.ListItemProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssigment.ListItemProperty, value);
            }
        }

        public static DependencyProperty UserProperty = System.Workflow.ComponentModel.DependencyProperty.Register("User", typeof(string), typeof(DeleteListItemPermissionAssigment));

        [Description("User from which we need to remove the permission")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string User
        {
            get
            {
                return ((string)(base.GetValue(DeleteListItemPermissionAssigment.UserProperty)));
            }
            set
            {
                base.SetValue(DeleteListItemPermissionAssigment.UserProperty, value);
            }
        }
        #endregion


        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            
            try
            {


                PermissionRequest myRevokeRequest = new PermissionRequest();

                myRevokeRequest.RequestType = PermissionActionType.Revoke;
                myRevokeRequest.ItemId = this.ListItem;
                myRevokeRequest.ListID = new Guid(this.ListId);
                myRevokeRequest.SiteID = this.__Context.Site.ID;
                myRevokeRequest.WebID = this.__Context.Web.ID;
                myRevokeRequest.User = this.User;
               


                WorkflowEnvironment.WorkBatch.Add(PermissionsService.Instance, myRevokeRequest);


                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //{

                //    using (SPSite site = new SPSite(__Context.Site.ID))
                //    {
                //        using (SPWeb web = site.AllWebs[__Context.Web.ID])
                //        {
                //            SPList list = web.Lists[new Guid(ListId)];

                //            SPListItem listItem = list.Items.GetItemById(ListItem);

                //            Common.RemoveListItemPermissionEntry(listItem, User, true);

                //            listItem.Update();

                //        }

                //    }

                //});
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
