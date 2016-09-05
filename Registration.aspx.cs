using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using SecurityLayer;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessages.Text = String.Empty;

        if (!IsPostBack)
        {
            txtEmail.MaxLength = GeneralConstants.EmailMaxLength;
            txtLogin.MaxLength = GeneralConstants.LoginMaxLength;
            txtPassword.MaxLength = GeneralConstants.PasswordMaxLength;
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
            txtPassword.Width = new Unit(txtPassword.MaxLength, UnitType.Em);
            txtHomeNumber.Width = new Unit(txtHomeNumber.MaxLength, UnitType.Em);
            txtWorkNumber.Width = new Unit(txtWorkNumber.MaxLength, UnitType.Em);
            txtMobileNumber.Width = new Unit(txtMobileNumber.MaxLength, UnitType.Em);
            txtFirstName.Width = new Unit(txtFirstName.MaxLength, UnitType.Em);
            txtLastName.Width = new Unit(txtLastName.MaxLength, UnitType.Em);
            txtStreetAddress.Width = new Unit(txtStreetAddress.MaxLength, UnitType.Em);
            txtSuburb.Width = new Unit(txtSuburb.MaxLength, UnitType.Em);
            txtCity.Width = new Unit(txtCity.MaxLength, UnitType.Em);
        }
    }

    protected void FirstLastNameValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateAlphabeticInput(ref args);

        if (!args.IsValid)
        {
            if (!lblErrorMessages.Text.Contains("First and Last Name should only have letters."))
            lblErrorMessages.Text += "First and Last Name should only have letters. ";
        }
    }

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

    protected void PasswordValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidatePasswordLength(ref args, 10);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Password minimum length is 10 characters. ";
        }
    }

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

    protected void LandlineNumberValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateLandlineNumber(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Home and work numbers should be in a valid local landline format. Examples include 09555444, 0733337777. ";
        }
    }

    protected void MobileNumberValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateMobileNumber(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Mobile Numbers should be in a valid mobile number format, in international or local form. ";
        }
    }

    protected void StreetAddressValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateStreetAddress(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Street address is not valid. Valid examples are 123a Simpson St, or 5545 Carolina Ave.";
        }
    }

    protected void SuburbCityValidation(object sender, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);

        if (!args.IsValid)
        {
            lblErrorMessages.Text += "Suburb and City should only contain letters, commas, full stops and apostrophes. ";
        }
    }

    protected void Reset_Click(object sender, EventArgs e)
    {
        txtEmail.Text = String.Empty;
        txtLogin.Text = String.Empty;
        txtPassword.Text = String.Empty;
        txtHomeNumber.Text = String.Empty;
        txtWorkNumber.Text = String.Empty;
        txtMobileNumber.Text = String.Empty;
        txtFirstName.Text = String.Empty;
        txtLastName.Text = String.Empty;
        txtStreetAddress.Text = String.Empty;
        txtSuburb.Text = String.Empty;
        txtCity.Text = String.Empty;
    }

    protected void Register_Click(object sender, EventArgs e)
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

            // add the registered user to the db.
            controller.RegisterCustomer(txtFirstName.Text, txtLastName.Text, txtLogin.Text,
                txtPassword.Text, txtEmail.Text, txtHomeNumber.Text, txtWorkNumber.Text,
                txtMobileNumber.Text, txtStreetAddress.Text, txtSuburb.Text, txtCity.Text);
            
            // email the Customer their registration details.
            // TODO: get emailing working on the web server.

            StringBuilder builder = new StringBuilder("~/Customer/Login.aspx");
            builder.Append("?").Append(GeneralConstants.QueryStringGeneralMessageKey);
            builder.Append("=").Append(GeneralConstants.QueryStringGeneralMessageSuccessfulRegistration);

            Response.RedirectPermanent(builder.ToString());
        }

        
    }
}