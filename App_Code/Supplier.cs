using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Supplier : BaseBusinessObject
    {
        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public string WorkNumber { get; set; }

        public string HomeNumber { get; set; }

        public string Email { get; set; }
    }
}
