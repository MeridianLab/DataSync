using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class ResultActionModel
    {
        public Guid Id { get; set; }
        public string ResultActionType { get; set; }
        public string VariableName { get; set; }
    }
}
