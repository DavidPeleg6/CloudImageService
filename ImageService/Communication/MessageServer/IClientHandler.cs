using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Event;
using System;
using System.Net.Sockets;

namespace Communication
{
    /// <summary>
    /// Interface for a class that handels communication with a client.
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// An event that is used to pass arguements to the client.
        /// </summary>
        event EventHandler<ClientCommandEventArgs> ClientCommandRecieved;
        /// <summary>
        /// Tells the server to start handeling a new client.
        /// </summary>
        /// <param name="client">The client to be handeled by the server.</param>
        void HandleClient(TcpClient client);
        /// <summary>
        /// Informs the client that the command is has rejested to be down has finished,
        /// the client uses this to then make use of the data the clienthandeler pass to it
        /// whiles't knowing that the server has finished doing all that ahs benn requested of it.
        /// </summary>
        /// <param name="sender">The client.</param>
        /// <param name="e">The arguements specifing the result of the command / the info that was requested.</param>
        void CommandDone(object sender, CommandDoneEventArgs e);
        /// <summary>
        /// Shuts the handleler down in a controlled manner and disconnects the clients.
        /// </summary>
        void StopHandler();
    }
}
