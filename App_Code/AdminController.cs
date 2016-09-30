using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Common;
using CommonLogging;
using DataLayer;
using SecurityLayer;

namespace BusinessLayer
{
    /// <summary>
    /// Business Layer controller for Adnin pages.
    /// 
    /// Changelog:
    ///     18-09-16        19:01   AskewR04    created class
    /// </summary>
    public class AdminController
    {
        private readonly DataManager _dm;

        /// <summary>
        ///     Constructor
        ///     Maintain reference to DataManager singleton during lifecycle.
        /// </summary>
        public AdminController()
        {
            // get reference to Data Manager here.
            _dm = DataManager.Instance;

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.AdminController :: AdminController Created.");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        ///     Get all customers.
        /// </summary>
        /// <returns>List, Customer, all customers</returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> items = _dm.GetAllCustomers();

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomers :: Retrieved all Customers.");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        ///     Get first name of a customer
        /// </summary>
        /// <param name="id">integer, id of customer</param>
        /// <returns>string, first name of customer</returns>
        public string GetCustomerFirstName(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            string customerId = "NULL";
            if (customer != null)
            {
                customerId = customer.ID.ToString();
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerFirstName :: Retrieved Single Customer: ");
            builder.Append(customerId);
            builder.Append(" for FirstName");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            if (customer == null)
            {
                return String.Empty;
            }
            return customer.FirstName;
        }

        /// <summary>
        ///     Get last name of customer
        /// </summary>
        /// <param name="id">integer, customer id</param>
        /// <returns>string, last name</returns>
        public string GetCustomerLastName(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            string customerId = "NULL";
            if (customer != null)
            {
                customerId = customer.ID.ToString();
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerLastName :: Retrieved Single Customer: ");
            builder.Append(customerId);
            builder.Append(" for LastName");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            if (customer == null)
            {
                return String.Empty;
            }
            return customer.LastName;
        }

        /// <summary>
        ///     Get login of customer
        /// </summary>
        /// <param name="id">integer, id of customer</param>
        /// <returns>string, login</returns>
        public string GetCustomerLogin(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerLogin :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for Login");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.Login;
        }

        /// <summary>
        ///     get email of costomer
        /// </summary>
        /// <param name="id">integer, id of customer</param>
        /// <returns>string, email</returns>
        public string GetCustomerEmail(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerEmail :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for Email");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.Email;
        }

        /// <summary>
        ///     Get home phone number of customer
        /// </summary>
        /// <param name="id">integer, id of customer</param>
        /// <returns>string, home number</returns>
        public string GetCustomerHomeNumber(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerHomeNumber :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for HomeNumber");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.HomeNumber;
        }

        /// <summary>
        ///     Get customer work number
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <returns>string, work number</returns>
        public string GetCustomerWorkNumber(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerWorkNumber :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for WorkNumber");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.WorkNumber;
        }

        /// <summary>
        ///     Get customer mobile phone number
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <returns>string, mobile number</returns>
        public string GetCustomerMobileNumber(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerMobileNumber :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for MobileNumber");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.MobileNumber;
        }

        /// <summary>
        ///     Get street address of customer
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <returns>string, street address</returns>
        public string GetCustomerStreetAddress(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerStreetAddress :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for StreetAddress");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.StreetAddress;
        }

        /// <summary>
        ///     Get suburb of customer
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <returns>string, suburb</returns>
        public string GetCustomerSuburb(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerSuburb :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for Suburb");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.Suburb;
        }

        /// <summary>
        ///     Get city of customer
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <returns>string, city</returns>
        public string GetCustomerCity(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerCity :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for City");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.City;
        }

        /// <summary>
        ///     check if this customer account is suspended
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <returns>true or false</returns>
        public bool GetIsCustomerDisabled(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetIsCustomerDisabled :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for IsDisabled");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer.IsDisabled;
        }


        /// <summary>
        ///    suspend a customer account
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        public void DisableCustomer(int id)
        {
            _dm.DisableExistingCustomer(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.DisableCustomer :: Disabled Customer: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());

        }

        /// <summary>
        ///     change customer details.
        ///     if new, add the customer with random password.
        ///     if not new, update the customer.
        ///     cannot update the password hash here. Instead Use: <see cref="UpdateCustomerPassword"/>
        ///     if id > 0 but not refers to a customer, do nothing
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <param name="firstName">string, first name</param>
        /// <param name="lastName">string, last name</param>
        /// <param name="email">string, email</param>
        /// <param name="login">string, login</param>
        /// <param name="homeNumber">string, home phone number</param>
        /// <param name="workNumber">string, work number</param>
        /// <param name="mobileNumber">string, mobile phone number</param>
        /// <param name="streetAddress">string, street address</param>
        /// <param name="suburb">string, suburb</param>
        /// <param name="city">string, city</param>
        public void AddOrUpdateCustomer(int id, string firstName,
            string lastName, string email, string login,
            string homeNumber, string workNumber, string mobileNumber, string streetAddress,
            string suburb, string city)
        {
            if (id < 0)
            {
                string hash = Security.GetPasswordHash(Security.GetRandomPassword());
                // add new customer
                _dm.AddNewCustomer(email, login, hash, firstName, lastName, homeNumber,
                    workNumber, mobileNumber, streetAddress, suburb, city);
            }
            else
            {
                // update customer
                _dm.UpdateExistingCustomer(id, email, login, firstName, lastName, homeNumber,
                    workNumber, mobileNumber, streetAddress, suburb, city);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.AddOrUpdateCustomer :: Completed Update, Single Customer: ");
            if (id < 0)
            {
                builder.Append(firstName);
                builder.Append(" ");
                builder.Append(lastName);
            }
            else
            {
                builder.Append(id);
            }
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());

        }

        /// <summary>
        ///     Update the password of an existing customer.
        ///     if id does not refer to existing customer, do nothing
        /// </summary>
        /// <param name="id">integer, id for customer</param>
        /// <param name="password">string, password, UNHASHED to update with</param>
        public void UpdateCustomerPassword(int id, string password)
        {
            string hash = Security.GetPasswordHash(password);
            _dm.UpdateExistingCustomerPassword(id, hash);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.UpdateCustomerPassword :: Completed Update, Single Customer: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());

        }


        /// <summary>
        ///     Get list of administrators
        /// </summary>
        /// <returns>List, Administrator, all admins</returns>
        public List<Administrator> GetAdministrators()
        {
            List<Administrator> items = _dm.GetAllAdministrators();
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetAdministrators :: Retrieved all administrators ");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        ///     Get login of admin
        /// </summary>
        /// <param name="id">integer, id of customer</param>
        /// <returns>string, login</returns>
        public string GetAdminLogin(int id)
        {
            Administrator admin = _dm.GetSingleAdministratorById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetAdminLogin :: Retrieved Single Admin: ");
            builder.Append(id);
            builder.Append(" for Login");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return admin.Login;
        }

        /// <summary>
        ///     Get email of admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string, email</returns>
        public string GetAdminEmail(int id)
        {
            Administrator admin = _dm.GetSingleAdministratorById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetAdminEmail :: Retrieved Single Admin: ");
            builder.Append(id);
            builder.Append(" for Email");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return admin.Email;
        }

        /// <summary>
        ///     Add or Update an Administrator.
        ///     Use a randomised password for security, if new admin.
        ///     if updateing but id does not refer to existing admin, do nothing.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email">string, email</param>
        /// <param name="login">string, login</param>
        public void AddOrUpdateAdmin(int id, string email, string login)
        {
            if (id < 0)
            {
                string hash = Security.GetPasswordHash(Security.GetRandomPassword());
                // add new admin
                _dm.AddNewAdmin(email, login, hash);
            }
            else
            {
                // update admin
                _dm.UpdateExistingAdmin(id, email, login);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.AddOrUpdateAdmin :: Completed Update, Admin: ");
            if (id < 0)
            {
                builder.Append(login);
            }
            else
            {
                builder.Append(id);
            }

            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        ///     Update the password of an existing admin.
        ///     if id does not refer to an admin, do nothing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password">string, UNHASHED password</param>
        public void UpdateAdminPassword(int id, string password)
        {
            string hash = Security.GetPasswordHash(password);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.UpdateAdminPassword :: Updated Admin: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            _dm.UpdateExistingAdminPassword(id, hash);
        }


        /// <summary>
        ///     Find one admin using a login name.
        ///     Returns the first admin that is found.
        ///     
        /// </summary>
        /// <param name="login">string, login</param>
        /// <returns>The first matching SiteUser (admin) or null</returns>
        public Administrator FindAdminByLoginName(string login)
        {
            Administrator admin =  _dm.GetSingleAdministratorByLogin(login);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.FindAdminByLoginName :: Retrieved Single Admin: ");
            builder.Append(admin.ID);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return admin;
        }

        /// <summary>
        ///     Find one customer using a login name
        ///     Returns the first customer found.
        /// </summary>
        /// <param name="login">string, login</param>
        /// <returns>The first matching Customer or null</returns>
        public Customer FindCustomerByLogin(string login)
        {
            Customer customer = _dm.GetSingleCustomerByLogin(login);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.FindCustomerByLogin :: Retrieved Single Customer: ");
            builder.Append(customer.ID);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer;
        }

        /// <summary>
        ///     Find one customer using an email name
        ///     Returns the first customer found.
        /// </summary>
        /// <param name="email">string, email</param>
        /// <returns>The first matching Customer or null</returns>
        public Customer FindCustomerByEmail(string email)
        {
            Customer customer = _dm.GetSingleCustomerByEmail(email);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.FindCustomerByEmail :: Retrieved Single Customer: ");
            builder.Append(customer.ID);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return customer;
        }


        /// <summary>
        ///     Get a list of categories
        /// </summary>
        /// <returns>List, Category, all categories</returns>
        public List<Category> GetCategories()
        {
            List<Category> items = _dm.GetAllCategories();
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCategories :: Retrieved All Categories ");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        ///     Get a list of colours
        /// </summary>
        /// <returns>List of all colours</returns>
        public List<Colour> GetColours()
        {
            List<Colour> items = _dm.GetAllColours();
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCategories :: Retrieved All Colours ");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        ///     Get the name of a colour, given an id.
        ///     If there is no such colour for this id, return null.
        /// </summary>
        /// <param name="id">integer, id of colour</param>
        /// <returns>null or name of Colour</returns>
        public string GetColourName(int id)
        {
            var item = _dm.GetSingleColourById(id);
            StringBuilder builder;

            if (item == null)
            {
                builder = new StringBuilder();
                builder.Append("AdminController.GetColourName :: Failed to retrieve Single Colour. ID: ");
                builder.Append(id);
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
                return null;
            }

            builder = new StringBuilder();
            builder.Append("AdminController.GetColourName :: Retrieved Single Colour: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return item.Name;
        }

        /// <summary>
        ///     Get the name of a Category, given an id.
        ///     If there is no such Category for this id, return null.
        /// </summary>
        /// <param name="id">integer, id of category</param>
        /// <returns>null or name of Category</returns>
        public string GetCategoryName(int id)
        {
            var item = _dm.GetSingleCategoryById(id);
            StringBuilder builder;

            if (item == null)
            {
                builder = new StringBuilder();
                builder.Append("AdminController.GetCategoryName :: Failed to retrieve Single Category. ID: ");
                builder.Append(id);
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
                return null;
            }

            builder = new StringBuilder();
            builder.Append("AdminController.GetCategoryName :: Retrieved Single Category: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
            return item.Name;
        }

        /// <summary>
        ///     Update the database with the colour represented by this id and name.
        ///     if ID >= 0, but does not refer to existing colour, do nothing.
        /// </summary>
        /// <param name="id">id of the Colour. May be for a new category.</param>
        /// <param name="name">name of the colour.</param>
        public void AddOrUpdateColour(int id, string name)
        {
            if (id < 0)
            {
                _dm.AddNewColour(name);
            }
            else
            {
                _dm.UpdateExistingColour(id, name);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.AddOrUpdateColour :: Updated Colour: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        ///     Update the database with the category represented by this id and name.
        ///     if ID >= 0, but does not refer to existing category, do nothing.
        /// </summary>
        /// <param name="id">id of the category. May be for a new category.</param>
        /// <param name="name">name of the category.</param>
        public void AddOrUpdateCategory(int id, string name)
        {
            if (id < 0)
            {
                _dm.AddNewCategory(name);
            }
            else
            {
                _dm.UpdateExistingCategory(id, name);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.AddOrUpdateCategory :: Updated Category: ");
            builder.Append(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, builder.ToString());
        }


        /// <summary>
        ///     Get all suppliers
        /// </summary>
        /// <returns>List, Supplier, all suppliers</returns>
        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = _dm.GetAllSuppliers();
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved all Suppliers.");
            return suppliers;
        }

        /// <summary>
        ///     Get name of supplier
        /// </summary>
        /// <param name="id">integer, id of supplier</param>
        /// <returns>string, name</returns>
        public string GetSupplierName(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Supplier Name, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.Name;
        }

        /// <summary>
        ///     get home phone number, of supplier
        /// </summary>
        /// <param name="id">integer, id of supplier</param>
        /// <returns>string, home number</returns>
        public string GetSupplierHomeNumber(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Supplier Contact, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.HomeNumber;
        }

        /// <summary>
        ///     Get work phone number of supplier
        /// </summary>
        /// <param name="id">integer, id of supplier</param>
        /// <returns>string, work number</returns>
        public string GetSupplierWorkNumber(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Supplier Contact, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.WorkNumber;
        }

        /// <summary>
        ///     Get mobile phone number of supplier
        /// </summary>
        /// <param name="id">integer, id of supplier</param>
        /// <returns>string, mobile number</returns>
        public string GetSupplierMobileNumber(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Supplier Contact, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.MobileNumber;
        }

        /// <summary>
        ///     Get email of supplier
        /// </summary>
        /// <param name="id">integer, id of supplier</param>
        /// <returns>string, email</returns>
        public string GetSupplierEmail(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Supplier Email, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.Email;
        }


        /// <summary>
        ///     Update the database with the supplier represented by this data
        ///     if id > 0 but does not refer to existing supplier, do nothing.
        /// </summary>
        /// <param name="id">id of the supplier. May be for a new supplier.</param>
        /// <param name="name">name of the supplier.</param>
        /// <param name="homeNumber">string, home phone number</param>
        /// <param name="workNumber">string, work phone number</param>
        /// <param name="mobileNumber">string, mobile number</param>
        /// <param name="email">string, email</param>
        public void AddOrUpdateSupplier(int id, string name, string homeNumber, string workNumber, string mobileNumber, string email)
        {
            if (id < 0)
            {
                _dm.AddNewSupplier(name, homeNumber, workNumber, mobileNumber, email);
            }
            else
            {
                _dm.UpdateExistingSupplier(id, name, homeNumber, workNumber, mobileNumber, email);
            }

            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Updated Supplier, ID:" + id + " Name:" + name);
        }


        /// <summary>
        ///     get list of caps
        /// </summary>
        /// <returns>List, Cap, all caps</returns>
        public List<Cap> GetCaps()
        {
            List<Cap> caps = _dm.GetAllCaps();
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved All Caps.");
            return caps;
        }

        /// <summary>
        ///     get name of a cap
        /// </summary>
        /// <param name="id">integer, id of cap</param>
        /// <returns>string, name</returns>
        public string GetCapName(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Cap Name, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.Name;
        }

        /// <summary>
        ///     Get price of cap
        /// </summary>
        /// <param name="id">integer, id of cap</param>
        /// <returns>Double, Price</returns>
        public Double GetCapPrice(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Cap Price, ID:" + id);
            if (item == null)
            {
                return 0.00;
            }
            return item.Price;
        }

        /// <summary>
        ///     get description of cap
        /// </summary>
        /// <param name="id">integer, id of cap</param>
        /// <returns>string, description</returns>
        public string GetCapDescription(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Cap Description, ID:" + id);
            if (item == null)
            {
                return String.Empty;
            }
            return item.Description;
        }

        /// <summary>
        ///     Get URL to server image for cap
        /// </summary>
        /// <param name="id">integer, id of cap</param>
        /// <returns>string, url to cap image</returns>
        public string GetCapImageUrl(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Cap ImageUrl, ID:" + id);
            if (item == null)
            {
                return GeneralConstants.AdminReplyToEmailDefault;
            }
            return item.ImageUrl;
        }

        /// <summary>
        ///     Get category of cap
        /// </summary>
        /// <param name="id">integer, id of cap</param>
        /// <returns>Category, associated with cap</returns>
        public Category GetCapCategory(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Cap Category, Cap ID:" + id);
            return _dm.GetSingleCategoryById(item.CategoryId);
        }

        /// <summary>
        ///     Get supplier, of cap
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Supplier, associated with cap</returns>
        public Supplier GetCapSupplier(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Cap Supplier, Cap ID:" + id);
            return _dm.GetSingleSupplierById(item.SupplierId);
        }


        /// <summary>
        ///     Update the database with the cap represented by this data
        ///     if new, add the cap
        ///     if not, update
        ///     if ID > 0, but not refer to existing cap, do nothing
        /// </summary>
        /// <param name="id">id of the cap. May be for a new cap.</param>
        /// <param name="name">name of the cap.</param>
        /// <param name="price">floating point, price of cap</param>
        /// <param name="description">string, description</param>
        /// <param name="imageUrl">string, url of cap image</param>
        /// <param name="categoryId">integer, id of category</param>
        /// <param name="supplierId">integer, id of supplier</param>
        public void AddOrUpdateCap(int id, string name, Single price, string description, string imageUrl, int categoryId, int supplierId)
        {
            if (id < 0)
            {
                _dm.AddNewCap(name, price, description, imageUrl, categoryId, supplierId);
            }
            else
            {
                _dm.UpdateExistingCap(id, name, price, description, imageUrl, categoryId, supplierId);
            }

            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Updated Cap, ID:" + id + " Name:" + name);
        }

        /// <summary>
        ///     Get list of all orders
        /// </summary>
        /// <returns>List, CustomerOrder, all orders</returns>
        public List<CustomerOrder> GetOrders()
        {
            List<CustomerOrder> items = _dm.GetAllOrders();
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved All Orders.");
            return items;
        }


        /// <summary>
        ///     Get the customer for an order
        /// </summary>
        /// <param name="orderId">integer, id of order</param>
        /// <returns>Customer, for order</returns>
        public Customer GetCustomerByOrderId(int orderId)
        {
            CustomerOrder order = _dm.GetSingleOrderById(orderId);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Order Customer, Order ID:" + orderId);
            return _dm.GetSingleCustomerById(order.UserId);
        }

        /// <summary>
        ///     Get status of order
        /// </summary>
        /// <param name="orderId">integer, id of order</param>
        /// <returns>string, status</returns>
        public string GetOrderStatus(int orderId)
        {
            CustomerOrder order = _dm.GetSingleOrderById(orderId);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Order Status, ID:" + orderId);
            if (order == null)
            {
                return String.Empty;
            }
            return order.Status;
        }

        /// <summary>
        ///     Get date of order
        /// </summary>
        /// <param name="orderId">integer, id of order</param>
        /// <returns>DateTime, date of order, or empty DateTime</returns>
        public DateTime GetOrderDate(int orderId)
        {
            CustomerOrder order = _dm.GetSingleOrderById(orderId);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Order Status, ID:" + orderId);
            if (order == null)
            {
                return new DateTime();
            }
            return order.DatePlaced;
        }

        /// <summary>
        ///     Get list of summaries of orders.
        /// </summary>
        /// <returns>List, OrderSummaries, of all orders</returns>
        public List<OrderSummary> GetOrderSummaries()
        {
            List<CustomerOrder> orders = _dm.GetAllOrders();
            List<OrderSummary> summaries = new List<OrderSummary>();

            foreach (var customerOrder in orders)
            {
                int qty = 0;
                double cost = 0;
                List<OrderItem> orderItems = _dm.GetAllOrderItemsByOrderId(customerOrder.ID);

                foreach (var orderItem in orderItems)
                {
                    qty += orderItem.Quantity;
                    cost += orderItem.Cap.Price * orderItem.Quantity;
                }

                summaries.Add( new OrderSummary {OrderId = customerOrder.ID, CustomerOrder = customerOrder, SubTotalPrice = cost, TotalQuantity = qty});

            }

            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved All Order Summaries.");

            return summaries;
        }


        /// <summary>
        ///     Update order, status
        ///     if ID does not refer to an order, do nothing
        /// </summary>
        /// <param name="id">integer, id of order</param>
        /// <param name="status">string, status</param>
        public void UpdateOrderStatus(int id, string status)
        {
            _dm.UpdateOrderStatus(id, status);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Updated Order Status, ID:" + id);
        }

        /// <summary>
        ///     Get list of order items
        /// </summary>
        /// <param name="id">integer, order id</param>
        public List<OrderItem> GetItemsForOrderWithId(int id)
        {
            List<OrderItem> orderitems = _dm.GetAllOrderItemsByOrderId(id);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved All OrderItems, ID:" + id);
            return orderitems;
        }

        /// <summary>
        ///     Given a login and password request, check these are valid.
        ///     Login must be for an existing administrator.
        /// </summary>
        /// <param name="login">string, login</param>
        /// <param name="password">string, UNHASHED password</param>
        public bool LoginIsValid(string login, string password)
        {
            // customer with this login must exist in the system.
            Administrator admin = _dm.GetSingleAdministratorByLogin(login);
            if (admin != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Administrator for Login Check. ID:" + admin.ID);
                // the supplied password must match the stored hash.
                var suppliedHash = Security.GetPasswordHash(password);

                if (admin.Password.Equals(suppliedHash))
                {
                    // matching login and password
                    return true;
                }
            }
            else
            {
                return false;
            }


            return false;
        }

        /// <summary>
        ///     Get an administrator, using the login
        /// </summary>
        /// <param name="login">string, login</param>
        /// <returns>Administrator object, or null</returns>
        public Administrator GetAdministratorByLogin(string login)
        {
            Administrator admin = _dm.GetSingleAdministratorByLogin(login);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(LoggingLevel.Info, "Retrieved Administrator. ID:" + admin.ID);
            return admin;
        }
    }
}
