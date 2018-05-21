using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infrastructure.Event
{
    /// <summary>
    /// The event arguements used by the command recieved event.
    /// </summary>
    public class CommandRecievedEventArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }      // The command arguements.
        public string RequestDirPath { get; set; }  // The Request Directory
        /// <summary>
        /// Constructs a new CommandRecievedEventArgs object.
        /// </summary>
        /// <param name="id">The command ID, should be given in terms of the command enum.</param>
        /// <param name="args">The command arguements.</param>
        /// <param name="path">The path leading to the requested directory.</param>
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }

        public static string CommandRecievedToJSON(CommandRecievedEventArgs args)
        {
            JObject command_obj = new JObject();
            command_obj["Commmand ID:"] = args.CommandID;
            command_obj["args amount:"] = args.Args.Length;
            for (int i = 0; i < args.Args.Length; i++)
            {
                command_obj["args " + i + ":"] = args.Args[i];
            }
            if (args.RequestDirPath != null)
                command_obj["Path:"] = args.RequestDirPath;
            else
                command_obj["Path:"] = "null";
            return command_obj.ToString();
        }

        public static CommandRecievedEventArgs CommandRecievedFromJSON(string message)
        {
            JObject com_recived = JObject.Parse(message);
            int id = (int)com_recived["Commmand ID:"];
            string dir_path = (string)com_recived["Path:"];
            int command_amount = (int)com_recived["args amount: "];
            string[] args = new string[command_amount];
            for(int i = 0; i < command_amount; i++)
            {
                args[i] = (string)com_recived["args " + i + ":"];
            }
            return new CommandRecievedEventArgs(id, args, dir_path);
        }

        public static string ConfigToJSON(string output_dir, string source_name, string log_name, string thumb_size
            , String[] Handlers)
        {
            JObject config = new JObject();
            config["OutputDir"] = output_dir;
            config["SourceName"] = source_name;
            config["LogName"] = log_name;
            config["ThumbnailSize"] = thumb_size;
            String[] Handelers = Handlers;
            int handler_amount = Handelers.Length;
            config["handler amount: "] = handler_amount;
            for (int i = 0; i < handler_amount; i++)
            {
                config["handler" + i + ": "] = Handelers[i];
            }
            return config.ToString();
        }
    }
}
