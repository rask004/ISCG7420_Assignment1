<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="DefaultPageTitle" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    Quality Caps Website
</asp:Content>
<asp:Content ID="CategoryListing" ContentPlaceHolderID="PageContentLeft" Runat="Server">
    <div id="CategoriesSection" class="container PageSection">
        <div class="row">
            <div class="col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <H3 style="margin-left: 30%"><asp:Label ID="lblLeftHeader" Text="Categories" runat="server" /></H3>  
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="container-fluid">
                    <asp:DataList ID="dlstCategoriesWithProducts" CellPadding="5" CellSpacing="5" OnItemDataBound="dlstCategoriesWithProducts_OnItemDataBound"
                        RepeatDirection="Vertical" RepeatColumns="1" RepeatLayout="Flow"
                        runat="server">
                        
                        <HeaderTemplate>
                            <div class="row" style="margin-bottom:2%">
                                <div class="col-md-2"></div>
                                <div class="col-md-8"></div>
                                <div class="col-md-2"></div>
                            </div>
                        </HeaderTemplate>
                
                        <ItemTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <span><H4><asp:ImageButton ID="imgCategoryPicture"
                                            AlternateText='Image for <%# DataBinder.Eval(Container.DataItem, "name") %>'
                                            Width="50%"
                                            runat="server"/></H4></span>
                                    </div>
                                    <div class="col-md-6"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8">
                                        <span><H4><asp:Label ID="lblCategoryName" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                            runat="server"/></H4></span>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label Visible="False" ID="lblCategoryId" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                            runat="server"/>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
            
                        <SeparatorTemplate>
                            <div class="row" style="margin-top:2%">
                                <div class="col-md-2"></div>
                                <div class="col-md-8"></div>
                                <div class="col-md-2"></div>
                            </div>
                        </SeparatorTemplate>
                        
                        <FooterTemplate>
                            <div class="row" style="margin-top:2%">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <asp:Label Visible='<%#bool.Parse((dlstCategoriesWithProducts.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No Record Found!"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                        </FooterTemplate>
                
                    </asp:DataList>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ProductsListing" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <div id="ProductListingSection" class="container PageSection">
        <div class="row">
            <div class="col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <H3 style="margin-left: 38%"><asp:Label ID="lblCentreHeader" Text="ProductsName" runat="server" /></H3>  
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="container-fluid">
                    <asp:DataList ID="dlstAvailableProducts" CellPadding="5" CellSpacing="5"
                        RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Flow" 
                        OnItemDataBound="dlstAvailableProducts_OnItemDataBound"
                        runat="server">
                        
                        <HeaderTemplate>
                            <div class="row" style="margin-bottom:2%">
                                <div class="col-md-2"></div>
                                <div class="col-md-8"></div>
                                <div class="col-md-2"></div>
                            </div>
                        </HeaderTemplate>
                
                        <ItemTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <span><H4><asp:ImageButton ID="imgCapPicture"
                                            AlternateText='Image for <%# DataBinder.Eval(Container.DataItem, "name") %>'
                                            ImageUrl='<%# DataBinder.Eval(Container.DataItem, "imageUrl") %>'
                                            Width="50%"
                                            runat="server"/></H4></span>
                                    </div>
                                    <div class="col-md-6"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8">
                                        <span><H4><asp:Label ID="lblCapName" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                            runat="server"/></H4></span>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label Visible="False" ID="lblCapId" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                            runat="server"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8">
                                        <span><H4><asp:Label ID="lblCapPrice" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("C", CultureInfo.CurrentCulture) %>'
                                            runat="server"/></H4></span>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
            
                        <SeparatorTemplate>
                            <div class="row" style="margin-top:2%">
                                <div class="col-md-2"></div>
                                <div class="col-md-8"></div>
                                <div class="col-md-2"></div>
                            </div>
                        </SeparatorTemplate>
                        
                        <FooterTemplate>
                            <div class="row" style="margin-top:2%">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <asp:Label Visible='<%#bool.Parse((dlstAvailableProducts.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No Record Found!"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                        </FooterTemplate>
                
                    </asp:DataList>
                </div>
                
            </div>
        </div>
    </div>
    <div class="row"></div>
    <div class="row"></div>
</asp:Content>
<asp:Content ID="ShoppingBasket" ContentPlaceHolderID="PageContentRight" Runat="Server">
    <div id="ShoppingBasketSection" class="container PageSection">
        <div class="row">
            <div class="col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <H3 style="margin-left: 28%"><asp:Label ID="lblRightHeader" Text="Shopping Cart" runat="server" /></H3>  
                </span>
            </div>
        </div>
    </div>
    <div class="row"></div>
    <div class="row"></div>
    This will show the shopping basket, scrollbar if many items, with subtotal, gst, total, checkout and cancel links.
</asp:Content>

