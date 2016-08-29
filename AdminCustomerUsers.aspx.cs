using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Common;
using BusinessLayer;
using DataLayer;
using SecurityLayer;

/// <summary>
///     The Admin page for the SiteUsers Entity - Customers.
///
///     Change Log:
/// 
///     19-8-16     22:35       AskewR04 Created Admin Page for Customers.
/// </summary>
public partial class AdminUsers : System.Web.UI.Page
{
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
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
            AdminController controller = new AdminController();
            int itemId = Convert.ToInt32(e.CommandArgument);
            string firstName = controller.GetCustomerFirstName(itemId);
            string lastName = controller.GetCustomerLastName(itemId);
            string login = controller.GetCustomerLogin(itemId);
            string email = controller.GetCustomerEmail(itemId);
            string homeContactNumber = controller.GetCustomerHomeNumber(itemId);
            string workContactNumber = controller.GetCustomerWorkNumber(itemId);
            string mobileContactNumber = controller.GetCustomerMobileNumber(itemId);
            string streetAddress = controller.GetCustomerStreetAddress(itemId);
            string suburb = controller.GetCustomerSuburb(itemId);
            string city = controller.GetCustomerCity(itemId);
            bool isDisabled = controller.GetIsCustomerDisabled(itemId);

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
            txtUserPassword.Text = String.Empty;
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
        txtUserPassword.Text = String.Empty;
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
        foreach (char c in args.Value)
        {
            if (!Validation.ValidationNameGeneral.Contains(c))
            {
                args.IsValid = false;
                return;
            }
        }

        args.IsValid = true;
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

        AdminController controller = new AdminController();

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

        AdminController controller = new AdminController();

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
        if (txtUserHomeNumber.Text == String.Empty &&
            txtUserWorkNumber.Text == String.Empty &&
            txtUserMobileNumber.Text == String.Empty)
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
                AdminController controller = new AdminController();
                int id = Convert.ToInt32(lblUsersId.Text);
                controller.DisableCustomer(id);
                lblUserIsDisabled.Text = true.ToString();
                lblMessageJumboTron.Text = "SUCCESS: Customer disabled: " +
                                           lblUsersId.Text + ", " + txtUserFirstName.Text + " " + txtUserLastName.Text;
                btnDisableCustomer.Enabled = false;
            }
            catch (FormatException ex)
            {
                lblMessageJumboTron.Text = "Error attempting to disable Customer: " +
                                           lblUsersId.Text + ", " + txtUserFirstName.Text + " " + txtUserLastName.Text;
            }
            catch (NullReferenceException ex)
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
                controller.AddOrUpdateCustomer(id, txtUserFirstName.Text,
                    txtUserLastName.Text, txtUserEmail.Text, txtUserLogin.Text,
                    txtUserHomeNumber.Text,
                    txtUserWorkNumber.Text, txtUserMobileNumber.Text, txtUserStreetAddress.Text, txtUserSuburb.Text,
                    txtUserCity.Text);

                if (txtUserPassword.Enabled)
                {
                    Customer user = controller.FindCustomerByLogin(txtUserLogin.Text);
                    controller.UpdateCustomerPassword(user.ID, txtUserPassword.Text);
                }

                Reload_Sidebar();

                btnUserRegeneratePassword.Text = GeneralConstants.ButtonTextChangePassword;
                txtUserPassword.Text = String.Empty;
                txtUserPassword.Enabled = false;

                lblMessageJumboTron.Text = "SUCCESS: User added or updated: " +
                    lblUsersId.Text + ", " + txtUserFirstName.Text + " " + txtUserLastName.Text;
            }
            else
            {
                lblMessageJumboTron.Text = "ERROR: could not send email to client. Admin was not updated.";
            }
        }

    }
}