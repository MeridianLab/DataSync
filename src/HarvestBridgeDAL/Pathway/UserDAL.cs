using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.DAL.Pathway
{
    public class UserDAL : DataAccess
    {
        public UserDAL() : base("ReportPortal")
        { }

        public DataTable Authenticate(string username, string password)
        {
            BuildParameter("username", username, DbType.String);
            BuildParameter("password", password, DbType.String);
            IsStoredProcedure = true;
            DataTable dtRet = Read("spRp20GetUser");
            return dtRet;
        }
    }
}
