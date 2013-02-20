<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="CustomControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint.ApplicationPages" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CCIappWorkflowSendEEC.aspx.cs" Inherits="Hypertek.IOffice.Workflow.Layouts.CCIappWorkflowSendEEC" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="CCI" Src="~/_controltemplates/Hypertek.IOffice.Workflow/LinkToDocumentEditor.ascx" TagName="LinkToDocumentEditor" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
<style>
	.content{
		color:#000000;
		margin-top: 6px; 
		font-size: 8.5pt; 
		font-family:Tahoma;
	}
	.header{
		color:#525252; 
		font-weight:bold; 
		font-size: 8.5pt; 
		font-family:Tahoma;
		margin-left: 3px;
	}
	.button{
		margin-top: 15px;
		text-align: right;
		font-family:Tahoma;
	}
	.link{
		margin-left:  5px;
		margin-right: 5px;
		
	}
</style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <table >
     <tr>
        <td width="10px"></td>
        <td>
             <CCI:LinkToDocumentEditor ID="LinkToDocumentEditor1" runat="server"></CCI:LinkToDocumentEditor>
        </td>
     </tr>

	 <tr>
		<td width="10px"></td>
		<td>
	 <div >
        <div class="header">Send External Email Collaboration</div>
	    <br /><div style="border: 1px solid #D8D8D8;" ></div>
        <table cellpadding="2">
            <tr>
                <td class="content">From:</td>
                <td class="content"><asp:Label ID="lbFrom" runat="server" /></td>
            </tr>
            <tr>
                <td class="content" valign="top">To:</td>
                <td class="content" ><NOBR><asp:Textbox ID="txtTo" runat="server" Width="400"/>
                <asp:RequiredFieldValidator runat="server" Text="*" ControlToValidate="txtTo" ErrorMessage="Please enter email" Display="Dynamic"></asp:RequiredFieldValidator>
                </NOBR>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                    ErrorMessage="Please enter a valid email" ControlToValidate="txtTo" ValidationExpression="(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
             <tr>
                <td class="content" valign="top">CC:</td>
                <td class="content"><asp:Textbox ID="txtCC" runat="server" Width="400"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                    ErrorMessage="Please enter a valid email" ControlToValidate="txtCC" ValidationExpression="(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
             <tr>
                <td class="content" valign="top">Subject:</td>
                <td class="content"><asp:TextBox ID="txtSubject" runat="server" Width="400"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="txtSubject" ErrorMessage="Please enter email subject" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td class="content">Attachments:</td>
                <td class="content"><asp:Label ID="lbAttachments" runat="server" /></td>
            </tr>
             <tr>
                <td class="content" style="vertical-align:text-top">Body:</td>
                <td class="content" valign="top">
                <SharePoint:InputFormTextBox ID="txtBody" RichText="true" CssClass="ms-long"
                                RichTextMode="Compatible" runat="server" TextMode="MultiLine"
                                Rows="15">
                                </SharePoint:InputFormTextBox>
                 <SharePoint:InputFormRequiredFieldValidator runat="server" ControlToValidate="txtBody" Display="Dynamic" ErrorMessage="Please enter email body" Text="*"></SharePoint:InputFormRequiredFieldValidator>
                </td>
            </tr>
        </table>
		<div class="button">
			<asp:Button ID="btnSend" runat="server" Text="Send" CssClass="ms-ButtonHeightWidth" style="width:auto"/>
			<asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" CssClass="ms-ButtonHeightWidth" style="width:auto"/>
		</div>
    </div>
	</td>
		<td width="10px"></td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Send External Email Collaboration
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Send External Email Collaboration
</asp:Content>
