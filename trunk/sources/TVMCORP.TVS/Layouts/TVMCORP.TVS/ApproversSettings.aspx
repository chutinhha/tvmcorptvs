<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproversSettings.aspx.cs" Inherits="TVMCORP.TVS.Layouts.TVMCORP.TVS.ApproversSettings" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
        <wssuc:inputformsection id="InputFormSection1" title="Trưởng bộ phận" description="" runat="server">
            <template_inputformcontrols>
                <wssuc:InputFormControl ID="InputFormControl1" runat="server">
                    <Template_Control>
                        <SharePoint:PeopleEditor runat="server" ID="peTruongBoPhan" Width="100%" MultiSelect="false" SelectionSet="User"/>
                        <asp:CheckBox runat="server" ID="chkAllowToChangeTruongBophan" Text="Cho phép thay đổi khi biên tập"></asp:CheckBox>
                    </Template_Control>
                </wssuc:InputFormControl>
            </template_inputformcontrols>
        </wssuc:inputformsection>
        <wssuc:inputformsection id="InputFormSection2" title="Người mua hàng" description="" runat="server">
            <template_inputformcontrols>
                <wssuc:InputFormControl ID="InputFormControl2" runat="server">
                    <Template_Control>
                        <SharePoint:PeopleEditor runat="server" ID="peNguoiMuaHang" Width="100%" MultiSelect="false" SelectionSet="User"/>
                        <asp:CheckBox runat="server" ID="chkAllowToChangeNguoiMuaHang" Text="Cho phép thay đổi khi biên tập"></asp:CheckBox>
                    </Template_Control>
                </wssuc:InputFormControl>
            </template_inputformcontrols>
        </wssuc:inputformsection>
        <wssuc:inputformsection id="InputFormSection3" title="Người duyệt" description="" runat="server">
            <template_inputformcontrols>
                <wssuc:InputFormControl ID="InputFormControl3" runat="server">
                    <Template_Control>
                        <SharePoint:PeopleEditor runat="server" ID="peNguoiDuyet" Width="100%" MultiSelect="false" SelectionSet="User"/>
                        <asp:CheckBox runat="server" ID="chkAllowToChangeNguoiDuyet" Text="Cho phép thay đổi khi biên tập"></asp:CheckBox>
                    </Template_Control>
                </wssuc:InputFormControl>
            </template_inputformcontrols>
        </wssuc:inputformsection>
        <wssuc:inputformsection id="InputFormSection4" title="Phòng kế toán" description="" runat="server">
            <template_inputformcontrols>
                <wssuc:InputFormControl ID="InputFormControl4" runat="server">
                    <Template_Control>
                        <SharePoint:PeopleEditor runat="server" ID="pePhongKeToan" Width="100%" MultiSelect="false" SelectionSet="User"/>
                        <asp:CheckBox runat="server" ID="chkAllowToChangePhongKeToan" Text="Cho phép thay đổi khi biên tập"></asp:CheckBox>
                    </Template_Control>
                </wssuc:InputFormControl>
            </template_inputformcontrols>
        </wssuc:inputformsection>
        <wssuc:inputformsection id="InputFormSection5" title="Người xác nhận" description="" runat="server">
            <template_inputformcontrols>
                <wssuc:InputFormControl ID="InputFormControl5" runat="server">
                    <Template_Control>
                        <SharePoint:PeopleEditor runat="server" ID="peNguoiXacNhan" Width="100%" MultiSelect="false" SelectionSet="User"/>
                        <asp:CheckBox runat="server" ID="chkAllowToChangeNguoiXacNhan" Text="Cho phép thay đổi khi biên tập"></asp:CheckBox>
                    </Template_Control>
                </wssuc:InputFormControl>
            </template_inputformcontrols>
        </wssuc:inputformsection>
        <wssuc:buttonsection id="ButtonSection1" runat="server" showstandardcancelbutton="false">
            <template_buttons>
			    <asp:Button runat="server" class="ms-ButtonHeightWidth" Text="<%$Resources:wss,multipages_okbutton_text%>" 
			                id="btnSave" UseSubmitBehavior="false" accesskey="<%$Resources:wss,okbutton_accesskey%>" />			
			
                <asp:Button runat="server" class="ms-ButtonHeightWidth" Text="Delete" ID="btnDelete" CausesValidation="False" Visible="false" />

                <asp:Button runat="server" class="ms-ButtonHeightWidth" Text="Cancel" ID="btnCancel" CausesValidation="False" />
		</template_buttons>
        </wssuc:buttonsection>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Approvers Settings
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Approvers Settings
</asp:Content>
