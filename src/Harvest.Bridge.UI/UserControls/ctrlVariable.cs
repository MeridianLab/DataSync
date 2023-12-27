using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Logger;
using Harvest.Bridge.Sync;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Harvest.Bridge.UI.UserControls
{
    public partial class ctrlVariable : UserControl, MyUserControl
    {
        private IFormMain _frmParent;
        private VariableModel _variableModel;
        private ProjectModel _parentProject;

        public ctrlVariable()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntFrm)
        {
            _frmParent = prntFrm as IFormMain;
            _variableModel = obj as VariableModel;
            txtName.Text = _variableModel.Name;
            txtDescription.Text = _variableModel.Description;
            cmdValueType.SelectedText = _variableModel.SourceType;
            cmbSourceDB.Text = _variableModel.DBSourceName;
            cmbValueType.Text = _variableModel.ValueType;

            _parentProject = _variableModel.ParentProject;

            cmdValueType_SelectedIndexChanged(null, null);
        }

        private void ctrlVariable_Load(object sender, EventArgs e)
        {

        }

        private void cmdValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmdValueType.Text)
            {
                case "GenerateId":
                    txtInputSQL.Visible = false;
                    cmbSourceDB.Visible = false;
                    lblSourceDB.Visible = false;
                    txtValueStr.Text = string.Empty;
                    txtValueStr.Visible = false;
                    lblValueType.Visible = false;
                    cmbValueType.Visible = false;
                    break;
                case "Value":
                case "String":
                case "Numeric":
                    txtValueStr.Visible = true;
                    txtValueStr.Text = _variableModel.Value;
                    txtInputSQL.Text = string.Empty;
                    lblValueType.Visible = true;
                    cmbValueType.Visible = true;

                    txtInputSQL.Visible = false;
                    cmbSourceDB.Visible = false;
                    lblSourceDB.Visible = false;
                    break;
                case "SQL":
                    txtInputSQL.Visible = true;
                    txtInputSQL.Text = _variableModel.Value;
                    cmbSourceDB.Visible = true;
                    lblSourceDB.Visible = true;
                    lblValueType.Visible = true;
                    cmbValueType.Visible = true;

                    txtValueStr.Text = string.Empty;
                    txtValueStr.Visible = false;
                    break;
            }
        }

        private void cmbValueType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Save()
        {
            _variableModel.Name = txtName.Text;
            _variableModel.Description = txtDescription.Text;
            _variableModel.SourceType = cmdValueType.Text;
            if (txtValueStr.Visible)
            {
                _variableModel.Value = txtValueStr.Text;
            }
            else if (txtInputSQL.Visible)
            {
                _variableModel.Value = txtInputSQL.Text;
                _variableModel.DBSourceName = cmbSourceDB.Text;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            lblTestResultValue.Visible = true;
            txtTestResultValue.Visible = true;
            txtTestResultValue.Text = string.Empty;
            Save();
            try
            {
                BridgeLog.Info($"Preparing to process variable '{_variableModel.Name}'");
                object v = null;
                if (_parentProject != null)
                {
                    new VariableWorker(_parentProject.ParentSolution).ProcessVariable(_variableModel);
                    v = _parentProject.ParentSolution.GetVariableValue(_variableModel.Name);
                }
                else
                {
                    new VariableWorker(_variableModel.ParentSolution).ProcessVariable(_variableModel);
                    v = _variableModel.ParentSolution.GetVariableValue(_variableModel.Name);
                }
                BridgeLog.Info($"Resulting variable value '{v}'");
                BridgeLog.Info($"This variable can be used in sql statements referenced like [~{_variableModel.Name}~] which will be merged at run time to [{v}]");
                txtTestResultValue.Text = _variableModel.Value;
            }
            catch (Exception ex)
            {
                BridgeLog.Error(ex.Message);
                txtTestResultValue.Text = "Error:" + ex.Message + Environment.NewLine + "See logs for more information.";
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _frmParent.CrntTreeNode.Text = txtName.Text;
        }
    }
}
