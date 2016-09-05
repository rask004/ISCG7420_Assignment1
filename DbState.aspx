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

    </div>
    
</body>

