using System;
using System.Collections.Generic;

namespace BusinessLayer
{

    public class Cap :BaseBusinessObject
    {
        public Cap()
        {
            
        }

        public string Name { get; set; }

        public Single Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
