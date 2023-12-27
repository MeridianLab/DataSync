using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.DBStore;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Sync
{
    public class JSONStoreWorker
    {
        public JSONStoreWorker() { }

        public List<JSONStoreModel> GetAvailableList(string dbSource)
        {
            List<JSONStoreModel> retList = new List<JSONStoreModel>();
            DataTable dt = new JSONStoreDAL(dbSource).GetAvailableVersions();
            foreach(DataRow dr in dt.Rows)
            {
                JSONStoreModel model = new JSONStoreModel();
                model.Name= dr["Name"].ToString();
                model.Id = Guid.Parse(dr["id"].ToString());
                model.IsLocked = bool.Parse(dr["IS_LOCKED"].ToString());
                model.CreateDate = DateTime.Parse(dr["CREATE_DATE"].ToString());
                model.ModifiedDate = DateTime.Parse(dr["MODIFIED_DATE"].ToString());
                retList.Add(model);
            }

            return retList;
        }

        public SolutionModel OpenSolutionFromDB(string dbSource, Guid id)
        {
            string base64Encoded = new JSONStoreDAL(dbSource).GetJsonModel(id);
            return LoadProjectFromJSON.LoadFromBase64(base64Encoded, id.ToString(), dbSource);
        }

        public void SaveRecord(SolutionModel solutionModel, string dbSource)
        {
            string base64Encoded = JsonCommonHelper.GetBase64Value(solutionModel);
            DataTable dtRet = new JSONStoreDAL(dbSource).SaveRecord(solutionModel.Id, solutionModel.SolutionName, base64Encoded, false);
        }
    }
}
