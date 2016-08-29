using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using asp_Assignment;
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
    public List<BusinessLayer.Category> GetCategoriesWithCaps()
    {
        List<BusinessLayer.Cap> caps = null; //_dm.GetAllCaps();
        List<BusinessLayer.Category> categories = new List<BusinessLayer.Category>();
        foreach (var cap in caps)
        {
            if (!categories.Contains(cap.Category))
            {
                categories.Add(cap.Category);
            }
        }
        return categories;
    }





    /// <summary>
    ///     Register a new Customer.
    /// 
    ///     Assumed the new Customer Details have already been validated.
    /// 
    /// </summary>
    /// <param name="CapId"></param>
    /// <returns></returns>
    public Cap GetCapDetails(int CapId)
    {
        return null; //_dm.GetSingleInstance<Cap>(CapId);
    }

    
}