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

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual string UserType
        {
            get { return "A"; }
        }

    }
}
