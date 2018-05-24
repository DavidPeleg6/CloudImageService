using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Communication.Client;
using ImageService.Infrastructure.Event;

namespace ImageService.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// The main window of the GUI, other windows are tabs in it.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor, makes a new main window object.
        /// In doing so, it makes new Settings and Logs windows to use as tabs
        /// and also connects to the server via the client in order to request data from it.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Client LocalClient = Client.GetInstance();
            LocalClient.ClientStart();
            if (!LocalClient.GetStatus())
            {
                Background = Brushes.Gray;
            }
            TabItem TabItem1 = new TabItem();
            TabItem TabItem2 = new TabItem();
            Settings SettingsWindow = new Settings();
            Logs LogsWindow = new Logs();
            TabItem1.Content = SettingsWindow.Content;
            TabItem1.Header = "Settings";
            TabItem1.Height = 25;
            TabItem1.Width = 100;
            TabItem2.Content = LogsWindow.Content;
            TabItem2.Header = "Logs";
            TabItem2.Height = 25;
            TabItem2.Width = 100;
            TabControl.Items.Add(TabItem1);
            TabControl.Items.Add(TabItem2);
        }
    }
}
