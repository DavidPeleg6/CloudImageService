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
        /// <summary>
        /// Formatted the eventargs to JSON.
        /// </summary>
        /// <param name="args">The args to be formatted.</param>
        /// <returns>JSON formatted event args, ready to be sent.</returns>
        public static string CommandRecievedToJSON(CommandRecievedEventArgs args)
        {
            JObject JSONCommandObject = new JObject();
            JSONCommandObject["Commmand ID:"] = args.CommandID;
            if (args.Args != null)
            {
                JSONCommandObject["args amount:"] = args.Args.Length;
                for (int i = 0; i < args.Args.Length; i++)
                {
                    JSONCommandObject["args " + i + ":"] = args.Args[i];
                }
            }
            else
            {
                JSONCommandObject["args amount:"] = 0;
            }
            if (args.RequestDirPath != null)
                JSONCommandObject["Path:"] = args.RequestDirPath;
            else
                JSONCommandObject["Path:"] = null;//TODO: maybe make null a string
            return JSONCommandObject.ToString();
        }
        /// <summary>
        /// UnJSON formatted some commandrecieved eventargs.
        /// </summary>
        /// <param name="message">The eventargs to be converted back.</param>
        /// <returns>Eventargs in an eventarg format.</returns>
        public static CommandRecievedEventArgs CommandRecievedFromJSON(string message)
        {
            JObject JSONCommandRecievedArgs = JObject.Parse(message);
            int ID = (int)JSONCommandRecievedArgs["Commmand ID:"];
            int CommandAmmount = (int)JSONCommandRecievedArgs["args amount:"];
            string DirectoryPath = (string)JSONCommandRecievedArgs["Path:"];
            string[] Args = new string[CommandAmmount];
            for(int i = 0; i < CommandAmmount; i++)
            {
                Args[i] = (string)JSONCommandRecievedArgs["args " + i + ":"];
            }
            return new CommandRecievedEventArgs(ID, Args, DirectoryPath);
        }
        /// <summary>
        /// Takes config data and converts it to a JSON format.
        /// </summary>
        /// <param name="outputDir">Output directory, taken from app.config.</param>
        /// <param name="sourceName">Source Name, taken from app.config.</param>
        /// <param name="logName">Log Name, taken from app.config.</param>
        /// <param name="thumbSize">Thumbnail size, taken from app.config.</param>
        /// <param name="Handlers">Handeler list, taken from the get config command.</param>
        /// <returns>JSON formatted config data.</returns>
        public static string ConfigToJSON(string outputDir, string sourceName, string logName, string thumbSize
            , String[] Handlers)
        {
            JObject JSONConfigObject = new JObject();
            JSONConfigObject["type"] = "config";
            JSONConfigObject["OutputDir"] = outputDir;
            JSONConfigObject["SourceName"] = sourceName;
            JSONConfigObject["LogName"] = logName;
            JSONConfigObject["ThumbnailSize"] = thumbSize;
            String[] Handelers = Handlers;
            int handler_amount = Handelers.Length;
            JSONConfigObject["handler amount: "] = handler_amount;
            for (int i = 0; i < handler_amount; i++)
            {
                JSONConfigObject["handler" + i + ": "] = Handelers[i];
            }
            return JSONConfigObject.ToString();
        }
    }
}
