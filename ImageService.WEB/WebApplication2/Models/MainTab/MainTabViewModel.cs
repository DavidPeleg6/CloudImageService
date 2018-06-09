using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebApplication2.Models.MainTab
{
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
        public MainTabViewModel()
        {
            this.Model = new MainTabModel();
        }
    }
}