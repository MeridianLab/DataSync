<%@ Page Language="C#" AutoEventWireup="true" Title="Database Connection View" MasterPageFile="~/Site.Master" CodeBehind="DbConnectionView.aspx.cs" Inherits="Harvest.Bridge.WebSite.Config.DbConnectionView" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
table, th, td {
  border: 1px solid black;
  border-collapse:collapse;
  padding:10px;
}
</style>
    <h2><%: Title %></h2>
    <table style="width:95%">
        <tr>
            <th>Connection Name</th>
            <th>Database Sever</th>
            <th>Datbase Name</th>
            <th>Username</th>
        </tr>
        <asp:Literal ID="ltrlDBViewTable" runat="server"></asp:Literal>
    </table>
</asp:Content>