<%@ Assembly Name="TVMCORP.TVS.WORKFLOWS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=44dc3ce128de1979" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc" TagName="ApprovalUC" Src="/_controltemplates/TVMCORP.TVS.WORKFLOWS/ApprovalUC.ascx" %>

<%@ Page Language="C#" DynamicMasterPageFile="~masterurl/default.master" AutoEventWireup="true"
    Inherits="TVMCORP.TVS.WORKFLOWS.Workflows.ApprovalWFAssociation" CodeBehind="ApprovalWFAssociation.aspx.cs" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>


<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <SharePoint:CssRegistration ID="CssRegistration3" Name="/_layouts/1033/styles/Themable/layouts.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration2" Name="/_layouts/1033/styles/Themable/corev4.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration1" Name="/_layouts/1033/styles/Themable/forms.css" runat="server" />
    
    <style type="text/css">
        .ms-ButtonHeightWidth 
        {
            width: 14.2em !important;
        }
        
        .ms-formlabel label
        {
            font-weight: normal !important;
            font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        
        .ms-formbody select
        {
            width: 180px !important;
        }
        
        input[type="checkbox"], input[type="radio"] {
            margin: 0 2px 0 0;
        }
    </style>
    
    <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="0" cellSpacing="0" cellPadding="0">
        <uc:ApprovalUC ID="ucApprovalUC" runat="server" />
        <%--
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <uc:ApprovalUC runat="server" id="xxx">
                    </uc:ApprovalUC>
                    <asp:Panel ID="Panel1" runat="server">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Terminate Options
                </td>
                <td>
                    <asp:Button ID="btnAddApprover" runat="server" Text="Add" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem>End of first rejection</asp:ListItem>
                        <asp:ListItem>End on Item/Document Change</asp:ListItem>
                        <asp:ListItem>Enable content Approval</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    Workflow Option
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Delay on start
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkStartingNofication" runat="server" Text="Starting Nofication" />
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkEnableVerboseLog" runat="server" Text="Enable Verbose Log" />
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkEnableReassign" runat="server" Text="Enable Reassign" />
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="chkEnableRequestChange" runat="server" Text="Enable Request Change" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkEnableHoldOn" runat="server" Text="Enable Hold On" />
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>--%>
        <tr>
            <td style="padding: 5px 0px 2px 0px;">
                <wssuc:toolbar cssclass="ms-formtoolbar" id="toolBarTbl" rightbuttonseparator="&amp;#160;" runat="server">
                    <Template_Buttons>
                        <asp:Button ID="AssociateWorkflow" CssClass="ms-ButtonHeightWidth"  runat="server" OnClick="AssociateWorkflow_Click" Text="Associate Workflow" />
                        <asp:Button ID="Cancel" runat="server" CssClass="ms-ButtonHeightWidth"  Text="Cancel" OnClick="Cancel_Click" />
                    </Template_Buttons>
                </wssuc:toolbar>
            </td>
        </tr>
    </table>
</asp:content>
<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
    Workflow Association Form
    
    
</asp:content>
<asp:content id="PageTitleInTitleArea" runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea">
    Workflow Association Form
</asp:content>
