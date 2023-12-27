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
    public partial class ctrlDataSync : UserControl, MyUserControl
    {
        private Form _frmParent;
        private ProjectStepModel _projectStep;
        private ProjectModel _parentProject;

        public ctrlDataSync()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntFrm)
        {
            _frmParent = prntFrm;
            _projectStep = obj as ProjectStepModel;
            txtName.Text = _projectStep.Name;
            txtDescription.Text = _projectStep.Description;
            cmbSourceDB.Text = _projectStep.DatabaseSource;
            cmbTargetDB.Text = _projectStep.DatabaseTarget;
            
            _parentProject = _projectStep.ParentProject;

            if (_projectStep.DataMap != null)
            {
                txtTablename.Text = _projectStep.DataMap.TableName;
                nudBatchSize.Text = _projectStep.DataMap.BatchSize.ToString();
                dtgMapColumns.DataSource = _projectStep.DataMap.ColumnMapModels;

                txtPrimaryKey.Text = _projectStep.DataMap.PKey.TargetName;
                txtEpochTimeClmn.Text = _projectStep.DataMap.EpochKey.TargetName;

                if(_projectStep.DataMap.FKeys!= null)
                {
                    if (_projectStep.DataMap.FKeys.Count > 0 && _projectStep.DataMap.FKeys[0] != null)
                    {
                        txtFK1Clmn.Text = _projectStep.DataMap.FKeys[0].SourceName;
                        txtFK1ClmnTarget.Text = _projectStep.DataMap.FKeys[0].TargetName;
                    }
                    if (_projectStep.DataMap.FKeys.Count > 1 && _projectStep.DataMap.FKeys[1] != null)
                    {
                        txtFK2Clmn.Text = _projectStep.DataMap.FKeys[1].SourceName;
                        txtFK2ClmnTarget.Text = _projectStep.DataMap.FKeys[1].TargetName;
                    }
                    if (_projectStep.DataMap.FKeys.Count > 2 && _projectStep.DataMap.FKeys[2] != null)
                    {
                        txtFK3Clmn.Text = _projectStep.DataMap.FKeys[2].SourceName;
                        txtFK3ClmnTarget.Text = _projectStep.DataMap.FKeys[2].TargetName;
                    }
                    if (_projectStep.DataMap.FKeys.Count > 3 && _projectStep.DataMap.FKeys[3] != null)
                    {
                        txtFK4Clmn.Text = _projectStep.DataMap.FKeys[3].SourceName;
                        txtFK4ClmnTarget.Text = _projectStep.DataMap.FKeys[3].TargetName;
                    }
                }
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            _projectStep.DataMap.ColumnMapModels.Add(new ColumnMapModel());
            dtgMapColumns.DataSource = null;
            dtgMapColumns.DataSource = _projectStep.DataMap.ColumnMapModels;
            dtgMapColumns.Update();
            dtgMapColumns.Refresh();
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (dtgMapColumns.SelectedRows != null)
            {
                _projectStep.DataMap.ColumnMapModels.RemoveAt(dtgMapColumns.SelectedRows[0].Index);
                dtgMapColumns.DataSource = null;
                dtgMapColumns.DataSource = _projectStep.DataMap.ColumnMapModels;
                dtgMapColumns.Update();
                dtgMapColumns.Refresh();
            }
        }

        private void dtgMapColumns_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbSourceDB_DropDown(object sender, EventArgs e)
        {
            cmbSourceDB.Items.Clear();
            cmbSourceDB.Items.Add("none");
            cmbSourceDB.Items.Add("SourceDB");
            cmbSourceDB.Items.Add("StagingDB");
            cmbSourceDB.Items.Add("TargetDB");

            foreach(var proj in _parentProject.ProjectSteps)
            {
                if(string.IsNullOrEmpty(proj.VariableName) == false)
                {
                    cmbSourceDB.Items.Add($"InMemory:{proj.VariableName}");
                }
            }
        }

        public void Save()
        {
            _projectStep.Name = txtName.Text;
            _projectStep.Description = txtDescription.Text;
            _projectStep.DatabaseSource = cmbSourceDB.Text;
            _projectStep.DatabaseTarget = cmbTargetDB.Text;

            if (_projectStep.DataMap == null) { _projectStep.DataMap = new DataMapModel(); }

            _projectStep.DataMap.TableName = txtTablename.Text;
            _projectStep.DataMap.BatchSize = (int)nudBatchSize.Value;
            _projectStep.DataMap.PKey.TargetName = txtPrimaryKey.Text; _projectStep.DataMap.PKey.SourceName = txtPrimaryKey.Text;
            _projectStep.DataMap.EpochKey.TargetName = txtEpochTimeClmn.Text; _projectStep.DataMap.EpochKey.SourceName = txtEpochTimeClmn.Text;

            _projectStep.DataMap.FKeys = new List<DBRelationShipKeyModel>();
            if (string.IsNullOrWhiteSpace(txtFK1Clmn.Text) == false)
            {
                _projectStep.DataMap.FKeys.Add(new DBRelationShipKeyModel() 
                { 
                    SourceName = txtFK1Clmn.Text, TargetName = txtFK1ClmnTarget.Text 
                });
            }
            if (string.IsNullOrWhiteSpace(txtFK2Clmn.Text) == false)
            {
                _projectStep.DataMap.FKeys.Add(new DBRelationShipKeyModel() { SourceName = txtFK2Clmn.Text, TargetName = txtFK2ClmnTarget.Text });
            }
            if (string.IsNullOrWhiteSpace(txtFK3Clmn.Text) == false)
            {
                _projectStep.DataMap.FKeys.Add(new DBRelationShipKeyModel() { SourceName = txtFK3Clmn.Text, TargetName = txtFK3ClmnTarget.Text });
            }
            if (string.IsNullOrWhiteSpace(txtFK4Clmn.Text) == false)
            {
                _projectStep.DataMap.FKeys.Add(new DBRelationShipKeyModel() { SourceName = txtFK4Clmn.Text, TargetName = txtFK4ClmnTarget.Text });
            }
        }
    }
}
