using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace SummerProjectWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// This bool var is used for deciding whether the add or the minus button shall be toggled.
        /// Set to false, if "Add"-button shall be toggled at the beginning.
        /// Note: Exclude this to config file later!
        /// </summary>
        private static bool minusToggled = false;

        /*
         * Image resources for toggling the buttons
         */
        BitmapImage setAdd = new BitmapImage(
                    new Uri("/Images/Add.png", UriKind.Relative));

        BitmapImage setMinus = new BitmapImage(
                    new Uri("/Images/Minus.png", UriKind.Relative));

        BitmapImage unsetAdd = new BitmapImage(
            new Uri("/Images/Add_untoggled.png", UriKind.Relative));        

        BitmapImage unsetMinus = new BitmapImage(
            new Uri("/Images/Minus_untoggled.png", UriKind.Relative));

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Logic which is run when user navigates to MainPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // in case that HomeScreen shall be shown with minus toggled first, change images
            if (MainPage.minusToggled)
            {
                this.addImg.Source = this.unsetAdd;
                this.minusImg.Source = this.setMinus;
            }
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        /// <summary>
        /// Toggles the toggleButtons with a little help of a static bool var.
        /// Checks the static var and toggles if needed.
        /// </summary>
        /// <param name="sender">The sending toggleButton</param>
        /// <param name="e">Routed event args which contain the original sender</param>
        private void Toggle(object sender, RoutedEventArgs e)
        {
            // toggle add button, untoggle minus button
            if (MainPage.minusToggled && (sender as Button).Name == "addButton")
            {
                this.addImg.Source = null;
                this.addImg.Source = setAdd;

                this.minusImg.Source = null;
                this.minusImg.Source = this.unsetMinus;

                MainPage.minusToggled = false;
            }
            // toggle minus button
            else if (!MainPage.minusToggled && (sender as Button).Name == "minusButton")
            {
                this.addImg.Source = null;
                this.addImg.Source = unsetAdd;

                this.minusImg.Source = null;
                this.minusImg.Source = setMinus;

                MainPage.minusToggled = true;
            }
        }
    }
}