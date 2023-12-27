<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProjectStepDataSync.ascx.cs" Inherits="Harvest.Bridge.WebSite.Editor.Ctrls.ctrlProjectStepDataSync" %>
<table border="1" style="width:100%">
    <tr>
        <th colspan="3" style="text-align:center">Step Data Sync</th>
    </tr>
    <tr>
        <td><asp:CheckBox ID="chkEnabled" Text="Enabled" runat="server" />&nbsp;Step Name: <asp:TextBox ID="txtStepName" runat="server" Width="100%"></asp:TextBox></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><span>Source DB Name <asp:DropDownList ID="drpDBSourceName" runat="server"></asp:DropDownList></span></td>
        <td></td>
    </tr>
      <tr>
        <td><span>Target DB Name <asp:DropDownList ID="drpTargetName" runat="server"></asp:DropDownList></span></td>
        <td></td>
    </tr>
    <tr>
        <td>Tablename: <asp:TextBox ID="txtTableName" runat="server"></asp:TextBox></td>
        <td></td>
    </tr>
    <tr>
        <td>Batch Size: <asp:TextBox ID="txtBatchSize" runat="server"></asp:TextBox></td>
        <td></td>
    </tr>
    <tr>
        <td>Primary Key Column: <asp:TextBox ID="txtPrimaryKeyClm" runat="server"></asp:TextBox></td>
        <td></td>
    </tr>
    <tr>
        <td>Epoch Time Column <asp:TextBox ID="txtEpochTimeClm" runat="server"></asp:TextBox></td>
        <td></td>
    </tr>

</table>
<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" />