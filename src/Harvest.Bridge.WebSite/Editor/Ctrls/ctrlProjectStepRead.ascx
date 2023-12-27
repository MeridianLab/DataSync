<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProjectStepRead.ascx.cs" Inherits="Harvest.Bridge.WebSite.Editor.Ctrls.ctrlProjectStepRead" %>
<table border="1" style="width:100%">
    <tr>
        <th colspan="3" style="text-align:center">Step Type SQL Read</th>
    </tr>
    <tr>
        <td><asp:CheckBox ID="chkEnabled" Text="Enabled" runat="server" />&nbsp;Step Name: <asp:TextBox ID="txtStepName" runat="server" Width="100%"></asp:TextBox></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><span>Source DB Name <asp:DropDownList ID="drpDBSourceName" runat="server"></asp:DropDownList></span></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3"><asp:TextBox ID="txtSQL" runat="server" Rows="20" TextMode="MultiLine" Width="95%"></asp:TextBox>
        </td>        
    </tr>
    <tr>
        <td colspan="2">Result Action: <asp:DropDownList ID="drpResultAction" runat="server"></asp:DropDownList>
            &nbsp;Variable Name:<asp:TextBox ID="txtVariableName" runat="server"></asp:TextBox></td>
        <td>&nbsp;</td>
    </tr>
</table>
<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" />