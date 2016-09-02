<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AdminSuppliers.aspx.cs" Inherits="AdminSupplier" %>

<%--  
    The Admin page for the Supplier Entity.
    
    Change Log:
        18-8-16  14:30       AskewR04        Created page and layout.

--%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Suppliers</title>
    <script type="text/javascript" src="~/JS/common.js" >
    </script>
    <script type="text/javascript" src="~/JS/Validation.js">
    </script>
</asp:Content>

<asp:Content ID="AdminSupplierSideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="SupplierListingSection" class="AdminSection" 
        style="position: fixed; overflow-y: scroll; overflow-x: hidden; width: 22%; max-height:86%">
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
                                            ID = "btnSideBarItem"
                                            CssClass = "col-md-12 SidebarButton" 
                                            CausesValidation="false"
                                            Text ='<%# DataBinder.Eval(Container.DataItem, "name") %>'
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

<asp:Content ID="AdminSupplierMain" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div id="SupplierEditingForm" class="container AdminSection" runat="server">
        <div class="row">
            <span class="BlankRow"></span>
        </div>

        <asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <span class="ContentShiftRight">
                            <asp:Button ID="btnAddSupplier" CausesValidation="false" OnClick="AddButton_Click" 
                                CssClass="MainButton" Text="Add New Supplier..." runat="server"/>
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
                            <b><asp:Label CssClass="MainItemHeader" ID="lblSupplierIdHeader" runat="server" AssociatedControlID="lblSupplierId" 
                                                Text="ID:" /></b>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblSupplierId" Text="Supplier_Id" runat="server" /></b>
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
                            <asp:Label ID="lblSupplierNameHeader" runat="server" AssociatedControlID="txtSupplierName" 
                                                CssClass="MainItemHeader" Text="Supplier Name:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><asp:TextBox ID="txtSupplierName" Enabled="false" runat="server" /></span>
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
                            <asp:RequiredFieldValidator ID="valRequiredSupplierName" runat="server" 
                            ControlToValidate="txtSupplierName"
                            ErrorMessage="The Supplier Name is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valInvalidSupplierName"
                            ControlToValidate="txtSupplierName"
                            ErrorMessage="The Supplier Name must only have alphabetic characters, spaces, commas, periods or apostrophes."
                            ClientValidationFunction="ValidateNameString"
                            OnServerValidate="SupplierNameValidation"
                            Display="Static"
                            ForeColor="Red"
                            />
                        </b></span>
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
                            <asp:Label ID="lblSupplierNumberHeader" runat="server" AssociatedControlID="txtSupplierContactNumber" 
                                                CssClass="MainItemHeader" Text="Contact Number:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><asp:TextBox ID="txtSupplierContactNumber" Enabled="false" runat="server" /></span>
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
                            <asp:RequiredFieldValidator ID="valRequiredSupplierContactNumber" runat="server" 
                            ControlToValidate="txtSupplierContactNumber"
                            ErrorMessage="The Supplier Contact Number is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valNumberSupplierContact"
                            ControlToValidate="txtSupplierContactNumber"
                            ErrorMessage="The Supplier Number must only have numbers. It must be in the form 0N[N]NNN[N]NNN, e.g. 09555444, 0755557777."
                            ClientValidationFunction="ValidateLandlineNumber"
                            OnServerValidate="SupplierNumberValidation"
                            Display="Static"
                            ForeColor="Red"
                            />
                        </b></span>
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
                            <asp:Label ID="lblSupplierEmailHeader" runat="server" AssociatedControlID="txtSupplierEmail" 
                                                CssClass="MainItemHeader" Text="Email:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><asp:TextBox ID="txtSupplierEmail" Enabled="false" runat="server" /></span>
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
                            <asp:RequiredFieldValidator ID="valRequiredSupplierEmail" runat="server" 
                            ControlToValidate="txtSupplierEmail"
                            ErrorMessage="The Supplier Email is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valCharsAndFormatSupplierEmail"
                            ControlToValidate="txtSupplierEmail"
                            ErrorMessage="The Supplier Email must match the format <chars@[subdomain.]site.domain>."
                            ClientValidationFunction="ValidateEmail"
                            OnServerValidate="SupplierEmailValidation"
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
                    <div class="col-md-12" >
                        <asp:Label id="lblMessageJumboTron" CssClass="jumbotron" style="float: right; margin: 4px;" runat="server" />
                    </div>
                </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveChanges" />
                <asp:AsyncPostBackTrigger ControlID="btnAddSupplier"/>
                <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>