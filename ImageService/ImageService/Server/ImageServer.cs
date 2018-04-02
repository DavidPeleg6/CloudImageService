using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region Properties
        /// <summary>
        /// The event that notifies about a new Command being recieved
        /// </summary>
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        #endregion
        /// <summary>
        /// Constructs a new Image server object.
        /// </summary>
        /// <param name="log">The logger that will log the servers actions.</param>
        public ImageServer(ILoggingService log)
        {
            m_logging = log;
            m_controller = new ImageController(new ImageServiceModal());
        }
        /// <summary>
        /// Adds a directory at the given path.
        /// </summary>
        /// <param name="path">The directory's path.</param>
        public void AddDirectory(string path)
        {
            IDirectoryHandler Handler = new DirectoyHandler(m_logging, m_controller);
            CommandRecieved += Handler.OnCommandRecieved;
            Handler.DirectoryClose += OnDirectoryClose;
        }
        /// <summary>
        /// A function that is called when a directory is closed.
        /// </summary>
        /// <param name="sender">The sender of the command to close the directory.</param>
        /// <param name="e">The arguements for the directory closing command.</param>
        public void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            m_logging.Log("watcher for dir " + e.DirectoryPath + "  is closing", Logging.Modal.MessageTypeEnum.INFO);
        }
        /// <summary>
        /// Closes the server.
        /// </summary>
        public void CloseServer()
        {
            CommandRecievedEventArgs Args = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,
                null, "*");
            CommandRecieved(this, Args);
        }
       
    }
}
