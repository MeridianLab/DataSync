<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProjectStepUpdate.ascx.cs" Inherits="Harvest.Bridge.WebSite.Editor.Ctrls.ctrlProjectStepUpdate" %>
<table border="1" style="width:100%">
    <tr>
        <th colspan="3" style="text-align:center">Step Type SQL Update</th>
    </tr>
    <tr>
        <td colspan="2"><asp:CheckBox ID="chkEnabled" Text="Enabled" runat="server" />&nbsp;Step Name: <asp:TextBox ID="txtStepName" runat="server" Width="100%"></asp:TextBox></td>
        <td style="width:33%">&nbsp;</td>
    </tr>
    <tr>
        <td><span>Source DB Name <asp:DropDownList ID="drpDBSourceName" runat="server"></asp:DropDownList></span></td>
        <td><span>Target DB Name <asp:DropDownList ID="drpDBTargetName" runat="server"></asp:DropDownList></span></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3"><asp:TextBox ID="txtSQL" runat="server" Rows="20" TextMode="MultiLine" Width="95%"></asp:TextBox>
        </td>        
    </tr>
</table>
<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" />