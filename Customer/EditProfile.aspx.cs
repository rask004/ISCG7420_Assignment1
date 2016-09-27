using System;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using SecurityLayer;

/// <summary>
///     Allows customer to update their profile
/// 
///     Changelog:
///     24-08-16        19:01   AskewR04    created static class
/// </summary>
public partial class Customer_Profile : Page
{
    /// <summary>
    ///     load the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        lblErrorMessages.Text = string.Empty;

        if (!IsPostBack)
        {
            txtEmail.MaxLength = GeneralConstants.EmailMaxLength;
            txtLogin.MaxLength = GeneralConstants.LoginMaxLength;
            txtHomeNumber.MaxLength = GeneralConstants.HomeNumberMaxLength;
            txtWorkNumber.MaxLength = GeneralConstants.WorkNumberMaxLength;
            txtMobileNumber.MaxLength = GeneralConstants.MobileNumberMaxLength;
            txtFirstName.MaxLength = GeneralConstants.FirstNameMaxLength;
            txtLastName.MaxLength = GeneralConstants.LastNameMaxLength;
            txtStreetAddress.MaxLength = GeneralConstants.StreetAddressMaxLength;
            txtSuburb.MaxLength = GeneralConstants.SuburbMaxLength;
            txtCity.MaxLength = GeneralConstants.CityMaxLength;

            txtEmail.Width = new Unit(txtEmail.MaxLength, UnitType.Em);
            txtLogin.Width = new Unit(txtLogin.MaxLength, UnitType.Em);
            txtHomeNumber.Width = new Unit(txtHomeNumber.MaxLength, UnitType.Em);
            txtWorkNumber.Width = new Unit(txtWorkNumber.MaxLength, UnitType.Em);
            txtMobileNumber.Width = new Unit(txtMobileNumber.MaxLength, UnitType.Em);
            txtFirstName.Width = new Unit(txtFirstName.MaxLength, UnitType.Em);
            txtLastName.Width = new Unit(txtLastName.MaxLength, UnitType.Em);
            txtStreetAddress.Width = new Unit(txtStreetAddress.MaxLength, UnitType.Em);
            txtSuburb.Width = new Unit(txtSuburb.MaxLength, UnitType.Em);
            txtCity.Width = new Unit(txtCity.MaxLength, UnitType.Em);

            try
            {
                var login = Session[Security.SessionIdentifierLogin].ToString();

                var controller = new PublicController();
                var customer = controller.GetCustomerByLogin(login);

                if (customer == null)
                {
                    // Abandon the session
                    Session.Abandon();
                    // Log Error
                    var message = "ERROR: Was expecting a valid customer. Login used to retrieve customer: " + login;
                    (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, message);
                    throw new NullReferenceException("The customer was not found when the database was queried.");
                }
                txtEmail.Text = customer.Email;
                txtLogin.Text = customer.Login;
                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtWorkNumber.Text = customer.WorkNumber;
                txtHomeNumber.Text = customer.HomeNumber;
                txtMobileNumber.Text = customer.MobileNumber;
                txtStreetAddress.Text = customer.StreetAddress;
                txtSuburb.Text = customer.Suburb;
                txtCity.Text = customer.City;
            }
            catch (NullReferenceException nex)
            {
                throw new Exception("Cannot identify the current customer.", nex);
            }
        }
    }

    /// <summary>
    ///     Validate the first or last name, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void FirstLastNameValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateAlphabeticInput(ref args);

        if (!args.IsValid)
        {
            if (!lblErrorMessages.Text.Contains("First and Last Name should only have letters."))
                lblErrorMessages.Text += "First and Last Name should only have letters. ";
        }
    }

    /// <summary>
    ///     Validate the email, and post warnings if not valid
    ///     Email must be unique
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void EmailValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateEmailInput(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Email should be a valid email address. ";
            return;
        }

        var controller = new PublicController();
        // If email is unchanged
        if (controller.GetCustomerByLogin(txtLogin.Text).Email.Equals(txtEmail.Text))
        {
            args.IsValid = true;
        }
        // If email changed but in use by another customer
        else if (controller.EmailIsAlreadyInUse(args.Value))
        {
            args.IsValid = false;
            lblErrorMessages.Text += "This Email is already in use. ";
        }
    }

    /// <summary>
    ///     Validate the password, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void PasswordValidation(object sender, ServerValidateEventArgs args)
    {
        if (txtUserPassword.Enabled)
        {
            Validation.ValidatePasswordLength(ref args, 10);
        }

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Password minimum length is 10 characters. ";
        }
    }

    /// <summary>
    ///     Check that at least one phone number field is filled out, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void ContactNumberRequired(object sender, ServerValidateEventArgs args)
    {
        if (txtHomeNumber.Text == string.Empty &&
            txtWorkNumber.Text == string.Empty &&
            txtMobileNumber.Text == string.Empty)
        {
            args.IsValid = false;
            lblErrorMessages.Text += "At least one contact number is required. ";
        }
    }

    /// <summary>
    ///     Validate a land line number, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void LandlineNumberValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateLandlineNumber(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text +=
                "Home and work numbers should be in a valid local landline format. Examples include 09555444, 0733337777. ";
        }
    }

    /// <summary>
    ///     Validate a mobile number, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void MobileNumberValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateMobileNumber(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text +=
                "Mobile Numbers should be in a valid mobile number format, in international or local form. ";
        }
    }

    /// <summary>
    ///     Validate a street address, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void StreetAddressValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateStreetAddress(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text +=
                "Street address is not valid. Valid examples are 123a Simpson St, or 5545 Carolina Ave.";
        }
    }

    /// <summary>
    ///     Validate a suburb or city as a generic name, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void SuburbCityValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Suburb and City should only contain letters, commas, full stops and apostrophes. ";
        }
    }

    /// <summary>
    ///     Complete the registration, by creating the new customer.
    ///     Email the customer upon completion.
    ///     And redirect to login page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Update_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            if (lblErrorMessages.Text == string.Empty)
            {
                lblErrorMessages.Text = "Please fill in the required fields. ";
            }
        }
        else
        {
            var controller = new PublicController();
            var customer = controller.GetCustomerByLogin(txtLogin.Text);

            if (customer != null)
            {
                // update the registered user in the db.
                controller.UpdateRegisteredCustomer(customer.ID, txtFirstName.Text, txtLastName.Text, txtLogin.Text,
                    txtEmail.Text, txtHomeNumber.Text, txtWorkNumber.Text,
                    txtMobileNumber.Text, txtStreetAddress.Text, txtSuburb.Text, txtCity.Text);


                // update the change in password
                if (btnUserChangePassword.Enabled)
                {
                    // email the Customer their new password.
                    try
                    {
                        controller.UpdatePasswordForCustomer(customer.ID, txtUserPassword.Text);
                        Session[Security.SessionIdentifierSecurityToken] =
                            Security.GenerateSecurityTokenHash(customer.Login,
                                Security.GetPasswordHash(txtUserPassword.Text));

                        var replyToEmail = controller.GetAvailableAdminEmail();
                        if (replyToEmail.Equals(string.Empty))
                        {
                            replyToEmail = GeneralConstants.AdminReplyToEmailDefault;
                        }
                        
                        GeneralFunctions.SendEmail(txtEmail.Text,
                            GeneralConstants.EmailPasswordChangeSubject,
                            String.Format(GeneralConstants.EmailPasswordChangeBody, txtFirstName.Text, txtLastName.Text, txtLogin.Text, txtUserPassword.Text),
                            replyToEmail);
                    }
                    catch (SmtpException smtpEx)
                    {
                        throw new Exception("Failed to notify customer of password change by email.", smtpEx);
                    }
                }

                var builder = new StringBuilder("~/Customer");
                Response.Redirect(builder.ToString());
            }
            else
            {
                throw new Exception("Could not recognise or retrieve customer. Login Used: " + txtLogin.Text);
            }
        }
    }

    /// <summary>
    ///     Allow user to change password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUserChangePassword_OnClick(object sender, EventArgs e)
    {
        if (btnUserChangePassword.Text == "Change Password")
        {
            btnUserChangePassword.Text = "Cancel Changes";
            txtUserPassword.Enabled = true;
        }
        else
        {
            btnUserChangePassword.Text = "Change Password";
            txtUserPassword.Enabled = false;
            txtUserPassword.Text = "";
        }
    }

    /// <summary>
    ///     Manage errors, failure to register or send email.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnError(EventArgs e)
    {
        Session["lastError"] = Server.GetLastError();
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
            Server.GetLastError().Message + "; " + Request.RawUrl);
        Response.Redirect("~/Error/Default");
    }
}