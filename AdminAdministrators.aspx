<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AdminAdministrators.aspx.cs" Inherits="AdminUsers" %>
<%@ Import Namespace="asp_Assignment" %>

<%--  
    The Admin page for the SiteUsers Entity. - Admin users only
    
    Change Log:
    
    25-8-16     00:35       AskewR04        Created Admin Page for Customers.
        

--%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Users</title>
    <script type="text/javascript" src="JS/common.js" >
    </script>
    <script type="text/javascript" src="JS/Validation.js">
    </script>
    
</asp:Content>

<asp:Content ID="AdminUsersSideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="UsersListingSection" class="AdminSection" 
        style="position: fixed; overflow-y: scroll; overflow-x: hidden; width: 22%; max-height:86%">
        <%-- to be filled with items from the currently used DB Table --%>
        <div class="row">
            <div id="divLeftSidebar" class="col-md-12">
                <span class="DecoHeader" style="margin-left: 11%;">
                    <H3 style="margin-left: 20%"><asp:Label ID="lblSideBarHeader" Text="SideBarName" runat="server" /></H3>  
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <span class="BlankRow"></span>
            </div>
        </div>
    
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
                                            ID = "btnSideBarItem"
                                            CssClass = "col-md-12 SidebarButton" 
                                            CausesValidation="false"
                                            Text ='<%# DataBinder.Eval(Container.DataItem, "login") %>'
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

<asp:Content ID="AdministratorsMain" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div id="UsersEditingForm" class="container AdminSection" 
        style="position: fixed; overflow-y: auto; overflow-x: hidden; width: 55%; max-height:86%">
        <div class="row">
            <span class="BlankRow"></span>
        </div>

        <asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <span class="ContentShiftRight">
                            <asp:Button ID="btnAddAdmin" CausesValidation="false" OnClick="AddButton_Click" 
                                CssClass="MainButton" Text="Add New Admin..." runat="server"/>
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
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
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                    <div class="col-md-4">
                        
                    </div>
                    <div class="col-md-4">
                        
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
                        
                    </div>
                    <div class="col-md-4">
                        
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
                        <span class="ContentShiftRight"><asp:TextBox ID="txtUserEmail" Enabled="false" 
                            runat="server" /></span>
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
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-8">
                        <b>
                            <asp:RequiredFieldValidator ID="valRequiredEmail" runat="server" 
                            ControlToValidate="txtUserEmail"
                            ErrorMessage="The User Email is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valCharsEmail"
                            ControlToValidate="txtUserEmail"
                            ErrorMessage="The User must have a valid Email."
                            ClientValidationFunction="ValidateEmail"
                            OnServerValidate="UserEmailValidation"
                            Display="Static"
                            ForeColor="Red"
                            />
                            <asp:RequiredFieldValidator ID="valRequiredLogin" runat="server" 
                            ControlToValidate="txtUserLogin"
                            ErrorMessage="The User Login is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valCharsLogin"
                            ControlToValidate="txtUserLogin"
                            ErrorMessage="The User Login must only have alphanumeric characters."
                            ClientValidationFunction="ValidateAlphanumeric"
                            OnServerValidate="UserLoginValidation"
                            Display="Static"
                            ForeColor="Red"
                            />
                            <asp:CustomValidator runat="server"  ID="valIfRequiredPassword"
                            ControlToValidate="txtUserPassword"
                            ErrorMessage="The Admin password must be at least 10 chars."
                            OnServerValidate="UserPassValidation"
                            Display="Static"
                            ForeColor="Red"
                            />
                        </b>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftLeft">
                            <asp:Button ID="btnCancelChanges" OnClick="CancelButton_Click" CausesValidation="false" 
                                CssClass="MainButton" Text="Cancel" Enabled="false" runat="server"/>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight">
                            <asp:Button ID="btnSaveChanges" 
                                OnClick="SaveButton_Click" 
                                CssClass="MainButton" Text="Save Changes" Enabled="false" runat="server"/>
                        </span>
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
                    <div class="col-md-12" >
                        <asp:Label id="lblMessageJumboTron" CssClass="jumbotron" style="float: right; margin: 4px;" runat="server" />
                    </div>
                </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveChanges" />
                <asp:AsyncPostBackTrigger ControlID="btnAddAdmin"/>
                <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

