using System;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using Microsoft.AspNet.Identity;
using SecurityLayer;

/// <summary>
///     The Admin page for the SiteUsers Entity. - Admin users only
///     Change Log:
///     25-8-16     00:35       AskewR04 Created Admin Page for Customers.
///     20-9-16     18:06       Final review
/// </summary>
public partial class AdminUsers : Page
{
    /// <summary>
    /// </summary>
    private void Reload_Sidebar()
    {
        var controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetAdministrators();
        dbrptSideBarItems.DataBind();
    }

    /// <summary>
    ///     Load the page, prepare the table of items, and the admin form
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

        if (!Page.IsPostBack)
        {
            txtUserEmail.MaxLength = GeneralConstants.EmailMaxLength;
            txtUserLogin.MaxLength = GeneralConstants.LoginMaxLength;
            txtUserPassword.MaxLength = GeneralConstants.PasswordMaxLength;

            Reload_Sidebar();

            lblSideBarHeader.Text = "Administrators";

            lblMessageJumboTron.Text = "READY.";
        }
    }

    /// <summary>
    ///     Repeater Button Command handler for Repeater button clicks.
    /// </summary>
    /// <param name="sender">The Repeater</param>
    /// <param name="e">Command Parameters</param>
    protected void dbrptSideBarItems_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "loadItem")
        {
            var controller = new AdminController();
            var itemId = Convert.ToInt32(e.CommandArgument);
            var login = controller.GetAdminLogin(itemId);
            var email = controller.GetAdminEmail(itemId);

            lblUsersId.Text = itemId.ToString();
            txtUserLogin.Text = login;
            txtUserEmail.Text = email;

            txtUserLogin.Enabled = true;
            txtUserEmail.Enabled = true;

            txtUserPassword.Enabled = false;
            txtUserPassword.Text = string.Empty;
            btnUserRegeneratePassword.Enabled = true;
            btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;

            btnSaveChanges.Enabled = true;
            btnAddAdmin.Enabled = true;
            btnCancelChanges.Enabled = true;

            lblMessageJumboTron.Text = "Item " + itemId + ", " + login + " Loaded.";
        }
    }

    /// <summary>
    ///     Allows admin to prepare a password reset.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonPassword_Click(object sender, EventArgs e)
    {
        if (btnUserRegeneratePassword.Text == GeneralConstants.ButtonTextChangePassword)
        {
            txtUserPassword.Enabled = true;
            btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextCancelChangePassword;
        }
        else
        {
            txtUserPassword.Text = string.Empty;
            txtUserPassword.Enabled = false;
            btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;
        }
    }

    /// <summary>
    ///     Undo any uncommitted changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtUserLogin.Enabled = false;
        txtUserEmail.Enabled = false;
        txtUserPassword.Text = string.Empty;
        btnUserRegeneratePassword.Enabled = false;

        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Prepare the form for adding a new admin.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddButton_Click(object sender, EventArgs e)
    {
        lblUsersId.Text = string.Empty;
        txtUserEmail.Text = string.Empty;
        txtUserLogin.Text = string.Empty;
        txtUserPassword.Text = string.Empty;

        txtUserEmail.Enabled = true;
        txtUserLogin.Enabled = true;
        txtUserPassword.Enabled = true;

        btnUserRegeneratePassword.Enabled = false;
        btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;

        txtUserLogin.Focus();

        btnSaveChanges.Enabled = true;
        btnCancelChanges.Enabled = true;

        lblMessageJumboTron.Text = "Ready to add Administrator. Please fill out the required fields.";
    }

    /// <summary>
    ///     Validation function for the User Email
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserEmailValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateEmailInput(ref args);
    }

    /// <summary>
    ///     Validation function for the User Login
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserLoginValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateAlphaNumericInput(ref args);

        if (!args.IsValid)
        {
            valCharsEmail.ErrorMessage = Validation.ValidationLoginErrorMessageGeneral;
            return;
        }

        var controller = new AdminController();

        foreach (var user in controller.GetAdministrators())
        {
            if (user.Login.Equals(args.Value) && user.ID != Convert.ToInt32(lblUsersId.Text))
            {
                args.IsValid = false;
                valCharsEmail.ErrorMessage = Validation.ValidationLoginErrorMessageInUse;
                break;
            }
        }
    }


    /// <summary>
    ///     Validate password from user
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserPassValidation(object source, ServerValidateEventArgs args)
    {
        if (txtUserPassword.Enabled)
        {
            Validation.ValidatePasswordLength(ref args, 10);
        }
        else
        {
            args.IsValid = true;
        }
    }


    /// <summary>
    ///     Save Changes.
    ///     If id is for an existing User, update the User.
    ///     Else add a new User.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int id;
            try
            {
                id = Convert.ToInt32(lblUsersId.Text);
            }
            catch (FormatException)
            {
                id = -1;
            }

            var controller = new AdminController();
            controller.AddOrUpdateAdmin(id, txtUserEmail.Text, txtUserLogin.Text);
            var admin = controller.GetAdministratorByLogin(txtUserLogin.Text);

            if (admin == null)
            {
                (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
                    "ERROR: Cannot retrieve Admin instance, after insert or update to entities. Login Used:" +
                    txtUserLogin.Text);
                lblMessageJumboTron.Text = "Error: Cannot locate Admin Entity despite update/insert. Login used: " +
                                           txtUserLogin.Text;
            }
            else
            {
                // update the change in password
                if (btnUserRegeneratePassword.Enabled)
                {
                    // email the Customer their new password.
                    try
                    {
                        controller.UpdateAdminPassword(admin.ID, txtUserPassword.Text);
                        Session[Security.SessionIdentifierSecurityToken] =
                            Security.GenerateSecurityTokenHash(admin.Login,
                                Security.GetPasswordHash(txtUserPassword.Text));

                        GeneralFunctions.SendEmail(admin.Email,
                            GeneralConstants.EmailPasswordChangeSubject,
                            string.Format(GeneralConstants.EmailPasswordChangeBody, "Administrator", "",
                                txtUserLogin.Text,
                                txtUserPassword.Text),
                            GeneralConstants.AdminReplyToEmailDefault);

                        lblMessageJumboTron.Text = "SUCCESS: ";
                    }
                    catch (SmtpException smtpEx)
                    {
                        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
                            "ERROR: Unable to send email in response to change in Admin password. Exception Message: " +
                            smtpEx.Message + "; " + smtpEx.StatusCode);

                        lblMessageJumboTron.Text = "WARNING: failed to send password change email. ";
                    }
                }

                Reload_Sidebar();

                btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;
                txtUserPassword.Text = string.Empty;
                txtUserPassword.Enabled = false;

                lblMessageJumboTron.Text += "Admin added or updated: " +
                                            lblUsersId.Text + ", " + txtUserLogin.Text;
            }
        }
    }
}