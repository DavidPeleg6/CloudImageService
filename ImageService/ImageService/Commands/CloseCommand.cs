using ImageService.Commands;
using ImageService.Infrastructure.Event;
using ImageService.Logging.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// Command that tell the server to stop listening to a halder, i.e close a handelelr.
    /// </summary>
    public class CloseCommand : ICommand
    {
        public EventHandler<DirectoryCloseEventArgs> InformClose;
        /// <summary>
        /// Constructor, does nothing.
        /// </summary>
        public CloseCommand() { }
        /// <summary>
        /// Command execuation, tells the service to stop listening to a handleler given in args[0].
        /// </summary>
        /// <param name="args">[0] contains the path to the directory that shouldn't be listened to.</param>
        /// <param name="result">Informs the caller if the call was succsusfull.</param>
        /// <returns>A string letting the caller know that the execuation has completed.</returns>
        string ICommand.Execute(string[] args, out bool result)
        {
            result = true;
            InformClose?.Invoke(this, new DirectoryCloseEventArgs(args[0], null));
            JObject obj = new JObject();
            obj["type"] = "close command done";

            return obj.ToString();
        }
    }
}
