<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseFilterUserControl.ascx.cs"
    Inherits="TVMCORP.TVS.ListDefinitions.PurchaseDefinition.PurchaseFilter.PurchaseFilterUserControl" %>
<table>
    <tr>
        <td style="padding-bottom: 3px" class="ms-addnew">
            <span class="s4-clust" style="height: 10px; width: 10px; position: relative; display: inline-block;
                overflow: hidden;">
                <img style="left: -0px !important; top: -128px !important; position: absolute;" alt=""
                    src="/_layouts/images/fgimg.png">
            </span>&nbsp;
            <asp:LinkButton ID="linkButtonAdd" runat="server">Add new item</asp:LinkButton>
        </td>
    </tr>
</table>
