using Communication.Client;
using ImageService.Infastructure;
using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Enums;
using ImageService.Infrastructure.Event;
using ImageService.Logging.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.WEB.Models
{
    /// <summary>
    /// The model for the main window of the website.
    /// Loads data from the server (via the client)
    /// and passes it to the view (via the viewmodel).
    /// </summary>
    public class MainTabModel : IMainTabModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Client communication data members
        private Client LocalClient;
        private bool AnswerRecieved;
        private string Answer;
        public event EventHandler<CommandRecievedEventArgs> SendCommand;
        #endregion
        /// <summary>
        /// Lets the viewmodel know that a property has changed.
        /// </summary>
        /// <param name="propertyName">The property that has changed.</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #region Data
        int ModelImageCount;
        string ModelName1;
        string ModelName2;
        string ModelID1;
        string ModelID2;
        #endregion
        #region Data getters and setters
        /// <summary>
        /// A property specifing if the service is running or not.
        /// Is actually just the status of the client.
        /// Since if the client is running that means that it managed to connect to the server
        /// and from that it follows that the server is running, as well as the service that runs it.
        /// </summary>
        public bool Running
        {
            get
            {
                return LocalClient.GetStatus();
            }
        }
        /// <summary>
        /// The number of images the service processed since it was launched.
        /// </summary>
        public int ImageCount
        {
            get
            {
                return ModelImageCount;
            }
            set
            {
                ModelImageCount = value;
                NotifyPropertyChanged("ImageCount");
            }
        }
        /// <summary>
        /// The name of one of the developers.
        /// </summary>
        public string Name1
        {
            get
            {
                return ModelName1;
            }
            set
            {
                ModelName1 = value;
                NotifyPropertyChanged("Name1");
            }
        }
        /// <summary>
        /// The name of the other developer.
        /// </summary>
        public string Name2
        {
            get
            {
                return ModelName2;
            }
            set
            {
                ModelName2 = value;
                NotifyPropertyChanged("Name2");
            }
        }
        /// <summary>
        /// The ID of one of the developers.
        /// </summary>
        public string ID1
        {
            get
            {
                return ModelID1;
            }
            set
            {
                ModelID1 = value;
                NotifyPropertyChanged("ID1");
            }
        }
        /// <summary>
        /// The ID of the other developer.
        /// </summary>
        public string ID2
        {
            get
            {
                return ModelID2;
            }
            set
            {
                ModelID2 = value;
                NotifyPropertyChanged("ID2");
            }
        }
        #endregion

        /// <summary>
        /// Constructor,
        /// Gets a client and loads data from it.
        /// If the client cannot load data, "connection error" is loaded instead.
        /// </summary>
        public MainTabModel()
        {
            LocalClient = Client.GetInstance;
            if (!LocalClient.GetStatus())
            {
                this.ModelImageCount = 0;
                this.ModelName1 = "Connection error.";
                this.ModelName2 = "Connection error.";
                this.ModelID1 = "Connection error.";
                this.ModelID2 = "Connection error.";
                return;
            }
            SendCommand += LocalClient.CommandRecieved;
            LocalClient.CommandDone += CommandDone;
            CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.GetStatsCommand, null, null);
            AnswerRecieved = false;
            SendCommand(this, args);
            while (!AnswerRecieved);
            JObject Obj = JObject.Parse(Answer);
            this.ModelImageCount = (int)Obj["photo_amount"];
            this.ModelName1 = (string)Obj["Name1"];
            this.ModelName2 = (string)Obj["Name2"];
            this.ModelID1 = (string)Obj["ID1"];
            this.ModelID2 = (string)Obj["ID2"];
        }

        /// <summary>
        /// Function that is called when a command has been completed by the server,
        /// </summary>
        /// <param name="sender">The client.</param>
        /// <param name="args">The data that was sent by the client.</param>
        public void CommandDone(object sender, CommandDoneEventArgs args)
        {
            JObject JSONObjectLogs = JObject.Parse(args.Message);
            if ((string)JSONObjectLogs["type"] == "stats")
            {
                Answer = args.Message;
                AnswerRecieved = true;
            }
        }
    }
}