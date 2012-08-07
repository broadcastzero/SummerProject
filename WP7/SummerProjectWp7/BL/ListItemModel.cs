namespace SummerProjectWp7.BL
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;

    /// <summary>
    /// This class represents a table of ListViewItems
    /// </summary>
    [Table]
    public class ListItemClass
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column()]
        public bool Outgo { get; set; }
        [Column()]
        public double Amount { get; set; }
        [Column()]
        public string Description { get; set; }
        [Column()]
        public string Category { get; set; }
        [Column()]
        public DateTime SaveDate { get; set; }
    }

    public class ListItemContext : DataContext
    {
        public ListItemContext(string sConnectionString)
            : base(sConnectionString)
        { }

        public Table<ListItemClass> ListItems
        {
            get { return this.GetTable<ListItemClass>(); }
        }
    }
}