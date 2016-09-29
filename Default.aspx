<%@ Page Title="Quality Caps LTD" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="System.Globalization" %>

<%--  
    The Main page for the Quality Caps Website.
    Page used for customer shopping.
    
    Change Log:
    
    5-9-16      19:00   AskewR04    Created Page and Layout
    8-9-16      20:00   AskewR04    Updated page with data controls.
    14-9-16     20:00   AskewR04    Updated page, interaction with mock session, showing categories and products, product details.
    15-9-16     20:00   AskewR04    Updated page, functional Shopping Cart, pagination (except for Products Datalist.)
    27-9-16     20:00   AskewR04    Updated page, css

--%>

<asp:Content ID="CategoryListing" ContentPlaceHolderID="PageContentLeft" Runat="Server">
    <div id="CategoriesSection" class="container PageSection">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <div>
                        <H3><asp:Label ID="lblLeftHeader" Text="Categories" runat="server" /></H3>  
                    </div>
                </span>
            </div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="CategorySideBarPanel" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="container-fluid">
                            <asp:ListView ID="lvCategoriesWithProducts" OnItemDataBound="LstvCategoriesWithProductsOnItemDataBound"
                                          OnItemCommand="LstvCategoriesWithProducts_OnItemCommand"
                                          runat="server">

                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder">

                                    </asp:PlaceHolder>
                                    <br/>
                                    <div class="row">
                                        <div class="col-xs-2 col-sm-3 col-md-4"></div>
                                        <div class="col-xs-10 col-sm-6 col-md-4">
                                            <asp:DataPager ID="dpgItemPager" runat="server" PagedControlID="lvCategoriesWithProducts" PageSize="3">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                ShowNextPageButton="false"/>
                                                    <asp:NumericPagerField ButtonType="Link"/>
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false"/>
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                        <div class="col-xs-0 col-sm-3 col-md-4"></div>
                                    </div>
                                </LayoutTemplate>

                                <ItemTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-xs-1 col-sm-1 col-md-2"></div>
                                            <div class="col-xs-10 col-sm-7 col-md-4">
                                                <span><H4><asp:ImageButton ID="ibtnCategoryPicture"
                                                    AlternateText='Image for <%# DataBinder.Eval(Container.DataItem, "name") %>'
                                                    Width="70%"
                                                    CommandName="loadCapsByCategory" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                    runat="server"/></H4></span>
                                            </div>
                                            <div class="col-xs-1 col-sm-4 col-md-6"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-1 col-sm-1 col-md-2"></div>
                                            <div class="col-xs-11 col-sm-11 col-md-10">
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

                                <ItemSeparatorTemplate>
                                    <br/>
                                </ItemSeparatorTemplate>

                                <EmptyDataTemplate>
                                    <div class="row">
                                        <div class="col-xs-0 col-sm-1 col-md-2"></div>
                                        <div class="col-xs-12 col-sm-11 col-md-10">
                                            <span class="ContentShiftLeft">
                                                    <H4>There are no categories with available products.</H4>
                                                </span>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>

                            </asp:ListView>
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
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <span class="DecoHeader" style="margin-left: 11%;">
                            <div>
                                <H3><asp:Label ID="lblCentreHeader" Text="Caps" runat="server" /></H3>  
                            </div>
                        </span>
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="container-fluid">
                            <div id="divAvailableProducts" class="container-fluid" runat="server">
                                <asp:DataList ID="dlstAvailableProducts" Visible="True" RepeatDirection="Vertical"
                                              RepeatColumns="2" RepeatLayout="Table" CellSpacing="5" CellPadding="5"
                                              runat="server" OnItemDataBound="dlstAvailableProducts_OnItemDataBound"
                                              GridLines="Both" FooterStyle="border:1px solid black" Width="100%"
                                              OnItemCommand="dlstAvailableProducts_OnItemCommand">

                                    <ItemTemplate>
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-xs-0 col-sm-1 col-md-1"></div>
                                                <div class="col-xs-12 col-sm-5 col-md-4">
                                                    <span><H4><asp:ImageButton ID="ibtnCapPicture"
                                                        AlternateText='Image for <%# DataBinder.Eval(Container.DataItem, "name") %>'
                                                        ImageUrl='<%# DataBinder.Eval(Container.DataItem, "imageUrl") %>'
                                                        CommandName="loadCapDetails" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                        Width="100%"
                                                        runat="server"/></H4></span>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-7">
                                                    <div class="row">
                                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                                            <span><H4><asp:Label ID="lblCapName" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                                                runat="server"/></H4></span>
                                                        </div>
                                                        <div class="col-xs-0 col-sm-0 col-md-0">
                                                            <asp:Label Visible="False" ID="lblCapId"
                                                                       Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                                       runat="server"/>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-2 col-sm-0 col-md-0"></div>
                                                        <div class="col-xs-8 col-sm-12 col-md-12">
                                                            <span><H4><asp:Label ID="lblCapPrice" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")) %>'
                                                                runat="server"/></H4></span>
                                                        </div>
                                                        <div class="col-xs-2 col-sm-0 col-md-0">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>

                                <div class="row">
                                    <div class="col-xs-0 col-sm-3 col-md-3"></div>
                                    <div class="col-xs-3 col-sm-2 col-md-2">
                                        <asp:LinkButton runat="server" ID="btnPreviousProductPage" OnClick="btnChangeProductPage_OnClick"
                                                        Text="Previous"/>
                                    </div>
                                    <div class="col-xs-6 col-sm-2 col-md-2">
                                        <asp:Label runat="server" ID="lblCurrentProductPage"/>
                                    </div>
                                    <div class="col-xs-3 col-sm-2 col-md-2">
                                        <span class="ContentShiftRight">
                                        <asp:LinkButton runat="server" ID="btnNextProductPage" OnClick="btnChangeProductPage_OnClick"
                                            Text="Next"/>
                                    </span></div>
                                    <div class="col-xs-0 col-sm-3 col-md-3"></div>
                                </div>
                            </div>

                            <asp:Table ID="tblSingleItemDetail" CssClass="table-responsive" CellPadding="5" CellSpacing="5" Visible="False"
                                       runat="server">
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <label>ID:</label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-xs-0 col-sm-1 col-md-1">

                                        </div>
                                        <div class="col-xs-12 col-sm-11 col-md-11">
                                            <asp:Label ID="lblCurrentCapId" runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <asp:Label ID="lblCurrentCapName" runat="server"/>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-xs-0 col-sm-1 col-md-1">

                                        </div>
                                        <div class="col-xs-12 col-sm-11 col-md-11">
                                            <asp:Label ID="lblCurrentCapPrice" runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <label>Colour:</label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-xs-0 col-sm-1 col-md-1">

                                        </div>
                                        <div class="col-xs-12 col-sm-11 col-md-11">
                                            <asp:DropDownList ID="ddlCapColours" Enabled="True"
                                                              DataTextField="name"
                                                              DataValueField="id"
                                                              runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <label>Quantity:</label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-xs-0 col-sm-1 col-md-1">

                                        </div>
                                        <div class="col-xs-12 col-sm-11 col-md-11">
                                            <input type="number" id="nptQuantity" min="1" max="99" name="nptQuantity" value="1"
                                                   runat="server"/>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <img style="max-height: 100%; max-width: 100%" ID="imgCurrentCapPicture" runat="server"/>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="col-xs-0 col-sm-1 col-md-1">

                                        </div>
                                        <div class="col-xs-12 col-sm-1 col-md-11">
                                            <div ID="divCurrentCapDescription" style="text-align: justify;" runat="server"></div>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <p></p>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell Width="50%">
                                        <asp:Button ID="btnCancel" Text="Cancel" CssClass="caption btn btn-primary" OnClick="btnCancel_OnClick" runat="server"/>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnAddCapToBasket" Text="Add To Basket" CssClass="caption btn btn-primary" OnClick="btnAddCapToBasket_OnClick" Enabled="True" runat="server"/>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="ShoppingBasket" ContentPlaceHolderID="PageContentRight" Runat="Server">
<asp:UpdatePanel ID="ShoppingCartPanel" UpdateMode="Always" runat="server">
<ContentTemplate>
<div id="ShoppingBasketSection" class="container PageSection">
<div class="row">
    <div class="col-xs-12 col-sm-12 col-md-12">
        <span class="DecoHeader" style="margin-left: 11%;">
                            <div>
                                <H3><asp:Label ID="lblRightHeader" Text="Shopping Cart" runat="server" /></H3> 
                            </div> 
                        </span>
    </div>
</div>
<div class="row"></div>
<div class="row">
    <div class="col-xs-12 col-sm-12 col-md-12">
        <div class="container" style="border: grey 1px solid; margin: 4%;">
            <asp:ListView ID="lvShoppingItems" Visible="True"
                          OnPagePropertiesChanging="lstvShoppingItems_OnPagePropertiesChanging"
                          OnItemCommand="lstvShoppingItems_OnItemCommand"
                          runat="server">

                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder">

                    </asp:PlaceHolder>
                    <br/>
                    <div class="row">
                        <div class="col-xs-2 col-sm-4 col-md-4"></div>
                        <div class="col-xs-8 col-sm-4 col-md-4">
                            <asp:DataPager ID="dpgItemPager" runat="server" PagedControlID="lvShoppingItems" PageSize="4">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                ShowNextPageButton="false"/>
                                    <asp:NumericPagerField ButtonType="Link"/>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false"/>
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div class="col-xs-2 col-sm-4 col-md-4"></div>
                    </div>
                </LayoutTemplate>

                <ItemTemplate>
                    <br/>
                    <div class="row">
                        <div class="col-xs-0 col-sm-1 col-md-1"></div>
                        <div class="col-xs-12 col-sm-3 col-md-3">
                            <label>ID: <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")).ToString() %></label>
                        </div>
                        <div class="col-xs-8 col-sm-6 col-md-5">
                            <label><%# DataBinder.Eval(Container.DataItem, "Cap.name") %></label>
                        </div>
                        <div class="col-xs-4 col-sm-2 col-md-2">
                            <asp:Button runat="server" ID="btnDeleteCartItem" Text="X" ForeColor="Red"
                                        CommandName="deleteCartItem"
                                        CommandArgument='<%# new[] {Convert.ToInt32(DataBinder.Eval(Container.DataItem, "capId")), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "colourId"))} %>'/>
                        </div>
                        <div class="col-sm-0 col-md-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-0 col-sm-2 col-md-1"></div>
                        <div class="col-xs-12 col-sm-3 col-md-3">
                            <label><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cap.price")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")) %></label>
                        </div>
                        <div class="col-xs-8 col-sm-4 col-md-4">
                            <label><%# DataBinder.Eval(Container.DataItem, "Colour.name") %></label>
                        </div>
                        <div class="col-xs-4 col-sm-2 col-md-2">
                            <label>X <%# DataBinder.Eval(Container.DataItem, "quantity") %></label>
                        </div>
                        <div class="col-xs-0 col-sm-1 col-md-2"></div>
                    </div>
                </ItemTemplate>

                <EmptyDataTemplate>
                    <div class="row">
                        <div class="col-xs-1 col-sm-3 col-md-2"></div>
                        <div class="col-xs-11 col-sm-9 col-md-10">
                            <span class="ContentShiftLeft">
                                                    <H4>Your Shopping Cart is empty.</H4>
                                                </span>
                        </div>
                    </div>
                </EmptyDataTemplate>

            </asp:ListView>
        </div>
        <div class="row">
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
            <div class="col-xs-6 col-sm-7 col-md-6">
                <H4>
                    <label>SubTotal</label>
                </H4>
            </div>
            <div class="col-xs-6 col-sm-3 col-md-4">
                <H4><label>$</label>
                    <label ID="lblSubtotal" Text="0.00" runat="server"/>
                </H4>
            </div>
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
        </div>
        <div class="row">
            <div class="col-md-12"></div>
        </div>
        <div class="row">
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
            <div class="col-xs-6 col-sm-7 col-md-6">
                <H4>
                    <label>GST</label>
                </H4>
            </div>
            <div class="col-xs-6 col-sm-3 col-md-4">
                <H4><label>$</label>
                    <label ID="lblGst" Text="0.00" runat="server"/>
                </H4>
            </div>
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
        </div>
        <div class="row">
            <div class="col-md-12"></div>
        </div>
        <div class="row">
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
            <div class="col-xs-6 col-sm-7 col-md-6">
                <H4>
                    <label>TOTAL</label>
                </H4>
            </div>
            <div class="col-xs-6 col-sm-3 col-md-4">
                <H4><label>$</label>
                    <label ID="lblTotalCost" Text="0.00" runat="server"/>
                </H4>
            </div>
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
        </div>
        <div class="row">
            <div class="col-md-12"></div>
        </div>
        <div class="row">
            <div class="col-md-12"></div>
        </div>
        <div class="row" style="border-top: black 2px solid; margin: 4%;">
            <div class="col-xs-0 col-sm-1 col-md-1"></div>
            <div class="col-xs-12 col-sm-7 col-md-5">
                <H4>
                    <asp:Button ID="btnCartClear" runat="server" Text="Clear"
                                CssClass="caption btn btn-primary" OnClick="btnCartClear_OnClick"/>
                </H4>
            </div>
            <div class="col-xs-12 col-sm-3 col-md-5" style="text-align: right">
                <H4>
                    <asp:LoginView id="lgnviewCart" runat="server">
                        <AnonymousTemplate>
                            <i>
                                <span style="color: black">Login to complete order.</span></i>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <asp:Button CssClass="caption btn btn-primary" ID="btnProceedToCheckout" runat="server" Text="Checkout"
                                        OnClick="btnProceedToCheckout_OnClick" Enabled="True"/>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </H4>
            </div>
            <div class="col-xs-0 col-xs-2 col-sm-1 col-md-1"></div>
        </div>
    </div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>