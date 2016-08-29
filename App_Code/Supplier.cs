using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BusinessLayer
{
    public class Supplier : BaseBusinessObject
    {
        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }
    }
}
