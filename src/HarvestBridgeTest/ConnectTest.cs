using Harvest.Bridge.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Harvest.BridgeTest
{
    [TestClass]
    public class ConnectTest
    {
        [TestMethod]
        public void TestConnection()
        {
            DALSQLRead dalRead = new DALSQLRead("StagingDB");
            int cnt = dalRead.TestRecordCount();
            Assert.IsTrue(cnt > 0);
        }
    }
}
