using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using Microsoft.AspNet.Identity;
using SecurityLayer;

public partial class Customer_Details : Page
{
    /// <summary>
    ///     Reload the customer's orders
    /// </summary>
    protected void ReBind_CustomerOrders()
    {
        var controller = new PublicController();
        var login = Session[Security.SessionIdentifierLogin].ToString();
        var summaryOfOrders = controller.GetAllOrderSummariesByCustomer(login);
        gvCustomerOrders.DataSource = summaryOfOrders;
        gvCustomerOrders.DataBind();
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Bound Data Source: " + gvCustomerOrders);
    }

    /// <summary>
    ///     load the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Security.SessionIdentifierSecurityToken] == null)
        {
            Session.Abandon();
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Response.Redirect("~/Default");
        }

        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        try
        {
            var login = Session[Security.SessionIdentifierLogin].ToString();
            var controller = new PublicController();
            var customer = controller.GetCustomerByLogin(login);

            if (customer == null)
            {
                lblCustomerName.InnerText = "Could not retrieve the customer for this login: " + login;
                lblCustomerEmail.InnerText = string.Empty;
                lblCustomerHomeNumber.InnerText = string.Empty;
                lblCustomerWorkNumber.InnerText = string.Empty;
                lblCustomerMobileNumber.InnerText = string.Empty;
                lblCustomerStreetAddress.InnerText = string.Empty;
                lblCustomerSuburb.InnerText = string.Empty;
                lblCustomerCity.InnerText = string.Empty;

                gvCustomerOrders.DataSource = null;
                gvCustomerOrders.DataBind();
            }
            else
            {
                lblCustomerName.InnerText = customer.FirstName + " " + customer.LastName;
                lblCustomerEmail.InnerText = customer.Email;
                lblCustomerHomeNumber.InnerText = customer.HomeNumber;
                lblCustomerWorkNumber.InnerText = customer.WorkNumber;
                lblCustomerMobileNumber.InnerText = customer.MobileNumber;
                lblCustomerStreetAddress.InnerText = customer.StreetAddress;
                lblCustomerSuburb.InnerText = customer.Suburb;
                lblCustomerCity.InnerText = customer.City;

                ReBind_CustomerOrders();
            }

            if (Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey] != null)
            {
                if (Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey]
                    .Equals(GeneralConstants.QueryStringGeneralMessageSuccessfulProfileUpdate))
                {
                    lblMessage.InnerText = "Your profile was successfully updated.";
                }
                else if (Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey]
                    .Equals(GeneralConstants.QueryStringGeneralMessageSuccessfulPlacedNewOrder))
                {
                    lblMessage.InnerText = "Your new Order has been completed.";
                }
            }
        }
        catch (NullReferenceException nex)
        {
            throw new Exception("Cannot identify the current customer.", nex);
        }
    }

    /// <summary>
    ///     handler, pagination for customer orders grid view
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvCustomerOrders_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerOrders.PageIndex = e.NewPageIndex;
        ReBind_CustomerOrders();
    }

    /// <summary>
    ///     Handle errors
    /// </summary>
    /// <param name="e"></param>
    protected override void OnError(EventArgs e)
    {
        Session["lastError"] = Server.GetLastError();
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
            Server.GetLastError().Message + "; " + Request.RawUrl);
        Response.Redirect("~/Error/Default");
    }
}