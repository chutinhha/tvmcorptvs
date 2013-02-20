<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseForm.ascx.cs"
    Inherits="TVMCORP.TVS.ControlTemplates.TVMCORP.TVS.PurchaseForm" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<style type="text/css">
    .container_form
    {
        width: 960px;
        margin: 0 auto;
        align: center;
        margin: 0px 10px 10px 10px;
    }
    .title_request
    {
        margin: 0px 0px 0px 5px;
        padding: 10px 0px 10px 0px;
        font: bold 18px Arial, Helvetica, sans-serif;
        color: #003366;
        text-transform: uppercase;
    }
    .title_request span
    {
        font: bold 12px Arial, Helvetica, sans-serif;
        color: #757575;
    }
    .title_unit
    {
        color: #000000;
        font: normal 12px Arial,Helvetica, sans-serif;
        text-align: left;
        padding: 5px 5px 5px 5px;
    }
    .title_unit span
    {
        font: bold 14px Arial, Helvetica, sans-serif;
        color: #ff0000;
        padding: 0px 0px 0px 5px;
    }
    .date_request
    {
        color: #000000;
        font: normal 12px Arial,Helvetica, sans-serif;
        padding: 5px 5px 5px 5px;
    }
    .date_request span
    {
        font: normal 12px Arial, Helvetica, sans-serif;
        color: #ff0000;
        padding: 0px 0px 0px 5px;
    }
    
    table.tablelist
    {
        width: 100%;
        border: 1px solid #e2edf9;
        border-collapse: collapse;
    }
    
    table.tablelist tr td
    {
        padding: 5px;
        font: normal 11px Arial,Helvetica, sans-serif;
        color: #4d4d4d;
        border: 1px solid #45aefe !important;
    }
    table.tablelist tr.row_title td
    {
        background: #038cd3;
        color: #ffffff;
        font: bold 12px Arial,Helvetica, sans-serif;
        text-decoration: none;
        padding: 5px 5px 5px 5px;
    }
    
    table.tablelist tr.row1 td
    {
        background: #cce6f5;
        color: #000000;
        font: bold 12px Arial,Helvetica, sans-serif;
        text-transform: uppercase;
        padding: 5px 5px 5px 5px;
    }
    table.tablelist tr.row2 td
    {
        background: #ffffff;
        color: #303030;
        font: normal 12px Arial,Helvetica, sans-serif;
    }
    table.tablelist tr.row2 td.request_text_lable
    {
        background-color: #ffffff;
        color: #000000;
        font: normal 12px Arial,Helvetica, sans-serif;
        padding: 5px 5px 5px 5px;
    }
    table.tablelist tr.row2 td.sum
    {
        background-color: #ffffff;
        color: #000000;
        font: bold 16px Arial,Helvetica, sans-serif;
        padding: 5px 5px 5px 5px;
    }
    table.tablelist tr.row2 td.sum span
    {
        background-color: #ffffff;
        color: #ff0000;
        font: bold 16px Arial,Helvetica, sans-serif;
        padding: 5px 5px 5px 5px;
    }
</style>
<span id='part1'>
    <SharePoint:InformationBar ID="InformationBar1" runat="server" />
    <div id="listFormToolBarTop">
        <wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&amp;#160;"
            runat="server">
            <Template_RightButtons>
                <SharePoint:NextPageButton ID="NextPageButton1" runat="server" />
                <SharePoint:SaveButton ID="SaveButton1" runat="server" />
                <SharePoint:GoBackButton ID="GoBackButton1" runat="server" />
            </Template_RightButtons>
        </wssuc:ToolBar>
    </div>
    <SharePoint:FormToolBar ID="FormToolBar1" runat="server" />
    <SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage1" runat="server" />
    <table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
        width="100%">
        <%--<SharePoint:ChangeContentType ID="ChangeContentType1" runat="server" />--%>
        <SharePoint:FolderFormFields ID="FolderFormFields1" runat="server" />
        <%--<SharePoint:ListFieldIterator ID="ListFieldIterator1" runat="server" />--%>
        <table style="display: none;">
            <tr>
                <td>
                    <SharePoint:FormField FieldName="Title" ID="ffTitle" runat="server">
                    </SharePoint:FormField>
                </td>
                <td>
                    <SharePoint:FormField FieldName="Department" ID="ffDepartment" runat="server">
                    </SharePoint:FormField>
                </td>
                <td>
                    <SharePoint:FormField FieldName="RequestDate" ID="ffRequestDate" runat="server">
                    </SharePoint:FormField>
                </td>
            </tr>
        </table>
        <div class="container_form">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="100px">
                        <img src="../../_layouts/images/TVMCORP.TVS.WORKFLOWS/logo_form.png" alt="logo"
                            align="absmiddle" />
                    </td>
                    <td width="860px" class="title_request" align="center">
                        <asp:Literal ID="literalTitleRequest" runat="server"></asp:Literal>
                        <br />
                        <span>
                            <asp:Literal ID="literalMonthRequest" runat="server"></asp:Literal>
                            <asp:Literal ID="literalMonthYear" runat="server"></asp:Literal>
                        </span>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" class="title_unit" align="left">
                        <asp:Literal ID="literalDepartmentRequest" runat="server"></asp:Literal>
                        : <span>
                            <asp:Literal ID="literalDepartment" runat="server"></asp:Literal>
                        </span>
                    </td>
                    <td width="50%" class="date_request" align="right">
                        <asp:Literal ID="literalDateRequestTitle" runat="server"></asp:Literal>
                        : <span>
                            <asp:Literal ID="literalDateRequest" runat="server"></asp:Literal>
                        </span>
                    </td>
                </tr>
            </table>
            <table width="100%" cellspacing="0" cellpadding="0" class="tablelist" border="2"
                style="border-collapse: collapse">
                <asp:Repeater ID="repeaterStationery" runat="server">
                    <HeaderTemplate>
                        <tr class="row_title">
                            <td width="10%" align="center" valign="middle">
                                <asp:Literal ID="literalStationerySTT" runat="server"></asp:Literal>
                            </td>
                            <td width="40%" align="center" valign="middle">
                                <asp:Literal ID="literalStationeryNameRequest" runat="server"></asp:Literal>
                            </td>
                            <td width="10%" align="center" valign="middle">
                                <asp:Literal ID="literalStationeryUnit" runat="server"></asp:Literal>
                            </td>
                            <td width="10%" align="center" valign="middle">
                                <asp:Literal ID="literalStationeryQuantityRequest" runat="server"></asp:Literal>
                            </td>
                            <td width="30%" align="center" valign="middle">
                                <asp:Literal ID="literalStationeryNote" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="trParentCategory" runat="server" class="row1">
                            <td align="left" valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" valign="middle" colspan="4">
                                <asp:Literal ID="literalParentName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr id="trChildCategory" runat="server" class="row2">
                            <td align="center" valign="middle" class="request_text_lable">
                                <asp:Literal ID="literalOrderNumber" runat="server"></asp:Literal>
                            </td>
                            <td align="left" valign="middle" class="request_text_lable">
                                <asp:Literal ID="literalStationeryName" runat="server"></asp:Literal>
                            </td>
                            <td align="center" valign="middle" class="request_text_lable">
                                <asp:Literal ID="literalUnit" runat="server"></asp:Literal>
                            </td>
                            <td align="center" valign="middle" class="request_text_lable">
                                <div id="divAmount" runat="server">
                                </div>
                            </td>
                            <td align="left" valign="middle" class="request_text_lable">
                                <div id="divNote" runat="server">
                                </div>
                            </td>
                        </tr>
                        <tr class="row2" style="display: none;">
                            <td align="center" valign="middle" class="sum" colspan="3">
                                Tổng
                            </td>
                            <td align="left" valign="middle" class="sum">
                            </td>
                            <td align="left" valign="middle" class="request_text_lable">
                                &nbsp;
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <SharePoint:ApprovalStatus ID="ApprovalStatus1" runat="server" />
        <SharePoint:FormComponent ID="FormComponent1" TemplateName="AttachmentRows" runat="server" />
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="ms-formline">
                <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
        <tr>
            <td width="100%">
                <SharePoint:ItemHiddenVersion ID="ItemHiddenVersion1" runat="server" />
                <SharePoint:ParentInformationField ID="ParentInformationField1" runat="server" />
                <SharePoint:InitContentType ID="InitContentType1" runat="server" />
                <wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&amp;#160;"
                    runat="server">
                    <Template_Buttons>
                        <SharePoint:CreatedModifiedInfo ID="CreatedModifiedInfo1" runat="server" />
                    </Template_Buttons>
                    <Template_RightButtons>
                        <SharePoint:SaveButton ID="SaveButton2" runat="server" />
                        <SharePoint:GoBackButton ID="GoBackButton2" runat="server" />
                    </Template_RightButtons>
                </wssuc:ToolBar>
            </td>
        </tr>
    </table>
</span>
<SharePoint:AttachmentUpload ID="AttachmentUpload1" runat="server" />
