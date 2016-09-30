<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.master" AutoEventWireup="true" CodeFile="AdminCustomerUsers.aspx.cs" Inherits="AdminUsers" %>

<%--  
    The Admin page for the SiteUsers Entity - Customers.
    
    Change Log:
    
    19-8-16     22:35       AskewR04        Created Admin Page for Customers.
        

--%>

<asp:Content ID="AdminUsersSideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="UsersListingSection" class="AdminSection"
         style="max-height: 86%; overflow-x: hidden; overflow-y: scroll; position: fixed; width: 22%;">
        <%-- to be filled with items from the currently used DB Table --%>
        <div class="row">
            <div id="divLeftSidebar" class="col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <H3 style="margin-left: 25%"><asp:Label ID="lblSideBarHeader" Text="SideBarName" runat="server" /></H3>  
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <span class="BlankRow"></span>
            </div>
        </div>

        <!-- Sidebar for listing items -->
        <asp:UpdatePanel ID="ItemSideBarPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Repeater
                            id="dbrptSideBarItems"
                            OnItemCommand="dbrptSideBarItems_ItemCommand"
                            runat="server">
                            <HeaderTemplate>
                                <table class="col-md-12">
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr class="container-fluid">
                                    <td class="col-md-12 SidebarItem">
                                        <asp:Button
                                            ID="btnSideBarItem"
                                            CssClass="col-md-12 SidebarButton"
                                            CausesValidation="false"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "login") %>'
                                            CommandName="loadItem"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                            runat="server"/>
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </table>
                            </FooterTemplate>

                        </asp:Repeater>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

<asp:Content ID="AdminCategoryMain" ContentPlaceHolderID="AdminContentMain" Runat="Server">
<div id="UsersEditingForm" class="container AdminSection"
     style="max-height: 86%; overflow-x: hidden; overflow-y: auto; position: fixed; width: 55%;">
<div class="row">
    <span class="BlankRow"></span>
</div>

<!-- Editing subform for individual items -->
<asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
<ContentTemplate>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
                        
    </div>
    <div class="col-md-4">
        <span>
                            <b><asp:Label CssClass="MainItemHeader" ID="lblUsersIdHeader" runat="server" 
                                AssociatedControlID="lblUsersId" 
                                                Text="ID:" /></b>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblUsersId" Text="Placeholder_Id" runat="server" /></b>
                        </span>
    </div>
    <div class="col-md-2">

    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
                        
    </div>
    <div class="col-md-4">
        <span>
                            <b><asp:Label CssClass="MainItemHeader" ID="lblUsersDisabledStatusHeader" runat="server" 
                                AssociatedControlID="lblUserIsDisabled" 
                                                Text="Disabled:" /></b>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblUserIsDisabled" Text="?" runat="server" /></b>
                        </span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserFirstNameHeader" runat="server" AssociatedControlID="txtUserFirstName" 
                                                CssClass="MainItemHeader" Text="First Name:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserFirstName" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserLastNameHeader" runat="server" AssociatedControlID="txtUserLastName" 
                                                CssClass="MainItemHeader" Text="Last Name:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserLastName" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserEmailHeader" runat="server" AssociatedControlID="txtUserEmail" 
                                                CssClass="MainItemHeader" Text="Email:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserEmail" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserLoginHeader" runat="server" AssociatedControlID="txtUserLogin" 
                                                CssClass="MainItemHeader" Text="Login:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserLogin" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftLeft">
                            <asp:Button ID="btnUserRegeneratePassword" OnClick="ButtonPassword_Click"
                                CausesValidation="false" Text="Change Password" Enabled="false" runat="server"/>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserPassword" Text=""
                            Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserHomeNumberHeader" runat="server" AssociatedControlID="txtUserHomeNumber" 
                                                CssClass="MainItemHeader" Text="Home Number:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserHomeNumber" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserWorkNumberHeader" runat="server" AssociatedControlID="txtUserWorkNumber" 
                                                CssClass="MainItemHeader" Text="Work Number:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtUserWorkNumber" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserMobileNumberHeader" runat="server" AssociatedControlID="txtUserMobileNumber" 
                                                CssClass="MainItemHeader" Text="Mobile Number:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <asp:TextBox ID="txtUserMobileNumber" Enabled="false" runat="server" />
                        </span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserStreetAddressHeader" runat="server" AssociatedControlID="txtUserStreetAddress" 
                                                CssClass="MainItemHeader" Text="Street Address:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <asp:TextBox ID="txtUserStreetAddress" Enabled="false" runat="server" />
                        </span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserSuburbHeader" runat="server" AssociatedControlID="txtUserSuburb" 
                                                CssClass="MainItemHeader" Text="Suburb:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <asp:TextBox ID="txtUserSuburb" Enabled="false" runat="server" />
                        </span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-4">
        <span>
                            <asp:Label ID="lblUserCityHeader" runat="server" AssociatedControlID="txtUserCity" 
                                                CssClass="MainItemHeader" Text="City:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <asp:TextBox ID="txtUserCity" Enabled="false" runat="server" />
                        </span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
    </div>
    <div class="col-md-8">
        <b>
            <asp:RequiredFieldValidator ID="valRequiredFirstName" runat="server"
                                        ControlToValidate="txtUserFirstName"
                                        ErrorMessage="The User First Name is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsFirstName"
                                 ControlToValidate="txtUserFirstName"
                                 ErrorMessage="The User First Name must only have alphabetic characters."
                                 ClientValidationFunction="ValidateNameString"
                                 OnServerValidate="UserNameValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator ID="valRequiredLastName" runat="server"
                                        ControlToValidate="txtUserLastName"
                                        ErrorMessage="The User Last Name is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsLastName"
                                 ControlToValidate="txtUserLastName"
                                 ErrorMessage="The User Last Name must only have alphabetic characters."
                                 ClientValidationFunction="ValidateNameString"
                                 OnServerValidate="UserNameValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator ID="valRequiredEmail" runat="server"
                                        ControlToValidate="txtUserEmail"
                                        ErrorMessage="The User Email is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsEmail"
                                 ControlToValidate="txtUserEmail"
                                 ErrorMessage="The User must have a valid Email."
                                 ClientValidationFunction="ValidateEmail"
                                 OnServerValidate="UserEmailValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator ID="valRequiredLogin" runat="server"
                                        ControlToValidate="txtUserLogin"
                                        ErrorMessage="The User Login is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsLogin"
                                 ControlToValidate="txtUserLogin"
                                 ErrorMessage="The User Login must only have alphanumeric characters."
                                 ClientValidationFunction="ValidateAlphanumeric"
                                 OnServerValidate="UserLoginValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valIfRequiredPassword"
                                 ControlToValidate="txtUserPassword"
                                 ErrorMessage="The User must have a password with length of at least 10."
                                 OnServerValidate="UserPassValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valRequiredNumber"
                                 ErrorMessage="The User must have at least one contact number."
                                 OnServerValidate="UserNumberRequiredValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valHomeNumberCorrect"
                                 ControlToValidate="txtUserHomeNumber"
                                 ErrorMessage="The User Home Number must be in the format 0N[N]NNN[N]NNN, e.g. 095554444, 075555333, 0455551111."
                                 ClientValidationFunction="ValidateLandlineNumber"
                                 OnServerValidate="UserLandlineNumberValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valWorkNumberCorrect"
                                 ControlToValidate="txtUserWorkNumber"
                                 ErrorMessage="The User Work Number must be in the format 0N[N]NNN[N]NNN, e.g. 095554444, 075555333, 0455551111."
                                 ClientValidationFunction="ValidateLandlineNumber"
                                 OnServerValidate="UserLandlineNumberValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valMobileNumberCorrect"
                                 ControlToValidate="txtUserMobileNumber"
                                 ErrorMessage="The User Mobile Number must be in the format +NNNN[N]NNN[N]NNN or 02N[N]NNN[N]NNN, e.g. +61385554444, 02755553333."
                                 ClientValidationFunction="ValidateMobileNumber"
                                 OnServerValidate="UserMobileNumberValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator runat="server" ID="valRequiredStreetAddress"
                                        ErrorMessage="The User must have a street address."
                                        ControlToValidate="txtUserStreetAddress"
                                        ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valStreetAddressCorrect"
                                 ControlToValidate="txtUserStreetAddress"
                                 ErrorMessage="The Street Address must be in the format <numbers>[number or letter] <letters>[ <letters>...], e.g. 34a Happy Valley Rd, 123 Charles Cres."
                                 ClientValidationFunction="ValidateStreetAddress"
                                 OnServerValidate="UserStreetAddressValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator runat="server" ID="valRequiredSuburb"
                                        ControlToValidate="txtUserSuburb"
                                        ErrorMessage="The User must have a suburb."
                                        ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valSuburbCorrect"
                                 ControlToValidate="txtUserSuburb"
                                 ErrorMessage="The Suburb must only have letters or spaces, apostrophes, commas or periods"
                                 ClientValidationFunction="ValidateNameString"
                                 OnServerValidate="GeneralNameValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator runat="server" ID="valRequiredCity"
                                        ControlToValidate="txtUserCity"
                                        ErrorMessage="The User must have a city."
                                        ForeColor="Red"/>
            <asp:CustomValidator runat="server" ID="valCityCorrect"
                                 ControlToValidate="txtUserCity"
                                 ErrorMessage="The Suburb must only have letters or spaces, apostrophes, commas or periods."
                                 ClientValidationFunction="ValidateNameString"
                                 OnServerValidate="GeneralNameValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
        </b>
    </div>
    <div class="col-md-2">
    </div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-md-4">
        <span class="ContentShiftLeft">
                            <asp:Button ID="btnCancelChanges" OnClick="CancelButton_Click" CausesValidation="false" 
                                CssClass="MainButton" Text="Cancel" Enabled="false" runat="server"/>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftLeft">
                            <asp:Button ID="btnDisableCustomer" OnClick="DisableButton_Click" CausesValidation="false" 
                                CssClass="MainButton" Text="Disable Account" Enabled="false" runat="server"/>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <asp:Button ID="btnSaveChanges" 
                                OnClick="SaveButton_Click" 
                                CssClass="MainButton" Text="Save Changes" Enabled="false" runat="server"/>
                        </span>
    </div>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <span class="BlankRow"></span>
</div>
<div class="row">
    <div class="col-md-12">
        <asp:Label id="lblMessageJumboTron" CssClass="jumbotron" style="float: right; margin: 4px;" runat="server"/>
    </div>
</div>

</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSaveChanges"/>
    <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
</Triggers>
</asp:UpdatePanel>
</div>
</asp:Content>