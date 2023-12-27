<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlVariable.ascx.cs" Inherits="Harvest.Bridge.WebSite.Editor.Ctrls.ctrlVariable" %>
<table border="1" style="width:100%">
    <tr>
        <th colspan="2" style="text-align:center">Global Variable</th>
    </tr>
    <tr>
        <td><asp:CheckBox ID="chkEnabled" Text="Enabled" runat="server" />&nbsp;Name: <asp:TextBox ID="txtVariableName" runat="server" Width="100%"></asp:TextBox></td>
        <td style="width:33%">&nbsp;</td>
    </tr>
    <tr>
        <td><span>Source DB Name <asp:DropDownList ID="drpDBSourceName" runat="server"></asp:DropDownList></span></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><span>Target DB Name <asp:DropDownList ID="drpDBTargetName" runat="server"></asp:DropDownList></span></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><span>Value Type <asp:DropDownList ID="drpValueType" runat="server"></asp:DropDownList></span></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2"><asp:TextBox ID="txtSQL" runat="server" Rows="20" TextMode="MultiLine" Width="95%"></asp:TextBox></td>
    </tr>
</table>
<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" />