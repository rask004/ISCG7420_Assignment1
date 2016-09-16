using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;

// TODO: Complete Totals, in client section of possible.

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
        Response.RedirectPermanent(builder.ToString());
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
        }
    }
}