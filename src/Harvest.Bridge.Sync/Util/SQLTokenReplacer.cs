using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Sync.Util
{
    public class SQLTokenReplacer
    {
        private List<SQLTokenReplacementModel> _sqlTokenReplList;
        public SQLTokenReplacer() { }

        public List<SQLTokenReplacementModel> TokenReplacementList
        {
            get
            {
                if (_sqlTokenReplList == null)
                {
                    DALConfiguration dalConfiguration = new DALConfiguration();
                    string val = String.Empty;
                    if(dalConfiguration.GetConfigValue("SQLTokenReplacement.Disable") == "True")
                    {
                        // Do nothing, skipping token replacemens
                    }
                    else
                    {
                        val = new DALConfiguration().GetConfigValue("SQLTokenReplacement");
                    }
                    if (string.IsNullOrEmpty(val))
                    {
                        _sqlTokenReplList = new List<SQLTokenReplacementModel>();
                    }
                    else
                    {
                        _sqlTokenReplList = (List<SQLTokenReplacementModel>)JsonCommonHelper.DeserializeObject(val, type: typeof(List<SQLTokenReplacementModel>));
                    }
                }

                return _sqlTokenReplList;
            }
        }

        public void SaveTokenReplacement(List<SQLTokenReplacementModel> tokenReplacement)
        {
            string json = JsonCommonHelper.SerializeObject(tokenReplacement);
            new DALConfiguration().UpdateConfig("SQLTokenReplacement", json);
        }
    }
}
