<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs" Inherits="CheckIn.Web_Pages.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td style="width: 50%"></td>
                    <td align="left">
                        <div class="page-content">
                            <div class="page_border">
                                <div class="form-horizontal">
                                    <h1>Forgot Password </h1>
                                    <%--   <asp:Label ID="Label1" runat="server" Text="It only takes few minutes"></asp:Label>    --%>
                                    <div class="tabbable">
                                        <div class="control-group">
                                            <label class="control-label" for="txtEmail">Email</label>
                                            <div class="controls">
                                                <span class="input-icon input-icon-right">
                                                    <asp:TextBox ID="txtEmail" CssClass="input-xlarge" runat="server"></asp:TextBox>
                                                    <i class="icon-envelope"></i>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ValidationGroup="loginControl"
                                                        ErrorMessage="Email can not be empty" ControlToValidate="txtEmail"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="*" ValidationGroup="loginControl"
                                                        ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Invalid Email Address " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                              
                                                    </asp:RegularExpressionValidator>

                                                </span>
                                            </div>
                                        </div>

                                        <div align="center">
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>

                                        </div>

                                        <div class="control-group">
                                            <asp:CustomValidator ID="loginValidation" runat="server"
                                                ForeColor="Red" ErrorMessage="Login failed. Please check your username and password." ValidationGroup="loginControl" Visible="false"></asp:CustomValidator>
                                            <asp:ValidationSummary ID="ValidationSummary"
                                                ValidationGroup="loginControl" runat="server" DisplayMode="BulletList"
                                                CssClass="validationSummary"
                                                ForeColor="Red"></asp:ValidationSummary>
                                        </div>

                                        <div align="center">
                                            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="buttonPink" ValidationGroup="loginControl" CausesValidation="true"
                                                OnClick="btnResetPassword_Click" />
                                        </div>
                                        <asp:UpdateProgress ID="updProgress"
                                            AssociatedUpdatePanelID="UpdatePanel1"
                                            runat="server">
                                            <ProgressTemplate>
                                                <img alt="progress" src="../Images/progress.gif" />
                                                Please wait while we are retrieving your account details...          
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <br />

                                    </div>

                                </div>

                            </div>

                        </div>

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
