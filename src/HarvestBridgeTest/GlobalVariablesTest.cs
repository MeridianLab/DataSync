using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using Harvest.Bridge.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Harvest.Bridge.Test
{
    [TestClass]
    public class GlobalVariablesTest
    {
        [TestMethod]
        public void TestLoadingGlobalVariables()
        {
            string driverFilePath = ConfigurationManager.AppSettings["DriverFolderPath"];
            string projectFileName = "BridgeSyncProject.json";
            SolutionModel solutionModel = LoadProjectFromJSON.LoadProject(driverFilePath + projectFileName);

            foreach (VariableModel gv in solutionModel.GlobalVariables)
            {
                new VariableWorker(solutionModel).ProcessVariable(gv);
            }
        }
    }
}
