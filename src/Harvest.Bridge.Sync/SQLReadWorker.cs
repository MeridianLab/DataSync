using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Harvest.Bridge.Sync
{
    internal class SQLReadWorker
    {
        internal SQLReadWorker() { }

        public DataTable ReadData(ProjectModel projectModel, ProjectStepModel projectStep)
        {
            string sql = projectStep.SQLText;

            if (projectModel.ParentSolution.ImportForSingleDay)
            {
                sql = UpdateSQLRunForSpecificDate(projectModel, sql);
            }
            else if (projectModel.ParentSolution.RuntimeParams["ImportMinutes"] != null)
            {
                sql = UpdateSQLRunForMinutes(projectModel, sql);
            }

            sql = StringHelpers.MergeString(sql, projectModel.ParentSolution.VariableValues);

            if (projectModel.MaxProcessDayCnt > 0)
            {
                string sqlStart = "declare @epochStartDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(day,-" + projectModel.MaxStartDay + ",GetDate())));" + Environment.NewLine;
                sqlStart += "declare @epochEndDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(day,-" + projectModel.MaxEndDay + ",GetDate())));" + Environment.NewLine;
                sql = sqlStart + sql;

                switch (projectModel.Name)
                {
                    case "TestResults":
                        sql = sql.Replace("copia.Result.runStamp >= @epochDt", "copia.Result.runStamp >= @epochStartDt AND copia.Result.runStamp < @epochEndDt");
                        break;
                    case "OrdPanel":
                        sql = sql.Replace("copia.Specimen.updateStamp > @epochDt", "copia.Specimen.updateStamp >= @epochStartDt AND copia.Specimen.updateStamp < @epochEndDt");
                        break;
                    case "Bloodtest":
                        sql = sql.Replace("updateStamp > @epochDt", "updateStamp >= @epochStartDt AND updateStamp < @epochEndDt");
                        break;
                    case "Patient":
                        sql = sql.Replace("copia.Patient.updateStamp > @epochDt", "copia.Patient.updateStamp >= @epochStartDt AND copia.Patient.updateStamp < @epochEndDt");
                        break;
                }
            }           

            DataTable dtRet = new DALSQLRead(projectStep.DatabaseSource).ReadData(sql);

            if (projectStep.ResultAction != null)
            {
                ProcessResultAction(projectModel, projectStep.ResultAction, dtRet);
            }
            return dtRet;
        }


        private string UpdateSQLRunForMinutes(ProjectModel projectModel, string sql)
        {
            // --declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(day, -1, GetDate())));
            // declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(MINUTE, -15, GetDate())))
            sql = sql.Replace("DateAdd(day,-[~ImportDays~]", $"DateAdd(MINUTE, -{projectModel.ParentSolution.RuntimeParams["ImportMinutes"]}");

            return sql;
        }

        private string UpdateSQLRunForSpecificDate(ProjectModel projectModel, string sql)
        {
            switch (projectModel.Name)
            {
                case "TestResults":
                case "OrdPanel":
                case "Bloodtest":
                case "Patient":
                    break;
                default:
                    return sql;
            }
            /*
-- Example of lines being replaced
declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(day,-[~ImportDays~],GetDate())))

WHERE copia.Specimen.updateStamp > @epochDt

-- Updated Lines
declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(CAST(DateAdd(day,-6,GetDate()) AS DATE)))
declare @epochDtTo as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(CAST(DateAdd(day,-5,GetDate()) AS DATE)))


WHERE (copia.Result.runStamp >= @epochDt AND copia.Result.runStamp <= @epochDtTo)
*/
            string importForDate = projectModel.ParentSolution.RuntimeParams.Get("ImportForSingleDate");
            string importForDateEnd = importForDate.Replace("12:00:00 AM", "11:59:59 PM");
            string epochPart = 
$@"declare @epochStartDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec('{importForDate}'))
declare @epochEndDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec('{importForDateEnd}'))
";
            sql = sql.Replace("declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(day,-[~ImportDays~],GetDate())))", epochPart);

            switch (projectModel.Name)
            {
                case "TestResults":
                    sql = sql.Replace("copia.Result.runStamp >= @epochDt", "copia.Result.runStamp >= @epochStartDt AND copia.Result.runStamp < @epochEndDt");
                    break;
                case "OrdPanel":
                    sql = sql.Replace("updateStamp > @epochDt", "updateStamp >= @epochStartDt AND updateStamp < @epochEndDt");
                    break;
                case "Bloodtest":
                    sql = sql.Replace("copia.Specimen.updateStamp > @epochDt", "copia.Specimen.updateStamp >= @epochStartDt AND copia.Specimen.updateStamp < @epochEndDt");
                    break;
                case "Patient":
                    sql = sql.Replace("copia.Patient.updateStamp > @epochDt", "copia.Patient.updateStamp >= @epochStartDt AND copia.Patient.updateStamp < @epochEndDt");
                    break;
            }

            return sql;
        }

        private void ProcessResultAction(ProjectModel processModel, ResultActionModel resultAction, DataTable dtRet)
        {
            if (resultAction.ResultActionType == "MemoryStore")
            {
                VariableValueModel v = new VariableValueModel();
                v.VariableName = resultAction.VariableName;
                v.ObjectValue = dtRet;
                processModel.ParentSolution.VariableValues.Add(v);
            }
        }
    }
}
