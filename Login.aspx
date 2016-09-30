<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="General_Login" %>

<%--  
    The Customer Login page for the Quality Caps Website.
    
    Change Log:

--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <div class="container PageSection">
        <div class="row">
            <span class="DecoHeader" style="margin-left:11%">
                <div>
                    <H3>Login</H3>
                </div>
            </span>
        </div>
        <div class="row">
            <span class="BlankRow"></span></div>
        <div class="row">
            <span class="BlankRow"></span></div>
        <div class="row">
            <div class="col-md-3">

            </div>
            <div class="col-md-6">
                <div style="display: table-cell; text-align: center; vertical-align: middle">
                    <asp:Login ID="lgnTestingSection" runat="server"
                               OnAuthenticate="lgnTestingSection_OnAuthenticate" OnLoggingIn="lgnTestingSection_OnLoggingIn">
                    </asp:Login>
                    <!-- add asp controls for reset password -->
                </div>
            </div>
            <div class="col-md-3">

            </div>

        </div>
        <div class="row">
            <span class="BlankRow"></span></div>
        <div class="row">
            <div class="col-md-3">

            </div>
            <div class="col-md-6">
                <span><label ID="lblLoginMessages" runat="server"></label></span>
            </div>
            <div class="col-md-3">

            </div>

        </div>
        <div class="row">
            <span class="BlankRow"></span></div>
    </div>
</asp:Content>