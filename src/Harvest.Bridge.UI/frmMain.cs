using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Logger;
using Harvest.Bridge.Sync;
using Harvest.Bridge.UI.UIHelpers;
using Harvest.Bridge.UI.UserControls;
using Harvest.Bridge.Util;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Harvest.Bridge.UI
{
    public partial class frmMain : Form, IFormMain
    {
        private SolutionModel _bridgeSolutionModel;
        private TreeNode _crntTreeNode;
        private MyUserControl _userControl;

        public TreeNode CrntTreeNode
        {
            get { return _crntTreeNode; }
            set { _crntTreeNode = value; }
        }

        public SolutionModel SolutionModel
        {
            get { return _bridgeSolutionModel; }
            set { _bridgeSolutionModel = value; }
        }

        public frmMain()
        {
            InitializeComponent();
            BridgeLog.SetRTFReference(rtfLog);
        }

        public void AddProjectItem(object newObject)
        {
            ShowStepUserControl(newObject);
        }

        private void trvProjectView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            spltMainContent.Panel2.Controls.Clear();
            if (_crntTreeNode != null) { _crntTreeNode.BackColor = Color.Empty; }
            if(_userControl != null) { _userControl.Save(); _userControl = null; }
            _crntTreeNode = e.Node;
            _crntTreeNode.BackColor = Color.Silver;
            if (e.Node.Tag != null)
            {
                ShowStepUserControl(e.Node.Tag);
            }
        }

        private void ShowStepUserControl(object projectStep)
        {
            switch (projectStep.GetType().Name)
            {
                case "SolutionModel":
                    _userControl = new ctrlSolutionEditor();
                    _userControl.InitControl(projectStep as SolutionModel, this);
                    break;
                case "VariableModel":
                    _userControl = new ctrlVariable();
                    _userControl.InitControl(projectStep as VariableModel, this);
                    break;
                case "ProjectModel":
                    _userControl = new ctrlProjectEditor();
                    _userControl.InitControl(projectStep as ProjectModel, this);
                    break;
                case "ProjectStepModel":
                    ProjectStepModel step = projectStep as ProjectStepModel;
                    if (step.StepType == "SQLUpdate" || step.StepType == "SyncUpdate")
                    {
                        _userControl = new ctrlSQLUpdate();
                        _userControl.InitControl(step, this);
                    }
                    else if (step.StepType == "SQLRead")
                    {
                        _userControl = new ctrlSQLRead();
                        _userControl.InitControl(step, this);
                    }
                    else if (step.StepType == "Sync")
                    {
                        _userControl = new ctrlDataSync();
                        _userControl.InitControl(step, this);
                    }
                    else if(step.StepType == "StepFlow")
                    {
                        _userControl = new CtrlStepFlow();
                        _userControl.InitControl(step, this);
                    }
                    else if (step.StepType == "RemoveVariable")
                    {
                        _userControl = new ctrlVariableRemove();
                        _userControl.InitControl(step, this);
                    }
                    break;
            }
            if (_userControl != null)
            {
                spltMainContent.Panel2.Controls.Clear();
                spltMainContent.Panel2.Controls.Add((UserControl)_userControl);
                ((UserControl)_userControl).Dock = DockStyle.Fill;
            }
        }

        #region Helper/Test Code
        private void testLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BridgeLog.Debug("This is a test Debug log line");
            BridgeLog.Info("This is a test Info log line");
            BridgeLog.Warning("This is a test Warning log line");
            BridgeLog.Error("This is a test Error log line");
        }
        #endregion

        public void AddVariableGlobalVariable(VariableModel newObject)
        {
            TreeNode gNode = CrntTreeNode;
            if (CrntTreeNode.Text != "Global Variables")
            {
                gNode = CrntTreeNode.Nodes[0];
            }
            TreeNode gItemNode = new TreeNode(newObject.Name);
            gItemNode.Name = newObject.Id.ToString();
            gItemNode.Text = newObject.Name;
            gItemNode.Tag = newObject;
            gNode.Nodes.Add(gItemNode);
            TreeViewEventArgs e = new TreeViewEventArgs(gItemNode);
            trvProjectView.SelectedNode = gItemNode;
        }

        public void AddProjectVariable(string parentId, VariableModel newObject)
        {
            TreeNode node = CrntTreeNode;
            if (CrntTreeNode.Text != "Variables")
            {
                node = CrntTreeNode.Nodes[0];
            }
            TreeNode newNode = new TreeNode(newObject.Name);
            newNode.Name = newObject.Id.ToString();
            newNode.Text = newObject.Name;
            newNode.Tag = newObject;
            node.Nodes.Add(newNode);
            TreeViewEventArgs e = new TreeViewEventArgs(newNode);
            trvProjectView.SelectedNode = newNode;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtfLog.Clear();
        }

        #region Drag and Drop
        private void trvProjectView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void trvProjectView_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.  
            Point targetPoint = trvProjectView.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.  
            TreeNode targetNode = trvProjectView.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.  
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not   
            // the dragged node or a descendant of the dragged node.  
            if (!draggedNode.Equals(targetNode))
            {
                // If it is a move operation, remove the node from its current   
                // location and add it to the node at the drop location.  
                //if (e.Effect == DragDropEffects.Move)
                //{
                //    draggedNode.Remove();
                //    targetNode.Nodes.Add(draggedNode);
                //}

                // If it is a copy operation, clone the dragged node   
                // and add it to the node at the drop location.  
                if (e.Effect == DragDropEffects.Copy)
                {
                    if (targetNode.Tag != null &&
                        targetNode.Tag.GetType().Name == "ProjectModel")
                    {
                        if (MessageBox.Show($"Copy {draggedNode.Text} to project {targetNode.Text}", "Copy Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ProjectModel targetProjModel = targetNode.Tag as ProjectModel;
                            string json = JsonCommonHelper.SerializeObject(draggedNode.Tag);
                            ProjectStepModel newStepModel = (ProjectStepModel)JsonCommonHelper.DeserializeObject(json, typeof(ProjectStepModel));
                            newStepModel.Id = Guid.NewGuid();

                            targetProjModel.ProjectSteps.Add(newStepModel);
                            ReloadTreeView();
                        }
                    }
                    else if(targetNode.Text == "Variables" && 
                        draggedNode.Tag != null && draggedNode.Tag.GetType().Name == "VariableModel")
                    {
                        ProjectModel targetProjModel = targetNode.Parent.Tag as ProjectModel;
                        string json = JsonCommonHelper.SerializeObject(draggedNode.Tag);
                        VariableModel newVariableModel = (VariableModel)JsonCommonHelper.DeserializeObject(json, typeof(VariableModel));
                        newVariableModel.Id = Guid.NewGuid();

                        if(targetProjModel.ProcessVariables == null) { targetProjModel.ProcessVariables = new System.Collections.Generic.List<VariableModel>(); }
                        targetProjModel.ProcessVariables.Add(newVariableModel);
                        ReloadTreeView();

                    }
                }

                // Expand the node at the location   
                // to show the dropped node.  
                targetNode.Expand();
            }
        }

        private void trvProjectView_DragLeave(object sender, EventArgs e)
        {

        }

        private void trvProjectView_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.  
            Point targetPoint = trvProjectView.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.  
            trvProjectView.SelectedNode = trvProjectView.GetNodeAt(targetPoint);
        }

        private void trvProjectView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.  
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // Copy the dragged node when the right mouse button is used.  
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }
        #endregion

        #region Load and Save Methods

        #region File System
        public void SaveAndReloadProject()
        {
            LoadProjectFromJSON.SaveSolutionToFile(_bridgeSolutionModel.SolutionFileName, _bridgeSolutionModel);
            _bridgeSolutionModel = LoadProjectFromJSON.LoadProject(_bridgeSolutionModel.SolutionFileName);
            new TreeViewHelper().LoadTreeView(trvProjectView, _bridgeSolutionModel, this);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAndReloadProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Files (*.json)|*.json";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
            {
                try
                {
                    _bridgeSolutionModel = LoadProjectFromJSON.LoadProject(openFileDialog.FileName);
                    this.Text = "Loaded File " + openFileDialog.FileName;
                    new TreeViewHelper().LoadTreeView(trvProjectView, _bridgeSolutionModel, this);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_bridgeSolutionModel != null)
            {
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.FileName = _bridgeSolutionModel.SolutionFileName;
                saveFileDialog.Filter = "Files (*.json)|*.json";
                DialogResult dialog = saveFileDialog.ShowDialog();
                if (dialog.Equals(DialogResult.OK))
                {
                    _bridgeSolutionModel.SolutionFileName = Path.GetFileName(saveFileDialog.FileName);
                    LoadProjectFromJSON.SaveSolutionToFile(saveFileDialog.FileName, _bridgeSolutionModel);
                }
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = _bridgeSolutionModel.SolutionFileName.Replace(".json", $"_backup_{System.DateTime.Now.Ticks}.json");
            LoadProjectFromJSON.SaveSolutionToFile(fileName, _bridgeSolutionModel);
        }

        #endregion
        public void ReloadTreeView()
        {
            new TreeViewHelper().LoadTreeView(trvProjectView, _bridgeSolutionModel, this);
            this.Text= _bridgeSolutionModel.SolutionName + "\t" + "Source: " + _bridgeSolutionModel.SolutionSource;
        }

        #region Database Save Methods
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDatabaseOpen frmDatabaseOpen = new frmDatabaseOpen(this);
            frmDatabaseOpen.ShowDialog();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDatabaseSave frmDatabaseSave = new frmDatabaseSave(this, _bridgeSolutionModel);
            frmDatabaseSave.ShowDialog();
        }
        #endregion

        #endregion

        private void dataCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataCompare.frmDataCompare frmData = new DataCompare.frmDataCompare();
            frmData.Show();
        }
    }
}
