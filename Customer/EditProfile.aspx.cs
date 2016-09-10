using System;
using System.Collections.Generic;
using System.Linq;
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
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Loaded Page " + Page.Title + ", " + Request.RawUrl);

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

            // TODO: fill the text and label fields with the customer details.
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
        if (controller.EmailIsAlreadyInUse(args.Value))
        {
            args.IsValid = false;
            lblErrorMessages.Text += "This Email is already in use. ";
            return;
        }

    }

    /// <summary>
    ///     Validate the login, and post warnings if not valid
    ///     Login must be unique
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void LoginValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateAlphaNumericInput(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Login should only be letters and numbers. ";
            return;
        }

        PublicController controller = new PublicController();
        if (controller.LoginIsAlreadyInUse(args.Value))
        {
            args.IsValid = false;
            lblErrorMessages.Text += "This Login is already in use. ";
            return;
        }
    }

    /// <summary>
    ///     Validate the password, and post warnings if not valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void PasswordValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidatePasswordLength(ref args, 10);

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
            try
            {
                int id = Convert.ToInt32(Session[GeneralConstants.SessionCustomerIdentifier]);

                PublicController controller = new PublicController();

                // update the registered user in the db.
                controller.UpdateRegisteredCustomer(id, txtFirstName.Text, txtLastName.Text, txtLogin.Text,
                    txtEmail.Text, txtHomeNumber.Text, txtWorkNumber.Text,
                    txtMobileNumber.Text, txtStreetAddress.Text, txtSuburb.Text, txtCity.Text);

                // email the Customer their new registration details.
                // TODO: get emailing working on the web server.
            }
            catch (FormatException ex)
            {
                Response.Write("ERROR: Customer Identifier could not be converted. Was expecting an integer.");
                (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}