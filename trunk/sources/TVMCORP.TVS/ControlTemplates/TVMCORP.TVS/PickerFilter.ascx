<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PickerFilter.ascx.cs"
    Inherits="TVMCORP.TVS.Controls.PickerFilter" %>


    <script language="javascript" type="text/javascript">
        window.resizeTo(800, 520);
        function executeQuery() {
            
                   var operators=document.getElementById('queryOperators');
                   var query=document.getElementById('queryTextBox');
                    var list=document.getElementById('columnList');
                    var search='';
                    var multiParts = new Array();
                        multiParts.push(operators.value);
             if(list!=null)
                        multiParts.push(list.value);
                    else
                        multiParts.push('');
                    multiParts.push(query.value);

                    search = ConvertMultiColumnValueToString(multiParts);
                    PickerDialogSetClearState();
                    
                    var ctx = new PickerDialogCallbackContext();
                    ctx.resultTableId = 'resultTable';
                    ctx.queryTextBoxElementId = 'queryTextBox';
                    ctx.errorElementId = 'ctl00_PlaceHolderDialogBodySection_ctl07';
                    ctx.htmlMessageElementId = 'ctl00_PlaceHolderDialogBodySection_ctl08';
                    ctx.queryButtonElementId = 'queryButton';
                    PickerDialogShowWait(ctx);
                    WebForm_DoCallback('ctl00$PlaceHolderDialogBodySection$ctl06',search,PickerDialogHandleQueryResult,ctx,PickerDialogHandleQueryError,true);
                }</script>

<table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
    <tbody>
        <asp:Repeater runat="server" ID="rptFilterCriteria">
            <ItemTemplate>
           
        <tr style="width: 100%;">
            <td style="white-space: nowrap" class="ms-descriptiontext">
                <label for="ctl00_PlaceHolderDialogBodySection_ctl06_queryTextBox">
                    <b>
                        <asp:Literal Text="text" ID="ltrFieldName" runat="server" /></b>&nbsp;
                        <asp:HiddenField runat="server" ID="fieldId" />
                        </label>
               <%-- <select onchange="SearchFieldChanged();" class="ms-pickerdropdown" id="ctl00_PlaceHolderDialogBodySection_ctl06_columnList"
                    name="ctl00$PlaceHolderDialogBodySection$ctl06$columnList">
                    <option value="fa564e0f-0c70-4ab9-b863-0177e6ddd247" selected="selected">Tiêu đề</option>
                </select>--%>
            </td>
            <td style="padding-right: 20px;">
                <asp:DropDownList runat="server" CssClass="ms-pickerdropdown" ID="ddlQUeryOpt">
                    
                </asp:DropDownList>
                <%--<select class="ms-pickerdropdown" id="ctl00_PlaceHolderDialogBodySection_ctl06_queryOperators"
                    name="ctl00$PlaceHolderDialogBodySection$ctl06$queryOperators">
                    <option value="Contains">contains</option>
                    <option value="BeginsWith">begins with</option>
                    <option value="Eq">equals</option>
                    <option value="Neq">not equal</option>
                </select>--%>
            </td>
            <td style="width: 100%;">
                <asp:PlaceHolder runat="server" id="plhControl">
                </asp:PlaceHolder>
                <%--<input type="text" style="width: 100%;" alwaysenablesilent="true" onkeydown="var e=event; if(!e) e=window.event; if(!browseris.safari &amp;&amp; e.keyCode==13) { document.getElementById('ctl00_PlaceHolderDialogBodySection_ctl06_queryButton').click(); return false; }"
                    class="ms-pickersearchbox" accesskey="S" id="ctl00_PlaceHolderDialogBodySection_ctl06_queryTextBox"
                    maxlength="1000" name="ctl00$PlaceHolderDialogBodySection$ctl06$queryTextBox">--%>
            </td>
            
        </tr>
         </ItemTemplate>
        </asp:Repeater>
        <tr>
        <td colspan="3" align="right">
                <div class="ms-searchimage">
                    <input type="image" style="border-width: 0px;" onclick="executeQuery();return false;"
                        alt="Search" src="/_layouts/images/gosearch.gif" title="Search" id="queryButton" runat="server"/></div>
            </td>
        </tr>
    </tbody>
</table>
