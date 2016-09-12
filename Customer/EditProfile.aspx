﻿<%@ Page Title="Quality Caps - Edit Profile" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="Customer_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <%= Title %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <div class="container PageSectionCentre">
        <div class="row">
            <div class="DecoHeader" style="margin-left:12%">
                <H3 style="margin-left:40%">Edit Profile</H3>
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                    ErrorMessage="*" ForeColor="red"
                    />
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtFirstName"
                            OnServerValidate="FirstLastNameValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtFirstName" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                    ErrorMessage="*" ForeColor="red"
                    />
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtLastName"
                            OnServerValidate="FirstLastNameValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtLastName" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        
        <div class="row"><span class="BlankRow"></span></div>
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
                    ErrorMessage="*" ForeColor="red"
                    />
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtEmail"
                            OnServerValidate="EmailValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtEmail" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>

        <div class="row"><span class="BlankRow"></span></div>
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
                    <asp:TextBox id="txtLogin" Enabled="False" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                            OnServerValidate="ContactNumberRequired"
                            />        
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtHomeNumber"
                            OnServerValidate="LandlineNumberValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtHomeNumber" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                            OnServerValidate="LandlineNumberValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtWorkNumber" runat="server" />
                </span>
            </div>
            <div class="col-md-2">      
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                            OnServerValidate="MobileNumberValidation"
                            />        
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtMobileNumber" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        
        
        <div class="row"><span class="BlankRow"></span></div>
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
                    ErrorMessage="*" ForeColor="red"
                    />
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtStreetAddress"
                            OnServerValidate="StreetAddressValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtStreetAddress"  runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                    ErrorMessage="*" ForeColor="red"
                    />
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtSuburb"
                            OnServerValidate="SuburbCityValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtSuburb" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>
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
                    ErrorMessage="*" ForeColor="red"
                    />
                <asp:CustomValidator runat="server"
                            ControlToValidate="txtCity"
                            OnServerValidate="SuburbCityValidation"
                            />
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight ">
                    <asp:TextBox id="txtCity" runat="server" />
                </span>
            </div>
            <div class="col-md-2">
                        
            </div>
        </div>
        
        <div class="row"><span class="BlankRow"></span></div>
        <div class="row">
            <div class="col-md-1">
                        
            </div>
            <div class="col-md-10">
                <b><asp:Label Text="" ForeColor="red" ID="lblErrorMessages" runat="server"/></b>
            </div>
            <div class="col-md-1">
                        
            </div>
        </div>
        <div class="row"><span class="BlankRow"></span></div>

        <div class="row">
            <div class="container-fluid">
            
                <div class="row">
                    <div class="col-md-1">
                        
                    </div>
                    <div class="col-md-5">
                        <span class="ContentShiftLeft DecoSubHeader">
                            <H5 style="margin-left: 33.3%"><b>
                                <input ID="btnResetRegistration" type="reset" value="Reset" 
                                    onclick="this.form.reset(); return false;"/>
                            </b></H5>
                        </span>
                    </div>
                    <div class="col-md-5">
                        <span class="ContentShiftRight DecoSubHeader">
                            <H5 style="margin-left: 33.3%"><b>
                                <input ID="btnSubmitRegistration" name="submitRegistration" 
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
