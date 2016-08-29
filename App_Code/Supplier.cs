using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Supplier : BaseBusinessObject
    {
        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }
    }
}
