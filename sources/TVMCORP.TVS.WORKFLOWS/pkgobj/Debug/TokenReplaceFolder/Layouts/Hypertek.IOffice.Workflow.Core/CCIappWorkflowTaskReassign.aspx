<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="CustomControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint.ApplicationPages" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CCIappWorkflowTaskReassign.aspx.cs" Inherits="Hypertek.IOffice.Workflow.Layouts.CCIappWorkflowTaskReassign" DynamicMasterPageFile="~masterurl/default.master" %>
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
             <CCI:LinkToDocumentEditor runat="server"></CCI:LinkToDocumentEditor>
        </td>
     </tr>

	 <tr>
		<td width="10px"></td>
		<td>
	 <div >
        <div class="header">Reassign Task</div>
	    <br /><div style="border: 1px solid #D8D8D8;" ></div>
        <div>If you are not the appropriate person to perform this task or if you want to delegate it to another person, use this form to reassign your task to another person.</div>
	    <div class="content">
            Reassign this task to:<br />
            <SharePoint:PeopleEditor ID="peditReasign" runat="server" AllowEmpty="false" MultiSelect="false" Width="400" ValidatorEnabled="true"/>
        </div>
	    <div class="content">
		    If necessary, update the task instructions:<br/>
		    <asp:TextBox ID="txtInstruction" Columns="50" Height="100" runat="server" TextMode="MultiLine"></asp:Textbox>
	    </div>
        <div runat="server" id="divDueDate" visible="false">
        <br /><div style="border: 1px solid #D8D8D8;" ></div>
            <div class="header">Due Date</div>
            <div class="content">
                If a due date is specified and email is enabled on the server, the task owner will receive a reminder on that date if their task is not finished.
            </div>
		    <div class="content">Task is due by:<br/>
			    <SharePoint:DateTimeControl ID="dtDueBy" runat="server" DateOnly="true" />
		    </div>
        </div>
		<div class="button">
			<asp:Button ID="btnApprove" runat="server" Text="Send" CssClass="ms-ButtonHeightWidth" style="width:auto"/>
			<asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" CssClass="ms-ButtonHeightWidth" style="width:auto"/>
		</div>
    </div>
	</td>
		<td width="10px"></td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Task Reassign
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Task Reassign
</asp:Content>
