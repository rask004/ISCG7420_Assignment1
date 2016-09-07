<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<%--  
    The contact page for the Quality Caps Website.
    
    Change Log:
        8-8-16  21:11       AskewR04        Created page and layout, filled with content.
        9-8-16  11:45       AskewR04        Updated this page after updating Master page.

--%>
<asp:Content ID="ContactTitle" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    Quality Caps - Contact Information
</asp:Content>
<asp:Content ID="ContactBody" ContentPlaceHolderID="PageContentCentre" Runat="Server">
    <%-- Had lots of problems with bootstrap grid, found this approach worked best --%>
    <div class="container PageSectionCentre">
        <%-- Gives a pleasantly spaced layout. Tables was trialled but it wasn't working right. --%>
        <div class="row">
            <div class="DecoHeader" style="margin-left:12%">
                <H3 style="margin-left:43%">Contact Us</H3>
            </div>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">Phone Number:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">99-5555-5555</span>
            </div>
            <div class="col-md-2"></div>
            
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">(9am to 5:30pm)</span>
            </div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-2"></div>
           
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">Fax Number:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">99-5555-6666</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">Sales:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">sales@QualityCaps.co.nz</span>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">General:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">general@QualityCaps.co.nz</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">IT Support:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">support@QualityCaps.co.nz</span>
            </div>
            <div class="col-md-2"></div>
            
            
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">Mailing Address:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">PO Box 7711</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">44 Simon Says Street</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">Sunny Side Suburb</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">Auckland</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">Postcode:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">9999</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <span class="ContentShiftLeft">Country:</span>
            </div>
            <div class="col-md-4">
                <span class="ContentShiftRight">New Zealand</span>
            </div>
            <div class="col-md-2"></div>
            
        </div>
        <div class="row">
            <span class="BlankRow"></span>
        </div>
    </div>
</asp:Content>

