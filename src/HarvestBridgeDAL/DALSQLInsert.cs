using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public class DALSQLInsert : DataAccess
    {
        public DALSQLInsert(string dbConnectionName) : base(dbConnectionName) { }

        public int ProcessInserts(ProjectModel processModel)
        {
            int retCnt = 0;
            DataMapModel syncMap = processModel.CurrentProcess.DataMap;
            List<SyncActionModel> actions = syncMap.SyncActions.FindAll(s => s.Action == ActionEnum.Insert);
            if (actions.Count > 0)
            {
                string insertTemplate = BuildInsertTemplate(syncMap);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < actions.Count; i++)
                {
                    string sql = insertTemplate;
                    SyncActionModel actionModel = actions[i];
                    for (int c = 0; c < syncMap.ColumnMapModels.Count; c++)
                    {
                        ColumnMapModel clm = syncMap.ColumnMapModels[c];
                        // $"@p{posCnt}_XX";
                        string val = GetValue(actionModel.SourceRow, clm.ColumnName);
                        if (_isParameterized)
                        {
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
                                string pName = $"@p{c}_{i}";
                                BuildParameter(pName, val, GetDateType(clm.DBType));
                                sql = sql.Replace($"@p{c}_XX", pName);
                            }
                        }
                        else
                        {
                            if (clm.DBType.Equals("Numeric", StringComparison.InvariantCultureIgnoreCase))
                            {
                                sql = sql.Replace($"@p{c}_XX", val);
                            }
                            else if(clm.DBType.Equals("bit", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if(val.Equals("True", StringComparison.InvariantCultureIgnoreCase))
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

                    }
                    sb.AppendLine(sql);
                }

                retCnt = ExecuteScalar(sb.ToString());
            }
            return actions.Count;
        }

        private string BuildInsertTemplate(DataMapModel syncMap)
        {
            string retVal = $"INSERT INTO {syncMap.TableName} ";
            string clmnNames = string.Empty;
            string paramNames = string.Empty;
            int posCnt = 0;
            foreach (ColumnMapModel clm in syncMap.ColumnMapModels)
            {
                if (posCnt > 0)
                {
                    clmnNames += ",";
                    paramNames += ",";
                }
                clmnNames += clm.ColumnNameTarget ?? clm.ColumnName;
                paramNames += $"@p{posCnt}_XX";
                posCnt++;
            }
            return $"{retVal} ({clmnNames}) VALUES ({paramNames});";
        }
    }
}
