using ImageService.Modal;
using ImageService.Modal.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ImageService.Server
{
    class ClientHandler : IClientHandler
    {
        private List<TcpClient> clients;
        public event EventHandler<CommandRecievedEventArgs> ClientCommandRecieved;

        public ClientHandler()
        {
            this.clients = new List<TcpClient>();
        }
        //TODO add write
        public void HandleClient(TcpClient client)
        {
            clients.Add(client);
            Task t = new Task(() =>
            {
                try
                {
                    bool clientAlive = true;
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        while (clientAlive)
                        {
                            //TODO learn to decipher message through JSON
                            string commandLine = reader.ReadLine();
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

        //TODO write the event args normally might cause bug due to multiple threads writing together
        public void OnLogChange(object sender, LogChangedEventArgs e)
        {
            foreach(var client in clients)
            {
                try
                {
                    using (NetworkStream stream = client.GetStream())
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(e.ToString());
                    }
                } catch (Exception ex)
                {
					Console.WriteLine("error socket {0}", ex.Message);
                    CloseClient(client);
                }
            }
        }

        public void CloseClient(TcpClient client)
        {
            client.Close();
            clients.Remove(client);
        }
    }
}
