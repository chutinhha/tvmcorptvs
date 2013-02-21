<%@ Control Language="C#" AutoEventWireup="false" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="ApplicationPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.ApplicationPages.WebControls" %>
<%@ Register TagPrefix="SPHttpUtility" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.Utilities" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>

<%@ Register TagPrefix="uc" TagName="PurchaseNewFormControl" Src="~/_controltemplates/TVMCORP.TVS/PurchaseNewForm.ascx" %>
<%@ Register TagPrefix="uc" TagName="PurchaseEditFormControl" Src="~/_controltemplates/TVMCORP.TVS/PurchaseEditForm.ascx" %>
<%@ Register TagPrefix="uc" TagName="PurchaseDispFormControl" Src="~/_controltemplates/TVMCORP.TVS/PurchaseDispForm.ascx" %>


<SharePoint:RenderingTemplate ID="PurchaseDispFormTemplate" runat="server">
  <Template>
        <uc:PurchaseDispFormControl ID="PurchaseDispFormTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="PurchaseEditFormTemplate" runat="server">
  <Template>
        <uc:PurchaseEditFormControl ID="PurchaseEditFormTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="PurchaseNewFormTemplate" runat="server">
  <Template>
        <uc:PurchaseNewFormControl ID="PurchaseNewFormTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>
