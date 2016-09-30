using System;
using System.Collections.Generic;

namespace BusinessLayer
{

    /// <summary>
    ///     Represents a Cap Product
    /// </summary>
    public class Cap :BaseBusinessObject
    {
        public Cap()
        {
            
        }

        /// <summary>
        ///     Name of Cap
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Cost of Cap
        /// </summary>
        public Single Price { get; set; }

        /// <summary>
        ///     Description of cap
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Server URL of image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        ///     ID of supplier
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     ID of Category
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///     Reference to category object
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        ///     Reference to supplier object.
        /// </summary>
        public virtual Supplier Supplier { get; set; }
    }
}
