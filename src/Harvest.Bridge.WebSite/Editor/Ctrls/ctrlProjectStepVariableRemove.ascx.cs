using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ctrlProjectStepVariableRemove : EditorBase.EditorControls
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPreRender(EventArgs e)
        {
            btnSave.Enabled = IsInEditMode;
            if (CrntStepId != Guid.Empty)
            {
                LoadProjectStep();
                // Load Project Step
            }
            base.OnPreRender(e);
        }

        private void LoadProjectStep()
        {
            SolutionModel slnModel = GetSolutionModel();

            ProjectModel prjModel = slnModel.Projects.FirstOrDefault(p => p.Id == CrntProjectId);

            ProjectStepModel projectStep = prjModel.ProjectSteps.FirstOrDefault(ps => ps.Id == CrntStepId);
            if (projectStep != null)
            {
                txtStepName.Text = projectStep.Name;
                chkEnabled.Checked = projectStep.Enabled;

                drpVariableName.Items.Clear();
                drpVariableName.Items.Add("select one");
                foreach (var proj in prjModel.ProjectSteps)
                {
                    if (string.IsNullOrEmpty(proj.VariableName) == false)
                    {
                        drpVariableName.Items.Add($"InMemory: {proj.VariableName}");
                    }
                }

                drpVariableName.Text = "InMemory: " + projectStep.VariableName;
            }
        }
    }
}