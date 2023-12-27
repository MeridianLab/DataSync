using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Sync
{
    internal class SyncDataWorker : WorkerBase
    {
        private bool _forceUpdateAllAsDefaultAction = false;
        public Guid BridgeLogId { get; }
        public SyncDataWorker(Guid bridgeLogId) 
        { 
            BridgeLogId= bridgeLogId;
        }
        internal void SyncData(DataTable dtSource, ProjectModel processModel, ProjectStepModel projectStep)
        {
            int crntStartPos = 0;
            if (dtSource.Rows.Count == 0)
            {
                Logger.BridgeLog.Info($"No records to process for step '{projectStep.Name}'");
            }
            else
            {
                _forceUpdateAllAsDefaultAction = projectStep.ParentProject.ParentSolution.DefaultUpdateActionUpdateAll;
                if (projectStep.DataMap.SourceKeyOrdinals == null) { projectStep.DataMap.SourceKeyOrdinals = new KeyOrdinals(); }
                SetColumnOrdinalsForSource(projectStep.DataMap, projectStep.DataMap.SourceKeyOrdinals, dtSource);

                int insertCnt = 0;
                int updateCnt = 0;
                do
                {
                    projectStep.DataMap.SyncActions = new List<SyncActionModel>();
                    DataTable dtTargetSyncResutls = new SyncCompareSource(projectStep.DatabaseTarget).RetrieveSourceRecords(processModel, dtSource, crntStartPos);
                    if (dtTargetSyncResutls != null)
                    {
                        BuildSyncActionList(projectStep.DataMap, dtSource, dtTargetSyncResutls, crntStartPos);

                        insertCnt += new DALSQLInsert(projectStep.DatabaseTarget).ProcessInserts(processModel);
                        updateCnt += new DALSQLUpdate(projectStep.DatabaseTarget).ProcessUpdates(processModel);
                    }
                    if (processModel.ParentSolution.CancelRun)
                    {
                        throw new ApplicationException("Cancel invoked");
                    }
                    crntStartPos += projectStep.DataMap.BatchSize + 1;
                } while (projectStep.DataMap.SyncActions.Count > 0);
                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Info, "Total Record Count Created:" + insertCnt);
                WriteLogDetail(BridgeLogId, projectStep, CategoryEnum.Database, LogLevelEnum.Info, "Total Record Count Updated:" + updateCnt);
            }
        }

        private void BuildSyncActionList(DataMapModel syncMap, DataTable dtSource, DataTable dtTargetResult, int startPos)
        {
            Hashtable syncResultLookup = new Hashtable(27);
            if (syncMap.TargetKeyOrdinals == null) { syncMap.TargetKeyOrdinals= new KeyOrdinals(); }
            SetColumnOrdinalsForTarget(syncMap, syncMap.TargetKeyOrdinals, dtTargetResult);

            if (syncMap.SourceKeyOrdinals.PKeyOrdinal == -1)
            {
                if (syncMap.SourceKeyOrdinals == null) { syncMap.SourceKeyOrdinals = new KeyOrdinals(); }
                SetColumnOrdinalsForSource(syncMap, syncMap.SourceKeyOrdinals, dtSource);
            }

            for (int i = 0; i < dtTargetResult.Rows.Count; i++)
            {
                string hashKey = BuildHashKey(syncMap.TargetKeyOrdinals, dtTargetResult.Rows[i]);
                if (syncResultLookup.ContainsKey(hashKey) == false)
                {
                    syncResultLookup.Add(hashKey, i);
                }
            }
            int stopPos = startPos + syncMap.BatchSize;
            for (int i = startPos; i < stopPos; i++)
            {
                if (i >= dtSource.Rows.Count)
                {
                    break;
                }
                string srcHashKey = BuildHashKey(syncMap.SourceKeyOrdinals, dtSource.Rows[i]);

                SyncActionModel syncModel = new SyncActionModel()
                {
                    Action = _forceUpdateAllAsDefaultAction ? ActionEnum.Update : ActionEnum.None,
                    IsComplete = false,
                };

                if (syncResultLookup.ContainsKey(srcHashKey))
                {
                    DataRow drTargetResult = dtTargetResult.Rows[(int)syncResultLookup[srcHashKey]];
                    string sourceEpoch = dtSource.Rows[i][syncMap.SourceKeyOrdinals.EpochOrdinal].ToString();
                    string targetEpoch = drTargetResult[syncMap.TargetKeyOrdinals.EpochOrdinal].ToString();
                    bool isUpdate = true;
                    if (sourceEpoch == targetEpoch)
                    {
                        if (_forceUpdateAllAsDefaultAction == false)
                        {
                            isUpdate = false;
                            syncModel.Action = ActionEnum.None;
                            syncModel.IsComplete = true;
                        }
                    }
                    if(isUpdate)
                    {
                        syncModel.SourceRow = dtSource.Rows[i];
                        syncModel.EpochValue = dtSource.Rows[i][syncMap.SourceKeyOrdinals.EpochOrdinal].ToString();
                        syncModel.PValue = dtSource.Rows[i][syncMap.SourceKeyOrdinals.PKeyOrdinal].ToString();
                        syncModel.Action = ActionEnum.Update;
                    }
                }
                else
                {
                    // Insert
                    syncModel.SourceRow = dtSource.Rows[i];
                    syncModel.EpochValue = dtSource.Rows[i][syncMap.SourceKeyOrdinals.EpochOrdinal].ToString();
                    syncModel.PValue = dtSource.Rows[i][syncMap.SourceKeyOrdinals.PKeyOrdinal].ToString();
                    syncModel.Action = ActionEnum.Insert;
                }

                syncMap.SyncActions.Add(syncModel);
            }
        }

        private void SetColumnOrdinalsForSource(DataMapModel syncMap, KeyOrdinals keyOrdinals, DataTable dt)
        {
            keyOrdinals.PKeyOrdinal = dt.Columns[syncMap.PKey.SourceName].Ordinal;

            if (string.IsNullOrEmpty(syncMap.EpochKey.SourceName) == false)
            {
                keyOrdinals.EpochOrdinal = dt.Columns[syncMap.EpochKey.SourceName].Ordinal;
            }

            if (syncMap.FKeys.Count > 0)
            {
                keyOrdinals.FKey1Ordinal = dt.Columns[syncMap.FKeys[0].SourceName].Ordinal;
            }
            if (syncMap.FKeys.Count > 1)
            {
                keyOrdinals.FKey2Ordinal = dt.Columns[syncMap.FKeys[1].SourceName].Ordinal;
            }
            if (syncMap.FKeys.Count > 2)
            {
                keyOrdinals.FKey3Ordinal = dt.Columns[syncMap.FKeys[2].SourceName].Ordinal;
            }
        }

        private void SetColumnOrdinalsForTarget(DataMapModel syncMap, KeyOrdinals keyOrdinals, DataTable dt)
        {
            keyOrdinals.PKeyOrdinal = dt.Columns[syncMap.PKey.TargetName].Ordinal;

            if (string.IsNullOrEmpty(syncMap.EpochKey.TargetName) == false)
            {
                keyOrdinals.EpochOrdinal = dt.Columns[syncMap.EpochKey.TargetName].Ordinal;
            }

            if (syncMap.FKeys.Count > 0)
            {
                keyOrdinals.FKey1Ordinal = dt.Columns[syncMap.FKeys[0].TargetName].Ordinal;
            }
            if (syncMap.FKeys.Count > 1)
            {
                keyOrdinals.FKey2Ordinal = dt.Columns[syncMap.FKeys[1].TargetName].Ordinal;
            }
            if (syncMap.FKeys.Count > 2)
            {
                keyOrdinals.FKey3Ordinal = dt.Columns[syncMap.FKeys[2].TargetName].Ordinal;
            }
        }

        private string BuildHashKey(KeyOrdinals keyOrdinals, DataRow dr)
        {
            string retVal = dr[keyOrdinals.PKeyOrdinal].ToString();
            if (keyOrdinals.FKey1Ordinal != -1)
            {
                retVal += "-" + dr[keyOrdinals.FKey1Ordinal].ToString();
            }
            if (keyOrdinals.FKey2Ordinal != -1)
            {
                retVal += "-" + dr[keyOrdinals.FKey2Ordinal].ToString();
            }
            if (keyOrdinals.FKey3Ordinal != -1)
            {
                retVal += "-" + dr[keyOrdinals.FKey3Ordinal].ToString();
            }
            return retVal;
        }
    }
}
