using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    public class CustomerOrder :BaseBusinessObject
    {
        public CustomerOrder()
        {
            
        }

        public int UserId { get; set; }

        public string Status { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime DatePlaced { get; set; }
    }
}
