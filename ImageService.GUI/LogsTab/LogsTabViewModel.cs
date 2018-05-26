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
    /// The viewmodel of the Logs tab.
    /// Passes information between the view and model.
    /// </summary>
    class LogsTabViewModel
    {
        private ILogsTabModel Model;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Executes when one of the models properties changes.
        /// </summary>
        /// <param name="propertyName">The property that changed</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// The list of logs.
        /// Stored in the model.
        /// </summary>
        public ObservableCollection<LogData> LogList
        {
            get
            {
                return Model.LogList;
            }
            set
            {
                Model.LogList = value;
                OnPropertyChanged("LogList");
            }
        }

        private bool CanExecute;
        private ICommand _refreshButtonCommand;
        /// <summary>
        /// A command that is executed when the refresh button is pressed.
        /// Executes RefreshButtonAction.
        /// </summary>
        public ICommand RefreshButtonCommand
        {
            get
            {
                return _refreshButtonCommand ?? (_refreshButtonCommand = new CommandHandler(() => RefreshButtonAction(), CanExecute));
            }
        }
        /// <summary>
        /// Requests the model to refresh its log list.
        /// </summary>
        public void RefreshButtonAction()
        {
            this.Model.RefreshButtonPress();
            //TODO: check here
        }
        /// <summary>
        /// Constructor,
        /// Sets the model to be LogsTabModel and sets executability of the refresh command to be true.
        /// </summary>
        public LogsTabViewModel()
        {
            this.Model = new LogsTabModel();
            CanExecute = true;
        }
    }
}
