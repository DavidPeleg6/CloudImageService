using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// A class implementing the image controller interface which is used to execute commands given by the server.
    /// </summary>
    public class ImageController : IImageController
    {
        private IImageServiceModal MyModal;                      // The Modal Object
        private Dictionary<int, ICommand> CommandDictionary;
        /// <summary>
        /// Contructs a new image controller object.
        /// </summary>
        /// <param name="modal">The image service modal the controller will controll.</param>
        public ImageController(IImageServiceModal modal)
        {
            MyModal = modal;                    // Storing the Modal Of The System
            CommandDictionary = new Dictionary<int, ICommand>();
            NewFileCommand file = new NewFileCommand(MyModal);
            LogCommand logger = new LogCommand();
            CloseCommand close = new CloseCommand();
            GetConfigCommand conf = new GetConfigCommand();
            GetStatsCommand stats = new GetStatsCommand();
            //link new file command with stats command
            file.InformNewFile += stats.PhotoAdded;
            close.InformClose += conf.HandlerRemoved;
            //close.inform_close += logger.OnLogChange; //TODO: check this
            file.InformNewFile += logger.OnLogChange;
            CommandDictionary.Add((int)CommandEnum.NewFileCommand, file);
            CommandDictionary.Add((int)CommandEnum.LogCommand, logger);
            CommandDictionary.Add((int)CommandEnum.CloseCommand, close);
            CommandDictionary.Add((int)CommandEnum.GetConfigCommand, conf);

        }
        /// <summary>
        /// Executes a command specified by the commandID using internal logic.
        /// </summary>
        /// <param name="commandID">The ID of the command to be executed, given in terms of the command enum.</param>
        /// <param name="args">The commands arguements.</param>
        /// <param name="result">True if command execution was succsesfull and false otherwise.</param>
        /// <returns>The commands output.</returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            ICommand Command;
            if (!CommandDictionary.TryGetValue(commandID, out Command))
            {
                //cound't find the command in the dictionary
                resultSuccesful = false;
                return "Unknown command.";
            }
            return Command.Execute(args, out resultSuccesful);
        }
    }
}
