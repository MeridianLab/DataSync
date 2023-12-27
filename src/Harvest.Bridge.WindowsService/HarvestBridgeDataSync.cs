using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.History;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Sync;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace Harvest.Bridge.WindowsService
{
    public partial class HarvestBridgeDataSync : ServiceBase
    {
        private string[] _initArguments = null;
        private int _minutesBetweenRuns = -1;
        private int _minutesPauseTime = -1;
        private int _lastMinuteCheckForFullRun;
        private Process _dataSyncProcess = null;
        private string _projectName = null;
        private string _projectSource = null;
        private string _workingFolderPath = null;
        System.Diagnostics.EventLog _serviceEventLog = null;
        System.Threading.Timer _intervalTimer = null;

        public HarvestBridgeDataSync(string[] args)
        {
            _initArguments = args;
            InitializeComponent();
            SetupEventLog(args);
        }

        public void DebugOnStart(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            SetupEventLog(args);
            _serviceEventLog.WriteEntry("Application Starting " + System.DateTime.Now.ToString(), EventLogEntryType.Information);
            LoadConfigValues();

            CleanupAbandonedJobs();

            _intervalTimer = new System.Threading.Timer(DoWorkTimer_Tick, null, 10000, Timeout.Infinite);
        }

        protected override void OnStop()
        {
            _serviceEventLog.WriteEntry("Application Stopping " + System.DateTime.Now.ToString(), EventLogEntryType.Information);
            if(_dataSyncProcess != null && _dataSyncProcess.HasExited == false)
            {
                _dataSyncProcess.Kill();
            }
        }

        private void LoadConfigValues()
        {
            if (_minutesBetweenRuns < 0)
            {
                _minutesBetweenRuns = GetArgValueAsInt("RuntimeSync_FrequencyInMinutes", 15);
            }
            else
            {
                int tmpValue = GetArgValueAsInt("RuntimeSync_FrequencyInMinutes", 15);
                if(tmpValue != _minutesBetweenRuns)
                {
                    _minutesBetweenRuns = tmpValue;
                    SaveNextRunTime(DateTime.Now.AddMinutes(_minutesBetweenRuns), "Updated value from configuration table.");
                }
            }
            _minutesPauseTime = GetArgValueAsInt("RuntimeSync_PauseMinutes", 5);
            
            _projectSource = GetArgValueRequired("RuntimeSync_ProjectSource");
            _projectName = GetArgValueRequired("RuntimeSync_ProjectName");

            _workingFolderPath = GetArgValueRequired("RuntimeSync_WorkingFolderPath");
        }

        private void DoWorkTimer_Tick(object state)
        {
            LoadConfigValues();

            if(CheckForIncrementalRunTime() == false)
            {
                CheckForCatchupSync();
            }

            SaveConfigurationValue("Service Heartbeat", DateTime.Now.ToString());
            _intervalTimer.Change(10000, Timeout.Infinite);
        }

        private void CheckForCatchupSync()
        {
            if (_lastMinuteCheckForFullRun != DateTime.Now.Minute &&
                (_dataSyncProcess == null || _dataSyncProcess.HasExited == true))
            {
                _lastMinuteCheckForFullRun = DateTime.Now.Minute;

                DataSyncFullSchedule fullDataSync = new DataSyncFullSchedule();
                ScheduleFullRunModel schedule = fullDataSync.GetSchedule();
                if (schedule != null && schedule.IsEnabled)
                {
                    if (schedule.IsEnabled && DateTime.Now > schedule.NextStartDate &&
DateTime.Now.Hour >= schedule.StartTime) // && schedule.IsRunning == false)
                    {
                        if (schedule.LastCompleteDate < DateTime.Now)
                        {
                            // Find index of current running date so we can go to next
                            int index = schedule.ScheduleDays.IndexOf(schedule.CurrentRunningForDate.Date) + 1;
                            if (index < schedule.ScheduleDays.Count)
                            {
                                schedule.CurrentRunningForDate = schedule.ScheduleDays[index];
                                System.Diagnostics.Debug.WriteLine($"Time To start process for day {schedule.CurrentRunningForDate} Index:{index}");
                                new DataSyncFullSchedule().SaveSchedule(schedule);
                                System.Diagnostics.Debug.WriteLine("Run catchup for date:" + schedule.CurrentRunningForDate);
                                DALConfiguration configuration = new DALConfiguration();
                                configuration.UpdateConfig("Import.RunForSingleDay", "True");
                                configuration.UpdateConfig("Import.DataFromDateTime", schedule.CurrentRunningForDate.ToString());
                                configuration.UpdateConfig("RuntimeSync_NextScheduledRuntime", System.DateTime.Now.ToString());

                            }
                            else
                            {
                                // Done
                                schedule = new DataSyncFullSchedule().SaveNextFullRunSchedule(DateTime.Now.AddDays(1));
                            }

                        }
                    }

                }
            }
        }

        private bool CheckForIncrementalRunTime()
        {
            bool isRunning = false;
            long nextRunTimeInTicks = GetNextScheduledRunTime();

            if (nextRunTimeInTicks < DateTime.Now.Ticks)
            {
                if (IsImportEnabled() == false)
                {
                    string msg = $"Import process disabled by config key 'RuntimeSync_IsEnabled', pausing runtime for {_minutesPauseTime} minutes.";
                    SaveNextRunTime(DateTime.Now.AddMinutes(_minutesPauseTime), msg);
                }
                else if (_dataSyncProcess == null)
                {
                    // Run data sync
                    isRunning = true;
                    StartNewDataSyncProcess();
                }
                else if (_dataSyncProcess.HasExited == false)
                {
                    string msg = $"Application DoWork Time, however previous run has not completed, will pause runtime for {_minutesPauseTime} minutes.";
                    isRunning = true;
                    SaveNextRunTime(DateTime.Now.AddMinutes(_minutesPauseTime), msg);
                }
                else
                {
                    // Run data sync
                    StartNewDataSyncProcess();
                    isRunning = true;
                }
            }
            return isRunning;
        }

        private void StartNewDataSyncProcess()
        {
            try
            {
                LoadConfigValues();
                DataSyncSchedule dataSyncSchedule = new DataSyncSchedule();
                ScheduleModel nextSchedule = dataSyncSchedule.CalculateNextRunTime();
                dataSyncSchedule.SaveNextScheduleInfo(nextSchedule);
                _minutesBetweenRuns = nextSchedule.FrequencyInMinutes;

                _dataSyncProcess = new Process();
                // ProjectFile:"C:\Users\joelb\source\repos\HarvestBridge\DataSyncMapping\BridgeSyncProject.json"
                string argument = $"ProjectName:\"{_projectName}\" ProjectSource:{_projectSource}";
                _dataSyncProcess.StartInfo.Arguments = argument;
                _dataSyncProcess.Exited += _dataSyncProcess_Exited;
                _dataSyncProcess.StartInfo.FileName = _workingFolderPath + "\\HarvestBridge.exe";
                _serviceEventLog.WriteEntry($"Starting Sync Process, {_dataSyncProcess.StartInfo.FileName} {_dataSyncProcess.StartInfo.Arguments}", EventLogEntryType.Information);
                _dataSyncProcess.Start();
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }

        private void SaveNextRunTime(DateTime dt, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                msg = string.Empty;
            }
            else
            {
                msg += Environment.NewLine;
            }
            SaveConfigurationValue("RuntimeSync_NextScheduledRuntime", dt.ToString());
            _serviceEventLog.WriteEntry($"{msg}Next scheduled data sync runtime in {_minutesBetweenRuns} minutes, at '{dt.ToString()}'", EventLogEntryType.Information);
        }

        private long GetNextScheduledRunTime()
        {
            string val = GetArgValue("RuntimeSync_NextScheduledRuntime", string.Empty);
            if (string.IsNullOrEmpty(val))
            {
                val = DateTime.Now.ToString();
            }
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(val, out dt) == false)
            {
                dt = DateTime.Now;
            }
            return dt.Ticks;
        }

        private bool IsImportEnabled()
        {
            string value = GetArgValue("RuntimeSync_IsEnabled", null);
            if(value == null)
            {
                SaveConfigurationValue("RuntimeSync_IsEnabled", "true");
                value = "true";
            }
            return bool.Parse(value);
        }

        private void _dataSyncProcess_Exited(object sender, EventArgs e)
        {
            _dataSyncProcess = null;
            _serviceEventLog.WriteEntry("Sync Process Complete.", EventLogEntryType.Information);
        }

        private void CleanupAbandonedJobs()
        {
            DALLogHistory logHistory = new DALLogHistory();
            List<HistoryModel> historyModels = logHistory.GetHistory(1);
            if(historyModels.Count > 0 && historyModels[0].Status.Trim() == "Running")
            {
                List<HistoryStepModel> stepHistoryList = logHistory.GetProcessStepHistory(historyModels[0].Id);
                if(stepHistoryList.Count > 0)
                {
                    if (DateTime.Now.AddMinutes(-20) > stepHistoryList[0].CreateDate)
                    {
                        // Cancel it out
                        DALBridgeLog dalBridgeLog = new DALBridgeLog();
                        dalBridgeLog.WriteBridgeFinishLog(historyModels[0].Id, "Canceled by Windows Service, process appears unresponsive.", "Canceled");
                    }
                }
            }
        }

        #region Helpers
        private int GetArgValueAsInt(string argName, int defaultValue)
        {
            string retVal = GetArgValue(argName, defaultValue.ToString());
            int retInt = -1;
            int.TryParse(retVal, out retInt);
            return retInt;
        }

        private string GetArgValueRequired(string argName)
        {
            string retVal = GetArgValue(argName, null);
            if (string.IsNullOrEmpty(retVal))
            {
                string msg = $"Required application parameter not found, [{argName}], unable to continue.";
                _serviceEventLog.WriteEntry(msg, EventLogEntryType.Error);
                throw new ArgumentException(msg);
            }
            return retVal;
        }

        private string GetArgValue(string argName, string defaultValue)
        {
            string retVal = new DALConfiguration().GetConfigValue(argName);
            if (string.IsNullOrEmpty(retVal))
            {
                foreach (string arg in _initArguments)
                {
                    if (arg.StartsWith(argName + ":", StringComparison.OrdinalIgnoreCase))
                    {
                        retVal = arg.Substring(argName.Length + 1);
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(retVal))
            {
                retVal = ConfigurationManager.AppSettings[argName];
            }
            if (string.IsNullOrEmpty(retVal))
            {
                retVal = defaultValue;
            }
            return retVal;
        }

        private void SaveConfigurationValue(string keyName, string value)
        {
            new DALConfiguration().UpdateConfig(keyName, value);
        }

        private void SetupEventLog(string[] args)
        {
            string eventSourceName = "HarvestBridgeSync";
            string logName = "HarvestBridgeSync";

            _serviceEventLog = new EventLog();

            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }

            _serviceEventLog.Source = eventSourceName;
            _serviceEventLog.Log = logName;
        }

        #endregion
    }
}
