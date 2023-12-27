using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class ColumnMapModel
    {
        public string ColumnName { get; set; }
        public string ColumnNameTarget { get; set; }
        public string DBType { get; set; }
        public int MaxLength { get; set; }
        public string DefaultValue { get; set; }
        public string MergeString { get; set; }
    }
}
