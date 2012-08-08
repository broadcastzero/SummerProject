namespace SummerProjectWp7.BL
{
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
    using SummerProjectWp7.DAL;
    using SummerProjectWp7.UserExceptions;
using System.Collections.Generic;

    /// <summary>
    /// This class is responsible for managing and updating the list view. 
    /// It gets values out of the database and fills and refreshes the list.
    /// </summary>
    public class ListItemManager
    {
        /// <summary>
        /// Validates the entry params and sends the business object to the DAL
        /// </summary>
        /// <param name="entry"></param>
        public void SaveNewEntry(ListItemClass entry, ListBox listbox)
        {
            // instance of argument provided?
            if (entry == null)
            {
                throw new ArgumentException("No argument provided");
            }

            // check provided values
            double output;

            if (!Double.TryParse(entry.Amount.ToString(), out output) || entry.Amount < 0)
            {
                throw new ArgumentException("Please check amount!");
            }
            else if (entry.Category == null || entry.Category.Length == 0)
            {
                throw new ArgumentException("Please check Category!");
            }
            else if (entry.Description == null)
            {
                entry.Description = string.Empty; // is allowed to be empty, but not null
            }

            // add current datetime
            entry.SaveDate = DateTime.Today;

            // save to database
            DataBaseManager manager = new DataBaseManager();
            manager.AddItem(entry);

            listbox.ItemsSource = manager.GetItems();
        }

        /// <summary>
        /// Refreshes the list view.
        /// </summary>
        /// <param name="month">The month which shall be loaded (this month - param)</param>
        public IList<ListItemClass> RefreshList(int month)
        {
            DataBaseManager dbm = new DataBaseManager();
            return dbm.GetItems();
        }
    }
}
