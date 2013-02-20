<%@ Assembly Name="Hypertek.IOffice.Workflow.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="uc" TagName="ucApprover" Src="/_controltemplates/Hypertek.IOffice.Workflow.Core/ApproverUC.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmailSelector" Src="~/_controltemplates/Hypertek.IOffice.Infrastructure/EmailTemplateSelector.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalUC.ascx.cs" EnableViewState="true" Inherits="Hypertek.IOffice.Workflow.Core.Controls.ApprovalUC" %>

<%@ Assembly Name="Hypertek.IOffice.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=079ac6f381ab0c9f" %>
<%@ Import Namespace="Hypertek.IOffice.Model"%>
<%@ Import Namespace="Hypertek.IOffice.Model.Workflow"%>
<script type="text/javascript">
    var listId = '<%=ListID %>';
    function AssignTaskEventComplete_Callback(dialogResult, returnValue) {
        if (dialogResult == SP.UI.DialogResult.OK) {

        }
    }
    function showTaskEventEditorDialog(key, title, session, type) {
        
        var contentypeId = "";
        //var key = getSessionkey(mode);
        var mode = false;
        var eventOwner = '<%=EventOwners.ApprovalWF.ToString()%>';
        

        var sDialogUrl = '<%=SPContext.Current.Web.Url%>\u002f_layouts\u002fHypertek.IOffice.Infrastructure\u002fTaskEvent.aspx?CloseOnCancel=true&readonly=' + mode + "&title=" + title + "&session=" + session + "&type=" + type + "&taskContentTypeId=" + contentypeId;
        sDialogUrl = sDialogUrl + "&eventOwner=" + eventOwner + "&timer=" + (new Date()).getTime();
        sDialogUrl = sDialogUrl+ "&List=<%=ListID %>"
        //commonShowModalDialog(sDialogUrl + "&timer=" + (new Date()).getTime(), getEventFormSize(), AssignTaskEventComplete_Callback);
        
        var options = {
            url: sDialogUrl,
            width: 650,
            height: 550,
            title: title,
            dialogReturnValueCallback: AssignTaskEventComplete_Callback
        };
        SP.UI.ModalDialog.showModalDialog(options);

    }

</script>
<asp:HiddenField  id="_uniqueID" runat="server"  />

<tr>
    <td style="padding: 0px 0px 2px 0px;">
        <H3 class="ms-standardheader" style="font-size:1.1em">
			<nobr>Approvers</nobr>
		</H3>
    </td>
</tr>
<tr>
    <td>
	    <asp:Panel runat="server" ID="phUserInfoBox" EnableViewState="true"></asp:Panel>
    </td>
</tr>
<tr>
    <td style="padding: 5px 0px 2px 0px;">
        <wssuc:toolbar cssclass="ms-formtoolbar" id="toolBarTbl" rightbuttonseparator="&amp;#160;" runat="server">
            <Template_Buttons>
                <asp:Button ID="btnAddApprover" CssClass="ms-ButtonHeightWidth"  runat="server" Text="Add" OnClick="btnAddApprover_Click" Width="71px" />
            </Template_Buttons>
        </wssuc:toolbar>
    </td>
</tr>
<tr >
    <td style="padding: 15px 0px 2px 0px;">
        <H3 class="ms-standardheader" style="font-size:1.1em">
			<nobr>Terminate Options</nobr>
		</H3>
    </td>
</tr>
<tr>
	<td style="padding: 5px 10px 5px 5px; border: 1px solid #676767;" valign="top">
        <table border="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2" valign="top" class="ms-formbody">
                    <asp:CheckBoxList ID="chkTeminateOpt" runat="server">
                        <asp:ListItem>End of first rejection</asp:ListItem>
                        <asp:ListItem>End on Item/Document Change</asp:ListItem>
                        <asp:ListItem>Enable content Approval</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td style="padding: 15px 0px 2px 0px;">
        <H3 class="ms-standardheader" style="font-size:1.1em">
			<nobr>Workflow Options</nobr>
		</H3>
    </td>
</tr>
<tr>
	<td style="padding: 5px 10px 5px 5px; border: 1px solid #676767;" valign="top">
        <table border="0" cellspacing="0" width="100%">
            <tr>
                <td width="30%" valign="top" class="ms-formlabel">
                    <H3 class="ms-standardheader">Delay on start</H3>
                </td>
                <td  valign="top" class="ms-formbody">
                    <asp:TextBox ID="txtDelayOnStart" CssClass="ms-long ms-spellcheck-true"  runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td width="30%" valign="top" class="ms-formlabel">
                    <h3 class="ms-standardheader">Execute condition</h3>
                </td>
                <td  valign="top" class="ms-formbody">
                    <asp:CheckBox id="chkEnableExectureCondition" Text="Enable conditional check" runat="server" /> <br />
                    <asp:DropDownList ID="ddlFields" runat="server"></asp:DropDownList>
                    <span> equal </span>

                    <asp:TextBox runat="server" ID="txtConditionValue"></asp:TextBox>

                    <br />

                    <asp:CheckBox Text="Approve data if ignore" runat="server" ID="chkApproveIfByPass" />
                </td>
            </tr>
            <tr>
	            <td width="30%" valign="top" class="ms-formlabel">
                    <h3 class="ms-standardheader">Events & Actions settings</h3>
                </td>
                
                <td  valign="top" class="ms-formbody">
                    
                        <table cellspacing="0 " cellpadding="0" border="0" class="ms-descriptiontext">
				        <tbody>
                         <tr>
						        <td style="padding-bottom: 5px; padding-left:10px"><img src="/_layouts/images/square.gif" alt=""/></td>
						        <td style="padding-left: 5px; padding-bottom: 5px;">
							        <a onclick="showTaskEventEditorDialog(0,'Workflow Started event','<%=_uniqueID.Value %>', '<%=TaskEventTypes.WFStarted.ToString() %>');return false" id="A2" href="#">Workflow Started event</a>
							        <asp:Label Text="" runat="server" ID="Label2" />
						        </td>
					        </tr>

                             <tr>
						            <td style="padding-bottom: 5px; padding-left:10px"><img src="/_layouts/images/square.gif" alt=""/></td>
						            <td style="padding-left: 5px; padding-bottom: 5px;">
							            <a onclick="showTaskEventEditorDialog(0,'Workflow Terminated event','<%=_uniqueID.Value %>', '<%=TaskEventTypes.WorkflowTerminated.ToString() %>');return false" id="A3" href="#">Workflow Terminated event</a>
							            <asp:Label Text="" runat="server" ID="Label3" />
						            </td>
					            </tr>

					        <tr>
						        <td style="padding-bottom: 5px; padding-left:10px"><img src="/_layouts/images/square.gif" alt=""/></td>
						        <td style="padding-left: 5px; padding-bottom: 5px;">
							        <a onclick="showTaskEventEditorDialog(0,'Workflow Approved event','<%=_uniqueID.Value %>', '<%=TaskEventTypes.WFApproved.ToString() %>');return false" id="" href="#">Workflow Approved event</a>
							        <asp:Label Text="" runat="server" ID="lbTaskCreatedActions" />
						        </td>
					        </tr>
					  
                           

                       <tr>
						        <td style="padding-bottom: 5px; padding-left:10px"><img src="/_layouts/images/square.gif" alt=""/></td>
						        <td style="padding-left: 5px; padding-bottom: 5px;">
							        <a onclick="showTaskEventEditorDialog(0,'Workflow Rejected event','<%=_uniqueID.Value %>', '<%=TaskEventTypes.WFRejected.ToString() %>');return false" id="A1" href="#">Workflow Rejected event</a>
							        <asp:Label Text="" runat="server" ID="Label1" />
						        </td>
					        </tr>

																																																						   
				        </tbody>
			        </table>

                    </td>
            </tr>
        
    </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="ms-formlabel">
                    <asp:CheckBox ID="chkStartingNofication" runat="server" Text="Starting Nofication" />
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="ms-formlabel">
                    <uc:EmailSelector runat="server" id="notifyEmail" AllowNull="true" />
                </td>
            </tr>
            <tr>
                <td width="30%" valign="top" class="ms-formlabel">
                    <H3 class="ms-standardheader">
                        <asp:RadioButton ID="rdgUseMetadata" runat="server" Text="Use Metadata" OnCheckedChanged="rdgUseMetadata_CheckedChanged" />
                    </H3>
                </td>
                <td  valign="top" class="ms-formbody">
                    <asp:DropDownList ID="cboUseMetaData" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="30%" valign="top" class="ms-formlabel">
                    <H3 class="ms-standardheader">
                        <asp:RadioButton ID="rdgSpecificUser" runat="server" Text="Specific users/groups"
                        OnCheckedChanged="rdgSpecificUser_CheckedChanged" />
                    </H3>
                </td>
                <td  valign="top" class="ms-formbody">
                    <SharePoint:PeopleEditor runat="server" ID="peSpecificUsesGroup" Width="100%" MultiSelect="true" SelectionSet="User,SecGroup,SPGroup"/>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="ms-formlabel">
                    <asp:CheckBox ID="chkEnableVerboseLog" runat="server" Text="Enable Verbose Log" />
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="ms-formlabel">
                <asp:CheckBox ID="chkEnableApprove" runat="server" Text="Enable Approve" Enabled="false" Checked="true" Width="200px"/> 
                <label>Label:</label> <asp:TextBox ID="txtApproveLable" runat="server"  Text="Approve"/> <br />
                <asp:CheckBox ID="chkEnableReject" runat="server" Text="Enable Reject" Enabled="false" Checked="true"  Width="200px" /> 
                <label>Label:</label> <asp:TextBox ID="txtRejectLabel" runat="server" Text="Reject" /> <br />

                <asp:CheckBox ID="chkEnableReassign" runat="server" Text="Enable Reassign"  Width="200px"/> 
                <label>Label:</label> <asp:TextBox runat="server" ID="txtReassignLabel" Text="Reassign" /> <br />
                <asp:CheckBox ID="chkEnableRequestChange" runat="server" Text="Enable Request Change"  Width="200px"/> 
                <label>Label:</label> <asp:TextBox ID="txtRequestChangeLabel" runat="server"  Text="Request Change"/> <br />

                <asp:CheckBox ID="chkEnableHoldOn" runat="server" Text="Enable Hold On"  Width="200px"/>
                <label>Label:</label> <asp:TextBox ID="txtOnHold" runat="server"  Text="Hold"/> <br />

                <asp:CheckBox ID="chkRequestInfomrmation" runat="server" Text="Request Information"  Width="200px"/>
                <label>Label:</label> <asp:TextBox ID="txtRequestInfLabel" runat="server" Text="Request Info" /> <br />

                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="ms-formlabel">
                    
                </td>
            </tr>
        </table>
    </td>
</tr>



<tr>
    <td style="padding: 15px 0px 2px 0px;">
        <H3 class="ms-standardheader" style="font-size:1.1em">
			<nobr>Update permission</nobr>
		</H3>
    </td>
</tr>

<tr>
	<td style="padding: 5px 10px 5px 5px; border: 1px solid #676767;" valign="top">
        <table border="0" cellspacing="0" width="100%">
            <tr>
                
                <td width="30%" valign="top" class="ms-formlabel">
                    <H3 class="ms-standardheader">
                        <asp:CheckBox ID="chkEnableUpdatePermission" Text="Enable permission update" runat="server" />
                    </H3>
                </td>

                <td  valign="top" class="ms-formbody">
                        <asp:CheckBox ID="chkKeepPermissions" Text="Keep current permission" runat="server" />
                    </td>
            </tr>
        </table>
    </td>
</tr>

<asp:Repeater runat="server" ID="rptPermissions" Visible ="false">
    <ItemTemplate>
        <tr>
	        <td style="padding: 5px 10px 5px 5px; border: 1px solid #676767;" valign="top">
                <table border="0" cellspacing="0" width="100%">
                    <tr>
                
                        <td width="30%" valign="top" class="ms-formlabel">
                            <H3 class="ms-standardheader">
                                <asp:Literal Text="text" runat="server" id="ltrPermissionName"/>
                            </H3>
                        </td>

                        <td  valign="top" class="ms-formbody">

                            <asp:CheckBox Text="Author of current item" runat="server"  id="chkAuthor"/> <br />

                            <asp:CheckBox Text="All approvers" runat="server"  id="chkApprovers"/> <br />
                            <label> or select users from metadata below:</label><br />

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
                    </tr>
                </table>
            </td>
        </tr>

    </ItemTemplate>
</asp:Repeater>




