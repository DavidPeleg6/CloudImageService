using Communication.Client;
using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Enums;
using ImageService.Infrastructure.Event;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ImageService.GUI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// The window that displays the services settings, is used as a tab in the main window.
    /// </summary>
    public partial class Settings : Window
    {
        private Client client;
        private bool AnswerRecieved;
        private string Answer;
        public event EventHandler<CommandRecievedEventArgs> SendCommand;
        /// <summary>
        /// Constructs a settings window, is called when the GUI is loaded and is displayed trough the mainwindow class.
        /// Attempt to load config information from the server using the client,
        /// sets the value of all of its visual componment to "Connection error, failed to load data." if it couldn't.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            client = Client.GetInstance();
            if (!client.GetStatus())
            {
                OutputDirLabel.Content = "Connection error, failed to load data.";
                SourceNameLabel.Content = "Connection error, failed to load data.";
                LogNameLabel.Content = "Connection error, failed to load data.";
                ThumbnailLabel.Content = "Connection error, failed to load data.";
                AddToList("Connection error, failed to load data.");
                return;
            }
            SendCommand += client.CommandRecieved;
            client.CommandDone += CommandDone;
            CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null);
            AnswerRecieved = false;
            SendCommand(this, args);
            while (!AnswerRecieved);
            JObject obj = JObject.Parse(Answer);
            OutputDirLabel.Content = (string)obj["OutputDir"];
            SourceNameLabel.Content = (string)obj["SourceName"];
            LogNameLabel.Content = (string)obj["LogName"];
            ThumbnailLabel.Content = (string)obj["ThumbnailSize"];
            int handeler_count = (int)obj["handler amount: "];
            for (int i = 0; i < handeler_count; i++)
            {
                AddToList((string)obj["handler" + i + ": "]);
            }
        }
        /// <summary>
        /// Sends a request to the server to remove a handeler, i.e stop listening to a folder.
        /// If the request went trough, the handeler is also removed from the list in the GUI,
        /// otherwise an appropriate error message is displaied.
        /// </summary>
        /// <param name="sender">The mouse..?</param>
        /// <param name="e">Dont know, but I don't use it anyways.</param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem l;
            for (int i = 0; i < HandelerList.Items.Count; i++)
            {
                l = (ListBoxItem)HandelerList.Items.GetItemAt(i);
                if (l.IsSelected)
                {
                    String[] fuck = { l.Content.ToString() };//TODO: maybe not
                    CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, fuck , l.Content.ToString()); //TODO: maybe return the one below instead
                    //CommandRecievedEventArgs args = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, l.Content.ToString());
                    AnswerRecieved = false;
                    SendCommand(this, args);
                    while (!AnswerRecieved) ;
                    JObject obj = JObject.Parse(Answer);
                    string ans = (string)obj["type"];
                    
                    if (ans == "close command done")
                    {
                        HandelerList.Items.RemoveAt(i);
                        RemoveButton.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Communication error, handeler removal failed.");
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// Updates when the user chooses an item from the list, used to enable the remove button.
        /// </summary>
        /// <param name="sender">The mouse..?</param>
        /// <param name="e">Nothing important.</param>
        private void HandelerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HandelerList.SelectedIndex != -1 && client.GetStatus())
                RemoveButton.IsEnabled = true;
        }
        /// <summary>
        /// Adds an item to the handeler list, string only.
        /// </summary>
        /// <param name="s">The string to be added to the list.</param>
        private void AddToList(string s)
        {
            HandelerList.Items.Add(new ListBoxItem { Content = s });
        }
        /// <summary>
        /// A function that is activated when the server finishes processing the request to close a handleler.
        /// </summary>
        /// <param name="sender">The client, originally the server, originally the service.</param>
        /// <param name="args">Data about what command was finished and how it was finished.</param>
        public void CommandDone(object sender, CommandDoneEventArgs args)
        {
            JObject obj = JObject.Parse(args.Message);
            if ((string)obj["type"] == "close command done" || (string)obj["type"] == "config")
            {
                Answer = args.Message;
                AnswerRecieved = true;
            }
        }
    }
}
