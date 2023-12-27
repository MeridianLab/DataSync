using Harvest.Bridge.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public class SyncCompareSource : DataAccess
    {
        bool parameterized = false;
        public SyncCompareSource(string dbConnectionName) : base(dbConnectionName) { }

        public DataTable RetrieveSourceRecords(ProjectModel processModel, DataTable dtSource, int startPos)
        {
            //StringBuilder sb = new StringBuilder();
            string fkSelect = BuildFKSelect(processModel.CurrentProcess.DataMap);
            string epochSelect = string.Empty;
            if(string.IsNullOrEmpty(processModel.CurrentProcess.DataMap.EpochKey.SourceName) == false)
            {
                epochSelect = "," + processModel.CurrentProcess.DataMap.EpochKey.SourceName;
            }
            string selectStatment = $"SELECT {processModel.CurrentProcess.DataMap.PKey.TargetName}{epochSelect}{fkSelect} FROM {processModel.CurrentProcess.DataMap.TableName} WHERE {processModel.CurrentProcess.DataMap.PKey.TargetName} IN (";
            bool runQuery = false;
            int stopPos = startPos + processModel.CurrentProcess.DataMap.BatchSize;
            bool first = true;
            for (int i=startPos; i< stopPos; i++)
            {
                if (i >= dtSource.Rows.Count)
                {
                    break;
                }

                if (!first) { selectStatment += ","; }
                first = false;

                string pvalue = dtSource.Rows[i][processModel.CurrentProcess.DataMap.SourceKeyOrdinals.PKeyOrdinal].ToString();
                if (parameterized == true)
                {
                    string paramName = "pf" + i;
                    BuildParameter(paramName, pvalue, DbType.String);
                    selectStatment += "@" + paramName;
                }
                else
                {
                    selectStatment += "'" + StrFix(pvalue) + "'";
                }
                runQuery = true;

                if(i >= stopPos)
                {
                    break;
                }
            }
            DataTable retTable = null;
            if (runQuery)
            {
                selectStatment += ")";
                retTable = Read(selectStatment);
            }

            return retTable;
        }

        private string BuildFKSelect(DataMapModel syncMap)
        {
            string retVal = string.Empty;

            if(syncMap.FKeys != null)
            {
                foreach(var fkey in syncMap.FKeys)
                {
                    retVal = ", " + fkey.TargetName;
                }
            }
            return retVal;
        }

        private string BuildFKWhere(DataMapModel syncMap)
        {
            string retVal = string.Empty;
            int pos = 1;
            foreach(var fkey in syncMap.FKeys)
            {
                retVal = $" AND {fkey.SourceName} = @fk{pos}xx";
                pos++;
            }

            return retVal;
        }
    }
}
