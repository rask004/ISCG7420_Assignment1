using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using SecurityLayer;

public partial class Customer_Profile : System.Web.UI.Page
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

        lblErrorMessages.Text = String.Empty;

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
            
            string login = Session[Security.SessionIdentifierLogin].ToString();

            PublicController controller = new PublicController();
            Customer customer = controller.GetCustomerByLogin(login);

            if (customer == null)
            {
                // Abandon the session
                Session.Abandon();
                // Log Error
                string message = "ERROR: Was expecting a valid customer. Login used to retrieve customer: " + login;
                (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, message);
                // TODO: complete this error handling
                // Email Admin about Error
                // Redirect to error page for unrecognized customers.
            }
            else
            {
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

        PublicController controller = new PublicController();
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
        if (txtHomeNumber.Text == String.Empty &&
            txtWorkNumber.Text == String.Empty &&
            txtMobileNumber.Text == String.Empty)
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
            if (lblErrorMessages.Text == String.Empty)
            {
                lblErrorMessages.Text = "Please fill in the required fields. ";
            }
        }
        else
        {
            PublicController controller = new PublicController();
            Customer customer = controller.GetCustomerByLogin(txtLogin.Text);

            if (customer != null)
            {
                // update the registered user in the db.
                controller.UpdateRegisteredCustomer(customer.ID, txtFirstName.Text, txtLastName.Text, txtLogin.Text,
                    txtEmail.Text, txtHomeNumber.Text, txtWorkNumber.Text,
                    txtMobileNumber.Text, txtStreetAddress.Text, txtSuburb.Text, txtCity.Text);

                
                // update the change in password
                if (btnUserRegeneratePassword.Enabled)
                {
                    // email the Customer their new password.
                    try
                    {
                        controller.UpdatePasswordForCustomer(customer.ID, txtUserPassword.Text);
                        Session[Security.SessionIdentifierSecurityToken] = Security.GenerateSecurityTokenHash(customer.Login,
                            Security.GetPasswordHash(txtUserPassword.Text));

                        string ReplyToEmail = controller.GetAvailableAdminEmail();
                        if (ReplyToEmail.Equals(String.Empty))
                        {
                            ReplyToEmail = GeneralConstants.AdminReplyToEmailDefault;
                        }

                        // TODO: get emailing working on password change
                        /*
                        GeneralFunctions.SendEmail(txtEmail.Text,
                            GeneralConstants.EmailPasswordChangeSubject,
                            String.Format(GeneralConstants.EmailPasswordChangeBody, txtFirstName.Text, txtLastName.Text, txtLogin.Text, txtUserPassword.Text),
                            ReplyToEmail);
                        */
                    }
                    catch (SmtpException smtpEx)
                    {
                        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, "ERROR: Unable to send email in response to change in Customer password. Exception Message: " + smtpEx.Message + "; " + smtpEx.StatusCode);
                        // if emailing fails, redirect to error page, notifying customer of password update, email fail, and remedy action to take.
                    }
                }

                StringBuilder builder = new StringBuilder("~/Customer");
                Response.Redirect(builder.ToString());
            }
            else
            {
                // Abandon the session
                Session.Abandon();
                // Log Error
                string message = "ERROR: Customer not recognized. Login used: " + txtLogin.Text;
                (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, message);
                // TODO: complete this error handling
                // Email Admin about Error
                // Redirect to error page for unrecognized customers.
            }

            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUserRegeneratePassword_OnClick(object sender, EventArgs e)
    {
        if (btnUserRegeneratePassword.Text == "Change Password")
        {
            btnUserRegeneratePassword.Text = "Cancel Changes";
            txtUserPassword.Enabled = true;
        }
        else
        {
            btnUserRegeneratePassword.Text = "Change Password";
            txtUserPassword.Enabled = false;
            txtUserPassword.Text = "";
        }

    }
}