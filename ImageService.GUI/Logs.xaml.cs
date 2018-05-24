using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ImageService.Logging.Modal;
using Communication.Client;
using ImageService.Infrastructure.Event;
using ImageService.Infastructure.Event;
using Newtonsoft.Json.Linq;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json;
using ImageService.Infastructure;

namespace ImageService.GUI
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// A window that display log information to the user.
    /// </summary>
    public partial class Logs : Window
    {
        ObservableCollection<LogData> ListItems;
        private Client LocalClient;
        private bool AnswerRecieved;
        private string Answer;
        public event EventHandler<CommandRecievedEventArgs> SendCommand;
        /// <summary>
        /// Constructor, makes a log window and loads log data from the server via the client.
        /// If the log data cannot be loaded the log "Connection error, failed to load data." is displaied instead.
        /// </summary>
        public Logs()
        {
            InitializeComponent();
            ListItems = new ObservableCollection<LogData>();
            LogList.ItemsSource = ListItems;
            LocalClient = Client.GetInstance();
            if (!LocalClient.GetStatus())
            {
                AddLog(MessageTypeEnum.FAIL, "Connection error, failed to load data.");
                return;
            }
            SendCommand += LocalClient.CommandRecieved;
            LocalClient.CommandDone += CommandDone;
            CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null);
            AnswerRecieved = false;
            SendCommand(this, args);
            while (!AnswerRecieved);
            JObject obj = JObject.Parse(Answer);
            List<ISPair> htmlAttributes = JsonConvert.DeserializeObject<List<ISPair>>((string)obj["Dict"]);
            foreach (ISPair entry in htmlAttributes)
            {
                AddLog(entry.Type, entry.Message);
            }
        }
        
        /// <summary>
        /// Adds a log to the log list display.
        /// </summary>
        /// <param name="type">The type of log, 0 for INFO, 1 for ERROR, 2 for WARNING</param>
        /// <param name="messege">The messege itself that was logged.</param>
        private void AddLog(MessageTypeEnum type, string messege)
        {
            ListItems.Add(new LogData(type, messege));
        }
        /// <summary>
        /// Function that is called when a command has been completed by the server,
        /// if the action was a log request, the logs screen responds to it by parsing the logs and displaying them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void CommandDone(object sender, CommandDoneEventArgs args)
        {
            JObject JSONObjectLogs = JObject.Parse(args.Message);
            if ((string)JSONObjectLogs["type"] == "log")
            {
                Answer = args.Message;
                AnswerRecieved = true;
            }
        }
        /// <summary>
        /// A button that refreshes the log list.
        /// </summary>
        /// <param name="sender">The mouse?...</param>
        /// <param name="e">don't know what this is but I don't use it anyways.</param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LogList.ItemsSource = null;
            ListItems = new ObservableCollection<LogData>();
            LogList.ItemsSource = ListItems;
            LocalClient = Client.GetInstance();
            if (!LocalClient.GetStatus())
            {
                AddLog(MessageTypeEnum.FAIL, "Connection error, failed to load data.");
                return;
            }
            SendCommand += LocalClient.CommandRecieved;
            LocalClient.CommandDone += CommandDone;
            CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null);
            AnswerRecieved = false;
            SendCommand(this, args);
            while (!AnswerRecieved);
            JObject obj = JObject.Parse(Answer);
            List<ISPair> htmlAttributes = JsonConvert.DeserializeObject<List<ISPair>>((string)obj["Dict"]);
            foreach (ISPair entry in htmlAttributes)
            {
                AddLog(entry.Type, entry.Message);
            }
        }
    }
}
