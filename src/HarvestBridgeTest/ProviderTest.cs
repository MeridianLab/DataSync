using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Harvest.Bridge.Test
{
    [TestClass]
    public class ProviderTest : TestBase
    {
        [TestMethod]
        public void SyncProviderTest()
        {
            SolutionModel solutionModel = LoadSolution("BridgeSyncProject.json");
            SolutionWorker solutionWorker = new SolutionWorker(solutionModel);
            solutionWorker.ProcessProject("Provider");
        }
    }
}
