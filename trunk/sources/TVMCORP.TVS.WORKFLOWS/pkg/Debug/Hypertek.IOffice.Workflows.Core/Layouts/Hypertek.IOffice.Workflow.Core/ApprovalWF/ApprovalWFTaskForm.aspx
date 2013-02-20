<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalWFTaskForm.aspx.cs"
    Inherits="Hypertek.IOffice.Workflow.Core.Workflows.ApprovalWFTaskForm" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:CssRegistration ID="CssRegistration3" Name="/_layouts/1033/styles/Themable/layouts.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration2" Name="/_layouts/1033/styles/Themable/corev4.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration1" Name="/_layouts/1033/styles/Themable/forms.css" runat="server" />
    <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="0" cellSpacing="0" cellPadding="0">
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;"> 
                <asp:Literal ID="ltrView" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
                <asp:Literal ID="ltrEdit" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 10px 0px 2px 0px;">
                <%= Resources.ApprovalWorkflowResources.TaskForm_TaskStatus %> : <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
                <asp:Literal ID="lblRequestBy" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
                <asp:Literal ID="ltrDueDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            
            <td colspan="4" style="padding: 0px 0px 2px 0px;"><%= Resources.ApprovalWorkflowResources.TaskForm_TaskInstruction %></td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
                <asp:TextBox ID="txtInitMessage" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="10" Columns="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
            
            <%= Resources.ApprovalWorkflowResources.TaskForm_Message %>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="10" Columns="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px 0px 2px 0px;">
                <SharePoint:ListFieldIterator runat="server" ID="updatedFieldsIterator">
                
                </SharePoint:ListFieldIterator>
            </td>

        </tr>
        <tr>
            <td colspan="3" align="left">
                <ul style="float:left;list-style-type:none">
                    <li style="float:left; padding-left:20px"><asp:LinkButton runat="server" ID="btnHold" Text="Hold On" CommandArgument="ApprovalWFHoldTaskForm" /></li>
                    <li style="float:left; padding-left:20px"><asp:LinkButton runat="server" ID="btnReassign" Text="Reassign" CommandArgument="ApprovalWFReasignTaskForm" /></li>
                    <li style="float:left;padding-left:20px"><asp:LinkButton runat="server" ID="btnRequestInf" Text="Request Information" CommandArgument="ApprovalWFRequestInfoTaskForm" /></li>
                    <li style="float:left;padding-left:20px"><asp:LinkButton runat="server" ID="btnRequestChange" Text="Request Change" CommandArgument="ApprovalWFChangeTaskForm" /></li>
                </ul>
            </td>
        
             <td nowrap="nowrap" align="right">
                <asp:Button runat="server" ID="btnApprove" Text="Approve" />
                 <asp:Button runat="server" ID="btnReject" Text="Reject" />
                <asp:Button runat="server" ID="btnCancel" Text="Cancel" Enabled="true" OnClientClick="window.frameElement.commitPopup();return false;"/>
            </td>
        </tr>

    </table>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    
</asp:Content>
