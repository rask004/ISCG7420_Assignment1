<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AdminImages.aspx.cs" Inherits="AdminImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Categories</title>
    <script type="text/javascript" src="~/Content/common.js" >
    </script>
    <script type="text/javascript" src="~/Content/Validation.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentSideBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AdminContentMain" Runat="Server">
    <div id="CategoryEditingForm" class="container AdminSection" runat="server"
        style="position: fixed; overflow-y: scroll; overflow-x: hidden; width: 66%; min-height: 55%; max-height:86%">
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span class="BlankRow"></span>
            </div>
            <div class="col-md-4">
                <span>
                    <asp:FileUpload ID="fupImageUploader" runat="server"/>
                </span>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnUploadImage" Text="Upload..." OnClick="btnUploadImage_OnClick" runat="server"/>
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
            <div class="col-md-8">
                <asp:Label ID="lblStatusMessage" runat="server" Text="Ready."/>
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
            <div class="col-md-8">
                <div id="fileListingDiv" class="container-fluid" runat="server">
                    <asp:DataList RepeatDirection="Vertical" OnItemDataBound="dtlUploadedImages_OnItemDataBound" RepeatColumns="2" RepeatLayout="Table" 
                        CellSpacing="10" CellPadding="5" ID="dtlUploadedImages" runat="server">
                        <ItemTemplate>
                            <td runat="server" style="border: black 1px solid; padding: 0.5%">
                                <p>
                                    <asp:Image ID="imgCurrentImage" ImageUrl=<%# DataBinder.Eval(Container.DataItem, "Value") %> runat="server"/>
                                </p>
                                <p>
                                    <%# DataBinder.Eval(Container.DataItem, "Text") %>
                                </p>
                                <p>
                                    <asp:Button ID="btnDeleteImage" Text="Delete" runat="server"/>
                                </p>

                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            
            <div class="col-md-2">
                <span class="BlankRow"></span>
            </div>
        </div>
    </div>
</asp:Content>

