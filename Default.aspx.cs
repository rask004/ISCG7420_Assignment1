using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using BusinessLayer;

/// <summary>
/// 
/// </summary>
public partial class _Default : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private void Load_Categories()
    {
        PublicController controller = new PublicController();
        List<Category> categories = controller.GetCategoriesWithCaps();
        dlstCategoriesWithProducts.DataSource = categories;
        dlstCategoriesWithProducts.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void LoadInitialProducts()
    {
        PublicController controller = new PublicController();
        List<Category> categories = controller.GetCategoriesWithCaps();
        if (categories.Count > 0)
        {
            int categoryId = categories[0].ID;
            string categoryName = categories[0].Name;
            List<Cap> caps = controller.GetAllCapsByCategoryId(categoryId);

            Load_Caps(caps);
            lblCentreHeader.Text = categoryName;
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="caps"></param>
    private void Load_Caps(List<Cap> caps)
    {
        lstvAvailableProducts.DataSource = caps;
        lstvAvailableProducts.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Bind_Colours()
    {
        PublicController controller = new PublicController();
        List<Colour> colours = controller.GetAllcolours();
        ddlCapColours.DataSource = colours;
        ddlCapColours.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Bind_CartItems()
    {
        List<OrderItem> cartItems;
        if (Session[GeneralConstants.SessionCartItems] != null)
        {
            cartItems = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        }
        else
        {
            cartItems = new List<OrderItem>();
        }

        lstvShoppingItems.DataSource = cartItems;
        lstvShoppingItems.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Loaded Page " + Page.Title + ", " + Request.RawUrl);


        if (!IsPostBack)
        {
            Load_Categories();

            Bind_Colours();

            Bind_CartItems();

            LoadInitialProducts();

            UpdateCartTotals();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dlstCategoriesWithProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            ImageButton img = (e.Item.FindControl("imgCategoryPicture") as ImageButton);
            try
            {
                int id = Convert.ToInt32((e.Item.FindControl("lblCategoryId") as Label).Text);
                PublicController controller = new PublicController();
                if (img != null)
                {
                    img.ImageUrl = controller.GetFirstCapImageByCategoryId(id);
                }
            }
            catch (NullReferenceException ex)
            {
                (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error, "NullReferenceException, Could not reference a control. Method:" + ex.TargetSite + "; message:" + ex.Message);
                if (img != null)
                {
                    img.ImageUrl = GeneralConstants.CapImageDefaultFileName;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dlstCategoriesWithProducts_OnItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "loadCapsByCategory")
        {
            PublicController controller = new PublicController();
            int categoryId = Convert.ToInt32(e.CommandArgument);
            string categoryName = controller.GetCategoryName(categoryId);
            List<Cap> caps = controller.GetAllCapsByCategoryId(categoryId);

            lblCentreHeader.Text = categoryName;

            Load_Caps(caps);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvAvailableProducts_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            // think I need to do something here, but not sure what.
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvAvailableProducts_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "loadCapDetails")
        {
            PublicController controller = new PublicController();
            int capId = Convert.ToInt32(e.CommandArgument);
            Cap cap = controller.GetCapDetails(capId);

            lblCurrentCapId.Text = cap.ID.ToString();
            lblCurrentCapName.Text = cap.Name;
            lblCurrentCapPrice.Text = cap.Price.ToString("C", CultureInfo.CurrentCulture);

            imgCurrentCapPicture.ImageUrl = cap.ImageUrl;
            lblCurrentCapDescription.Text = cap.Description;

            nptQuantity.Value = "1";
            ddlCapColours.SelectedIndex = 0;

            tblSingleItemDetail.Visible = true;
            lstvAvailableProducts.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProceedToCheckout_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCartClear_OnClick(object sender, EventArgs e)
    {
        List<OrderItem> cartItems = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        cartItems.Clear();
        Bind_CartItems();
        UpdateCartTotals();
    }
    
    /// <summary>
    /// 
    /// </summary>
    protected void UpdateCartTotals()
    {
        List<OrderItem> cartItems = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        if (cartItems != null)
        {
            if (cartItems.Count > 0)
            {
                double subtotal = 0;
                foreach (var orderItem in cartItems)
                {
                    subtotal += orderItem.Quantity * orderItem.Cap.Price;
                }
                double gst = subtotal*GeneralConstants.MoneyGstRate;

                lblSubtotal.Text = subtotal.ToString("C", CultureInfo.CurrentCulture);
                lblGst.Text = gst.ToString("C", CultureInfo.CurrentCulture);
                lblTotalCost.Text = (subtotal + gst).ToString("C", CultureInfo.CurrentCulture);
            }
            else
            {
                lblSubtotal.Text = "0.00";
                lblGst.Text = "0.00";
                lblTotalCost.Text = "0.00";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        tblSingleItemDetail.Visible = false;
        lstvAvailableProducts.Visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCapToBasket_OnClick(object sender, EventArgs e)
    {
        OrderItem newShoppingCartItem = new OrderItem();
        newShoppingCartItem.CapId = Convert.ToInt32(lblCurrentCapId.Text);
        newShoppingCartItem.ColourId = Convert.ToInt32(ddlCapColours.SelectedValue);

        PublicController controller = new PublicController();
        newShoppingCartItem.Cap = controller.GetCapById(newShoppingCartItem.CapId);
        newShoppingCartItem.Colour = controller.GetColourById(newShoppingCartItem.ColourId);
        newShoppingCartItem.Quantity = Convert.ToInt32(nptQuantity.Value);

        List<OrderItem> cartItems = (Session[GeneralConstants.SessionCartItems] as List<OrderItem>);
        bool itemAlreadyExists = false;
        for (int i = 0; i < cartItems.Count; i++)
        {
            if (newShoppingCartItem.CapId == cartItems[i].CapId &&
                newShoppingCartItem.ColourId == cartItems[i].ColourId)
            {
                // Already have cart items with this colour, so just add to the quantity.
                cartItems[i].Quantity += newShoppingCartItem.Quantity;
                itemAlreadyExists = true;
                break;
            }
        }

        if (!itemAlreadyExists)
        {
            (Session[GeneralConstants.SessionCartItems] as List<OrderItem>).Add(newShoppingCartItem);
        }

        Bind_CartItems();

        UpdateCartTotals();
    }
}