using Harvest.Bridge.Common.Models;
using System;
using System.Linq;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ProjectStepsList : EditorBase.EditorControls
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            if (HasSelectedSolution)
            {
                BindProjectStepsSelectBox();
            }
            base.OnPreRender(e);
        }

        private void BindProjectStepsSelectBox()
        {
            if (CrntProjectId != Guid.Empty)
            {
                if (CrntProjectId.ToString() == "11111111-1111-1111-1111-111111111111")
                {
                    // Global Variables
                    drpProjectStepList.Items.Clear();
                    Session["CurrentProjectStepType"] = "GlobalVariables";

                    drpProjectStepList.Items.Clear();
                    drpProjectStepList.Items.Add("Select One");

                    SolutionModel slnModel = GetSolutionModel();
                    foreach (VariableModel var in slnModel.GlobalVariables)
                    {
                        drpProjectStepList.Items.Add(var.Name);
                    }

                    Session["Loaded-drpProjectStepList"] = CrntProjectId;
                }
                else
                {
                    if (Session["Loaded-drpProjectStepList"] == null || (Guid)Session["Loaded-drpProjectStepList"] != CrntProjectId)
                    {
                        SolutionModel slnModel = GetSolutionModel();

                        ProjectModel prjModel = slnModel.Projects.FirstOrDefault(p => p.Id == CrntProjectId);
                        lblProjectName.Text = prjModel.Name;

                        drpProjectStepList.Items.Clear();
                        drpProjectStepList.Items.Add("Select One");
                        foreach (ProjectStepModel step in prjModel.ProjectSteps)
                        {
                            drpProjectStepList.Items.Add(step.Name);
                        }
                        Session["Loaded-drpProjectStepList"] = CrntProjectId;
                    }
                }
            }
        }

        protected void drpProjectStep_OnChange(object sender, EventArgs e)
        {
            string stepName = drpProjectStepList.SelectedItem.Value;
            if (ViewingGlobalVariables())
            {
                Session["CurrentProjectStepType"] = "GlobalVariable";
                Session["StepName"] = stepName;
            }
            else
            {
                ProjectModel prjModel = GetCurrentProjectModel();
                ProjectStepModel stepModel = prjModel.ProjectSteps.FirstOrDefault(s => s.Name == stepName);
                if (stepModel != null)
                {
                    CrntStepId = stepModel.Id;
                    Session["CurrentProjectStepType"] = stepModel.StepType;
                }
            }
        }
    }
}