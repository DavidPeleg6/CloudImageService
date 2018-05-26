using ImageService.Modal;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Event;
using ImageService.Infastructure.Event;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// A class that handles directories.
    /// </summary>
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController MyController;              // The Image Processing Controller
        private ILoggingService MyLogging;
        private FileSystemWatcher[] MyDirectoryWWatcher;             // The Watcher of the Directory
        private string MyPath;                              // The Path of directory
        #endregion
        /// <summary>
        /// The Event That Notifies that the Directory is being closed
        /// </summary>
        public event EventHandler<CommandDoneEventArgs> CommandDone;
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClosing;
        public event EventHandler<LogChangedEventArgs> LogChanged;
        /// <summary>
        /// Contructs a new DirectoyHandler instance.
        /// </summary>
        /// <param name="logger">The logger that will log the handelers actions.</param>
        /// <param name="controller">The controller that the handeler will use.</param>
        public DirectoyHandler(ILoggingService logger, IImageController controller)
        {
            this.MyDirectoryWWatcher = new FileSystemWatcher[4];
            this.MyLogging = logger;
            this.MyController = controller;
        }

        /// <summary>
        /// method to start listening on a given directory
        /// </summary>
        /// <param name="dirPath">path to given directory</param>
        public bool StartHandleDirectory(string dirPath)
        {
            this.MyPath = dirPath;
            if (!System.IO.Directory.Exists(dirPath))
            {
                this.MyLogging.Log("Directory does not exist: " + dirPath, MessageTypeEnum.FAIL);
                UpdateLog("Directory does not exist: " + dirPath, MessageTypeEnum.FAIL);
                LogChanged?.Invoke(this, new LogChangedEventArgs("Directory does not exist " + dirPath, MessageTypeEnum.FAIL));
                return false;
            }
            //set the watchers and filter appropriate types so not every change event invokes the watcher
            MyDirectoryWWatcher[0] = new FileSystemWatcher(dirPath, "*.jpg");
            MyDirectoryWWatcher[1] = new FileSystemWatcher(dirPath, "*.gif");
            MyDirectoryWWatcher[2] = new FileSystemWatcher(dirPath, "*.bmp");
            MyDirectoryWWatcher[3] = new FileSystemWatcher(dirPath, "*.png");
            //set event
            foreach(FileSystemWatcher watcher in MyDirectoryWWatcher)
            {
                watcher.Created += new FileSystemEventHandler(OnCreated);
                //watcher.Changed += new FileSystemEventHandler(OnCreated);
                watcher.EnableRaisingEvents = true;
            }
            this.MyLogging.Log("Watching directory: " + dirPath, MessageTypeEnum.INFO);
            UpdateLog("Watching directory: " + dirPath, MessageTypeEnum.INFO);
            LogChanged?.Invoke(this, new LogChangedEventArgs("Watching directory " + dirPath, MessageTypeEnum.INFO));
            return true;
        }
       
        /// <summary>
        /// the event to occur on new object creation
        /// </summary>
        /// <param name="source">the file system watcher</param>
        /// <param name="e">path of new photo</param>
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            //set args for command
            string[] Args = new string[1];
            Args[0] = e.FullPath;
            bool Result = true;
            Task<string> NewFileTask = new Task<string>(() => {
                string message = this.MyController.ExecuteCommand((int)CommandEnum.NewFileCommand, Args, out Result);
                return message;
            });
            NewFileTask.Start();
            NewFileTask.Wait();
            //check if successful and write to log
            if (!Result)
            {
                this.MyLogging.Log(NewFileTask.Result, MessageTypeEnum.FAIL);
                UpdateLog(NewFileTask.Result, MessageTypeEnum.FAIL);
                LogChanged?.Invoke(this, new LogChangedEventArgs("copy failed " + e.FullPath, MessageTypeEnum.FAIL));
                return;
            }
            this.MyLogging.Log(NewFileTask.Result, MessageTypeEnum.INFO);
            UpdateLog(NewFileTask.Result, MessageTypeEnum.INFO);
            LogChanged?.Invoke(this, new LogChangedEventArgs("copy succeeded " + e.FullPath, MessageTypeEnum.INFO));
        }

        /// <summary>
        /// method to be activated when command enters
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e"> args for the command</param>
        public void OnCommandRecieved(object sender, ClientCommandEventArgs e)
        {
            //check which command was given and execute in a new Task
            Task t = new Task(() =>
            {
                bool result;
                string output;
                try
                {
                    output = MyController.ExecuteCommand(e.Args.CommandID, e.Args.Args, out result);
                }
                catch
                {
                    output = "";
                }
                if (e.Args.CommandID == (int)CommandEnum.CloseCommand)
                {
                    if (e.Args.RequestDirPath == MyPath || String.Equals("*", e.Args.RequestDirPath))
                    {
                        //CommandDone?.Invoke(this, new CommandDoneEventArgs(e.Client, output));
                        // return;

                        for (int i = 0; i < MyDirectoryWWatcher.Length; i++)
                        {
                            MyDirectoryWWatcher[i].EnableRaisingEvents = false;
                            MyDirectoryWWatcher[i].Dispose();
                        }
                        UpdateLog("directory closed: " + MyPath, MessageTypeEnum.INFO);
                        MyLogging.Log("directory closed: " + MyPath, MessageTypeEnum.INFO);
                        DirectoryClosing?.Invoke(this, new DirectoryCloseEventArgs(null, null));
                        CommandDone?.Invoke(this, new CommandDoneEventArgs(e.Client, output));
                        return;
                    }
                }
                CommandDone?.Invoke(this, new CommandDoneEventArgs(e.Client, output));
            });
            t.Start();
        }
        /// <summary>
        /// Updates the log to include a new messege.
        /// </summary>
        /// <param name="message">The messege to be logged.</param>
        /// <param name="type">The type of messege to be saved.</param>
        public void UpdateLog(string message, MessageTypeEnum type)
        {
            string[] args = new string[2];
            bool Result;
            args[0] = ((int)type).ToString();
            args[1] = message;
            this.MyController.ExecuteCommand((int)CommandEnum.LogCommand, args, out Result);
        }
    }
}
