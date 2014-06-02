<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Packages.aspx.cs" Inherits="CheckIn.Web_Pages.Packages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr align="center">
            <td align="left" width="50%">
                <table style="background-color: #e6e6f5" width="100%" cellpadding-left="10">
                 <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label9" runat="server" Text="Select a Package"></asp:Label>
                        </td>
                         <td style="padding-left: 20px;">
                            <asp:RadioButton ID="RadioButton1" Checked="true" Text="Regular Package (20$ per month)" GroupName="Package" runat="server" />
                            <br />
                            <asp:RadioButton ID="RadioButton2" Text="Premium Package (100$ per month)" GroupName="Package" runat="server" />
                        </td>
                    </tr>
              
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label2" runat="server" Text="Country"></asp:Label>
                        </td>
                         <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtCountry" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label3" runat="server" Text="Credit Card Number"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtCreditCardNumber" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label4" runat="server" Text="Payment Type"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtPaymentType" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label5" runat="server" Text="Expiration Date"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtExpirationDate" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label6" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtFirstName" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label7" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtLastName" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label8" runat="server" Text="Billing Address Line 1"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtAddress1" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label10" runat="server" Text="Billing Address Line 2"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtAddress2" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label12" runat="server" Text="City"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtCity" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label13" runat="server" Text="Zip Code"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtZipCode" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label14" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox ID="txtEmail" Width="80%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="padding-left: 20px;" colspan=2 align="center">
                            <asp:Button ID="btnMakePayment" runat="server" Text="Make a Payment" CssClass="buttonPink" 
                                onclick="btnMakePayment_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" width="50%">
                <table style="background-color: #e6e6f5" width="100%" cellpadding-left="10">
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:Label ID="Label11" runat="server" Text="Regular Package Information"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Premium Package Information"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="padding-left: 20px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
