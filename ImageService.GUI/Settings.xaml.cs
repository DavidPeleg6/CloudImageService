using System.Windows;
using System.Windows.Controls;

namespace ImageService.GUI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            AddToList("a"); AddToList("b"); AddToList("c");
            AddToList("a"); AddToList("b"); AddToList("c");
        }
        /// <summary>
        /// Updates the button to be pressable if there are any options selected.
        /// </summary>
        /// <param name="sender">The mouse..?</param>
        /// <param name="e">The event specifing that the selection was changed.</param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem l;
            for (int i = 0; i < HandelerList.Items.Count; i++)
            {
                l = (ListBoxItem)HandelerList.Items.GetItemAt(i);
                if (l.IsSelected)
                {
                    bool ServerConfirmedRemoval = true;
                    // TODO: send command to remove the item to the server, only remove if the server responded, otherwise error.
                    //ServerConfirmedDeletion=RequestDeletion(l.content);
                    if (ServerConfirmedRemoval)
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
        /// Updates when the user chooses an item from the list, used to enable the button.
        /// </summary>
        /// <param name="sender">The mouse..?</param>
        /// <param name="e">Nothing important.</param>
        private void HandelerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HandelerList.SelectedIndex != -1)
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
    }
}
