using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.DBStore;
using Harvest.Bridge.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harvest.Bridge.UI
{
    public partial class frmDatabaseOpen : Form
    {
        private frmMain _parentForm;
        public frmDatabaseOpen(frmMain parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            LoadAvailableList();
        }

        private void LoadAvailableList()
        {
            List<JSONStoreModel> jSONStoreModels = new JSONStoreWorker().GetAvailableList(cmdDBSource.Text);
            dtgOpen.DataSource = jSONStoreModels;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dtgOpen.SelectedRows.Count == 1) 
            {
                JSONStoreModel model = dtgOpen.SelectedRows[0].DataBoundItem as JSONStoreModel;
                _parentForm.SolutionModel = new JSONStoreWorker().OpenSolutionFromDB(cmdDBSource.Text, model.Id);
                _parentForm.SolutionModel.SolutionSource = cmdDBSource.Text;
                _parentForm.ReloadTreeView();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
