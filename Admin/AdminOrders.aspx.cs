using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BusinessLayer;
using CommonLogging;
using DataLayer;
using Microsoft.Ajax.Utilities;
using SecurityLayer;

/// <summary>
/// 
///     The Admin page for the Orders Entity.
///
///     Change Log:
/// 
///     24-8-16  15:30       AskewR04 Created page and layout.
/// </summary>
public partial class AdminOrders : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private void Reload_Sidebar()
    {
        AdminController controller = new AdminController();
        dbrptSideBarItems.DataSource = controller.GetOrders();
        dbrptSideBarItems.DataBind();
    }

    /// <summary>
    ///     Load the page, prepare the table of items, and the admin form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info, "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        if (!Page.IsPostBack)
        {
            Reload_Sidebar();

            foreach (string permittedOrderStatus in GeneralConstants.PermittedOrderStatuses)
            {
                ddlOrderStatus.Items.Add(new ListItem {Text = permittedOrderStatus, Value = permittedOrderStatus});
            }

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
            int orderId = Convert.ToInt32(e.CommandArgument);
            Customer customer = controller.GetCustomerByOrderId(orderId);
            int customerId = customer.ID;
            string customerFirstName = controller.GetCustomerFirstName(customerId);
            string customerLastName = controller.GetCustomerFirstName(customerId);

            string orderStatus = controller.GetOrderStatus(orderId);

            lblOrderId.Text = orderId.ToString();
            lblCustomerName.Text = customerId + ", " + customerFirstName + " " + customerLastName;
            for (int i = 0; i < ddlOrderStatus.Items.Count; i++)
            {
                if (ddlOrderStatus.Items[i].Text.Equals(orderStatus))
                {
                    ddlOrderStatus.SelectedIndex = i;
                    break;
                }
            }

            Double subTotal = 0;

            // remove all old table data before adding new.
            for (int i = tblOrderItemListing.Rows.Count - 1; i >= 1; i--)
            {
                tblOrderItemListing.Rows.RemoveAt(i);
            }

            List<OrderItem> orderItems = controller.GetItemsForOrderWithId(orderId);
            TableRow tr;
            TableCell tc;
            if (orderItems.Count == 0)
            {
                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "This order has no products.";
                tr.Cells.Add(tc);
                tblOrderItemListing.Rows.Add(tr);

                lblOrderSubtotal.Text = "0.00";
                lblOrderGst.Text = "0.00";
                lblOrderTotal.Text = "0.00";
            }
            else
            {

                foreach (var orderItem in orderItems)
                {
                    tr = new TableRow();
                    tc = new TableCell {Text = orderItem.Cap.Name};
                    tr.Cells.Add(tc);
                    tc = new TableCell {Text = orderItem.Colour.Name};
                    tr.Cells.Add(tc);
                    tc = new TableCell {Text = orderItem.Quantity.ToString()};
                    tr.Cells.Add(tc);
                    tc = new TableCell {Text = orderItem.Cap.Price.ToString()};
                    tr.Cells.Add(tc);
                    tblOrderItemListing.Rows.Add(tr);

                    subTotal += Convert.ToDouble(orderItem.Cap.Price * orderItem.Quantity);
                }
            }

            Double gst = subTotal * GeneralConstants.MoneyGstRate;

            lblOrderSubtotal.Text = subTotal.ToString();
            lblOrderGst.Text = (gst).ToString();
            lblOrderTotal.Text = (subTotal + gst).ToString();

            ddlOrderStatus.Enabled = true;

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
    ///     Validation function for the Colour Name
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void ColourNameValidation(object source, ServerValidateEventArgs args)
    {
        foreach (char c in args.Value)
        {
            if (!Validation.ValidationAlphabetic.Contains(c))
            {
                args.IsValid = false;
                return;
            }
        }

        args.IsValid = true;
    }

    /// <summary>
    ///     Save Changes.
    ///     If id is for an existing Colour, update the Colour.
    ///     Else add a new Colour.
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
            //Reload_Sidebar();

            lblMessageJumboTron.Text = "SUCCESS: Order updated: " + 
                lblOrderId.Text + ", " + ddlOrderStatus.Items[ddlOrderStatus.SelectedIndex].Text;
        }

    }
}