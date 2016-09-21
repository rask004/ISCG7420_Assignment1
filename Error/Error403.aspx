<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Error403.aspx.cs" Inherits="Error_Error403" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MetaPlaceholder" Runat="Server">
    <meta http-equiv="refresh" content="5;url=/"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    UNAUTHORIZED PAGE
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>You do not have the authority to view this page.</p>
    <br/>
    <p>Redirecting to Home Page...</p>
    <br/>
</asp:Content>

