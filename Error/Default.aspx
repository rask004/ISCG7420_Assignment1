<%@ Page Title="Quality Caps - Error, General" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Error_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    GENERAL ERROR
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>
        An Error has occurred. 
    </p>
    
    <%-- If no previous error, state Error is unknown. redirect to home page. --%>
    
    <%-- Else, offer to send email to admin, show error details. --%>
</asp:Content>

