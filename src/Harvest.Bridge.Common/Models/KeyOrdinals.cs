using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class KeyOrdinals
    {
        public int PKeyOrdinal { get; set; }
        public int FKey1Ordinal { get; set; } = -1;
        public int FKey2Ordinal { get; set; } = -1;
        public int FKey3Ordinal { get; set; } = -1;
        public int EpochOrdinal { get; set; }

    }
}
