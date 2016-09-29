<%@ Page Title="Quality Caps - Customer Profile" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Customer_Details" %>

<%--  
    The page for the Quality Caps Website.
    
    Change Log:
    
    27-9-16     13:00       AskewR04    Completed page.
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="AdditionalScripts" Runat="Server">
    <script type="text/javascript" src="../Content/common.js">
    
    </script>
    <script type="text/javascript" src="../Content/Validation.js">
    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
<div class="container PageSection" style="border: black solid 1px;">
<div class="row">
    <span class="DecoHeader" style="margin-left:11%">
        <div>
            <H3>Customer Profile</H3>
        </div>
    </span>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-10 col-md-8">
        <label ID="lblMessage" runat="server" style="background-color: #DDDDDD; font-size: 1.5em"></label>
    </div>
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <br/>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-5 col-md-3">
        <H5>
                        <label for="lblCustomerName">Customer:</label>
        </H5>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5">
        <H5>
            <label runat="server" ID="lblCustomerName" Text=""></label>
        </H5>
    </div>

    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-5 col-md-3">
        <H5>
                        <label for="lblCustomerEmail">Email:</label>
        </H5>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5">
        <H5>
                        <label runat="server" ID="lblCustomerEmail" Text=""></label>
        </H5>
    </div>

    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-5 col-md-3">
        <H5>
                        <label for="lblCustomerHomeNumber">Home Number:</label>
        </H5>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5">
        <H5>
                        <label runat="server" ID="lblCustomerHomeNumber" Text=""></label>
        </H5>
    </div>

    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-5 col-md-3">
        <H5>
                        <label for="lblCustomerWorkNumber">Work Number:</label>    
        </H5>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5">
        <H5>
            <span class="ContentShiftLeft">
                        <label runat="server" ID="lblCustomerWorkNumber" Text=""></label>
                    </span>
        </H5>
    </div>

    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-5 col-md-3">
        <H5>
                        <label for="lblCustomerMobileNumber">Mobile Number:</label>
        </H5>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5">
        <H5>
                        <label runat="server" ID="lblCustomerMobileNumber" Text=""></label>
        </H5>
    </div>

    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-12 col-sm-5 col-md-3">
        <H5>
                        <label for="lblCustomerStreetAddress">Address:</label>
        </H5>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5">
        <H5>
                        <label runat="server" ID="lblCustomerStreetAddress" Text=""></label>
        </H5>
    </div>
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
    <div class="col-xs-0 col-sm-3 col-md-3">
    </div>
    <div class="col-xs-12 col-sm-2 col-md-3">
        <H5>
                        <label runat="server" ID="lblCustomerSuburb" Text=""></label>
        </H5>
    </div>
    <div class="col-xs-12 col-sm-3 col-md-2">
        <H5>
                        <label runat="server" ID="lblCustomerCity" Text=""></label>
        </H5>
    </div>
    <div class="col-xs-0 col-sm-1 col-md-2"></div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-xs-2 col-sm-2 col-md-2"></div>
    <div class="col-xs-8 col-sm-8 col-md-8">
        <span class="DecoSubHeader" style="margin-left:25%">
                <div><H4>
                    <a href="EditProfile.aspx">Update Customer Details</a>
                </H4></div>
</span>
    </div>
    <div class="col-xs-2 col-sm-2 col-md-2"></div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-1"></div>
    <div class="col-xs-12 col-sm-10 col-md-10">

            <asp:GridView Width="100%" ID="gvCustomerOrders" AllowSorting="False" CellPadding="5"
                          CellSpacing="5" AutoGenerateDeleteButton="False" AutoGenerateEditButton="False"
                          AutoGenerateSelectButton="False" AutoGenerateColumns="False"
                          AllowPaging="True" runat="server" PageSize="5"
                          OnPageIndexChanging="grdvCustomerOrders_OnPageIndexChanging">

                <Columns>
                    <asp:BoundField DataField="OrderId" HeaderText="ID" ReadOnly="True"/>
                    <asp:BoundField DataField="CustomerOrder.DatePlaced" DataFormatString="{0:d}"
                                    HeaderText="Date" ReadOnly="True" NullDisplayText=""/>
                    <asp:BoundField DataField="CustomerOrder.Status" HeaderText="Status" ReadOnly="True"/>
                    <asp:BoundField DataField="TotalQuantity" HeaderText="Qty" ReadOnly="True"/>
                    <asp:BoundField DataField="TotalPrice" HeaderText="Cost" ReadOnly="True"
                                    DataFormatString="{0:c}"/>

                </Columns>

                <EmptyDataTemplate>
                    <H4>
                        <span><label>You have no Orders on record.</label></span></H4>
                </EmptyDataTemplate>

            </asp:GridView>
    </div>
    <div class="col-xs-0 col-sm-1 col-md-1"></div>
</div>

</div>
</asp:Content>