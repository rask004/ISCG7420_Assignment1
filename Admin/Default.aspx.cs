using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using SecurityLayer;

/// <summary>
/// Changelog:
///     12-08-16        19:01   AskewR04    created  class
/// </summary>
public partial class Admin_Default : Page
{
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
    }
}