using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.History;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Harvest.Bridge.UI.UserControls
{
    public partial class ctrlSolutionEditor : UserControl, MyUserControl
    {
        private SolutionModel _solutionModel;
        private IFormMain _parentFrm;
        private bool _IsRunningTest = false;
        private SolutionWorker _solutionWorker;
        public ctrlSolutionEditor()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntForm)
        {
            _parentFrm = prntForm as IFormMain;
            _solutionModel = obj as SolutionModel;

            LoadListBox();
        }

        private void LoadListBox()
        {
            lstProjectSequence.Items.Clear();
            foreach (ProjectModel prj in _solutionModel.Projects)
            {
                lstProjectSequence.Items.Add(prj.Name);
            }
        }

        public void Save()
        {
            // throw new NotImplementedException();
        }

        private void btnSequenceUp_Click(object sender, EventArgs e)
        {
            int indexOf = lstProjectSequence.SelectedIndex;
            if (indexOf > 0)
            {
                string projectName = lstProjectSequence.SelectedItem.ToString();
                lstProjectSequence.Items.RemoveAt(indexOf);
                lstProjectSequence.Items.Insert(indexOf - 1, projectName);
                lstProjectSequence.SelectedIndex = indexOf - 1;

                btnApplySequenceUpdate.Enabled = true;
            }
        }

        private void btnSequenceDown_Click(object sender, EventArgs e)
        {
            int indexOf = lstProjectSequence.SelectedIndex;
            if (indexOf < lstProjectSequence.Items.Count - 1)
            {
                string projectName = lstProjectSequence.SelectedItem.ToString();
                lstProjectSequence.Items.RemoveAt(indexOf);
                lstProjectSequence.Items.Insert(indexOf + 1, projectName);
                lstProjectSequence.SelectedIndex = indexOf + 1;

                btnApplySequenceUpdate.Enabled = true;
            }
        }

        private void btnApplySequenceUpdate_Click(object sender, EventArgs e)
        {
            int pos = 0;
            foreach (string projName in lstProjectSequence.Items)
            {
                int crntIndex = _solutionModel.Projects.FindIndex(p => p.Name == projName);
                if (crntIndex < 0)
                {
                    // Add New Project
                    ProjectModel newProj = new ProjectModel() { Name = projName };
                    _solutionModel.Projects.Insert(pos, newProj);
                }
                else if (crntIndex != pos)
                {
                    // Move Project
                    ProjectModel tmp = _solutionModel.Projects[crntIndex];
                    _solutionModel.Projects.Remove(tmp);
                    _solutionModel.Projects.Insert(pos, tmp);
                }
                pos++;
            }
            _parentFrm.ReloadTreeView();
        }

        private void txtNewProjectName_TextChanged(object sender, EventArgs e)
        {
            btnAddProject.Enabled = txtNewProjectName.Text.Length > 3;
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            lstProjectSequence.Items.Add(txtNewProjectName.Text);
            btnApplySequenceUpdate.Enabled = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text != "Cancel Run")
            {
                DALConfiguration configuration = new DALConfiguration();

                _solutionModel.DefaultUpdateActionUpdateAll = chkDefaultActionUpdateAll.Checked;
                DateTime dtPrevRunTime = DateTime.Now;
                int tm = 0;
                bool preDeleteData = false;
                if (cmbTimeFrameType.Text == "Minutes")
                {
                    if (int.TryParse(txtTimeFrame.Text, out tm))
                    {
                        dtPrevRunTime = dtPrevRunTime.AddMinutes(-tm);
                    }
                }
                else if (cmbTimeFrameType.Text == "Date")
                {
                    if (DateTime.TryParse(txtTimeFrame.Text, out dtPrevRunTime))
                    {
                        configuration.UpdateConfig("Import.RunForSingleDay", "True");
                    }
                    else
                    {
                        MessageBox.Show("Specified time frame type 'Date', however the valued entered '" + txtTimeFrame.Text + "'is not a valid date, unable to continue");
                        txtTimeFrame.Focus();
                        return;
                    }
                }
                else
                {
                    if (int.TryParse(txtTimeFrame.Text, out tm))
                    {
                        dtPrevRunTime = dtPrevRunTime.AddDays(-tm);
                        preDeleteData = true;
                    }
                }

                configuration.UpdateConfig("Import.DataFromDateTime", dtPrevRunTime.ToString());
                configuration.UpdateConfig("PreDeleteDataOnNextRun", preDeleteData.ToString());

                btnRun.Text = "Cancel Run";
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                btnRun.Text = "Test Run";
                _solutionModel.CancelRun = true;
                _IsRunningTest = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_IsRunningTest == false)
            {
                _IsRunningTest = true;
                _solutionModel.CancelRun = false;
                _solutionModel.VariableValues = null;
                _solutionWorker = new SolutionWorker(_solutionModel);
                _solutionWorker.ProcessSolution();
                //foreach (ProjectModel proj in solutionWorker.SolutionModel.Projects)
                //{
                //    proj.Reset();
                //    lblCrntProject.Invoke((MethodInvoker)delegate ()
                //    {
                //        lblCrntProject.Text = proj.Name;
                //    });
                //    solutionWorker.ProcessSolution(proj.Name);
                //}
                // _solutionModel.cur.Reset();
                //solutionWorker.ProcessProject(_projectModel.Name);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _IsRunningTest = false;
            btnRun.Text = "Test Run";
            _solutionWorker = null;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tmrHistoryGrid.Enabled = false;
            if (tabControl1.SelectedIndex == 2)
            {
                LoadConfigurationTable();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                LoadHistoryGrid();
                tmrHistoryGrid.Enabled = true;
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                LoadSQLTokenReplacement();
            }
        }

        private void LoadSQLTokenReplacement()
        {
            dtgSQLTokenReplacement.DataSource = null;
            Sync.Util.SQLTokenReplacer tokenReplacer = new Sync.Util.SQLTokenReplacer();
            tokenReplacer.TokenReplacementList.Add(new SQLTokenReplacementModel());
            dtgSQLTokenReplacement.DataSource = tokenReplacer.TokenReplacementList;
            dtgSQLTokenReplacement.Refresh();
        }

        private void btnSaveSQLTokenRepl_Click(object sender, EventArgs e)
        {
            if (dtgSQLTokenReplacement.DataSource != null)
            {
                List<SQLTokenReplacementModel> tokenReplacement = (List<SQLTokenReplacementModel>)dtgSQLTokenReplacement.DataSource;
                tokenReplacement.RemoveAll(tr => tr.OldValue == null);
                new Sync.Util.SQLTokenReplacer().SaveTokenReplacement(tokenReplacement);
                LoadSQLTokenReplacement();
            }
        }

        private void btnSaveConfigChanges_Click(object sender, EventArgs e)
        {
            DataTable dt = dtgConfiguration.DataSource as DataTable;
            if (dt != null)
            {
                new DALConfiguration().SaveChanges(dt);
                LoadConfigurationTable();
            }
        }

        private void LoadConfigurationTable()
        {
            dtgConfiguration.DataSource = null;
            dtgConfiguration.DataSource = new DALConfiguration().GetConfigurationData();
            dtgConfiguration.Refresh();
        }

        private void LoadHistoryGrid()
        {
            dtgHistory.DataSource = null;
            List<HistoryModel> hList = new DALLogHistory().GetHistory();
            dtgHistory.DataSource = hList;
            if (hList.Count > 0)
            {
                LoadHistoryDetailsGrids(hList[0].Id);
            }
        }

        private void dtgHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                List<HistoryModel> hList = dtgHistory.DataSource as List<HistoryModel>;
                LoadHistoryDetailsGrids(hList[e.RowIndex].Id);
            }
        }

        private void LoadHistoryDetailsGrids(Guid id)
        {
            DALLogHistory logHistory = new DALLogHistory();
            dtgHistorySteps.DataSource = null;
            dtgHistorySteps.DataSource = logHistory.GetProcessStepHistory(id);
            dtgHistorySteps.Refresh();
            dtgHistoryImportCounts.DataSource = null;
            dtgHistoryImportCounts.DataSource = logHistory.GetRowCountHistory(id);
            dtgHistoryImportCounts.Refresh();
        }

        private void btnHistoryRefresh_Click(object sender, EventArgs e)
        {
            LoadHistoryGrid();
        }

        private void tmrHistoryGrid_Tick(object sender, EventArgs e)
        {
            // LoadHistoryGrid();
        }
    }
}
