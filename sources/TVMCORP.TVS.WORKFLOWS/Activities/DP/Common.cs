using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Microsoft.SharePoint;
using System.Net.Mail;
using Microsoft.SharePoint.Workflow;
using System.Workflow.ComponentModel;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.SharePoint.Utilities;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;

namespace TVMCORP.TVS.WORKFLOWS.Activities.DP
{

    public class WorkflowHistoryTraceListener : TraceListener
    {
        ISharePointService service = null;
        Guid workflowInstanceID = default(Guid);


        public WorkflowHistoryTraceListener(ActivityExecutionContext context, Guid workflowInstanceID)
        {
            service = (ISharePointService)context.GetService(typeof(ISharePointService));

            if (service == null)
            {
                throw new InvalidOperationException();
            }

            this.workflowInstanceID = workflowInstanceID;

        }
        public override void Write(string message)
        {
            WriteLine(message);

        }

        public override void WriteLine(string message)
        {
            service.LogToHistoryList(workflowInstanceID, SPWorkflowHistoryEventType.None, 0, TimeSpan.MinValue, "", message, message);
        }
    }

    #region  Trace Provider

    //internal static class TraceProvider
    //{
    //    // Fields
    //    private static ulong hTraceLog;
    //    private static ulong hTraceReg;

    //    // Methods
    //    private static unsafe uint ControlCallback(NativeMethods.WMIDPREQUESTCODE RequestCode, IntPtr Context, uint* InOutBufferSize, IntPtr Buffer)
    //    {
    //        uint num;
    //        switch (RequestCode)
    //        {
    //            case NativeMethods.WMIDPREQUESTCODE.WMI_ENABLE_EVENTS:
    //                hTraceLog = NativeMethods.GetTraceLoggerHandle(Buffer);
    //                num = 0;
    //                break;

    //            case NativeMethods.WMIDPREQUESTCODE.WMI_DISABLE_EVENTS:
    //                hTraceLog = (ulong)0;
    //                num = 0;
    //                break;

    //            default:
    //                num = 0x57;
    //                break;
    //        }
    //        InOutBufferSize[0] = 0;
    //        return num;
    //    }

    //    public static void RegisterTraceProvider()
    //    {
    //        Guid controlGuid = SPFarm.Local.TraceSessionGuid;

    //    }

    //    public static uint TagFromString(string wzTag)
    //    {
    //        Debug.Assert(wzTag.Length == 4);
    //        return (uint)((((wzTag[0] << 0x18) | (wzTag[1] << 0x10)) | (wzTag[2] << 8)) | wzTag[3]);
    //    }

    //    public static void UnregisterTraceProvider()
    //    {
    //        Debug.Assert(NativeMethods.UnregisterTraceGuids(hTraceReg) == 0);
    //    }

    //    public static void WriteTrace(uint tag, TraceSeverity level, Guid correlationGuid, string exeName, string productName, string categoryName, string message)
    //    {
    //        NativeMethods.ULSTrace evnt = new NativeMethods.ULSTrace();
    //        evnt.Header.Size = (ushort)Marshal.SizeOf(typeof(NativeMethods.ULSTrace));
    //        evnt.Header.Flags = 0x20000;
    //        evnt.ULSHeader.dwVersion = 1;
    //        evnt.ULSHeader.dwFlags = NativeMethods.TraceFlags.TRACE_FLAG_ID_AS_ASCII;
    //        evnt.ULSHeader.Size = (ushort)Marshal.SizeOf(typeof(NativeMethods.ULSTraceHeader));
    //        evnt.ULSHeader.Id = tag;
    //        evnt.Header.Class.Level = (byte)level;
    //        evnt.ULSHeader.wzExeName = exeName;
    //        evnt.ULSHeader.wzProduct = productName;
    //        evnt.ULSHeader.wzCategory = categoryName;
    //        evnt.ULSHeader.wzMessage = message;
    //        evnt.ULSHeader.correlationID = correlationGuid;
    //        if (message.Length < 800)
    //        {
    //            ushort num = (ushort)((800 - (message.Length + 1)) * 2);
    //            evnt.Header.Size = (ushort)(evnt.Header.Size - num);
    //            evnt.ULSHeader.Size = (ushort)(evnt.ULSHeader.Size - num);
    //        }
    //        if (hTraceLog != 0)
    //        {
    //            NativeMethods.TraceEvent(hTraceLog, ref evnt);
    //        }
    //    }

    //    // Nested Types
    //    private static class NativeMethods
    //    {
    //        // Fields
    //        internal const int ERROR_INVALID_PARAMETER = 0x57;
    //        internal const int ERROR_SUCCESS = 0;
    //        internal const int TRACE_VERSION_CURRENT = 1;
    //        internal const int WNODE_FLAG_TRACED_GUID = 0x20000;

    //        // Methods
    //        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
    //        internal static extern ulong GetTraceLoggerHandle([In] IntPtr Buffer);
    //        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
    //        internal static extern unsafe uint RegisterTraceGuids([In] EtwProc cbFunc, [In] void* context, [In] ref Guid controlGuid, [In] uint guidCount, IntPtr guidReg, [In] string mofImagePath, [In] string mofResourceName, out ulong regHandle);
    //        [DllImport("advapi32.dll", SetLastError = true)]
    //        internal static extern uint TraceEvent([In] ulong traceHandle, [In] ref ULSTrace evnt);
    //        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
    //        internal static extern uint UnregisterTraceGuids([In] ulong regHandle);

    //        // Nested Types
    //        internal unsafe delegate uint EtwProc(TraceProvider.NativeMethods.WMIDPREQUESTCODE requestCode, IntPtr requestContext, uint* bufferSize, IntPtr buffer);

    //        [StructLayout(LayoutKind.Sequential)]
    //        internal struct EVENT_TRACE_HEADER
    //        {
    //            internal ushort Size;
    //            internal ushort FieldTypeFlags;
    //            internal TraceProvider.NativeMethods.EVENT_TRACE_HEADER_CLASS Class;
    //            internal uint ThreadId;
    //            internal uint ProcessId;
    //            internal long TimeStamp;
    //            internal Guid Guid;
    //            internal uint ClientContext;
    //            internal uint Flags;
    //        }

    //        [StructLayout(LayoutKind.Sequential)]
    //        internal struct EVENT_TRACE_HEADER_CLASS
    //        {
    //            internal byte Type;
    //            internal byte Level;
    //            internal ushort Version;
    //        }

    //        internal enum TraceFlags
    //        {
    //            TRACE_FLAG_END = 2,
    //            TRACE_FLAG_ID_AS_ASCII = 4,
    //            TRACE_FLAG_MIDDLE = 3,
    //            TRACE_FLAG_START = 1
    //        }

    //        [StructLayout(LayoutKind.Sequential)]
    //        internal struct ULSTrace
    //        {
    //            internal TraceProvider.NativeMethods.EVENT_TRACE_HEADER Header;
    //            internal TraceProvider.NativeMethods.ULSTraceHeader ULSHeader;
    //        }

    //        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    //        internal struct ULSTraceHeader
    //        {
    //            internal ushort Size;
    //            internal uint dwVersion;
    //            internal uint Id;
    //            internal Guid correlationID;
    //            internal TraceProvider.NativeMethods.TraceFlags dwFlags;
    //            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
    //            internal string wzExeName;
    //            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
    //            internal string wzProduct;
    //            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
    //            internal string wzCategory;
    //            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 800)]
    //            internal string wzMessage;
    //        }

    //        internal enum WMIDPREQUESTCODE
    //        {
    //            WMI_GET_ALL_DATA,
    //            WMI_GET_SINGLE_INSTANCE,
    //            WMI_SET_SINGLE_INSTANCE,
    //            WMI_SET_SINGLE_ITEM,
    //            WMI_ENABLE_EVENTS,
    //            WMI_DISABLE_EVENTS,
    //            WMI_ENABLE_COLLECTION,
    //            WMI_DISABLE_COLLECTION,
    //            WMI_REGINFO,
    //            WMI_EXECUTE_METHOD
    //        }
    //    }

    //    public enum TraceSeverity
    //    {
    //        Assert = 7,
    //        CriticalEvent = 1,
    //        Exception = 4,
    //        High = 20,
    //        InformationEvent = 3,
    //        Medium = 50,
    //        Monitorable = 15,
    //        Unassigned = 0,
    //        Unexpected = 10,
    //        Verbose = 100,
    //        WarningEvent = 2
    //    }
    //}

    #endregion


    internal struct AttachmentInfo
    {
        public Stream Stream;
        public string FileName;

        public AttachmentInfo(Stream _stream, string _fileName)
        {
            this.Stream = _stream;
            this.FileName = _fileName;


        }
    }


    [global::System.Serializable]
    public class DPWorkflowException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DPWorkflowException() { }
        public DPWorkflowException(string message) : base(message) { }
        public DPWorkflowException(string message, Exception inner) : base(message, inner) { }

        protected DPWorkflowException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    internal static class Common
    {
        /// <summary>
        /// downloads a file over http using a GET request with default credentials
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static Stream GetHttpFileUsingDefaultCredentials(string url)
        {
            return GetHttpFileWCredentials(url, CredentialCache.DefaultNetworkCredentials);

        }
        /// <summary>
        /// downloads a file over http using a GET request with custom credentials
        /// </summary>
        /// <param name="url"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        internal static Stream GetHttpFileWCredentials(string url, ICredentials credentials)
        {
            WebRequest myFileDl = WebRequest.Create(url);

            myFileDl.Credentials = credentials;

            WebResponse myWr = myFileDl.GetResponse();

            return myWr.GetResponseStream();
        }

        /// <summary>
        /// resolves all workflow lookup items within a string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string ProcessStringField(ActivityExecutionContext context, string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            Activity parent = context.Activity;

            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }

            return Helper.ProcessStringField(str, parent, null);

        }


        /// <summary>
        /// adds a specified role for a given user on a list item
        /// </summary>
        /// <param name="web">ref to SPWeb</param>
        /// <param name="listItem">list item in question</param>
        /// <param name="role">role to add for user e.g. Contribute, View or any custom permission level</param>
        /// <param name="loginName">login name of user or sharepoint group</param>
        /// <returns></returns>
        internal static SPListItem SetItemPermissions(SPWeb web, SPListItem listItem, string role, string loginName)
        {

            if (!listItem.HasUniqueRoleAssignments)
            {
                listItem.BreakRoleInheritance(true);

            }

            SPRoleDefinition RoleDefinition = web.RoleDefinitions[role];

            SPPrincipal myUser = FindUserOrSiteGroup(web.Site, loginName);

            if (myUser == null)
            {
                throw new ArgumentException(string.Format("user:{0} is not a valid sharepoint user!"));
            }

            SPRoleAssignment RoleAssignment = new SPRoleAssignment(myUser);

            RoleAssignment.RoleDefinitionBindings.Add(RoleDefinition);

            listItem.RoleAssignments.Add(RoleAssignment);

            return listItem;

        }

        public static string ParseSendTo(WorkflowContext context, ArrayList curArray)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            List<string> list = new List<string>();
            for (num = 0; num < curArray.Count; num++)
            {
                if (curArray[num] == null)
                {
                    list.Add(string.Empty);
                }
                else
                {
                    string[] textArray = curArray[num].ToString().Split(new char[] { ';' });
                    foreach (string text in textArray)
                    {
                        list.Add(text);
                    }
                }
            }
            for (num = 0; num < list.Count; num++)
            {
                if (list[num] != null)
                {
                    if (!flag)
                    {
                        flag = true;
                    }
                    else
                    {
                        builder.Append(";");
                    }
                    builder.Append(Helper.ResolveToEmailName(context, list[num]));
                }
            }
            return builder.ToString();
        }


        /// <summary>
        /// determines whether a given login name is a sharepoint or active directory and returns a common object is the login name is valid
        /// </summary>
        /// <param name="site"></param>
        /// <param name="userOrGroup"></param>
        /// <returns></returns>
        internal static SPPrincipal FindUserOrSiteGroup(SPSite site, string userOrGroup)
        {
            SPPrincipal myUser = null;


            //is this a user
            if (SPUtility.IsLoginValid(site, userOrGroup))
            {

                myUser = site.RootWeb.EnsureUser(userOrGroup);
            }
            else
            {   //might be a group
                foreach (SPGroup g in site.RootWeb.SiteGroups)
                {
                    if (g.Name == userOrGroup)
                        myUser = g;

                }
            }



            return myUser;
        }


        /// <summary>
        /// checks if a user role is assigned - greedy version
        /// </summary>
        /// <param name="site"></param>
        /// <param name="web"></param>
        /// <param name="listId"></param>
        /// <param name="listItem"></param>
        /// <param name="role"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal static bool IsUserRoleAssigned(SPSite site, SPWeb web, string listId, int listItem, string role, string userName)
        {
            bool result = false;

            SPList list = web.Lists[new Guid(listId)];

            SPListItem item = list.GetItemById(listItem);

            try
            {
                SPPrincipal myUser = null;

                myUser = FindUserOrSiteGroup(site, userName);


                SPRoleAssignment myAssigment = item.RoleAssignments.GetAssignmentByPrincipal(myUser);

                if (myAssigment != null)

                    result = myAssigment.RoleDefinitionBindings.Contains(web.RoleDefinitions[role]);

            }
            catch
            {


            }

            return result;

        }

        /// <summary>
        /// checks if a user has a specific role assigned on a list item
        /// </summary>
        /// <param name="context"></param>
        /// <param name="listId"></param>
        /// <param name="listItem"></param>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool IsUserRoleAssigned(WorkflowContext context, string listId, int listItem, string role, string userName)
        {
            bool result = false;



            SPSecurity.RunWithElevatedPrivileges(delegate()
               {

                   using (SPSite site = new SPSite(context.Site.ID))
                   {
                       using (SPWeb web = site.AllWebs[context.Web.ID])
                       {

                           result = IsUserRoleAssigned(site, web, listId, listItem, role, userName);

                       }
                   }
               });


            return result;

        }

        /// <summary>
        /// checks if a given user is part of a sharepoint group
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sharepointGroup"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsUserPartOfSharepointGroup(WorkflowContext context, string listId, int listItem, string user, string sharepointGroup)
        {
            bool result = false;

            SPSecurity.RunWithElevatedPrivileges(delegate()
               {
                   using (SPSite site = new SPSite(context.Site.ID))
                   {
                       using (SPWeb web = site.AllWebs[context.Web.ID])
                       {
                           SPGroup grp = web.SiteGroups[sharepointGroup];

                           foreach (SPUser mbr in grp.Users)
                           {
                               if (mbr.LoginName.ToLower() == user.ToLower())
                               {
                                   result = true;

                               }
                           }
                       }
                   }
               });

            return result;
        }

        /// <summary>
        /// determines if a given list item contains any attachments
        /// </summary>
        /// <param name="context"></param>
        /// <param name="listId"></param>
        /// <param name="listItem"></param>
        /// <param name="user"></param>
        /// <param name="sharepointGroup"></param>
        /// <returns></returns>
        public static bool ListItemContainsAttachments(WorkflowContext context, string listId, int listItem)
        {
            SPList myList = context.Web.Lists[new Guid(listId)];

            SPListItem myItem = myList.Items.GetItemById(listItem);

            return myItem.Attachments.Count > 0;
        }

        /// <summary>
        /// gets all of the attachments for a given list item
        /// </summary>
        /// <param name="listItems"></param>
        /// <returns></returns>
        public static AttachmentInfo[] GetListItemAttachments(SPListItem listItems)
        {
            List<AttachmentInfo> myAttachments = new List<AttachmentInfo>();

            for (int i = 0; i < listItems.Attachments.Count; i++)
            {
                //attachments are actualy SPFiles stored in SPFolder that is part of our list
                SPFile myAttachmentFile = listItems.Web.GetFile(listItems.Attachments.UrlPrefix + listItems.Attachments[i]);

                AttachmentInfo myAI = new AttachmentInfo(myAttachmentFile.OpenBinaryStream(), myAttachmentFile.Name);

                myAttachments.Add(myAI);
            }

            return myAttachments.ToArray();
        }


        /// <summary>
        /// removes any Limited Access Role Assigments from the list item
        /// </summary>
        /// <param name="item"></param>
        public static void RemoveListItemLimitedPermissions(SPListItem item)
        {

            if (!item.HasUniqueRoleAssignments)
                return;


            List<int> usersToRemove = new List<int>();

            foreach (SPRoleAssignment ra in item.RoleAssignments)
            {
                if (ra.RoleDefinitionBindings.Count == 1 && ra.RoleDefinitionBindings.Contains(item.Web.RoleDefinitions.GetByType(SPRoleType.Guest)))
                {
                    usersToRemove.Add(ra.Member.ID);

                }
            }

            foreach (int id in usersToRemove)
            {
                item.RoleAssignments.RemoveById(id);

            }

        }



        /// <summary>
        /// removes role assigments for principal, breaking inheritance if specified
        /// </summary>
        /// <param name="item"></param>
        /// <param name="principal"></param>
        /// <param name="breakRoleInheritance"></param>
        internal static void RemoveListItemPermissionEntry(SPListItem item, string principalName, bool breakRoleInheritance)
        {
            List<int> usersToRemove = new List<int>();


            if (!item.HasUniqueRoleAssignments && breakRoleInheritance)
                item.BreakRoleInheritance(true);
            else if (!item.HasUniqueRoleAssignments)
                return;

            SPPrincipal myPrincipal = FindUserOrSiteGroup(item.Web.Site, principalName);



            foreach (SPRoleAssignment ra in item.RoleAssignments)
            {
                if (ra.Member.ID == myPrincipal.ID)
                {
                    usersToRemove.Add(ra.Member.ID);

                }
            }

            foreach (int id in usersToRemove)
            {
                item.RoleAssignments.RemoveById(id);


            }


        }
        /// <summary>
        /// logs exceptions to sharepoint's workflow history log
        /// </summary>
        /// <param name="e"></param>
        /// <param name="context"></param>
        internal static void LogExceptionToWorkflowHistory(Exception e, ActivityExecutionContext context, Guid wrkflowInstanceID)
        {

            ISharePointService service = (ISharePointService)context.GetService(typeof(ISharePointService));

            if (service == null)
            {
                throw new InvalidOperationException();
            }

            service.LogToHistoryList(wrkflowInstanceID, SPWorkflowHistoryEventType.WorkflowError, 0, TimeSpan.MinValue, "Error", e.ToString(), "");

        }

        internal static void AddCommentWorkflowHistory(string message, ActivityExecutionContext context, Guid wrkflowInstanceID)
        {

            ISharePointService service = (ISharePointService)context.GetService(typeof(ISharePointService));

            if (service == null)
            {
                throw new InvalidOperationException();
            }

            service.LogToHistoryList(wrkflowInstanceID, SPWorkflowHistoryEventType.None, 0, TimeSpan.MinValue, "", message, message);

        }

        internal static DPWorkflowException WrapWithFriedlyException(Exception e, string message)
        {
            return new DPWorkflowException(message, e);

        }


        /// <summary>
        /// resolves the issue of System.Net.Mail's lack of support for email addresses separated by ';'
        /// </summary>
        /// <param name="col"></param>
        /// <param name="addresses"></param>
        private static void PopulateMailAddressCollection(MailAddressCollection col, string addresses)
        {

            if (addresses.Contains(";"))
            {
                string[] addrCol = addresses.Split(';');

                foreach (string addr in addrCol)
                {
                    if (addr.Contains("@"))
                        col.Add(addr);

                }
            }
            else
                col.Add(addresses);

        }
        /// <summary>
        /// sends an email using mail server configured in sharepoint
        /// </summary>
        /// <param name="site"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <param name="attachmentName"></param>
        internal static void SendMailWithAttachment(SPSite site, string from, string to, string cc, string subject, string body, Stream attachment, string attachmentName, bool urgent)
        {
            SendMailWithAttachment(site, from, to, cc, subject, body, new AttachmentInfo[] { new AttachmentInfo(attachment, attachmentName) }, urgent);

        }
        /// <summary>
        /// greedy version
        /// </summary>
        /// <param name="site"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        /// <param name="urgent"></param>
        internal static void SendMailWithAttachment(SPSite site, string from, string to, string cc, string subject, string body, AttachmentInfo[] attachments, bool urgent)
        {
            //check if mail server is specified on web application
            if (!string.IsNullOrEmpty(site.WebApplication.OutboundMailServiceInstance.Server.Address))
            {
                string finalFrom = site.WebApplication.OutboundMailSenderAddress;

                if (!string.IsNullOrEmpty(from))
                    finalFrom = from;

                //create the mail message
                MailMessage mail = new MailMessage();

                //set the addresses


                mail.From = new MailAddress(finalFrom);

                PopulateMailAddressCollection(mail.To, to);

                if (!string.IsNullOrEmpty(cc))
                    PopulateMailAddressCollection(mail.CC, cc);



                if (urgent)
                    mail.Priority = MailPriority.High;



                //set the content
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                foreach (AttachmentInfo ai in attachments)
                {
                    //add an attachment from stream
                    mail.Attachments.Add(new Attachment(ai.Stream, ai.FileName));
                }

                //send the message
                SmtpClient smtp = new SmtpClient(site.WebApplication.OutboundMailServiceInstance.Server.Address);

                smtp.Send(mail);

            }
            else
            {
                throw new InvalidOperationException("Outgoing mail settings are not specified for this web application!");

            }
        }
        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        public static byte[] ReadFully(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }




    }


}
