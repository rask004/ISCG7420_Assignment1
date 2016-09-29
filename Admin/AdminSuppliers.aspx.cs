using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using Microsoft.AspNet.Identity;
using SecurityLayer;

/// <summary>
///     The Admin page for the Supplier Entity.
///     Change Log:
///     18-8-16  14:30       AskewR04 Created page and layout.
/// </summary>
public partial class AdminSupplier : Page
{
    /// <summary>
    ///     Reload list of suppliers.
    /// </summary>
    private void Reload_Sidebar()
    {
        var controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetSuppliers();
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
            txtSupplierHomeNumber.MaxLength = GeneralConstants.HomeNumberMaxLength;
            txtSupplierWorkNumber.MaxLength = GeneralConstants.WorkNumberMaxLength;
            txtSupplierMobileNumber.MaxLength = GeneralConstants.MobileNumberMaxLength;
            txtSupplierName.MaxLength = GeneralConstants.SupplierNameMaxLength;
            txtSupplierEmail.MaxLength = GeneralConstants.SupplierEmailMaxLength;

            txtSupplierHomeNumber.Width = new Unit(txtSupplierHomeNumber.MaxLength, UnitType.Em);
            txtSupplierWorkNumber.Width = new Unit(txtSupplierWorkNumber.MaxLength, UnitType.Em);
            txtSupplierMobileNumber.Width = new Unit(txtSupplierMobileNumber.MaxLength, UnitType.Em);
            txtSupplierName.Width = new Unit(txtSupplierName.MaxLength, UnitType.Em);
            txtSupplierEmail.Width = new Unit(txtSupplierEmail.MaxLength, UnitType.Em);

            Reload_Sidebar();

            lblSideBarHeader.Text = "Suppliers";

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
            var name = controller.GetSupplierName(itemId);
            var homeContact = controller.GetSupplierHomeNumber(itemId);
            var workContact = controller.GetSupplierWorkNumber(itemId);
            var mobileContact = controller.GetSupplierMobileNumber(itemId);
            var email = controller.GetSupplierEmail(itemId);
            if (name == null)
            {
                lblSupplierId.Text = string.Empty;
                txtSupplierName.Text = string.Empty;
                txtSupplierHomeNumber.Text = string.Empty;
                txtSupplierEmail.Text = string.Empty;
                lblMessageJumboTron.Text = "could not load item " + itemId;
            }
            else
            {
                lblSupplierId.Text = itemId.ToString();
                txtSupplierName.Text = name;
                txtSupplierHomeNumber.Text = homeContact;
                txtSupplierWorkNumber.Text = workContact;
                txtSupplierMobileNumber.Text = mobileContact;
                txtSupplierEmail.Text = email;

                txtSupplierName.Enabled = true;
                txtSupplierHomeNumber.Enabled = true;
                txtSupplierWorkNumber.Enabled = true;
                txtSupplierMobileNumber.Enabled = true;
                txtSupplierEmail.Enabled = true;

                btnSaveChanges.Enabled = true;
                btnCancelChanges.Enabled = true;

                lblMessageJumboTron.Text = "Item " + lblSupplierId.Text + " Loaded.";
            }
        }
    }

    /// <summary>
    ///     Prepare item form with a new available id, so user can add a new item.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddButton_Click(object sender, EventArgs e)
    {
        lblSupplierId.Text = string.Empty;
        txtSupplierName.Text = string.Empty;
        txtSupplierName.Enabled = true;

        txtSupplierHomeNumber.Text = string.Empty;
        txtSupplierHomeNumber.Enabled = true;

        txtSupplierWorkNumber.Text = string.Empty;
        txtSupplierWorkNumber.Enabled = true;

        txtSupplierMobileNumber.Text = string.Empty;
        txtSupplierMobileNumber.Enabled = true;

        txtSupplierEmail.Text = string.Empty;
        txtSupplierEmail.Enabled = true;

        txtSupplierName.Focus();

        btnSaveChanges.Enabled = true;
        btnCancelChanges.Enabled = true;

        lblMessageJumboTron.Text = "Ready to add supplier. Please fill out the required fields.";
    }

    /// <summary>
    ///     Undo any uncommitted changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtSupplierName.Enabled = false;
        txtSupplierHomeNumber.Enabled = false;
        txtSupplierWorkNumber.Enabled = false;
        txtSupplierMobileNumber.Enabled = false;
        txtSupplierEmail.Enabled = false;

        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Validation function for the Supplier Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void SupplierNameValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);
    }

    /// <summary>
    ///     Validation function for the Supplier Contact Number
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void SupplierNumberValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateLandlineNumber(ref args);
    }

    /// <summary>
    ///     Validation function for the User Contact Numbers.
    ///     At least one contact number is required.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void NumberRequiredValidation(object source, ServerValidateEventArgs args)
    {
        if (txtSupplierHomeNumber.Text == string.Empty &&
            txtSupplierWorkNumber.Text == string.Empty &&
            txtSupplierMobileNumber.Text == string.Empty)
        {
            args.IsValid = false;
            return;
        }

        args.IsValid = true;
    }

    /// <summary>
    ///     Validation function for the User Home or Work number
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void LandlineNumberValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateLandlineNumber(ref args);
    }

    /// <summary>
    ///     Validation function for the User Mobile Number
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void MobileNumberValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateMobileNumber(ref args);
    }

    /// <summary>
    ///     Validation function for the Supplier Email
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void SupplierEmailValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateEmailInput(ref args);
    }

    /// <summary>
    ///     Save Changes.
    ///     If id is for an existing Supplier, update the Supplier.
    ///     Else add a new Supplier.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var controller = new AdminController();

            int id;

            try
            {
                id = Convert.ToInt32(lblSupplierId.Text);
            }
            catch (FormatException)
            {
                id = -1;
            }

            controller.AddOrUpdateSupplier(id,
                txtSupplierName.Text, txtSupplierHomeNumber.Text, "", "", txtSupplierEmail.Text);

            Reload_Sidebar();

            lblMessageJumboTron.Text = "SUCCESS: Supplier added or updated: " + lblSupplierId.Text + ", " +
                                       txtSupplierName.Text;
        }
    }
}