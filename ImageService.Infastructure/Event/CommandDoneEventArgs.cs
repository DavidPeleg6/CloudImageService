using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infastructure.Event
{
    public class CommandDoneEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public string Message { get;  }

        public CommandDoneEventArgs(TcpClient client, string msg)
        {
            Message = msg;
            Client = client;
        }
    }
}
