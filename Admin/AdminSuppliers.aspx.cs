using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BusinessLayer;
using CommonLogging;
using SecurityLayer;

/// <summary>
/// 
///     The Admin page for the Colour Entity.
/// 
///     Change Log:
/// 
///     18-8-16  14:30       AskewR04 Created page and layout.
/// 
/// </summary>
public partial class AdminSupplier : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
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
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        if (!Page.IsPostBack)
        {
            txtSupplierContactNumber.MaxLength = GeneralConstants.WorkNumberMaxLength;
            txtSupplierName.MaxLength = GeneralConstants.SupplierNameMaxLength;
            txtSupplierEmail.MaxLength = GeneralConstants.SupplierEmailMaxLength;

            txtSupplierContactNumber.Width = new Unit(txtSupplierContactNumber.MaxLength, UnitType.Em);
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
            AdminController controller = new AdminController();
            int itemId = Convert.ToInt32(e.CommandArgument);
            string name = controller.GetSupplierName(itemId);
            string contact = controller.GetSupplierContactNumber(itemId);
            string email = controller.GetSupplierEmail(itemId);
            if (name == null)
            {
                lblSupplierId.Text = String.Empty;
                txtSupplierName.Text = String.Empty;
                txtSupplierContactNumber.Text = String.Empty;
                txtSupplierEmail.Text = String.Empty;
                lblMessageJumboTron.Text = "could not load item " + itemId;
            }
            else
            {
                lblSupplierId.Text = itemId.ToString();
                txtSupplierName.Text = name;
                txtSupplierContactNumber.Text = contact;
                txtSupplierEmail.Text = email;

                txtSupplierName.Enabled = true;
                txtSupplierContactNumber.Enabled = true;
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
        lblSupplierId.Text = String.Empty;
        txtSupplierName.Text = String.Empty;
        txtSupplierName.Enabled = true;

        txtSupplierContactNumber.Text = String.Empty;
        txtSupplierContactNumber.Enabled = true;

        txtSupplierEmail.Text = String.Empty;
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
        txtSupplierContactNumber.Enabled = false;
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
            AdminController controller = new AdminController();

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
                txtSupplierName.Text, txtSupplierContactNumber.Text, txtSupplierEmail.Text);

            Reload_Sidebar();

            lblMessageJumboTron.Text = "SUCCESS: Supplier added or updated: " + lblSupplierId.Text + ", " + txtSupplierName.Text;
        }

    }
}