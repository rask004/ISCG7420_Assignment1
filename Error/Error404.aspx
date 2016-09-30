<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Error404.aspx.cs" Inherits="Error_Error404" %>

<asp:Content runat="server" ContentPlaceHolderID="MetaPlaceholder">
    <meta http-equiv="refresh" content="5;url=/"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    PAGE NOT FOUND
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>A Page you requested does not exist.</p>
    <br/>
    <p>Redirecting to Home Page...</p>
    <br/>
</asp:Content>

