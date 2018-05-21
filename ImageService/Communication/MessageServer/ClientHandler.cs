using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Communication
{
    public class ClientHandler : IClientHandler
    {
        private List<TcpClient> clients;
        private bool handler_alive;

        public event EventHandler<ClientCommandEventArgs> ClientCommandRecieved;

        public ClientHandler()
        {
            handler_alive = true;
            clients = new List<TcpClient>();
        }
        public void HandleClient(TcpClient client)
        {
            clients.Add(client);
            Task t = new Task(() =>
            {
                try
                {
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (handler_alive)
                        {
                            string commandLine = reader.ReadLine();
                            if(commandLine == "close")
                            {
                                CloseClient(client);
                                break;
                            }
                            CommandRecievedEventArgs args = CommandRecievedEventArgs.CommandRecievedFromJSON(commandLine);
                            ClientCommandRecieved?.Invoke(this, new ClientCommandEventArgs(client, args));
                            Console.WriteLine("Got command: {0}", commandLine);
                        }
                    }
                } catch (Exception e) {
					Console.WriteLine("error socket {0}", e.Message);
				}
                CloseClient(client);
            });
            t.Start();
        }

        public void OnLogChange(object sender, LogChangedEventArgs e)
        {
            foreach(var client in clients)
            {
                WriteResult(client, LogChangedEventArgs.LogChangeToJSON(e));
            }
        }

        public void CommandDone(object sender, CommandDoneEventArgs e)
        {
            if (e.Client == null)
                return;
            WriteResult(e.Client, e.Message);
        }

        private void WriteResult(TcpClient client, string message)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error socket {0}", ex.Message);
                CloseClient(client);
            }
        }

        private void CloseClient(TcpClient client)
        {
            client.Close();
            clients.Remove(client);
        }

        public void StopHandler()
        {
            handler_alive = false;
        }
    }
}
