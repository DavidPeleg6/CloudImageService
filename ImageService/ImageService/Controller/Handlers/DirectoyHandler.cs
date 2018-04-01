﻿using ImageService.Modal;
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
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public DirectoyHandler(ILoggingService logger)
        {
            this.m_logging = logger;
            this.m_controller = new ImageController(new ImageServiceModal());
        }
        //starts the file system watcher
        //returns if directory doesnt exists
        public void StartHandleDirectory(string dirPath)
        {
            this.m_path = dirPath;
            if (!System.IO.Directory.Exists(dirPath))
            {
                return;
            }
            //set the watcher an filter appropriate types
            m_dirWatcher = new FileSystemWatcher(dirPath, "*.jpg");
            m_dirWatcher.Filter = "*.gif";
            m_dirWatcher.Filter = "*.bmp";
            m_dirWatcher.Filter = "*.png";
            m_dirWatcher.Changed += new FileSystemEventHandler(OnCreated);
            m_dirWatcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            bool result;
            string[] args = new string[1];
            args[0] = e.FullPath;
            string msg = this.m_controller.ExecuteCommand(0, args, out result);
            if(result)
            {
                this.m_logging.Log(msg, (MessageTypeEnum)0);
            } else
            {
                this.m_logging.Log(msg, (MessageTypeEnum)2);
            }
        }
    }
}