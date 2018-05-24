using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infastructure.Event
{
    /// <summary>
    /// Event arguements used to specify to a client that has requested something
    /// from a server that the action in question has been completed.
    /// </summary>
    public class CommandDoneEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public string Message { get;  }
        /// <summary>
        /// Constructor, just makes a new CommandDoneEventArgs object.
        /// </summary>
        /// <param name="client">The client that will be informed.</param>
        /// <param name="msg">The information that will be given to the client.</param>
        public CommandDoneEventArgs(TcpClient client, string msg)
        {
            Message = msg;
            Client = client;
        }
    }
}
