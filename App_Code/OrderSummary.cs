using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class OrderSummary
    {
        public int OrderId { get; set; }

        public int TotalQuantity { get; set; }

        public double TotalPrice { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
