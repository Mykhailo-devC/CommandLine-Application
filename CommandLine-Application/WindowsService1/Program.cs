using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {

            /*ServiceController controller = new ServiceController("AvidHubService");
            BackgroundWorker worker = new BackgroundWorker();
            ServiceContainer serviceContainer = new ServiceContainer();
            
            Debug.WriteLine(controller);*/
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
