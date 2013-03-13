using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.MODELS;
using TVMCORP.TVS.UTIL.Utilities;
using System.Text;
using TVMCORP.TVS.UTIL.Helpers;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.WORKFLOWS.Controls
{
    public partial class UploadExecutedDataEditor : ActionEditorControl
    {
        private Control control;
        public Guid EditingFieldId
        {
            get
            {
                if (ViewState["EditingFieldId"] == null) return Guid.Empty;
                return (Guid)ViewState["EditingFieldId"];
            }
            set
            {
                ViewState["EditingFieldId"] = value;
            }
        }
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
            }
            if (!IsPostBack)
            {
                displayActionData();
            }
            else
            {
                ReloadEditorControl();
            }
            base.OnLoad(e);
        }

        private void ReloadEditorControl()
        {
            List<SPField> fields = getAvailableColumns();
            SPField field = null;

            if (this.EditingFieldId != Guid.Empty)
            {
                Guid id = this.EditingFieldId;
                field = fields.Where(p => p.Id == id).FirstOrDefault();
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
            return fields.Where(p => !p.Hidden && p.Title != "Predecessors" && p.Title != "Related Issues" && p.Hidden == false)
                .OrderBy(p => p.Title)
                .ToList(); ;

        }
        private void showSiteColumns()
        {
            List<SPField> userFields = getAvailableColumns();
            
        }

        public override TaskActionSettings GetAction()
        {
            EnsureChildControls();
            if (chkRemoveAction.Checked) return null;
            

            return new UploadExecutedSettings()
            {
              TemplateFile = txtTemplate.Text,
             CopyMetadata = chkCopyMetadata.Checked,
             CopyPermission = chkCopyPermission.Checked,
             DestinationLib = txtDestination.Text,
             DocumentFormat = rblDocumentFormat.SelectedValue
            };
        }

        private void displayActionData()
        {
            showSiteColumns();

            if (Action == null) return;

            UploadExecutedSettings savedAction = (UploadExecutedSettings)Action;
            txtTemplate.Text = savedAction.TemplateFile;
            chkCopyPermission.Checked = savedAction.CopyPermission;
            chkCopyMetadata.Checked = savedAction.CopyMetadata;
            txtDestination.Text = savedAction.DestinationLib;

            try
            {
                if (!string.IsNullOrEmpty(savedAction.DocumentFormat))
                {
                    rblDocumentFormat.SelectedValue = savedAction.DocumentFormat;
                }
                
            }
            catch { }
        }

    }
}
