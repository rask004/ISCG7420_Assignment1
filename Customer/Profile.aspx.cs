using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;

public partial class Customer_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        try
        {
            int customerId = Convert.ToInt32(Session[GeneralConstants.SessionCustomerIdentifier]);
            if (customerId <= 0)
            {
                lblCustomerName.Text = "Could not retrieve the customer for this Identifier.";
                lblCustomerEmail.Text = String.Empty;
                lblCustomerHomeNumber.Text = String.Empty;
                lblCustomerWorkNumber.Text = String.Empty;
                lblCustomerMobileNumber.Text = String.Empty;
                lblCustomerStreetAddress.Text = String.Empty;
                lblCustomerSuburb.Text = String.Empty;
                lblCustomerCity.Text = String.Empty;
            }
            else
            {
                PublicController controller = new PublicController();
                Customer customer = controller.GetCustomerById(customerId);
                lblCustomerName.Text = customer.FirstName + " " + customer.LastName;
                lblCustomerEmail.Text = customer.Email;
                lblCustomerHomeNumber.Text = customer.HomeNumber;
                lblCustomerWorkNumber.Text = customer.WorkNumber;
                lblCustomerMobileNumber.Text = customer.MobileNumber;
                lblCustomerStreetAddress.Text = customer.StreetAddress;
                lblCustomerSuburb.Text = customer.Suburb;
                lblCustomerCity.Text = customer.City;

                // update the Orders table.
            }
        }
        catch (FormatException ex)
        {
            Response.Write("ERROR: Customer Identifier could not be converted. Was expecting an integer.");
            (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, ex.Message + "\n\n" + ex.InnerException.Message);
        }
    }
}