using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Configuration;
using System.Net.Sockets;
using System.IO;
using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Event;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Communication.Client
{
    /// <summary>
    /// A client that the GUI uses to communicate with the server in order to request data from it.
    /// Is a singleton in order to make sure all of the GUIs diffrent componments use the same client to communicate with the server.
    /// </summary>
    public class Client
    {
        private static Client _instance;
        private bool ClientAlive;
        private bool MessagePending;
        private bool WaitResult;
        private string Message;
        public event EventHandler<CommandDoneEventArgs> CommandDone; 
        /// <summary>
        /// Constructor, sets all of the clients values to false and makes the messege empty.
        /// Those values will be filled by data requests.
        /// </summary>
        private Client()
        {
            ClientAlive = false;
            MessagePending = false;
            WaitResult = false;
            Message = "";
        }
        /// <summary>
        /// Gets an instance of the client, functions in place of a constructor.
        /// </summary>
        /// <returns>An instance of the client.</returns>
        public static Client GetInstance
        {
            get { return _instance ?? (_instance = new Client()); }
        }
        /// <summary>
        /// Conneects to the server (localhost for the time being).
        /// And waits for one of the classes using the client to request some data from the server.
        /// Once someone makes a request (by activating CommandRecieved) it sends that request to the server
        /// and then reads the result sent back into CommandDoneEventArgs
        /// which the requester get CommandDone.
        /// </summary>
        public void ClientStart()
        {
            //TODO get path and port from client caller
            //Communication.Server server = Communication.Server.GetServer();
            //string IP = server.GetLocalIPAddress();
            //int port = server.Port;
            string IP = "127.0.0.1";
            int Port = 8000;
            if (IP == null)
                return;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), Port);
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(ep);
            }
            catch
            {
                return;
            }
            ClientAlive = true;
            Task t = new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (ClientAlive)
                    {
                        if (MessagePending)
                        {
                            // Send data to server
                            writer.Write(Message);
                            MessagePending = false;
                            WaitResult = true;
                        }
                        if (WaitResult)
                        {
                            // Get result from server
                            string result = reader.ReadString();
                            WaitResult = false;
                            if (result == "close")
                            {
                                ClientAlive = false;
                                client.Close();
                                break;
                            }
                            CommandDone(this, new CommandDoneEventArgs(null, result));
                        }
                    }
                }
            }); 
            t.Start();
        }
        /// <summary>
        /// Requests data from the server.
        /// Clarification regarding the title, it refers to the fact the command is being recieved by the server from the client.
        /// </summary>
        /// <param name="sender">The object requesting the data (should usually just by "this").</param>
        /// <param name="args">The request being sent.</param>
        public void CommandRecieved(object sender, CommandRecievedEventArgs args)
        {
            if (args.Args != null && String.Equals(args.Args[0], "close"))
            {
                ClientAlive = false;
            }
            Message = CommandRecievedEventArgs.CommandRecievedToJSON(args);
            MessagePending = true;
        }
        /// <summary>
        /// Gets the status of the client, i.e if it's connected to the server.
        /// This is used by classes using the client to check if they can request data from the server.
        /// </summary>
        /// <returns>The status of the client.</returns>
        public bool GetStatus()
        {
            return ClientAlive;
        }
        /// <summary>
        /// Constructs a dictionary of log objects using JSON formatted data sent from the server.
        /// </summary>
        /// <param name="log">JSON formatted data sent by the server.</param>
        /// <returns>A dictionary of logs to be printed on the Logs screen.</returns>
        public Dictionary<int, string> LogFromJSON(string log)
        {
            Dictionary<int, string> dict = JsonConvert.DeserializeObject<Dictionary<int, string>>(log);
            return dict;
        }
    }
}
