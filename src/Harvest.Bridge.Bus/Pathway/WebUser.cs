using Harvest.Bridge.Common.Models.Pathway;
using Harvest.Bridge.DAL.Pathway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Bus.Pathway
{
    public class WebUser
    {
        public WebUserModel Authenticate(string username, string password)
        {
            DataTable dt = new UserDAL().Authenticate(username, password);
            if (dt.Rows.Count == 0)
            {
                throw new ApplicationException("Invalid username or password");
            }
            WebUserModel webUser = new WebUserModel(dt.Rows[0]);
            //webUser._isAuthenticated = true;
            //webUser._userName = username;
            //webUser._userID = (int)userDataCollection.DataReader["userID"];
            //if (userDataCollection.DataReader["ProviderIdList"] != null)
            //    webUser._providerIdList = userDataCollection.DataReader["ProviderIdList"].ToString();
            //webUser._limitToProvider = Utility.DbValToBool(userDataCollection.DataReader["LimitToProvider"], false);
            //webUser._passwordResetReq = Utility.DbValToBool(userDataCollection.DataReader["PasswordResetReq"], false);
            //Utility.DbValToBool(userDataCollection.DataReader["IsSuperUser"], false);
            //Utility.DbValToBool(userDataCollection.DataReader["IsAdministrator"], false);
            //if (!userDataCollection.HasValidLocation(webUser._userID))
            //    throw new ApplicationException("No valid location found");
            //webUser.Privileges.Reload(username);
            return webUser;
        }
    }
}
