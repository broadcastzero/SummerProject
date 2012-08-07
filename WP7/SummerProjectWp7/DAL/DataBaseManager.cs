namespace SummerProjectWp7.DAL
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
    using SummerProjectWp7.BL;

    /// <summary>
    /// This class is responsible for saving and loading data to/from the database.
    /// </summary>
    public class DataBaseManager
    {
        /// <summary>
        /// Saves a new entry to the database.
        /// </summary>
        /// <param name="entry">The business object</param>
        public void Save(ListEntry entry)
        {
            App.ListEntries.Add(entry);
        }        
    }
}
