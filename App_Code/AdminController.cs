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
    /// </summary>
    public class AdminController
    {
        private readonly DataManager _dm;

        private readonly Logger _logger;

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
            _logger = (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger);
            _logger.Log(LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> items = _dm.GetAllCustomers();

            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomers :: Retrieved all Customers.");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerFirstName(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerFirstName :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for FirstName");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.FirstName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerLastName(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerLastName :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for LastName");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.LastName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerLogin(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerLogin :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for Login");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.Login;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerEmail(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerEmail :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for Email");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.Email;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerHomeNumber(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerHomeNumber :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for HomeNumber");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.HomeNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerWorkNumber(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerWorkNumber :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for WorkNumber");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.WorkNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerMobileNumber(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerMobileNumber :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for MobileNumber");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.MobileNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerStreetAddress(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerStreetAddress :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for StreetAddress");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.StreetAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerSuburb(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerSuburb :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for Suburb");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.Suburb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCustomerCity(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCustomerCity :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for City");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.City;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetIsCustomerDisabled(int id)
        {
            Customer customer = _dm.GetSingleCustomerById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetIsCustomerDisabled :: Retrieved Single Customer: ");
            builder.Append(id);
            builder.Append(" for IsDisabled");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer.IsDisabled;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DisableCustomer(int id)
        {
            _dm.DisableExistingCustomer(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.DisableCustomer :: Disabled Customer: ");
            builder.Append(id);
            _logger.Log(LoggingLevel.Info, builder.ToString());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="login"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="streetAddress"></param>
        /// <param name="suburb"></param>
        /// <param name="city"></param>
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
            _logger.Log(LoggingLevel.Info, builder.ToString());

        }

        /// <summary>
        ///     Update the password of an existing customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public void UpdateCustomerPassword(int id, string password)
        {
            string hash = Security.GetPasswordHash(password);
            _dm.UpdateExistingCustomerPassword(id, hash);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.UpdateCustomerPassword :: Completed Update, Single Customer: ");
            builder.Append(id);
            _logger.Log(LoggingLevel.Info, builder.ToString());

        }


        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public List<Administrator> GetAdministrators()
        {
            List<Administrator> items = _dm.GetAllAdministrators();
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetAdministrators :: Retrieved all administrators ");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAdminLogin(int id)
        {
            Administrator admin = _dm.GetSingleAdministratorById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetAdminLogin :: Retrieved Single Admin: ");
            builder.Append(id);
            builder.Append(" for Login");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return admin.Login;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAdminEmail(int id)
        {
            Administrator admin = _dm.GetSingleAdministratorById(id);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetAdminEmail :: Retrieved Single Admin: ");
            builder.Append(id);
            builder.Append(" for Email");
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return admin.Email;
        }

        /// <summary>
        ///     Add or Update an Administrator.
        ///     Use a randmised password for security.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="login"></param>
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
            
            _logger.Log(LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        ///     Update the password of an existing admin.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public void UpdateAdminPassword(int id, string password)
        {
            string hash = Security.GetPasswordHash(password);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.UpdateAdminPassword :: Updated Admin: ");
            builder.Append(id);
            _logger.Log(LoggingLevel.Info, builder.ToString());
            _dm.UpdateExistingAdminPassword(id, hash);
        }


        /// <summary>
        ///     Find one admin using a login name.
        ///     Returns the first admin that is found.
        ///     
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The first matching SiteUser (admin) or null</returns>
        public Administrator FindAdminByLoginName(string login)
        {
            Administrator admin =  _dm.GetSingleAdministratorByLogin(login);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.FindAdminByLoginName :: Retrieved Single Admin: ");
            builder.Append(admin.ID);
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return admin;
        }

        /// <summary>
        ///     Find one customer using a login name
        ///     Returns the first customer found.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The first matching Customer or null</returns>
        public Customer FindCustomerByLogin(string login)
        {
            Customer customer = _dm.GetSingleCustomerByLogin(login);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.FindCustomerByLogin :: Retrieved Single Customer: ");
            builder.Append(customer.ID);
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer;
        }

        /// <summary>
        ///     Find one customer using an email name
        ///     Returns the first customer found.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The first matching Customer or null</returns>
        public Customer FindCustomerByEmail(string email)
        {
            Customer customer = _dm.GetSingleCustomerByEmail(email);
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.FindCustomerByEmail :: Retrieved Single Customer: ");
            builder.Append(customer.ID);
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return customer;
        }


        /// <summary>
        ///     Get a list of categories
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> items = _dm.GetAllCategories();
            StringBuilder builder = new StringBuilder();
            builder.Append("AdminController.GetCategories :: Retrieved All Categories ");
            _logger.Log(LoggingLevel.Info, builder.ToString());
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
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return items;
        }

        /// <summary>
        ///     Get the name of a colour, given an id.
        ///     If there is no such colour for this id, return null.
        /// </summary>
        /// <param name="id"></param>
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
                _logger.Log(LoggingLevel.Error, builder.ToString());
                return null;
            }

            builder = new StringBuilder();
            builder.Append("AdminController.GetColourName :: Retrieved Single Colour: ");
            builder.Append(id);
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return item.Name;
        }

        /// <summary>
        ///     Get the name of a Category, given an id.
        ///     If there is no such Category for this id, return null.
        /// </summary>
        /// <param name="id"></param>
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
                _logger.Log(LoggingLevel.Error, builder.ToString());
                return null;
            }

            builder = new StringBuilder();
            builder.Append("AdminController.GetCategoryName :: Retrieved Single Category: ");
            builder.Append(id);
            _logger.Log(LoggingLevel.Info, builder.ToString());
            return item.Name;
        }

        /// <summary>
        ///     Update the database with the colour represented by this id and name.
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
            _logger.Log(LoggingLevel.Error, builder.ToString());
        }

        /// <summary>
        ///     Update the database with the category represented by this id and name.
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
            _logger.Log(LoggingLevel.Error, builder.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = _dm.GetAllSuppliers();
            _logger.Log(LoggingLevel.Info, "Retrieved all Suppliers.");
            return suppliers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSupplierName(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Supplier Name, ID:" + id);
            return item.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSupplierContactNumber(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Supplier Contact, ID:" + id);
            return item.ContactNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSupplierEmail(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Supplier Email, ID:" + id);
            return item.Email;
        }


        /// <summary>
        ///     Update the database with the supplier represented by this data
        /// </summary>
        /// <param name="id">id of the supplier. May be for a new supplier.</param>
        /// <param name="name">name of the supplier.</param>
        /// <param name="contactNumber"></param>
        /// <param name="email"></param>
        public void AddOrUpdateSupplier(int id, string name, string contactNumber, string email)
        {
            if (id < 0)
            {
                _dm.AddNewSupplier(name, contactNumber, email);
            }
            else
            {
                _dm.UpdateExistingSupplier(id, name, contactNumber, email);
            }

            _logger.Log(LoggingLevel.Info, "Updated Supplier, ID:" + id + " Name:" + name);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Cap> GetCaps()
        {
            List<Cap> caps = _dm.GetAllCaps();
            _logger.Log(LoggingLevel.Info, "Retrieved All Caps.");
            return caps;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCapName(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Cap Name, ID:" + id);
            return item.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Double GetCapPrice(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Cap Price, ID:" + id);
            return item.Price;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCapDescription(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Cap Description, ID:" + id);
            return item.Description;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCapImageUrl(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Cap ImageUrl, ID:" + id);
            return item.ImageUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCapCategory(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Cap Category, Cap ID:" + id);
            return _dm.GetSingleCategoryById(item.CategoryId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Supplier GetCapSupplier(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
            _logger.Log(LoggingLevel.Info, "Retrieved Cap Supplier, Cap ID:" + id);
            return _dm.GetSingleSupplierById(item.SupplierId);
        }


        /// <summary>
        ///     Update the database with the cap represented by this data
        /// </summary>
        /// <param name="id">id of the cap. May be for a new cap.</param>
        /// <param name="name">name of the cap.</param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
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

            _logger.Log(LoggingLevel.Info, "Updated Cap, ID:" + id + " Name:" + name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CustomerOrder> GetOrders()
        {
            List<CustomerOrder> items = _dm.GetAllOrders();
            _logger.Log(LoggingLevel.Info, "Retrieved All Orders.");
            return items;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Customer GetCustomerByOrderId(int orderId)
        {
            CustomerOrder order = _dm.GetSingleOrderById(orderId);
            _logger.Log(LoggingLevel.Info, "Retrieved Order Customer, Order ID:" + orderId);
            return _dm.GetSingleCustomerById(order.UserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public string GetOrderStatus(int orderId)
        {
            CustomerOrder order = _dm.GetSingleOrderById(orderId);
            _logger.Log(LoggingLevel.Info, "Retrieved Order Status, ID:" + orderId);
            return order.Status;
        }


        /// <summary>
        ///     
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void UpdateOrderStatus(int id, string status)
        {
            _dm.UpdateOrderStatus(id, status);
            _logger.Log(LoggingLevel.Info, "Updated Order Status, ID:" + id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public List<OrderItem> GetItemsForOrderWithId(int id)
        {
            List<OrderItem> orderitems = _dm.GetAllOrderItemsByOrderId(id);
            _logger.Log(LoggingLevel.Info, "Retrieved All OrderItems, ID:" + id);
            return orderitems;
        }
    }
}
