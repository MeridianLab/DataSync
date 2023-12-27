using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Xml;

namespace Harvest.Bridge.Test
{
    [TestClass]
    public class PatientTest : TestBase
    {
        [TestMethod]
        public void SyncPatientTest()
        {
            SolutionModel solutionModel = LoadSolution("BridgeSyncProject.json");
            SolutionWorker solutionWorker = new SolutionWorker(solutionModel);
            solutionWorker.ProcessProject("Patient");
        }
    }
}
