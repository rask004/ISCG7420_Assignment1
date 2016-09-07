using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;

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

    private void LoadProducts(int categoryId)
    {
        if (categoryId < 1)
        {

        }
        else
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Categories();

            if (dlstCategoriesWithProducts.Items.Count == 0)
            {
                
            }
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
            string categoryName = (e.Item.FindControl("lblCategoryName") as Label).Text;
            PublicController controller = new PublicController();
            img.ImageUrl = controller.GetFirstCapImageByCategoryId(id);

            lblCentreHeader.Text = categoryName;

            List<Cap> caps = controller.GetAllCapsByCategoryId(id);
            dlstAvailableProducts.DataSource = caps;
            dlstAvailableProducts.DataBind();
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
            int id = Convert.ToInt32((e.Item.FindControl("lblCapId") as Label).Text);
            PublicController controller = new PublicController();
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
}