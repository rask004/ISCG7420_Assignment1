<%@ Page Title="Quality Caps - Customer Profile" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Customer_Details" %>

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
                <span class="ContentShiftRight">
                    <H4>
                        <label>Customer:</label>
                    </H4>
                </span>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblCustomerName" Text=""></asp:Label>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <span class="ContentShiftRight">
                    <H4>
                        <label>Email:</label>
                    </H4>
                </span>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblCustomerEmail" Text=""></asp:Label>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <span class="ContentShiftRight">
                    <H4>
                        <label>Home Number:</label>
                    </H4>
                </span>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblCustomerHomeNumber" Text=""></asp:Label>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <span class="ContentShiftRight">
                    <H4>
                        <label>Work Number:</label>    
                    </H4>
                </span>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblCustomerWorkNumber" Text=""></asp:Label>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <span class="ContentShiftRight">
                    <H4>
                        <label>Mobile Number:</label>
                    </H4>
                </span>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblCustomerMobileNumber" Text=""></asp:Label>
            </div>
            
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
                <span class="ContentShiftRight">
                    <H4>
                        <label>Address:</label>
                    </H4>
                </span>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblCustomerStreetAddress" Text=""></asp:Label>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-3">
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" ID="lblCustomerSuburb" Text=""></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" ID="lblCustomerCity" Text=""></asp:Label>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <span class="ContentStretchHorizontal"><H4>
                    <a href="EditProfile.aspx">Update Customer Details</a>
                </H4></span>
            </div>
            <div class="col-md-4"></div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <div class="container-fluid" style="border: solid 1px black; padding: 1%;">
                    <asp:GridView ID="grdvOrderItems" AllowSorting="False" CellPadding="0" 
                        CellSpacing="5" AutoGenerateDeleteButton="False" AutoGenerateEditButton="False"
                        AutoGenerateSelectButton="False" runat="server">
                        
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-1"></div>
        </div>
        
    </div>
</asp:Content>

