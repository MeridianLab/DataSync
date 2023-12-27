<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Harvest.Bridge.WebSite.Editor.MainView" %>

<%@ Register Src="~/Editor/Ctrls/ProjectStepsList.ascx" TagPrefix="uc1" TagName="ProjectStepsList" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
th, td {
  padding: 3px;
}
    </style>
    <table border="0" style="width:100%;height:100%">
        <tr>
            <td colspan="2">
                <asp:Label runat="server" Text="Solution:"></asp:Label>:<asp:DropDownList ID="drpSolution" runat="server" OnSelectedIndexChanged="drpProjectSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>               
            </td>
        </tr>
        <tr>
            <td style="width:20%" valign="top">
                <asp:Panel ID="pnlSolutionView" runat="server" BorderStyle="Solid" BorderWidth="2"></asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="pnlMainView" runat="server" Width="100%">
                    <table>
                        <tr>
                            <td>
                                <uc1:ProjectStepsList runat="server" id="ProjectStepsList" />
                            </td>
                        </tr>
                        <tr>
                                <asp:Panel ID="pnlStepView" runat="server" ViewStateMode="Disabled"></asp:Panel>
                        </tr>
                    </table>

                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>