<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidatedUser.aspx.cs" Inherits="CheckIn.CPA.ValidatedUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   
    
    <asp:Panel ID="PnlValid" runat="server">
         <p><b>Congratulation ! </b>Your Account is Validated  </p>
         <p>
        Please <a href="SignIn.aspx" >Login</a>
    </p>
     
    </asp:Panel>
    
    <asp:Panel ID="pnlNotValid" runat="server">
        <p>
            Validation  Expired .
        </p>
    </asp:Panel>
</asp:Content>
