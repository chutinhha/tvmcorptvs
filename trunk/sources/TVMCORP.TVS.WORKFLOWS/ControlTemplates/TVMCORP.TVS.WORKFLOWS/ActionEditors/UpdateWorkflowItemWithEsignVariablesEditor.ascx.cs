using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TVMCORP.TVS.WORKFLOWS.Controls;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.Util.Helpers;
using TVMCORP.TVS.Util.Utilities;

namespace TVMCORP.TVS.WORKFLOWS.CONTROLTEMPLATES.TVMCORP.TVS.WORKFLOWS
{
    public partial class UpdateWorkflowItemWithEsignVariablesEditor : ActionEditorControl
    {
        protected override void OnInit(EventArgs e)
        {
            chkRemoveAction.Attributes.Add("onclick", string.Format("{0}_click(this);", chkRemoveAction.ClientID));
            base.OnInit(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (chkRemoveAction.Checked)
            {
                StringBuilder script = new StringBuilder();
                script.Append("<script type=\"text/javascript\" language=\"javascript\">");
                script.AppendFormat("       _spBodyOnLoadFunctionNames.push(\"{0}_click(document.getElementById('{1}'))\");", chkRemoveAction.ClientID, chkRemoveAction.ClientID);
                script.Append("</script>");
                ltrScript.Text = script.ToString();
            }
            base.OnPreRender(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            if (IsFirstLoad)
            {
                showSiteColumns();
                showESignMetadataProperties();
            }
            if (!IsPostBack)
            {
                displayActionData();
            }
            base.OnLoad(e);
        }

        private void showESignMetadataProperties()
        {
            ddlMetadataProperty.Items.Add(new ListItem() { Text = "Select a property", Value = string.Empty });

            Type type = typeof(ESignEventMetadata);
            List<PropertyInfo> properties = type.GetProperties().Cast<PropertyInfo>().OrderBy(p => p.Name).ToList();

            SPQuery postsQuery = new SPQuery();
            postsQuery.Query = string.Format(
                "<Query>" +
                   "<OrderBy>" +
                      "<FieldRef Name='Title' />" +
                   "</OrderBy>" +
                "</Query>");


            string list = Request["listSettings"];
            if (string.IsNullOrEmpty(list)) return;

            SPList dataList = Utility.GetListFromURL(list);
            SPListItemCollection data = dataList.GetItems(postsQuery);
            IEnumerable<SPListItem> items = data.Cast<SPListItem>();
            foreach (var item in items)
            {
                ddlMetadataProperty.Items.Add(new ListItem() { Text = item["Title"].ToString(), Value = item["Title"].ToString() });
            }
        }

        protected List<SPField> getAvailableColumns()
        {
            IEnumerable<SPField> fields = null;

            if (!string.IsNullOrEmpty(base.ContentTypeId))
            {
                //Load user colum from content type.
                SPContentType ct = base.GetContentType();
                if (ct != null)
                {
                    fields = ct.Fields.Cast<SPField>();
                }
            }
            else
            {
                fields = SPContext.Current.Web.AvailableFields.Cast<SPField>();
            }
            List<SPField> results = fields.Where(p => !p.Hidden)
                .OrderBy(p => p.Title)
                .ToList();
            return results;
        }
        private void showSiteColumns()
        {
            List<SPField> userFields = getAvailableColumns();
            ControlHelper.LoadFieldsToDropdown(ddlItemColumn, userFields);           
        }
        public override TaskActionSettings GetAction()
        {
            EnsureChildControls();
            if (chkRemoveAction.Checked) return null;
            return new UpdateWorkflowItemWithESignVariablesSettings()
            {
                FieldId = ddlItemColumn.SelectedValue,
                VariableName = ddlMetadataProperty.SelectedValue
            };
        }
        private void displayActionData()
        {
            showSiteColumns();
            showESignMetadataProperties();

            if (Action == null) return;
            UpdateWorkflowItemWithESignVariablesSettings savedAction = (UpdateWorkflowItemWithESignVariablesSettings)Action;
            ddlMetadataProperty.SelectedValue = savedAction.VariableName;
            ddlItemColumn.SelectedValue = savedAction.FieldId;

        }
    }
}
