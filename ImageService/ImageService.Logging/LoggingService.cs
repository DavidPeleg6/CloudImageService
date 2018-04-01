
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved; //this event happens when you get a messege
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
