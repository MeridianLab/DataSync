using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class SyncActionModel
    {
        public Guid Id { get; set; }
        public string PValue { get; set; }
        public string EpochValue { get; set; }
        public DataRow SourceRow { get; set; }
        public ActionEnum Action { get; set; }
        public bool IsComplete { get; set; }
    }
}
