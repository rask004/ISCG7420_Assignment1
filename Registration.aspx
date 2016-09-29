<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<%--  
    The Registration page for the Quality Caps Website.
    
    Change Log:

--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
<div class="container PageSection">
<div class="row">
    <span class="DecoHeader" style="margin-left: 11%;">
        <div>
            <H3>Registration</H3>
        </div>
    </span>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>First Name:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtFirstName"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtFirstName"
                             OnServerValidate="FirstLastNameValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtFirstName" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Last Name:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtLastName"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtLastName"
                             OnServerValidate="FirstLastNameValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtLastName" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Email:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtEmail"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtEmail"
                             OnServerValidate="EmailValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtEmail" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Login:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtLogin"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtLogin"
                             OnServerValidate="LoginValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtLogin" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Password:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtPassword"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtPassword"
                             OnServerValidate="PasswordValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtPassword" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>


<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Home Contact Number:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:CustomValidator runat="server"
                             OnServerValidate="ContactNumberRequired"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtHomeNumber"
                             OnServerValidate="LandlineNumberValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtHomeNumber" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Work Contact Number:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtWorkNumber"
                             OnServerValidate="LandlineNumberValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtWorkNumber" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">
    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Mobile Contact Number:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtMobileNumber"
                             OnServerValidate="MobileNumberValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtMobileNumber" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>


<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Street Address:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtStreetAddress"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtStreetAddress"
                             OnServerValidate="StreetAddressValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtStreetAddress"  runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>Suburb:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtSuburb"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtSuburb"
                             OnServerValidate="SuburbCityValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtSuburb" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-0 col-sm-1 col-md-2">

    </div>
    <div class="col-xs-12 col-sm-4 col-md-3">
        <span>
                    <label>City:</label>
                </span>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtCity"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtCity"
                             OnServerValidate="SuburbCityValidation"/>
    </div>
    <div class="col-xs-10 col-sm-5 col-md-4">
                    <asp:TextBox CssClass="form-control input-sm" id="txtCity" runat="server" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-xs-2 col-sm-1 col-md-1">

    </div>
    <div class="col-xs-8 col-sm-10 col-md-10">
        <b>
            <asp:Label Text="" ForeColor="red" ID="lblErrorMessages" runat="server"/>
        </b>
    </div>
    <div class="col-xs-2 col-sm-1 col-md-1">

    </div>
</div>
<div class="row">
    <span class="BlankRow"></span></div>

<div class="row">
    <div class="container-fluid">

        <div class="row">
            <div class="col-xs-4 col-sm-1 col-md-2">

            </div>
            <div class="col-xs-8 col-sm-3 col-md-3">
                <span>
                    <div>
                        <H5>
                            <input ID="btnResetRegistration" type="reset" value="Reset"  class="btn btn-primary"
                                onclick="this.form.reset(); return false;"/>
                        </H5>
                    </div>
                </span>
            </div>
            <div class="col-xs-4 col-sm-5 col-md-3"></div>
            <div class="col-xs-8 col-sm-2 col-md-3">
                <span>
                    <div>
                        <H5>
                            <input ID="btnSubmitRegistration" name="submitRegistration" class="btn btn-primary"
                                type="submit" value="Register"  OnServerClick="Register_Click" runat="server"/>
                        </H5>
                    </div>
                </span>
            </div>
            <div class="col-xs-0 col-sm-1 col-md-2">

            </div>
        </div>
    </div>
</div>
</div>
</asp:Content>