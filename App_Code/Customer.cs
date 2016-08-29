using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer;

namespace BusinessLayer
{

    /// <summary>
    /// Business Object representing a Customer
    /// </summary>
    public class Customer : Administrator
    {
        public Customer()
        {
            IsDisabled = false;
        }

        public override string UserType
        {
            get { return "C"; }
        }

        public string HomeNumber { get; set; }

        public string WorkNumber { get; set; }

        public string MobileNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StreetAddress { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public bool IsDisabled { get; set; }

    }
}