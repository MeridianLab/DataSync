<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProjectStepVariableRemove.ascx.cs" Inherits="Harvest.Bridge.WebSite.Editor.Ctrls.ctrlProjectStepVariableRemove" %>
<table border="1" style="width:100%">
    <tr>
        <th colspan="2" style="text-align:center">Step Type Remove Variable</th>
    </tr>
    <tr>
        <td><asp:CheckBox ID="chkEnabled" Text="Enabled" runat="server" />&nbsp;Step Name: <asp:TextBox ID="txtStepName" runat="server" Width="100%"></asp:TextBox></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><span>Remove variable <asp:DropDownList ID="drpVariableName" runat="server"></asp:DropDownList></span></td>
        <td>&nbsp;</td>
    </tr>
</table>
<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" />