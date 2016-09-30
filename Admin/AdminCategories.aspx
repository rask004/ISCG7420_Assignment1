<%@ Page Title="Admin - Categories" Language="C#" MasterPageFile="~/Master/Admin.master" AutoEventWireup="true" CodeFile="AdminCategories.aspx.cs" Inherits="AdminCategories" %>

<%--  
    The Admin page for the Category Entity.
    
    Change Log:
        10-8-16  15:01       AskewR04        Created page and layout.
        11-8-16  19:00       AskewR04        Updated page to meet changes in master page, and improved with Data controls.
        

--%>

<asp:Content ID="AdminCategorySideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="CategoryListingSection" class="AdminSection"
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
                                <tr class="col-md-12">
                                    <td class="col-md-12 SidebarItem">
                                        <asp:Button
                                            ID="btnSideBarItem"
                                            CssClass="col-md-12 SidebarButton"
                                            CausesValidation="false"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
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
    <div id="CategoryEditingForm" class="container AdminSection" runat="server">
        <div class="row">
            <span class="BlankRow"></span>
        </div>

        <!-- Editing subform for individual items -->
        <asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <span class="ContentShiftRight">
                            <asp:Button ID="btnAddCategory" CausesValidation="false" OnClick="AddButton_Click" 
                                CssClass="MainButton" Text="Add New Category..." runat="server"/>
                        </span>
                    </div>
                </div>
                <div class="row">
                    <span class="BlankRow"></span>
                </div>
                <div class="row">
                    <div class="col-md-2">

                    </div>
                    <div class="col-md-4">
                        <span>
                            <b><asp:Label CssClass="MainItemHeader" ID="lblCategoryIdHeader" runat="server" AssociatedControlID="lblCategoryId" 
                                                Text="ID:" /></b>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblCategoryId" Text="Category_Id" runat="server" /></b>
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
                            <asp:Label ID="lblCategoryNameHeader" runat="server" AssociatedControlID="txtCategoryName" 
                                                CssClass="MainItemHeader" Text="Category Name:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><asp:TextBox ID="txtCategoryName" Enabled="false" runat="server" /></span>
                    </div>
                    <div class="col-md-2">
                        <span class="BlankRow"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftLeft"><b>
                            <asp:RequiredFieldValidator ID="valRequiredCategoryName" runat="server" 
                            ControlToValidate="txtCategoryName"
                            ErrorMessage="The Category Name is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valCharsCategoryName"
                            ControlToValidate="txtCategoryName"
                            ErrorMessage="The Category Name must only have alphabetic characters, spaces, commas, periods or apostrophes."
                            ClientValidationFunction="ValidateNameString"
                            OnServerValidate="CategoryNameValidation"
                            Display="Static"
                            ForeColor="Red"
                            />
                        </b></span>
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
                    <div class="col-md-6">
                        <span class="ContentShiftLeft">
                            <asp:Button ID="btnCancelChanges" OnClick="CancelButton_Click" CausesValidation="false" 
                                CssClass="MainButton" Text="Cancel" Enabled="false" runat="server"/>
                        </span>
                    </div>
                    <div class="col-md-6">
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
                <asp:AsyncPostBackTrigger ControlID="btnAddCategory"/>
                <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>