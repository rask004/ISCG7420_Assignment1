using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BusinessLayer
{

    public class Cap
    {
        public Cap()
        {
            
        }

        public string name { get; set; }

        public double price { get; set; }

        public string description { get; set; }

        public string imageUrl { get; set; }

        public int supplierId { get; set; }

        public int categoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
