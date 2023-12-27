using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.WindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                HarvestBridgeDataSync app = new HarvestBridgeDataSync(args);
                app.DebugOnStart(args);                
                do
                {
                    System.Threading.Thread.Sleep(1000);
                }while(true);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new HarvestBridgeDataSync(args)
                };

                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
