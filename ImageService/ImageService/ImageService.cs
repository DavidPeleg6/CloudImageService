using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Configuration;
using ImageService.Infrastructure;
using ImageService.Server;

namespace ImageService
{
    /// <summary>
    /// An enum used by the system to identify the state of the service.
    /// </summary>
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }
    [StructLayout(LayoutKind.Sequential)]
    /// <summary>
    /// Provied information about the state of the service.
    /// </summary>
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };
    /// <summary>
    /// A service that runs in the background of the system and services images.
    /// </summary>
    public partial class ImageService : ServiceBase
    {
        private ILoggingService Logging;
        //private EventLog eventLog1;
        //private int EventID = 1;
        private ImageServer Server;
        /// <summary>
        /// Constructs a new imageservice object.
        /// </summary>
        public ImageService()
        {
            InitializeComponent();
            string EventSourceName = ConfigurationManager.AppSettings["SourceName"];
            string LogName = ConfigurationManager.AppSettings["LogName"];
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(EventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(EventSourceName, LogName);
            }
            eventLog1.Source = EventSourceName;
            eventLog1.Log = LogName;
        }
        /// <summary>
        /// The class used by the event for logging.
        /// </summary>
        /// <param name="sender">the sender of the thing</param>
        /// <param name="args">the evernt arguements</param>
        private void C_MessageRecieved(object sender, MessageRecievedEventArgs args)
        {
            EventLogEntryType EventLogType;
            switch (args.Status)
            {
                case MessageTypeEnum.FAIL:
                    EventLogType = EventLogEntryType.Error;
                    break;
                case MessageTypeEnum.INFO:
                    EventLogType = EventLogEntryType.Information;
                    break;
                case MessageTypeEnum.WARNING:
                    EventLogType = EventLogEntryType.Warning;
                    break;
                default:
                    EventLogType = EventLogEntryType.Error;
                    break;
            }
            eventLog1.WriteEntry(args.Message, EventLogType);
        }
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        /// <summary>
        /// A function that is trigged when the service is started.
        /// First it logs the fact that is it starting up the service and also changes its status to SERVICE_START_PENDING
        /// After that, it logs the fact that the service was started and changes its status to SERVICE_RUNNING.
        /// </summary>
        /// <param name="args">The arguements given to the service on startup.</param>
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Starting ImageService.");
            // Update the service state to Start Pending.  
            ServiceStatus ServiceStatus = new ServiceStatus();
            ServiceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            ServiceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref ServiceStatus);
            //START IT UP HERE
            Logging = new LoggingService();
            Logging.MessageRecieved += C_MessageRecieved;
            //TODO write shit normally
            IClientHandler handler = new ClientHandler();
            Server = new ImageServer(Logging ,handler);
            String[] Handelers = ConfigurationManager.AppSettings["Handler"].Split(';');
            for (int i = 0; i < Handelers.Count(); i++)
            {
                Server.AddDirectory(Handelers[i]);
            }
            // Update the service state to Running.  
            ServiceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref ServiceStatus);
            Server.ServerStart();
            eventLog1.WriteEntry("Started ImageService.");
        }
        /// <summary>
        /// A function that is trigged when the service is stopped.
        /// Logs the fact that the service was stopped and changes its status to SERVICE_STOPPED.
        /// </summary>
        protected override void OnStop()
        {
            //eventLog1.WriteEntry("Stopping ImageService.");
            // Update the service state to stop Pending.  
            ServiceStatus ServiceStatus = new ServiceStatus();
            // serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            //serviceStatus.dwWaitHint = 100000;
            //SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            Server.CloseServer();
            //uncomment the above to reinclude 'stop pending' messege
            // Update the service state to stopped.  
            ServiceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref ServiceStatus);
            eventLog1.WriteEntry("Stopped ImageService.");
        }
        /// <summary>
        /// A function to be triggered every time a log entry is written, doesn't do anything for now.
        /// </summary>
        /// <param name="sender">The object that requested the log to be written.</param>
        /// <param name="e">The event args, containing the messege type and messege itself.</param>
        private void EventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}