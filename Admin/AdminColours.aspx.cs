using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BusinessLayer;
using CommonLogging;
using SecurityLayer;

/// <summary>
///     The Admin page for the Colour Entity.
///
///     Change Log:
///
///     18-8-16  12:00       AskewR04 Created page and layout.
/// </summary>
public partial class AdminColours : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetColours();
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
            txtColourName.MaxLength = GeneralConstants.ColourNameMaxLength;

            txtColourName.Width = new Unit(txtColourName.MaxLength, UnitType.Em);

            Reload_Sidebar();

            lblSideBarHeader.Text = "Colours";

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
            string name = controller.GetColourName(itemId);
            if (name == null)
            {
                lblColourId.Text = String.Empty;
                txtColourName.Text = String.Empty;
                lblMessageJumboTron.Text = "could not load item " + itemId;
            }
            else
            {
                lblColourId.Text = itemId.ToString();
                txtColourName.Text = name;

                txtColourName.Enabled = true;

                btnSaveChanges.Enabled = true;
                btnCancelChanges.Enabled = true;

                lblMessageJumboTron.Text = "Item " + lblColourId.Text + " Loaded.";
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
        lblColourId.Text = String.Empty;
        txtColourName.Text = String.Empty;
        txtColourName.Enabled = true;

        txtColourName.Focus();

        btnSaveChanges.Enabled = true;
        btnCancelChanges.Enabled = true;

        lblMessageJumboTron.Text = "Ready to add colour. Please fill out the required fields.";
    }

    /// <summary>
    ///     Undo any uncommitted changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtColourName.Enabled = false;

        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Validation function for the Colour Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void ColourNameValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);
    }

    /// <summary>
    ///     Save Changes.
    ///     If id is for an existing Colour, update the Colour.
    ///     Else add a new Colour.
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
                id = Convert.ToInt32(lblColourId.Text);
            }
            catch (FormatException)
            {
                id = -1;
            }

            controller.AddOrUpdateColour(id,
                txtColourName.Text);

            Reload_Sidebar();

            lblMessageJumboTron.Text = "SUCCESS: Colour added or updated: " + lblColourId.Text + ", " + txtColourName.Text;
        }

    }
}