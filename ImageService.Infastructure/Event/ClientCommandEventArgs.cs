using ImageService.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infastructure.Event
{
    /// <summary>
    /// Used by to pass informatation to the client.
    /// </summary>
    public class ClientCommandEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public CommandRecievedEventArgs Args { get; }
        /// <summary>
        /// Constructor, makes a new CommandRecievedEventArgs object.
        /// </summary>
        /// <param name="client">The client that the information will pass to./param>
        /// <param name="args">The arguements that will be passed to the client.</param>
        public ClientCommandEventArgs(TcpClient client, CommandRecievedEventArgs args)
        {
            Client = client;
            Args = args;
        }
    }
}
