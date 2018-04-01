
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
            EventHandler<MessageRecievedEventArgs> handler = MessageRecieved;
            if (handler != null)
            {
                MessageRecievedEventArgs args = new MessageRecievedEventArgs();
                args.Status = type;
                args.Message = message;
                handler(this, args);
            }
        }
    }
}
