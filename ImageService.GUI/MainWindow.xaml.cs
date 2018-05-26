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
            Client LocalClient = Client.GetInstance;
            LocalClient.ClientStart();
            if (!LocalClient.GetStatus())
            {
                Background = Brushes.Gray;
            }
            InitializeComponent();
        }
        /// <summary>
        /// This function is here to shut the process down when the window is closed.
        /// </summary>
        /// <param name="e">Not sure what this is, I copied this function from stackoverflow.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
