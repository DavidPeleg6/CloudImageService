using Communication.Client;
using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Enums;
using ImageService.Infrastructure.Event;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageService.WEB.Models
{
    /// <summary>
    /// The model for the settings tab.
    /// Stores data that is displayed by the settings window.
    /// Can also make a call to remove an item from the list of handelelr.
    /// </summary>
    class ConfigTabModel : IConfigTabModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Display data members
        private string ModelOutputDirectory;
        private string ModelSourceName;
        private string ModelLogName;
        private string ModelThumbnailSize;
        private List<HandelerData> ModelHandelerList;
        #endregion
        #region Client communication data members
        private Client LocalClient;
        private bool AnswerRecieved;
        private string Answer;
        public event EventHandler<CommandRecievedEventArgs> SendCommand;
        #endregion
        /// <summary>
        /// A function that is called when a property is changed.
        /// Lets the viewmodel know that a change has occured, which in turn also updates the view.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Getters and setters
        /// <summary>
        /// The directory that the images should be copied to and the thumbnails should be stored in.
        /// </summary>
        public string OutputDirectory {
            get
            {
                return ModelOutputDirectory;
            }
            set
            {
                ModelOutputDirectory = value;
                NotifyPropertyChanged("OutputDirectory");
            }
        }
        /// <summary>
        /// The log source file name.
        /// </summary>
        public string SourceName {
            get
            {
                return ModelSourceName;
            }
            set
            {
                ModelSourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }
        /// <summary>
        /// The name of the log file.
        /// </summary>
        public string LogName {
            get
            {
                return ModelLogName;
            }
            set
            {
                ModelLogName = value;
                NotifyPropertyChanged("LogName");
            }
        }
        /// <summary>
        /// The size of the thumbnails that the service will generate.
        /// </summary>
        public string ThumbnailSize {
            get
            {
                return ModelThumbnailSize;
            }
            set
            {
                ModelThumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }
        /// <summary>
        /// The list of handelers that will be displayed in the GUI.
        /// </summary>
        public List<HandelerData> HandelerList {
            get
            {
                return ModelHandelerList;
            }
            set
            {
                ModelHandelerList = value;
                NotifyPropertyChanged("HandelerList");
            }
        }
        #endregion

        /// <summary>
        /// Constructor,
        /// loads data from the service via the client.
        /// If the server is unreachable, all of the data is set to
        /// "Connection error, failed to load data." instead.
        /// </summary>
        public ConfigTabModel()
        {
            LocalClient = Client.GetInstance;
            ModelHandelerList = new List<HandelerData>();
            if (!LocalClient.GetStatus())
            {
                this.ModelOutputDirectory = "Connection error, failed to load data.";
                this.ModelSourceName = "Connection error, failed to load data.";
                this.ModelLogName = "Connection error, failed to load data.";
                this.ModelThumbnailSize = "Connection error, failed to load data.";
                this.ModelHandelerList.Add(new HandelerData() { Text = "Connection error, failed to load data." });
                return;
            }
            SendCommand += LocalClient.CommandRecieved;
            LocalClient.CommandDone += CommandDone;
            CommandRecievedEventArgs Args = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null);
            AnswerRecieved = false;
            SendCommand(this, Args);
            while (!AnswerRecieved) ;
            JObject Obj = JObject.Parse(Answer);
            this.ModelOutputDirectory = (string)Obj["OutputDir"];
            this.ModelSourceName = (string)Obj["SourceName"];
            this.ModelLogName = (string)Obj["LogName"];
            this.ModelThumbnailSize = (string)Obj["ThumbnailSize"];
            int HandelerCount = (int)Obj["handler amount: "];
            for (int i = 0; i < HandelerCount; i++)
            {
                ModelHandelerList.Add(new HandelerData() { Text = (string)Obj["handler" + i + ": "] });
            }
        }
        /// <summary>
        /// A function that is activated when the server finishes processing the request to close a handleler.
        /// </summary>
        /// <param name="sender">The client, originally the server, originally the service.</param>
        /// <param name="args">Data about what command was finished and how it was finished.</param>
        public void CommandDone(object sender, CommandDoneEventArgs args)
        {
            JObject Obj = JObject.Parse(args.Message);
            if ((string)Obj["type"] == "close command done" || (string)Obj["type"] == "config")
            {
                Answer = args.Message;
                AnswerRecieved = true;
            }
        }
        /// <summary>
        /// Sends a request to the server to remove a handeler, i.e stop listening to a folder.
        /// If the request went trough, the handeler is also removed from the list in the GUI.
        /// </summary>
        /// <param name="handeler">The handeler to be removed.</param>
        /// <returns>True if the removal sucseeded, false otherwise.</returns>
        public bool RemoveHandeler(HandelerData handeler)
        {
            for (int i = 0; i < ModelHandelerList.Count; i++)
            {
                if (ModelHandelerList.ElementAt(i).Text == handeler.Text)
                {
                    String[] HandelerNameArray = { handeler.Text.Replace(@"\\", @"\") };
                    CommandRecievedEventArgs Args = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, HandelerNameArray, handeler.Text);
                    AnswerRecieved = false;
                    SendCommand(this, Args);
                    while (!AnswerRecieved);
                    JObject Obj = JObject.Parse(Answer);
                    string Ans = (string)Obj["type"];
                    if (Ans == "close command done")
                    {
                        ModelHandelerList.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
