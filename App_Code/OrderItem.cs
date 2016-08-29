using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    public class OrderItem
    {
        public int orderId { get; set; }
        
        public int capId { get; set; }
        
        public int colourId { get; set; }

        public int quantity { get; set; }

        public virtual Cap Cap { get; set; }

        public virtual Colour Colour { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
