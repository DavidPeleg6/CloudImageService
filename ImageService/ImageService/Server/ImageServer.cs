using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using ImageService.Infrastructure.Event;
using System;
using ImageService.Logging.Modal;
using ImageService.Infastructure.Event;

namespace ImageService.Server
{
    /// <summary>
    /// A server that gets commands from clients and process them according to internal logic.
    /// </summary>
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        private Communication.Server com_server;
        #endregion

        #region Properties
        /// <summary>
        /// The event that notifies about a new Command being recieved
        /// </summary>
        public event EventHandler<ClientCommandEventArgs> CommandRecieved;
        #endregion

        /// <summary>
        /// Constructs a new Image server object.
        /// </summary>
        /// <param name="log">The logger that will log the servers actions.</param>
        public ImageServer(ILoggingService log)
        {
            m_logging = log;
            m_controller = new ImageController(new ImageServiceModal());
            com_server = Communication.Server.GetServer();
            com_server.ClHandler.ClientCommandRecieved += ClientCommand;
        }

        /// <summary>
        /// Adds a directory at the given path.
        /// </summary>
        /// <param name="path">The directory's path.</param>
        public void AddDirectory(string path)
        {
            IDirectoryHandler Handler = new DirectoyHandler(m_logging, m_controller);
            if (Handler.StartHandleDirectory(path))
            {
                CommandRecieved += Handler.OnCommandRecieved;
                //Handler.LogChanged += com_server.ClHandler.OnLogChange; //TODO: ... something
                Handler.CommandDone += com_server.ClHandler.CommandDone;
            }
        }
        /// <summary>
        /// A function that is called when a directory is closed and shoudln't be listened to anymore.
        /// </summary>
        /// <param name="sender">The DirectoyHandler, is made to stop listening.</param>
        /// <param name="args">not used by this function</param>
        public void DirectoryClosed(object sender, DirectoryCloseEventArgs args)
        {
            CommandRecieved -= (sender as DirectoyHandler).OnCommandRecieved;
            (sender as DirectoyHandler).CommandDone -= com_server.ClHandler.CommandDone;
            //(sender as DirectoyHandler).LogChanged -= com_server.ClHandler.OnLogChange; //TODO: ... something
        }
        /// <summary>
        /// Passes a command to the client.
        /// </summary>
        /// <param name="sender">This server.</param>
        /// <param name="args">The commandargs to be passed to the client.</param>
        public void ClientCommand(object sender, ClientCommandEventArgs args)
        {
            CommandRecieved(this, args);
        }
        /// <summary>
        /// Closes the server.
        /// </summary>
        public void CloseServer()
        {
            ClientCommandEventArgs Args = new ClientCommandEventArgs(null, 
                new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, "*"));
            CommandRecieved?.Invoke(this, Args);
            com_server.ServerStop();
        }
       
    }
}
