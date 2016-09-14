<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
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
            <asp:UpdatePanel ID="CategorySideBarPanel" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="container-fluid">
                            <asp:DataList ID="dlstCategoriesWithProducts" CellPadding="5" CellSpacing="5" OnItemDataBound="dlstCategoriesWithProducts_OnItemDataBound"
                                RepeatDirection="Vertical" RepeatColumns="1" RepeatLayout="Flow"
                                OnItemCommand="dlstCategoriesWithProducts_OnItemCommand"
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
                                                    CommandName="loadCapsByCategory" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                    runat="server"/></H4></span>
                                            </div>
                                            <div class="col-md-6"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-8">
                                                <span><H4><asp:Label ID="lblCategoryName" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                                    runat="server"/>
                                                    <asp:Label ID="lblCategoryId" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                    Visible="False"
                                                    runat="server"/>
                                                      </H4></span>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ProductsListing" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <asp:UpdatePanel ID="AvailableProductsPanel" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="ProductListingSection" class="container PageSection">
                <div class="row">
                    <div class="col-md-12">
                        <span class="DecoHeader" style="margin-left: 11%;">
                            <H3 style="margin-left: 38%"><asp:Label ID="lblCentreHeader" Text="Caps" runat="server" /></H3>  
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="container-fluid">
                            <asp:ListView ID="lstvAvailableProducts" Visible="True"
                                runat="server" OnItemDataBound="lstvAvailableProducts_OnItemDataBound"
                                OnItemCommand="lstvAvailableProducts_OnItemCommand">
                
                                <ItemTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-4">
                                                <span><H4><asp:ImageButton ID="imgCapPicture"
                                                    AlternateText='Image for <%# DataBinder.Eval(Container.DataItem, "name") %>'
                                                    ImageUrl='<%# DataBinder.Eval(Container.DataItem, "imageUrl") %>'
                                                    CommandName="loadCapDetails" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                    Width="44%"
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
                        
                                <EmptyDataTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-4">
                                        
                                            </div>
                                            <div class="col-md-8">
                                                <H4><b>There are no Caps to display.</b></H4>
                                            </div>
                                </EmptyDataTemplate>
                
                            </asp:ListView>
                            
                            <asp:Table ID="tblSingleItemDetail" CellPadding="5" CellSpacing="5" Visible="False"
                                runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <label>ID:</label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-md-1">
                                            
                                        </div>
                                        <div class="col-md-11">
                                            <asp:Label ID="lblCurrentCapId" runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCurrentCapName" runat="server"/>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-md-1">
                                            
                                        </div>
                                        <div class="col-md-11">
                                            <asp:Label ID="lblCurrentCapPrice" runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <label>Colour:</label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-md-1">
                                            
                                        </div>
                                        <div class="col-md-11">
                                            <asp:DropDownList ID="ddlCapColours" Enabled="True" 
                                                DataTextField="name"
                                                DataValueField ="id"
                                                runat="server"
                                                />
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <label>Quantity:</label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-md-1">
                                            
                                        </div>
                                        <div class="col-md-11">
                                            <input type="number" id="nptQuantity" min="1" max="99" name="nptQuantity" value="1"
                                                runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Image ID="imgCurrentCapPicture" runat="server"/>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-md-1">
                                            
                                        </div>
                                        <div class="col-md-11">
                                            <asp:Label ID="lblCurrentCapDescription" runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <p></p>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_OnClick" runat="server"/>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnAddCapToBasket" Text="Add To Basket" OnClick="btnAddCapToBasket_OnClick" Enabled="True"  runat="server"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                        
                            </asp:Table>
                        </div>
                
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row"></div>
    <div class="row"></div>
</asp:Content>
<asp:Content ID="ShoppingBasket" ContentPlaceHolderID="PageContentRight" Runat="Server">
    <asp:UpdatePanel ID="ShoppingCartPanel" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="ShoppingBasketSection" class="container PageSection">
                <div class="row">
                    <div class="col-md-12">
                        <span class="DecoHeader" style="margin-left: 11%;">
                            <H3 style="margin-left: 28%"><asp:Label ID="lblRightHeader" Text="Shopping Cart" runat="server" /></H3>  
                        </span>
                    </div>
                </div>
                <div class="row"></div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="container" style="margin: 4%; border: grey 1px solid;">
                            <asp:ListView ID="lstvShoppingItems" Visible="True"
                                runat="server">
                        
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-md-4"><label>ID:</label></div>
                                        <div class="col-md-7 ContentShiftLeft">
                                            <asp:Label runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")).ToString() %>'/>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"><label>Name:</label></div>
                                        <div class="col-md-7">
                                            <asp:Label runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Cap.name") %>'/>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"><label>Colour:</label></div>
                                        <div class="col-md-7">
                                            <asp:Label runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Colour.name") %>'/>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"><label>Price:</label></div>
                                        <div class="col-md-7">
                                            <asp:Label runat="server" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.price")).ToString("C", CultureInfo.CurrentCulture) %>'/>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"><label>Quantity:</label></div>
                                        <div class="col-md-7">
                                            <asp:Label runat="server"
                                                    Text= '<%# DataBinder.Eval(Container.DataItem, "quantity") %>'/>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"></div>
                                    </div>
                                </ItemTemplate>
                        
                                <EmptyDataTemplate>
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-10">
                                                <span class="ContentShiftLeft">
                                                    <H4>Your Shopping Cart is empty.</H4>
                                                </span>
                                            </div>
                                        </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5"><H4>
                                <label class="ContentShiftLeft">SubTotal</label>
                            </H4></div>
                            <div class="col-md-5"><H4>
                                $ <asp:Label CssClass="ContentShiftRight" ID="lblSubtotal" Text="0.00" runat="server"/>
                            </H4></div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5"><H4>
                                <label class="ContentShiftLeft">GST</label>
                            </H4></div>
                            <div class="col-md-5"><H4>
                                $ <asp:Label CssClass="ContentShiftRight" ID="lblGst" Text="0.00" runat="server"/>
                            </H4></div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5"><H4>
                                <label class="ContentShiftLeft">TOTAL</label>
                            </H4></div>
                            <div class="col-md-5"><H4>
                                $ <asp:Label CssClass="ContentShiftRight" ID="lblTotalCost" Text="0.00" runat="server"/>
                            </H4></div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-5">
                        
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row" style="border-top: black 2px solid; margin: 4%;">
                            <div class="col-md-1"></div>
                            <div class="col-md-5"><H4>
                                <asp:Button ID="btnCartClear" CssClass="ContentShiftLeft" runat="server" Text="Clear"
                                    OnClick="btnCartClear_OnClick" />
                            </H4></div>
                            <div class="col-md-5"><H4>
                                <asp:Button ID="btnProceedToCheckout" CssClass="ContentShiftRight" runat="server" Text="Checkout"
                                    OnClick="btnProceedToCheckout_OnClick" Enabled="False"/>
                            </H4></div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

