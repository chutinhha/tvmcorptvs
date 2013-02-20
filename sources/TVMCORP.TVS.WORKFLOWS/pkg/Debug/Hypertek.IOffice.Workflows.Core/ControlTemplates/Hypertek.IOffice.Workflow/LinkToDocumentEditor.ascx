<%@ Assembly Name="TVMCORP.TVS.WORKFLOWS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=44dc3ce128de1979" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LinkToDocumentEditor.ascx.cs" Inherits="TVMCORP.TVS.WORKFLOWS.Controls.LinkToDocumentEditor" %>

<% if(!string.IsNullOrEmpty(LinkToWorkflowItemProperties)){ %>
<table style="margin-top: 10px;" border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td width="10" valign="center" style="padding: 4px;">
                <img IMG SRC="/_layouts/images/Workflows.gif" alt=/>
            </td>
            <td>
                <a onclick="<%=LinkToWorkflowItemProperties %>" href="#"><%=LinkToWorkflowItemPropertiesText %></a><br />
                <a href="#" onclick="<%=FunctionToWorkflowItemDocument %>"><%=LinkToWorkflowItemDocumentText %></a>
            </td>
        </tr>
    </table>
<% }%>     
