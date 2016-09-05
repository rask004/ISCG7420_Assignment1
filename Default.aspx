<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
        <asp:DataList ID="dlstCategoriesWithProducts" runat="server">
            <HeaderTemplate>
                <table>
                    
            </HeaderTemplate>
                
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                runat="server"/>
                    </td>
                </tr> 

            </ItemTemplate>
            
            <SeparatorTemplate>
                <tr>
                    <td></td>
                </tr>
            </SeparatorTemplate>
            
            <FooterTemplate>
                </table>
            </FooterTemplate>
                
        </asp:DataList>
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
    </div>
    <div class="row"></div>
    <div class="row"></div>
    This will show the current products in the selected category, paginated
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

