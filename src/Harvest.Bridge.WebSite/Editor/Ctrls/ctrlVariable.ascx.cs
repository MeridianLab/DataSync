using Harvest.Bridge.Common.Models;
using Harvest.Bridge.WebSite.Editor.EditorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ctrlVariable : EditorControls
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
            string varName = Session["StepName"].ToString();

            VariableModel variableModel = slnModel.GlobalVariables.FirstOrDefault(gv => gv.Name == varName);

            if (variableModel != null)
            {
                txtVariableName.Text = variableModel.Name;
                //txtSQL.Text = variableModel.s.SQLText;
                //chkEnabled.Checked = projectStep.Enabled;

                //drpDBSourceName.Items.Clear();
                //drpDBSourceName.Items.Add("none");
                //drpDBSourceName.Items.Add("SourceDB");
                //drpDBSourceName.Items.Add("StagingDB");
                //drpDBSourceName.Items.Add("TargetDB");

                //foreach (var proj in prjModel.ProjectSteps)
                //{
                //    if (string.IsNullOrEmpty(proj.VariableName) == false)
                //    {
                //        drpDBSourceName.Items.Add($"InMemory:{proj.VariableName}");
                //    }
                //}

                //drpDBTargetName.Items.Clear();
                //drpDBTargetName.Items.Add("SourceDB");
                //drpDBTargetName.Items.Add("StagingDB");
                //drpDBTargetName.Items.Add("TargetDB");

                //drpDBSourceName.Text = projectStep.DatabaseSource;
                //drpDBTargetName.Text = projectStep.DatabaseTarget;
            }
        }

    }
}