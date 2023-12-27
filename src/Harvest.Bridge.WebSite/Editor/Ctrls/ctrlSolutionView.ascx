<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlSolutionView.ascx.cs" Inherits="Harvest.Bridge.WebSite.Editor.Ctrls.ctrlSolutionView" %>
<asp:Panel ID="pnlSolutionView" runat="server">
    <asp:ListBox ID="lstProjectView" Height="650px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstProjectView_OnIndexChange"></asp:ListBox>
</asp:Panel>
