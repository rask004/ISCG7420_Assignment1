using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    /// <summary>
    ///     Represents a Supplier
    /// </summary>
    public class Supplier : BaseBusinessObject
    {
        /// <summary>
        ///  Name of Supplier
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mobile Phone Number of Supplier
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Work Phone Number of Supplier
        /// </summary>
        public string WorkNumber { get; set; }

        /// <summary>
        /// Home Phone Number of Supplier
        /// </summary>
        public string HomeNumber { get; set; }

        /// <summary>
        ///  Email of Supplier
        /// </summary>
        public string Email { get; set; }
    }
}
