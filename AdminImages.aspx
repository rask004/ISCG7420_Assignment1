<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AdminImages.aspx.cs" Inherits="AdminImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <title>Administration - Categories</title>
    <script type="text/javascript" src="JS/common.js" >
    </script>
    <script type="text/javascript" src="JS/Validation.js">
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
                    <input type="file" id="uploadFileImage" name="uploadFileImage" onclick="previewFile()"/>
                </span>
            </div>
            <div class="col-md-4">
                <!-- submit file button -->
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
                Status message will show here.
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
                <div id="fileListingDiv" class="container-fluid" style="border: 1px solid black;" runat="server"></div>
            </div>
            
            <div class="col-md-2">
                <span class="BlankRow"></span>
            </div>
        </div>
    </div>
</asp:Content>

