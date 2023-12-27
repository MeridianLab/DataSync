using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ctrlProjectStepRead : EditorBase.EditorControls
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
            if(projectStep != null)
            {
                txtStepName.Text= projectStep.Name;
                txtSQL.Text = projectStep.SQLText;
                chkEnabled.Checked = projectStep.Enabled;

                drpDBSourceName.Items.Clear();
                drpDBSourceName.Items.Add("SourceDB");
                drpDBSourceName.Items.Add("StagingDB");
                drpDBSourceName.Items.Add("TargetDB");

                drpResultAction.Items.Clear();
                drpResultAction.Items.Add("None");
                drpResultAction.Items.Add("InMemory");

                drpDBSourceName.Text = projectStep.DatabaseSource;
                drpResultAction.Text = projectStep.ExpressionAction;
                txtVariableName.Text = projectStep.VariableName;
            }
        }

    }
}