<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendEmailWithRecipientVariableToStaticAddressEditor.ascx.cs" Inherits="TVMCORP.TVS.WORKFLOWS.CONTROLTEMPLATES.TVMCORP.TVS.WORKFLOWS.SendEmailWithRecipientVariableToStaticAddressEditor" %>

<%@ Register Tagprefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="EmailTemplateSelector" src="~/_controltemplates/TVMCORP.TVS.WORKFLOWS/EmailTemplateSelector.ascx" %>


<wssawc:FormDigest runat="server" id="FormDigest" />
<div style="color:red" id="errorMessageHolder" enableviewstate="false" runat="server"></div>
	<script type="text/javascript">
		function <%=chkRemoveAction.ClientID%>_click(control){
			ValidatorEnable_<%=TaskEmailTemplateSelector.ClientID %>(!control.checked);
			ValidatorEnable(document.getElementById('<%=txtEmaiValidator.ClientID%>'),!control.checked);
			ValidatorEnable(document.getElementById('<%=txtEmailRegexValidator.ClientID%>'),!control.checked);
		}
	</script>
<TABLE border="0" width="95%" cellspacing="0" cellpadding="0" class="ms-formtable" >
	<wssuc:InputFormSection runat="server" Title="Send email with ESign Recipient variables to static addresses">
		<Template_Description>
			<SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="This action will send an email to specified email addresses, the email template support to using ESign's variables" EncodeMethod='HtmlEncode'/>
		</Template_Description>
		<Template_InputFormControls>
			<wssuc:EmailTemplateSelector runat="server" id="TaskEmailTemplateSelector" ValidationGroup="SubmitValidate" />
			
			<wssuc:InputFormControl runat="server" LabelText="To Email Addresses - separated by semicolon">
			<Template_Control>
			<wssawc:InputFormTextBox Title="To Email Addresses" class="ms-long" ID="txtEmail" Runat="server" TextMode="MultiLine" Columns="40" Rows="5" />
			 <wssawc:InputFormRequiredFieldValidator ID="txtEmaiValidator" 
														ControlToValidate="txtEmail"
														Display="Dynamic" 
														ErrorMessage="Please enter email addresses separated by semicolon (;)" 
														Runat="server"
														ValidationGroup="SubmitValidate"/>
			<asp:RegularExpressionValidator ID="txtEmailRegexValidator" 
									runat="server"     
									ErrorMessage="<br/>Please enter email addresses separated by semicolon (;)" 
									ControlToValidate="txtEmail"     
									ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
									ValidationGroup="SubmitValidate"
									Display="Dynamic" /><br />

			<asp:CheckBox ID="chkRemoveAction" Text="Remove this action" runat="server"  />					                                    
			</Template_Control>
			</wssuc:InputFormControl>
		</Template_InputFormControls>
	</wssuc:InputFormSection>
	</TABLE>
	<asp:Literal ID="ltrScript" runat="server"/>