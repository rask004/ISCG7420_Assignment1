using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;

/// <summary>
/// 
/// </summary>
public partial class _Default : System.Web.UI.Page
{
    /// <summary>
    ///     Property to aid pagination of the Available Products Section.
    /// </summary>
    public int AvailableProductsCurrentPageIndex
    {
        get
        {
            if (ViewState["pg"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(ViewState["pg"]);
            }
        }
        set
        {
            ViewState["pg"] = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int CurrentCategoryId
    {
        get
        {
            if (ViewState["cid"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ViewState["cid"]);
            }
        }
        set
        {
            ViewState["cid"] = value;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void Load_Categories()
    {
        PublicController controller = new PublicController();
        List<Category> categories = controller.GetCategoriesWithCaps();
        lstvCategoriesWithProducts.DataSource = categories;
        lstvCategoriesWithProducts.DataBind();
    }

    /// <summary>
    ///     Show the Grid of Available Products
    /// </summary>
    private void ShowProductsGrid()
    {
        tblSingleItemDetail.Visible = false;
        divAvailableProducts.Visible = true;
    }

    /// <summary>
    ///     Show the Details section for the currently viewed product.
    /// </summary>
    private void ShowProductDetailsTable()
    {
        tblSingleItemDetail.Visible = true;
        divAvailableProducts.Visible = false;
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
            CurrentCategoryId = categoryId;
            string categoryName = categories[0].Name;

            Load_Caps();
            lblCentreHeader.Text = categoryName;

            lblCurrentProductPage.Text = (AvailableProductsCurrentPageIndex + 1).ToString();
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void Load_Caps()
    {
        PublicController controller = new PublicController();
        List<Cap> caps = controller.GetAllCapsByCategoryId(CurrentCategoryId);
        PagedDataSource pagedData = new PagedDataSource();
        pagedData.DataSource = caps;
        pagedData.CurrentPageIndex = AvailableProductsCurrentPageIndex;
        pagedData.AllowPaging = true;
        pagedData.PageSize = 9;
        dlstAvailableProducts.DataSource = pagedData;
        Prepare_PageButtons(pagedData);
        dlstAvailableProducts.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="datasource"></param>
    private void Prepare_PageButtons(PagedDataSource datasource)
    {
        btnPreviousProductPage.Enabled = !(datasource.IsFirstPage);
        btnNextProductPage.Enabled = !(datasource.IsLastPage);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Bind_Colours()
    {
        PublicController controller = new PublicController();
        List<Colour> colours = controller.GetAllColours();
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

            ShowProductsGrid();

            UpdateCartTotals();

            List<OrderItem> items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;

            if (!items.Any())
            {
                // disable checkout button.
                Button btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
                if (btnCheckout != null)
                {
                    btnCheckout.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LstvCategoriesWithProductsOnItemDataBound(object sender, ListViewItemEventArgs e)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void LstvCategoriesWithProducts_OnItemCommand(object source, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "loadCapsByCategory")
        {
            PublicController controller = new PublicController();
            int categoryId = Convert.ToInt32(e.CommandArgument);
            CurrentCategoryId = categoryId;
            string categoryName = controller.GetCategoryName(categoryId);
            lblCentreHeader.Text = categoryName;

            Load_Caps();

            ShowProductsGrid();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dlstAvailableProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            // think I need to do something here, but not sure what.
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProceedToCheckout_OnClick(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder("~/Customer/Checkout.aspx");
        Response.Redirect(builder.ToString());
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

        // no cart items, so disable checkout button.
        Button btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
        if (btnCheckout != null)
        {
            btnCheckout.Enabled = false;
        }
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
        ShowProductsGrid();
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

        // cart items count > 0, therefore make sure checkout button is enabled.
        Button btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
        if (btnCheckout != null)
        {
            btnCheckout.Enabled = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dlstAvailableProducts_OnItemCommand(object source, DataListCommandEventArgs e)
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

            ShowProductDetailsTable();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvShoppingItems_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lstvShoppingItems.FindControl("dpgItemPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        Bind_CartItems();
        UpdateCartTotals();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvShoppingItems_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteCartItem")
        {
            (Session[GeneralConstants.SessionCartItems] as List<OrderItem>).RemoveAt(e.Item.DataItemIndex);
            Bind_CartItems();
            UpdateCartTotals();

            List<OrderItem> items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;

            if (!items.Any())
            {
                // disable checkcout button.
                Button btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
                if (btnCheckout != null)
                {
                    btnCheckout.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChangeProductPage_OnClick(object sender, EventArgs e)
    {
        if ((sender as LinkButton).Equals(btnPreviousProductPage))
        {
            AvailableProductsCurrentPageIndex--;
        }
        else if ((sender as LinkButton).Equals(btnNextProductPage))
        {
            AvailableProductsCurrentPageIndex++;
        }

        Load_Caps();

        lblCurrentProductPage.Text = (AvailableProductsCurrentPageIndex + 1).ToString();
    }
}