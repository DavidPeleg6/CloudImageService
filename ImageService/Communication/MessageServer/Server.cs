using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Communication
{
    public class Server
    {
        public int Port { get; set; }
        private TcpListener listener;
        private static Server instance;
        public IClientHandler ClHandler { get; set; }

        private Server()
        {
            ClHandler = new ClientHandler();
        }

        public static Server GetServer()
        {
            if (instance == null)
                instance = new Server();
            return instance;
        }

        public void ServerStart(IPEndPoint ep)
        {
            //IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["IPaddress"])
            //    , int.Parse(ConfigurationManager.AppSettings["port"]));
            Port = ep.Port;
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ClHandler.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        public void ServerStop()
        {
            listener.Stop();
            ClHandler.StopHandler();
        }
    }
}
