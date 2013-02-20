using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using System.Reflection;
using System.Text;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using TVMCORP.TVS.Util.Helpers;

namespace TVMCORP.TVS.WORKFLOWS.Controls
{
    public partial class UpdateWorkflowItemWithESignMetaDataEditor : ActionEditorControl
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
            Type type = typeof(ESignEventMetadata);
            List<PropertyInfo> properties = type.GetProperties().Cast<PropertyInfo>().OrderBy(p => p.Name).ToList();

            ddlMetadataProperty.Items.Add(new ListItem() { Text = "Select a property", Value = string.Empty });
            foreach (PropertyInfo pi in properties)
            {
                FriendlyNameAttribute friendlyNameObj = (FriendlyNameAttribute)pi.GetCustomAttributes(typeof(FriendlyNameAttribute), true).FirstOrDefault();
                ddlMetadataProperty.Items.Add(new ListItem() { Text = friendlyNameObj != null ? friendlyNameObj.FriendlyName : pi.Name, Value = pi.Name });
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
            return fields.Where(p => !p.Hidden)
                .OrderBy(p => p.Title)
                .ToList(); ;

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
            return new UpdateWorkflowItemWithESignMetadataSettings()
            {
                FieldId = ddlItemColumn.SelectedValue,
                ESignProperty = ddlMetadataProperty.SelectedValue
            };
        }
        private void displayActionData()
        {
            showSiteColumns();
            showESignMetadataProperties();
            if (Action == null) return;
            UpdateWorkflowItemWithESignMetadataSettings savedAction = (UpdateWorkflowItemWithESignMetadataSettings)Action;
            ddlMetadataProperty.SelectedValue = savedAction.ESignProperty;
            ddlItemColumn.SelectedValue = savedAction.FieldId;
        }
    }
}
