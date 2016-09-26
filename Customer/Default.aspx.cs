using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using SecurityLayer;

public partial class Customer_Details : System.Web.UI.Page
{

    /// <summary>
    /// 
    /// </summary>
    protected void ReBind_CustomerOrders()
    {
        PublicController controller = new PublicController();
        string login = Session[Security.SessionIdentifierLogin].ToString();
        List<OrderSummary> summaryOfOrders = controller.GetAllOrderSummariesByCustomer(login);
        grdvCustomerOrders.DataSource = summaryOfOrders;
        grdvCustomerOrders.DataBind();
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Bound Data Source: " + grdvCustomerOrders.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        try
        {
            string login = Session[Security.SessionIdentifierLogin].ToString();
            PublicController controller = new PublicController();
            Customer customer = controller.GetCustomerByLogin(login);

            if (customer == null)
            {
                lblCustomerName.Text = "Could not retrieve the customer for this login: " + login;
                lblCustomerEmail.Text = String.Empty;
                lblCustomerHomeNumber.Text = String.Empty;
                lblCustomerWorkNumber.Text = String.Empty;
                lblCustomerMobileNumber.Text = String.Empty;
                lblCustomerStreetAddress.Text = String.Empty;
                lblCustomerSuburb.Text = String.Empty;
                lblCustomerCity.Text = String.Empty;

                grdvCustomerOrders.DataSource = null;
                grdvCustomerOrders.DataBind();
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
                if (Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey].ToString()
                    .Equals(GeneralConstants.QueryStringGeneralMessageSuccessfulProfileUpdate))
                {
                    lblMessage.InnerText = "Your profile was successfully updated.";
                }
                else if (Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey].ToString()
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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvCustomerOrders_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvCustomerOrders.PageIndex = e.NewPageIndex;
        ReBind_CustomerOrders();
    }


    protected override void OnError(EventArgs e)
    {
        Session["lastError"] = Server.GetLastError();
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
            Server.GetLastError().Message + "; " + Request.RawUrl);
        Response.Redirect("~/Error/Default");
    }
}