using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    public class OrderItem
    {
        public int OrderId { get; set; }
        
        public int CapId { get; set; }
        
        public int ColourId { get; set; }

        public int Quantity { get; set; }

        public virtual Cap Cap { get; set; }

        public virtual Colour Colour { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
