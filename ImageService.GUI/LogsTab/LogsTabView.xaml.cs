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
    public partial class LogsTabView : UserControl
    {
        /// <summary>
        /// Constructor,
        /// loads the components and sets the datacontext to LogsTabViewModel.
        /// </summary>
        public LogsTabView()
        {
            InitializeComponent();
            this.DataContext = new LogsTabViewModel();
        }
    }
}
