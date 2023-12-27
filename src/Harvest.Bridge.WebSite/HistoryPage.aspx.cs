using Harvest.Bridge.Common.Models.History;
using Harvest.Bridge.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite
{
    public partial class HistoryPage : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageRequiredAuthentication();
            if (IsPostBack == false)
            {
                LoadHistorySelection();
            }
        }

        private void LoadHistorySelection()
        {
            DALLogHistory logHistory = new DALLogHistory();
            List<HistoryModel> historyModels = logHistory.GetHistory(250);

            drpRunHistory.Items.Clear();
            drpRunHistory.Items.Add("Select One");
            foreach (HistoryModel m in historyModels)
            {
                ListItem item = new ListItem(m.Status + "-" + m.StartDateTime.ToString(), m.Id.ToString());
                drpRunHistory.Items.Add(item);
            }
            if (Request.QueryString["id"] != null) 
            {
                drpRunHistory.SelectedValue = Request.QueryString["id"];
                drpRunHistory_SelectedIndexChanged(null, null);
            }

        }

        private void PopulateDetailTable(Guid id)
        {
            DALLogHistory logHistory = new DALLogHistory();
            List<HistoryStepModel> stepList = logHistory.GetProcessStepHistory(id);
            rptRunHistory.DataSource = stepList;
            rptRunHistory.DataBind();

            grdHistoryImportCounts.DataSource = logHistory.GetRowCountHistory(id);
            grdHistoryImportCounts.DataBind();
        }

        protected void drpRunHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpRunHistory.SelectedValue.Length > 0)
            {
                Guid guid = Guid.Empty;
                if (Guid.TryParse(drpRunHistory.SelectedItem.Value, out guid))
                {
                    PopulateDetailTable(guid);
                }
            }

        }
    }
}