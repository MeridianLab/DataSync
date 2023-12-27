using System;
using System.Data;

namespace Harvest.Bridge.DAL
{
    public class JSONStoreDAL : DataAccess
    {
        public JSONStoreDAL(string dbConnectionName) : base(dbConnectionName)
        { }

        public DataTable GetAvailableVersions()
        {
            string sql = "SELECT TOP (1000) [ID],[NAME],[IS_LOCKED],[CREATE_DATE],[MODIFIED_DATE] FROM SYNC_JSON_DRIVER ORDER BY [MODIFIED_DATE] DESC";
            return Read(sql);
        }

        public string GetJsonModel(Guid id)
        {
            string retVal = null;
            string sql = "SELECT JSON_MODEL FROM SYNC_JSON_DRIVER WHERE ID = '" + id + "'";
            retVal = Read(sql).Rows[0][0].ToString();
            return retVal;
        }

        public string GetJsonModel(string name)
        {
            string retVal = null;
            string sql = "SELECT JSON_MODEL FROM SYNC_JSON_DRIVER WHERE Name = '" + StrFix(name) + "'";
            retVal = Read(sql).Rows[0][0].ToString();
            return retVal;
        }

        public string GetJsonModel(object id)
        {
            throw new NotImplementedException();
        }

        public DataTable SaveRecord(Guid id, string name, string jsonModel, bool isLocked)
        {
            int cnt = ExecuteScalar("SELECT COUNT(*) FROM SYNC_JSON_DRIVER WHERE ID = '" + id.ToString() + "'");
            string sql = null;
            if (cnt == 0)
            {
                // Create Record
                sql = $"INSERT INTO SYNC_JSON_DRIVER ([ID],[NAME],[JSON_MODEL],[IS_LOCKED],[CREATE_DATE],[MODIFIED_DATE]) VALUES ('{id.ToString()}','{StrFix(name, 150)}','{StrFix(jsonModel)}',0,GETDATE(),GETDATE())";                
            }
            else
            {
                // Update Record
                sql = $"UPDATE SYNC_JSON_DRIVER SET NAME='{StrFix(name, 150)}', JSON_MODEL='{StrFix(jsonModel)}', MODIFIED_DATE=GETDATE() WHERE ID='{id.ToString()}'";                
            }
            ExecuteScalar(sql);
            return Read($"SELECT * FROM SYNC_JSON_DRIVER WHERE ID = '{id.ToString()}'");
        }
    }
}
