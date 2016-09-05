using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BusinessLayer;
using DataLayer;
using SecurityLayer;

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

    /// <summary>
    ///     Constructor
    ///     Create ShoppingController and store reference to DataManager.
    /// </summary>
    public PublicController()
    {
        _dm = DataManager.Instance;
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
    public void RegisterCustomer(string firstName, string lastName, string login, string password, string email, string homeNumber,
        string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
    {
        string hash = Security.GetPasswordHash(password);
        _dm.AddNewCustomer(email, login, hash, firstName, lastName, homeNumber, workNumber, mobileNumber,
            streetAddress, suburb, city);
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
    public void UpdateRegisteredCustomer(int id, string firstName, string lastName, string login, string email, string homeNumber,
        string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
    {

        _dm.UpdateExistingCustomer(id, email, login, firstName, lastName, homeNumber, workNumber, mobileNumber,
            streetAddress, suburb, city);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="passwordhash"></param>
    public void UpdatePasswordForCustomer(int id, string passwordhash)
    {
        _dm.UpdateExistingCustomerPassword(id, passwordhash);

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
            return caps[0].ImageUrl;
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public List<Cap> GetAllCapsByCategoryId(int categoryId)
    {
        return _dm.GetCapsByCategoryId(categoryId);
    }

    
}