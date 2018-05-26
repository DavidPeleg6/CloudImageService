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
    public partial class SettingsTabView : UserControl
    {
        /// <summary>
        /// Constructor,
        /// loads the components and sets the datacontext to SettingsTabViewModel.
        /// </summary>
        public SettingsTabView()
        {
            InitializeComponent();
            this.DataContext = new SettingsTabViewModel();
        }
    }
}
