using Harvest.Bridge.Common.Interfaces;
using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Logger;
using System;
using System.Windows.Forms;

namespace Harvest.Bridge.UI.UIHelpers
{
    internal class TreeViewHelper
    {
        private IFormMain _prntFrm;
        private SolutionModel _solutionModel;
        internal void LoadTreeView(TreeView trvProjectView, SolutionModel solutionModel, Form prntForm)
        {
            _prntFrm = prntForm as IFormMain;
            _solutionModel = solutionModel;
            trvProjectView.Nodes.Clear();
            if (solutionModel != null)
            {
                TreeNode slnNode = new TreeNode("Database Sync Solution");
                slnNode.Tag = solutionModel;
                trvProjectView.Nodes.Add(slnNode);

                TreeNode gNode = new TreeNode("Global Variables");
                AddDAddNewContestMenu(gNode);
                trvProjectView.Nodes.Add(gNode);
                if (solutionModel.GlobalVariables != null)
                {
                    foreach (var globalVar in solutionModel.GlobalVariables)
                    {
                        TreeNode gItemNode = new TreeNode(globalVar.Name);
                        gItemNode.Name = globalVar.Id.ToString();
                        gItemNode.Tag = globalVar;
                        AddDeleteContestMenu(gItemNode);
                        gNode.Nodes.Add(gItemNode);
                    }
                }
                foreach (ProjectModel proj in solutionModel.Projects)
                {
                    TreeNode trvNode = new TreeNode(proj.Name);
                    trvNode.Tag = proj;
                    trvNode.Name = proj.Id.ToString();
                    AddDeleteContestMenu(trvNode);
                    trvProjectView.Nodes.Add(trvNode);

                    TreeNode varNodeList = new TreeNode("Variables");
                    AddDAddNewContestMenu(varNodeList);
                    trvNode.Nodes.Add(varNodeList);
                    if (proj.ProcessVariables != null)
                    {
                        foreach (var v in proj.ProcessVariables)
                        {
                            v.ParentProject = proj;
                            TreeNode pvItemNode = new TreeNode(v.Name);
                            pvItemNode.Tag = v;
                            pvItemNode.Name = v.Id.ToString();
                            AddDeleteContestMenu(pvItemNode);
                            varNodeList.Nodes.Add(pvItemNode);
                        }
                    }

                    foreach (ProjectStepModel prjStep in proj.ProjectSteps)
                    {
                        prjStep.ParentProject = proj;
                        TreeNode trvStep = new TreeNode(prjStep.Name);
                        trvStep.Tag = prjStep;
                        trvStep.Name = prjStep.Id.ToString();
                        AddDeleteContestMenu(trvStep);
                        trvNode.Nodes.Add(trvStep);
                    }
                }
            }
        }

        private void AddDeleteContestMenu(TreeNode trNode)
        {
            ToolStripMenuItem tsmDelete = new ToolStripMenuItem();
            tsmDelete.Text = "Delete";
            tsmDelete.Tag = trNode;
            tsmDelete.Click += new System.EventHandler(TreeViewDeleteCtxClick);
            trNode.ContextMenuStrip = new ContextMenuStrip();
            trNode.ContextMenuStrip.Items.Add(tsmDelete);
        }

        private void AddDAddNewContestMenu(TreeNode trNode)
        {
            ToolStripMenuItem tsmAddNew = new ToolStripMenuItem();
            tsmAddNew.Text = "Add New";
            tsmAddNew.Tag = trNode;
            tsmAddNew.Click += new System.EventHandler(TreeViewAddNewCtxClick);
            trNode.ContextMenuStrip = new ContextMenuStrip();
            trNode.ContextMenuStrip.Items.Add(tsmAddNew);
        }

        internal void TreeViewDeleteCtxClick(object sender, EventArgs e)
        {
            ToolStripMenuItem itemNode = sender as ToolStripMenuItem;
            if (itemNode != null)
            {
                TreeNode trn = itemNode.Tag as TreeNode;

                if (MessageBox.Show($"Are you sure you want to delete '{itemNode.Text}'?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    switch (trn.Tag.GetType().Name)
                    {
                        case "VariableModel":
                            VariableModel vm = trn.Tag as VariableModel;
                            // is it global variable or project variable?
                            if (vm.ParentProject == null)
                            {
                                _solutionModel.GlobalVariables.Remove(vm);
                                trn.Remove();
                                BridgeLog.Info($"Delete global variable item '{itemNode.Text}'");
                            }
                            else
                            {
                                vm.ParentProject.ProcessVariables.Remove(vm);
                                trn.Remove();
                                BridgeLog.Info($"Delete project variable item '{itemNode.Text}' from project '{vm.ParentProject.Name}'");
                            }
                            break;
                        case "ProjectModel":
                            ProjectModel projectModel = trn.Tag as ProjectModel;
                            if (projectModel.ParentSolution != null)
                            {
                                projectModel.ParentSolution.Projects.Remove(projectModel);
                                trn.Remove();
                                BridgeLog.Info($"Delete project '{projectModel.Name}'");
                            }
                            break;
                        case "ProjectStepModel":
                            ProjectStepModel prjsm = trn.Tag as ProjectStepModel;
                            if (prjsm.ParentProject != null)
                            {
                                prjsm.ParentProject.ProjectSteps.Remove(prjsm);
                                trn.Remove();
                                BridgeLog.Info($"Delete project step item '{itemNode.Text}' from project '{prjsm.ParentProject.Name}'");
                            }
                            break;
                    }
                }
            }
        }

        internal void TreeViewAddNewCtxClick(object sender, EventArgs e)
        {
            ToolStripMenuItem itemNode = sender as ToolStripMenuItem;
            if (itemNode != null)
            {
                TreeNode trn = itemNode.Tag as TreeNode;
                _prntFrm.CrntTreeNode = trn;
                switch (trn.Text)
                {
                    case "Global Variables":
                        VariableModel gModel = new VariableModel()
                        {
                            Id = Guid.NewGuid(),
                            Name = "<New>",
                            SourceType = "Value",
                            ValueType = "String"
                        };
                        _solutionModel.GlobalVariables.Add(gModel);
                        _prntFrm.AddVariableGlobalVariable(gModel);
                        break;
                    case "Variables":
                        ProjectModel project = trn.Parent.Tag as ProjectModel;
                        VariableModel pModel = new VariableModel()
                        {
                            Id = Guid.NewGuid(),
                            Name = "<New>",
                            SourceType = "Value",
                            ValueType = "String"
                        };
                        project.ProcessVariables.Add(pModel);
                        _prntFrm.AddProjectVariable(project.Id.ToString(), pModel);
                        break;
                }
            }
        }
    }
}
