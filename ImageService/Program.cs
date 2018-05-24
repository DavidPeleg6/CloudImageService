using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyNewService
{
    /// <summary>
    /// This class is just the entry point for the application.
    /// All it does is start the service up.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ImageService.ImageService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
