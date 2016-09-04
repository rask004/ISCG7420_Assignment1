using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    /// <summary>
    ///     Summary description for BaseBusinessObject
    /// </summary>
    public class BaseBusinessObject
    {
        public BaseBusinessObject()
        {
        }

        public int Key
        {
            get
            {
                return ID;
            }
        }

        public int ID { get; set; }
    }
}