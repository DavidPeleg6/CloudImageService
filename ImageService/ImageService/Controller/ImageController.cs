﻿using ImageService.Commands;
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
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;
        /// <summary>
        /// Contructs a new image controller object.
        /// </summary>
        /// <param name="modal">The image service modal the controller will controll.</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>();
            NewFileCommand file = new NewFileCommand(m_modal);
            LogCommand logger = new LogCommand();
            CloseCommand close = new CloseCommand();
            GetConfigCommand conf = new GetConfigCommand();
            //TODO might fuck us
            close.InformClose += conf.HandlerRemoved;
            //close.inform_close += logger.OnLogChange;
            //
            file.InformNewFile += logger.OnLogChange;
            commands.Add((int)CommandEnum.NewFileCommand, file);
            commands.Add((int)CommandEnum.LogCommand, logger);
            commands.Add((int)CommandEnum.CloseCommand, close);
            commands.Add((int)CommandEnum.GetConfigCommand, conf);
            
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
            if (!commands.TryGetValue(commandID, out Command))
            {
                //cound't find the command in the dictionary
                resultSuccesful = false;
                return "Unknown command.";
            }
            return Command.Execute(args, out resultSuccesful);
        }
    }
}
