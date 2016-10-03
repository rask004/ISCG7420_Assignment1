using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;

/// <summary>
///     Changelog:
///     04-09-16    18:04   AskewR04    created page
/// </summary>
public partial class _Default : Page
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
            return Convert.ToInt16(ViewState["pg"]);
        }
        set { ViewState["pg"] = value; }
    }

    /// <summary>
    /// </summary>
    public int CurrentCategoryId
    {
        get
        {
            if (ViewState["cid"] == null)
            {
                return 0;
            }
            return Convert.ToInt32(ViewState["cid"]);
        }
        set { ViewState["cid"] = value; }
    }


    /// <summary>
    /// </summary>
    private void Load_Categories()
    {
        var controller = new PublicController();
        var categories = controller.GetCategoriesWithCaps();
        lvCategoriesWithProducts.DataSource = categories;
        lvCategoriesWithProducts.DataBind();
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
    ///     On no postback, load the initial set of prodicts to show
    /// </summary>
    private void LoadInitialProducts()
    {
        var controller = new PublicController();
        var categories = controller.GetCategoriesWithCaps();
        if (categories.Count > 0)
        {
            var categoryId = categories[0].ID;
            CurrentCategoryId = categoryId;
            var categoryName = categories[0].Name;

            Load_Caps();
            lblCentreHeader.Text = categoryName;

        }
    }

    /// <summary>
    ///     Load the caps for the current category
    /// </summary>
    private void Load_Caps()
    {
        var controller = new PublicController();
        var caps = controller.GetAllCapsByCategoryId(CurrentCategoryId);
        var pagedData = new PagedDataSource();
        pagedData.DataSource = caps;
        pagedData.CurrentPageIndex = AvailableProductsCurrentPageIndex;
        pagedData.AllowPaging = true;
        pagedData.PageSize = 6;
        dlstAvailableProducts.DataSource = pagedData;
        Prepare_PageButtons(pagedData);
        dlstAvailableProducts.DataBind();
    }

    /// <summary>
    ///     Load the required pagination buttons
    /// </summary>
    /// <param name="datasource">PagedDataSource, paginates the data</param>
    private void Prepare_PageButtons(PagedDataSource datasource)
    {
        if (datasource.PageCount == 0)
        {
            btnPreviousProductPage.Visible = false;
            btnNextProductPage.Visible = false;
            lblCurrentProductPage.Visible = false;
        }
        else
        {
            lblCurrentProductPage.Visible = true;
            lblCurrentProductPage.Text = "Page " + (AvailableProductsCurrentPageIndex + 1).ToString() + " of  " + datasource.PageCount.ToString();
            btnPreviousProductPage.Visible = !datasource.IsFirstPage;
            btnNextProductPage.Visible = !datasource.IsLastPage;
        }
    }

    /// <summary>
    ///     load in the available colours
    /// </summary>
    private void Bind_Colours()
    {
        var controller = new PublicController();
        var colours = controller.GetAllColours();
        ddlCapColours.DataSource = colours;
        ddlCapColours.DataBind();
    }

    /// <summary>
    ///     Rebind the items in the cart
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

        lvShoppingItems.DataSource = cartItems;
        lvShoppingItems.DataBind();
    }

    /// <summary>
    ///     Load the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);


        if (!IsPostBack)
        {
            Load_Categories();

            Bind_Colours();

            Bind_CartItems();

            LoadInitialProducts();

            ShowProductsGrid();

            UpdateCartTotals();

            var items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;

            if (!items.Any())
            {
                // disable checkout button.
                var btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
                if (btnCheckout != null)
                {
                    btnCheckout.Enabled = false;
                    btnCheckout.CssClass += " disabled";
                }
            }
        }
    }

    /// <summary>
    ///     ItemDataBound eventhandler, listview categories
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LstvCategoriesWithProductsOnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var imgButton = e.Item.FindControl("ibtnCategoryPicture") as ImageButton;
        try
        {
            var id = Convert.ToInt32((e.Item.FindControl("lblCategoryId") as Label).Text);
            var controller = new PublicController();
            if (imgButton != null)
            {
                imgButton.ImageUrl = controller.GetFirstCapImageByCategoryId(id);
            }
        }
        catch (NullReferenceException ex)
        {
            (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
                "NullReferenceException, Could not reference a control. Method:" + ex.TargetSite + "; message:" +
                ex.Message);
            if (imgButton != null)
            {
                imgButton.ImageUrl = GeneralConstants.CapImageDefaultFileName;
            }
        }
    }

    /// <summary>
    ///     OnItemCommand eventhandler, listview Categories
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void LstvCategoriesWithProducts_OnItemCommand(object source, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "loadCapsByCategory")
        {
            var controller = new PublicController();
            var categoryId = Convert.ToInt32(e.CommandArgument);
            CurrentCategoryId = categoryId;
            var categoryName = controller.GetCategoryName(categoryId);
            lblCentreHeader.Text = categoryName;

            AvailableProductsCurrentPageIndex = 0;
            Load_Caps();



            ShowProductsGrid();
        }
    }

    /// <summary>
    ///     OnItemDataBound eventhandler, datalist prodicts
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
    ///     button click handler, checkout button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProceedToCheckout_OnClick(object sender, EventArgs e)
    {
        var builder = new StringBuilder("~/Customer/Checkout.aspx");
        Response.Redirect(builder.ToString());
    }

    /// <summary>
    ///     click handler, clear cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCartClear_OnClick(object sender, EventArgs e)
    {
        var cartItems = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        cartItems.Clear();
        Bind_CartItems();
        UpdateCartTotals();

        // no cart items, so disable checkout button.
        var btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
        if (btnCheckout != null)
        {
            btnCheckout.Enabled = false;
        }
    }

    /// <summary>
    ///     support method, update the totals shown in the cart
    /// </summary>
    protected void UpdateCartTotals()
    {
        var cartItems = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        if (cartItems != null)
        {
            if (cartItems.Count > 0)
            {
                double subtotal = 0;
                foreach (var orderItem in cartItems)
                {
                    subtotal += orderItem.Quantity*orderItem.Cap.Price;
                }
                var gst = subtotal*GeneralConstants.MoneyGstRate;

                lblSubtotal.InnerText = subtotal.ToString("F2");
                lblGst.InnerText = gst.ToString("F2");
                lblTotalCost.InnerText = (subtotal + gst).ToString("F2");
            }
            else
            {
                lblSubtotal.InnerText = "0.00";
                lblGst.InnerText = "0.00";
                lblTotalCost.InnerText = "0.00";
            }
        }
    }

    /// <summary>
    ///     click handler, cancel the current product view
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        ShowProductsGrid();
    }

    /// <summary>
    ///     click handler, add a cap to the shopping cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCapToBasket_OnClick(object sender, EventArgs e)
    {
        var newShoppingCartItem = new OrderItem();
        newShoppingCartItem.CapId = Convert.ToInt32(lblCurrentCapId.Text);
        newShoppingCartItem.ColourId = Convert.ToInt32(ddlCapColours.SelectedValue);

        var controller = new PublicController();
        newShoppingCartItem.Cap = controller.GetCapById(newShoppingCartItem.CapId);
        newShoppingCartItem.Colour = controller.GetColourById(newShoppingCartItem.ColourId);
        newShoppingCartItem.Quantity = Convert.ToInt32(nptQuantity.Value);

        var cartItems = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        var itemAlreadyExists = false;
        for (var i = 0; i < cartItems.Count; i++)
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
        var btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
        if (btnCheckout != null)
        {
            btnCheckout.Enabled = true;
            btnCheckout.CssClass = "btn btn-primary";
        }
    }

    /// <summary>
    ///     OnItemCommand handler, datalist products
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dlstAvailableProducts_OnItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "loadCapDetails")
        {
            var controller = new PublicController();
            var capId = Convert.ToInt32(e.CommandArgument);
            var cap = controller.GetCapDetails(capId);

            lblCurrentCapId.Text = cap.ID.ToString();
            lblCurrentCapName.Text = cap.Name;
            lblCurrentCapPrice.Text = cap.Price.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            imgCurrentCapPicture.Src = cap.ImageUrl;
            divCurrentCapDescription.InnerText = cap.Description;

            nptQuantity.Value = "1";
            ddlCapColours.SelectedIndex = 0;

            ShowProductDetailsTable();
        }
    }

    /// <summary>
    ///     Page change handler, listview for cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvShoppingItems_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvShoppingItems.FindControl("dpgItemPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows,
            false);
        Bind_CartItems();
        UpdateCartTotals();
    }

    /// <summary>
    ///     OnItemCommand handler, listview for cart
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

            var items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;

            if (!items.Any())
            {
                // disable checkcout button.
                var btnCheckout = lgnviewCart.FindControl("btnProceedToCheckout") as Button;
                if (btnCheckout != null)
                {
                    btnCheckout.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    ///     clikc handler, change the current product page.
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
    }

    /// <summary>
    ///     Update the current page for Categories.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvCategoriesWithProducts_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager pager = lvCategoriesWithProducts.FindControl("dpgItemPager") as DataPager;
        pager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        Load_Categories();
    }
}