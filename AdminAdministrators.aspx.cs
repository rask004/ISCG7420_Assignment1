using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.UI.WebControls;
using asp_Assignment;
using BusinessLayer;
using SecurityLayer;

/// <summary>
///     The Admin page for the SiteUsers Entity. - Admin users only
///     
///     Change Log:
///
///     25-8-16     00:35       AskewR04 Created Admin Page for Customers.
/// </summary>
public partial class AdminUsers : System.Web.UI.Page
{
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
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
            AdminController controller = new AdminController();
            int itemId = Convert.ToInt32(e.CommandArgument);
            string login = controller.GetAdminLogin(itemId);
            string email = controller.GetAdminEmail(itemId);

            lblUsersId.Text = itemId.ToString();
            txtUserLogin.Text = login;
            txtUserEmail.Text = email;
            
            txtUserLogin.Enabled = true;
            txtUserEmail.Enabled = true;

            txtUserPassword.Enabled = false;
            txtUserPassword.Text = String.Empty;
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
            txtUserPassword.Text = String.Empty;
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
        txtUserPassword.Text = String.Empty;
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
        lblUsersId.Text = String.Empty;
        txtUserEmail.Text = String.Empty;
        txtUserLogin.Text = String.Empty;
        txtUserPassword.Text = String.Empty;
        
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

        AdminController controller = new AdminController();

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
    ///     
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
            AdminController controller = new AdminController();

            int id;

            bool clientWasEmailedUponUpdate = true;

            try
            {
                id = Convert.ToInt32(lblUsersId.Text);
            }
            catch (FormatException)
            {
                id = -1;
            }

            // TODO: fix emailing.
            // try sending email before updating.
            string replyToAddress = String.Empty;

            string emailMessage = "Greetings {0},\n\nYour account has been updated.\n" +
                                  "Please use the following credentials the next time you login:\n\n" +
                                  "login:\t\t{0}\n{1}\n" +
                                  "\nRegards\n\nThe Quality Caps Administration Team\n\n";

            string emailedPasswordSubMessage;

            if (txtUserPassword.Enabled)
            {
                emailedPasswordSubMessage = "password:\t" + txtUserPassword.Text;
            }
            else
            {
                emailedPasswordSubMessage = String.Empty;
            }

            var admins = controller.GetAdministrators();
            if (admins.Count == 0)
            {
                replyToAddress = GeneralConstants.AdminReplyToEmailDefault;
            }
            else
            {
                replyToAddress = admins[0].Email;
            }

            /* try
            {
                GeneralFunctions.SendEmail(txtUserEmail.Text, GeneralConstants.UserNewPasswordEmailSubject,
                    String.Format(emailMessage, txtUserLogin.Text, emailedPasswordSubMessage, txtUserEmail.Text),
                    replyToAddress);
            }
            catch (SmtpException)
            {
                // failed to send the email
                clientWasEmailedUponUpdate = false;
            } */

            // only update the db if email was actually sent.
            if (clientWasEmailedUponUpdate)
            {
                controller.AddOrUpdateAdmin(id, txtUserEmail.Text, txtUserLogin.Text);

                if (txtUserPassword.Enabled)
                {
                    string login = txtUserLogin.Text;
                    var admin = controller.FindAdminByLoginName(login);
                    if (admin != null)
                    {
                        controller.UpdateAdminPassword(admin.ID, txtUserPassword.Text);
                    }
                    
                }

                Reload_Sidebar();

                btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;
                txtUserPassword.Text = String.Empty;
                txtUserPassword.Enabled = false;

                lblMessageJumboTron.Text = "SUCCESS: Admin added or updated: " +
                                           lblUsersId.Text + ", " + txtUserLogin.Text;
            }
            else
            {
                lblMessageJumboTron.Text = "ERROR: could not send email to client. Admin was not updated.";
            }
        }
    }
}