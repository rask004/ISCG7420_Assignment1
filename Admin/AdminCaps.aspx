<%@ Page Title="Admin - Caps" Language="C#" MasterPageFile="~/Master/Admin.master" AutoEventWireup="true" CodeFile="AdminCaps.aspx.cs" Inherits="AdminCaps" %>

<%--  
    The Admin page for the Product Entity.
    
    Change Log:
        22-8-16  12:30       AskewR04        Created page and layout.

--%>

<asp:Content ID="AdminProductSideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="ProductListingSection" class="AdminSection"
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

<asp:Content ID="AdminCapMain" ContentPlaceHolderID="AdminContentMain" Runat="Server">
<div id="CapEditingForm" class="container AdminSection" runat="server"
     style="max-height: 86%; overflow-x: hidden; overflow-y: scroll; position: fixed; width: 66%;">
<div class="row">
    <span class="BlankRow"></span>
</div>

<!-- Editing subform for individual items -->
<asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
<ContentTemplate>
<div class="row">
    <div class="col-md-12">
        <span class="ContentShiftRight">
                            <asp:Button ID="btnAddCap" CausesValidation="false" OnClick="AddButton_Click" 
                                CssClass="MainButton" Text="Add New Cap..." runat="server"/>
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
                            <b><asp:Label CssClass="MainItemHeader" ID="lblCapIdHeader" runat="server" AssociatedControlID="lblCapId" 
                                                Text="ID:" /></b>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblCapId" Text="Placeholder_Id" runat="server" /></b>
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

    </div>
    <div class="col-md-4">
        <span>
                            <b><asp:Label CssClass="MainItemHeader" ID="lblCapCategoryHeader" runat="server" 
                                Text="Category:"  /></b>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                            <b><asp:DropDownList id="ddlCapCategories" 
                                DataTextField="name" DataValueField="id" Enabled="False" runat="server" /></b>
                        </span>
    </div>
    <div class="col-md-2">

    </div>
</div>
<div class="row">
    <div class="col-md-2">

    </div>
    <div class="col-md-4">
        <span>
                            <b><asp:Label CssClass="MainItemHeader" ID="lblCapSupplierHeader" runat="server" 
                                Text="Supplier:"  /></b>
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight ">
                            <b><asp:DropDownList id="ddlCapSuppliers" 
                                DataTextField="name" DataValueField="id" Enabled="False" runat="server" /></b>
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
        <span>
                            <asp:Label ID="lblCapNameHeader" runat="server" AssociatedControlID="txtCapName" 
                                                CssClass="MainItemHeader" Text="Cap Name:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight"><asp:TextBox ID="txtCapName" Enabled="false" runat="server" /></span>
    </div>
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="BlankRow"></span>
    </div>
    <div class="col-md-3">
        <span>
                            <asp:Label ID="lblCapPriceHeader" runat="server" AssociatedControlID="txtCapPrice" 
                                                CssClass="MainItemHeader" Text="Cap Price:" />
                        </span>
    </div>
    <div class="col-md-2">
    </div>
    <div class="col-md-3">
        <span class="ContentShiftRight"><b>$ </b><asp:TextBox ID="txtCapPrice" Enabled="false" runat="server" /></span>
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
                            <asp:Label ID="lblCapDescriptionHeader" runat="server" AssociatedControlID="txtCapDescription" 
                                                CssClass="MainItemHeader" Text="Description:" />
                        </span>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <asp:TextBox ID="txtCapDescription" Rows="3" Enabled="false" runat="server" />
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
                            
                        </span>
    </div>
    <div class="col-md-4">

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
    </div>
    <div class="col-md-10">
        <asp:Label runat="server" Text="Cap Image:" AssociatedControlID="ddlImgCapList"
                   CssClass="MainItemHeader">
        </asp:Label>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <span class="ContentShiftLeft">

                        </span>
    </div>
    <div class="col-md-4">
        <asp:DropDownList ID="ddlImgCapList" Enabled="False" AutoPostBack="True"
                          OnSelectedIndexChanged="ddlImgCapList_ChangeSelection" runat="server"/>
    </div>
    <div class="col-md-4">
        <span class="ContentShiftRight">
                            <img style="max-height: 100%; max-width: 100%;" ID="imgCapImagePreview" 
                            src="~\Images\Cap_NoImage.png" runat="server" />
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
    </div>
    <div class="col-md-8">
        <b>
            <asp:RequiredFieldValidator ID="valRequiredProductName" runat="server"
                                        ControlToValidate="txtCapName"
                                        ErrorMessage="The Product Name is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsProductName"
                                 ControlToValidate="txtCapName"
                                 ErrorMessage="The Cap Name must only have alphanumeric characters, spaces, commas, periods or apostrophes."
                                 ClientValidationFunction="ValidateNameString"
                                 OnServerValidate="CapTextValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator ID="valRequiredProductPrice" runat="server"
                                        ControlToValidate="txtCapPrice"
                                        ErrorMessage="The Product Price is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsProductPrice"
                                 ControlToValidate="txtCapPrice"
                                 ErrorMessage="The Cap Price should have a valid decimal format, e.g. 123.45, 678, 9.0."
                                 ClientValidationFunction="ValidateDecimalString"
                                 OnServerValidate="CapMoneyValidation"
                                 Display="Static"
                                 ForeColor="Red"/>
            <asp:RequiredFieldValidator ID="valRequiredProductDescription" runat="server"
                                        ControlToValidate="txtCapDescription"
                                        ErrorMessage="The Product Description is a required field." ForeColor="red"/>
            <asp:CustomValidator runat="server" ID="valCharsProductDescription"
                                 ControlToValidate="txtCapDescription"
                                 ErrorMessage="The Cap Description must only have alphanumeric characters, spaces, commas, periods or apostrophes."
                                 ClientValidationFunction="ValidateNameString"
                                 OnServerValidate="CapTextValidation"
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
    <asp:AsyncPostBackTrigger ControlID="btnAddCap"/>
    <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
</Triggers>
</asp:UpdatePanel>
</div>
</asp:Content>