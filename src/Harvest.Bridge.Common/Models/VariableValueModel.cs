using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class VariableValueModel
    {
        public string VariableName { get; set; }
        public object ObjectValue { get; set; }
        public bool IsGlobal { get; set; } = false;
    }
}
