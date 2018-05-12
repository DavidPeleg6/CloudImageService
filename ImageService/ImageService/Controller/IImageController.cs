using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// An interface for objects that execute commands given to them.
    /// </summary>
    public interface IImageController
    {
        /// <summary>
        /// Executes a command specified by the commandID using internal logic.
        /// </summary>
        /// <param name="commandID">The ID of the command to be executed, given in terms of the command enum.</param>
        /// <param name="args">The commands arguements.</param>
        /// <param name="result">True if command execution was succsesfull and false otherwise.</param>
        /// <returns>The commands output.</returns>
        string ExecuteCommand(int commandID, string[] args, out bool result);          // Executing the Command Requet
    }
}
