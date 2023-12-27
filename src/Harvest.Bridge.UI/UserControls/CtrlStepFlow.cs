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
    public partial class CtrlStepFlow : UserControl, MyUserControl
    {
        private Form _frmParent;
        private ProjectStepModel _projectStepFlow;
        private ProjectModel _parentProject;

        public CtrlStepFlow()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntForm)
        {
            _frmParent = prntForm;
            _projectStepFlow = obj as ProjectStepModel;
            lblControlTitle.Text = _projectStepFlow.StepType;
            chkEnabled.Checked = _projectStepFlow.Enabled;
            txtName.Text = _projectStepFlow.Name;
            txtDescription.Text = _projectStepFlow.Description;
            txtSQL.Text = _projectStepFlow.SQLText;
            cmbSourceDB.Text = _projectStepFlow.DatabaseSource;
            cmbExpression.Text = _projectStepFlow.Expression;
            cmbResultAction.Text = _projectStepFlow.ExpressionAction;
            _parentProject = _projectStepFlow.ParentProject;
        }

        public void Save()
        {
            _projectStepFlow.Enabled = chkEnabled.Checked;
            _projectStepFlow.Name = txtName.Text;
            _projectStepFlow.Description = txtDescription.Text;
            _projectStepFlow.SQLText = txtSQL.Text;
            _projectStepFlow.DatabaseSource = cmbSourceDB.Text;
            _projectStepFlow.Expression = cmbExpression.Text;
            _projectStepFlow.ExpressionAction = cmbResultAction.Text;
        }
    }
}
