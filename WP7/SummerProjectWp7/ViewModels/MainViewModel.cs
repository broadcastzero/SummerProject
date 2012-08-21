namespace SummerProjectWp7
{
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
    using SummerProjectWp7.BL;

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            MainViewModel.CategoryList = new ObservableCollection<CategoryViewModel>();
            MainViewModel.MonthList = new List<Month>();
        }

        /// <summary>
        /// A collection for CategoryViewModel objects.
        /// </summary>
        public static ObservableCollection<CategoryViewModel> CategoryList { get; private set; }

        public static List<Month> MonthList { get; private set; }

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
            MainViewModel.CategoryList.Add(new CategoryViewModel() { Category = "Food" });
            MainViewModel.CategoryList.Add(new CategoryViewModel() { Category = "Beer" });

            // Load months            
            MainViewModel.MonthList.Add(new Month { Number = 1, Name = "January" });
            MainViewModel.MonthList.Add(new Month { Number = 2, Name = "February" });
            MainViewModel.MonthList.Add(new Month { Number = 3, Name = "March" });
            MainViewModel.MonthList.Add(new Month { Number = 4, Name = "April" });
            MainViewModel.MonthList.Add(new Month { Number = 5, Name = "May" });
            MainViewModel.MonthList.Add(new Month { Number = 6, Name = "June" });
            MainViewModel.MonthList.Add(new Month { Number = 7, Name = "July" });
            MainViewModel.MonthList.Add(new Month { Number = 8, Name = "August" });
            MainViewModel.MonthList.Add(new Month { Number = 9, Name = "September" });
            MainViewModel.MonthList.Add(new Month { Number = 10, Name = "October" });
            MainViewModel.MonthList.Add(new Month { Number = 11, Name = "November" });
            MainViewModel.MonthList.Add(new Month { Number = 12, Name = "December" });

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