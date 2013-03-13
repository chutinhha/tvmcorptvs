using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVMCORP.TVS.UTIL.Utilities.Camlex;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;

namespace TVMCORP.TVS.ControlTemplates.TVMCORP.TVS
{
    public class PurchaseHelper
    {
        static Dictionary<int, string> itemDetails;
        public static void GetReferences(GroupedItemPicker groupItemPicker)
        {
            itemDetails = new Dictionary<int, string>();
            string query = string.Format(@"<Where>
                                                <And>
                                                    <Eq>
                                                        <FieldRef Name='Author' LookupId='True'/>
                                                        <Value Type='User'>{0}</Value>
                                                    </Eq>
                                                    <Neq>
                                                        <FieldRef Name='ID' />
                                                        <Value Type='Counter'>{1}</Value>
                                                    </Neq>
                                                </And>
                                            </Where>", SPContext.Current.Web.CurrentUser.ID, SPContext.Current.ItemId);
            SPQuery spQuery = new SPQuery();
            spQuery.ViewFields = string.Concat("<FieldRef Name='ID' />",
                                                "<FieldRef Name='Title' />",
                                                "<FieldRef Name='ContentType' />");
            spQuery.Query = query;
            SPListItemCollection referenceItems = SPContext.Current.List.GetItems(spQuery);
            foreach (SPListItem referenceItem in referenceItems)
            {
                itemDetails.Add(referenceItem.ID, referenceItem.Title);
                groupItemPicker.AddItem(referenceItem.ID.ToString(), referenceItem.Title, string.Empty, referenceItem["ContentType"].ToString());
            }

            if (SPContext.Current.ListItem["References"] != null)
            {
                SPFieldLookupValueCollection selectedReferences = SPContext.Current.ListItem["References"] as SPFieldLookupValueCollection;
                foreach (SPFieldLookupValue selectedReference in selectedReferences)
                {
                    groupItemPicker.AddSelectedItem(selectedReference.LookupId.ToString(), selectedReference.LookupValue);
                }
            }
        }

        public static SPFieldLookupValueCollection GetMultipleItemSelectionValues(GroupedItemPicker groupItemPicker)
        {
            SPFieldLookupValueCollection lookupValues = new SPFieldLookupValueCollection();
            if (groupItemPicker.SelectedIds.Count > 0)
            {
                lookupValues.AddRange(from KeyValuePair<int, string> kvp in itemDetails
                                      where (from gip in groupItemPicker.SelectedIds.Cast<string>()
                                             where Convert.ToInt32(gip) == kvp.Key
                                             select gip).Contains(kvp.Key.ToString())
                                      select new SPFieldLookupValue(kvp.Key, kvp.Value));
            }

            return lookupValues;
        }
    }
}
