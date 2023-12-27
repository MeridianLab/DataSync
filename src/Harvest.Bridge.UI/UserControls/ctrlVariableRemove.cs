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
    public partial class ctrlVariableRemove : UserControl, MyUserControl
    {
        private Form _frmParent;
        private ProjectStepModel _projectStepModel;
        private ProjectModel _parentProject;
        public ctrlVariableRemove()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntFrm)
        {
            _frmParent = prntFrm;
            _projectStepModel = obj as ProjectStepModel;
            txtName.Text = _projectStepModel.Name;
            cmdVariables.Text = _projectStepModel.VariableName;
            txtDescription.Text = _projectStepModel.Description;
            _parentProject = _projectStepModel.ParentProject;
        }

        private void ctrlVariableRemove_Load(object sender, EventArgs e)
        {
            _projectStepModel.Name= txtName.Text;
            _projectStepModel.Description= txtDescription.Text;
        }

        private void cmdVariables_DropDown(object sender, EventArgs e)
        {
            cmdVariables.Items.Clear();

            foreach (var proj in _parentProject.ProjectSteps)
            {
                if (string.IsNullOrEmpty(proj.VariableName) == false)
                {
                    cmdVariables.Items.Add($"InMemory: {proj.VariableName}");
                }
            }

        }

        public void Save()
        {
            _projectStepModel.Name = txtName.Text;
            _projectStepModel.Description = txtDescription.Text;
            _projectStepModel.VariableName = cmdVariables.Text;
        }
    }
}
