using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ctrlProjectStepDataSync : EditorBase.EditorControls
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
                //txtSQL.Text = projectStep.SQLText;

                drpDBSourceName.Items.Clear();
                drpDBSourceName.Items.Add("SourceDB");
                drpDBSourceName.Items.Add("StagingDB");
                drpDBSourceName.Items.Add("TargetDB");

                drpTargetName.Items.Clear();
                drpTargetName.Items.Add("SourceDB");
                drpTargetName.Items.Add("StagingDB");
                drpTargetName.Items.Add("TargetDB");

                drpDBSourceName.Text = projectStep.DatabaseSource;
                //txtTableName.Text = projectStep.ta;
                //drpExpression.Text = projectStep.Expression;
            }
        }
    }
}