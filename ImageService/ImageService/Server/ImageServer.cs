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
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        public ImageServer(ILoggingService log)
        {
            m_logging = log;
            m_controller = new ImageController(new ImageServiceModal());
        }

        public void AddDirectory(string path)
        {
            IDirectoryHandler handler = new DirectoyHandler(m_logging, m_controller);
            CommandRecieved += handler.OnCommandRecieved;
            handler.DirectoryClose += OnDirectoryClose;
        }

        public void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            m_logging.Log("watcher for dir " + e.DirectoryPath + "  is closing", Logging.Modal.MessageTypeEnum.INFO);
        }

        public void CloseServer()
        {
            CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,
                null, "*");
            CommandRecieved(this, args);
        }
       
    }
}
