using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using BusinessLayer;

namespace BusinessLayer
{

    public class Colour : BaseBusinessObject
    {
        public Colour()
        {

        }

        public string Name { get; set; }
    }
}
