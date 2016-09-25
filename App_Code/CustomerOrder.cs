using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    /// <summary>
    ///     Represents a Customer Order
    /// </summary>
    public class CustomerOrder :BaseBusinessObject
    {
        public CustomerOrder()
        {
            
        }

        /// <summary>
        ///     Id of customer
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Status of Order, should be waiting or shipped (lower case);
        /// </summary>
        public string Status
        {
            get { return Status; }
            set { Status = value.ToLower(); }
        }

        /// <summary>
        ///     Reference to customer object, identified by UserId
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        ///     Date the order was placed.
        /// </summary>
        public DateTime DatePlaced { get; set; }
    }
}
