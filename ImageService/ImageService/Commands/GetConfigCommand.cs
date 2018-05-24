using ImageService.Commands;
using ImageService.Infrastructure.Event;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// A command that requests information regarding the app.config file.
    /// Used by the settings window of the gui.
    /// In addition, the command also keeps track of which handlelers have been closed.
    /// This is done here (as opposed to the service itself) for several reasons:
    /// We assumed the removal command shouldn't alter the app.config file.
    /// In addition, since the command shouldn't know of the existance of the server, it can't reference it.
    /// </summary>
    public class GetConfigCommand : ICommand
    {
        String[] ActiveHandelers;
        /// <summary>
        /// Constructor, constructs a list of active handelers by checking which adresses point to a folder that exists.
        /// </summary>
        public GetConfigCommand()
        {
            int Count = 0;
            String[] Handelers = ConfigurationManager.AppSettings["Handler"].Split(';');
            for (int i = 0; i < Handelers.Length; i++)
            {
                if (System.IO.Directory.Exists(Handelers[i]))
                {
                    Count++;
                }
            }
            int j = 0;
            ActiveHandelers = new String[Count];
            for (int i = 0; i < Count; i++)
            {
                while (!System.IO.Directory.Exists(Handelers[j]))
                {
                    j++;
                }
                ActiveHandelers[i] = Handelers[j++];
            }
        }
        /// <summary>
        /// Gets the config data and returns it to the caller.
        /// </summary>
        /// <param name="args">not used in this command.</param>
        /// <param name="result">is set to true when command execution is finished.</param>
        /// <returns>JSON formatted config data.</returns>
        string ICommand.Execute(string[] args, out bool result)
        {
            string out_dir = ConfigurationManager.AppSettings["OutputDir"];
            string src_name = ConfigurationManager.AppSettings["SourceName"];
            string log_name = ConfigurationManager.AppSettings["LogName"];
            string thumb_size = ConfigurationManager.AppSettings["ThumbnailSize"];
            if(ActiveHandelers.Length == 0)
            {
                //TODO: may cause JSON exception
            }
            result = true;
            return CommandRecievedEventArgs.ConfigToJSON(out_dir, src_name, log_name, thumb_size, ActiveHandelers);
        }
        /// <summary>
        /// A function that is called when a handleler is removed,
        /// removes it from the internal list kept by getConfigCommand in order to ensure
        /// future calls to the function will only return the handlelers currently being used by the service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HandlerRemoved(object sender, DirectoryCloseEventArgs args)
        {
            List<String> Temp = ActiveHandelers.ToList<String>();
            if (!Temp.Contains(args.DirectoryPath))
            {
                return;
            }
            Temp.Remove(args.DirectoryPath);
            ActiveHandelers = Temp.ToArray<String>();
        }

    }
}
