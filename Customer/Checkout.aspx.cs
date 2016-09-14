using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;

public partial class Customer_Checkout : System.Web.UI.Page
{

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
            ReBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void ReBind()
    {
        List<OrderItem> items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        lstvCheckoutItems.DataSource = items;
        lstvCheckoutItems.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        PublicController controller = new PublicController();
        List<Colour> colours = controller.GetAllcolours();
        
        DropDownList ddlColoursList = e.Item.FindControl("ddlCapColoursCheckout") as DropDownList;
        ddlColoursList.DataSource = colours;
        ddlColoursList.DataBind();
        
        int colourId = (e.Item.DataItem as OrderItem).ColourId;
        int i = 0;
        // assign the correct index in the dropdownlist, according to the colour for this OrderItem
        for (i = ddlColoursList.Items.Count - 1; i >= 0; i--)
        {
            if (Convert.ToInt32(ddlColoursList.Items[i].Value) == colourId)
            {
                break;
            }
        }

        ddlColoursList.SelectedIndex = i;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lstvCheckoutItems.FindControl("dpgItemPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        ReBind();
    }

    protected void UpdateSessionCart()
    {
        List<OrderItem> items = lstvCheckoutItems.DataSource as List<OrderItem>;
    }

    protected void Cancel_OnClick(object sender, EventArgs e)
    {
        UpdateSessionCart();
    }
}