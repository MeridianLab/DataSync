using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Harvest.Bridge.Common.Models
{
    public class DataMapModel
    {
        public DataMapModel() 
        {
            //TargetKeyOrdinals = new KeyOrdinals();
            //SourceKeyOrdinals= new KeyOrdinals();
            ColumnMapModels = new List<ColumnMapModel>();
        }
        public string TableName { get; set; }
        public string Name { get; set; }
        public int BatchSize { get; set; }
        public DBRelationShipKeyModel PKey { get; set; }

        public List<DBRelationShipKeyModel> FKeys { get; set; }

        public DBRelationShipKeyModel EpochKey { get; set; }

        [JsonIgnore]
        public KeyOrdinals TargetKeyOrdinals { get; set; }
        [JsonIgnore]
        public KeyOrdinals SourceKeyOrdinals { get; set; }

        [JsonIgnore]
        public List<SyncActionModel> SyncActions { get; set; }
        public List<ColumnMapModel> ColumnMapModels { get; set; }
    }
}
