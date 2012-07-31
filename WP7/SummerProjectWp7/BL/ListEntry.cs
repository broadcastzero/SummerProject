namespace SummerProjectWp7.BL
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    public class ListEntry
    {
        /// <summary>
        /// Gets or sets the information, if the list entry is an outgo (true) or an income (false)
        /// </summary>
        public bool Outgo { get; set; }

        private double amount;
        /// <summary>
        /// Gets or sets the amount of money which is earned or spent
        /// </summary>
        public double Amount
        {
            get { return this.amount; }
            set 
            {
                if (value >= 0)
                {
                    this.amount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the description of the income/outgo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the category
        /// </summary>
        public string Category { get; set; }
    }
}
