using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Logger;
using System.Diagnostics;

namespace Harvest.Bridge.Sync
{
    internal class ProjectWorker
    {
        private Util.SQLTokenReplacer _sqlTokenReplacer;
        public ProjectWorker(SolutionWorker solutionWorker, ProjectModel projectModel)
        {
            SolutionWorker = solutionWorker;
            ProjectModel = projectModel;
        }

        public ProjectModel ProjectModel { get; private set; }
        public SolutionWorker SolutionWorker { get; set; }

        internal void RunProcess()
        {
            if (ProjectModel.Enabled == false)
            {
                WriteLogMessage($"Will not process step '{ProjectModel.Name}' because the step is disabled.", "Warning");
                return;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            WriteLogMessage($"Starting Step '{ProjectModel.Name}', processing", "Info");

            InitProcess();

            if (ProjectModel.ProjectSteps.Count > 0)
            {
                do
                {
                    PreStepRunCleanup(ProjectModel.CurrentProcess);
                    new ProjectStepWorker(ProjectModel, SolutionWorker.BridgeLogId).ProcessStep(ProjectModel.CurrentProcess);

                    if(ProjectModel.FlowControl == ProjectFlowControlEnum.StopCurrentProject || ProjectModel.FlowControl == ProjectFlowControlEnum.StopProcess)
                    {
                        break;
                    }
                    PostStepClenaup(ProjectModel.CurrentProcess);
                } while (ProjectModel.NextStep() != null);
            }

            PostRunCleanup();

            WriteLogMessage($"Finishing process '{ProjectModel.Name}', Total Time:{sw.Elapsed.ToString()}", "Info");
        }

        private void InitProcess()
        {
            if (ProjectModel.ProcessVariables != null)
            {
                new VariableWorker(ProjectModel.ParentSolution).ProcessVariables(ProjectModel.ProcessVariables);
            }

            if (SolutionWorker.SolutionModel.RuntimeParams["ImportMinutes"] == null)
            {
                int importDays = -1;
                if (SolutionWorker.SolutionModel.RuntimeParams["ImportDays"] != null)
                {
                    importDays = int.Parse(SolutionWorker.SolutionModel.RuntimeParams["ImportDays"].ToString());
                }
                else
                {
                    importDays = int.Parse(SolutionWorker.SolutionModel.GetVariableValue("ImportDays").ToString());
                }

                DALConfiguration dalConfig = new DALConfiguration();
                string configValue = dalConfig.GetConfigValue("MaxDays." + ProjectModel.Name);
                if (string.IsNullOrEmpty(configValue) == false)
                {
                    int configMaxDays = int.Parse(configValue);
                    if (importDays > configMaxDays)
                    {
                        ProjectModel.ImportDays = importDays;
                        ProjectModel.MaxProcessDayCnt = configMaxDays;
                        ProjectModel.MaxStartDay = importDays;
                        ProjectModel.MaxEndDay = importDays - configMaxDays;
                    }
                }
            }
        }

        private void PostRunCleanup()
        {
            ProjectModel.ParentSolution.VariableValues.RemoveAll(v => v.IsGlobal == false);
            foreach (ProjectStepModel projectStep in ProjectModel.ProjectSteps)
            {
                if (projectStep.DataMap != null)
                {
                    projectStep.DataMap.SourceKeyOrdinals = null;
                    projectStep.DataMap.SyncActions = null;
                }
            }
        }

        private void PreStepRunCleanup(ProjectStepModel projectStep)
        {
            projectStep.LogDetails = new System.Collections.Generic.List<LogDetail>();
            ProcessSQLTokenReplacement(projectStep);        
        }
        private void ProcessSQLTokenReplacement(ProjectStepModel projectStep)
        {
            if (_sqlTokenReplacer == null)
            {
                _sqlTokenReplacer = new Util.SQLTokenReplacer();
            }
            if(string.IsNullOrEmpty(projectStep.SQLText) == false)
            {
                if (_sqlTokenReplacer.TokenReplacementList.Count > 0)
                {
                    projectStep.SQLText_Original = projectStep.SQLText;
                    foreach (var tokenReplacement in _sqlTokenReplacer.TokenReplacementList)
                    {
                        projectStep.SQLText = projectStep.SQLText.Replace(tokenReplacement.OldValue, tokenReplacement.NewValue);
                    }
                }
            }
        }

        private void PostStepClenaup(ProjectStepModel projectStep)
        {
            if(projectStep.SQLText_Original != null)
            {
                projectStep.SQLText = projectStep.SQLText_Original;
                projectStep.SQLText_Original = null;
            }
        }

        public void WriteLogMessage(string msg, string logLevel)
        {
            BridgeLog.Info(msg);
            DALBridgeLog dalBridgeLog = new DALBridgeLog();
            dalBridgeLog.WriteBridgeLogDetailMsg(SolutionWorker.BridgeLogId, ProjectModel.Name, string.Empty, msg, logLevel);
        }
    }
}
