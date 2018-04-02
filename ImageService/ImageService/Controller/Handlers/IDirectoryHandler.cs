using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// An interface for classes that handle directories.
    /// </summary>
    public interface IDirectoryHandler
    {
        /// <summary>
        /// The Event That Notifies that the Directory is being closed
        /// </summary>
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed
        /// <summary>
        /// method to start listening on a given directory
        /// </summary>
        /// <param name="dirPath">path to given directory</param>
        void StartHandleDirectory(string dirPath);                               // The Function Recieves the directory to Handle
        /// <summary>
        /// method to be activated when command enters
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e"> args for the command</param>
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);     // The Event that will be activated upon new Command
    }
}
