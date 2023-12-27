using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Sync;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Harvest.Bridge.UI.UserControls
{
    public partial class ctrlProjectEditor : UserControl, MyUserControl
    {
        private IFormMain _frmParent;
        private SolutionModel _solutionModel;
        private ProjectModel _projectModel;
        private bool _IsRunningTest = false;
        public ctrlProjectEditor()
        {
            InitializeComponent();
        }

        public void InitControl(object obj, Form prntForm)
        {
            _frmParent = prntForm as IFormMain;
            _projectModel = obj as ProjectModel;
            _solutionModel = _projectModel.ParentSolution;
            txtName.Text = _projectModel.Name;
            chkEnabled.Checked= _projectModel.Enabled;
            txtDescription.Text = _projectModel.Description;
            chkAllToProcessParallel.Checked = _projectModel.AllowParallelProcessing;
            chkParallelForceSync.Checked = _projectModel.ParallelForceSync;

            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            BindListProjectSteps();
        }

        private void BindListProjectSteps()
        {
            lstProjectSteps.Items.Clear();
            foreach (ProjectStepModel psm in _projectModel.ProjectSteps)
            {
                lstProjectSteps.Items.Add(psm);
            }
        }

        public void Save()
        {
            _projectModel.Name = txtName.Text;
            _projectModel.Enabled= chkEnabled.Checked;
            _projectModel.Description = txtDescription.Text;
            _projectModel.AllowParallelProcessing= chkAllToProcessParallel.Checked;
            _projectModel.ParallelForceSync= chkParallelForceSync.Checked;
        }

        private void btnAddStep_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNewStepType.Text))
            {
                MessageBox.Show("Please select step type");
            }
            else
            {
                ProjectStepModel projectStep = new ProjectStepModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "<New>",
                    StepType= cmbNewStepType.Text
                };
                _projectModel.ProjectSteps.Add(projectStep);
                _frmParent.AddProjectItem(projectStep);
                _frmParent.ReloadTreeView();
            }
        }

        private void btnAddVariable_Click(object sender, System.EventArgs e)
        {
            VariableModel model = new VariableModel()
            {
                Id = Guid.NewGuid(),
                Name = "<New>",
                SourceType = "Value",
                ValueType = "String"
            };
            _projectModel.ProcessVariables.Add(model);
            _frmParent.AddProjectVariable(_projectModel.Id.ToString(), model);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _frmParent.CrntTreeNode.Text = txtName.Text;
        }

        private void btnSequenceUp_Click(object sender, EventArgs e)
        {
            int indexOf = _projectModel.ProjectSteps.IndexOf(lstProjectSteps.SelectedItem as ProjectStepModel);
            if (indexOf > 0)
            {
                _projectModel.ProjectSteps.RemoveAt(indexOf);
                _projectModel.ProjectSteps.Insert(indexOf - 1, lstProjectSteps.SelectedItem as ProjectStepModel);
                BindListProjectSteps();
                lstProjectSteps.SelectedIndex = indexOf - 1;
            }
        }

        private void btnSequenceDown_Click(object sender, EventArgs e)
        {
            int indexOf = _projectModel.ProjectSteps.IndexOf(lstProjectSteps.SelectedItem as ProjectStepModel);
            if (indexOf < _projectModel.ProjectSteps.Count - 1)
            {
                _projectModel.ProjectSteps.RemoveAt(indexOf);
                _projectModel.ProjectSteps.Insert(indexOf + 1, lstProjectSteps.SelectedItem as ProjectStepModel);
                BindListProjectSteps();
                lstProjectSteps.SelectedIndex = indexOf + 1;
            }
        }

        private void btnSaveReload_Click(object sender, EventArgs e)
        {
            _frmParent.SaveAndReloadProject();
        }

        private void btnTestProject_Click(object sender, EventArgs e)
        {
            if (btnTestProject.Text != "Cancel Run")
            { // Run Start
                DALConfiguration dalConfig = new DALConfiguration();
                DateTime dtPrevRunTime = DateTime.Now;
                int tm = 0;
                if (cmbTimeFrameType.Text == "Minutes")
                {
                    if (int.TryParse(txtTimeFrame.Text, out tm))
                    {
                        dtPrevRunTime = dtPrevRunTime.AddMinutes(-tm);
                    }
                }
                else if(cmbTimeFrameType.Text == "Date")
                {
                    if(DateTime.TryParse(txtTimeFrame.Text, out dtPrevRunTime))
                    {
                        dalConfig.UpdateConfig("Import.RunForSingleDay", "True");
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
                    }
                }
                new DALConfiguration().UpdateConfig("Import.DataFromDateTime", dtPrevRunTime.ToString());

                _renderedLogItemPos = -1;
                rtfStepLog.Clear();
                btnTestProject.Text = "Cancel Run";
                tmrProgress.Enabled = true;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                btnTestProject.Text = "Test Run";
                _projectModel.ParentSolution.CancelRun = true;
                tmrProgress.Enabled = false;
                _IsRunningTest = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_IsRunningTest == false)
            {
                _IsRunningTest = true;
                _projectModel.ParentSolution.CancelRun = false;
                _solutionModel.VariableValues = null;
                SolutionWorker solutionWorker = new SolutionWorker(_solutionModel);
                _projectModel.Reset();
                solutionWorker.ProcessProject(_projectModel.Name);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _IsRunningTest = false;
            btnTestProject.Text = "Test Run";
            tmrProgress.Enabled = false;
            lstProjectSteps.ClearSelected();
        }

        int _renderedLogItemPos = -1;
        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach(ProjectStepModel ps in _projectModel.ProjectSteps)
            {
                if (ps != null && ps.LogDetails != null)
                {
                    foreach (var logDetail in ps.LogDetails)
                    {
                        cnt++;
                        if (cnt > _renderedLogItemPos)
                        {
                            rtfStepLog.AppendText(logDetail.ToString());
                        }
                    }
                }
            }
            _renderedLogItemPos= cnt;
        }
    }
}
