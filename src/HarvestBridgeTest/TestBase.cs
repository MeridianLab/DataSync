using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Test
{
    public abstract class TestBase
    {
        public SolutionModel LoadSolution(string solutionFileName)
        {
            string driverFilePath = ConfigurationManager.AppSettings["DriverFolderPath"];
            return LoadProjectFromJSON.LoadProject(driverFilePath + solutionFileName);
        }
    }
}
