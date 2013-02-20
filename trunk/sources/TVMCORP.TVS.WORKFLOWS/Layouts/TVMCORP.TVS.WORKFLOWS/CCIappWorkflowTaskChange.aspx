<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CCIappWorkflowTaskChange.aspx.cs" Inherits="TVMCORP.TVS.WORKFLOWS.Layouts.CCIappWorkflowTaskChange" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="CCI" Src="~/_controltemplates/TVMCORP.TVS.WORKFLOWS/LinkToDocumentEditor.ascx" TagName="LinkToDocumentEditor" %>

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
        <div class="header">Infomation Requested</div>
	    <br /><div style="border: 1px solid #D8D8D8;" ></div>
        <div class="content">Due by: <asp:Label runat="server" ID="lblDueBy" ></asp:Label></div>

	    <div class="content">
		    <asp:TextBox ID="txtInstruction" Columns="50" Height="100" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:Textbox>
	    </div>
		<div class="content">Type your response:<br/>
			<asp:TextBox ID="txtComment" Columns="50" Height="100" runat="server" TextMode="MultiLine"></asp:Textbox>
		</div>
		<div class="button">
			<asp:Button ID="btnSendResponse" runat="server" Text="Send Response" CssClass="ms-ButtonHeightWidth" style="width:auto"/>
			<asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" CssClass="ms-ButtonHeightWidth" style="width:auto"/>
		</div>
		<br /><div style="border: 1px solid #D8D8D8;" ></div>
		<div class="header" >Other options</div>
		<div class="content" style="padding-left:3px"> 
            <asp:HyperLink runat="server" ID="hplReassign" Target="_self" >Reassign task</asp:HyperLink>			
		</div>
    </div>
	</td>
		<td width="10px"></td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Task Change
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Task Change
</asp:Content>