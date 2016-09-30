using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    /// <summary>
    ///     Represents a Colour
    /// </summary>
    public class Colour : BaseBusinessObject
    {
        public Colour()
        {

        }

        /// <summary>
        ///     Name of the colour
        /// </summary>
        public string Name { get; set; }
    }
}
