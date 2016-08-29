using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using BusinessLayer;

namespace BusinessLayer
{

    public class Category : BaseBusinessObject
    {
        public Category()
        {
            
        }

        public string Name { get; set; }
    }
}
