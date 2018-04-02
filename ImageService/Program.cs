using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyNewService
{
    static class Program
    {
        /// <summary>
        /// The entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //TODO something probably
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ImageService.ImageService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
