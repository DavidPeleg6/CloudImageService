using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// An interface for command type objects.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The commands arguements.</param>
        /// <param name="result">True if the command was executed succsusfully, false otherwise.</param>
        /// <returns>The commands output.</returns>
        string Execute(string[] args, out bool result);          // The Function That will Execute The 
    }
}
