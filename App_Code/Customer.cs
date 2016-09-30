using System;
using System.Collections.Generic;

namespace BusinessLayer
{

    /// <summary>
    /// Business Object representing a Customer
    /// </summary>
    public class Customer : Administrator
    {
        /// <summary>
        ///     All new customers assumed to have an active account
        /// </summary>
        public Customer()
        {
            IsDisabled = false;
        }

        /// <summary>
        ///     Customer type is fixed to "C"
        /// </summary>
        public override string UserType
        {
            get { return "C"; }
        }

        /// <summary>
        ///     Home Phone Number
        /// </summary>
        public string HomeNumber { get; set; }

        /// <summary>
        ///     Work Phone Number
        /// </summary>
        public string WorkNumber { get; set; }

        /// <summary>
        ///     Mobile Phone Number
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        ///     First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Street Address
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        ///     Suburb
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        ///     City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Whether or not this customer account is Suspended.
        /// </summary>
        public bool IsDisabled { get; set; }

    }
}