using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.UTIL.Extensions;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Utilities;
using System.Text;

namespace TVMCORP.TVS.Layouts
{
    public partial class DiscussionResolver : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var item = SPContext.Current.ListItem;

            var threadIdStr = item.GetCustomProperty(Constants.THREAD_ID);

            var discussionList = Utility.GetListFromURL(Constants.DISCUSSION_URL);

            if (string.IsNullOrEmpty(threadIdStr))
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(item.Web.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(item.Web.ID))
                        {
                            try
                            {
                                // Required in this case since the http request is a get.
                                web.AllowUnsafeUpdates = true;

                                // Get the elevated item.
                                SPList itemListElevated = web.Lists[item.ParentList.ID];
                                SPListItem itemElevated = itemListElevated.GetItemById(item.ID);

                                // Create a new thread based on the name of the document.
                                SPList discussionListElevated = web.Lists[discussionList.ID];
                                SPListItem newThread = discussionListElevated.Items.Add();
                                newThread["Title"] = itemElevated.DisplayName;
                                newThread["Body"] = GenerateDicussionThreadBody(itemElevated);
                                newThread.SystemUpdate();

                                // Get the ID of the newly created thread;
                                int threadId = newThread.ID;

                                // Now update the document with the ID of the Discussion Thread.
                                itemElevated.SetCustomProperty(Constants.THREAD_ID, newThread.ID.ToString());
                                itemElevated.SystemUpdate();
                                //TODO: This makes the event fire again, which in this case does not hurt anything, but should be changed.
                            }
                            finally
                            {
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    }
                });

                
            }
            var threadItem = discussionList.GetItemById(int.Parse(threadIdStr));
            Response.Redirect(item.Web.Site.MakeFullUrl(threadItem.Url));

        }
        public static string GenerateDicussionThreadBody(SPListItem item)
        {
            // Build the body that will be used in the thread.
            StringBuilder body = new StringBuilder();
            body.Append(@"<table>");
            body.AppendFormat(@"<tr><td><strong>Name:</strong></td><td>&nbsp;</td><td>{0}</td></tr>", item.DisplayName);
            body.AppendFormat(@"<tr><td><strong>View Properties:</strong></td><td>&nbsp;</td><td><a href='{0}'>{1}</a></td></tr>", item.DisplayFormUrl(), item.Url);
            body.Append(@"</table>");
            body.Append(@"<br/><div>Reply to this post to start a discussion around this item.</div>");

            return body.ToString();
        }
       
    }
}
