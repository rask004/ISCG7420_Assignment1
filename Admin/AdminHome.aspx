<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Home Page</title>
    <script type="text/javascript" src="~/Content/common.js" >
    </script>
    <script type="text/javascript" src="~/Content/Validation.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div class="container-fluid PageSection" style="max-width: 66%; ">
        <div style="text-align: center; display: table-cell; vertical-align: middle">
                <asp:Login ID="lgnAdminSection" runat="server"></asp:Login>
                <!-- add asp controls for reset password -->
        </div>
    </div>
</asp:Content>

