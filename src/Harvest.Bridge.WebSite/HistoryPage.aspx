<%@ Page Language="C#" Title="History" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HistoryPage.aspx.cs" Inherits="Harvest.Bridge.WebSite.HistoryPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Select History to be viewed <asp:DropDownList ID="drpRunHistory" runat="server" OnSelectedIndexChanged="drpRunHistory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></h3>

    <asp:GridView ID="grdHistoryImportCounts" runat="server" Width="95%"></asp:GridView>
    <br/>
    <asp:Repeater ID="rptRunHistory" runat="server">
        <HeaderTemplate>
            <table border="1" class="table">
                <tr>
                    <th>Datetime</th>
                    <th>Project Name</th>
                    <th>Step Name</th>
                    <th>Message</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("CreateDate") %></td>
                <td><%# Eval("ProjectName") %></td>
                <td><%# Eval("StepName") %></td>
                <td><%# Eval("Message") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>