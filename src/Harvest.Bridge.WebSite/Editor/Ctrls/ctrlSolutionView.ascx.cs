using Harvest.Bridge.Common.Models;
using Harvest.Bridge.WebSite.Editor.EditorBase;
using System;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor.Ctrls
{
    public partial class ctrlSolutionView : EditorControls
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lstProjectView_OnIndexChange(object sender, EventArgs e)
        {
            CrntProjectId = Guid.Parse(lstProjectView.SelectedItem.Value);
            Session.Remove("ProjectStepId");
            Session.Remove("CurrentProjectStepType");
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (HasSelectedSolution)
            {
                BindControls();
            }
            base.OnPreRender(e);
        }

        private void BindControls()
        {
            SolutionModel solutionModel = GetSolutionModel();

            lstProjectView.DataTextField = "Name";
            lstProjectView.DataValueField = "Id";
            lstProjectView.DataSource = solutionModel.Projects;
            lstProjectView.DataBind();

            lstProjectView.Items.Insert(0, new ListItem("Global Variables", "11111111-1111-1111-1111-111111111111"));
        }

    }
}