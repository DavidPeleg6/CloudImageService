using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal.Event
{
    public class LogChangedEventArgs
    {
        public MessageTypeEnum Type { get; set; }       // The path to the directory.

        public string Message { get; set; }             // The Message That went to the logger
        /// <summary>
        /// Contructs a new DirectoryCloseEventArgs object.
        /// </summary>
        /// <param name="dirPath">The directory name and path to be stored.</param>
        /// <param name="message">The messege to be stored.</param>
        public LogChangedEventArgs(string message, MessageTypeEnum type)
        {
            Type = type;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

        public override string ToString()
        {
            return ((int)Type).ToString() + " " + Message;
        }

    }
}
