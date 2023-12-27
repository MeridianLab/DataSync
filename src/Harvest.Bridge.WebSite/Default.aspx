<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Harvest.Bridge.WebSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
#tblDetails.jumbotron th, td {
  padding: 3px;
}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:Literal ID="ltlInfoMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
            <div class="jumbotron">
                <table border="0" style="width:95%;padding:5px;">
                    <tr>
                        <td colspan="3" style="text-align:center"><h2>Bridge Sync Details</h2></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:center"><h4>Page Load Time: <%= System.DateTime.Now.ToString() %></h4></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;width:33%" class="Title">Current Enabled</td>
                        <td style="width:33%"><asp:Label ID="lblCurrentEnabled" runat="server" Text=""></asp:Label></td>
                        <td><asp:Button ID="btnEnabled" runat="server" CssClass="btn btn-primary" Visible="false" Text="Disable" OnClick="btnEnabled_OnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;vertical-align:top">Next Scheduled</td>
                        <td><asp:Label ID="lblNextRunTime" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblNextScheduleDay" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblIsDefaultSchedule" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnRunNow" runat="server" CssClass="btn btn-primary" Visible="false" Text="Run Now" OnClick="btnRunNow_OnClick" />
                            &nbsp;<asp:LinkButton ID="lnbAdvancedRunNow" runat="server" Visible="false" OnClick="lnbAdvancedRunNow_Click">Advanced</asp:LinkButton>
                            <asp:Panel ID="pnlAdvancedRunNow" runat="server" Visible="false">
                                <asp:Label ID="Label1" runat="server" Text="Run For "></asp:Label>
                                <asp:DropDownList ID="drpJobRunType" runat="server"></asp:DropDownList>
                                <asp:TextBox ID="txtRunForDays" runat="server" Text="0"></asp:TextBox><br />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right">Previous Runtime:</td>
                        <td><asp:Label ID="lblPreviousRuntime" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align:right">Runtime Frequeny(Minutes):</td>
                        <td><asp:Label ID="lblRunFrequency" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align:right">Service Heartbeat:</td>
                        <td><asp:Label ID="lblServiceHeartbeat" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="tmrHistoryView" EventName="Tick" />
        </Triggers>

    </asp:UpdatePanel>
    <asp:CheckBox ID="chkFullHistory" Visible="false" Text="View Full History" runat="server" AutoPostBack="True" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="rptHistory" runat="server">
                <HeaderTemplate>
                    <table border="1" class="table">
                        <tr>
                            <th>Status</th>
                            <th>Description</th>
                            <th>StartDateTime</th>
                            <th>EndDateTime</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <% if (IsUserAuthenticated)
                            { %>
                                <a title="view details" href="./HistoryPage?id=<%# Eval("id") %>"><%# Eval("Status") %></a>
                            <%}
                            else
                            { %>
                                <%# Eval("Status") %>
                            <%} %>
                        </td>
                        <td><%# Eval("Description") %></td>
                        <td><%# Eval("StartDateTime") %></td>
                        <td><%# Eval("EndDateTime") %></td>
                    </tr>
                    <%# Eval("StepDetails") %>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="tmrHistoryView" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Timer ID="tmrHistoryView" runat="server" Interval="15000" Enabled="True"></asp:Timer>
</asp:Content>
