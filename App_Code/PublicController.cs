using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Common;
using CommonLogging;
using DataLayer;
using SecurityLayer;

namespace BusinessLayer
{
    /// <summary>
    ///     Business Object to manage Customer activities.
    /// 
    ///     Responsible for supporting
    ///         Adding, updating and removing items in the shopping cart
    ///         Checkout reporting
    ///         Post checkout reporting
    ///         Registering new Customers
    ///         
    ///     
    /// </summary>
    public class PublicController
    {
        private readonly DataManager _dm;

        private readonly Logger _logger;

        /// <summary>
        ///     Constructor
        ///     Create ShoppingController and store reference to DataManager.
        /// </summary>
        public PublicController()
        {
            _dm = DataManager.Instance;

            StringBuilder builder = new StringBuilder();
            builder.Append("PublicController.PublicController :: PublicController Created.");
            _logger = (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger);
            _logger.Log(LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="streetAddress"></param>
        /// <param name="suburb"></param>
        /// <param name="city"></param>
        public void RegisterCustomer(string firstName, string lastName, string login, string password, string email,
            string homeNumber,
            string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
        {
            string hash = Security.GetPasswordHash(password);
            _dm.AddNewCustomer(email, login, hash, firstName, lastName, homeNumber, workNumber, mobileNumber,
                streetAddress, suburb, city);
            _logger.Log(LoggingLevel.Info,"Customer Registered. Login:" + login + ", Email:" + email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="login"></param>
        /// <param name="email"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="streetAddress"></param>
        /// <param name="suburb"></param>
        /// <param name="city"></param>
        public void UpdateRegisteredCustomer(int id, string firstName, string lastName, string login, string email,
            string homeNumber,
            string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
        {

            _dm.UpdateExistingCustomer(id, email, login, firstName, lastName, homeNumber, workNumber, mobileNumber,
                streetAddress, suburb, city);
            _logger.Log(LoggingLevel.Info, "Customer Updated. ID:" + id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordhash"></param>
        public void UpdatePasswordForCustomer(int id, string passwordhash)
        {
            _dm.UpdateExistingCustomerPassword(id, passwordhash);
            _logger.Log(LoggingLevel.Info, "Customer Password Updated. ID:" + id);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool LoginIsAlreadyInUse(string login)
        {
            Customer customer = _dm.GetSingleCustomerByLogin(login);
            if (customer != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailIsAlreadyInUse(string email)
        {
            Customer customer = _dm.GetSingleCustomerByEmail(email);
            if (customer != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Get a list of categories which have Caps associated.
        /// </summary>
        /// <returns>List of all Categories</returns>
        public List<Category> GetCategoriesWithCaps()
        {
            List<Category> categories = _dm.GetAllCategories();
            for (int i = categories.Count - 1; i >= 0; i--)
            {
                List<Cap> caps = _dm.GetCapsByCategoryId(categories[i].ID);
                if (caps.Count == 0)
                {
                    categories.RemoveAt(i);
                }
            }

            _logger.Log(LoggingLevel.Info, "Retrieved all Categories having caps.");

            return categories;
        }

        /// <summary>
        ///     Retrieve a cap, given it's id.
        /// 
        /// </summary>
        /// <param name="CapId"></param>
        /// <returns></returns>
        public Cap GetCapDetails(int CapId)
        {
            return null; //_dm.GetSingleInstance<Cap>(CapId);
        }

        /// <summary>
        ///     Get a single cap ImageUrl, from the first cap found with this category id.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>a string (imageUrl), or null</returns>
        public string GetFirstCapImageByCategoryId(int categoryId)
        {
            List<Cap> caps = _dm.GetCapsByCategoryId(categoryId);
            if (caps.Any())
            {
                _logger.Log(LoggingLevel.Info, "Image URL Retrieved from Cap in Category. source Category ID:" + categoryId);
                return caps[0].ImageUrl;
            }


            _logger.Log(LoggingLevel.Info, "Failed to retrieve Image URL from Cap in Category. source Category ID:" + categoryId);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Cap> GetAllCapsByCategoryId(int categoryId)
        {
            _logger.Log(LoggingLevel.Info, "Retrieved all Caps sharing Category. Category ID:" + categoryId);
            return _dm.GetCapsByCategoryId(categoryId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int customerId)
        {
            _logger.Log(LoggingLevel.Info, "Retrieved Customer. ID:" + customerId);
            return _dm.GetSingleCustomerById(customerId);
        }


    }
}