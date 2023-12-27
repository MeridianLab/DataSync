using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Logger;
using System;
using System.Data;
using System.Diagnostics;

namespace Harvest.Bridge.Sync
{
    internal class ProjectStepWorker : WorkerBase
    {
        private ProjectModel _projectModel;
        public Guid BridgeLogId { get;}
        public ProjectStepWorker(ProjectModel projectModel, Guid bridgeLogId)   
        {
            _projectModel = projectModel;
            BridgeLogId = bridgeLogId;
        }

        public void ProcessStep(ProjectStepModel projectStep)
        {
            if (_projectModel.ParentSolution.CancelRun)
            {
                throw new ApplicationException("Cancelling Process");
            }
            else
            {
                if(bool.Parse(new DALConfiguration().GetConfigValue("RuntimeSync_IsEnabled")) == false)
                {
                    _projectModel.ParentSolution.CancelRun= true;
                    throw new ApplicationException("Cancelling Process, IsEnabled=False");
                }
            }
            if(projectStep.Enabled == false)
            {
                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Flow, LogLevelEnum.Warning, $"Skipping step '{projectStep.Name}' step type:'{projectStep.StepType}' for process '{_projectModel.Name}', step is disabled.");
                return;
            }
            
            BridgeLog.Info($"Beginning step '{projectStep.Name}' step type:'{projectStep.StepType}' for process '{_projectModel.Name}'");
            Stopwatch swStep = new Stopwatch();
            swStep.Start();

            try
            {
                switch (projectStep.StepType)
                {
                    case "SQLUpdate":
                        new DAL.DALSQLUpdate(projectStep.DatabaseTarget).Update(_projectModel, projectStep);
                        break;
                    case "SQLRead":
                        DataTable dt = new SQLReadWorker().ReadData(_projectModel, projectStep);
                        if (dt != null)
                        {
                            if (_projectModel.MaxStartDay > 0)
                            {
                                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Info, $"Multi-Part Job ImportDays:{_projectModel.ImportDays} Current StartDay:{_projectModel.MaxStartDay} EndDay:{_projectModel.MaxEndDay} Returned total rows:" + dt.Rows.Count);
                            }
                            else
                            {
                                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Info, $"Returned total rows:" + dt.Rows.Count);
                            }
                        }
                        break;
                    case "StepFlow":
                        new StepFlowWorker(BridgeLogId).Evaluate(_projectModel, projectStep);
                        break;
                    case "Sync":
                        new SyncWorker(BridgeLogId).RunProcessStep(_projectModel, projectStep);
                        break;
                    case "SyncUpdate":
                        new SyncWorker(BridgeLogId).RunSyncUpdateStep(_projectModel, projectStep);
                        break;
                    case "RemoveVariable":
                        new VariableWorker(_projectModel.ParentSolution).RemoveVariable(projectStep);
                        break;
                    case "Skip":
                        break;
                    default:
                        throw new ApplicationException($"Unhandled StepType:'{projectStep.StepType}'");
                }
            }
            catch(Exception ex)
            {
                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.StepDetails, LogLevelEnum.Error, ex.Message);
                projectStep.ParentProject.ParentSolution.SolutionFinishStatus = "Error";
                projectStep.ParentProject.ParentSolution.SolutionFinishMessage = "Error: " + ex.Message;
                throw;
            }

            WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Flow, LogLevelEnum.Info, 
                $"Finishing step '{projectStep.Name}' step type:'{projectStep.StepType}' for process '{_projectModel.Name}', Total Time:{swStep.Elapsed.ToString()}");
        }
    }
}
