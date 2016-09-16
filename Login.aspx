<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Customer_Login" %>

<%--  
    The Customer Login page for the Quality Caps Website.
    
    Change Log:

--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <div class="container PageSectionCentre">
        <div class="row">
            <div class="DecoHeader" style="margin-left:12%">
                <H3 style="margin-left:50%">Login</H3>
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
        <div class="row"><span class="BlankRow"></span></div>
        <div class="row">
            <div class="col-md-3">
                        
            </div>
            <div class="col-md-6">
                <div style="text-align: center; display: table-cell; vertical-align: middle">
                    <asp:Login ID="lgnAdminSection" runat="server"></asp:Login>
                    <!-- add asp controls for reset password -->
                </div>
            </div>
            <div class="col-md-3">
                
            </div>
            
        </div>
        <div class="row"><span class="BlankRow"></span></div>
        <div class="row">
            <div class="col-md-3">
                        
            </div>
            <div class="col-md-6">
                <span><label ID="lblLoginMessages" runat="server"></label></span>
            </div>
            <div class="col-md-3">
                
            </div>
            
        </div>
        <div class="row"><span class="BlankRow"></span></div>
    </div>
</asp:Content>

