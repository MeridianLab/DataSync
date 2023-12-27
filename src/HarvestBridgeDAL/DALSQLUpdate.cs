using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;

namespace Harvest.Bridge.DAL
{
    public class DALSQLUpdate : DataAccess
    {
        public DALSQLUpdate(string dbConnectionName) : base(dbConnectionName) { }

        public int Update(ProjectModel processModel, ProjectStepModel projectStep)
        {
            string sql = projectStep.SQLText;

            sql = StringHelpers.MergeString(sql, processModel.ParentSolution.VariableValues);

            return ExecuteScalar(sql);
        }

        public int SyncUpdate(DataTable dtSource, ProjectModel processModel)
        {
            int retCnt = 0;
            string sqlTemplate = processModel.CurrentProcess.SQLText;
            StringBuilder sbSql = new StringBuilder();
            int counter = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                sbSql.AppendLine(MergSql(row, sqlTemplate));
                counter++;
                if (counter > 100)
                {
                    retCnt += ExecuteScalar(sbSql.ToString());
                    counter = 0;
                    sbSql.Clear();
                }
            }

            if (sbSql.ToString().Length > 0)
            {
                retCnt += ExecuteScalar(sbSql.ToString());
            }
            return counter;
        }

        public int ProcessUpdates(ProjectModel processModel)
        {
            DataMapModel syncMap = processModel.CurrentProcess.DataMap;
            List<SyncActionModel> actions = syncMap.SyncActions.FindAll(s => s.Action == ActionEnum.Update);
            if (actions.Count > 0)
            {
                string updateTemplate = BuildUpdateClmnNames(syncMap);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < actions.Count; i++)
                {
                    string sql = updateTemplate;
                    SyncActionModel actionModel = actions[i];
                    string pKeyVal = GetValue(actionModel.SourceRow, syncMap.PKey.TargetName);
                    sql = sql.Replace("[~WhereClause~]", pKeyVal);
                    for (int c = 0; c < syncMap.ColumnMapModels.Count; c++)
                    {
                        ColumnMapModel clm = syncMap.ColumnMapModels[c];
                        //        // $"@p{posCnt}_XX";
                        string val = GetValue(actionModel.SourceRow, clm.ColumnName);
                        if (clm.DBType.Equals("Numeric", StringComparison.InvariantCultureIgnoreCase))
                        {
                            sql = sql.Replace($"@p{c}_XX", val);
                        }
                        else if (clm.DBType.Equals("bit", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (val.Equals("True", StringComparison.InvariantCultureIgnoreCase))
                            {
                                sql = sql.Replace($"@p{c}_XX", "1");
                            }
                            else
                            {
                                sql = sql.Replace($"@p{c}_XX", "0");
                            }
                        }
                        else
                        {
                            if (val != null && clm.MaxLength != -1 && val.Length > clm.MaxLength)
                            {
                                val = val.Substring(0, clm.MaxLength);
                            }
                            if (string.IsNullOrEmpty(val))
                            {
                                sql = sql.Replace($"@p{c}_XX", "null");
                            }
                            else
                            {
                                sql = sql.Replace($"@p{c}_XX", "'" + StrFix(val) + "'");
                            }
                        }
                    }
                    sb.AppendLine(sql);
                }

                bool success = false;
                int cnt = 0;
                do
                {
                    try
                    {
                        cnt++;
                        ExecuteScalar(sb.ToString());
                        success = true;
                    }
                    catch
                    {
                        if (cnt > 3)
                        {
                            throw;
                        }
                        System.Threading.Thread.Sleep(1500);
                    }                    
                }
                while (success == false || cnt <= 3);

            }
            return actions.Count;
        }

        private string BuildUpdateClmnNames(DataMapModel syncMap)
        {
            StringBuilder retVal = new StringBuilder($"UPDATE {syncMap.TableName} SET ");
            int posCnt = 0;
            bool first = true;
            foreach (ColumnMapModel clm in syncMap.ColumnMapModels)
            {
                string clmName = clm.ColumnNameTarget ?? clm.ColumnName;
                if (clmName != syncMap.PKey.TargetName)
                {
                    if (first == false)
                    {
                        retVal.Append(",");
                    }
                    first = false;
                    retVal.Append(clmName + $" = @p{posCnt}_XX");
                }
                posCnt++;
            }
            retVal.Append(" WHERE " + syncMap.PKey.TargetName + " = [~WhereClause~];");

            return retVal.ToString();
        }


        private string MergSql(DataRow row, string sqlTemplate)
        {
            NameValueCollection nvm = Util.StringHelpers.MergeStringReturnCollection(sqlTemplate);
            foreach (string r in nvm.Keys)
            {
                if (r.StartsWith("Source."))
                {
                    string clmName = r.Substring("Source.".Length);
                    string val = row[clmName].ToString();
                    sqlTemplate = sqlTemplate.Replace(StringHelpers.OPEN_VAL + r + StringHelpers.CLOSE_VAL, val);
                }
            }
            return sqlTemplate;
        }
    }
}
