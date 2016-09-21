<%@ Page Title="Quality Caps - Error, General" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Error_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    GENERAL ERROR
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>
        An Error has occurred. 
    </p>
    
    <% Exception ex = Server.GetLastError(); %>
    
    <%-- If no previous error, show unknown section. --%>
    <section class="container-fluid" id="UnknownErrorSection" runat="server">
        <div class="col-md-12">
            <br/>
            <p>The cause of the Error is unknown.</p>
            <br/>
        </div>
    </section>
    <%-- Else, show details and email possibility. --%>
    <section class="container-fluid" id="KnownErrorSection" runat="server">
        <div class="col-md-12">
            <br/>
            <p><b><label>Error Type:</label></b> <label id="lblErrorName" runat="server"></label></p>
            <br/>
            <p><b><label>Description:</label></b> <label id="lblErrorDescription" runat="server"></label></p>
            <p><b><label>Source:</label></b> <label id="lblErrorSourceMethod" runat="server"></label></p>
            <p><b><label>HResult Code:</label></b> <label id="lblErrorHResult" runat="server"></label></p>
            <br/>
            <p><b><label>InnerException:</label></b> <label id="lblInnerException" runat="server"></label></p>
            <br/>
            <asp:Button Text="Send Email to Admin" ID="btnEmailAdmin" OnClick="btnEmailAdmin_OnClick" runat="server"/>
        </div>
    </section>
    
    <%-- Else, offer to send email to admin, show error details. --%>
</asp:Content>

