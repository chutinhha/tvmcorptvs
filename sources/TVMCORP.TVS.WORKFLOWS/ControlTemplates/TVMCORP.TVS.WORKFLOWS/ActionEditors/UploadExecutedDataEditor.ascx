<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadExecutedDataEditor.ascx.cs" Inherits="TVMCORP.TVS.WORKFLOWS.Controls.UploadExecutedDataEditor" %>

<%@ Register Tagprefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" src="~/_controltemplates/ButtonSection.ascx" %>

<style type="text/css">
    .rblFormat td label
    {
        padding: 0 15px 0 5px;
    }
</style>

<wssawc:FormDigest runat="server" id="FormDigest" />
<div style="color:red" id="errorMessageHolder" enableviewstate="false" runat="server"></div>
<script type="text/javascript">        
		function <%=chkRemoveAction.ClientID%>_click(control){
			ValidatorEnable(document.getElementById('<%=ddlItemColumnValidator.ClientID%>'),!control.checked);
		}
</script>
<TABLE border="0" width="95%" cellspacing="0" cellpadding="0" class="ms-formtable" >
	<wssuc:InputFormSection runat="server" Title="Create & Upload executed documents">
		<Template_Description>
			<SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="Using this action at the final approval step to create executed document for current item/document." EncodeMethod='HtmlEncode'/>
		</Template_Description>
		<Template_InputFormControls>
		
			<wssuc:InputFormControl runat="server" LabelText="Document template">
				<Template_Control>
					<asp:TextBox runat="server" id="txtTemplate" CssClass="ms-long"/>
					 <wssawc:InputFormRequiredFieldValidator ID="ddlItemColumnValidator" 
															ControlToValidate="txtTemplate"
															Display="Dynamic" 
															ErrorMessage="Please enter the url to document template" 
															Runat="server"
															ValidationGroup="SubmitValidate"/>

					<br />
					<asp:CheckBox Text="Copy Metadata" runat="server"  ID="chkCopyMetadata"/>
					<br />
					<asp:CheckBox Text="Copy permission" runat="server" ID="chkCopyPermission" />
					<br /><br />
					Document Format:
					<br />
					<asp:RadioButtonList ID="rblDocumentFormat" runat="server" RepeatDirection="Horizontal" CssClass="rblFormat">
						<asp:ListItem Value="docx" Text="docx" Selected="True"></asp:ListItem>
						<asp:ListItem Value="pdf" Text="pdf"></asp:ListItem>
					</asp:RadioButtonList>
				</Template_Control>
			</wssuc:InputFormControl>
			
			<wssuc:InputFormControl ID="InputFormControl1" runat="server" LabelText="Target Library">
				<Template_Control>
					<asp:TextBox runat="server" id="txtDestination" CssClass="ms-long"/>
					 <wssawc:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" 
															ControlToValidate="txtDestination"
															Display="Dynamic" 
															ErrorMessage="Please enter the url to document library" 
															Runat="server"
															ValidationGroup="SubmitValidate"/>

				   
					
				</Template_Control>
			</wssuc:InputFormControl>


			<wssuc:InputFormControl runat="server">
				<Template_Control>
					<asp:CheckBox ID="chkRemoveAction" Text="Remove this action" runat="server" />
				</Template_Control>
			</wssuc:InputFormControl>

		</Template_InputFormControls>
	</wssuc:InputFormSection>
</TABLE>
<asp:Literal ID="ltrScript" runat="server"/>