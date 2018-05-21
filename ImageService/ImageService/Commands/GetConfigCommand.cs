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
    public class GetConfigCommand : ICommand
    {
        public GetConfigCommand() { }
        
        string ICommand.Execute(string[] args, out bool result)
        {
            string out_dir = ConfigurationManager.AppSettings["OutputDir"];
            string src_name = ConfigurationManager.AppSettings["SourceName"];
            string log_name = ConfigurationManager.AppSettings["LogName"];
            string thumb_size = ConfigurationManager.AppSettings["ThumbnailSize"];
            String[] Handelers = ConfigurationManager.AppSettings["Handler"].Split(';');
            result = true;
            return CommandRecievedEventArgs.ConfigToJSON(out_dir, src_name, log_name, thumb_size, Handelers);
        }

    }
}
