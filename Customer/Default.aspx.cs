using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
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
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        try
        {
            var login = Session[Security.SessionIdentifierLogin].ToString();
            var controller = new PublicController();
            var customer = controller.GetCustomerByLogin(login);

            if (customer == null)
            {
                lblCustomerName.Text = "Could not retrieve the customer for this login: " + login;
                lblCustomerEmail.Text = string.Empty;
                lblCustomerHomeNumber.Text = string.Empty;
                lblCustomerWorkNumber.Text = string.Empty;
                lblCustomerMobileNumber.Text = string.Empty;
                lblCustomerStreetAddress.Text = string.Empty;
                lblCustomerSuburb.Text = string.Empty;
                lblCustomerCity.Text = string.Empty;

                gvCustomerOrders.DataSource = null;
                gvCustomerOrders.DataBind();
            }
            else
            {
                lblCustomerName.Text = customer.FirstName + " " + customer.LastName;
                lblCustomerEmail.Text = customer.Email;
                lblCustomerHomeNumber.Text = customer.HomeNumber;
                lblCustomerWorkNumber.Text = customer.WorkNumber;
                lblCustomerMobileNumber.Text = customer.MobileNumber;
                lblCustomerStreetAddress.Text = customer.StreetAddress;
                lblCustomerSuburb.Text = customer.Suburb;
                lblCustomerCity.Text = customer.City;

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