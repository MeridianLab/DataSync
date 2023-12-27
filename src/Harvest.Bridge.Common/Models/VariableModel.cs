using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class VariableModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; } = true;

        public string Description { get; set; }
        public string Value { get; set; }
        public string SourceType { get; set; }
        public string DBSourceName { get; set; }
        public string ValueType { get; set; }
        [JsonIgnore]
        public ProjectModel ParentProject { get; set; }
        [JsonIgnore]
        public SolutionModel ParentSolution { get; set; }
    }
}
