using System;
using System.Linq;
using Common;

public partial class Customer_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.AllKeys.Contains(GeneralConstants.QueryStringGeneralMessageKey)
            && Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey]
                .Equals(GeneralConstants.QueryStringGeneralMessageSuccessfulRegistration))
        {
            lblLoginMessages.InnerText = "Registration Successful. Please check your email for your registration notice.";
        }
    }
}