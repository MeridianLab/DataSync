<%@ Page Language="C#" Title="Scheduler" AutoEventWireup="true" CodeBehind="Scheduler.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Harvest.Bridge.WebSite.Config.Scheduler" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
#tblDetails.jumbotron th, td {
  padding: 3px;
}
    </style>
    <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    <div class="jumbotron">
        <table border="0" style="width:95%;padding:5px;">
            <tr>
                <td colspan="3" style="text-align:center"><h2>Bridge Sync Schedule</h2></td>
            </tr>
            <tr>
                <td style="text-align:right">Next Scheduled Run</td>
                <td><asp:Label ID="lblNextRunTime" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align:right">Runtime Frequeny(Minutes):</td>
                <td><asp:Label ID="lblRunFrequency" runat="server" Text=""></asp:Label></td>
                <td></td>
            </tr>
        </table>
    </div>

    <table class="table">
        <tr><th>Day</th><th>Use Default</th><th>Enabled</th><th>Start Time (hours 0-24)</th><th>Stop Time (hours 0-24)</th><th>Run Frequency (Minutes)</th></tr>
        <asp:PlaceHolder ID="plhTableData" runat="server"></asp:PlaceHolder>
    </table>

    <table>
        <tr>
            <th colspan="2">Daily Sync Catchup Runs</th>
        </tr>
        <tr>
            <td>Enabled</td><td>
                <asp:CheckBox ID="chkCatchupEnabled" runat="server" /></td>
        </tr>
        <tr>
            <td>Start Hour (0-24)</td><td>
                <asp:TextBox ID="txtCatchupStartHour" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Number of Days</td><td>
                <asp:TextBox ID="txtCatchupNumOfDays" runat="server"></asp:TextBox></td>
        </tr>
    </table>

    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_OnClick" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_OnClick" />
</asp:Content>