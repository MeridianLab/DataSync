using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Util;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Sync
{
    internal class StepFlowWorker
    {
        public Guid BridgeLogId { get; }
        public StepFlowWorker(Guid bridgeLogId)
        {
            BridgeLogId = bridgeLogId;
        }

        public void Evaluate(ProjectModel projectModel, ProjectStepModel projectStep)
        {
            projectModel.FlowControl = ProjectFlowControlEnum.Continue;

            string sql = projectStep.SQLText;

            if (projectModel.ParentSolution.RuntimeParams["ImportMinutes"] != null)
            {
                // --declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(day, -1, GetDate())));
                // declare @epochDt as BIGINT = (select ReportHelper.dbo.ufnDateTime2EpochSec(DateAdd(MINUTE, -15, GetDate())))
                sql = sql.Replace("DateAdd(day,-[~ImportDays~]", $"DateAdd(MINUTE, -{projectModel.ParentSolution.RuntimeParams["ImportMinutes"]}");
            }
            sql = StringHelpers.MergeString(sql, projectModel.ParentSolution.VariableValues);

            DataTable dt = new DAL.DALSQLRead(projectStep.DatabaseSource).ReadData(sql);
            string logMessage = projectStep.Expression;
            bool evalResult = false;
            switch (projectStep.Expression)
            {
                case "Record Count = 0":
                    if (dt.Rows.Count == 0)
                    {
                        evalResult = true;
                    }
                    logMessage += ", actual response [" + dt.Rows.Count + "] resulting in [" + evalResult.ToString() + "]";
                    break;
                case "Record Count > 0":
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        evalResult = true;
                    }
                    logMessage += ", actual response [" + dt.Rows.Count + "] resulting in [" + evalResult.ToString() + "]";
                    break;
                case "Execute Scalar = 0":
                case "Execute Scalar > 0":
                    int i = 0;
                    if (dt.Rows.Count == 1 && dt.Columns.Count == 1)
                    {
                        int.TryParse(dt.Rows[0][0].ToString(), out i);
                    }
                    if(projectStep.Expression == "Execute Scalar = 0")
                    {
                        if (i == 0) { evalResult = true; }
                    }
                    else
                    {
                        if (i > 0) { evalResult = true; }
                    }
                    logMessage += ", actual response [" + i.ToString() + "] resulting in [" + evalResult.ToString() + "]";

                    break;
            }

            if (evalResult)
            {
                switch(projectStep.ExpressionAction)
                {
                    case "Stop Project, Moves To Next Project":
                        projectModel.FlowControl = ProjectFlowControlEnum.StopCurrentProject;
                        break;
                    case "Stop Solution, Stop processing":
                        projectModel.FlowControl = ProjectFlowControlEnum.StopProcess;
                        break;
                }
            }

            logMessage += " : " + projectModel.FlowControl.ToString();
            projectStep.LogDetails.Add(new LogDetail(CategoryEnum.Flow, logMessage));
        }
    }
}
