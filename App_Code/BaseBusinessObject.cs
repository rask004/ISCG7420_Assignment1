using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    /// <summary>
    ///     Base Class for a BusinessObject
    /// </summary>
    public class BaseBusinessObject
    {
        public BaseBusinessObject()
        {
        }

        /// <summary>
        ///     Represents the Database Key for this object.
        /// </summary>
        public virtual int Key
        {
            get
            {
                return ID;
            }
        }

        /// <summary>
        ///     Generic Key for objects. 
        /// </summary>
        public int ID { get; set; }
    }
}