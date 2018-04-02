
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// A class used to record the actions the service takes to the log.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// An event triggered when a messege is recieved, logs it.
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved; //this event happens when you get a messege
        /// <summary>
        /// A functions using the eventhandeler above to handle a messege.
        /// </summary>
        /// <param name="message">The messege to be logged.</param>
        /// <param name="type">The type of messege.</param>
        public void Log(string message, MessageTypeEnum type)
        {
            EventHandler<MessageRecievedEventArgs> Handler = MessageRecieved;
            if (Handler != null)
            {
                MessageRecievedEventArgs Args = new MessageRecievedEventArgs();
                Args.Status = type;
                Args.Message = message;
                Handler(this, Args);
            }
        }
    }
}
