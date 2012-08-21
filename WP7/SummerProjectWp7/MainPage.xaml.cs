namespace SummerProjectWp7
{
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
    using System.Text.RegularExpressions;
    using SummerProjectWp7.BL;
    using SummerProjectWp7.UserExceptions;
    using Coding4Fun.Phone.Controls;
    using SummerProjectWp7.Helpers;
    using System.IO.IsolatedStorage;
    using SummerProjectWp7.DAL;

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
                    new Uri("/SummerProjectWp7;component/Images/Add.png", UriKind.Relative));

        BitmapImage setMinus = new BitmapImage(
                    new Uri("/SummerProjectWp7;component/Images/Minus.png", UriKind.Relative));

        BitmapImage unsetAdd = new BitmapImage(
            new Uri("/SummerProjectWp7;component/Images/Add_untoggled.png", UriKind.Relative));

        BitmapImage unsetMinus = new BitmapImage(
            new Uri("/SummerProjectWp7;component/Images/Minus_untoggled.png", UriKind.Relative));

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the category combobox
            DataContext = App.ViewModel;

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Logic which is run when user navigates to MainPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // in case that HomeScreen shall be shown with minus toggled first, change images
            if (MainPage.minusToggled)
            {
                this.addImg.Source = this.unsetAdd;
                this.minusImg.Source = this.setMinus;
            }

            // Load data to list
            //ListItemManager manager = new ListItemManager();
            //manager.RefreshList(this.itemListBox, DateTime.Now.Month);

            base.OnNavigatedTo(e);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
                this.monthPicker.ItemsSource = MainViewModel.MonthList;
                this.monthPicker.SelectedIndex = DateTime.Now.Month - 1;
            }

            //IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;
        }

        /// <summary>
        /// Toggles the input toggleButtons with a little help of a static bool var.
        /// Checks the static var and toggles if needed.
        /// </summary>
        /// <param name="sender">The sending toggleButton</param>
        /// <param name="e">Routed event args which contain the original sender</param>
        private void ToggleInput(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Checks if amount input textbox is a valid decimal number everytime a button is pressed
        /// (must contain not more than one dot and not more than two numbers after point).
        /// </summary>
        /// <param name="sender">The sending textbox</param>
        /// <param name="e">The KeyEventArgs</param>
        private void ValidateAmountInput(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            // do not allow more than one dot
            if (tbx.Text.Contains('.') && e.PlatformKeyCode == 190)
            {
                e.Handled = true;
            }
            // do not allow more than two numbers after dot
            else if(tbx.Text.Contains('.') && (tbx.Text.Length - tbx.Text.IndexOf('.')) > 2)
            {
               e.Handled = true;
            }
        }

        /// <summary>
        /// Saves a new entry to the database
        /// </summary>
        /// <param name="sender">The sending ApplicationBarIconButton</param>
        /// <param name="e">The EventArgs</param>
        private void SaveNewEntry(object sender, EventArgs e)
        {
            // Hide message label and keyboard
            //this.msgLabelInput.Visibility = Visibility.Collapsed;
            this.Focus();

            // Get all values out of the input fields
            ListItemClass entry = new ListItemClass();
            CategoryViewModel model = this.categoryListPicker.SelectedItem as CategoryViewModel;

            entry.Outgo = MainPage.minusToggled;
            entry.Category = model.Category;
            entry.Description = this.descriptionTextBox.Text;
            
            double output;
            bool success = Double.TryParse(this.amountTextBox.Text, out output);

            // Shows message in case of parsing error or if category is empty, does not continue with saving the entry
            if (!success || entry.Category == string.Empty)
            {
                this.ShowMessage( "Please check amount", Colors.Red);
                return;
            }

            entry.Amount = output;

            // Save new entry and show success or error message
            try
            {
                ListItemManager manager = new ListItemManager();
                manager.SaveNewEntry(entry, this.itemListBox);

                // select current month after saving
                this.monthPicker.SelectedIndex = DateTime.Now.Month - 1;
            }
            catch (ArgumentException ex)
            {
                this.ShowMessage(ex.Message, Colors.Red);
                return;
            }
            catch (DataBaseException ex)
            {
                this.ShowMessage( ex.Message, Colors.Red);
                return;
            }

            // Show success message
            this.ShowMessage("Successfully saved", Colors.Green);
        }

        /// <summary>
        /// Shows a message in a toast notification
        /// </summary>
        /// <param name="msg">The message which shall be displayed</param>
        /// <param name="col"></param>
        private void ShowMessage(string msg, Color col)
        {
            ToastPrompt toast = new ToastPrompt();
            toast.Title = "Info";
            toast.Message = msg;
            toast.ImageSource = new BitmapImage(new Uri("ApplicationIcon.png", UriKind.RelativeOrAbsolute));
            toast.Background = new SolidColorBrush(col);

            toast.Show();
        }

        /// <summary>
        /// Shows the settings page
        /// </summary>
        /// <param name="sender">The sending ApplicationBarIconButton</param>
        /// <param name="e">The EventArgs</param>
        private void ShowSettings(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Toggles the month list view (this month / last month)
        /// </summary>
        /// <param name="sender">The sending ToggleButton</param>
        /// <param name="e">The event args</param>
        /*private void ToggleMonth(object sender, RoutedEventArgs e)
        {
            // toggle this month, untoggle last month
            if (this.lastMonthButton.IsChecked.GetValueOrDefault() && (sender as ToggleButton).Name == "thisMonthButton")
            {
                this.lastMonthButton.IsChecked = false;
                this.thisMonthButton.IsChecked = true;

                // refresh list
                ListItemManager lm = new ListItemManager();
                lm.RefreshList(1);
            }
            // toggle last month button
            else if (this.thisMonthButton.IsChecked.GetValueOrDefault() && (sender as ToggleButton).Name == "lastMonthButton")
            {
                this.lastMonthButton.IsChecked = true;
                this.thisMonthButton.IsChecked = false;

                // refresh list
                ListItemManager lm = new ListItemManager();
                lm.RefreshList(0);
            }
            // do not untoggle this month if toggled
            else if (!this.thisMonthButton.IsChecked.GetValueOrDefault() && (sender as ToggleButton).Name == "thisMonthButton")
            {
                this.thisMonthButton.IsChecked = true;
            }
            // do not untoggle last month if toggled
            else if (!this.lastMonthButton.IsChecked.GetValueOrDefault() && (sender as ToggleButton).Name == "lastMonthButton")
            {
                this.lastMonthButton.IsChecked = true;
            }
        }*/

        /// <summary>
        /// Hides keyboard if "enter" is pressed
        /// </summary>
        /// <param name="sender">The sending control</param>
        /// <param name="e">The KeyEventArgs</param>
        private void HideKeyboard(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }
            else
            {
                this.Focus();
            }
        }

        private void ShowSelectedMonth(object sender, SelectionChangedEventArgs e)
        {
            ListItemManager manager = new ListItemManager();
            manager.RefreshList(this.itemListBox, this.monthPicker.SelectedIndex + 1);
        }
    }
}