using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public class DataAccess : DALBase
    {
        protected DataAccess() { }

        protected DataAccess(string dbConnectionName) 
        {
            _dbConnectionStrName = dbConnectionName;
        }

        protected bool IsStoredProcedure
        {
            get { return _command.CommandType == CommandType.StoredProcedure; }
            set
            {
                if (value)
                {
                    _command.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    _command.CommandType = CommandType.Text;
                }
            }
        }
        protected DataTable Read(string sqlStatement)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            DataTable retTable = new DataTable();
            try
            {
                this.Open();
                this._command.CommandText = sqlStatement;
                this._command.CommandTimeout = 360;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(this._command);
                sqlDataAdapter.Fill(retTable);
            }
            catch (Exception ex)
            {
                LogError(sqlStatement, sw, ex);
                throw new Exception(ex.Message + " -- Offending SQL: " + sqlStatement);
            }
            finally
            {
                this.Close();
            }
            this.LogTime(sqlStatement + Environment.NewLine + " Row Count " + (object)retTable.Rows.Count, sw);            
            return retTable;
        }

        protected int ExecuteScalar(string sqlStatement) => this.ExecuteScalar(sqlStatement, 0);

        protected int ExecuteScalar(string sqlStatement, int defaultValue)
        {
            int num = -1;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                Open();
                _command.CommandText = sqlStatement;
                object obj = this._command.ExecuteScalar();
                if (obj is DBNull || obj == null)
                    num = defaultValue;
                else if (obj is int)
                    num = obj is int d ? int.Parse(obj.ToString()) : throw new Exception("Unexpected type returned from ExecuteScalar");
                else if (obj is decimal)
                    num = obj is Decimal d ? Decimal.ToInt32(d) : throw new Exception("Unexpected type returned from ExecuteScalar");
            }
            catch (Exception ex)
            {
                LogError(sqlStatement, sw, ex);
                throw;
            }
            finally
            {
                Close();
            }
            LogTime(sqlStatement + Environment.NewLine + "Rows Effected Count " + (object)num, sw);
            return num;
        }

    }
}
