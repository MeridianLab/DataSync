using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public class DALUtil : DataAccess
    {
        public DALUtil() : base("StagingDB") { }

        public void TruncateStagingTables()
        {
            SetConnectionStringByName("StagingDB");
            ExecuteScalar(
@"truncate table Bloodtest 
truncate table OrdPanel 
truncate table Provider
truncate table Test 
truncate table TestResults  
");
        }

        public void PreDeleteDataByDateRange(int forDays)
        {
            TruncateStagingTables();

            SetConnectionStringByName("TargetDB");
            string sql =
@"delete from Bloodtest where DRAWDATE > getdate()-d;
delete from OrdPanel where apprdate > getdate()-d;
-- delete from Patient where updateDate > getdate()-d;
delete from TestResults where rundate > getdate()-d;";
            sql = sql.Replace("-d", "-" + forDays);
            ExecuteScalar(sql);
        }
    }
}
