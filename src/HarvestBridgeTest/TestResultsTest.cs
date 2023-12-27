using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Harvest.Bridge.Test
{
    [TestClass]
    public class TestResultsTest : TestBase
    {
        [TestMethod]
        public void SyncTestResultsTest()
        {
            SolutionModel solutionModel = LoadSolution("BridgeSyncProject.json");
            SolutionWorker solutionWorker = new SolutionWorker(solutionModel);
            solutionWorker.ProcessProject("TestResult");
        }
    }
}
