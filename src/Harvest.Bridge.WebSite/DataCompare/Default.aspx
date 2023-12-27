<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Harvest.Bridge.WebSite.DataCompare.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
.container{
    margin-left:20px;
    margin-right:20px;
}
    </style>
    <asp:Literal ID="ltlInfoMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
    <table style="width:100%">
        <tr>
            <td colspan="2">
                Quey Type For Comparison:<asp:DropDownList ID="drpQueryComparison" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpQueryComparison_OnChange"></asp:DropDownList>
                <p><asp:CheckBox ID="chkExcludeMatches" Text="Only Return Records that do not match" runat="server" Checked="True" /><br />
                <asp:CheckBox ID="chkTwoDigitPrecision" runat="server" Checked="True" Text="Check Result two digit precision"></asp:CheckBox></p>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtSQL" runat="server" TextMode="MultiLine" Width="100%" Height="125px"></asp:TextBox>
            </td>
        </tr>
        <tr><td colspan="2">
            <asp:Button ID="btnRun" runat="server" Text="Run Query" OnClick="btnRun_Click" /></td></tr>
        <tr>
            <th>mlab-db01 results</th><th>mlab-db01n results</th>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdvDB01" runat="server" CssClass="table table-hover"></asp:GridView>
            </td>
            <td>
                <asp:GridView ID="grdvDB01n" runat="server" CssClass="table table-hover"></asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>