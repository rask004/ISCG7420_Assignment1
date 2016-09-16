<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<%--  
    The Administrator Login page for the Quality Caps Website.
    
    Change Log:

--%>

<asp:Content ID="Content3" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div class="container-fluid PageSection" >
        <div style="text-align: center; display: table-cell; vertical-align: middle">
                <asp:Login ID="lgnAdminSection" runat="server"></asp:Login>
                <!-- add asp controls for reset password -->
        </div>
    </div>
</asp:Content>

