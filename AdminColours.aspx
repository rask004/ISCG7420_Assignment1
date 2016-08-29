<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AdminColours.aspx.cs" Inherits="AdminColours" %>

<%--  
    The Admin page for the Colour Entity.
    
    Change Log:
        18-8-16  12:00       AskewR04        Created page and layout.

--%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Colours</title>
    <script type="text/javascript" src="JS/common.js" >
    </script>
    <script type="text/javascript" src="JS/Validation.js">
    </script>
</asp:Content>

<asp:Content ID="AdminColourSideBar" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
    <div id="ColourListingSection" class="AdminSection" 
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

<asp:Content ID="AdminColourMain" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div id="ColourEditingForm" class="container AdminSection" runat="server">
        <div class="row">
            <span class="BlankRow"></span>
        </div>

        <asp:UpdatePanel ID="CurrentItemMainPanel" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <span class="ContentShiftRight">
                            <asp:Button ID="btnAddColour" CausesValidation="false" OnClick="AddButton_Click" 
                                CssClass="MainButton" Text="Add New Colour..." runat="server"/>
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
                            <b><asp:Label CssClass="MainItemHeader" ID="lblColourIdHeader" runat="server" AssociatedControlID="lblColourId" 
                                                Text="ID:" /></b>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight ">
                            <b><asp:Label ID="lblColourId" Text="Colour_Id" runat="server" /></b>
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
                            <asp:Label ID="lblColourNameHeader" runat="server" AssociatedControlID="txtColourName" 
                                                CssClass="MainItemHeader" Text="Colour Name:" />
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span class="ContentShiftRight"><asp:TextBox ID="txtColourName" Enabled="false" runat="server" /></span>
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
                            <asp:RequiredFieldValidator ID="valRequiredColourName" runat="server" 
                            ControlToValidate="txtColourName"
                            ErrorMessage="The Colour Name is a required field." ForeColor="red"
                            />
                            <asp:CustomValidator runat="server"  ID="valCharsColourName"
                            ControlToValidate="txtColourName"
                            ErrorMessage="The Colour Name must only have alphabetic characters, spaces, commas, periods or apostrophes."
                            ClientValidationFunction="ValidateNameString"
                            OnServerValidate="ColourNameValidation"
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
                <asp:AsyncPostBackTrigger ControlID="btnAddColour"/>
                <asp:AsyncPostBackTrigger ControlID="btnCancelChanges"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>