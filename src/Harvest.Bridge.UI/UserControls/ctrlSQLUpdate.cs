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
    public partial class ctrlSQLUpdate : UserControl, MyUserControl
    {
        private Form _frmParent;
        private ProjectStepModel _projectStep;
        private ProjectModel _parentProject;

        public ctrlSQLUpdate()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntFrm)
        {
            _frmParent = prntFrm;
            _projectStep = obj as ProjectStepModel;
            lblControlTitle.Text = _projectStep.StepType;
            chkEnabled.Checked = _projectStep.Enabled;
            txtName.Text = _projectStep.Name;
            txtDescription.Text = _projectStep.Description;
            txtSQL.Text = _projectStep.SQLText;
            cmbTargetDB.Text = _projectStep.DatabaseTarget;
            cmbSourceDB.Text = _projectStep.DatabaseSource;
            _parentProject = _projectStep.ParentProject;
        }

        public void Save()
        {
            _projectStep.Enabled = chkEnabled.Checked;
            _projectStep.Name = txtName.Text;
            _projectStep.Description = txtDescription.Text;
            _projectStep.SQLText = txtSQL.Text;
            _projectStep.DatabaseTarget = cmbTargetDB.Text;
            _projectStep.DatabaseSource= cmbSourceDB.Text;
        }

        private void cmbSourceDB_DropDown(object sender, EventArgs e)
        {
            cmbSourceDB.Items.Clear();
            cmbSourceDB.Items.Add("none");
            cmbSourceDB.Items.Add("SourceDB");
            cmbSourceDB.Items.Add("StagingDB");
            cmbSourceDB.Items.Add("TargetDB");

            foreach (var proj in _parentProject.ProjectSteps)
            {
                if (string.IsNullOrEmpty(proj.VariableName) == false)
                {
                    cmbSourceDB.Items.Add($"InMemory:{proj.VariableName}");
                }
            }

        }
    }
}
