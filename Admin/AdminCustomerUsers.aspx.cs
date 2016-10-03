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
///     The Admin page for the SiteUsers Entity - Customers.
///     Change Log:
///     19-8-16     22:35       AskewR04 Created Admin Page for Customers.
///     20-9-16     18:06       AskewR04 Final review
/// </summary>
public partial class AdminUsers : Page
{
    /// <summary>
    ///     Load sidebar with customer
    /// </summary>
    private void Reload_Sidebar()
    {
        var controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetCustomers();
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
            txtUserHomeNumber.MaxLength = GeneralConstants.HomeNumberMaxLength;
            txtUserWorkNumber.MaxLength = GeneralConstants.WorkNumberMaxLength;
            txtUserMobileNumber.MaxLength = GeneralConstants.MobileNumberMaxLength;
            txtUserFirstName.MaxLength = GeneralConstants.FirstNameMaxLength;
            txtUserLastName.MaxLength = GeneralConstants.LastNameMaxLength;
            txtUserStreetAddress.MaxLength = GeneralConstants.StreetAddressMaxLength;
            txtUserSuburb.MaxLength = GeneralConstants.SuburbMaxLength;
            txtUserCity.MaxLength = GeneralConstants.CityMaxLength;

            txtUserEmail.Width = new Unit(txtUserEmail.MaxLength, UnitType.Em);
            txtUserLogin.Width = new Unit(txtUserLogin.MaxLength, UnitType.Em);
            txtUserPassword.Width = new Unit(txtUserPassword.MaxLength, UnitType.Em);
            txtUserHomeNumber.Width = new Unit(txtUserHomeNumber.MaxLength, UnitType.Em);
            txtUserWorkNumber.Width = new Unit(txtUserWorkNumber.MaxLength, UnitType.Em);
            txtUserMobileNumber.Width = new Unit(txtUserMobileNumber.MaxLength, UnitType.Em);
            txtUserFirstName.Width = new Unit(txtUserFirstName.MaxLength, UnitType.Em);
            txtUserLastName.Width = new Unit(txtUserLastName.MaxLength, UnitType.Em);
            txtUserStreetAddress.Width = new Unit(txtUserStreetAddress.MaxLength, UnitType.Em);
            txtUserSuburb.Width = new Unit(txtUserSuburb.MaxLength, UnitType.Em);
            txtUserCity.Width = new Unit(txtUserCity.MaxLength, UnitType.Em);


            Reload_Sidebar();

            lblSideBarHeader.Text = "Customers";

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
            var firstName = controller.GetCustomerFirstName(itemId);
            var lastName = controller.GetCustomerLastName(itemId);
            var login = controller.GetCustomerLogin(itemId);
            var email = controller.GetCustomerEmail(itemId);
            var homeContactNumber = controller.GetCustomerHomeNumber(itemId);
            var workContactNumber = controller.GetCustomerWorkNumber(itemId);
            var mobileContactNumber = controller.GetCustomerMobileNumber(itemId);
            var streetAddress = controller.GetCustomerStreetAddress(itemId);
            var suburb = controller.GetCustomerSuburb(itemId);
            var city = controller.GetCustomerCity(itemId);
            var isDisabled = controller.GetIsCustomerDisabled(itemId);

            lblUsersId.Text = itemId.ToString();
            txtUserFirstName.Text = firstName;
            txtUserLastName.Text = lastName;
            txtUserLogin.Text = login;
            txtUserEmail.Text = email;
            txtUserHomeNumber.Text = homeContactNumber;
            txtUserWorkNumber.Text = workContactNumber;
            txtUserMobileNumber.Text = mobileContactNumber;
            txtUserStreetAddress.Text = streetAddress;
            txtUserSuburb.Text = suburb;
            txtUserCity.Text = city;
            lblUserIsDisabled.Text = isDisabled.ToString();

            txtUserFirstName.Enabled = true;
            txtUserLastName.Enabled = true;
            txtUserLogin.Enabled = true;
            txtUserEmail.Enabled = true;
            txtUserHomeNumber.Enabled = true;
            txtUserWorkNumber.Enabled = true;
            txtUserMobileNumber.Enabled = true;
            txtUserStreetAddress.Enabled = true;
            txtUserSuburb.Enabled = true;
            txtUserCity.Enabled = true;

            txtUserPassword.Enabled = false;
            txtUserPassword.Text = string.Empty;
            btnUserRegeneratePassword.Enabled = true;
            btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;

            if (isDisabled == false)
            {
                // enable the disable customer button
                btnDisableCustomer.Enabled = true;
            }
            else
            {
                // disable the disable customer button
                btnDisableCustomer.Enabled = false;
            }

            btnSaveChanges.Enabled = true;
            btnCancelChanges.Enabled = true;

            lblMessageJumboTron.Text = "Item " + itemId + ", " + firstName + " " + lastName + " Loaded.";
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
        txtUserFirstName.Enabled = false;
        txtUserLastName.Enabled = false;
        txtUserLogin.Enabled = false;
        txtUserEmail.Enabled = false;
        txtUserHomeNumber.Enabled = false;
        txtUserWorkNumber.Enabled = false;
        txtUserMobileNumber.Enabled = false;
        txtUserCity.Enabled = false;
        txtUserSuburb.Enabled = false;
        txtUserStreetAddress.Enabled = false;

        btnUserRegeneratePassword.Enabled = false;
        txtUserPassword.Enabled = false;
        txtUserPassword.Text = string.Empty;
        btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;

        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;
        // disable the disable customer button
        btnDisableCustomer.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Validation function for the User First or Last Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void GeneralNameValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);
    }

    /// <summary>
    ///     Validation function for the User First or Last Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserNameValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateAlphabeticInput(ref args);
    }

    /// <summary>
    ///     Validation function for the User Email
    ///     Emails should match the email regex pattern.
    ///     Also they should be unique.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserEmailValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateEmailInput(ref args);

        if (!args.IsValid)
        {
            valCharsEmail.ErrorMessage = Validation.ValidationEmailErrorMessageGeneral;
            return;
        }

        var controller = new AdminController();

        foreach (var customerUser in controller.GetCustomers())
        {
            if (customerUser.Email.Equals(args.Value) && customerUser.ID != Convert.ToInt32(lblUsersId.Text))
            {
                args.IsValid = false;
                valCharsEmail.ErrorMessage = Validation.ValidationEmailErrorMessageInUse;
                break;
            }
        }
    }

    /// <summary>
    ///     Validation function for the User Login
    ///     Logins should be alphanumeric.
    ///     Also they should be unique.
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

        foreach (var customerUser in controller.GetCustomers())
        {
            if (customerUser.Login.Equals(args.Value) && customerUser.ID != Convert.ToInt32(lblUsersId.Text))
            {
                args.IsValid = false;
                valCharsEmail.ErrorMessage = Validation.ValidationLoginErrorMessageInUse;
                break;
            }
        }
    }

    /// <summary>
    ///     Validation function for the User Contact Numbers.
    ///     At least one contact number is required.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserNumberRequiredValidation(object source, ServerValidateEventArgs args)
    {
        if (txtUserHomeNumber.Text == string.Empty &&
            txtUserWorkNumber.Text == string.Empty &&
            txtUserMobileNumber.Text == string.Empty)
        {
            args.IsValid = false;
            return;
        }

        args.IsValid = true;
    }

    /// <summary>
    ///     Password validations.
    ///     Skipped if the password textBox is disabled - meaning to not update the password.
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
    ///     Validation function for the User Home or Work number
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserLandlineNumberValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateLandlineNumber(ref args);
    }

    /// <summary>
    ///     Validation function for the User Mobile Number
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserMobileNumberValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateMobileNumber(ref args);
    }

    /// <summary>
    ///     Validation function for the Address
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void UserStreetAddressValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateStreetAddress(ref args);
    }

    /// <summary>
    ///     Disable the Customer Account.
    ///     Only works if ID is valid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DisableButton_Click(object sender, EventArgs e)
    {
        if (lblUserIsDisabled.Text == false.ToString())
        {
            try
            {
                var controller = new AdminController();
                var id = Convert.ToInt32(lblUsersId.Text);
                controller.DisableCustomer(id);
                lblUserIsDisabled.Text = true.ToString();
                lblMessageJumboTron.Text = "SUCCESS: Customer disabled: " +
                                           lblUsersId.Text + ", " + txtUserFirstName.Text + " " + txtUserLastName.Text;
                btnDisableCustomer.Enabled = false;
            }
            catch (FormatException)
            {
                lblMessageJumboTron.Text = "Error attempting to disable Customer: " +
                                           lblUsersId.Text + ", " + txtUserFirstName.Text + " " + txtUserLastName.Text;
            }
            catch (NullReferenceException)
            {
                lblMessageJumboTron.Text = "Error, there is no Customer with this ID: " +
                                           lblUsersId.Text;
            }
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
            controller.AddOrUpdateCustomer(id, txtUserFirstName.Text,
                txtUserLastName.Text, txtUserEmail.Text, txtUserLogin.Text,
                txtUserHomeNumber.Text,
                txtUserWorkNumber.Text, txtUserMobileNumber.Text, txtUserStreetAddress.Text, txtUserSuburb.Text,
                txtUserCity.Text);

            var customer = controller.FindCustomerByLogin(txtUserLogin.Text);

            if (customer == null)
            {
                lblMessageJumboTron.Text = "ERROR: cannot find the customer inserted / updated. Login Used:" +
                                           txtUserLogin.Text;
                (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
                    "ERROR: cannot find the customer inserted / updated. Login Used:" +
                    txtUserLogin.Text);
            }
            else
            {
                // update the change in password
                if (btnUserRegeneratePassword.Enabled)
                {
                    // email the Customer their new password.
                    try
                    {
                        controller.UpdateCustomerPassword(customer.ID, txtUserPassword.Text);
                        Session[Security.SessionIdentifierSecurityToken] =
                            Security.GenerateSecurityTokenHash(customer.Login,
                                Security.GetPasswordHash(txtUserPassword.Text));

                        GeneralFunctions.SendEmail(customer.Email,
                            GeneralConstants.EmailPasswordChangeSubject,
                            string.Format(GeneralConstants.EmailPasswordChangeBody, customer.FirstName,
                                customer.LastName, customer.Login,
                                txtUserPassword.Text),
                            GeneralConstants.AdminReplyToEmailDefault);

                        lblMessageJumboTron.Text = "SUCCESS: ";
                    }
                    catch (SmtpException smtpEx)
                    {
                        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
                            "ERROR: Unable to send email in response to change in Customer password. Exception Message: " +
                            smtpEx.Message + "; " + smtpEx.StatusCode);
                        lblMessageJumboTron.Text =
                            "WARNING: Changed password but could not email customer regarding this. Please manually email customer. ";
                    }
                }
                else
                {
                    lblMessageJumboTron.Text = "SUCCESS: ";
                }

                Reload_Sidebar();

                btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;
                txtUserPassword.Text = string.Empty;
                txtUserPassword.Enabled = false;

                lblMessageJumboTron.Text += "User added or updated. ID:" +
                                            lblUsersId.Text + ", Login:" + txtUserLogin.Text;
            }
        }
    }
}