using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using System.Security.Permissions;
using ImageService.Commands;


namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher[] m_dirWatcher;             // The Watcher of the Directory
        private string m_path;                              // The Path of directory
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public DirectoyHandler(ILoggingService logger, IImageController controller)
        {
            m_dirWatcher = new FileSystemWatcher[4];
            this.m_logging = logger;
            this.m_controller = controller;
        }

        /// <summary>
        /// method to start listening on a given directory
        /// </summary>
        /// <param name="dirPath">path to given directory</param>
        public void StartHandleDirectory(string dirPath)
        {
            this.m_path = dirPath;
            if (!System.IO.Directory.Exists(dirPath))
            {
                this.m_logging.Log("directory does not exist", MessageTypeEnum.FAIL);
                return;
            }
            //set the watchers and filter appropriate types so not every change event invokes the watcher
            m_dirWatcher[0] = new FileSystemWatcher(dirPath, "*.jpg");
            m_dirWatcher[1] = new FileSystemWatcher(dirPath, "*.gif");
            m_dirWatcher[2] = new FileSystemWatcher(dirPath, "*.bmp");
            m_dirWatcher[3] = new FileSystemWatcher(dirPath, "*.png");
            //set event
            foreach(FileSystemWatcher watcher in m_dirWatcher)
            {
                watcher.Changed += new FileSystemEventHandler(OnCreated);
                watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// the event to occur on new object creation
        /// </summary>
        /// <param name="source">the file system watcher</param>
        /// <param name="e">path of new photo</param>
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            //set args for command
            string[] args = new string[1];
            args[0] = e.FullPath;
            string msg = this.m_controller.ExecuteCommand((int)CommandEnum.NewFileCommand, args, out bool result);
            //check if successful and write to log
            if(!result)
            {
                this.m_logging.Log(msg, MessageTypeEnum.FAIL);
            }
            this.m_logging.Log(msg, MessageTypeEnum.INFO);

        }

        /// <summary>
        /// method to be activated when command enters
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e"> args for the command</param>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            if(!System.IO.Path.Equals(e.RequestDirPath, m_path))
            {
                return;
            }
            //check which command was given
            switch (e.CommandID)
            {
                /*
                case (int)CommandEnum.NewFileCommand:
                    //set args for a new command to image controller
                    bool result;
                    string[] args = new string[1];
                    args[0] = e.RequestDirPath;
                    string msg = this.m_controller.ExecuteCommand((int)CommandEnum.NewFileCommand, args, out result);
                    //update log
                    if(!result)
                    {
                        m_logging.Log(msg, MessageTypeEnum.FAIL);
                    }
                    m_logging.Log(msg, MessageTypeEnum.INFO);
                    break;
                 */
                case (int)CommandEnum.CloseCommand:
                    //stop listening on folder
                    foreach (FileSystemWatcher watcher in m_dirWatcher)
                    {
                        watcher.EnableRaisingEvents = false;
                        watcher.Dispose();
                    }
                    EventHandler<DirectoryCloseEventArgs> closeDir = DirectoryClose;
                    //if directory is closed method
                    if(closeDir != null)
                    {
                        DirectoryCloseEventArgs arg = new DirectoryCloseEventArgs(m_path, "directory closed");
                        closeDir(this, arg);
                    }
                    //update log
                    m_logging.Log("directory closed", MessageTypeEnum.INFO);
                    break;
            }
        }
    }
}
