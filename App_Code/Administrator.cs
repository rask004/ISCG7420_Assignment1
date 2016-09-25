using System;
using System.Collections.Generic;


namespace BusinessLayer
{
    /// <summary>
    /// Business object representing an Administrator
    /// </summary>
    public class Administrator : BaseBusinessObject
    {

        public Administrator()
        {

        }

        /// <summary>
        ///     Email of Administrator
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Login of Administrator
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     HASHED password of Administrator
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     User Type, Administrator is fixed as "A"
        /// </summary>
        public virtual string UserType
        {
            get { return "A"; }
        }

    }
}
