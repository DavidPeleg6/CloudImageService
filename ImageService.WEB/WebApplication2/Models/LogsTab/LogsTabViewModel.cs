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
        public List<LogData> LogList
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
        /// <summary>
        /// Constructor,
        /// Sets the model to be LogsTabModel and sets executability of the refresh command to be true.
        /// </summary>
        public LogsTabViewModel()
        {
            this.Model = new LogsTabModel();
        }
    }
}
