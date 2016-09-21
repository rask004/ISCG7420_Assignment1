using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using SecurityLayer;

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

        List<OrderItem> items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;

        // redirect to Default page if cart is empty.
        if (!items.Any())
        {
            Response.Redirect("~/");
        }

        if (!IsPostBack)
        {
            ReBind();

            RecalculateTotals();
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
    protected void RecalculateTotals()
    {
        OrderSummary summary = new OrderSummary();
        List<OrderItem> items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        foreach (var orderItem in items)
        {
            summary.SubTotalPrice += orderItem.Cap.Price * orderItem.Quantity;
        }

        lblSubTotal.InnerText = summary.SubTotalPrice.ToString("C", CultureInfo.CurrentCulture);
        lblSubTotalGst.InnerText = (summary.SubTotalGst).ToString("C", CultureInfo.CurrentCulture);
        lblFullTotal.InnerText = (summary.TotalPrice).ToString("C", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        PublicController controller = new PublicController();
        List<Colour> colours = controller.GetAllColours();
        
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Cancel_OnClick(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder("~/Default.aspx");
        Response.Redirect(builder.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        bool cartContentsChanged = false;
        if (e.CommandName == "editItem")
        {
            var btnModify = e.Item.FindControl("btnModifyItem") as Button;
            btnModify.CommandName = "updateItem";
            btnModify.Text = "Save";

            var input = e.Item.FindControl("nptQuantity") as HtmlInputControl;
            var colourList = e.Item.FindControl("ddlCapColoursCheckout") as DropDownList;
            
            input.Disabled = false;
            colourList.Enabled = true;
        }
        else if (e.CommandName == "updateItem")
        {
            var btnModify = e.Item.FindControl("btnModifyItem") as Button;
            btnModify.CommandName = "editItem";
            btnModify.Text = "Edit";

            OrderItem o = (Session[GeneralConstants.SessionCartItems] as List<OrderItem>)[e.Item.DataItemIndex];
            var input = e.Item.FindControl("nptQuantity") as HtmlInputControl;
            var colourList = e.Item.FindControl("ddlCapColoursCheckout") as DropDownList;

            o.Quantity = Convert.ToInt32(input.Value);
            PublicController controller = new PublicController();
            o.Colour = controller.GetColourById(Convert.ToInt32(colourList.SelectedValue));
            o.ColourId = o.Colour.ID;

            input.Disabled = true;
            colourList.Enabled = false;

            cartContentsChanged = true;
        }
        else if (e.CommandName == "deleteItem")
        {
            (Session[GeneralConstants.SessionCartItems] as List<OrderItem>).RemoveAt(e.Item.DataItemIndex);
            cartContentsChanged = true;
        }
        else if (e.CommandName == "undoItem")
        {
            OrderItem o = (Session[GeneralConstants.SessionCartItems] as List<OrderItem>)[e.Item.DataItemIndex];
            var input = e.Item.FindControl("nptQuantity") as HtmlInputControl;
            var colourList = e.Item.FindControl("ddlCapColoursCheckout") as DropDownList;

            input.Value = o.Quantity.ToString();
            colourList.SelectedIndex = colourList.Items.IndexOf(colourList.Items.FindByValue(o.ColourId.ToString()));

            input.Disabled = true;
            colourList.Enabled = false;

            var btnModify = e.Item.FindControl("btnModifyItem") as Button;
            btnModify.CommandName = "editItem";
            btnModify.Text = "Edit";
        }

        if (cartContentsChanged)
        {
            ReBind();

            RecalculateTotals();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CompleteOrder_OnClick(object sender, EventArgs e)
    {
        PublicController controller = new PublicController();
        string login = Session[Security.SessionIdentifierLogin].ToString();
        List<OrderItem> items = (Session[GeneralConstants.SessionCartItems] as List<OrderItem>);
        controller.PlaceOrderForCustomer(login, items);
        items.Clear();
        StringBuilder builder = new StringBuilder();
        builder.Append("~/Customer");
        builder.Append("?");
        builder.Append(GeneralConstants.QueryStringGeneralMessageKey);
        builder.Append("=");
        builder.Append(GeneralConstants.QueryStringGeneralMessageSuccessfulPlacedNewOrder);
        Response.Redirect(builder.ToString());
    }
}