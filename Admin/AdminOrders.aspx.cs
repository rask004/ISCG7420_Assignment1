using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Common;
using CommonLogging;

/// <summary>
///     The Admin page for the Orders Entity.
///     Change Log:
///     24-8-16  15:30       AskewR04 Created page and layout.
///     20-9-16     18:06       AskewR04 Final review
/// </summary>
public partial class AdminOrders : Page
{
    /// <summary>
    ///     Reload the sidebar list
    /// </summary>
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetOrders();
        dbrptSideBarItems.DataBind();
    }

    /// <summary>
    ///     Reload the list of order items
    /// </summary>
    private void Rebind_OrderItems()
    {
        int orderId;
        try
        {
            orderId = Convert.ToInt32(Session["AdminOrderId"]);
        }
        catch (FormatException)
        {
            orderId = 0;
        }
        AdminController controller = new AdminController();
        grdvCustomerOrders.DataSource = controller.GetItemsForOrderWithId(orderId);
        grdvCustomerOrders.DataBind();
    }

    /// <summary>
    ///     Load the page, prepare the table of items, and the admin form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        if (!Page.IsPostBack)
        {
            Reload_Sidebar();

            foreach (string permittedOrderStatus in GeneralConstants.PermittedOrderStatuses)
            {
                ddlOrderStatus.Items.Add(new ListItem {Text = permittedOrderStatus, Value = permittedOrderStatus});
            }

            grdvCustomerOrders.DataSource = new List<OrderItem>();
            grdvCustomerOrders.DataBind();

            lblSideBarHeader.Text = "Orders";

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
            int OrderId = Convert.ToInt32(e.CommandArgument);
            Session["AdminOrderId"] = OrderId;
            Customer customer = controller.GetCustomerByOrderId(OrderId);

            // set the customer details
            int customerId = customer.ID;
            string customerFirstName = controller.GetCustomerFirstName(customerId);
            string customerLastName = controller.GetCustomerLastName(customerId);

            string orderStatus = controller.GetOrderStatus(OrderId);
            string datePlaced = controller.GetOrderDate(OrderId).ToString("d");

            lblOrderId.Text = OrderId.ToString();
            lblCustomerName.Text = customerId + ", " + customerFirstName + " " + customerLastName;

            // set list for status to correct value.
            ddlOrderStatus.Enabled = true;
            for (int i = 0; i < ddlOrderStatus.Items.Count; i++)
            {
                if (ddlOrderStatus.Items[i].Text.Equals(orderStatus))
                {
                    ddlOrderStatus.SelectedIndex = i;
                    break;
                }
            }

            lblOrderDate.Text = datePlaced;

            // collect Totals for the order.
            OrderSummary summary = new OrderSummary {OrderId = OrderId};
            summary.OrderId = OrderId;
            List<OrderItem> orderItems = controller.GetItemsForOrderWithId(OrderId);

            foreach (var orderItem in orderItems)
            {
                summary.SubTotalPrice += Convert.ToDouble(orderItem.Cap.Price*orderItem.Quantity);
                summary.TotalQuantity += orderItem.Quantity;
            }

            lblOrderSubtotal.Text = summary.SubTotalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
            lblOrderGst.Text = summary.SubTotalGst.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
            lblOrderTotal.Text = summary.TotalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            Rebind_OrderItems();


            btnSaveChanges.Enabled = true;
            btnCancelChanges.Enabled = true;

            lblMessageJumboTron.Text = "Item " + lblOrderId.Text + " Loaded.";
        }
    }

    /// <summary>
    ///     Undo any uncommitted changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        btnSaveChanges.Enabled = false;
        btnCancelChanges.Enabled = false;

        ddlOrderStatus.Enabled = false;

        lblMessageJumboTron.Text = "Operation Cancelled.";
    }

    /// <summary>
    ///     Save Changes.
    ///     Update any pending change in status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            AdminController controller = new AdminController();

            controller.UpdateOrderStatus(Convert.ToInt32(lblOrderId.Text), ddlOrderStatus.SelectedValue);

            // Only updating (no Inserts or Deletes), therefore sidebar contents will not change.
            Reload_Sidebar();

            lblMessageJumboTron.Text = "SUCCESS: Order updated: " +
                                       lblOrderId.Text + ", " + ddlOrderStatus.Items[ddlOrderStatus.SelectedIndex].Text;
        }
    }

    /// <summary>
    ///     Manage pagination for multiple orders.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvCustomerOrders_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvCustomerOrders.PageIndex = e.NewPageIndex;
        Rebind_OrderItems();
    }
}