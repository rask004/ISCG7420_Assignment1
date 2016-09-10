using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using WebGrease.Css.Extensions;
using BusinessLayer;
using CommonLogging;
using SecurityLayer;

/// <summary>
///      
/// The Admin page for the Category Entity.
///
/// Change Log:
///        9-8-16  18:01       AskewR04   Created page and layout.
///        9-8-16  23:01       AskewR04   Completed app logic for handling DB updates and adds.
///       10-8-16  15:05       AskewR04   Updated logic to update SideBar upon save.
///       11-8-16  16:00       AskewR04   Changed sidebar to Repeater. Changed master and page layout for easier
///                                         page update management. Added a jumbotron message box for informing 
///                                         admin of current state.
///
/// </summary>
public partial class AdminCategories : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetCategories();
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
            txtCategoryName.MaxLength = GeneralConstants.CategoryNameMaxLength;

            txtCategoryName.Width = new Unit(txtCategoryName.MaxLength, UnitType.Em);

            Reload_Sidebar();

            lblSideBarHeader.Text = "Categories";

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
            string name = controller.GetCategoryName(itemId);
            if (name == null)
            {
                lblCategoryId.Text = String.Empty;
                txtCategoryName.Text = String.Empty;
                lblMessageJumboTron.Text = "could not load item " + itemId;
            }
            else
            {
                lblCategoryId.Text = itemId.ToString();
                txtCategoryName.Text = name;

                txtCategoryName.Enabled = true;

                btnSaveChanges.Enabled = true;
                btnCancelChanges.Enabled = true;

                lblMessageJumboTron.Text = "Item " + lblCategoryId.Text + " Loaded.";
            }
        }
    }

    /// <summary>
    ///     Prepare category form with a new available id, so user can add a new category.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddButton_Click(object sender, EventArgs e)
    {
        lblCategoryId.Text = String.Empty;
        txtCategoryName.Text = String.Empty;
        txtCategoryName.Enabled = true;

        txtCategoryName.Focus();

        btnSaveChanges.Enabled = true;
        btnCancelChanges.Enabled = true;

        lblMessageJumboTron.Text = "Ready to add category. Please fill out the required fields.";
    }

    /// <summary>
    ///     Undo any uncommitted changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtCategoryName.Enabled = false;

        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Validation function for the Category Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void CategoryNameValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);
    }

    /// <summary>
    ///     Save Changes.
    ///     If id is for an existing Category, update the category.
    ///     Else add a new category.
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
                id = Convert.ToInt32(lblCategoryId.Text);
            }
            catch (FormatException)
            {
                id = -1;
            }
            
            controller.AddOrUpdateCategory(id,
                txtCategoryName.Text);

            Reload_Sidebar();

            lblMessageJumboTron.Text = "SUCCESS: Category added or updated: " + txtCategoryName.Text;            
        }
        
    }
}