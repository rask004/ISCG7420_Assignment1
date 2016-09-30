<%@ Page Title="Quality Caps - Error, General" Language="C#" MasterPageFile="~/Master/Error.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Error_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ErrorTitle" Runat="Server">
    GENERAL ERROR
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MetaPlaceholder">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ErrorBody" Runat="Server">
    <section class="container-fluid">
                <b><i>An Error has occurred. </i></b>
    </section>
    <br/>
    
    <section class="container-fluid">
        <div class="col-xs-12 col-sm-12 col-md-12" style="border-bottom: 2px solid black"/>
    </section>
    
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%-- If no known error, show unknown section. --%>
            <section class="container-fluid" id="UnknownErrorSection" runat="server" style="font-size: 1.1em">
                <div class="col-xs-12 col-sm-12 col-md-12">
                    <br/>
                    <p>The cause of the Error is unknown.</p>
                    <br/>
                </div>
            </section>
            <%-- Else, show details and email possibility. --%>
            <section class="container-fluid" id="KnownErrorSection" runat="server" style="font-size: 1.1em">
                <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12">
                    <div class="col-xs-12 col-sm-2 col-md-3">
                        <label>Error Type:</label>
                    </div>
                    <div class="col-xs-12 col-sm-10 col-md-9">
                        <label id="lblErrorName" runat="server"></label>
                    </div>
                    <div class="col-xs-12 col-sm-2 col-md-3">
                        <label>HResult Code:</label>
                    </div>
                    <div class="col-xs-12 col-sm-10 col-md-9">
                        <label id="lblErrorHResult" runat="server"></label>
                    </div>
                    <div class="col-xs-12 col-sm-2 col-md-3">
                        <label>Message:</label>
                    </div>
                    <div class="col-xs-12 col-sm-10 col-md-9">
                        <label id="lblErrorMessage" runat="server"></label>
                    </div>
                </div>
                </div>

                <div class="row">
                    <span></span>
                </div>
                
                <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <label>Email Report to Admin:</label>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                    <div class="col-xs-12 col-sm-2 col-md-2">
                        <label>Name: </label>
                    </div>
                    <div class="col-xs-12 col-sm-9 col-md-8">
                        <asp:Textbox type="text" ID="txtSenderName" CssClass="form-control input-sm" AutoPostBack="False" runat="server"/>
                    </div>
                    <div class="col-xs-12 col-sm-1 col-md-2">
                        (Optional)
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-xs-12 col-sm-1 col-md-1">
                        <label>Email: </label>
                    </div>
                    <div class="col-xs-12 col-sm-1 col-md-1">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSenderEmail"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="txtSenderEmail" OnServerValidate="ValidateEmailInput" runat="server"></asp:CustomValidator>
                    </div>
                    <div class="col-xs-12 col-sm-10 col-md-10">
                        <asp:Textbox CssClass="form-control input-sm"  type="text" ID="txtSenderEmail" AutoPostBack="False" runat="server"/>
                    </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <label> All available Error information will be sent with this email. </label>
                        </div>
                    </div>
                    <br/>
                    
                    <asp:Button ID="btnSendEmail" CssClass="btn btn-lg btn-info" runat="server" Text="Send Email" OnClick="btnSendEmail_OnClick"/>
                    <br/>
                    <p><asp:Literal runat="server" ID="litEmailResponse"/></p>

                </div>
                </div>

            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <br/><br/><br/>

</asp:Content>

