using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.GUI
{
    /// <summary>
    /// An interface for the settings tab.
    /// Specifies that objects implementing it should store all of the
    /// settings data the settings window needs to display.
    /// Also specifies that it should be able to remove handelers from the list.
    /// </summary>
    interface ISettingsTabModel : INotifyPropertyChanged
    {
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string ThumbnailSize { get; set; }
        ObservableCollection<string> HandelerList { get; set; }
        void RemoveHandeler(string handeler);
    }
}
