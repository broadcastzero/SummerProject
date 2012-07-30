using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SummerProjectWp7
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            MainViewModel.CategoryList = new ObservableCollection<CategoryViewModel>();
        }

        /// <summary>
        /// A collection for CategoryViewModel objects.
        /// </summary>
        public static ObservableCollection<CategoryViewModel> CategoryList { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            MainViewModel.CategoryList.Add(new CategoryViewModel() { Category = "Lebensmittel" });
            MainViewModel.CategoryList.Add(new CategoryViewModel() { Category = "Bier" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}