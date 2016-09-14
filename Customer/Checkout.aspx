<%@ Page Title="Quality Caps - Checkout" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Customer_Checkout" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <%= Title %>
    <script type="text/javascript">
        function updateTotal() {
            $(document).ready(function() {
                var count = $("#nptQuantity").Value;
                var price = $("#lblCapPrice").Value;
                var cost = count * price;
                $("#lblTotalPrice").Value = "$" + cost.toFixed(2).toString();
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <div class="container-fluid PageSectionCentre" style="border: black solid 1px;">
        <div class="row">
            <div class="DecoHeader" style="margin-left:12%">
                <H3 style="margin-left:39%">Checkout</H3>
            </div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        
        <asp:UpdatePanel ID="ShoppingCartPanel" UpdateMode="Conditional" ChildrenAsTriggers="True" runat="server">
            <ContentTemplate>
                <div class="container-fluid" style="border: 1px solid black; padding:0.5%">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:ListView ID="lstvCheckoutItems"
                                OnItemDataBound="lstvCheckoutItems_OnItemDataBound"
                                OnPagePropertiesChanging="lstvCheckoutItems_OnPagePropertiesChanging"
                                 runat="server">
                                
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder">
                                        
                                    </asp:PlaceHolder>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-4">
                                            <asp:DataPager ID="dpgItemPager" runat="server" PagedControlID="lstvCheckoutItems" PageSize="3">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                        ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                        <div class="col-md-4"></div>
                                    </div>
                                </LayoutTemplate>
                        
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Image Width="40%" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Cap.ImageUrl") %>' runat="server"/>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label># <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")).ToString() %></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label><%# (DataBinder.Eval(Container.DataItem, "Cap.Name")) %></label>
                                                </div>
                                                <div class="col-md-3">
                                                    Button
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label>Colour:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlCapColoursCheckout"
                                                        DataTextField="name"
                                                        DataValueField ="id"
                                                        runat="server"/>
                                                </div>
                                                <div class="col-md-6">
                                                
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label>Quantity:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input type="number" id="nptQuantity" min="1" max="99" name="nptQuantity" value='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'
                                                        runat="server" onchange="updateTotal();" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label>X </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.Price")).ToString("C", CultureInfo.CurrentCulture) %></label>
                                                    <label id="lblCapPrice" hidden ><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.Price")) %></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <label>=</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <label id="lblTotalPrice"><%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Quantity")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.Price"))).ToString("C", CultureInfo.CurrentCulture) %></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                </ItemTemplate>
                            
                                <ItemSeparatorTemplate>
                                    <br/>
                                </ItemSeparatorTemplate>
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
                                    <div class="col-md-1"></div>
                                    <div class="col-md-5">
                                        <label>SubTotal:</label>
                                    </div>
                                    <div class="col-md-1"><label>$</label></div>
                                    <div class="col-md-4">
                                        <label id="lblSubTotal"></label>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-5">
                                        <label>GST:</label>
                                    </div>
                                    <div class="col-md-1"><label>$</label></div>
                                    <div class="col-md-4">
                                        <label id="lblSubTotalGst"></label>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12"><br/></div>
                                </div>
                                <div class="row" style="border-top: 1px black solid">
                                    <div class="col-md-12"><br/></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-5">
                                        <label>Total:</label>
                                    </div>
                                    <div class="col-md-1"><label>$</label></div>
                                    <div class="col-md-4">
                                        <label id="lblFullTotal"></label>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12"><br/></div>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                    <div class="row">
                        <br/>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                        
                        </div>
                        <div class="col-md-3">
                            <H4><span class="ContentShiftRight">
                                <asp:LinkButton Text="Cancel" OnClick="Cancel_OnClick" runat="server"/>
                            </span></H4>
                        </div>
                        <div class="col-md-4">
                        
                        </div>
                        <div class="col-md-3">
                            <H4><span class="ContentShiftRight">

                            </span></H4>
                        </div>
                        <div class="col-md-1">
                        
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>



