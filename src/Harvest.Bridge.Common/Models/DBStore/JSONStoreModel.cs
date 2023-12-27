using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models.DBStore
{
    public class JSONStoreModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public bool IsLocked { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string JsonModel { get; set; }
    }
}
