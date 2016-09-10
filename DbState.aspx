<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DbState.aspx.cs" Inherits="DbState" %>

<!DOCTYPE html>

<html>
<head runat="server">
        <title>Quality Caps Website</title>
        <script type="text/javascript" src="~/Content/common.js">
        </script>
        <script type="text/javascript" src="~/Content/Validation.js">
        </script>
    
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--Bootstrap CSS code--%>
    <webopt:BundleReference runat="server" path="~/Content/css" />
</head>
<body>

    <div class="container-fluid">
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptCategories" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- CATEGORIES --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ID
                                </th>
                                <th style="border:1px gray solid">
                                    NAME
                                </th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "id") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "name") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptColours" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- COLOURS --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ID
                                </th>
                                <th style="border:1px gray solid">
                                    NAME
                                </th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "id") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "name") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptSuppliers" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- SUPPLIERS --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ID
                                </th>
                                <th style="border:1px gray solid">
                                    NAME
                                </th>
                                <th style="border:1px gray solid">
                                    CONTACT NUMBER
                                </th>
                                <th style="border:1px gray solid">
                                    EMAIL
                                </th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "id") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "name") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "contactnumber") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "email") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptCaps" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- CAPS --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ID
                                </th>
                                <th style="border:1px gray solid">
                                    NAME
                                </th>
                                <th style="border:1px gray solid">
                                    PRICE ($)
                                </th>
                                <th style="border:1px gray solid">
                                    DESCRIPTION
                                </th>
                                <th style="border:1px gray solid">
                                    SUPPLIER
                                </th>
                                <th style="border:1px gray solid">
                                    CATEGORY
                                </th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "id") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "name") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "price") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "description") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "imageUrl") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "supplierId") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "categoryId") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptCustomers" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- CUSTOMERS --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ID
                                </th>
                                <th style="border:1px gray solid">
                                    FIRSTNAME
                                </th>
                                <th style="border:1px gray solid">
                                    LASTNAME
                                </th>
                                <th style="border:1px gray solid">
                                    LOGIN
                                </th>
                                <th style="border:1px gray solid">
                                    EMAIL
                                </th>
                                <th style="border:1px gray solid">
                                    HOMENUMBER
                                </th>
                                <th style="border:1px gray solid">
                                    WORKNUMBER
                                </th>
                                <th style="border:1px gray solid">
                                    MOBILENUMBER
                                </th>
                                <th style="border:1px gray solid">
                                    STREETADDRESS
                                </th>
                                <th style="border:1px gray solid">
                                    SUBURB
                                </th>
                                <th style="border:1px gray solid">
                                    CITY
                                </th>
                                <th style="border:1px gray solid">
                                    DISABLED
                                </th>
                                
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "id") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "firstname") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "lastname") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "login") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "email") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "homeNumber") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "worknumber") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "mobilenumber") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "streetaddress") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "suburb") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "city") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "isdisabled") %>
                            </td>                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptOrders" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- ORDERS --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ID
                                </th>
                                <th style="border:1px gray solid">
                                    STATUS
                                </th>
                                <th style="border:1px gray solid">
                                    CUSTOMERID
                                </th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "id") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "status") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "userid") %>
                            </td>
                            
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        
        <div class="row" style="border: black 2px solid; margin:2%">
            <div class="col-md-12">
                <asp:Repeater 
                    id="rptOrderItems" 
                    runat="server">
                    <HeaderTemplate>
                        <table class="col-md-12">
                            <tr>
                                <th>
                                    -- ORDERITEM --
                                </th>
                            </tr>
                            <tr>
                                <th style="border:1px gray solid">
                                    ORDERID
                                </th>
                                <th style="border:1px gray solid">
                                    CAPID
                                </th>
                                <th style="border:1px gray solid">
                                    COLOURID
                                </th>
                                <th style="border:1px gray solid">
                                    QUANTITY
                                </th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "orderid") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "capid") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "colourid") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "quantity") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
    </div>
    
</body>
</html>

