<%@ Page Title="Quality Caps - Error, General" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Error_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    GENERAL ERROR
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>
        An Error has occurred. 
    </p>
    
    <asp:UpdatePanel UpdateMode="Conditional" ChildrenAsTriggers="True"  runat="server">
        <ContentTemplate>
    
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
                    <p><b><label>Source URL:</label></b> <label id="lblErrorSourceUrl" runat="server"></label></p>
                    <p><b><label>HResult Code:</label></b> <label id="lblErrorHResult" runat="server"></label></p>
                    <br/>
                    <p><b><label id="lblInnerExceptionHeader" hidden runat="server">InnerException:</label></b> 
                        <label id="lblInnerExceptionName" runat="server"></label></p>
                    <br/>
                    <asp:Button Text="Email Report to Admin" ID="btnSendEmail" OnClick="btnSendEmail_OnClick" runat="server"/>
                    <br/>
                    <p><asp:Literal runat="server" ID="litEmailResponse"/></p>
                    <label id="hddnInnerName"  runat="server"></label>
                    <label id="hddnInnerMessage"  runat="server"></label>
                    <label id="hddnInnerHResult"  runat="server"></label>
                    <label id="hddnInnerStackTrace"  runat="server"></label>
                </div>
            </section>
        
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

