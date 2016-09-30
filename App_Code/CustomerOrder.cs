using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    /// <summary>
    ///     Represents a Customer Order
    /// </summary>
    public class CustomerOrder :BaseBusinessObject
    {
        private string _status;

        public CustomerOrder()
        {
            _status = "waiting";
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
            get { return _status; }
            set { _status = value.ToLower(); }
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
