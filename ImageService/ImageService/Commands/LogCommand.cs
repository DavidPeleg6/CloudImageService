using ImageService.Infastructure;
using ImageService.Infrastructure.Event;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// Command that requests the log information from the service.
    /// </summary>
    class LogCommand : ICommand
    {
        private List<ISPair> LogsList;
       /// <summary>
       /// Constructor, doesn't really do anything, just prepars an object to store the log.
       /// </summary>
        public LogCommand()
        {
            LogsList = new List<ISPair>();
        }
        /// <summary>
        /// Executes the command, requesting all of the logdata from the service and returning it to the caller.
        /// </summary>
        /// <param name="args">args[1] is the message and args[0] is type</param>
        /// <param name="result">Speficies if the command executed corretly.</param>
        /// <returns>Eventargs containing the log infromation.</returns>
        string ICommand.Execute(string[] args, out bool result)
        {
            if (args != null && args.Length > 0)
            {
                result = true;
                LogsList.Add(new ISPair((MessageTypeEnum)int.Parse(args[0]), args[1]));
                return "";
            }
            result = true;
            return LogChangedEventArgs.CompleteLogToJSON(LogsList);
        }
        /// <summary>
        /// Function that is called when the log data has changed and the clients using the data (such as the gui) need to be updated.
        /// </summary>
        /// <param name="sender">The service.</param>
        /// <param name="args">args containing information about the changed log data.</param>
        public void OnLogChange(object sender, LogChangedEventArgs args)
        {
            LogsList.Add(new ISPair((MessageTypeEnum)args.Type, args.Message));
        }
    }
}
