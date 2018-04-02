using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// An interface implemented by classes used to log actions taken by the service.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// An event triggered when a messege is recieved, logs it.
        /// </summary>
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>
        /// A functions using the eventhandeler above to handle a messege.
        /// </summary>
        /// <param name="message">The messege to be logged.</param>
        /// <param name="type">The type of messege.</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}
