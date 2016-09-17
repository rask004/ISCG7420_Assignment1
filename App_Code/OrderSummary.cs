using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class OrderSummary
    {
        public static double GstRate
        {
            get { return 0.15; }
        }

        public int OrderId { get; set; }

        public int TotalQuantity { get; set; }

        public double SubTotalPrice { get; set; }

        public double SubTotalGst
        {
            get { return SubTotalPrice * GstRate; }

        }
        public double TotalPrice
        {
            get { return SubTotalPrice * GstRate + SubTotalPrice; }
        }

        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
