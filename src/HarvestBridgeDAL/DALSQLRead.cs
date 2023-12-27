using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public class DALSQLRead : DataAccess
    {
        public DALSQLRead(string dbConnectionName) : base(dbConnectionName) { }

        public int TestRecordCount()
        {
            int ret = 0;

            ret = ExecuteScalar("SELECT COUNT(*) FROM Bloodtest");

            return ret;
        }

        public DataTable ReadData(string sqlStatement)
        {
            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;" + Environment.NewLine + sqlStatement;
            
            DataTable dtRet = Read(sql);

            return dtRet;
        }

    }
}
