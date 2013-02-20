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
using Microsoft.SharePoint;
using System.Diagnostics;

namespace TVMCORP.TVS.WORKFLOWS.Core.Activities.DP
{
    [ActivityValidator(typeof(CopyListItemActivityValidator))]
    public partial class CopyListItemExtended : Activity
    {
        public static DependencyProperty __ContextProperty = System.Workflow.ComponentModel.DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(CopyListItemExtended));

        [Description("Context")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkflowContext __Context
        {
            get
            {
                return ((WorkflowContext)(base.GetValue(CopyListItemExtended.__ContextProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.__ContextProperty, value);
            }
        }



        public static DependencyProperty ListIdProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListId", typeof(string), typeof(CopyListItemExtended));

        [Description("ListId")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListId
        {
            get
            {
                return ((string)(base.GetValue(CopyListItemExtended.ListIdProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.ListIdProperty, value);
            }
        }




        public static DependencyProperty ListItemProperty = System.Workflow.ComponentModel.DependencyProperty.Register("ListItem", typeof(int), typeof(CopyListItemExtended));

        [Description("ListItem")]
        [ValidationOption(ValidationOption.Required)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ListItem
        {
            get
            {
                return ((int)(base.GetValue(CopyListItemExtended.ListItemProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.ListItemProperty, value);
            }
        }



        public static DependencyProperty MoveProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Move", typeof(string), typeof(CopyListItemExtended));

        [Description("Specifies whether to move an item or simply copy it")]

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Move
        {
            get
            {
                return ((string)(base.GetValue(CopyListItemExtended.MoveProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.MoveProperty, value);
            }
        }


        public static DependencyProperty OverwriteProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Overwrite", typeof(string), typeof(CopyListItemExtended));

        [Description("This is the description which appears in the Property Browser")]
        [Category("This is the category which will be displayed in the Property Browser")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string Overwrite
        {
            get
            {
                return ((string)(base.GetValue(CopyListItemExtended.OverwriteProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.OverwriteProperty, value);
            }
        }
        public static DependencyProperty DestinationListUrlProperty = System.Workflow.ComponentModel.DependencyProperty.Register("DestinationListUrl", typeof(string), typeof(CopyListItemExtended));

        [Description("Destination list url")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string DestinationListUrl
        {
            get
            {
                return ((string)(base.GetValue(CopyListItemExtended.DestinationListUrlProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.DestinationListUrlProperty, value);
            }
        }



        public static DependencyProperty OutListItemIDProperty = System.Workflow.ComponentModel.DependencyProperty.Register("OutListItemID", typeof(int), typeof(CopyListItemExtended));

        [Description("This is the description which appears in the Property Browser")]
        [Category("This is the category which will be displayed in the Property Browser")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int OutListItemID
        {
            get
            {
                return ((int)(base.GetValue(CopyListItemExtended.OutListItemIDProperty)));
            }
            set
            {
                base.SetValue(CopyListItemExtended.OutListItemIDProperty, value);
            }
        }

        public CopyListItemExtended()
        {
            InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            using (WorkflowHistoryTraceListener myTracer = new WorkflowHistoryTraceListener(executionContext, this.WorkflowInstanceId))
            {

               Trace.Listeners.Add(myTracer);

                try
                {
                    //elevate privilages, because SHAREPOINT\System account has full access privilages to the entire farm
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite sourceSite = new SPSite(this.__Context.Web.Site.ID))
                        {
                            using (SPWeb sourceWeb = sourceSite.AllWebs[this.__Context.Web.ID])
                            {
                                //replace any workflow variables
                                string destinationUrlProcessed = Common.ProcessStringField(executionContext, DestinationListUrl);

                                using (SPSite destSite = new SPSite(destinationUrlProcessed))
                                {
                                    using (SPWeb destWeb = destSite.OpenWeb())
                                    {
                                        SPList destinationList = null;

                                        //each list, even a non document library list has at least a root folder.
                                        SPFolder destFolder = destWeb.GetFolder(destinationUrlProcessed);

                                        if (!destFolder.Exists)
                                            throw new InvalidOperationException(string.Format("List at {0} does not exist!", DestinationListUrl));
                                    
                                            destinationList = destWeb.Lists[destFolder.ParentListId];

                                        

                                        SPList sourceList = sourceWeb.Lists[new Guid(this.ListId)];

                                        SPListItem sourceItem = sourceList.Items.GetItemById(ListItem);

                                        ListItemCopier.ListItemCopyOptions options = new ListItemCopier.ListItemCopyOptions();

                                        options.IncludeAttachments = true;

                                        options.OperationType = bool.Parse(Move) ? ListItemCopier.OperationType.Move : ListItemCopier.OperationType.Copy;

                                        options.Overwrite = bool.Parse(Overwrite);

                                        options.DestinationFolder = destFolder;

                                        using (ListItemCopier myCopier = new ListItemCopier(sourceItem, destinationList, options))
                                        {

                                            OutListItemID = myCopier.Copy();

                                        }

                                    }

                                }

                            }
                        }

                    });
                }
                catch (Exception e)
                {
                    Common.LogExceptionToWorkflowHistory(e, executionContext, this.WorkflowInstanceId);

                    throw Common.WrapWithFriedlyException(e, "Error while copying list items.");
                }


                Trace.Listeners.Remove(myTracer);

            }

            return base.Execute(executionContext);
        }
    }
}
