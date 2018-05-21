using ImageService.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infastructure.Event
{
    public class ClientCommandEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public CommandRecievedEventArgs Args { get; }

        public ClientCommandEventArgs(TcpClient client, CommandRecievedEventArgs args)
        {
            Client = client;
            Args = args;
        }
    }
}
