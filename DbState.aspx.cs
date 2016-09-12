﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class DbState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AdminController controller = new AdminController();
        rptCategories.DataSource = controller.GetCategories();
        rptCategories.DataBind();
        rptColours.DataSource = controller.GetColours();
        rptColours.DataBind();
        rptSuppliers.DataSource = controller.GetSuppliers();
        rptSuppliers.DataBind();
        rptCaps.DataSource = controller.GetCaps();
        rptCaps.DataBind();
        rptCustomers.DataSource = controller.GetCustomers();
        rptCustomers.DataBind();

        List<CustomerOrder> orders = controller.GetOrders();
        rptOrders.DataSource = orders;
        rptOrders.DataBind();

        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (var customerOrder in orders)
        {
            foreach (var orderItem in controller.GetItemsForOrderWithId(customerOrder.ID))
            {
                orderItems.Add(orderItem);
            }
        }
        rptOrderItems.DataSource = orderItems;
        rptOrderItems.DataBind();

    }
    
}