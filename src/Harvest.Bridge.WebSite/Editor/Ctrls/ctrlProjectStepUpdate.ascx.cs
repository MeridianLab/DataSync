using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ctrlProjectStepUpdate : EditorBase.EditorControls
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
                txtSQL.Text = projectStep.SQLText;
                chkEnabled.Checked = projectStep.Enabled;

                drpDBSourceName.Items.Clear();
                drpDBSourceName.Items.Add("none");
                drpDBSourceName.Items.Add("SourceDB");
                drpDBSourceName.Items.Add("StagingDB");
                drpDBSourceName.Items.Add("TargetDB");

                foreach (var proj in prjModel.ProjectSteps)
                {
                    if (string.IsNullOrEmpty(proj.VariableName) == false)
                    {
                        drpDBSourceName.Items.Add($"InMemory:{proj.VariableName}");
                    }
                }

                drpDBTargetName.Items.Clear();
                drpDBTargetName.Items.Add("SourceDB");
                drpDBTargetName.Items.Add("StagingDB");
                drpDBTargetName.Items.Add("TargetDB");

                drpDBSourceName.Text = projectStep.DatabaseSource;
                drpDBTargetName.Text = projectStep.DatabaseTarget;
            }
        }

    }
}