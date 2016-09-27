﻿<%@ Page Title="Quality Caps - Edit Profile" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="Customer_Profile" %>

<%--  
    The page for the Quality Caps Website.
    
    Change Log:

--%>
<asp:Content ID="Content1" ContentPlaceHolderID="AdditionalScripts" Runat="Server">
    <script type="text/javascript" src="../Content/common.js">
    
    </script>
    <script type="text/javascript" src="../Content/Validation.js">
    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
<div class="container PageSection">
<div class="row">
    <span class="DecoHeader" style="margin-left:12%">
    <div >
        <H3 >Edit Profile</H3>
    </div>
    </span>
</div>
<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>First Name:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtFirstName"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtFirstName"
                             OnServerValidate="FirstLastNameValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtFirstName" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Last Name:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtLastName"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtLastName"
                             OnServerValidate="FirstLastNameValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtLastName" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>

<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Email:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtEmail"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtEmail"
                             OnServerValidate="EmailValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtEmail" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Login:</label>
                </span>
    </div>
    <div class="col-md-1">
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtLogin" Enabled="False" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <asp:Button CssClass="btn btn-danger" ID="btnUserChangePassword" OnClick="btnUserChangePassword_OnClick"
                        CausesValidation="false" Text="Change Password" runat="server"/>
                </span>
    </div>
    <div class="col-md-1">
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtUserPassword"
                             OnServerValidate="PasswordValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                    <asp:TextBox CssClass="form-control input-sm" ID="txtUserPassword" Text=""
                        Enabled="false" runat="server" />
                </span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Home Contact Number:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:CustomValidator runat="server"
                             OnServerValidate="ContactNumberRequired"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtHomeNumber"
                             OnServerValidate="LandlineNumberValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtHomeNumber" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Work Contact Number:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtWorkNumber"
                             OnServerValidate="LandlineNumberValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtWorkNumber" runat="server" />
                </span>
    </div>
    <div class="col-md-2">
    </div>
</div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Mobile Contact Number:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtMobileNumber"
                             OnServerValidate="MobileNumberValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtMobileNumber" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Street Address:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtStreetAddress"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtStreetAddress"
                             OnServerValidate="StreetAddressValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtStreetAddress"  runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>Suburb:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtSuburb"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtSuburb"
                             OnServerValidate="SuburbCityValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtSuburb" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-3">
        <span class="ContentShiftLeft">
                    <label>City:</label>
                </span>
    </div>
    <div class="col-md-1">
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="txtCity"
                                    ErrorMessage="*" ForeColor="red"/>
        <asp:CustomValidator runat="server"
                             ControlToValidate="txtCity"
                             OnServerValidate="SuburbCityValidation"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                    <asp:TextBox CssClass="form-control input-sm" id="txtCity" runat="server" />
                </span>
    </div>
    <div class="col-md-2">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>

<div class="row">
    <div class="col-md-1">

    </div>
    <div class="col-md-10">
        <b>
            <asp:Label Text="" ForeColor="red" ID="lblErrorMessages" runat="server"/>
        </b>
    </div>
    <div class="col-md-1">

    </div>
</div>

<div class="row">
    <span class="BlankRow"></span></div>

<div class="row">
    <div class="container-fluid">

        <div class="row">
            <div class="col-md-1">

            </div>
            <div class="col-md-5">
                <span class="ContentShiftLeft DecoSubHeader">
                            <H5 style="margin-left: 33.3%"><b>
                                <input class="btn btn-primary" ID="btnResetRegistration" type="reset" value="Reset" 
                                    onclick="this.form.reset(); return false;"/>
                            </b></H5>
                        </span>
            </div>
            <div class="col-md-5">
                <span class="ContentShiftRight DecoSubHeader">
                            <H5 style="margin-left: 33.3%"><b>
                                <input class="btn btn-primary" ID="btnSubmitRegistration" name="submitRegistration" 
                                    type="submit" value="Update"  OnServerClick="Update_Click" runat="server"/>
                            </b></H5>
                        </span>
            </div>
            <div class="col-md-1">

            </div>
        </div>
    </div>
</div>
</div>
</asp:Content>