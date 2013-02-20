using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.ComponentModel;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace TVMCORP.TVS.WORKFLOWS.Activities.DP
{

    /// <summary>
    /// starts a workflow associated with a list
    /// </summary>
	public class StartAnotherWorkflow:Activity
	{
        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(StartAnotherWorkflow));


        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(StartAnotherWorkflow.__ContextProperty)));
            }
            set
            {
                base.SetValue(StartAnotherWorkflow.__ContextProperty, value);
            }
        }


        public static DependencyProperty ListIdProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListId", typeof(string), typeof(StartAnotherWorkflow));

        [Description("ID of the list we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(StartAnotherWorkflow.ListIdProperty)));
            }
            set
            {
                base.SetValue(StartAnotherWorkflow.ListIdProperty, value);
            }
        }

        public static DependencyProperty ListItemProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListItem", typeof(int), typeof(StartAnotherWorkflow));

        [Description("ID of the list item we are working with")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(StartAnotherWorkflow.ListItemProperty)));
            }
            set
            {
                base.SetValue(StartAnotherWorkflow.ListItemProperty, value);
            }
        }

        public static DependencyProperty WorkflowIdentifierProperty = System.Workflow.ComponentModel.DependencyProperty.Register("WorkflowIdentifier", typeof(string), typeof(StartAnotherWorkflow));

        [Description("Workflow name or template base id")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string WorkflowIdentifier
        {
            get
            {
                return ((string)(base.GetValue(StartAnotherWorkflow.WorkflowIdentifierProperty)));
            }
            set
            {
                base.SetValue(StartAnotherWorkflow.WorkflowIdentifierProperty, value);
            }
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            try
            {   //need to run under SHAREPOINT\system account because workflow owner might not have start workflow permissions on the target list
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(__Context.Site.ID))
                    {
                        using (SPWeb web = site.AllWebs[__Context.Web.ID])
                        {
                            SPList list = web.Lists[new Guid(ListId)];

                            SPListItem listItem = list.Items.GetItemById(ListItem);

                            SPWorkflowAssociation myWorkflowAssoc  = null;
                           
                            //resolve any lookup parameters
                            string wkId = Common.ProcessStringField(executionContext, this.WorkflowIdentifier);
                            
                            //find workflow association by name
                            myWorkflowAssoc = list.WorkflowAssociations.GetAssociationByName(wkId, System.Threading.Thread.CurrentThread.CurrentCulture);

                            if (myWorkflowAssoc != null)
                            {   //start the workflow
                                site.WorkflowManager.StartWorkflow(listItem, myWorkflowAssoc,myWorkflowAssoc.AssociationData);

                            }

                        }

                    }

                });
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
