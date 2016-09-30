<%@ Page Title="Quality Caps - Checkout" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Customer_Checkout" %>
<%@ Import Namespace="System.Globalization" %>

<%--  
    The page for the Quality Caps Website.
    
    Change Log:

--%>
<asp:Content ID="Content1" ContentPlaceHolderID="AdditionalScripts" Runat="Server">
    <script type="text/javascript" src="../Content/common.js">
    
    </script>
    <script type="text/javascript" src="../Content/Validation.js">
    
    </script>
    <script type="text/javascript">
        function updateTotal() {
            $(document)
                .ready(function() {
                    var count = $("#nptQuantity").Value;
                    var price = $("#lblCapPrice").Value;
                    var cost = count * price;
                    $("#lblTotalPrice").Value = "$" + cost.toFixed(2).toString();
                });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
<div class="container PageSection" style="border: black solid 1px;">
<div class="row">
    <span class="DecoHeader" style="margin-left: 12%">
    <div >
        <H3>Checkout</H3>
    </div>
    </span>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>

<div class="row">
    <span class="BlankRow"></span>
</div>

<asp:UpdatePanel ID="ShoppingCartPanel" UpdateMode="Conditional" ChildrenAsTriggers="True" runat="server">
<ContentTemplate>
<div class="container-fluid" style="border: 1px solid black; padding: 0.5%">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <asp:ListView ID="lvCheckoutItems"
                          OnItemDataBound="lstvCheckoutItems_OnItemDataBound"
                          OnItemCommand="lstvCheckoutItems_OnItemCommand"
                          OnPagePropertiesChanging="lstvCheckoutItems_OnPagePropertiesChanging"
                          runat="server">

                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder">

                    </asp:PlaceHolder>
                    <br/>
                    <div class="row">
                        <div class="col-xs-0 col-sm-4 col-md-4"></div>
                        <div class="col-xs-12 col-sm-4 col-md-4">
                            <asp:DataPager ID="dpgItemPager" runat="server" PagedControlID="lvCheckoutItems" PageSize="2">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                ShowNextPageButton="false"/>
                                    <asp:NumericPagerField ButtonType="Link"/>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false"/>
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div class="col-xs-0 col-sm-4 col-md-4"></div>
                    </div>
                </LayoutTemplate>

                <ItemTemplate>
                    <div class="row">
                        <div class="col-xs-12 col-sm-11 col-md-11">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-3">
                                    <asp:Image Width="75%" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Cap.ImageUrl") %>' runat="server"/>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-9">
                                    <div class="row">
                                        <div class="col-xs-2 col-sm-3 col-md-3">
                                            <label># <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")).ToString() %></label>
                                        </div>
                                        <div class="col-xs-10 col-sm-9 col-md-3">
                                            <label><%# DataBinder.Eval(Container.DataItem, "Cap.Name") %></label>
                                        </div>
                                        <div class="col-xs-4 col-sm-3 col-md-2">
                                            <asp:Button runat="server" Text="Edit" ID="btnModifyItem" CssClass="btn btn-info"
                                                        CommandName="editItem"
                                                        CommandArgument=<%# new[] {Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "colourId"))} %>/>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-3">
                                            <asp:Button runat="server" Text="Undo" CssClass="btn btn-info" 
                                                        CommandName="undoItem"
                                                        CommandArgument=<%# new[] {Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "colourId"))} %>/>
                                        </div>
                                        <div class="col-xs-4 col-sm-2 col-md-1">
                                            <asp:Button runat="server" Text="X" CssClass="btn btn-danger" OnClientClick="setTimeout('window.location.reload()', 5000);"
                                                        CommandName="deleteItem"
                                                        CommandArgument=<%# new[] {Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "colourId"))} %>/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-3 col-md-3">
                                            <label>Colour:</label>
                                        </div>
                                        <div class="col-xs-6 col-sm-3 col-md-3">
                                            <asp:DropDownList ID="ddlCapColoursCheckout"
                                                              DataTextField="name"
                                                              DataValueField="id" Enabled="False"
                                                              runat="server"/>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6">

                                        </div>
                                    </div>
                                    <div class="row">
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-4 col-md-5">
                                            <label>Quantity:</label>
                                            <span><input type="number" disabled id="nptQuantity" min="1" max="99" name="nptQuantity" value='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'
                                                   runat="server" onchange="updateTotal();"/></span>
                                        </div>
                                        <div class="col-xs-6 col-sm-3 col-md-3">
                                            <label>X </label>
                                            <span><label><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.Price")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")) %></label></span>
                                            <label id="lblCapPrice" hidden><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.Price")) %></label>
                                        </div>
                                        <div class="col-xs-6 col-sm-3 col-md-3">
                                            <label>=</label>
                                            <span><label id="lblTotalPrice"><%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Quantity"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.Price"))).ToString("C", CultureInfo.CreateSpecificCulture("en-US")) %></label></span>
                                        </div>
                                        <div class="col-xs-0 col-sm-2 col-md-1">

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-0 col-sm-1 col-md-1"></div>

                    </div>
                    <div class="row">
                        <br/>
                        <div class="col-xs-2 col-sm-1 col-md-1"></div>
                        <div class="col-xs-8 col-sm-10 col-md-10" style="border-bottom: darkgrey solid 1px"></div>
                        <div class="col-xs-2 col-sm-1 col-md-1"></div>
                    </div>
                </ItemTemplate>

                <ItemSeparatorTemplate>
                    <br/>
                </ItemSeparatorTemplate>
                
                <EmptyDataTemplate>
                    <div style="text-align: center">
                        Your shopping cart is now empty.
                    </div>
                </EmptyDataTemplate>
            </asp:ListView>

        </div>
    </div>
</div>
<br/>
<div class="container-fluid">
    <div class="row">
        <div class="col-md-6"></div>
        <div class="col-md-5">
            <br/>
            <div class="row">
                <div class="col-xs-0 col-sm-1 col-md-1"></div>
                <div class="col-xs-12 col-sm-6 col-md-6">
                    <label>SubTotal:</label>
                </div>
                <div class="col-xs-2 col-sm-0 col-md-0"></div>
                <div class="col-xs-10 col-sm-4 col-md-4">
                    <span><label id="lblSubTotal" runat="server">$</label></span>
                </div>
                <div class="col-xs-0 col-sm-1 col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-xs-0 col-sm-1 col-md-1"></div>
                <div class="col-xs-12 col-sm-6 col-md-6col-md-6">
                    <label>GST:</label>
                </div>
                <div class="col-xs-2 col-sm-0 col-md-0"></div>
                <div class="col-xs-10 col-sm-4 col-md-6col-md-4">
                    <span><label id="lblSubTotalGst" runat="server">$</label></span>
                </div>
                <div class="col-xs-0 col-sm-1 col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <br/>
                </div>
            </div>
            <div class="row" style="border-top: 1px darkgrey solid">
                <div class="col-md-12">
                    <br/>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-0 col-sm-1 col-md-1"></div>
                <div class="col-xs-12 col-sm-6 col-md-6">
                    <label>Total:</label>
                </div>
                <div class="col-xs-2 col-sm-0 col-md-0"></div>
                <div class="col-xs-10 col-sm-4 col-md-4">
                    <span><label id="lblFullTotal" runat="server">$</label></span>
                </div>
                <div class="col-xs-0 col-sm-1 col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <br/>
                </div>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <br/><br/>
    </div>
    <div class="row">
        <div class="col-xs-2 col-sm-1 col-md-1">

        </div>
        <div class="col-xs-10 col-sm-5 col-md-5">
            <H4>
                <span>
                    <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" OnClick="Cancel_OnClick" runat="server"/>
                </span>
            </H4>
        </div>
        <div class="col-xs-2 col-sm-1 col-md-1">

        </div>
        <div class="col-xs-10 col-sm-5 col-md-5">
            
            <H4>
                <span>
                    <asp:LinkButton ID="btnPlaceOrder" Text="Complete Order" CssClass="btn btn-primary" OnClick="CompleteOrder_OnClick" runat="server"/>
                </span>
            </H4>
        </div>
        <div class="col-xs-0 col-sm-1 col-md-1"></div>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>