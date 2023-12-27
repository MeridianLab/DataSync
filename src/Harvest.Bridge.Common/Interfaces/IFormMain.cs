using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harvest.Bridge.Common.Interfaces
{
    public interface IFormMain
    {
        SolutionModel SolutionModel { get; set; }
        TreeNode CrntTreeNode { get; set; }
        void SaveAndReloadProject();

        void AddProjectItem(object newObject);

        void AddVariableGlobalVariable(VariableModel newObject);

        void AddProjectVariable(string parentId, VariableModel newObject);

        void ReloadTreeView();
    }
}
