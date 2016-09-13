<%@ Page Title="Quality Caps - Customer Profile" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Customer_Details" %>
<%@ Import Namespace="Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <%= Title %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <div class="container-fluid PageSectionCentre" style="border: black solid 1px;">
        <div class="row">
            <div class="DecoHeader" style="margin-left:12%">
                <H3 style="margin-left:39%">Customer Profile</H3>
            </div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftRight">
                        <label>Customer:</label>
                    </span>
                </H5>
            </div>
            <div class="col-md-5">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerName" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftRight">
                        <label>Email:</label>
                    </span>
                </H5>
            </div>
            <div class="col-md-5">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerEmail" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftRight">
                        <label>Home Number:</label>
                    </span>
                </H5>
            </div>
            <div class="col-md-5">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerHomeNumber" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftRight">
                        <label>Work Number:</label>    
                    </span>
                </H5>
            </div>
            <div class="col-md-5">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerWorkNumber" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftRight">
                        <label>Mobile Number:</label>
                    </span>
                </H5>
            </div>
            <div class="col-md-5">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerMobileNumber" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftRight">
                        <label>Address:</label>
                    </span>
                </H5>
            </div>
            <div class="col-md-5">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerStreetAddress" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
            </div>
            <div class="col-md-3">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerSuburb" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            <div class="col-md-2">
                <H5>
                    <span class="ContentShiftLeft">
                        <asp:Label runat="server" ID="lblCustomerCity" Text=""></asp:Label>
                    </span>
                </H5>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <span class="ContentStretchHorizontal DecoHeader"><H4>
                    <a style="margin-left:12%" href="EditProfile.aspx">Update Customer Details</a>
                </H4></span>
            </div>
            <div class="col-md-4"></div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <div class="container" style="border: solid 1px black; padding: 1%;">

                    <asp:GridView Width="100%" ID="grdvCustomerOrders" AllowSorting="False" CellPadding="5" 
                        CellSpacing="5" AutoGenerateDeleteButton="False" AutoGenerateEditButton="False"
                        AutoGenerateSelectButton="False" AutoGenerateColumns="False"
                        AllowPaging="True" runat="server" PageSize="5" 
                        OnPageIndexChanging="grdvCustomerOrders_OnPageIndexChanging">
                        
                        <Columns>
                            <asp:BoundField DataField="OrderId" HeaderText="Order ID" ReadOnly="True" SortExpression="id"
                                />
                            <asp:BoundField DataField="CustomerOrder.Status" HeaderText="Status" ReadOnly="True" SortExpression="status"
                                />
                            <asp:BoundField DataField="TotalQuantity" HeaderText="Total Quantity" ReadOnly="True"
                                />
                            <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" ReadOnly="True"
                                DataFormatString="{0:c}" />

                        </Columns>
                        
                        <EmptyDataTemplate>
                            <H4><span><label>You have no Orders on record.</label></span></H4>
                        </EmptyDataTemplate>
                        
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-1"></div>
        </div>
        
    </div>
</asp:Content>

