using Harvest.Bridge.WebSite.DataCompare.CompareLogic;
using Harvest.Bridge.WebSite.DataCompare.DefinedQueries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.DataCompare
{
    public partial class Default : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageRequiredAuthentication();
            if (!IsPostBack)
            {
                drpQueryComparison.Items.Clear();
                drpQueryComparison.Items.Add("none");
                drpQueryComparison.Items.Add("vwTblAllPatientsComplete");
                drpQueryComparison.Items.Add("spAllPatientsComplete_JB");
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNewHarvestSQLData = new DAL.DALSQLRead("TargetDB").ReadData(txtSQL.Text);
                DataTable dtDb01HarvestSQLData = new DAL.DALSQLRead("DB01HarvestSQLDB").ReadData(txtSQL.Text);

                BaseCompareLogic.ShowMessageInfo(ltlInfoMessage, $"Query Complete, {DateTime.Now.ToString()}<br/>DB01 Row Count: {dtDb01HarvestSQLData.Rows.Count}<br/>DB01n Row Count: {dtNewHarvestSQLData.Rows.Count}");

                DataTable dtComparison = BuildComparisonView(dtDb01HarvestSQLData, dtNewHarvestSQLData);
                if (dtComparison != null)
                {
                    grdvDB01.DataSource = dtComparison;
                    grdvDB01.DataBind();
                }
                else
                {
                    grdvDB01.DataSource = dtDb01HarvestSQLData;
                    grdvDB01.DataBind();

                    grdvDB01n.DataSource = dtNewHarvestSQLData;
                    grdvDB01n.DataBind();
                }
            }
            catch (Exception ex)
            {
                BaseCompareLogic.ShowMessageInfo(ltlInfoMessage, "Error: " + ex.Message);
            }
        }

        private DataTable BuildComparisonView(DataTable dtDB01, DataTable dtDb01New)
        {
            DataTable retTable = null;
            switch (drpQueryComparison.SelectedItem.Text)
            {
                case "spAllPatientsComplete_JB":
                    retTable = spAllPatientsComplete_JB.BuildCompareTable(ltlInfoMessage, dtDB01, dtDb01New, chkExcludeMatches.Checked, chkTwoDigitPrecision.Checked);
                    break;
                case "vwTblAllPatientsComplete":
                    retTable = vwTblAllPatientsComplete.BuildCompareTable(ltlInfoMessage, dtDB01, dtDb01New, chkExcludeMatches.Checked, chkTwoDigitPrecision.Checked);
                    break;

            }
            return retTable;
        }

        protected void drpQueryComparison_OnChange(object sender, EventArgs e)
        {
            if(drpQueryComparison.SelectedIndex > 0)
            {
                chkExcludeMatches.Checked = true;
                chkExcludeMatches.Enabled = true;

                switch (drpQueryComparison.SelectedItem.Text)
                {
                    case "spAllPatientsComplete_JB":
                        txtSQL.Text = spAllPatientsComplete_JB.SqlQuery;
                        break;
                    case "vwTblAllPatientsComplete":
                        txtSQL.Text = vwTblAllPatientsComplete.SqlQuery;
                        break;
                    default:
                        chkExcludeMatches.Checked = false;
                        chkExcludeMatches.Enabled = false;
                        break;
                }
            }
        }

    }
}