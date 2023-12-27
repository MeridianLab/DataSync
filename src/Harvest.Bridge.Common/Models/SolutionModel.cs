using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class SolutionModel
    {
        public SolutionModel()
        {
            RuntimeParams = new NameValueCollection();
        }

        [JsonIgnore]
        public NameValueCollection RuntimeParams { get; set; }

        public Guid Id { get; set; }
        public string SolutionName { get; set; }
        public List<VariableModel> GlobalVariables { get; set; }
        [JsonIgnore]
        public List<VariableValueModel> VariableValues { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public string SolutionFileName { get; set; }
        [JsonIgnore]
        public string SolutionSource { get; set; }
        [JsonIgnore]
        public bool CancelRun { get; set; }
        [JsonIgnore]
        public DateTime? ImportStartTime { get; set; }
        [JsonIgnore]
        public bool ImportForSingleDay { get; set; }
        [JsonIgnore]
        public string SolutionFinishStatus { get; set; }
        [JsonIgnore]
        public string SolutionFinishMessage { get; set; }
        public bool DefaultUpdateActionUpdateAll { get; set; }

        public void SetProjectReferences()
        {
            foreach (var variable in GlobalVariables)
            {
                variable.ParentSolution = this;
            }
            foreach(ProjectModel project in Projects)
            {
                project.SetReferences(this);
            }
        }

        public object GetVariableValue(string variableName)
        {
            object retval = null;
            VariableValueModel vModel = VariableValues?.FirstOrDefault(g => g.VariableName == variableName);
            if (vModel != null)
            {
                retval = vModel.ObjectValue;
            }
            return retval;
        }

    }
}
