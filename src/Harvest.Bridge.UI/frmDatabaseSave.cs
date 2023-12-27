using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
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
    public partial class frmDatabaseSave : Form
    {
        private IFormMain _parentForm;
        private SolutionModel _solutionModel;

        public frmDatabaseSave(frmMain parentForm, SolutionModel solutionModel)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _solutionModel = solutionModel;

            txtName.Text = _solutionModel.SolutionName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            JSONStoreWorker storeWorker = new JSONStoreWorker();
            if (_solutionModel.SolutionName != txtName.Text)
            {
                _solutionModel.Id = Guid.NewGuid();
                _solutionModel.SolutionName = txtName.Text;
            }
            _solutionModel.SolutionSource = cmdDBSource.Text;
            storeWorker.SaveRecord(_solutionModel, cmdDBSource.Text);

            if(chkSetAsRuntimSolution.Checked)
            {
                new DALConfiguration().UpdateConfig("RuntimeSync_ProjectSource", cmdDBSource.Text);
                new DALConfiguration().UpdateConfig("RuntimeSync_ProjectName", txtName.Text);
            }

            _parentForm.ReloadTreeView();

            this.Close();
        }
    }
}
