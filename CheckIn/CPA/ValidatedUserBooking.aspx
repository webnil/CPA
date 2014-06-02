<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidatedUserBooking.aspx.cs" Inherits="CheckIn.CPA.ValidateUserBooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:Panel ID="PnlValid" runat="server">
    <table>
        <tr>
            <td width="70%" align="center"> <p><b>Congratulation ! </b>Your Account is Validated  </p>
         <p>
        Please Continue Appointment Booking 
             </p> </td>
      
                                <td >
                                    <asp:Panel ID="pnlRegisteredUser" runat="server"  Width="384px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left">
                                                    <table style="background-color: #e6e6f5" width="100%" cellpadding-left="10">
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:Label ID="Label16" runat="server" Text="Sign In" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:Label ID="Label17" runat="server" Text="Email"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:TextBox ID="txtLoginEmail" Width="70%" runat="server"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email can not be empty" SetFocusOnError="true"
                                                                    ControlToValidate="txtLoginEmail" ForeColor="Red" ValidationGroup="loginControl" ></asp:RequiredFieldValidator >
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Email Address " 
                                                                      SetFocusOnError="true" ControlToValidate="txtLoginEmail" ValidationGroup="loginControl"
                                                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:Label ID="Label18" runat="server" Text="Password"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:TextBox ID="txtLoginPassword" TextMode="Password" Width="70%" runat="server"></asp:TextBox>
                                                               
                                                                <br /> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password can not be empty" 
                                                                    ControlToValidate="txtLoginPassword" ForeColor="Red"   ValidationGroup="loginControl" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                <br />
                                                               <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ForeColor="Red" ControlToValidate="txtLoginPassword"
                                                                      ErrorMessage="Contain 8 to 10 characters, one digit and one alphabetic character, and must not contain special characters."
                                                                     ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" ValidationGroup="loginControl" SetFocusOnError="true">
                                                                 </asp:RegularExpressionValidator>
                                                                --%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:Label ID="Label19" runat="server" Text="Forgot your password?"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CustomValidator ID="loginValidation" runat="server" ForeColor="Red" ErrorMessage="Login failed. Please check your username and password."
                                                                    ValidationGroup="loginControl" Visible="false" SetFocusOnError="true"></asp:CustomValidator>
                                                                <asp:ValidationSummary ID="ValidationSummary" ValidationGroup="loginControl" runat="server" 
                                                                    DisplayMode="BulletList" CssClass="validationSummary" ForeColor="Red" ></asp:ValidationSummary>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 20px;">
                                                                <asp:Button ID="btnSignIn" runat="server" CssClass="buttonPink"  Text="Sign In" OnClick="btnSignIn_Click"  ValidationGroup="loginControl" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
        </table>
                               
    </asp:Panel>
    
    <asp:Panel ID="pnlNotValid" runat="server">
        <p>
            Validation  Expired .
        </p>
    </asp:Panel>
</asp:Content>

