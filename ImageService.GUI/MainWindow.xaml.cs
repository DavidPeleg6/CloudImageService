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

namespace ImageService.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!true) // TODO: in this point you check if the connection to the server was succsesfulll .
            {
                Background = Brushes.Gray;
            }
            TabItem ti1 = new TabItem();
            TabItem ti2 = new TabItem();
            Settings s = new Settings();
            Logs l = new Logs();
            ti1.Content = s.Content;
            ti1.Header = "Settings";
            ti1.Height = 25;
            ti1.Width = 100;
            ti2.Content = l.Content;
            ti2.Header = "Logs";
            ti2.Height = 25;
            ti2.Width = 100;
            tabControl1.Items.Add(ti1);
            tabControl1.Items.Add(ti2);
        }
    }
}
