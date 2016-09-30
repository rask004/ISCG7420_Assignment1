<%@ Page Title="Quality Caps - Error, Database" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="ErrorDatabaseConnection.aspx.cs" Inherits="Error_ErrorDatabaseConnection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    ERROR: Cannot connect to the Database
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MetaPlaceholder">
    <meta http-equiv="refresh" content="30;url=/"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>There was no response from the Database System.</p>
    <p>Retrying in 30 seconds...</p>
    <br/>
    <br/>
    <p>If this Error keeps happening, the Database may be down for maintenance at this time.</p>
    <br/>
    <br/>
    <%-- Send Email to Admin --%>
    <p><label id="lblResponse"></label></p>
</asp:Content>

