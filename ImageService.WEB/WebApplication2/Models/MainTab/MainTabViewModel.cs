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
    /// The viewmodel for the main window of the website.
    /// Acts as a seperation layer between the view and the model.
    /// Gets info from the model and passes it over to the view.
    /// </summary>
    public class MainTabViewModel
    {
        private IMainTabModel Model;
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
        #region Getters and setters
        /// <summary>
        /// A property specifing if the service is running or not.
        /// Specifies if the model can aquire data.
        /// </summary>
        public string Running
        {
            get
            {
                if (Model.Running)
                    return "Online";
                else
                    return "Offline";
            }
        }
        /// <summary>
        /// The number of images the service processed since it was launched.
        /// </summary>
        public int ImageCount
        {
            get
            {
                return Model.ImageCount;
            }
            set
            {
                if (Model.ImageCount != value)
                {
                    Model.ImageCount = value;
                    OnPropertyChanged("ImageCount");
                }
            }
        }
        /// <summary>
        /// The name of one of the developers.
        /// </summary>
        public string Name1
        {
            get
            {
                return Model.Name1;
            }
            set
            {
                if (Model.Name1 != value)
                {
                    Model.Name1 = value;
                    OnPropertyChanged("Name1");
                }
            }
        }
        /// <summary>
        /// The name of the other developer.
        /// </summary>
        public string Name2
        {
            get
            {
                return Model.Name2;
            }
            set
            {
                if (Model.Name2 != value)
                {
                    Model.Name2 = value;
                    OnPropertyChanged("Name2");
                }
            }
        }
        /// <summary>
        /// The ID of one of the developers.
        /// </summary>
        public string ID1
        {
            get
            {
                return Model.ID1;
            }
            set
            {
                if (Model.ID1 != value)
                {
                    Model.ID1 = value;
                    OnPropertyChanged("ID1");
                }
            }
        }
        /// <summary>
        /// The ID of the other developer.
        /// </summary>
        public string ID2
        {
            get
            {
                return Model.ID2;
            }
            set
            {
                if (Model.ID2 != value)
                {
                    Model.ID2 = value;
                    OnPropertyChanged("ID2");
                }
            }
        }
        #endregion
        /// <summary>
        /// Constructor, just sets the model to be a MainTabModel.
        /// </summary>
        public MainTabViewModel()
        {
            this.Model = new MainTabModel();
        }
    }
}