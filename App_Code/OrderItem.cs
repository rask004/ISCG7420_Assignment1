using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    /// <summary>
    ///     Represents one item linked to an order.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        ///     Represents the Composite Key of an OrderItem.
        /// </summary>
        public int[] Key
        {
            get { return new[] {OrderId, CapId, ColourId}; }
        }

        /// <summary>
        ///     Id of associated Order
        /// </summary>
        public int OrderId { get; set; }
        
        /// <summary>
        ///     Id of Associated Cap
        /// </summary>
        public int CapId { get; set; }
        
        /// <summary>
        ///     ID of cap colour
        /// </summary>
        public int ColourId { get; set; }

        /// <summary>
        ///     Quantity of cap
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     Reference to associated Cap
        /// </summary>
        public virtual Cap Cap { get; set; }

        /// <summary>
        ///     Reference to associated Colour
        /// </summary>
        public virtual Colour Colour { get; set; }

        /// <summary>
        ///     Reference to associated Order
        /// </summary>
        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
