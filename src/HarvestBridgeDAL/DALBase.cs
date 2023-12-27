using Harvest.Bridge.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL
{
    public abstract class DALBase
    {
        #region Members
        protected string _dbConnectionStrName = "TargetDB";
        protected SqlCommand _command;
        protected bool _isTransaction = false;
        protected bool MaintainConnection = false;
        private string _connectionString = (string)null;
        protected bool _isParameterized = false;
        #endregion

        protected void SetConnectionStringByName(string name)
        {
            _dbConnectionStrName = name;
            _connectionString = null;
        }

        protected bool Open()
        {
            if (this._isTransaction || this.MaintainConnection)
                return false;
            if (this._command != null && this._command.Connection != null)
                return true;
            if (this._command == null)
                this._command = new SqlCommand();
            this._command.Connection = this.GetDBConnection();
            if (this._command.Connection == null)
                this._command.Connection = this.GetDBConnection();
            this._command.Connection.Open();
            return true;
        }

        protected void Close()
        {
            if (this._isTransaction || this.MaintainConnection || this._command == null || this._command.Connection == null)
                return;
            if (this._command.Connection.State != 0)
                this._command.Connection.Close();
            this._command = (SqlCommand)null;
        }

        protected DbParameter BuildParameter(string name, object value, DbType datatype)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;
            param.Direction = ParameterDirection.Input;
            param.DbType = datatype;
            param.Value = value == null ? DBNull.Value : value;

            Open();
            _command.Parameters.Add(param);

            return param;
        }

        protected string GetValue(DataRow row, string columnName)
        {
            string retVal = null;
            if (row[columnName] is DBNull == false)
            {
                retVal = row[columnName].ToString();
            }

            return retVal;
        }

        protected DbType GetDateType(string dbTypeName)
        {
            DbType retVal = DbType.String;
            switch (dbTypeName.ToLowerInvariant())
            {
                case "numeric":
                    retVal = DbType.Int32; break;
                case "datetime":
                    retVal = DbType.DateTime; break;
            }
            return retVal;
        }
        public string StrFix(string value)
        {
            if(value == null)
            {
                value = string.Empty;
            }
            else if (value.IndexOf("'") != -1)
            {
                while (value.IndexOf("''") != -1)
                    value = value.Replace("''", "'");
                value = value.Replace("'", "''");
            }
            return value;
        }

        public string StrFix(string value, int maxLength)
        { 
            if(maxLength > 0 && value.Length > maxLength)
            {
                value = value.Substring(0, maxLength);
            }
            return StrFix(value);
        }

            protected void BeginTransaction()
        {
            if (this._isTransaction)
                return;
            this.Open();
            this._command.Transaction = this._command.Connection.BeginTransaction(IsolationLevel.RepeatableRead);
            this._isTransaction = true;
        }

        protected void Commit()
        {
            try
            {
                if (!this._isTransaction)
                    return;
                this._command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this._isTransaction = false;
                this.Close();
            }
        }

        protected void Rollback()
        {
            try
            {
                this._command.Transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this._isTransaction = false;
                this.Close();
            }
        }

        protected virtual string DBConnectionString
        {
            get
            {
                if (String.IsNullOrEmpty(this._connectionString))
                {
                    if (ConfigurationManager.ConnectionStrings[_dbConnectionStrName] == null)
                    {
                        throw new ConfigurationErrorsException($"Configuration Setting '{_dbConnectionStrName}' was not found, unable to continue.");
                    }
                    this._connectionString = ConfigurationManager.ConnectionStrings[_dbConnectionStrName].ConnectionString;
                }
                return this._connectionString;
            }
            set => this._connectionString = value;
        }

        private SqlConnection GetDBConnection()
        {
            return new SqlConnection(this.DBConnectionString);
        }

        public void Dispose()
        {
        }

        protected void LogTime(string msg, Stopwatch sw)
        {
            BridgeLog.Debug("Connection Name:'" + _dbConnectionStrName + "'" + Environment.NewLine + msg + ". Total Elapsed Time:" + sw.Elapsed.ToString());
        }

        protected void LogError(string msg, Stopwatch sw, Exception ex)
        {
            BridgeLog.Error("Connection Name:'" + _dbConnectionStrName + "'" + Environment.NewLine + msg + ". Total Elapsed Time:" + sw.Elapsed.ToString(), ex);
        }
    }
}
