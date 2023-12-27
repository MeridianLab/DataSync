using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.DBStore;
using Harvest.Bridge.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Editor
{
    public partial class MainView : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageRequiredAuthentication();

            var solutionView = LoadControl("Ctrls/ctrlSolutionView.ascx") as Ctrls.ctrlSolutionView;
            pnlSolutionView.Controls.Add(solutionView);

            if(Session["SolutionId"] == null)
            {
                ProjectStepsList.Visible = false;
            }
            else
            {
                ProjectStepsList.Visible=true;
            }

            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Session["CurrentProjectStepType"] != null)
            {
                pnlStepView.Controls.Clear();
                string stepType = Session["CurrentProjectStepType"].ToString();
                switch (stepType)
                {
                    case "Sync":
                        var ctrlDataSync = LoadControl("Ctrls/ctrlProjectStepDataSync.ascx") as Ctrls.ctrlProjectStepDataSync;
                        pnlStepView.Controls.Add(ctrlDataSync);
                        break;
                    case "SQLRead":
                        var ctrlRead = LoadControl("Ctrls/ctrlProjectStepRead.ascx") as Ctrls.ctrlProjectStepRead;
                        pnlStepView.Controls.Add(ctrlRead);
                        break;
                    case "SQLUpdate":
                        var ctrUpdate = LoadControl("Ctrls/ctrlProjectStepUpdate.ascx") as Ctrls.ctrlProjectStepUpdate;
                        pnlStepView.Controls.Add(ctrUpdate);
                        break;
                    case "StepFlow":
                        var ctrStepFlow = LoadControl("Ctrls/ctrlProjectStepFlow.ascx") as Ctrls.ctrlProjectStepFlow;
                        pnlStepView.Controls.Add(ctrStepFlow);
                        break;
                    case "RemoveVariable":
                        var ctrlRemoveVariable = LoadControl("Ctrls/ctrlProjectStepVariableRemove.ascx") as Ctrls.ctrlProjectStepVariableRemove;
                        pnlStepView.Controls.Add(ctrlRemoveVariable);
                        break;
                    case "GlobalVariable":
                        var ctrlVariables = LoadControl("Ctrls/ctrlVariable.ascx") as Ctrls.ctrlVariable;
                        pnlStepView.Controls.Add(ctrlVariables);
                        break;
                    default:
                        break;
                }
            }
            base.OnPreRender(e);
        }

        private void BindData()
        {
            List<JSONStoreModel> jSONStoreModels = new JSONStoreWorker().GetAvailableList("StagingDB");
            drpSolution.DataSource = jSONStoreModels;
            drpSolution.DataTextField = "Name";
            drpSolution.DataValueField = "Id";
            drpSolution.DataBind();
            drpSolution.Items.Insert(0, "select one");           
        }

        protected void drpProjectSelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSolution.SelectedItem.Text == "BridgeSyncProject 001")
            {                
                Guid id = Guid.Parse(drpSolution.SelectedItem.Value);             
                Session["SolutionId"] = id;
            }
        }
    }
}