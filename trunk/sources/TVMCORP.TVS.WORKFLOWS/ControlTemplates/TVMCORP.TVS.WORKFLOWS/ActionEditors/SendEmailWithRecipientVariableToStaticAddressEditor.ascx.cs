using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TVMCORP.TVS.WORKFLOWS.Controls;
using TVMCORP.TVS.WORKFLOWS.MODELS;
using System.Text;

namespace TVMCORP.TVS.WORKFLOWS.CONTROLTEMPLATES.TVMCORP.TVS.WORKFLOWS
{
    public partial class SendEmailWithRecipientVariableToStaticAddressEditor : ActionEditorControl
    {
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

        protected override void OnInit(EventArgs e)
        {
            chkRemoveAction.Attributes.Add("onclick", string.Format("{0}_click(this);", chkRemoveAction.ClientID));
            base.OnInit(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                _displayActionData();
            }
            base.OnLoad(e);
        }
        public override TaskActionSettings GetAction()
        {
            EnsureChildControls();
            if (chkRemoveAction.Checked) return null;
            return new SendEmailWithESignVariableToStaticAddressesSettings()
            {
                EmailAddress = txtEmail.Text.Trim(),
                EmailTemplateName = TaskEmailTemplateSelector.TemplateName,
                EmailTemplateUrl = TaskEmailTemplateSelector.TemplateUrl
            };
        }
        private void _displayActionData()
        {
            if (Action == null) return;
            SendEmailWithESignVariableToStaticAddressesSettings savedAction = (SendEmailWithESignVariableToStaticAddressesSettings)Action;
            txtEmail.Text = savedAction.EmailAddress;
            TaskEmailTemplateSelector.TemplateName = savedAction.EmailTemplateName;
            TaskEmailTemplateSelector.TemplateUrl = savedAction.EmailTemplateUrl;
        }
    }
}
