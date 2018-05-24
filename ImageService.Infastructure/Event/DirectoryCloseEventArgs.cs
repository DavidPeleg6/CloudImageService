using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infrastructure.Event
{
    /// <summary>
    /// The event arguements used by directory closing events.
    /// </summary>
    public class DirectoryCloseEventArgs : EventArgs
    {
        public string DirectoryPath { get; set; }       // The path to the directory.

        public string Message { get; set; }             // The Message That goes to the logger
        /// <summary>
        /// Contructs a new DirectoryCloseEventArgs object.
        /// </summary>
        /// <param name="dirPath">The directory name and path to be stored.</param>
        /// <param name="message">The messege to be stored.</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

    }
}
