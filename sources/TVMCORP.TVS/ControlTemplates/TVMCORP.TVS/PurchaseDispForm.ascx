<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseDispForm.ascx.cs" Inherits="TVMCORP.TVS.ControlTemplates.TVMCORP.TVS.PurchaseDispForm" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<style type="text/css">
    .container_form
    {
        width: 960px;
        margin: 0 auto;
        align: center;
        margin: 0px 10px 10px 10px;
        font-size: 12px !important;
    }
    
    .title_company
    {
        margin: 0px 0px 0px 5px;
        padding: 10px 0px 10px 0px;
        font: bold 14px Arial, Helvetica, sans-serif;
        color: #003366;
        text-transform: uppercase;
    }
    .title_company span
    {
        font-style: italic;
        font: bold 12px Arial, Helvetica, sans-serif;
        color: #757575;
    }
    
    .title_request
    {
        margin: 0px 0px 0px 5px;
        padding: 10px 0px 10px 0px;
        font: bold 24px Arial, Helvetica, sans-serif;
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
        font: bold 16px Arial, Helvetica, sans-serif;
        padding: 5px 5px 5px 5px;
    }
    
    .request_column1
    {
        padding: 3px 15px 3px 0px;
    }
    .request_column2
    {
        padding: 3px 0px 3px 15px;
    }
    
    
    /*--People Editor--*/
    .ms-usereditor
    {
        width: 100% !important;
    }
    
</style>
<span id='part1'>
    <SharePoint:InformationBar ID="InformationBar1" runat="server" />
    <%--<div id="listFormToolBarTop">
        <wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&amp;#160;"
            runat="server">
            <Template_RightButtons>
                <SharePoint:NextPageButton ID="NextPageButton1" runat="server" />
                <SharePoint:SaveButton ID="SaveButton1" runat="server" />
                <SharePoint:GoBackButton ID="GoBackButton1" runat="server" />
            </Template_RightButtons>
        </wssuc:ToolBar>
    </div>--%>
    <SharePoint:FormToolBar ID="FormToolBar1" runat="server" />
    <SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage1" runat="server" />
    <table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0" width="100%">
        <%--<SharePoint:ChangeContentType ID="ChangeContentType1" runat="server" />--%>
        <SharePoint:FolderFormFields ID="FolderFormFields1" runat="server" />
        <%--<SharePoint:ListFieldIterator ID="ListFieldIterator1" runat="server" />--%>
        
        <div class="container_form">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="40%" align="right">
                        <img src="../../_layouts/images/TVMCORP.TVS/logo_form.png" alt="logo" align="absmiddle" />
                    </td>
                    <td width="60%" class="title_company" align="center">
                        <asp:Literal ID="literalCompany" Text="CÔNG TY CỔ PHẦN TRUYỀN THÔNG TRÍ VIỆT" runat="server"></asp:Literal>
                        <br />
                        <span>
                            <asp:Literal ID="literalCompanyEnglish" Text="TRI VIET MEDIA CORP.(TVM)" runat="server"></asp:Literal>
                        </span>
                        <%--<br /> 
                        <span>
                            <asp:Literal ID="literalMonthRequest" runat="server"></asp:Literal>
                            <asp:Literal ID="literalMonthYear" runat="server"></asp:Literal>
                        </span>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="title_request" align="center">
                        <asp:Literal ID="literalTitle" Text="ĐỀ NGHỊ MUA HÀNG" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="request_column1" width="50%" align="right">
                        <asp:Literal ID="literal1" Text="Tiêu đề :" runat="server"></asp:Literal>
                    </td>
                    <td class="request_column2" width="50%" align="left">
                        <SharePoint:FormField FieldName="Title" ID="ffTitle" runat="server"> </SharePoint:FormField>
                    </td>
                </tr>
                <tr>
                    <td class="request_column1" width="50%" align="right">
                        <asp:Literal ID="literalDateRequest" Text="Ngày :" runat="server"></asp:Literal>
                    </td>
                    <td class="request_column2" width="50%" align="left">
                        <asp:Literal ID="literalDateRequestValue" Text="17/03/2013" runat="server"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td class="request_column1" width="50%" align="right">
                        <asp:Literal ID="literalUserRequest" Text="Người đề nghị :" runat="server"></asp:Literal>
                    </td>
                    <td class="request_column2" width="50%" align="left">
                        <%--<asp:Literal ID="literalUserRequestValue" Text="Trần Anh Tuấn" runat="server"></asp:Literal>--%>
                        <SharePoint:FormField FieldName="UserRequest" ID="ffUserRequest" runat="server"> </SharePoint:FormField>
                    </td>
                </tr>

                <tr>
                    <td class="request_column1" width="50%" align="right">
                        <asp:Literal ID="literalDepartmentRequest" Text="Bộ phận :" runat="server"></asp:Literal>
                    </td>
                    <td class="request_column2" width="50%" align="left">
                        <%--<asp:Literal ID="literalDepartmentRequestValue" Text="Dịch Vụ - Kỹ Thuật" runat="server"></asp:Literal>--%>
                        <SharePoint:FormField FieldName="DepartmentRequest" ID="ffDepartmentRequest" runat="server"> </SharePoint:FormField>
                    </td>
                </tr>

                <tr>
                    <td class="request_column1" width="50%" align="right">
                        <asp:RadioButton ID="rdbTypeOfApproval1" Text="Hành chính" GroupName="TypeOfApproval" Checked="true" runat="server" />
                    </td>
                    <td class="request_column2" width="50%" align="left">
                        <asp:RadioButton ID="rdbTypeOfApproval2" Text="Công nghệ thông tin" GroupName="TypeOfApproval" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;
                        <br />
                        &nbsp;&nbsp;
                    </td>
                </tr>


                <tr>
                    <td colspan="2">
                        <table width="100%" cellspacing="0" cellpadding="0" class="tablelist" border="2" style="border-collapse: collapse" >
                            <tr>
                                <td style="padding: 5px 0px 5px 0px; text-transform: uppercase; font: bold 16px Arial, Helvetica, sans-serif;" colspan="5">
                                    <asp:Literal ID="literalPurchaseDetail" Text="Nội dung mua hàng" runat="server"></asp:Literal>        
                                </td>
                            </tr>
                            <asp:Repeater ID="repeaterPurchaseDetail" runat="server">
                                <HeaderTemplate>
                                    <tr class="row_title">
                                        <td width="5%" align="center" valign="middle">
                                            <asp:Literal ID="literalOrder" Text="STT" runat="server"></asp:Literal>
                                        </td>
                                        <td width="45%" align="center" valign="middle">
                                            <asp:Literal ID="literalProductName" Text="Mô tả" runat="server"></asp:Literal>
                                        </td>
                                        <td width="10%" align="center" valign="middle">
                                            <asp:Literal ID="literalQuantity" Text="Số lượng" runat="server"></asp:Literal>
                                        </td>
                                        <td width="10%" align="center" valign="middle">
                                            <asp:Literal ID="literalPrice" Text="Đơn giá" runat="server"></asp:Literal>
                                        </td>
                                        <td width="30%" align="center" valign="middle">
                                            <asp:Literal ID="literalDescription" Text="Mục đích" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="row2">
                                        <td align="center" valign="middle" class="request_text_lable">
                                            <asp:Literal ID="literalOrderValue" runat="server"></asp:Literal>
                                        </td>
                                        <td align="left" valign="middle" class="request_text_lable">
                                            <asp:Literal ID="literaltProductName" runat="server"></asp:Literal>
                                        </td>
                                        <td align="center" valign="middle" class="request_text_lable">
                                            <asp:Literal ID="literaltQuantity" runat="server"></asp:Literal>
                                        </td>
                                        <td align="center" valign="middle" class="request_text_lable">
                                            <asp:Literal ID="literaltPrice" runat="server"></asp:Literal>
                                        </td>
                                        <td align="left" valign="middle" class="request_text_lable">
                                            <asp:Literal ID="literaltDescription" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;
                        <br />
                        &nbsp;&nbsp;
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <table width="100%" cellspacing="0" cellpadding="0" class="tablelist" border="2" style="border-collapse: collapse" >
                            <tr>
                                <td style="padding: 5px 0px 5px 0px; text-transform: uppercase; font: bold 16px Arial, Helvetica, sans-serif;" colspan="5">
                                    <asp:Literal ID="literalApprovalTitle" Text="Thông tin duyệt" runat="server"></asp:Literal>        
                                </td>
                            </tr>

                            <tr class="row_title">
                                <td width="20%" align="center" valign="middle">
                                    <asp:Literal ID="literalChief" Text="Trưởng bộ phận" runat="server"></asp:Literal>
                                </td>
                                <td width="20%" align="center" valign="middle">
                                    <asp:Literal ID="literalBuyer" Text="Người mua hàng" runat="server"></asp:Literal>
                                </td>
                                <td width="20%" align="center" valign="middle">
                                    <asp:Literal ID="literalApprover" Text="Duyệt" runat="server"></asp:Literal>
                                </td>
                                <td width="20%" align="center" valign="middle">
                                    <asp:Literal ID="literalAccountant" Text="Phòng kế toán" runat="server"></asp:Literal>
                                </td>
                                <td width="20%" align="center" valign="middle">
                                    <asp:Literal ID="literalConfirmer" Text="Người xác nhận" runat="server"></asp:Literal>
                                </td>
                            </tr>
                                
                            <tr class="row2">
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peChief" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="Chief" ID="ffChief" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="left" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peBuyer" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="Buyer" ID="ffBuyer" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peAccountant" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                        <SharePoint:FormField FieldName="Approver" ID="ffApprover" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peApprover" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="Accountant" ID="ffAccountant" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="left" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peConfirmer" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false" 
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="Confirmer" ID="ffConfirmer" runat="server">
                                    </SharePoint:FormField>
                                </td>
                            </tr>

                            <tr class="row2">
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peChief" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="ChiefStatus" ID="ffChiefStatus" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="left" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peBuyer" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="BuyerStatus" ID="ffBuyerStatus" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peAccountant" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="ApproverStatus" ID="ffApproverStatus" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peApprover" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="AccountantStatus" ID="ffAccountantStatus" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="left" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peConfirmer" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false" 
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="ConfirmerStatus" ID="ffConfirmerStatus" runat="server">
                                    </SharePoint:FormField>
                                </td>
                            </tr>

                            <tr class="row2">
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peChief" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="ChiefComment" ID="ffChiefComment" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="left" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peBuyer" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="BuyerComment" ID="ffBuyerComment" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peAccountant" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="ApproverComment" ID="ffApproverComment" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="center" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peApprover" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false"  
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                     <SharePoint:FormField FieldName="AccountantComment" ID="ffAccountantComment" runat="server">
                                    </SharePoint:FormField>
                                </td>
                                <td align="left" valign="middle" class="request_text_lable">
                                    <%--<SharePoint:PeopleEditor ID="peConfirmer" runat="server" SelectionSet="User" CssClass="ms-usereditor" MultiSelect="false" AllowEmpty="false" 
                                        ShowDataValidationErrorBorder="False" ValidatorEnabled="True" ValidateResolvedEntity="True" />--%>
                                    <SharePoint:FormField FieldName="ConfirmerComment" ID="ffConfirmerComment" runat="server">
                                    </SharePoint:FormField>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;
                        <br />
                        &nbsp;&nbsp;
                    </td>
                </tr>

                <tr>    
                    <td colspan="2">
                        <%--<SharePoint:ApprovalStatus ID="ApprovalStatus1" runat="server" />--%>
                        <SharePoint:FormComponent ID="FormComponent1" TemplateName="AttachmentRows" runat="server" />
                    </td>
                </tr>

            </table>
        </div>
        
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
