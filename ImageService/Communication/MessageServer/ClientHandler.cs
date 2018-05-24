using Communication.MessageServer;
using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Communication
{
    /// <summary>
    /// An object inplementing the ICLientHandleler interface, is used to handle TCP clients.
    /// </summary>
    public class ClientHandler : IClientHandler
    {
        private Dictionary<TcpClient, Message> clients;
        private bool handler_alive;

        public event EventHandler<ClientCommandEventArgs> ClientCommandRecieved;
        /// <summary>
        /// Constructor, makes new client handeler that currently does not handle any clients.
        /// </summary>
        public ClientHandler()
        {
            handler_alive = true;
            clients = new Dictionary<TcpClient, Message>();
        }
        /// <summary>
        /// Tells the server to start handeling a new client.
        /// </summary>
        /// <param name="client">The client to be handeled by the server.</param>
        public void HandleClient(TcpClient client)
        {
            clients.Add(client, new Message(true, false, ""));
            Task t = new Task(() =>
            {
                try
                {
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryReader reader = new BinaryReader(stream))
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        while (handler_alive)
                        {
                            if (clients[client].ReadMessage)
                            {
                                string commandLine = reader.ReadString();
                                clients[client].AmountWrote = 0;
                                if (commandLine == "close")
                                {
                                    CloseClient(client);
                                    break;
                                }
                                CommandRecievedEventArgs args = CommandRecievedEventArgs.CommandRecievedFromJSON(commandLine);
                                ClientCommandRecieved?.Invoke(this, new ClientCommandEventArgs(client, args));
                                clients[client].ReadMessage = false;
                            }
                            if (clients[client].WriteMessage)
                            {
                                if (clients[client].AmountWrote >= 1)
                                {
                                    clients[client].WriteMessage = false;
                                    clients[client].ReadMessage = true;
                                    continue;
                                }
                                writer.Write(clients[client].MessageString);
                                clients[client].AmountWrote++;
                                clients[client].WriteMessage = false;
                                clients[client].ReadMessage = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //TODO: maybe delete this try-catch
                }
                clients.Remove(client);
                CloseClient(client);
            });
            t.Start();
        }
        /// <summary>
        /// Informs the client that the command is has rejested to be down has finished,
        /// the client uses this to then make use of the data the clienthandeler pass to it
        /// whiles't knowing that the server has finished doing all that ahs benn requested of it.
        /// </summary>
        /// <param name="sender">The client.</param>
        /// <param name="e">The arguements specifing the result of the command / the info that was requested.</param>
        public void CommandDone(object sender, CommandDoneEventArgs e)
        {
            if (e.Client == null)
                return;
            clients[e.Client].WriteMessage = true;
            clients[e.Client].MessageString = e.Message;
        }
        /// <summary>
        /// Shuts down the connection with a TCP client and removes it from the list of currently handeled clients.
        /// </summary>
        /// <param name="client">The client to be removed.</param>
        private void CloseClient(TcpClient client)
        {
            client.Close();
            clients.Remove(client);
        }
        /// <summary>
        /// Shuts the handleler down in a controlled manner and disconnects the clients.
        /// </summary>
        public void StopHandler()
        {
            handler_alive = false;
        }
    }
}
