using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Sync;
using Harvest.Bridge.Util;
using System;
using System.IO;
using System.Text;

namespace Harvest.Bridge.WebSite.Config
{
    public partial class ConfigEditor : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageRequiredAuthentication();
            if (IsPostBack)
            {
                if (Request.Form["ctl00$MainContent$btnDownload"] != null)
                {
                    DownloadSolutionFile();
                }

                if (btnUploadSolutionFile.FileBytes.Length > 0)
                {
                    SavePostedFile();
                }
                SaveConfigChanges();
            }
            LoadConfigValues();
        }

        private void SaveConfigChanges()
        {
            DALConfiguration dalConfig = new DALConfiguration();
            dalConfig.UpdateConfig("RuntimeSync_IsEnabled", chkIsEnabled.Checked.ToString());
            dalConfig.UpdateConfig("TruncateStagingData", chkTruncateStagingTablesBeforeRun.Checked.ToString());
            dalConfig.UpdateConfig("Import.Resume", chkResumeImport.Checked.ToString());
            dalConfig.UpdateConfig("Import.DataFromDateTime", txtImportPreviousStartTime.Text);
            dalConfig.UpdateConfig("RuntimeSync_ProjectName", txtSolutionName.Text);
            dalConfig.UpdateConfig("RuntimeSync_ProjectSource", txtImportSolutionSource.Text);
            dalConfig.UpdateConfig("RuntimeSync_WorkingFolderPath", txtWorkingFolderPath.Text);
            dalConfig.UpdateConfig("RuntimeSync_PauseMinutes", txtPauseTime.Text);

            dalConfig.UpdateConfig("ImportDays", txtMaxImportDays.Text);
            dalConfig.UpdateConfig("MaxDays.Bloodtest", txtMaxDaysBloodTest.Text);
            dalConfig.UpdateConfig("MaxDays.OrdPanel", txtMaxDaysOrderPanel.Text);
            dalConfig.UpdateConfig("MaxDays.TestResults", txtMaxDaysTestResults.Text);
        }

        private void LoadConfigValues()
        {
            chkIsEnabled.Checked = bool.Parse(GetConfigValue("RuntimeSync_IsEnabled", "False"));
            chkResumeImport.Checked = bool.Parse(GetConfigValue("Import.Resume", "True"));
            chkTruncateStagingTablesBeforeRun.Checked = bool.Parse(GetConfigValue("TruncateStagingData", "True"));
            txtImportPreviousStartTime.Text = GetConfigValue("Import.DataFromDateTime", System.DateTime.Now.AddDays(10).ToString());
            txtSolutionName.Text = GetConfigValue("RuntimeSync_ProjectName");
            txtImportSolutionSource.Text = GetConfigValue("RuntimeSync_ProjectSource", "StagingDB");
            txtWorkingFolderPath.Text = GetConfigValue("RuntimeSync_WorkingFolderPath");
            txtPauseTime.Text = GetConfigValue("RuntimeSync_PauseMinutes", "15");

            txtMaxImportDays.Text = GetConfigValue("ImportDays", "10");
            txtMaxDaysBloodTest.Text = GetConfigValue("MaxDays.Bloodtest", "10");
            txtMaxDaysOrderPanel.Text = GetConfigValue("MaxDays.OrdPanel", "10");
            txtMaxDaysTestResults.Text = GetConfigValue("MaxDays.TestResults", "10");
        }

        private void SavePostedFile()
        {
            try
            {
                string fileName = btnUploadSolutionFile.FileName;

                string solutionFileJson;
                using (StreamReader inputStreamReader = new StreamReader(btnUploadSolutionFile.PostedFile.InputStream))
                {
                    solutionFileJson = inputStreamReader.ReadToEnd();
                }
                // Validate solutionFileJson
                SolutionModel solutionModel = JsonCommonHelper.DeserializeObject(solutionFileJson, typeof(SolutionModel)) as SolutionModel;
                if (solutionModel != null &&
                    string.IsNullOrEmpty(txtSolutionName.Text) == false &&    
                    solutionModel.SolutionName.Equals(txtSolutionName.Text, StringComparison.OrdinalIgnoreCase))
                {
                    // validate name in file matches name passed up
                    txtSolutionName.Text = solutionModel.SolutionName;
                    // save to database
                    new JSONStoreWorker().SaveRecord(solutionModel, txtImportSolutionSource.Text);
                    new DALConfiguration().UpdateConfig("RuntimeSync_ProjectName", txtSolutionName.Text);

                    ShowMessageSuccess("Solution file succesfully loaded to database and set as primary active import solution, active solution name '" + txtSolutionName.Text + "'");
                }
                else
                {
                    if (solutionModel == null)
                    {
                        ShowMessageDanger("Failed to upload file, deserialization of uploaded file failed.");
                    }
                    else if (solutionModel.SolutionName.Equals(fileName.Replace(".json", string.Empty), StringComparison.OrdinalIgnoreCase) == false)
                    {
                        ShowMessageDanger("Failed to upload file, specified file name found in json does not match uploaded file name. Uploaded filename:'" + solutionModel.SolutionName + "' filename found in json:'" + fileName + "'");
                    }
                    else if (string.IsNullOrEmpty(txtSolutionName.Text))
                    {
                        ShowMessageDanger("Failed to upload file, solution name was not provided.");
                    }
                }
            }
            catch(Exception ex)
            {
                ShowMessageDanger("Error: " + ex.Message);
            }
        }

        protected void DownloadSolutionFile()
        {
            string jsonDataBase64 = new JSONStoreDAL(txtImportSolutionSource.Text).GetJsonModel(txtSolutionName.Text);
            var base64EncodedBytes = System.Convert.FromBase64String(jsonDataBase64);
            string jsonString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            MemoryStream memStream = new MemoryStream(bytes);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/json";
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + txtSolutionName.Text + ".json;");
            // response.Write(jsonString);
            // response.TransmitFile(Server.MapPath("FileDownload.csv"));
            memStream.WriteTo(response.OutputStream);
            response.Flush();
            response.End();
        }

        protected void btnDownload_Click(object sender, EventArgs e) { }
        private void ShowMessageDanger(string message)
        {
            ShowMessage("danger", message);
        }
        private void ShowMessageSuccess(string message)
        {
            ShowMessage("success", message);
        }
        private void ShowMessageInfo(string message)
        {
            ShowMessage("info", message);
        }
        private void ShowMessage(string msgType, string message)
        {
            ltlInfoMessage.Text = $"<div class='alert alert-{msgType}' role='alert'>{message}</div>";
        }
    }
}