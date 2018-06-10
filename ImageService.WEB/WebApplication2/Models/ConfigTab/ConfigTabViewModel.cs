using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageService.WEB.Models
{
    /// <summary>
    /// The viewmodel for the settings tab.
    /// Passes information between the model and view. (and vice versa)
    /// </summary>
    public class ConfigTabViewModel : INotifyPropertyChanged
    {
        private IConfigTabModel Model;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A function that is called when a property changes in the model.
        /// Thus updating the value of the property in the view.
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        //Note, all of these are stored in the model, not locally.
        #region Getters and setters
        /// <summary>
        /// The directory that the images should be copied to and the thumbnails should be stored in.
        /// </summary>
        public string OutputDirectory
        {
            get
            {
                return Model.OutputDirectory;
            }
            set
            {
                if (Model.OutputDirectory != value)
                {
                    Model.OutputDirectory = value;
                    OnPropertyChanged("OutputDirectory");
                }
            }
        }
        /// <summary>
        /// The log source file name.
        /// </summary>
        public string SourceName
        {
            get
            {
                return Model.SourceName;
            }
            set
            {
                if (Model.SourceName != value)
                {
                    Model.SourceName = value;
                    OnPropertyChanged("SourceName");
                }
            }
        }
        /// <summary>
        /// The name of the log file.
        /// </summary>
        public string LogName
        {
            get
            {
                return Model.LogName;
            }
            set
            {
                if (Model.LogName != value)
                {
                    Model.LogName = value;
                    OnPropertyChanged("LogName");
                }
            }
        }
        /// <summary>
        /// The size of the thumbnails that the service will generate.
        /// </summary>
        public string ThumbnailSize
        {
            get
            {
                return Model.ThumbnailSize;
            }
            set
            {
                if (Model.ThumbnailSize != value)
                {
                    Model.ThumbnailSize = value;
                    OnPropertyChanged("ThumbnailSize");
                }
            }
        }
        /// <summary>
        /// The list of handelers that will be displayed in the GUI.
        /// </summary>
        public List<HandelerData> HandelerList
        {
            get
            {
                return Model.HandelerList;
            }
            set
            {
                if (Model.HandelerList != value)
                {
                    Model.HandelerList = value;
                    OnPropertyChanged("HandelerList");
                }
            }
        }
        #endregion

        /// <summary>
        /// A function that is activated when the remove button is pressed.
        /// Tells the model to remove the currently selected handeler.
        /// </summary>
        public bool RemoveHandeler(HandelerData handeler)
        {
            return this.Model.RemoveHandeler(handeler);
        }
        /// <summary>
        /// Constructor, sets the model to be SettingsTabModel
        /// and makes the remove button executable.
        /// </summary>
        public ConfigTabViewModel()
        {
            this.Model = new ConfigTabModel();
        }
    }
}
