<%@ Page Title="Quality Caps - Error, General" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Error_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    GENERAL ERROR
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MetaPlaceholder">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <p>
        An Error has occurred. 
    </p>
    
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%-- If no known error, show unknown section. --%>
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
                    <p><b><label>HResult Code:</label></b> <label id="lblErrorHResult" runat="server"></label></p>
                    <br/>
                    <br/>
                    <b><label>Email Report to Admin</label></b>
                    <br/>
                    <br/>
                    <label>Name: </label><asp:Textbox type="text" ID="txtSenderName" AutoPostBack="False" runat="server"/> (Optional)
                    <br/>
                    <label>Email: </label>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtSenderEmail"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="txtSenderEmail" OnServerValidate="OnServerValidate" runat="server"></asp:CustomValidator>
                    <asp:Textbox  type="text" ID="txtSenderEmail" AutoPostBack="False" runat="server"/> (Optional)
                    <br/>
                    <br/>
                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" OnClick="btnSendEmail_OnClick"/>
                    <br/>
                    <p><asp:Literal runat="server" ID="litEmailResponse"/></p>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
        

</asp:Content>

