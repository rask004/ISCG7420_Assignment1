﻿using System;
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
    ///     Responsible for supporting
    ///     Checkout
    ///     Retrieving customer details
    ///     Registering new Customers
    ///     Updating customers.
    ///     Getting categories associated to products.
    ///     Getting caps associated with a category.
    ///     Checking if a customer account is suspended.
    ///     Checking if a customer account matches a login and password.
    /// </summary>
    public class PublicController
    {
        private readonly DataManager _dm;

        /// <summary>
        ///     Constructor
        ///     Create ShoppingController and store reference to DataManager.
        /// </summary>
        public PublicController()
        {
            _dm = DataManager.Instance;

            var builder = new StringBuilder();
            builder.Append("PublicController.PublicController :: PublicController Created.");
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, builder.ToString());
        }

        /// <summary>
        ///     Register a new customer.
        ///     Assumes the customer has a unique login and email.
        /// </summary>
        /// <param name="firstName">string, first name of customer</param>
        /// <param name="lastName">string, last name of customer</param>
        /// <param name="login">string, login of customer</param>
        /// <param name="password">string, password of customer</param>
        /// <param name="email">string, email of customer</param>
        /// <param name="homeNumber">string, home phone number of customer</param>
        /// <param name="workNumber">string, work phone number of customer</param>
        /// <param name="mobileNumber">string, mobile phone number of customer</param>
        /// <param name="streetAddress">string, street address of customer</param>
        /// <param name="suburb">string, suburb of customer</param>
        /// <param name="city">string, city of customer</param>
        public void RegisterCustomer(string firstName, string lastName, string login, string password, string email,
            string homeNumber, string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
        {
            var hash = Security.GetPasswordHash(password);
            _dm.AddNewCustomer(email, login, hash, firstName, lastName, homeNumber, workNumber, mobileNumber,
                streetAddress, suburb, city);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Customer Registered. Login:" + login + ", Email:" + email);
        }

        /// <summary>
        ///     Update a customer.
        ///     Assume the login and email is unique.
        /// </summary>
        /// <param name="id">integer, id of customer to update</param>
        /// <param name="firstName">string, first name of customer</param>
        /// <param name="lastName">string, last name of customer</param>
        /// <param name="login">string, login of customer</param>
        /// <param name="email">string, email of customer</param>
        /// <param name="homeNumber">string, home phone number of customer</param>
        /// <param name="workNumber">string, work phone number of customer</param>
        /// <param name="mobileNumber">string, mobile phone number of customer</param>
        /// <param name="streetAddress">string, street address of customer</param>
        /// <param name="suburb">string, suburb of customer</param>
        /// <param name="city">string, city of customer</param>
        public void UpdateRegisteredCustomer(int id, string firstName, string lastName, string login, string email,
            string homeNumber,
            string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
        {
            _dm.UpdateExistingCustomer(id, email, login, firstName, lastName, homeNumber, workNumber, mobileNumber,
                streetAddress, suburb, city);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Customer Updated. ID:" + id);
        }

        /// <summary>
        ///     Update a customer password.
        ///     Assumed the supplied password is UNHASHED.
        /// </summary>
        /// <param name="id">integer, id of the customer</param>
        /// <param name="password">an unhashed password</param>
        public void UpdatePasswordForCustomer(int id, string password)
        {
            var hash = Security.GetPasswordHash(password);
            _dm.UpdateExistingCustomerPassword(id, hash);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Customer Password Updated. ID:" + id);
        }

        /// <summary>
        ///     Check if a login matches any existing customer.
        /// </summary>
        /// <param name="login">string, login, of Customer</param>
        /// <returns>true or false</returns>
        public bool LoginIsAlreadyInUse(string login)
        {
            var customer = _dm.GetSingleCustomerByLogin(login);
            if (customer != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Check if an email matches any existing customer.
        /// </summary>
        /// <param name="email">string, email, of Customer</param>
        /// <returns>true or false</returns>
        public bool EmailIsAlreadyInUse(string email)
        {
            var customer = _dm.GetSingleCustomerByEmail(email);
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
            var categories = _dm.GetAllCategories();
            for (var i = categories.Count - 1; i >= 0; i--)
            {
                var caps = _dm.GetCapsByCategoryId(categories[i].ID);
                if (caps.Count == 0)
                {
                    categories.RemoveAt(i);
                }
            }

            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Retrieved all Categories having caps.");

            return categories;
        }

        /// <summary>
        ///     Retrieve a cap, given it's id.
        /// </summary>
        /// <param name="CapId"></param>
        /// <returns>Cap, matching the id, or null</returns>
        public Cap GetCapDetails(int CapId)
        {
            var cap = _dm.GetSingleCapById(CapId);
            if (cap != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Cap. ID:" + CapId);
            }
            else
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Error, "Could not retrieve Cap. ID:" + CapId);
            }
            return cap;
        }

        /// <summary>
        ///     Get a single cap ImageUrl, from the first cap found with this category id.
        /// </summary>
        /// <param name="categoryId">integer, id of category</param>
        /// <returns>a string (imageUrl), or null</returns>
        public string GetFirstCapImageByCategoryId(int categoryId)
        {
            var caps = _dm.GetCapsByCategoryId(categoryId);
            if (caps.Any())
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Image URL Retrieved from Cap in Category. source Category ID:" + categoryId);
                return caps[0].ImageUrl;
            }


            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Failed to retrieve Image URL from Cap in Category. source Category ID:" + categoryId);
            return null;
        }

        /// <summary>
        ///     Get all caps liked to a given category.
        /// </summary>
        /// <param name="categoryId">integer, id of category</param>
        /// <returns>List, Cap, all caps matching a category</returns>
        public List<Cap> GetAllCapsByCategoryId(int categoryId)
        {
            var caps = _dm.GetCapsByCategoryId(categoryId);
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Retrieved all Caps sharing Category. Category ID:" + categoryId);
            return caps;
        }

        /// <summary>
        ///     Get a customer, using the ID.
        /// </summary>
        /// <param name="customerId">integer, id of the customer</param>
        /// <returns>Customer object or null</returns>
        public Customer GetCustomerById(int customerId)
        {
            var customer = _dm.GetSingleCustomerById(customerId);
            if (customer != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Customer. ID:" + customerId);
            }
            else
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Error, "Could not retrieve Customer. ID:" + customerId);
            }
            return customer;
        }

        /// <summary>
        ///     Get a customer, using a login.
        /// </summary>
        /// <param name="login">string login of customer</param>
        /// <returns>Customer object or null</returns>
        public Customer GetCustomerByLogin(string login)
        {
            var customer = _dm.GetSingleCustomerByLogin(login);
            if (customer != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Customer. Login:" + login);
            }
            else
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Error, "Could not retrieve Customer. Login:" + login);
            }

            return customer;
        }

        /// <summary>
        ///     Get name of a category, given a category id.
        /// </summary>
        /// <param name="id">id of matching category.</param>
        /// <returns>string, name of category, or null</returns>
        public string GetCategoryName(int id)
        {
            var category = _dm.GetSingleCategoryById(id);
            if (category != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Category. ID:" + id);
            }
            else
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Error, "Could not retrieve Category. ID:" + id);
            }
            if (category == null)
            {
                return string.Empty;
            }
            return category.Name;
        }

        /// <summary>
        ///     Get list of colours
        /// </summary>
        /// <returns>List, Colours</returns>
        public List<Colour> GetAllColours()
        {
            var colours = _dm.GetAllColours();
            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info, "Retrieved List of all Colours");
            return colours;
        }

        /// <summary>
        ///     Get a cap, using an id
        /// </summary>
        /// <param name="id">integer, id of cap</param>
        /// <returns>Cap object or null</returns>
        public Cap GetCapById(int id)
        {
            var cap = _dm.GetSingleCapById(id);
            if (cap != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Cap. ID:" + id);
            }
            else
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Error, "Could not retrieve Cap. ID:" + id);
            }
            return cap;
        }

        /// <summary>
        ///     Get a Colour, using an id
        /// </summary>
        /// <param name="id">integer, id of Colour</param>
        /// <returns>Colour object or null</returns>
        public Colour GetColourById(int id)
        {
            var colour = _dm.GetSingleColourById(id);
            if (colour != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Colour. ID:" + id);
            }
            else
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Error, "Could not retrieve Colour. ID:" + id);
            }
            return colour;
        }

        /// <summary>
        ///     Get list of orders made by a customer
        /// </summary>
        /// <param name="login">string, login of customer</param>
        /// <returns>List, CustomerOrder</returns>
        public List<CustomerOrder> GetAllOrdersByCustomer(string login)
        {
            var customer = _dm.GetSingleCustomerByLogin(login);
            if (customer != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Customer. ID:" + customer.ID);
                var orders = _dm.GetAllOrders();
                for (var i = orders.Count - 1; i >= 0; i--)
                {
                    if (orders[i].Customer.ID != customer.ID)
                    {
                        orders.RemoveAt(i);
                    }
                }

                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info,
                    "Retrieved Customer Orders for customer, ID:" + customer.ID + ", Count:" + orders.Count);
                return orders;
            }
            // No such customer exists, therefore there are no orders tied to that customer.
            return new List<CustomerOrder>();
        }

        /// <summary>
        ///     Get Summaries of each order (total quantity, total price, order id), related to a customer.
        /// </summary>
        /// <param name="login">the login of the customer</param>
        /// <returns>List, OrderSummary, summaries of each order.</returns>
        public List<OrderSummary> GetAllOrderSummariesByCustomer(string login)
        {
            var summaries = new List<OrderSummary>();

            List<OrderItem> orderItems;
            var orders = _dm.GetAllOrders();
            foreach (var customerOrder in orders)
            {
                // ignore orders not for this customer.
                if (!customerOrder.Customer.Login.Equals(login))
                {
                    continue;
                }

                // get totals for quantity and cost,
                var totalQuantity = 0;
                double totalCost = 0;
                orderItems = _dm.GetAllOrderItemsByOrderId(customerOrder.ID);
                foreach (var orderItem in orderItems)
                {
                    totalQuantity += orderItem.Quantity;
                    totalCost += orderItem.Cap.Price*orderItem.Quantity;
                }

                summaries.Add(new OrderSummary
                {
                    CustomerOrder = customerOrder,
                    OrderId = customerOrder.ID,
                    SubTotalPrice = totalCost,
                    TotalQuantity = totalQuantity
                });
            }

            (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                LoggingLevel.Info,
                "Retrieved Order Summaries for customer, login:" + login + ", Count:" + summaries.Count);
            return summaries;
        }

        /// <summary>
        ///     Given a login and password request, check these are valid.
        ///     Login must be for an existing customer.
        ///     Password when hashed must match the stored cryptographic hash for this customer.
        /// </summary>
        /// <param name="login">login for a customer</param>
        /// <param name="password">password (unhashed) for a customer</param>
        public bool LoginIsValid(string login, string password)
        {
            // customer with this login must exist in the system.
            var customer = _dm.GetSingleCustomerByLogin(login);
            if (customer != null)
            {
                (HttpContext.Current.Application.Get(GeneralConstants.LoggerApplicationStateKey) as Logger).Log(
                    LoggingLevel.Info, "Retrieved Customer for Login Check. ID:" + customer.ID);
                // the supplied password must match the stored hash.
                var suppliedHash = Security.GetPasswordHash(password);

                if (customer.Password.Equals(suppliedHash))
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
        ///     Retrieve the first available Email from an Admin.
        /// </summary>
        /// <returns>email address, for an administrator.</returns>
        public string GetAvailableAdminEmail()
        {
            var admins = _dm.GetAllAdministrators();
            if (admins.Count == 0)
            {
                return string.Empty;
            }
            return admins[0].Email;
        }

        /// <summary>
        ///     Complete and create a new order, given the customer login and the items for the order.
        /// </summary>
        /// <exception cref="NullReferenceException">If the login does not match an existing customer.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If creating a new order fails.</exception>
        /// <param name="login">string, login, for an existing customer.</param>
        /// <param name="items">List, OrderItem, list of items for the order.</param>
        public void PlaceOrderForCustomer(string login, List<OrderItem> items)
        {
            var customer = _dm.GetSingleCustomerByLogin(login);
            if (customer == null)
            {
                // throw exception - incorrect customer Identifier
                throw new NullReferenceException("ERROR: A requested customer does not exist. Customer Login: " + login);
            }
            // need to find id of new order, which is auto-increment generated.
            // find all existing orders before inserting a new order.
            var oldOrders = GetAllOrdersByCustomer(customer.Login);
            _dm.InsertNewOrder("Waiting", customer.ID);
            // now AFTER inserting new order, request all existing orders again - including the new order.
            // this assumes only one new order was inserted during the times between order requests.
            var newOrders = GetAllOrdersByCustomer(customer.Login);
            // now remove all orders in the first list from the second - should leave only the order(s) just inserted.
            // more efficient to use a double reverse loop, eliminating from both with each match.
            for (var i = newOrders.Count - 1; i >= 0; i--)
            {
                for (var j = oldOrders.Count - 1; j >= 0; j--)
                {
                    if (oldOrders[j].ID == newOrders[i].ID)
                    {
                        oldOrders.RemoveAt(j);
                        newOrders.RemoveAt(i);
                        break;
                    }
                }
            }

            try
            {
                var newOrder = newOrders[0];
                foreach (var orderItem in items)
                {
                    _dm.InsertNewOrderItem(newOrder.ID, orderItem.CapId, orderItem.ColourId, orderItem.Quantity);
                }
            }
            catch (ArgumentOutOfRangeException rangeEx)
            {
                // throw exception - could not find newly inserted order.
                throw new ArgumentOutOfRangeException("ERROR: could not find newly inserted Order.", rangeEx);
            }
        }

        /// <summary>
        ///     Check if a customer account is suspended or not.
        /// </summary>
        /// <returns>True if customer is suspended, false if not.</returns>
        public bool IsCustomerSuspended(string login)
        {
            var isSuspended = true;

            var customer = GetCustomerByLogin(login);

            if (customer == null)
            {
                // cannot suspend an administrator, or a non-existent customer.
                return false;
            }

            if (!customer.IsDisabled)
            {
                isSuspended = false;
            }

            return isSuspended;
        }
    }
}