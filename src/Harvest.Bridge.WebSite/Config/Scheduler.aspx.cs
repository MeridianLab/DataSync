using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using System;
using System.Text;
using System.Web.UI;

namespace Harvest.Bridge.WebSite.Config
{
    public partial class Scheduler : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageRequiredAuthentication();
            if (!IsPostBack) { LoadSchedule(); }
            lblNextRunTime.Text = GetConfigValue("RuntimeSync_NextScheduledRuntime", "-");
            lblRunFrequency.Text = GetConfigValue("RuntimeSync_FrequencyInMinutes", "-");
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            SaveIntervalSchedul();
            SaveCatchupSchedule();
            LoadSchedule();
            ShowMessageInfo(ltlMessage, "Scheduled Saved, " + DateTime.Now.ToString());
        }

        private void SaveIntervalSchedul()
        {
            DataSyncSchedule syncSchedule = new DataSyncSchedule();
            syncSchedule.LoadSchedule();
            // <tr><th>Day</th><th>Use Default</th><th>Enabled</th><th>Start Time</th><th>Stop Time</th><th>Run Frequency</th></tr>

            foreach (ScheduleModel s in syncSchedule.Schedules)
            {
                if (s.DayOfWeek != -1)
                {
                    // Use Default
                    string va = Request.Form["chkUseDefault_" + s.DayOfWeek.ToString()];
                    if (string.IsNullOrEmpty(va) == false && va.Equals("on", StringComparison.OrdinalIgnoreCase))
                    {
                        s.UseDefault = true;
                    }
                    else { s.UseDefault = false; }
                    // Enabled
                    va = Request.Form["chkIsEnabled_" + s.DayOfWeek.ToString()];
                    if (string.IsNullOrEmpty(va) == false && va.Equals("on", StringComparison.OrdinalIgnoreCase))
                    {
                        s.DayEnabled = true;
                    }
                    else { s.DayEnabled = false; }
                }
                // Start Time
                string st = Request.Form["txtStartTime_" + s.DayOfWeek.ToString()];
                if (string.IsNullOrEmpty(st) == false)
                {
                    int x = -1;
                    if (int.TryParse(st, out x))
                    {
                        if (x >= 0 && x <= 24)
                        {
                            s.StartTime = x;
                        }
                    }
                }
                // Stop Time
                string so = Request.Form["txtStopTime_" + s.DayOfWeek.ToString()];
                if (string.IsNullOrEmpty(so) == false)
                {
                    int x = -1;
                    if (int.TryParse(so, out x))
                    {
                        if (x >= 0 && x <= 24)
                        {
                            s.EndTime = x;
                        }
                    }
                }

                // Run Frequency
                string rf = Request.Form["txtFrequency_" + s.DayOfWeek.ToString()];
                if (string.IsNullOrEmpty(rf) == false)
                {
                    int x = -1;
                    if (int.TryParse(rf, out x))
                    {
                        if (x >= 5)
                        {
                            s.FrequencyInMinutes = x;
                        }
                    }
                }
            }

            syncSchedule.SaveSchedule();
            ScheduleModel nextSchedule = syncSchedule.CalculateNextRunTime();
            syncSchedule.SaveNextScheduleInfo(nextSchedule);
            UpdateConfigValue("RuntimeSync_NextScheduledRuntime", nextSchedule.NextScheduleRunTime.ToString());
            lblNextRunTime.Text = nextSchedule.NextScheduleRunTime.ToString();
            lblRunFrequency.Text = nextSchedule.FrequencyInMinutes.ToString();
        }

        private void SaveCatchupSchedule()
        {
            DataSyncFullSchedule fullDataSync = new DataSyncFullSchedule();
            ScheduleFullRunModel scheduleFullRun = fullDataSync.GetSchedule();

            int n = 0;
            int.TryParse(txtCatchupStartHour.Text, out n);
            scheduleFullRun.StartTime = n; // 8 pm

            int.TryParse(txtCatchupNumOfDays.Text, out n);
            scheduleFullRun.TotalDays = n;
            
            scheduleFullRun.IsEnabled = chkCatchupEnabled.Checked;

            fullDataSync.SaveSchedule(scheduleFullRun);
            if(scheduleFullRun.IsEnabled)
            {
                fullDataSync.SaveNextFullRunSchedule(DateTime.Now);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            DataSyncSchedule syncSchedule = new DataSyncSchedule();
            syncSchedule.LoadSchedule();
            StringBuilder sb = new StringBuilder();

            foreach (ScheduleModel s in syncSchedule.Schedules)
            {
                sb.Append($"<tr><th>{s.Day}</th><td>{GenerateUseDefault(s)}</td><td>{GenerateIsEnabled(s)}</td><td>{GenerateStartTime(s)}</td><td>{GenerateStopTime(s)}</td><td>{GenerateRunFrequency(s)}</td></tr>");
            }
            plhTableData.Controls.Add(new LiteralControl(sb.ToString()));


            DataSyncFullSchedule fullDataSync = new DataSyncFullSchedule();
            ScheduleFullRunModel scheduleFullRun = fullDataSync.GetSchedule();
            chkCatchupEnabled.Checked = scheduleFullRun.IsEnabled;
            txtCatchupStartHour.Text = scheduleFullRun.StartTime.ToString();
            txtCatchupNumOfDays.Text = scheduleFullRun.TotalDays.ToString();
        }

        private string GenerateRunFrequency(ScheduleModel schedule)
        {
            return $"<input type='text' id='txtFrequency_{schedule.DayOfWeek}' name='txtFrequency_{schedule.DayOfWeek}' value='{schedule.FrequencyInMinutes}'/>";
        }

        private string GenerateStartTime(ScheduleModel schedule)
        {
            return $"<input type='text' id='txtStartTime_{schedule.DayOfWeek}' name='txtStartTime_{schedule.DayOfWeek}' value='{schedule.StartTime}'/>";
        }

        private string GenerateStopTime(ScheduleModel schedule)
        {
            return $"<input type='text' id='txtStopTime_{schedule.DayOfWeek}' name='txtStopTime_{schedule.DayOfWeek}' value='{schedule.EndTime}'/>";
        }

        private string GenerateUseDefault(ScheduleModel schedule)
        {
            string strChecked = string.Empty;
            string strDisabled = string.Empty;
            if(schedule.DayOfWeek == -1)
            {
                strDisabled = "disabled";
            }
            else if (schedule.UseDefault)
            {
                strChecked = "checked=true";
            }
            return $"<input type='checkbox' id='chkUseDefault_{schedule.DayOfWeek}' name='chkUseDefault_{schedule.DayOfWeek}' {strDisabled}{strChecked}/>";
        }

        private string GenerateIsEnabled(ScheduleModel schedule)
        {
            string strChecked = string.Empty;
            string strDisabled = string.Empty;
            if (schedule.DayOfWeek == -1)
            {
                strDisabled = "disabled";
            }
            else if (schedule.DayEnabled)
            {
                strChecked = "checked=true";
            }
            return $"<input type='checkbox' id='chkIsEnabled_{schedule.DayOfWeek}' name='chkIsEnabled_{schedule.DayOfWeek}' {strDisabled}{strChecked}/>";
        }
    }
}