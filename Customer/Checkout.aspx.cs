using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;
using Microsoft.AspNet.Identity;
using SecurityLayer;

public partial class Customer_Checkout : Page
{
    /// <summary>
    ///     load the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Security.SessionIdentifierSecurityToken] == null)
        {
            Session.Abandon();
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Response.Redirect("~/Default");
        }

        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        var items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;

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
    ///     reload the checkout items
    /// </summary>
    protected void ReBind()
    {
        var items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        lvCheckoutItems.DataSource = items;
        lvCheckoutItems.DataBind();
    }

    /// <summary>
    ///     recalculate the checkout totals.
    /// </summary>
    protected void RecalculateTotals()
    {
        var summary = new OrderSummary();
        var items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        foreach (var orderItem in items)
        {
            summary.SubTotalPrice += orderItem.Cap.Price*orderItem.Quantity;
        }

        lblSubTotal.InnerText = summary.SubTotalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        lblSubTotalGst.InnerText = summary.SubTotalGst.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        lblFullTotal.InnerText = summary.TotalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
    }

    /// <summary>
    ///     ItemDataBound handler, checkout listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var controller = new PublicController();
        var colours = controller.GetAllColours();

        var ddlColoursList = e.Item.FindControl("ddlCapColoursCheckout") as DropDownList;
        ddlColoursList.DataSource = colours;
        ddlColoursList.DataBind();

        var colourId = (e.Item.DataItem as OrderItem).ColourId;
        var i = 0;
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
    ///     page change handler, checkout listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvCheckoutItems.FindControl("dpgItemPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows,
            false);
        ReBind();
    }

    /// <summary>
    ///     click handler, cancel checkout
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Cancel_OnClick(object sender, EventArgs e)
    {
        var builder = new StringBuilder("~/Default.aspx");
        Response.Redirect(builder.ToString());
    }

    /// <summary>
    ///     ItemCommand handler, checkout listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstvCheckoutItems_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        var cartContentsChanged = false;
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

            var o = (Session[GeneralConstants.SessionCartItems] as List<OrderItem>)[e.Item.DataItemIndex];
            var input = e.Item.FindControl("nptQuantity") as HtmlInputControl;
            var colourList = e.Item.FindControl("ddlCapColoursCheckout") as DropDownList;

            o.Quantity = Convert.ToInt32(input.Value);
            var controller = new PublicController();
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
            var o = (Session[GeneralConstants.SessionCartItems] as List<OrderItem>)[e.Item.DataItemIndex];
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
    ///     click handler, do checkout 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CompleteOrder_OnClick(object sender, EventArgs e)
    {
        var controller = new PublicController();
        var login = Session[Security.SessionIdentifierLogin].ToString();
        var items = Session[GeneralConstants.SessionCartItems] as List<OrderItem>;
        try
        {
            controller.PlaceOrderForCustomer(login, items);
        }
        catch (Exception lastEx)
        {
            if (lastEx is NullReferenceException)
            {
                var ex = new Exception("The login does not refer to an existing customer.", lastEx);
                throw ex;
            }
                // should log person out but don't know how

            if (lastEx is ArgumentOutOfRangeException)
            {
                var ex = new Exception("Attempted to insert new order but Encountered an Error.", lastEx);
                throw ex;
            }
            throw lastEx;
        }
        items.Clear();
        var builder = new StringBuilder();
        builder.Append("~/Customer");
        builder.Append("?");
        builder.Append(GeneralConstants.QueryStringGeneralMessageKey);
        builder.Append("=");
        builder.Append(GeneralConstants.QueryStringGeneralMessageSuccessfulPlacedNewOrder);
        Response.Redirect(builder.ToString());
    }


    /// <summary>
    ///     manage errors
    /// </summary>
    /// <param name="e"></param>
    protected override void OnError(EventArgs e)
    {
        Session["lastError"] = Server.GetLastError();
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Error,
            Server.GetLastError().Message + "; " + Request.RawUrl);
        Response.Redirect("~/Error/Default");
    }
}