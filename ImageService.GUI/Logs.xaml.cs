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

namespace ImageService.GUI
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        ObservableCollection<LogData> ListItems;
        public Logs()
        {
            InitializeComponent();
            ListItems = new ObservableCollection<LogData>();
            LogList.ItemsSource = ListItems;
            // TODO: load actuall data into listItems
            AddLog(LogDataEnum.INFO, "info");
            AddLog(LogDataEnum.ERROR, "error");
            AddLog(LogDataEnum.WARNING, "warning");
            AddLog(LogDataEnum.INFO, "info2");
            AddLog(LogDataEnum.INFO, "info3");
            AddLog(LogDataEnum.WARNING, "warning2");
            AddLog(LogDataEnum.DISPLAYERROR, "display error");
        }
        /// <summary>
        /// Adds a log to the log list display.
        /// </summary>
        /// <param name="type">The type of log, 0 for INFO, 1 for ERROR, 2 for WARNING</param>
        /// <param name="messege">The messege itself that was logged.</param>
        private void AddLog(LogDataEnum type, string messege)
        {
            ListItems.Add(new LogData(type, messege));
        }
    }
}
