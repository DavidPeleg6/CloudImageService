using ImageService.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class LogCommand : ICommand
    {
        private Dictionary<int, string> log;
       // private bool if_changed;

        public LogCommand()
        {
            log = new Dictionary<int, string>();
            //if_changed = false;
        }

        string ICommand.Execute(string[] args, out bool result)
        {
            //if message was added to logger and it is not just being asked for, args[1] will be the message and args[0] is type
            if(args != null)
            {
                result = /*if_changed =*/ true;
                log.Add(int.Parse(args[0]), args[1]);
                return null;
            }
            //result = if_changed;
            //if_changed = false;
            //if(result)
            result = true;
            return LogChangedEventArgs.CompleteLogToJSON(log);
            //return null;
        }

        public void OnLogChange(object sender, LogChangedEventArgs args)
        {
            //if_changed = true;
            log.Add((int)args.Type, args.Message);
        }
    }
}
