using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Communication
{
    /// <summary>
    /// A servar that runs as part of the windows service, recieves data requests from the GUI 
    /// (and probably other remote processes later in future HW assignments) takes the requested data from the service and passes
    /// it over to the requester via a JSON formatted TCP data tranfer.
    /// Is a singleton since the service only needs one server in order to communicate iwth clients.
    /// </summary>
    public class Server
    {
        public int Port { get; set; }
        private TcpListener listener;
        private static Server instance;
        public IClientHandler ClHandler { get; set; }
        /// <summary>
        /// Constructor, simply makes a new client handeler object.
        /// Is private since this is a singleton.
        /// </summary>
        private Server()
        {
            ClHandler = new ClientHandler();
        }
        /// <summary>
        /// Returns the server, acts as the constructor since the server is a singleton.
        /// </summary>
        /// <returns></returns>
        public static Server GetServer()
        {
            if (instance == null)
                instance = new Server();
            return instance;
        }
        /// <summary>
        /// Starts the server up and hands the client over to a clienthandeler to handle operating it.
        /// </summary>
        /// <param name="ep">The ip that the server should be listening to.</param>
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
        /// <summary>
        /// Returns the ip adress of the device the server is being run on.
        /// </summary>
        /// <returns>The ip adress of the device the server is being run on.</returns>
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
        /// <summary>
        /// Shuts the server down and stops the clienthandleler.
        /// </summary>
        public void ServerStop()
        {
            listener.Stop();
            ClHandler.StopHandler();
        }
    }
}
