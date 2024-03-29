﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;


namespace TVMCORP.TVS.CustomFields
{
    public class LinkViewItemFieldControl : TextField
    {

        protected Label EmailPrefix;
        protected Label EmailValueForDisplay;


        protected override string DefaultTemplateName
        {
            get
            {
                if (this.ControlMode == SPControlMode.Display)
                {
                    return this.DisplayTemplateName;
                }
                else
                {
                    return "LinkViewItemFieldControl";
                }
            }
        }

        public override string DisplayTemplateName
        {
            get
            {
                return "LinkViewItemFieldControlForDisplay";
            }
            set
            {
                base.DisplayTemplateName = value;
            }
        }

        protected override void CreateChildControls()
        {
            if (this.Field != null)
            {
                //base.CreateChildControls();
                //   this.EmailPrefix = (Label)TemplateContainer.FindControl("EmailPrefix");
               // this.textBox = (TextBox)TemplateContainer.FindControl("TextField");
                //this.EmailValueForDisplay = (Label)TemplateContainer.FindControl("EmailValueForDisplay");
            }

            if (this.ControlMode != SPControlMode.Display)
            {
                if (!this.Page.IsPostBack)
                {
                    if (this.ControlMode == SPControlMode.New)
                    {
                        textBox.Text = "";


                    }
                }


            }
            else
            {


                EmailValueForDisplay.Text = (String)this.ItemFieldValue;
            }
        }
        public override object Value
        {
            get
            {
                EnsureChildControls();
                return base.Value;
            }
            set
            {
                EnsureChildControls();
                base.Value = (String)value;

            }
        }

    }

}
