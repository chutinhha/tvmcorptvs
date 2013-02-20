using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using TVMCORP.TVS.UTIL.MODELS;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Extensions;

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities
{
    public class UpdatePermissionActivity : System.Workflow.ComponentModel.Activity
    {
        public UpdatePermissionActivity()
        {
            this.Name = typeof(UpdatePermissionActivity).Name;
        }


        public List<string> Approvers
        {
            get { return (List<string>)GetValue(ApproversProperty); }
            set { SetValue(ApproversProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Approvers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApproversProperty =
            DependencyProperty.Register("Approvers", typeof(List<string>), typeof(UpdatePermissionActivity));


        public List<PermissionUpdate> Permissions
        {
            get { return (List<PermissionUpdate>)GetValue(PermissionsProperty); }
            set { SetValue(PermissionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Permissions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PermissionsProperty =
            DependencyProperty.Register("Permissions", typeof(List<PermissionUpdate>), typeof(UpdatePermissionActivity) );


        public SPListItem Item

        {
            get { return (SPListItem)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(SPListItem), typeof(UpdatePermissionActivity));



        public bool KeepExistingPermissions
        {
            get { return (bool)GetValue(KeepExistingPermissionsProperty); }
            set { SetValue(KeepExistingPermissionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeepExistingPermissions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeepExistingPermissionsProperty =
            DependencyProperty.Register("KeepExistingPermissions", typeof(bool), typeof(UpdatePermissionActivity));


        
        protected override System.Workflow.ComponentModel.ActivityExecutionStatus Execute(System.Workflow.ComponentModel.ActivityExecutionContext executionContext)
        {
            Item.BreakRoleInheritance(KeepExistingPermissions);

            foreach (var item in Permissions)
            {
                var loginnames = GetLoginNames(item);
                Item.SetPermissions(item.Name, loginnames);
            }
            return ActivityExecutionStatus.Closed;
        }

        private List<string> GetLoginNames(PermissionUpdate item)
        {
            List<string> logins = new List<string>();

            if (item.Ower)
            {
                var user = new SPFieldUserValue(Item.Web, Item[SPBuiltInFieldId.Author].ToString());
                logins.Add(user.User.LoginName);
            }
            if (item.Approvers)
            {
                logins.AddRange(Approvers);
            }
            foreach (var col in item.Columns)
            {
                if (Item[col] != null)
                {
                    var field = (SPFieldUser)Item.Fields[col];
                    if (field.AllowMultipleValues)
                    {
                        SPFieldUserValueCollection userCollection = (SPFieldUserValueCollection)Item[col];
                        foreach (SPFieldUserValue user in userCollection)
                        {
                            logins.Add(user.User.LoginName); 
                        }
                    }
                    else
                    {
                        var user = new SPFieldUserValue(Item.Web, Item[col].ToString());
                        logins.Add(user.User.LoginName);
                    }
                    
                }
            }
            return logins;
        }
    }
}
