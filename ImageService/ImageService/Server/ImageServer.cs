using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using ImageService.Modal.Event;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
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
        private TcpListener listener;
        //TODO write shit normally
        private IClientHandler ch;
        #endregion

        #region Properties
        /// <summary>
        /// The event that notifies about a new Command being recieved
        /// </summary>
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        public event EventHandler<LogChangedEventArgs> LogChange;
        #endregion

        /// <summary>
        /// Constructs a new Image server object.
        /// </summary>
        /// <param name="log">The logger that will log the servers actions.</param>
        //TODO write shit normally
        public ImageServer(ILoggingService log, IClientHandler handler)
        {
            m_logging = log;
            m_controller = new ImageController(new ImageServiceModal());
            this.ch = handler;
            LogChange += handler.OnLogChange;
        }

        public void ServerStart()
        {
            //start listening on a port from app config
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["IPaddress"])
                , int.Parse(ConfigurationManager.AppSettings["port"]));
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
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
                Handler.DirectoryClose += OnDirectoryClose;
                Handler.LogChanged += ch.OnLogChange;
            }
        }
        /// <summary>
        /// A function that is called when a directory is closed.
        /// </summary>
        /// <param name="sender">The sender of the command to close the directory.</param>
        /// <param name="e">The arguements for the directory closing command.</param>
        public void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            m_logging.Log("watcher for dir " + e.DirectoryPath + "  is closing", Logging.Modal.MessageTypeEnum.INFO);
            //TODO write shit normally
            LogChange(this, new LogChangedEventArgs("watcher for dir " + e.DirectoryPath + "  is closing", Logging.Modal.MessageTypeEnum.INFO));
        }

        /// <summary>
        /// Closes the server.
        /// </summary>
        public void CloseServer()
        {
            CommandRecievedEventArgs Args = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,
                null, "*");
            CommandRecieved?.Invoke(this, Args);
            listener.Stop();
        }
       
    }
}
