<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
	Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
	Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="uc" TagName="EmailSelector" Src="~/_controltemplates/Hypertek.IOffice.Infrastructure/EmailTemplateSelector.ascx" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproverUC.ascx.cs"
	Inherits="Hypertek.IOffice.Workflow.Core.Controls.ApproverUC" EnableViewState="true" %>
<script src="/_layouts/1033/jquery.maskedinput-1.3.js" type="text/javascript"></script>
<script type="text/javascript">
	jQuery(document).ready(function () {
		//alert('hihi hehe');
		$("#<%=txtDurationPerTask.ClientID %>").mask("99");

	});
</script>
<tr>
	<td style="padding: 5px 10px 5px 5px; border: 1px solid #676767;" valign="top">
		<table border="0" cellspacing="0" width="100%">
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<H3 class="ms-standardheader">Approval Level Name</H3>
				</td>
				<td valign="top" class="ms-formbody">
					<asp:TextBox ID="txtAppLevelName" CssClass="ms-long ms-spellcheck-true" runat="server" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<H3 class="ms-standardheader">Approver(s)</H3>
				</td>
				<td valign="top" class="ms-formbody">
					<asp:RadioButtonList ID="rblApproverType" runat="server" RepeatDirection="Horizontal" Width="100%" AutoPostBack="true">
					</asp:RadioButtonList>
					<br />
					<asp:DropDownList ID="cboMetadata" runat="server" Width="100%"></asp:DropDownList>
					<SharePoint:PeopleEditor runat="server" ID="peSpecificUsesGroup" Width="100%" MultiSelect="true" SelectionSet="User,SecGroup,SPGroup" Visible="false"/>
					<asp:Label ID="lblManager" runat="server" Text="Return the manager of user who starts workflow " Visible="false"></asp:Label>
					<br /><br />
					<asp:CheckBox Text="Enable change approver" ID="chkEnableChangeApprovers" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="2" valign="top" class="ms-formlabel">
					<asp:CheckBox ID="chkExpandGroup" runat="server" Text="Expand Group" />&nbsp;&nbsp;&nbsp;
					<asp:CheckBox ID="chkIgnore" runat="server" Text="Ignore if there is no participant" />
				</td>
			</tr>
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<H3 class="ms-standardheader">Task sequence typeype</H3>
				</td>
				<td   valign="top" class="ms-formbody">
					<asp:RadioButtonList ID="rblTaskSequenceType" runat="server" RepeatColumns="2" Width="169px">
						<asp:ListItem Text="Parallel" Value="Parallel"></asp:ListItem>
						<asp:ListItem Selected="True" Text="Serial" Value="Sequence"></asp:ListItem>
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<H3 class="ms-standardheader">Due date for all task</H3>
				</td>
				<td  valign="top" class="ms-formbody">
					<asp:TextBox ID="txtDueDate" CssClass="ms-long ms-spellcheck-true" runat="server" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<H3 class="ms-standardheader">Duration per task (day)</H3>
				</td>
				<td  valign="top" class="ms-formbody">
					<asp:TextBox ID="txtDurationPerTask" CssClass="ms-long ms-spellcheck-true" runat="server" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="2" valign="top" class="ms-formlabel">
					<asp:CheckBox ID="chkEnableEmail" runat="server" Text="Enable Email" />
				</td>
			</tr>
			<tr>
				<td colspan="2" valign="top" class="ms-formlabel">
					<uc:EmailSelector runat="server" ID="notifyEmail" />
				</td>
			
			</tr>
			<tr>
				<td colspan="2" valign="top" class="ms-formlabel">
					<asp:CheckBox ID="chkAllowChangeMessage" runat="server" Text="Allow Change Message" />
				</td>
			</tr>
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<H3 class="ms-standardheader">Task Content Type</H3>
				</td>
				<td  valign="top" class="ms-formbody">
					<asp:DropDownList ID="ddlContentType" runat="server" Width="100%"></asp:DropDownList><br />
				</td>
			</tr>
			<tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<h3 class="ms-standardheader">Task Title</h3>
				</td>
				<td  valign="top" class="ms-formbody">
					<asp:TextBox ID="txtTaskTitle" CssClass="ms-long ms-spellcheck-true" runat="server" Width="100%"></asp:TextBox>
					<br />
					<asp:CheckBox Text="Append Name/Title to task title" runat="server" Checked="true" ID="chkAppendTitle" />
				</td>
			</tr>

            <tr>
				<td width="30%" valign="top" class="ms-formlabel">
					<h3 class="ms-standardheader">Properties Update</h3>
				</td>
				<td  valign="top" class="ms-formbody">
					<SharePoint:GroupedItemPicker id="MultiLookupPicker" runat="server"
			                CandidateControlId="SelectCandidate"
			                ResultControlId="SelectResult"
			                AddButtonId="AddButton"
			                RemoveButtonId="RemoveButton"
			                />
		                <table class="ms-long" cellpadding="0" cellspacing="0" border="0">
			                <tr>
				                <td class="ms-input">
					                <SharePoint:SPHtmlSelect id="SelectCandidate" width="143" height="125" runat="server" multiple="true" />
				                </td>
				                <td style="padding-left:10px">
				                <td align="center" valign="middle" class="ms-input"><button class="ms-ButtonHeightWidth" id="AddButton" runat="server"> <SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,multipages_gip_add%>" EncodeMethod='HtmlEncode'/> </button><br />
					                <br /><button class="ms-ButtonHeightWidth" id="RemoveButton" runat="server"> <SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="<%$Resources:wss,multipages_gip_remove%>" EncodeMethod='HtmlEncode'/> </button>
				                </td>
				                <td style="padding-left:10px">
				                <td class="ms-input">
					                <SharePoint:SPHtmlSelect id="SelectResult" width="143" height="125" runat="server" multiple="true" />
				                </td>
			                </tr>
		                </table>
				</td>
			</tr>

		</table>
	</td>
</tr>
<tr>
	<td>
		<img width="1" height="1" alt="" src="/_layouts/images/blank.gif">
	</td>
</tr>
