using System;
using System.Linq;
using Common;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using BusinessLayer;
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
        if (Request.QueryString.AllKeys.Contains(GeneralConstants.QueryStringGeneralMessageKey)
            && Request.QueryString[GeneralConstants.QueryStringGeneralMessageKey]
                .Equals(GeneralConstants.QueryStringGeneralMessageSuccessfulRegistration))
        {
            lblLoginMessages.InnerText = "Registration Successful. Please check your email for your registration notice.";
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
            }
            else
            {
                Session.Abandon();
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                
            }
        }



    }
}