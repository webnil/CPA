<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="CheckIn.CPA.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DropDownList ID="ddlState" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCity" runat="server">
    </asp:DropDownList>

    <asp:Button ID="btnSelect" runat="server" Text="Select" />


  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$='ddlState']").css("width","150px").select2({
           
        });

        $("[id$='ddlCity']").css("width", "150px").select2({
           
        });


    });
</script>
</asp:Content>