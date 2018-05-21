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
    class Client
    {
        private bool client_alive;
        private bool message_pending;
        private bool wait_result;
        private string message;
        public event EventHandler<CommandDoneEventArgs> CommandDone; 

        public Client()
        {
            client_alive = false;
            message_pending = false;
            wait_result = true;
            message = "";
        }

        public void ClientStart()
        {
            Communication.Server server = Communication.Server.GetServer();
            string IP = server.GetLocalIPAddress();
            int port = server.Port;
            if (IP == null)
                return;
            client_alive = true;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Task t = new Task(() =>
                    {
                        while(client_alive)
                        {
                            if (message_pending)
                            {
                                // Send data to server
                                Console.Write("sending Command ");
                                writer.Write(message);
                                message_pending = false;
                            }
                            if (wait_result)
                            {
                                // Get result from server
                                string result = reader.ReadString();
                                if (String.Equals(result, "close"))
                                {
                                    client_alive = false;
                                    break;
                                }
                                CommandDone(this, new CommandDoneEventArgs(null, result));
                            }
                        }
                    });
                    t.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.Data);
            }
            client.Close();
        }

        public void CommandRecieved(object sender, CommandRecievedEventArgs args)
        {
            if(args.Args != null && String.Equals(args.Args[0], "close"))
                client_alive = false;
            message_pending = true;
            message = CommandRecievedEventArgs.CommandRecievedToJSON(args);
        }

        public Dictionary<int, string> LogFromJSON(string log)
        {
            Dictionary<int, string> dict = JsonConvert.DeserializeObject<Dictionary<int, string>>(log);
            return dict;
        }
    }
}
