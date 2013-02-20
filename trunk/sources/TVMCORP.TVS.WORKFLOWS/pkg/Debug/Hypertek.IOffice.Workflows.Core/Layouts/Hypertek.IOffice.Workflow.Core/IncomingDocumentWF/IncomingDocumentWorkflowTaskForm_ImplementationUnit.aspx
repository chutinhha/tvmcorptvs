<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Assembly Name="Hypertek.IOffice.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Hypertek.IOffice.Resources" %> 
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomingDocumentWorkflowTaskForm_ImplementationUnit.aspx.cs" Inherits="Hypertek.IOffice.Workflow.Core.Layouts.Hypertek.IOffice.Workflow.Core.IncomingDocumentWorkflowTaskForm_ImplementationUnit" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:UIVersionedContent ID="UIVersionedContent1" UIVersion="4" runat="server">
        <contenttemplate>
            <SharePoint:CssRegistration Name="forms.css" runat="server"/>
        </contenttemplate>
    </SharePoint:UIVersionedContent>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                <table cellspacing="0" cellpadding="4" border="0" width="100%">
                    <tr>
                        <td class="ms-vb">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="ms-formlabel" valign="top" nowrap="true" width="25%">
                                        <%= OfficialDocumentResources.IncomingDocument %>
                                    </td>
                                    <td class="ms-formbody" valign="top" width="75%">
                                        <asp:HyperLink ID="hplIncomingDocTitle" runat="server" Target="_blank"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ms-formlabel" valign="top" nowrap="true" width="25%">
                                        <%= OfficialDocumentResources.IncomingDocumentType %>
                                    </td>
                                    <td class="ms-formbody" valign="top" width="75%">
                                        <asp:Label ID="lblIncomingDocType" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ms-formlabel" valign="top" nowrap="true" width="25%">
                                        <%= OfficialDocumentResources.ImplementationUnitNote %>
                                    </td>
                                    <td width="75%" class="ms-formbody" valign="top">
                                        <asp:TextBox ID="txtImplementationUnitNote" runat="server" TextMode="MultiLine" Rows="6" class="input_form"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="4" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Button Text="Complete" runat="server" ID="btnComplete" />
                        </td>
                        <td>
                            <asp:Button Text="Save" runat="server" ID="btnSave" />
                        </td>
                        <td nowrap="nowrap" class="ms-vb" width="99%">
                            <asp:Button Text="Cancel" runat="server" ID="btnCancel" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1%" class="ms-vb" valign="top">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:ListFormPageTitle ID="ListFormPageTitle1" runat="server" />
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
    <span class="die">
        <SharePoint:ListProperty Property="LinkTitle" runat="server" ID="ID_LinkTitle" />
    </span>
    <SharePoint:ListItemProperty ID="ID_ItemProperty" MaxLength="40" runat="server" />
</asp:Content>
