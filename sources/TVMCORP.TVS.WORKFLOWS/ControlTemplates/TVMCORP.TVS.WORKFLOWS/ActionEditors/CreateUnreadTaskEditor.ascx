﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateUnreadTaskEditor.ascx.cs" Inherits="TVMCORP.TVS.WORKFLOWS.Controls.CreateUnreadTaskEditor" %>

<%@ Register Tagprefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" src="~/_controltemplates/ButtonSection.ascx" %>

<wssawc:FormDigest runat="server" id="FormDigest" />
<div style="color:red" id="errorMessageHolder" enableviewstate="false" runat="server"></div>
<script type="text/javascript">
        function <%=chkRemoveAction.ClientID%>_click(control){
            
        }
    </script>
    
<TABLE border="0" width="95%" cellspacing="0" cellpadding="0" class="ms-formtable" >
	<wssuc:InputFormSection runat="server" Title="Create unread task">
		<Template_Description>
			<SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="Use this action to create unread task item for important document/items" EncodeMethod='HtmlEncode'/>
		</Template_Description>
		<Template_InputFormControls>
		
			<wssuc:InputFormControl runat="server" LabelText="">
			<Template_Control>
                <asp:RadioButton Text="Use content type/list setting" runat="server" ID="radListSetting"  GroupName="SettingOpt"/> <br />
                <asp:RadioButton  Text="Use custom setting" runat="server" ID="radCustomSetting" GroupName="SettingOpt"/> <br />
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