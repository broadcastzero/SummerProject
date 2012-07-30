using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SummerProjectWp7
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private string category;
        /// <summary>
        /// A category item - this property is used in the view to display its value using a Binding.
        /// </summary>
        public string Category
        {
            get { return category; }
            set 
            {
                if (value != category)
                {
                    category = value;
                    NotifyPropertyChanged("Category");
                }
            }
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
