<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.master" AutoEventWireup="true" CodeFile="AdminOrders.aspx.cs" Inherits="AdminOrders" %>

<%--  
    The Admin page for the Orders Entity.
    
    Change Log:
        24-8-16  15:30       AskewR04        Created page and layout.

--%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Orders</title>
    <script type="text/javascript" src="~/Content/common.js" >
    </script>
    <script type="text/javascript" src="~/Content/Validation.js">
    </script>
</asp:Content>

<asp:Content ID="AdminOrderSideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="OrderListingSection" class="AdminSection" 
        style="position: fixed; overflow-y: scroll; overflow-x: hidden; width: 22%; max-height:86%">
        <%-- to be filled with items from the currently used DB Table --%>
        <div class="row">
            <div id="divLeftSidebar" class="col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <H3 style="margin-left: 25%"><asp:Label ID="lblSideBarHeader" Text="SideBarName" runat="server" /></H3>  
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <span class="BlankRow"></span>
            </div>
        </div>
    
        <asp:UpdatePanel ID="ItemSideBarPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Repeater 
                            id="dbrptSideBarItems" 
                            OnItemCommand="dbrptSideBarItems_ItemCommand"
                            runat="server">
                            <HeaderTemplate>
                                <table class="col-md-12">
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr class="col-md-12">
                                    <td class="col-md-12 SidebarItem">
                                        <asp:Button
                                            ID = "btnSideBarItem"
                                            CssClass = "col-md-12 SidebarButton" 
                                            CausesValidation="false"
                                            Text ='<%# String.Format("{0}, {1}", 
                                                DataBinder.Eval(Container.DataItem, "id"), DataBinder.Eval(Container.DataItem, "status")) %>'
                                            CommandName="loadItem" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                            runat="server"/>
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </table>
                            </FooterTemplate>

                        </asp:Repeater>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

<asp:Content ID="AdminOrderMain" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div id="OrderEditingForm" class="container AdminSection" runat="server">
        <div class="row">
            <span class="BlankRow"></span>
        </div>

        <asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <span class="BlankRow">
                        </span>
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        
                    </div>
                    <div class="col-md-4">
                        <span>
                            <b><asp:Label CssClass="MainItemHeader" ID="lblOrderIdHeader" runat="server" AssociatedControlID="lblOrderId" 
                                                Text="ID:" /></b>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblOrderId" Text="Order_Id" runat="server" /></b>
                        </span>
                    </div>
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                    <div class="col-md-4">
                        <span>
                            <asp:Label ID="lblOrderCustomerHeader" runat="server" AssociatedControlID="lblCustomerName" 
                                                CssClass="MainItemHeader" Text="Customer:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><b><asp:Label ID="lblCustomerName" Text="Customer_Details" 
                            Enabled="false" runat="server" /></b></span>
                    </div>
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                    <div class="col-md-4">
                        <span>
                            <asp:Label ID="lblOrderStatusHeader" runat="server" AssociatedControlID="ddlOrderStatus" 
                                                CssClass="MainItemHeader" Text="Status:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><b>
                            <asp:DropDownList ID="ddlOrderStatus" Enabled="false" runat="server">
                            </asp:DropDownList>
                        </b></span>
                    </div>
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftLeft"><b>Order Subtotal:</b></span>
                    </div>
                    <div class="col-md-1">
                        <span class="ContentShiftRight"><b>$</b></span>
                    </div>
                    <div class="col-md-3">
                        <span class="ContentShiftRight">
                            <asp:Label ID="lblOrderSubtotal" Text="0.00" runat="server" />
                        </span>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftLeft"><b>Order GST:</b></span>
                    </div>
                    <div class="col-md-1">
                        <span class="ContentShiftRight"><b>$</b></span>
                    </div>
                    <div class="col-md-3">
                        <span class="ContentShiftRight">
                            <asp:Label ID="lblOrderGst" Text="0.00" runat="server" />
                        </span>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftLeft"><b>Order Full Total:</b></span>
                    </div>
                    <div class="col-md-1">
                        <span class="ContentShiftRight"><b>$</b></span>
                    </div>
                    <div class="col-md-3">
                        <span class="ContentShiftRight">
                            <asp:Label ID="lblOrderTotal" Text="0.00" runat="server" />
                        </span>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                    <div class="col-md-8">
                        <asp:Table ID="tblOrderItemListing" CssClass="AdminTable" runat="server">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell Text="Cap"/>
                                <asp:TableHeaderCell Text="Colour"/>
                                <asp:TableHeaderCell Text="Quantity"/>
                                <asp:TableHeaderCell Text="Price"/>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </div>
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <span class="ContentShiftLeft">
                            <asp:Button ID="btnCancelChanges" OnClick="CancelButton_Click" CausesValidation="false" 
                                CssClass="MainButton" Text="Cancel" Enabled="false" runat="server"/>
                        </span>
                    </div>
                    <div class="col-md-6">
                        <span class="ContentShiftRight">
                            <asp:Button ID="btnSaveChanges" 
                                OnClick="SaveButton_Click" 
                                CssClass="MainButton" Text="Save Changes" Enabled="false" runat="server"/>
                        </span>
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-12" >
                        <asp:Label id="lblMessageJumboTron" CssClass="jumbotron" style="float: right; margin: 4px;" runat="server" />
                    </div>
                </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveChanges" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>