using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harvest.Bridge.UI.UserControls
{
    public partial class ctrlSQLRead : UserControl, MyUserControl
    {
        private Form _frmParent;
        private ProjectStepModel _projectStep;
        private ProjectModel _parentProject;

        public ctrlSQLRead()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntFrm)
        {
            _frmParent = prntFrm;
            _projectStep = obj as ProjectStepModel;
            chkEnabled.Checked = _projectStep.Enabled;
            txtName.Text = _projectStep.Name;
            txtDescription.Text = _projectStep.Description;
            txtSQL.Text = _projectStep.SQLText;
            cmbSourceDB.Text = _projectStep.DatabaseSource;
            _parentProject = _projectStep.ParentProject;

            if (_projectStep.ResultAction != null)
            {
                cmbResultAction.Text = _projectStep.ResultAction.ResultActionType;
                txtVariableName.Text = _projectStep.ResultAction.VariableName;
            }
        }

        public void Save()
        {
            _projectStep.Enabled = chkEnabled.Checked;
            _projectStep.Name = txtName.Text;
            _projectStep.Description = txtDescription.Text;
            _projectStep.SQLText = txtSQL.Text;
            _projectStep.DatabaseSource = cmbSourceDB.Text;

            if (cmbResultAction.Text == "MemoryStore")
            {
                if (_projectStep.ResultAction == null) { _projectStep.ResultAction = new ResultActionModel(); }
                _projectStep.ResultAction.ResultActionType = cmbResultAction.Text;
                _projectStep.ResultAction.VariableName = txtVariableName.Text;
            }
            else
            {
                _projectStep.ResultAction = null;
            }
        }
    }
}
