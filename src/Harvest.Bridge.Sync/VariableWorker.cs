using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Harvest.Bridge.Sync
{
    public class VariableWorker : WorkerBase
    {
        private SolutionModel _solutionModel;
        public VariableWorker(SolutionModel solutionModel)
        {
            _solutionModel = solutionModel;
            if (_solutionModel.VariableValues == null) { _solutionModel.VariableValues = new List<VariableValueModel>(); }
        }
        public void ProcessVariables(List<VariableModel> variableList)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            foreach (VariableModel gv in variableList)
            {
                if (gv.Enabled == false) { continue; }
                BridgeLog.Info($"Processing Variable '{gv.Name}'");
                ProcessVariable(gv);
                BridgeLog.Info($"Variable '{gv.Name}' Processing Complete, resulting value '{gv.Value}'");
            }
            BridgeLog.Info("Processing Variables Complete, Total Time:" + sw.Elapsed.ToString());
        }

        public void ProcessVariable(VariableModel variableModel)
        {
            switch (variableModel.SourceType)
            {
                case "SQL":
                    LoadValueFromSQL(variableModel);
                    break;
                case "GenerateId":
                    SetVariableValue(variableModel, Guid.NewGuid().ToString());
                    break;
                default:
                    SetVariableValue(variableModel, variableModel.Value);
                    break;
            }
        }

        internal VariableValueModel SetVariableValue(VariableModel variable, object value)
        {
            RemoveVariable(variable.Name);
            VariableValueModel vvm = new VariableValueModel();
            vvm.VariableName = variable.Name;
            vvm.ObjectValue = value;
            _solutionModel.VariableValues.Add(vvm);
            return vvm;
        }

        internal void RemoveVariable(ProjectStepModel projectStep)
        {
            RemoveVariable(projectStep.VariableName);
        }

        internal void RemoveVariable(string variableName)
        {
            if (string.IsNullOrEmpty(variableName) == false)
            {
                VariableValueModel v = _solutionModel.VariableValues.FirstOrDefault(g => g.VariableName == variableName);
                _solutionModel.VariableValues.Remove(v);
            }
        }

        private void LoadValueFromSQL(VariableModel variableModel)
        {
            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;" + Environment.NewLine;
            sql += variableModel.Value;
            DataTable dt = new DALSQLRead(variableModel.DBSourceName).ReadData(sql);
            string val = string.Empty;

            switch (variableModel.ValueType)
            {
                case "CommaDelimitedString":
                    val = BuildCommaDelimitedString(dt);
                    break;
                case "Numeric":
                    val = 0.ToString();
                    if (dt.Rows.Count == 1 && dt.Rows.Count == 1 && (dt.Rows[0][0] is DBNull) == false)
                    {
                        val = dt.Rows[0][0].ToString();
                    }
                    break;
            }

            SetVariableValue(variableModel, val);
        }

        private string BuildCommaDelimitedString(DataTable dt)
        {
            string val = string.Empty;
            int pos = 0;
            int x = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos > 0)
                {
                    val += ",";
                }
                if(x > 10)
                {
                    val += " ";
                    x = 0;
                }
                val += dr[0].ToString();
                pos++;
                x++;
            }
            return val;
        }
    }
}
