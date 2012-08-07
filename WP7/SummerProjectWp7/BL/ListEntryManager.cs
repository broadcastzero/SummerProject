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

    /// <summary>
    /// This class is responsible for managing and updating the list view. 
    /// It gets values out of the database and fills and refreshes the list.
    /// </summary>
    public class ListEntryManager
    {
        /// <summary>
        /// Validates the entry params and sends the business object to the DAL
        /// </summary>
        /// <param name="entry"></param>
        public void SaveNewEntry(ListEntry entry)
        {
            // TODO: check values and move line below to DAL
            App.ListEntries.Add(entry);
        }

        /// <summary>
        /// Refreshes the list view.
        /// </summary>
        /// <param name="month">The month which shall be loaded (this month - param)</param>
        public void RefreshList(int month)
        {
            throw new NotImplementedException();
        }
    }
}
