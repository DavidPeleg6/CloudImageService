using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>
    /// The event arguements used by the command recieved event.
    /// </summary>
    public class CommandRecievedEventArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }      // The command arguements.
        public string RequestDirPath { get; set; }  // The Request Directory
        /// <summary>
        /// Constructs a new CommandRecievedEventArgs object.
        /// </summary>
        /// <param name="id">The command ID, should be given in terms of the command enum.</param>
        /// <param name="args">The command arguements.</param>
        /// <param name="path">The path leading to the requested directory.</param>
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }
    }
}
