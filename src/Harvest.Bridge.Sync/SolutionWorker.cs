using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harvest.Bridge.Sync
{
    public class SolutionWorker
    {
        public Guid BridgeLogId { get; set; }
        public SolutionWorker(SolutionModel solutionModel)
        {
            SolutionModel = solutionModel;
        }

        public SolutionModel SolutionModel { get; set; }

        public void ProcessProject(string projectName)
        {
            ProcessSolution(projectName);
        }

        public void ProcessSolution()
        {
            SolutionModel.ImportStartTime = System.DateTime.Now;
            ProcessSolution(null);
        }

        public void ProcessSolution(string projectName)
        {
            DALConfiguration dalConfigurationdal = new DALConfiguration();
            DALBridgeLog dalBridgeLog = new DALBridgeLog();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            BridgeLog.Info("Begin processing solution");
            bool testScheduleModeOnly = dalConfigurationdal.GetConfigValueAsBool("TestScheduleModeOnly");
            int projectRunCnt = 0;
            SolutionModel.SolutionFinishStatus = "Finished";
            try
            {
                SolutionModel.Projects.ForEach(p => p.Reset());

                LoadGlobalVariables(dalBridgeLog);
                PreProcessSolution();

                List<Task> parallelTasks = new List<Task>();

                dalBridgeLog.WriteBridgeRunningLog(BridgeLogId);

                if (testScheduleModeOnly)
                {
                    // Skip actual import work
                    BridgeLog.Warning("Configuration 'TestScheduleModeOnly' to value 'True'");
                    System.Threading.Thread.Sleep(90000);
                }
                else
                {
                    foreach (ProjectModel projectModel in SolutionModel.Projects)
                    {
                        projectModel.FlowControl = ProjectFlowControlEnum.Continue;
                        if (projectModel.Enabled == false)
                        {
                            string skipMsg = $"Skiping project '{projectModel.Name}', Enabled=False";
                            BridgeLog.Info(skipMsg);
                            continue;
                        }
                        if (string.IsNullOrEmpty(projectName) == false &&
                            projectModel.Name.Equals(projectName) == false)
                        {
                            string skipMsg = $"Skiping project '{projectModel.Name}', not equal to project name '{projectName}'";
                            BridgeLog.Info(skipMsg);
                            // Set to run specific project, not a match so skip
                            continue;
                        }
                        projectRunCnt++;

                        if (projectModel.ParallelForceSync)
                        {
                            HoldWhileTasksAreProcessing(parallelTasks);
                        }

                        if (projectModel.AllowParallelProcessing)
                        {
                            parallelTasks.Add(Task.Factory.StartNew(() => RunParallelProcess(projectModel), TaskCreationOptions.LongRunning));
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            ProjectWorker projectWorker = new ProjectWorker(this, projectModel);
                            projectWorker.RunProcess();
                            dalBridgeLog.WriteBridgeLogDetailMsg(BridgeLogId, projectModel.Name, string.Empty, "Step Processing Complete", "Info");
                        }

                        if (projectModel.FlowControl == ProjectFlowControlEnum.StopProcess)
                        {
                            dalBridgeLog.UpdateBridgeLog(BridgeLogId, "Project Step Flow set to stop process, preparing to exit.");
                            break;
                        }
                    }
                    HoldWhileTasksAreProcessing(parallelTasks);
                }

                if (string.IsNullOrEmpty(SolutionModel.SolutionFinishMessage))
                {
                    SolutionModel.SolutionFinishMessage = "Processing of solution complete, total time:" + sw.Elapsed.ToString();
                }

                if (SolutionModel.ImportStartTime != null && SolutionModel.SolutionFinishStatus != "Error")
                {
                    dalConfigurationdal.UpdateConfig("Import.DataFromDateTime", SolutionModel.ImportStartTime.Value.AddMinutes(-30).ToString());
                    BridgeLog.Info("Setting configuration 'Import.PreviousStartTime' to value '" + SolutionModel.ImportStartTime.ToString() + "'");
                }
            }
            catch (Exception ex)
            {
                dalBridgeLog.UpdateBridgeLog(BridgeLogId, "Error: " + ex.Message);
                BridgeLog.Error(ex.ToString());
                SolutionModel.SolutionFinishMessage+= Environment.NewLine + "Processing of solution was canceled due to an error, total time:" + sw.Elapsed.ToString();
                SolutionModel.SolutionFinishStatus = "Error";
            }

            if (string.IsNullOrEmpty(projectName) == false && projectRunCnt == 0)
            {
                throw new ApplicationException($"Specific project name to be ran was not found, '{projectName}'");
            }

            PostProcessSolution();
        }

        private void PostProcessSolution()
        {
            DALConfiguration dalConfigurationdal = new DALConfiguration();
            DALBridgeLog dalBridgeLog = new DALBridgeLog();

            dalConfigurationdal.UpdateConfig("Import.RunForSingleDay", "False");

            dalBridgeLog.WriteTableCounts(BridgeLogId);

            dalBridgeLog.WriteBridgeFinishLog(BridgeLogId, SolutionModel.SolutionFinishMessage, SolutionModel.SolutionFinishStatus);

            BridgeLog.Info(SolutionModel.SolutionFinishMessage);

            SolutionModel.RuntimeParams.Clear();
        }

        private void PreProcessSolution()
        {
            DALBridgeLog dalBridgeLog= new DALBridgeLog();
            dalBridgeLog.UpdatedAbandonedJobs();
            dalBridgeLog.CleanupBridgeLogData();

            DALConfiguration dalConfigurationdal = new DALConfiguration();

            SolutionModel.ImportForSingleDay = dalConfigurationdal.GetConfigValueAsBool("Import.RunForSingleDay");
            if (SolutionModel.ImportForSingleDay)
            {
                string tmpVal = dalConfigurationdal.GetConfigValue("Import.DataFromDateTime");
                DateTime tmpDt = DateTime.MinValue;
                if (DateTime.TryParse(tmpVal, out tmpDt))
                {
                    dalBridgeLog.WriteBridgeLogDetailMsg(BridgeLogId, "Pre-Setup", string.Empty, "Import job currently configured to import for single day only '" + tmpDt.ToShortDateString() + "', this value will be cleared after this run is complete.", "Info");
                    SolutionModel.RuntimeParams.Add("ImportForSingleDate", tmpDt.Date.ToString());
                }
            }


            if (dalConfigurationdal.GetConfigValueAsBool("TruncateStagingData"))
            {
                dalBridgeLog.WriteBridgeLogDetailMsg(BridgeLogId, "Pre-Setup", string.Empty, "Truncating staging tables data before job run", "Info");
                new DAL.DALUtil().TruncateStagingTables();
            }

            if (dalConfigurationdal.GetConfigValueAsBool("PreDeleteDataOnNextRun") &&
                SolutionModel.RuntimeParams["ImportMinutes"] != null)
            {                
                int importMinutes = 0;
                if (int.TryParse(SolutionModel.RuntimeParams["ImportMinutes"], out importMinutes))
                {
                    TimeSpan ts = new TimeSpan(0, importMinutes, 0);
                    dalBridgeLog.WriteBridgeLogDetailMsg(BridgeLogId, "Pre-Setup", string.Empty, "Pre-Deleting data for the previous " + ts.Days + " days.", "Info");
                    if (ts.Days > 0)
                    {
                        new DAL.DALUtil().PreDeleteDataByDateRange(ts.Days);
                    }
                }
                dalConfigurationdal.UpdateConfig("PreDeleteDataOnNextRun", false.ToString());
            }
        }

        private void HoldWhileTasksAreProcessing(List<Task> tasks)
        {
            bool isComplete = false;
            do
            {
                int completeCnt = 0;
                foreach(Task t in tasks)
                {
                    if (t.IsCompleted == false)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        completeCnt++;
                    }
                }
                isComplete = completeCnt == tasks.Count;
            } while (isComplete == false);
        }

        private void RunParallelProcess(ProjectModel projectModel)
        {
            ProjectWorker projectWorker = new ProjectWorker(this, projectModel);
            projectWorker.RunProcess();
        }

        private void LoadGlobalVariables(DALBridgeLog dalBridgeLog)
        {
            BridgeLog.Info($"Processing Global Variables");
            if (SolutionModel.VariableValues != null) { SolutionModel.VariableValues.Clear(); }
            new VariableWorker(SolutionModel).ProcessVariables(SolutionModel.GlobalVariables);

            DALConfiguration dalConfiguration = new DALConfiguration();

            SolutionModel.VariableValues.ForEach(v => v.IsGlobal = true);

            BridgeLogId = Guid.Parse(SolutionModel.GetVariableValue("BridgeLogId").ToString());
            dalBridgeLog.WriteBridgeStartLog(BridgeLogId, "Database Sync Process Starting");

            if (dalConfiguration.GetConfigValue("Import.Resume").Equals("True", StringComparison.OrdinalIgnoreCase) &&
                SolutionModel.RuntimeParams["ImportMinutes"] == null)
            {
                string prevImportTime = dalConfiguration.GetConfigValue("Import.DataFromDateTime");
                if (string.IsNullOrEmpty(prevImportTime) == false)
                {
                    DateTime dtPrevRun = DateTime.Parse(prevImportTime);
                    TimeSpan span = System.DateTime.Now.Subtract(dtPrevRun);
                    
                    SolutionModel.RuntimeParams.Add("ImportMinutes", ((int)span.TotalMinutes).ToString());

                    SolutionModel.VariableValues.Add(new VariableValueModel() { VariableName = "ImportMinutes", ObjectValue = SolutionModel.RuntimeParams["ImportMinutes"]});
                    dalBridgeLog.WriteBridgeLogDetailMsg(BridgeLogId, "Data Sync", string.Empty, $"Import.Result = 'True', data import including all data created within the previous {SolutionModel.RuntimeParams["ImportMinutes"]} minutes.", "Info");
                }
            }

            dalBridgeLog.WriteTableCounts(BridgeLogId);
            WriteGlobalVariablesToLog();
        }

        private void WriteGlobalVariablesToLog()
        {
            DALBridgeLog dalBridgeLog = new DALBridgeLog();
            foreach(VariableModel gv in SolutionModel.GlobalVariables)
            {
                string value = SolutionModel.GetVariableValue(gv.Name).ToString();
                dalBridgeLog.WriteBridgeLogDetailMsg(BridgeLogId, "Global Variables", string.Empty, $"Variable '{gv.Name}' Processing Complete, resulting value '{value}'", "Info");
            }
        }
    }
}
