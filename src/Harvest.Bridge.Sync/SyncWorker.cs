using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Logger;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Harvest.Bridge.Sync
{
    internal class SyncWorker : WorkerBase
    {
        private ProjectModel _projectModel;
        private ProjectStepModel _projectStep;
        public Guid BridgeLogId { get; }
        internal SyncWorker(Guid bridgeLogId)
        {
            BridgeLogId = bridgeLogId;
        }

        internal void RunProcessStep(ProjectModel projectModel, ProjectStepModel projectStep)
        {
            _projectModel = projectModel;
            _projectStep = projectStep;

            // Pull data from Source
            DataTable dtSource = RetrieveDataTable();
            if (dtSource != null)
            {
                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Info, "Database Source record count:" + dtSource.Rows.Count);
            }
            else
            {
                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Warning, "Database Source table was not found");
            }

            if (dtSource != null)
            {
                // Sync data in Target
                new SyncDataWorker(BridgeLogId).SyncData(dtSource, _projectModel, projectStep);
            }
        }

        internal void RunSyncUpdateStep(ProjectModel projectModel, ProjectStepModel projectStep)
        {
            _projectModel = projectModel;
            _projectStep = projectStep;

            // Pull data from Source
            DataTable dtSource = RetrieveDataTable();

            // Sync data in Target
            int cnt = new DAL.DALSQLUpdate(projectModel.CurrentProcess.DatabaseTarget).SyncUpdate(dtSource, _projectModel);
            WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Info, "Total record count updated:" + cnt);
        }

        private DataTable RetrieveDataTable()
        {
            DataTable dtRet = null;
            if(_projectStep.DatabaseSource.StartsWith("InMemory:"))
            {
                string vName = _projectStep.DatabaseSource.Split(':')[1];
                object varValue = _projectModel.ParentSolution.GetVariableValue(vName);
                if (varValue != null)
                {
                    dtRet = varValue as DataTable;
                    if (dtRet == null)
                    {
                        BridgeLog.Warning($"Failed to load datatable '{vName}' from memory");
                    }
                    else
                    {
                        BridgeLog.Info($"Loaded datatable '{vName}' from memory, total record count:" + dtRet.Rows.Count);
                    }
                }
            }
            return dtRet;
        }
    }
}
