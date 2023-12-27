<%@ Page Title="Configuration Editor" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ConfigEditor.aspx.cs" Inherits="Harvest.Bridge.WebSite.Config.ConfigEditor" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
table, th, td {
  border: 1px solid black;
  border-collapse:collapse;
  padding:10px;
}
</style>
    <h2><%: Title %></h2>
    <asp:Literal ID="ltlInfoMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
    <table style="width:95%">
        <tr>
            <td style="width:33%">Data Sync Process Enabled?</td>
            <td>
                <asp:CheckBox ID="chkIsEnabled" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Import Solution Name:</td>
            <td>
                <asp:TextBox ID="txtSolutionName" runat="server"></asp:TextBox>
                <br />
                <h4>
                    Upload Solution File&nbsp;<asp:FileUpload ID="btnUploadSolutionFile" runat="server" />
                    <asp:Button ID="btnUploadAction" CssClass="btn btn-primary" runat="server" Text="Upload" />
                    </h4>
                <h4>Download Active Solution File
                <asp:Button ID="btnDownload" CssClass="btn btn-primary" runat="server" Text="Download" OnClick="btnDownload_Click" /></h4>
            </td>
        </tr>
        <tr>
            <td>Import Solution SourceDB:</td>
            <td>
                <asp:TextBox ID="txtImportSolutionSource" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Data Sync Resume Enabled?</td>
            <td>
                <asp:CheckBox ID="chkResumeImport" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Truncate staging tables before beginning job</td>
            <td>
                <asp:CheckBox ID="chkTruncateStagingTablesBeforeRun" runat="server" />
        </tr>
        <tr>
            <td>Current Import Resume DateTime</td>
            <td>
                <asp:TextBox ID="txtImportPreviousStartTime" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Working Folder(Path to 'Harvest.Bridge.WindowsService.exe')</td>
            <td>
                <asp:TextBox ID="txtWorkingFolderPath" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Amount of time to pause if job is still running next scheduled run</td>
            <td>
                <asp:TextBox ID="txtPauseTime" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Max Import Days</td>
            <td>
                <asp:TextBox ID="txtMaxImportDays" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Max Days Bloodtest</td>
            <td>
                <asp:TextBox ID="txtMaxDaysBloodTest" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Max Days OrdPanel</td>
            <td>
                <asp:TextBox ID="txtMaxDaysOrderPanel" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Max Days Test Results</td>
            <td>
                <asp:TextBox ID="txtMaxDaysTestResults" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" Text="Apply" /></td>
        </tr>
    </table>
</asp:Content>
