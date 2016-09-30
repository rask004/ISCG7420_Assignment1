using System;
using System.Collections.Generic;

namespace BusinessLayer
{

    /// <summary>
    ///     Represents a Category
    /// </summary>
    public class Category : BaseBusinessObject
    {
        public Category()
        {
            
        }

        /// <summary>
        ///     Name of category.
        /// </summary>
        public string Name { get; set; }
    }
}
