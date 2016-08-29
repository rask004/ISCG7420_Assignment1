using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        ///     Constructor
        ///     Maintain reference to DataManager singleton during lifecycle.
        /// </summary>
        public AdminController()
        {
            // get reference to Data Manager here.
            _dm = DataManager.Instance;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> items = _dm.GetAllCustomers();
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
        }


        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public List<Administrator> GetAdministrators()
        {
            List<Administrator> items = _dm.GetAllAdministrators();
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
        }

        /// <summary>
        ///     Update the password of an existing admin.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public void UpdateAdminPassword(int id, string password)
        {
            string hash = Security.GetPasswordHash(password);
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
            return _dm.GetSingleAdministratorByLogin(login);

        }

        /// <summary>
        ///     Find one customer using a login name
        ///     Returns the first customer found.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The first matching Customer or null</returns>
        public Customer FindCustomerByLogin(string login)
        {
            return _dm.GetSingleCustomerByLogin(login);
        }

        /// <summary>
        ///     Find one customer using an email name
        ///     Returns the first customer found.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The first matching Customer or null</returns>
        public Customer FindCustomerByEmail(string email)
        {
            return _dm.GetSingleCustomerByEmail(email);
        }


        /// <summary>
        ///     Get a list of categories
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> items = _dm.GetAllCategories();
            return items;
        }

        /// <summary>
        ///     Get a list of colours
        /// </summary>
        /// <returns>List of all colours</returns>
        public List<Colour> GetColours()
        {
            List<Colour> items = _dm.GetAllColours();
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

            if (item == null)
            {
                return null;
            }

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

            if (item == null)
            {
                return null;
            }

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
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Supplier> GetSuppliers()
        {
            return _dm.GetAllSuppliers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSupplierName(int id)
        {
            Supplier item = _dm.GetSingleSupplierById(id);
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
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Cap> GetCaps()
        {
            return _dm.GetAllCaps();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCapName(int id)
        {
            Cap item = _dm.GetSingleCapById(id);
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
        public void AddOrUpdateCap(int id, string name, double price, string description, string imageUrl, int categoryId, int supplierId)
        {
            if (id < 0)
            {
                _dm.AddNewCap(name, price, description, imageUrl, categoryId, supplierId);
            }
            else
            {
                _dm.UpdateExistingCap(id, name, price, description, imageUrl, categoryId, supplierId);
            }
        }

    }
}
