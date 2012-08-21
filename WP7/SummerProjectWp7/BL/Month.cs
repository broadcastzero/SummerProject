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

namespace SummerProjectWp7.BL
{
    public class Month
    {
        private int number;
        /// <summary>
        /// Gets or sets the number of the month
        /// </summary>
        public int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                if (value > 0 && value < 13)
                    this.number = value;
                else
                    throw new ArgumentException("Number of month");
            }
        }

        /// <summary>
        /// Gets or sets the name of the month
        /// </summary>
        public string Name { get; set; }
    }
}
