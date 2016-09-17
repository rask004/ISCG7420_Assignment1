<%@ Page Title="Quality Caps - Error" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="ErrorDatabaseConnection.aspx.cs" Inherits="Error_ErrorDatabaseConnection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    ERROR: Cannot connect to the Database
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>The Database Server did not respond in time to a request.</p>
    <p>If this Error keeps happening, the Database Server may be down for maintenance or inactive.</p>
    <br/>
    <br/>
    <%-- Send Email to Admin --%>
    <p><label id="lblResponse"></label></p>
</asp:Content>

