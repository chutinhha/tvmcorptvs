using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Microsoft.SharePoint;
using TVMCORP.TVS.UTIL.Helpers;
using Microsoft.SharePoint.WebControls;
using System.Linq.Expressions;
using TVMCORP.TVS.UTIL.Utilities.Camlex;
using TVMCORP.TVS.UTIL;

namespace TVMCORP.TVS.Controls
{
    public partial class PickerFilter : UserControl
    {
        public List<SPField> SearchableFields { get; set; }
        protected override void OnInit(EventArgs e)
        {
            rptFilterCriteria.ItemDataBound += new RepeaterItemEventHandler(rptFilterCriteria_ItemDataBound);
            queryButton.ServerClick += new ImageClickEventHandler(queryButton_ServerClick);
            base.OnInit(e);
        }

        void queryButton_ServerClick(object sender, ImageClickEventArgs e)
        {
            
        }

        
        void rptFilterCriteria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SPField field = e.Item.DataItem as SPField;

                Literal ltrFieldName = e.Item.FindControl("ltrFieldName") as Literal;
                DropDownList ddlQUeryOpt = e.Item.FindControl("ddlQUeryOpt") as DropDownList;
                PlaceHolder plhControl = e.Item.FindControl("plhControl") as PlaceHolder;
                HiddenField fieldId = e.Item.FindControl("fieldId") as HiddenField;
                ltrFieldName.Text = field.Title;
                fieldId.Value = field.Id.ToString();

                //var fieldControl = field.FieldRenderingControl;
                //fieldControl.ControlMode = Microsoft.SharePoint.WebControls.SPControlMode.New;
                var fieldControl = ControlHelper.BuildValueSelectorControl(field, "");

                FormField formField = new FormField();
                formField.ControlMode = SPControlMode.New;
                formField.ListId = field.ParentList.ID;
                formField.FieldName = field.InternalName;

                //fieldControl.ListId = SPContext.Current.List.ID;

                plhControl.Controls.Add(formField);
                ControlHelper.LoadOperatorDropdown(field, ddlQUeryOpt);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            {
                rptFilterCriteria.DataSource = SearchableFields;
                rptFilterCriteria.DataBind();
            }
        }
        public string GetFilterCAML()
        {
            var expressions = new List<Expression<Func<SPListItem, bool>>>();
            //expressions.Add(x => (int)x["ID"] == 1);
            //expressions.Add(y => (string)y["Title"] == "Hello world");

            

            foreach (RepeaterItem item in rptFilterCriteria.Items)
            {
                SPField field = item.DataItem as SPField;

                Literal ltrFieldName = item.FindControl("ltrFieldName") as Literal;
                DropDownList ddlQUeryOpt = item.FindControl("ddlQUeryOpt") as DropDownList;
                PlaceHolder plhControl = item.FindControl("plhControl") as PlaceHolder;
                HiddenField fieldId = item.FindControl("fieldId") as HiddenField;

                FormField formField = plhControl.Controls[0] as FormField;
                if (formField.Value != null && ddlQUeryOpt.SelectedValue != Constants.NOT_APPLY_VALUE)
                {
                    Expression<Func<SPListItem, bool>> exp = GetSearchExp(formField.Value, formField.Field, ddlQUeryOpt.SelectedValue);
                    if (exp != null)
                    {
                        expressions.Add(exp);
                    }
                };

            }
            if(expressions.Count ==0) return string.Empty;
            string caml = Camlex.Query().WhereAll(expressions).ToString();
            return caml;
        }

        private Expression<Func<SPListItem, bool>> GetSearchExp(object searchValue, SPField field, string opt)
        {
            Expression<Func<SPListItem, bool>> func= null;
            Operators op = (Operators)Enum.Parse(typeof(Operators),opt);
            switch (op)
            {
                case Operators.Equal:
                    func = (y => (string)y[field.Id] == searchValue.ToString());
                    break;
                case Operators.NotEqual:
                    func = (y => (string)y[field.Id] != searchValue.ToString());
                    break;
                case Operators.EarlierThan:
                    break;
                case Operators.GreaterThan:
                        switch (field.Type)
	                {
                        case SPFieldType.Number:
                        case SPFieldType.Currency:

                            func = (y => y[field.Id] > (DataTypes.Number)searchValue.ToString());
                            break;
                        
                        default:
                            break;
	                }
                    break;
                case Operators.StartsWith:
                    break;
                case Operators.EndWith:
                    break;
                case Operators.Contains:
                    func = (y => ((string)y[field.Id]).Contains(searchValue.ToString()));
                    break;
                case Operators.LessThan:
                    switch (field.Type)
                    {
                        case SPFieldType.Number:
                        case SPFieldType.Currency:

                            func = (y => y[field.Id] < (DataTypes.Number)searchValue.ToString());
                            break;

                        default:
                            break;
                    }

                    break;
                case Operators.LaterThan:
                    break;
                case Operators.IsNull:
                    break;
                case Operators.IsNotNull:
                    break;
                default:
                    break;
            }
            return func;
        }
    }
}
