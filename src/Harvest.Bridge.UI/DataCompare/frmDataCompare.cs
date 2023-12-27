using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harvest.Bridge.UI.DataCompare
{
    public partial class frmDataCompare : Form
    {
        public frmDataCompare()
        {
            InitializeComponent();
            DefaultSQL();
        }

        private void DefaultSQL()
        {
            txtSQL.Text =
@"EXEC    [dbo].[spAllPatientsComplete_JB]
@locationName = N'CFKC - DOWNTOWN',
@drawDF = N'drawDFParam',
@drawDT = N'drawDTParam'";
            txtSQL.Text = txtSQL.Text.Replace("drawDFParam", DateTime.Now.AddDays(-2).ToShortDateString());
            txtSQL.Text = txtSQL.Text.Replace("drawDTParam", DateTime.Now.ToShortDateString());
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNewHarvestSQLData = new DAL.DALSQLRead("TargetDB").ReadData(txtSQL.Text);
                DataTable dtDb01HarvestSQLData = new DAL.DALSQLRead("DB01HarvestSQLDB").ReadData(txtSQL.Text);
                lblDB01Stat.Text = $"Row Count: {dtDb01HarvestSQLData.Rows.Count}";
                lblDB01nStat.Text = $"Row Count: {dtNewHarvestSQLData.Rows.Count}";

                dtgDB01Results.DataSource = dtDb01HarvestSQLData;
                dtgDB01Results.Refresh();

                dtgDB01nResults.DataSource = dtNewHarvestSQLData;
                dtgDB01nResults.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
