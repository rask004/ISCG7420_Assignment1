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
public partial class Account_AdministrationLogin : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lgnTestingSection_OnAuthenticate(object sender, AuthenticateEventArgs e)
    {
        AdminController controller = new AdminController();

        if (controller.LoginIsValid(lgnTestingSection.UserName.Trim(), lgnTestingSection.Password))
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, lgnTestingSection.UserName.Trim()));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.IsPersistent, lgnTestingSection.RememberMeText));
            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(id);

            Administrator admin = controller.GetAdministratorByLogin(lgnTestingSection.UserName.Trim());
            Session[Security.SessionIdentifierLogin] = admin.Login;
            Session[Security.SessionIdentifierSecurityToken] = Security.GenerateSecurityTokenHash(admin.Login,
                admin.Password);
        }
        else
        {
            Session.Abandon();
        }
    }
}