using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Harvest.Bridge.Test
{
    [TestClass]
    public class OrdPanelTest : TestBase
    {
        [TestMethod]
        public void ImportOrderPanelTest()
        {
            SolutionModel solutionModel = LoadSolution("BridgeSyncProject.json");
            SolutionWorker solutionWorker = new SolutionWorker(solutionModel);
            solutionWorker.ProcessProject("OrdPanel");
        }
    }
}
