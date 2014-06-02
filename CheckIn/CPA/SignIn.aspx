<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SignIn.aspx.cs" Inherits="CheckIn.Web_Pages.SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%"></td>
            <td align="left">
                <div class="page-content">
                    <div class="page_border">
                        <div class="form-horizontal">
                            <h1>Sign In </h1>
                            <%--   <asp:Label ID="Label1" runat="server" Text="It only takes few minutes"></asp:Label>    --%>
                            <div class="tabbable">
                                <div class="control-group">
                                    <label class="control-label" for="txtEmail">Email</label>
                                    <div class="controls">
                                        <span class="input-icon input-icon-right">
                                            <asp:TextBox ID="txtEmail" CssClass="input-xlarge" runat="server"></asp:TextBox>
                                            <i class="icon-envelope"></i>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ErrorMessage="Email can not be empty" ControlToValidate="txtEmail"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Invalid Email Address " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                              
                                            </asp:RegularExpressionValidator>

                                        </span>
                                    </div>
                                </div>
                                <%--<div class="control-group">
                                    <label class="control-label" for="txtPassword">Password</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtPassword" CssClass="input-xlarge"  TextMode="Password" Width="80%" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ErrorMessage="Password can not be empty" ControlToValidate="txtPassword"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>
                                <div class="control-group">
                                    <label class="control-label" for="txtPassword">Password</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtPassword" CssClass="input-xlarge" TextMode="Password" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password can not be empty" Text="*"
                                            ControlToValidate="txtPassword" ValidationGroup="signUpValidation" ForeColor="Red"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ControlToValidate="txtPassword"
                                            ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" ValidationGroup="signUpValidation" Text="*"
                                            ErrorMessage="Password should contain 8 and 10 characters, one digit and one alphabetic character, and must not contains any special character."></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:CustomValidator ID="loginValidation" runat="server"
                                        ForeColor="Red" ErrorMessage="Login failed. Please check your username and password." ValidationGroup="loginControl" Visible="false"></asp:CustomValidator>
                                    <asp:ValidationSummary ID="ValidationSummary"
                                        ValidationGroup="loginControl" runat="server" DisplayMode="BulletList"
                                        CssClass="validationSummary"
                                        ForeColor="Red"></asp:ValidationSummary>
                                </div>
                                <div class="control-group">
                                    <div class="controls">
                                        <a href="ForgotPassword.aspx">Forgot your password?</a>
                                    </div>
                                </div>
                                <div align="center">
                                    <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="buttonPink" ValidationGroup="loginControl" CausesValidation="true"
                                        OnClick="btnSignIn_Click" />
                                </div>
                                <br />

                            </div>

                        </div>

                    </div>

                </div>



            </td>
        </tr>
    </table>
</asp:Content>
