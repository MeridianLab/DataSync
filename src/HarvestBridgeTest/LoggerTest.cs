using Harvest.Bridge.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HarvestBridgeTest
{
    [TestClass]
    public class LoggerTest
    {

        [TestMethod]
        public void LogTest()
        {
            BridgeLog.Debug("This is a test Debug log line");
            BridgeLog.Info("This is a test Info log line");
            BridgeLog.Warning("This is a test Warning log line");
            BridgeLog.Error("This is a test Error log line");

            double d = 0;
            try
            {
                double y = 233 / d;
                Assert.IsNull(y);
            }catch(Exception ex)
            {
                BridgeLog.Error("Test error with exception.", ex);
            }
        }
    }
}
