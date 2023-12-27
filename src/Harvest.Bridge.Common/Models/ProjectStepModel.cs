using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Harvest.Bridge.Common.Models
{
    public class ProjectStepModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; } = true;

        public string StepType { get; set; }
        public string DatabaseSource { get; set; }
        public string DatabaseTarget { get; set; }
        public string SQLText { get; set; }
        
        [JsonIgnore]
        public string SQLText_Original { get; set; }
        public string VariableName { get; set; }

        public DataMapModel DataMap { get; set; }
        public ResultActionModel ResultAction { get; set; }
        public string Expression { get; set; }

        public string Description { get; set; }
        [JsonIgnore]
        public ProjectModel ParentProject { get; set; }

        [JsonIgnore]
        public List<LogDetail> LogDetails { get; set; }
        public string ExpressionAction { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
