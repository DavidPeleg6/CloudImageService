using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageService.GUI
{
    /// <summary>
    /// The viewmodel for the settings tab.
    /// Passes information between the model and view. (and vice versa)
    /// </summary>
    public class SettingsTabViewModel : INotifyPropertyChanged
    {
        private ISettingsTabModel Model;
        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectedItem;
        private bool _isButtonEnabled;
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
        /// A boolean indicating if the button should be pressable.
        /// </summary>
        public bool IsButtonEnabled
        {
            get
            {
                return this._isButtonEnabled;
            }
            set
            {
                if (this._isButtonEnabled != value)
                {
                    this._isButtonEnabled = value;
                    OnPropertyChanged("IsButtonEnabled");
                }
            }
        }
        /// <summary>
        /// The item currently selected in the listview in the view.
        /// When it is modified to a non-null value the button is enabled.
        /// When it is modified to a null value the button is disabled.
        /// </summary>
        public string SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                if (this._selectedItem != value)
                {
                    this._selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    if (this._selectedItem != null && this._selectedItem != "")
                        this.IsButtonEnabled = true;
                    else
                        this.IsButtonEnabled = false;
                }
            }
        }
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
        public ObservableCollection<string> HandelerList
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

        private bool CanExecute;
        private ICommand _clickCommand;
        /// <summary>
        /// A command that is executed when the remove button is pressed.
        /// Executes RemoveHandelerAction.
        /// </summary>
        public ICommand RemoveButtonCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => RemoveHandelerAction(), CanExecute));
            }
        }
        /// <summary>
        /// A function that is activated when the remove button is pressed.
        /// Tells the model to remove the currently selected handeler.
        /// </summary>
        public void RemoveHandelerAction()
        {
            this.Model.RemoveHandeler(this.SelectedItem);
            this.IsButtonEnabled = false;
        }
        /// <summary>
        /// Constructor, sets the model to be SettingsTabModel
        /// and makes the remove button executable.
        /// </summary>
        public SettingsTabViewModel()
        {
            this.Model = new SettingsTabModel();
            this.CanExecute = true;
        }
    }
}
