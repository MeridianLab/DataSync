using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Logger;
using Harvest.Bridge.Sync;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge
{
    internal class Program
    {
        System.Diagnostics.EventLog _serviceEventLog = null;
        // ProjectName:BridgeSyncProject 001 ProjectSource:StagingDB
        // ProjectFile:"C:\Users\joelb\source\repos\HarvestBridge\DataSyncMapping\BridgeSyncProject.json"
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                BridgeLog.Error("Expected usages, arguments ProjectName:??? ProjectSource:FileSystem|ConnectionStringName, not provided unable to continue.");
            }
            string projectName = GetArgValue(args, "ProjectName");
            string projectSource = GetArgValue(args, "ProjectSource");
            if (string.IsNullOrEmpty(projectName) == false)
            {
                LoadAndRun(projectName, projectSource);
            }
            else
            {
                BridgeLog.Error("Arguments ProjectFile:???, not provided unable to continue.");
            }
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

        static void LoadAndRun(string projectFile, string projectSource)
        {
            try
            {
                SolutionModel solutionModel = null;
                if (projectSource.Equals("FileSystem", StringComparison.OrdinalIgnoreCase))
                {
                    solutionModel = LoadProjectFromJSON.LoadProject(projectFile);
                }
                else
                {
                    string jsonData = new JSONStoreDAL(projectSource).GetJsonModel(projectFile);
                    solutionModel = LoadProjectFromJSON.LoadFromBase64(jsonData, projectFile, projectSource);
                }

                SolutionWorker solutionWorker = new SolutionWorker(solutionModel);

                solutionWorker.ProcessSolution();
            }
            catch (Exception ex)
            {
                BridgeLog.Error(ex.ToString());
            }
        }

        static string GetArgValue(string[] args, string argName)
        {
            string retVal = string.Empty;
            foreach(string arg in args) 
            { 
                if(arg.StartsWith(argName + ":", StringComparison.OrdinalIgnoreCase)) 
                {
                    retVal = arg.Substring(argName.Length + 1);
                    break;
                }
            }
            return retVal;
        }
    }
}
