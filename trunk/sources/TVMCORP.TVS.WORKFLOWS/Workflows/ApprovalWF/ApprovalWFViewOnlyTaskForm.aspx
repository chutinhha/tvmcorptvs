<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="buttonsection" Src="~/_controltemplates/buttonsection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalWFViewOnlyTaskForm.aspx.cs"
    Inherits="TVMCORP.TVS.WORKFLOWS.Workflows.ApprovalWFViewOnlyTaskForm" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:CssRegistration ID="CssRegistration3" Name="/_layouts/1033/styles/Themable/layouts.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration2" Name="/_layouts/1033/styles/Themable/corev4.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration1" Name="/_layouts/1033/styles/Themable/forms.css" runat="server" />
    <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="0" cellSpacing="0" cellPadding="0">
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;"> 
                <asp:Literal ID="ltrView" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">
                <asp:Literal ID="ltrEdit" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">
                Status: <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">
                <asp:Literal ID="lblRequestBy" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">
                <asp:Literal ID="ltrDueDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">Task Instruction</td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">
                <asp:TextBox ID="txtInitMessage" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="10" Columns="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">Message</td>
        </tr>
        <tr>
            <td colspan="5" style="padding: 0px 0px 2px 0px;">
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="10" Columns="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                <asp:Button runat="server" ID="btnApprove" Text="Approve" Enabled="false" />
            </td>
            <td nowrap="nowrap">
                <asp:Button runat="server" ID="btnReassign" Text="Reassign" Enabled="false"/>
            </td>
            <td nowrap="nowrap">
                <asp:Button runat="server" ID="btnRequestInf" Text="Request Information" Enabled="false" />
            </td>
            <td nowrap="nowrap">
                <asp:Button runat="server" ID="btnReject" Text="Reject" Enabled="false"/>
            </td>
            <td nowrap="nowrap">
                <asp:Button runat="server" ID="btnCancel" Text="Cancel" Enabled="true" OnClientClick="window.frameElement.commitPopup();return false;"/>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Approval Task Form
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Approval Task Form
</asp:Content>
