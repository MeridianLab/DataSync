using System;
using System.Configuration;
using System.Data;

namespace Harvest.Bridge.DAL
{
    public class DALConfiguration : DataAccess
    {
        public DALConfiguration() : base("StagingDB")
        {
        }

        public DataTable GetConfigurationData()
        {
            return Read("SELECT Name, [Value], CREATE_DATE CreateDate, MODIFIED_DATE ModifiedDate FROM BridgeConfig ORDER BY NAME");
        }
        public bool GetConfigValueAsBool(string name)
        {
            bool retVal = false;
            string val = GetConfigValue(name);
            if (string.IsNullOrEmpty(val) == false)
            {
                bool.TryParse(val, out retVal);
            }
            return retVal;
        }

        public string GetConfigValue(string name)
        {
            string val = ConfigurationManager.AppSettings[name];
            if (string.IsNullOrEmpty(val))
            {
                DataTable dt = Read("SELECT [VALUE] from BridgeConfig where Name = '" + StrFix(name) + "'");
                if (dt.Rows.Count == 1)
                {
                    val = dt.Rows[0][0].ToString();
                }
                else
                {
                    val = string.Empty;
                }
            }
            return val;
        }

        public int UpdateConfig(string name, string value)
        {
            int cnt = ExecuteScalar($"SELECT COUNT(*) FROM BridgeConfig WHERE NAME = '{StrFix(name)}'");
            int retCnt = -1;
            if (cnt == 1)
            {
                string sql = $"UPDATE BridgeConfig SET VALUE='{StrFix(value, 2500)}', MODIFIED_DATE = GETDATE() WHERE NAME = '{name}'";
                retCnt = ExecuteScalar(sql);
            }
            else
            {
                retCnt = ExecuteScalar($"INSERT INTO BridgeConfig(NAME, [VALUE], CREATE_DATE, MODIFIED_DATE) VALUES ('{StrFix(name, 50)}','{StrFix(value, 2500)}', GETDATE(), GETDATE());");
            }
            return retCnt;
        }

        public void SaveChanges(DataTable dt)
        {
            DataTable dtChanges = dt.GetChanges();
            if (dtChanges != null)
            {
                foreach (DataRow dr in dtChanges.Rows)
                {
                    string name = dr["Name"].ToString();
                    string value = dr["Value"].ToString();
                    if (dr.RowState == DataRowState.Added)
                    {
                        ExecuteScalar($"INSERT INTO BridgeConfig(NAME, [VALUE], CREATE_DATE, MODIFIED_DATE) VALUES ('{StrFix(name, 50)}','{StrFix(value, 2500)}', GETDATE(), GETDATE());");
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        ExecuteScalar($"UPDATE BridgeConfig SET VALUE = '{StrFix(value, 2500)}', MODIFIED_DATE = GETDATE() where NAME = '{StrFix(name, 50)}'");
                    }
                    else if (dr.RowState == DataRowState.Deleted)
                    {
                        ExecuteScalar($"DELETE FROM BridgeConfig WHERE NAME = '{StrFix(name, 50)}'");
                    }
                }
            }
        }
    }
}
