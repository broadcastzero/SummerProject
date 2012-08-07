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
    using System.IO.IsolatedStorage;
    using System.Windows.Resources;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class is responsible for saving and loading data to/from the database.
    /// </summary>
    public class DataBaseManager
    {
        public string ConnectionString = "Data Source=isostore:/ListItemsDB.sdf";

        /// <summary>
        /// Initiates a new instance of the DataBaseManager
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dbName"></param>
        public DataBaseManager()
        {
        }

        public void CreateDatabase()
        {
            using (var db = new ListItemContext(this.ConnectionString))
            {
                if (!db.DatabaseExists())
                {
                    db.CreateDatabase();
                }
            }
        }

        /// <summary>
        /// Gets all ListItems from the database
        /// </summary>
        /// <returns></returns>
        public IList<ListItemClass> GetItems()
        {
            List<ListItemClass> items = new List<ListItemClass>();
            using (var db = new ListItemContext(ConnectionString))
            {
                var query = from e in db.ListItems
                            select e;
                items = query.ToList();
            }

            return items;
        }

        /// <summary>
        /// Adds a new ListItem to the database
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ListItemClass item)
        {
            using (var db = new ListItemContext(ConnectionString))
            {
                db.ListItems.InsertOnSubmit(item);
                db.SubmitChanges();
            }
        }
    }
}
