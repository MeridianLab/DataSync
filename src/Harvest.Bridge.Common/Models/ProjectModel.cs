using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harvest.Bridge.Common.Models
{
    public class ProjectModel
    {
        public ProjectModel() 
        { 
            Id = Guid.NewGuid();
            ProjectSteps = new List<ProjectStepModel>();
        }
        private int _currentStepIndex = -1;        

        public Guid Id { get; set; }

        public bool Enabled { get; set; } = true;

        public bool AllowParallelProcessing { get; set; }
        public bool ParallelForceSync { get; set; }

        public string Name { get; set; }
        public List<VariableModel> ProcessVariables { get; set; }
        public List<ProjectStepModel> ProjectSteps { get; set; }
        [JsonIgnore]
        public int ImportDays { get; set; }
        [JsonIgnore]
        public int MaxStartDay { get; set; }
        [JsonIgnore] 
        public int MaxEndDay { get; set;}

        [JsonIgnore]
        public int MaxProcessDayCnt { get; set; }
                
        [JsonIgnore]
        public SolutionModel ParentSolution { get; private set; }

        [JsonIgnore]
        public ProjectStepModel CurrentProcess 
        { 
            get
            {
                if (_currentStepIndex < ProjectSteps.Count)
                {
                    return ProjectSteps[_currentStepIndex];
                }
                else { return null; }
            } 
        }

        public string Description { get; set; }
        
        [JsonIgnore]
        public ProjectFlowControlEnum FlowControl { get; set; }

        public void SetReferences(SolutionModel parentSolution)
        {
            ParentSolution = parentSolution;
            foreach (ProjectStepModel prjStep in ProjectSteps)
            {
                prjStep.ParentProject = this;
            }
        }

        public ProjectStepModel NextStep()
        {
            if (ProjectSteps.Count -1 > _currentStepIndex)
            {
                _currentStepIndex++;
                return ProjectSteps[_currentStepIndex];
            }
            else if(MaxStartDay > 0 && MaxProcessDayCnt > 0)
            {
                // Relooping until we have processed all days
                MaxStartDay = MaxEndDay;
                MaxEndDay = MaxEndDay - MaxProcessDayCnt;
                if (MaxEndDay < 0)
                {
                    MaxEndDay = 0;
                }
                _currentStepIndex = 0;
                return ProjectSteps[_currentStepIndex];
            }
            else
            {
                return null;
            }
        }

        public void Reset()
        {
            _currentStepIndex = 0;
        }
    }
}
