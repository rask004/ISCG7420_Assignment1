using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using WebGrease.Css.Extensions;
using BusinessLayer;
using SecurityLayer;

/// <summary>
///      
/// The Admin page for the Product Entity.
///
/// Change Log:
///        22-8-16  12:30       AskewR04   Created page and layout.
///
/// </summary>
public partial class AdminCaps : System.Web.UI.Page
{
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetCaps();
        dbrptSideBarItems.DataBind();

        List<Category> categories = controller.GetCategories();
        List<Supplier> suppliers = controller.GetSuppliers();
        if (categories.Count == 0 || suppliers.Count == 0)
        {
            btnAddCap.Enabled = false;
        }
        else
        {
            btnAddCap.Enabled = true;
        }

        ddlCapCategories.DataSource = controller.GetCategories();
        ddlCapCategories.DataBind();
        ddlCapCategories.DataTextField = "name";
        ddlCapCategories.DataValueField = "id";
        ddlCapSuppliers.DataSource = controller.GetSuppliers();
        ddlCapSuppliers.DataBind();
        ddlCapSuppliers.DataTextField = "name";
        ddlCapSuppliers.DataValueField = "id";
    }

    private void PrepareListOfUploadedImages()
    {
        DirectoryInfo uploadedDirectoryInfo = new DirectoryInfo(Server.MapPath(GeneralConstants.ImagesUploadFolder));
        ddlImgCapList.Items.Clear();

        ddlImgCapList.Items.Add(new ListItem { Text = GeneralConstants.CapImageDefaultListName, Value = GeneralConstants.CapImageDefaultFileName });

        // permitted types are in MIME form. Cannot directly compare to extension.
        // but each type will include the extension.
        foreach (var file in uploadedDirectoryInfo.GetFiles())
        {
            foreach (var permittedMimeType in GeneralConstants.PermittedContentTypes)
            {
                if (permittedMimeType.Contains(file.Extension.Substring(1)))
                {
                    ddlImgCapList.Items.Add(new ListItem { Text=file.Name, Value=GeneralConstants.ImagesUploadFolder + "/" + file.Name });
                    break;
                }
            }
        }
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
            txtCapName.MaxLength = GeneralConstants.CapNameMaxLength;
            txtCapDescription.MaxLength = GeneralConstants.CapDescriptionMaxLength;

            txtCapName.Width = new Unit(txtCapName.MaxLength, UnitType.Em);
            txtCapPrice.Width = new Unit(8, UnitType.Em);
            txtCapDescription.Width = new Unit(txtCapDescription.MaxLength / 3 + 2, UnitType.Em);
            txtCapDescription.Height = new Unit(6, UnitType.Em);

            PrepareListOfUploadedImages();
            ddlImgCapList.SelectedIndex = 0;

            Reload_Sidebar();

            lblSideBarHeader.Text = "Caps";

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
            string name = controller.GetCapName(itemId);
            string price = controller.GetCapPrice(itemId).ToString();
            string desc = controller.GetCapDescription(itemId);
            string pictureUrl = controller.GetCapImageUrl(itemId);
            lblCapId.Text = itemId.ToString();
            txtCapName.Text = name;
            txtCapPrice.Text = price;
            txtCapDescription.Text = desc;

            string[] pictureUrlParts = pictureUrl.Split('/');

            string pictureFileName = pictureUrlParts[pictureUrlParts.Length - 1];

            if (pictureFileName == null || ddlImgCapList.Items.FindByText(pictureFileName) == null)
            {
                ddlImgCapList.SelectedIndex = 0;
            }
            else
            {
                ddlImgCapList.SelectedIndex = ddlImgCapList.Items.IndexOf(ddlImgCapList.Items.FindByText(pictureFileName));
                ddlImgCapList_ChangeSelection(this, e);
            }
            
            txtCapPrice.Enabled = true;
            txtCapName.Enabled = true;
            txtCapDescription.Enabled = true;
            imgCapImagePreview.Enabled = true;
            ddlImgCapList.Enabled = true;

            ddlCapCategories.Enabled = true;
            ddlCapSuppliers.Enabled = true;

            btnSaveChanges.Enabled = true;
            btnCancelChanges.Enabled = true;

            lblMessageJumboTron.Text = "Item " + lblCapId.Text + " Loaded.";
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlImgCapList_ChangeSelection(object sender, EventArgs e)
    {
        if (ddlImgCapList.SelectedItem.Text.Equals(GeneralConstants.CapImageDefaultListName))
        {
            imgCapImagePreview.ImageUrl = ddlImgCapList.SelectedItem.Value;
        }
        else
        {
            imgCapImagePreview.ImageUrl = ddlImgCapList.SelectedItem.Value;
        }
    }

    /// <summary>
    ///     Prepare Product form with a new available id, so user can add a new Product.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddButton_Click(object sender, EventArgs e)
    {
        lblCapId.Text = String.Empty;
        txtCapName.Text = String.Empty;
        txtCapPrice.Text = GeneralConstants.CapPriceNew;
        txtCapDescription.Text = String.Empty;
        txtCapName.Enabled = true;
        txtCapPrice.Enabled = true;
        txtCapDescription.Enabled = true;
        ddlCapCategories.Enabled = true;
        ddlCapSuppliers.Enabled = true;

        if (ddlImgCapList.Items.Count == 0)
        {
            ddlImgCapList.Enabled = false;
            ddlImgCapList.SelectedIndex = 0;
        }
        else
        {
            ddlImgCapList.Enabled = true;
            ddlImgCapList.SelectedIndex = 0;
        }

        imgCapImagePreview.ImageUrl = GeneralConstants.CapImageDefaultFileName;

        txtCapName.Focus();

        btnSaveChanges.Enabled = true;
        btnCancelChanges.Enabled = true;

        lblMessageJumboTron.Text = "Ready to add Cap. Please fill out the required fields.";
    }

    /// <summary>
    ///     Undo any uncommitted changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtCapName.Enabled = false;
        txtCapDescription.Enabled = false;
        txtCapPrice.Enabled = false;
        imgCapImagePreview.ImageUrl = GeneralConstants.CapImageDefaultFileName;
        ddlCapCategories.Enabled = false;
        ddlCapSuppliers.Enabled = false;

        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;

        ddlImgCapList.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Validation function for the Product Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void CapTextValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateGenericName(ref args);
    }

    /// <summary>
    ///     Validation function for the Product Price
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void CapMoneyValidation(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateMoneyInput(ref args);
    }

    
    /// <summary>
    ///     Save Changes.
    ///     If id is for an existing Product, update the Product.
    ///     Else add a new Product.
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
                id = Convert.ToInt32(lblCapId.Text);
            }
            catch (FormatException)
            {
                id = -1;
            }

            controller.AddOrUpdateCap(id,
                txtCapName.Text, Convert.ToSingle(txtCapPrice.Text), txtCapDescription.Text,
                imgCapImagePreview.ImageUrl, Convert.ToInt32(ddlCapCategories.SelectedValue), Convert.ToInt32(ddlCapSuppliers.SelectedValue));

            Reload_Sidebar();

            PrepareListOfUploadedImages();

            lblMessageJumboTron.Text = "SUCCESS: Cap added or updated: " +
                                        txtCapName.Text +
                                        ", " + imgCapImagePreview.ImageUrl;
                            
        }
        
    }
}