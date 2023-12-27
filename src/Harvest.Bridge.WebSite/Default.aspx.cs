using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.History;
using Harvest.Bridge.Common.Models.Pathway;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Sync;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;

namespace Harvest.Bridge.WebSite
{
    public partial class _Default : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected override void OnPreRender(EventArgs e)
        {
            ShowHistory();
            AdjustAutoTimer();
            LoadBridgeStatusDetails();
            base.OnPreRender(e);
        }

        private bool IsRunningSync
        {
            get
            {
                bool retVal = false;
                if(IsPostBack == false)
                {
                    drpJobRunType.Items.Clear();
                    drpJobRunType.Items.Add("Days");
                    drpJobRunType.Items.Add("Date");
                }
                if (ViewState["RunningSync"] != null && (bool)ViewState["RunningSync"] == true)
                {
                    retVal = true;
                }
                return retVal;
            }
        }
        private void StartRunningSyncViewState()
        {
            ViewState["RunningSync"] = true;
            ViewState["SyncStartTime"] = System.DateTime.Now.AddMinutes(1);
        }

        private void CancelRunningSyncViewState()
        {
            if(ViewState["SyncStartTime"] != null)
            {
                DateTime dtStartTime = (DateTime)ViewState["SyncStartTime"];
                if(dtStartTime > DateTime.Now)
                {
                    return;
                }
            }
            ViewState.Remove("SyncStartTime");
            ViewState.Remove("RunningSync");
        }

        private void AdjustAutoTimer()
        {
            int cnt = 0;
            if (ViewState["AutoRefresh"] != null)
            {
                cnt = (int)ViewState["AutoRefresh"];
            }
            cnt++;
            ViewState["AutoRefresh"] = cnt;

            if (IsRunningSync)
            {
                tmrHistoryView.Interval = 2500;
                chkFullHistory.Checked = false;
            }
            else if(cnt < 15)
            {
                tmrHistoryView.Interval = 15000;
            }
            else if(cnt < 45)
            {                
                tmrHistoryView.Interval = 90000;
            }
            else 
            {
                tmrHistoryView.Interval = 150000;
            }
        }

        protected void btnRunNow_OnClick(object sender, EventArgs e)
        {
            if (pnlAdvancedRunNow.Visible)
            {
                DALConfiguration configuration = new DALConfiguration();
                DateTime dtForNextRun = DateTime.Now;

                if (drpJobRunType.SelectedItem.Text == "Date")
                {
                    if (DateTime.TryParse(txtRunForDays.Text, out dtForNextRun))
                    {
                        configuration.UpdateConfig("Import.RunForSingleDay", "True");
                    }
                    else
                    {
                        ShowMessageDanger(ltlInfoMessage, "Specified time frame type 'Date', however the valued entered '" + txtRunForDays.Text + "' is not a valid date, unable to continue");
                        return;
                    }
                }
                else
                {
                    int runDays = 0;
                    if (int.TryParse(txtRunForDays.Text, out runDays))
                    {
                        if (runDays > 0)
                        {
                            dtForNextRun = DateTime.Now;
                            dtForNextRun = dtForNextRun.AddDays(-runDays);
                        }
                        else
                        {
                            ShowMessageDanger(ltlInfoMessage, "Specified time frame type 'Days', however the valued entered '" + txtRunForDays.Text + "' is not a valid number, unable to continue");
                            return;
                        }
                    }
                }
                configuration.UpdateConfig("Import.DataFromDateTime", dtForNextRun.ToString());
            }

            StartRunningSyncViewState();
            AdjustAutoTimer();
            DALConfiguration dalConfig = new DALConfiguration();
            UpdateConfigValue("RuntimeSync_NextScheduledRuntime", System.DateTime.Now.ToString());
        }
        protected void btnEnabled_OnClick(object sender, EventArgs e)
        {
            ViewState["AutoRefresh"] = 1;
            UpdateConfigValue("RuntimeSync_IsEnabled", (btnEnabled.Text == "Enable").ToString());
        }

        private void LoadBridgeStatusDetails()
        {
            lblNextRunTime.Text = GetConfigValue("RuntimeSync_NextScheduledRuntime", "-");
            lblPreviousRuntime.Text = GetConfigValue("Import.DataFromDateTime", "-");
            lblCurrentEnabled.Text = GetConfigValue("RuntimeSync_IsEnabled", "-");
            lblRunFrequency.Text = GetConfigValue("RuntimeSync_FrequencyInMinutes", "-");
            lblServiceHeartbeat.Text = GetConfigValue("Service Heartbeat", "-");

            DataSyncSchedule dataSyncSchedule = new DataSyncSchedule();
            ScheduleModel nextSchedule = dataSyncSchedule.CalculateNextRunTime();
            lblNextScheduleDay.Text = $"Schedule Name: {nextSchedule.DefaultForActualDayOfWeekStr}";
            lblIsDefaultSchedule.Text = $"Is Default Schedule: {nextSchedule.IsDefault.ToString()}";

            btnEnabled.Text = lblCurrentEnabled.Text.Equals("True", StringComparison.OrdinalIgnoreCase) ? "Disable" : "Enable";

            if (IsRunningSync || lblCurrentEnabled.Text.Equals("False", StringComparison.OrdinalIgnoreCase))
            {
                btnRunNow.Enabled = false;
            }
            else
            {
                btnRunNow.Enabled = true;
            }
        }

        private void ShowHistory()
        {
            int historyCnt = 1;
            if (IsUserAuthenticated)
            {
                btnRunNow.Visible = true;
                btnEnabled.Visible = true;
                lnbAdvancedRunNow.Visible = true;
                chkFullHistory.Visible = true;
                historyCnt = 15;
                if (chkFullHistory.Checked)
                {
                    historyCnt = 250;
                }
            }
            tmrHistoryView.Enabled = true;
            List<HistoryModel> historyModels = new DALLogHistory().GetHistory(historyCnt);

            if(historyModels.Count > 0)
            {
                historyModels[0].StepDetails = PopulateStepDetails(historyModels[0]);
            }
            //foreach (HistoryModel historyModel in historyModels)
            //{
            //    historyModel.StepDetails = PopulateStepDetails(historyModel);
            //}
            rptHistory.DataSource = historyModels;
            rptHistory.DataBind();
            if(IsUserAuthenticated && historyModels.Count > 0)
            {
                switch (historyModels[0].Status.Trim())
                {
                    case "Starting":
                    case "Running":
                        StartRunningSyncViewState();
                        AdjustAutoTimer();
                        break;
                    case "Finished":
                        CancelRunningSyncViewState();
                        break;
                }
            }
        }

        public string PopulateStepDetails(HistoryModel historyModel)
        {
            string ret = string.Empty;
            switch (historyModel.Status.Trim())
            {
                case "Finished":
                    break;
                default:
                    ret = "<tr><td>&nbsp</td><td colspan='3'>" + BuildDetailTable(historyModel.Id) + "</td></tr>";
                    historyModel.EndDateTime = null;
                    break;
            }
            return ret;
        }

        private string BuildDetailTable(Guid id)
        {
            StringBuilder sbRet = new StringBuilder();
            if (IsUserAuthenticated)
            {
                List<HistoryStepModel> stepList = new DALLogHistory().GetProcessStepHistory(id);

                int maxCnt = 0;
                sbRet.AppendLine("<table border=\"1\" class=\"table\"><tr><th>Project</th><th>Step</th><th>Message</th></tr>");
                foreach (HistoryStepModel step in stepList)
                {
                    if (maxCnt > 15)
                    {
                        break;
                    }
                    sbRet.AppendLine($"<tr><td>{step.ProjectName}</td><td>{step.StepName}</td><td>{step.Message}</td></tr>");
                    maxCnt++;
                }
                sbRet.AppendLine("</table>");
            }
            return sbRet.ToString();
        }

        protected void lnbAdvancedRunNow_Click(object sender, EventArgs e)
        {
            pnlAdvancedRunNow.Visible = true;
        }
    }
}