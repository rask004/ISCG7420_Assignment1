using System;
using System.Linq;
using Common;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using BusinessLayer;
using CommonLogging;
using SecurityLayer;

/// <summary>
/// 
/// </summary>
public partial class Customer_Login : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);
        if (!IsPostBack)
        {
            var authenticationManager = Context.GetOwinContext().Authentication;
            // If already authenticated, redirect to default if customer, and to AdminCustomerUsers if Administrator.
            if (Request.IsAuthenticated && authenticationManager.User.Identity.IsAuthenticated)
            {
                if (authenticationManager.User.IsInRole("Customer"))
                {
                    Response.Redirect("~/");
                }
                else if (authenticationManager.User.IsInRole("Administrator"))
                {
                    Response.Redirect("~/Admin");
                }

                Response.Redirect("~/");
                
            }

            if (Request.QueryString.AllKeys.Contains(GeneralConstants.QueryStringGeneralMessageKey)
                && Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey]
                    .Equals(GeneralConstants.QueryStringGeneralMessageSuccessfulRegistration))
            {
                lblLoginMessages.InnerText =
                    "Registration Successful. Please check your email for your registration notice.";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lgnTestingSection_OnAuthenticate(object sender, AuthenticateEventArgs e)
    {
        PublicController customerManager = new PublicController();

        // Check if this user is a customer.
        if (customerManager.LoginIsValid(lgnTestingSection.UserName.Trim(), lgnTestingSection.Password))
        {
            // login the customer
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, lgnTestingSection.UserName.Trim()));
            claims.Add(new Claim(ClaimTypes.Role, "Customer"));
            claims.Add(new Claim(ClaimTypes.IsPersistent, lgnTestingSection.RememberMeText));
            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(id);

            // update the session securityToken for this customer.
            Customer customer = customerManager.GetCustomerByLogin(lgnTestingSection.UserName.Trim());
            Session[Security.SessionIdentifierLogin] = customer.Login;
            Session[Security.SessionIdentifierSecurityToken] = Security.GenerateSecurityTokenHash(customer.Login,
                customer.Password);

            Response.Redirect("~/Customer");
        }
        else
        {
            // Check if user is otherwise an administrator.
            AdminController administratorManager = new AdminController();
            // Check if this user is a customer.
            if (administratorManager.LoginIsValid(lgnTestingSection.UserName.Trim(), lgnTestingSection.Password))
            {
                // login the administrator
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, lgnTestingSection.UserName.Trim()));
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                claims.Add(new Claim(ClaimTypes.IsPersistent, lgnTestingSection.RememberMeText));
                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(id);

                // update the session securityToken for this customer.
                Administrator admin = administratorManager.GetAdministratorByLogin(lgnTestingSection.UserName.Trim());
                Session[Security.SessionIdentifierLogin] = admin.Login;
                Session[Security.SessionIdentifierSecurityToken] = Security.GenerateSecurityTokenHash(admin.Login,
                    admin.Password);

                Response.Redirect("~/Admin");
            }
            else
            {
                // force a logout.
                Session.Abandon();
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            }
        }



    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lgnTestingSection_OnLoggingIn(object sender, LoginCancelEventArgs e)
    {
        PublicController controller = new PublicController();

        if (controller.IsCustomerSuspended(lgnTestingSection.UserName.Trim()))
        {
            e.Cancel = true;
            lblLoginMessages.InnerText =
                "This Account has been suspended.\nTo request reactivation, please contact the Administrator.";
        }
    }
}