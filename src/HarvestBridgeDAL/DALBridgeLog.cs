using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public class DALBridgeLog : DataAccess
    {
        public DALBridgeLog() : base("StagingDB")
        { 
        }

        public int WriteTableCounts(Guid logId)
        {
            string crntConnectName = _dbConnectionStrName;
            // Read HavestSQL Table Counts
            SetConnectionStringByName("TargetDB");
            DataTable dt = Read(
@"select
(select count(*) from Bloodtest) BloodtestCnt,
(select count(*) from location) locationCnt,
(select count(*) from OrdPanel) OrdPanelCnt,
(select count(*) from .Patient) PatientCnt,
(select count(*) from Provider) ProviderCnt,
(select count(*) from Test) TestCnt,
(select count(*) from TestResults) TestResultsCnt");

            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                // Write Count to Staging Database Log Table
                string sql =
    $@"INSERT INTO [BridgeLogTblCounts]([BridgeLogId],[BloodtestCnt],[locationCnt],[OrdPanelCnt],[PatientCnt],[ProviderCnt],[TestCnt],[TestResultsCnt], CREATE_DATE)
select '[~LOGID~]', {dr[0]}, {dr[1]}, {dr[2]}, {dr[3]}, {dr[4]}, {dr[5]}, {dr[6]}, GETDATE()";
                sql = sql.Replace("[~LOGID~]", logId.ToString());
                SetConnectionStringByName("StagingDB");
                int cnt = ExecuteScalar(sql);
            }
            _dbConnectionStrName = crntConnectName;
            return dt.Rows.Count;
        }

        public void UpdatedAbandonedJobs()
        {
            ExecuteScalar("Update BridgeLog set Status='Failed' WHERE Status in ('Starting', 'Running') AND StartDateTime < DATEADD(MINUTE, -5, GETDATE())");
        }

        public void CleanupBridgeLogData()
        {
            ExecuteScalar(
@"delete from BridgeLogDetail where BridgeLogId in (select id from BridgeLog WHERE StartDateTime < DATEADD(DAY, -5, GETDATE()) AND STATUS = 'Finished')
delete from BridgeLogDetail where BridgeLogId in (select id from BridgeLog WHERE StartDateTime < DATEADD(DAY, -14, GETDATE()));
delete from BridgeLog WHERE StartDateTime < DATEADD(DAY, -14, GETDATE());");
        }

        public int WriteBridgeStartLog(Guid logId, string message) 
        {
            string sql = "INSERT INTO [BridgeLog] ([ID],[StartDateTime],[Status],[DescLog],[LogLevel]) VALUES ('[~ID~]',GETDATE(),'Starting','[~DescLog~]', 'Info')";
            sql = sql.Replace("[~ID~]", logId.ToString());
            sql = sql.Replace("[~DescLog~]", StrFix(message));
            return ExecuteScalar(sql);
        }

        public int WriteBridgeRunningLog(Guid logId)
        {
            string sql = "UPDATE [BridgeLog] SET STATUS = 'Running' WHERE ID = '" + logId.ToString() + "'";
            return ExecuteScalar(sql);
        }

        public int WriteBridgeFinishLog(Guid logId, string message, string status)
        {
            string sql = "UPDATE [BridgeLog] SET EndDateTime = GETDATE(), STATUS = '" + status + "', LogLevel='Info', DescLog=concat(DescLog, '" + Environment.NewLine + StrFix(message) + "') WHERE ID = '" + logId.ToString() +"'";
            return ExecuteScalar(sql);
        }

        public int UpdateBridgeLog(Guid logId, string message)
        {
            string sql = "UPDATE [BridgeLog] SET DescLog=concat(DescLog, '" + Environment.NewLine + StrFix(message) + "') WHERE ID = '" + logId.ToString() + "'";
            return ExecuteScalar(sql);
        }

        public int WriteBridgeLogDetailMsg(Guid logId, string projectName, string stepName, string message, string logLevel) 
        {
            string sql = "INSERT INTO BridgeLogDetail ([Id],[BridgeLogId],[ProjectName],[StepName],[Message],[Create_Date],[LogLevel]) VALUES (NEWID(),'[~BridgeLogId~]','[~ProjectName~]','[~StepName~]','[~Message~]',GETDATE(),'" + logLevel + "')";
            sql = sql.Replace("[~BridgeLogId~]", logId.ToString());
            sql = sql.Replace("[~ProjectName~]", StrFix(projectName, 100));
            sql = sql.Replace("[~StepName~]", StrFix(stepName, 200));
            sql = sql.Replace("[~Message~]", StrFix(message, 1000));            
            return ExecuteScalar(sql);
        }
    }
}
