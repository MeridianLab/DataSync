using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Logger;
using System;
using System.Collections.Generic;

namespace Harvest.Bridge.Sync
{
    public abstract class WorkerBase
    {
        protected void WriteLogDetail(Guid bridgeLogId, ProjectStepModel projectStep, CategoryEnum category, LogLevelEnum logLevel, string message)
        {
            if (projectStep.LogDetails == null) { projectStep.LogDetails = new List<LogDetail>(); }
            projectStep.LogDetails.Add(new LogDetail(category, message));

            if (logLevel != LogLevelEnum.Debug)
            {
                new DALBridgeLog().WriteBridgeLogDetailMsg(bridgeLogId, projectStep.ParentProject.Name, projectStep.Name, message, logLevel.ToString());
            }

            switch (logLevel)
            {
                case LogLevelEnum.Debug:
                    BridgeLog.Debug(message);
                    break;
                case LogLevelEnum.Info:
                    BridgeLog.Info(message);
                    break;
                case LogLevelEnum.Warning:
                    BridgeLog.Warning(message);
                    break;
                case LogLevelEnum.Error:
                    BridgeLog.Error(message);
                    break;
            }
        }
    }
}
