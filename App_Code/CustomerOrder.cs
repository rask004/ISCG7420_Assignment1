using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    
    public class CustomerOrder :BaseBusinessObject
    {
        public CustomerOrder()
        {
            
        }

        public int userId { get; set; }

        public string status { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
