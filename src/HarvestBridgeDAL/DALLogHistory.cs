using Harvest.Bridge.Common.Models.History;
using System;
using System.Collections.Generic;
using System.Data;

namespace Harvest.Bridge.DAL
{
    public class DALLogHistory : DataAccess
    {
        public DALLogHistory() : base("StagingDB")
        {
        }

        public List<HistoryModel> GetHistory(int topCnt = 500)
        {
            List<HistoryModel> retList = new List<HistoryModel>();
            DataTable dt = Read($"SELECT TOP ({topCnt}) [ID],[StartDateTime],[EndDateTime],[LogLevel],[Status],[DescLog] FROM [BridgeLog] ORDER BY StartDateTime DESC");
            foreach(DataRow dr in dt.Rows)
            {
                HistoryModel i = new HistoryModel();
                i.Id = (Guid)dr["ID"];
                i.Description = dr["DescLog"].ToString();
                i.Status = dr["Status"].ToString();
                i.LogLevel = dr["LogLevel"].ToString();
                i.StartDateTime = dr["StartDateTime"] is DBNull ? DateTime.MinValue : (DateTime)dr["StartDateTime"];
                i.EndDateTime = dr["EndDateTime"] is DBNull ? DateTime.MinValue : (DateTime)dr["EndDateTime"];
                retList.Add(i);
            }
            return retList;
        }

        public List<HistoryStepModel> GetProcessStepHistory(Guid bridgeId)
        {
            List<HistoryStepModel> retList = new List<HistoryStepModel>();

            DataTable dt = Read("SELECT [Id],[BridgeLogId],[ProjectName],[LogLevel],[StepName],[Message],[Create_Date] FROM [BridgeLogDetail] where bridgelogid = '" + bridgeId.ToString() + "' order by create_date");
            foreach(DataRow dr in dt.Rows)
            {
                HistoryStepModel hs = new HistoryStepModel();
                hs.Id = (Guid)dr["ID"];
                hs.Id = (Guid)dr["BridgeLogId"];
                hs.ProjectName = dr["ProjectName"].ToString();
                hs.StepName = dr["StepName"].ToString();
                hs.LogLevel = dr["LogLevel"].ToString();
                hs.Message = dr["Message"].ToString();
                hs.CreateDate = (DateTime)dr["Create_Date"];

                retList.Insert(0, hs);
            }

            return retList;
        }

        public DataTable GetRowCountHistory(Guid bridgeId)
        {
            DataTable dt = Read("SELECT [Create_Date],[BloodtestCnt],[locationCnt],[OrdPanelCnt],[PatientCnt],[ProviderCnt],[TestCnt],[TestResultsCnt] FROM BridgeLogTblCounts WHERE BridgeLogId = '" + bridgeId.ToString() + "' ORDER BY CREATE_DATE DESC");
            return dt;
        }
    }
}
