using ImageService.Commands;
using ImageService.Infrastructure.Event;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class CloseCommand : ICommand
    {
        public EventHandler<LogChangedEventArgs> inform_close;

        public CloseCommand() { }

        string ICommand.Execute(string[] args, out bool result)
        {
            result = true;
            inform_close?.Invoke(this, new LogChangedEventArgs("directory " + args[0] + " closed", MessageTypeEnum.INFO));
            //TODO find a way to delete the folder from app config
            return "close command done";
        }
    }
}
