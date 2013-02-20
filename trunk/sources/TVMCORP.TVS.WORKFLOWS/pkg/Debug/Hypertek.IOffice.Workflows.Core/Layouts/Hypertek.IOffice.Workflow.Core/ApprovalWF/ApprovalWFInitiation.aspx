<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>

<%@ Page Language="C#" DynamicMasterPageFile="~masterurl/default.master" AutoEventWireup="true"
    Inherits="Hypertek.IOffice.Workflow.Core.Workflows.ApprovalWFInitiation" CodeBehind="ApprovalWFInitiation.aspx.cs" %>

    

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:CssRegistration ID="CssRegistration3" Name="/_layouts/1033/styles/Themable/layouts.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration2" Name="/_layouts/1033/styles/Themable/corev4.css" runat="server" />
    <SharePoint:CssRegistration ID="CssRegistration1" Name="/_layouts/1033/styles/Themable/forms.css" runat="server" />
    
    <style type="text/css">
        .ms-ButtonHeightWidth 
        {
            width: 14.2em !important;
        }
    </style>
    <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="0" cellSpacing="0" cellPadding="0">
        <tr>
            <td style="padding: 0px 0px 2px 0px;">
                <H3 class="ms-standardheader" style="font-size:1.1em">
			        <nobr><%=Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_Approvers %></nobr>
		        </H3>
            </td>
        </tr>
        <asp:Repeater ID="cdcatalog" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="padding: 5px 10px 5px 5px; border: 1px solid #676767;" valign="top">
                        <table border="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="2" valign="top" class="ms-formlabel">
                                    <SharePoint:EncodedLiteral runat="server" EncodeMethod='HtmlEncode' Id='targetAppAdminDescription'/>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" valign="top" class="ms-formlabel">
                                    <H3 class="ms-standardheader"><%=Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_Approvers %></H3>
                                </td>
                                <td  valign="top" class="ms-formbody">
                                    <SharePoint:PeopleEditor runat="server" ID="peSpecificUsesGroup" ValidatorEnabled="true" SelectionSet="User,SecGroup,SPGroup" ForceClaims="true" width='400px' />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" valign="top" class="ms-formlabel">
                                    <H3 class="ms-standardheader"><%= Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_TaskDueDate%></H3>
                                </td>
                                <td  valign="top" class="ms-formbody">
                                    <asp:TextBox runat="server" ID="txtDueDate" CssClass="ms-long ms-spellcheck-true" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" valign="top" class="ms-formlabel">
                                    <H3 class="ms-standardheader"><%= Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_TaskDuration%></H3>
                                </td>
                                <td  valign="top" class="ms-formbody">
                                    <asp:TextBox runat="server" ID="txtDuration" CssClass="ms-long ms-spellcheck-true" Enabled="false" ReadOnly="true" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td width="30%" valign="top" class="ms-formlabel">
                                    <H3 class="ms-standardheader"><%= Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_MessageToApprover%></H3>
                                </td>
                                <td  valign="top" class="ms-formbody">
                                   
                                    <asp:TextBox runat="server" CssClass="ms-long ms-spellcheck-true" ID="txtMessage" TextMode="MultiLine" Rows="10" Visible="true"></asp:TextBox>

                                    <br />
                                </td>
                            </tr>
                          
                            <tr runat="server" id="trEmailMessage">
                                <td width="30%" valign="top" class="ms-formlabel">
                                    <H3 class="ms-standardheader"><% = Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_EmailMessage%></H3>
                                </td>
                                <td  valign="top" class="ms-formbody">
                                    <label><% = Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_EmailTitle%></label> <br />

                                    <asp:TextBox runat="server" CssClass="ms-long ms-spellcheck-true" ID="txtMessageTitle" TextMode="SingleLine"/>
                                    <br />
                                    <br />
                                    <label>Resources.ApprovalWorkflowResources.ApprovalWF_InitPage_EmailBody%>:</label> <br />
                                   <SharePoint:InputFormTextBox ID="rtfMessage" RichText="true" RichTextMode="FullHtml" runat="server" TextMode="MultiLine" Rows="20"></SharePoint:InputFormTextBox>

                                </td>
                            </tr>

                            
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <tr>
            <td style="padding: 5px 0px 2px 0px;">
                <asp:Button ID="StartWorkflow" CssClass="ms-ButtonHeightWidth" runat="server" OnClick="StartWorkflow_Click" Text="Start Workflow" />
                <asp:Button ID="Cancel" CssClass="ms-ButtonHeightWidth" runat="server" OnClick="Cancel_Click" Text="Cancel" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Workflow Initiation Form
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
    Workflow Initiation Form
</asp:Content>
