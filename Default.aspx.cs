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
        List<Category> categories =  controller.GetCategoriesWithCaps();
        dlstCategoriesWithProducts.DataSource = categories;
        dlstCategoriesWithProducts.DataBind();
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
            int id = Convert.ToInt32((e.Item.FindControl("lblCategoryId") as Label).Text);
            PublicController controller = new PublicController();
            img.ImageUrl = controller.GetFirstCapImageByCategoryId(id);
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

            lstvAvailableProducts.DataSource = caps;
            lstvAvailableProducts.DataBind();
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
                lblSubtotal.Text = gst.ToString("C", CultureInfo.CurrentCulture);
                lblSubtotal.Text = (subtotal + gst).ToString("C", CultureInfo.CurrentCulture);
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
        throw new NotImplementedException();
    }

}