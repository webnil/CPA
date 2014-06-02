<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="CheckIn.CPA.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-content">
                <div class="page_border">
                    <div class="form-horizontal">
                        <h1>Your Profile</h1>
                        <%--   <asp:Label ID="Label1" runat="server" Text="It only takes few minutes"></asp:Label>    --%>
                        <div class="tabbable">
                            <div class="control-group">
                                <label class="control-label" for="txtEmail">Email</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtEmail" CssClass="input-xlarge" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Email can not be empty" SetFocusOnError="true" Text="*"
                                        ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="signUpValidation"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Email Address" Text="*" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="signUpValidation" ForeColor="Red"></asp:RegularExpressionValidator>

                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ForeColor="Red" ErrorMessage="Email not Exist" ValidationGroup="signUpValidation" Text="*"></asp:CustomValidator>

                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Name</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFirstName"  runat="server" class="input-medium"></asp:TextBox>
                                    <asp:CustomValidator ID="CustValFisrtName" ValidationGroup="signUpValidation" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" Text="*"></asp:CustomValidator>
                                    <asp:TextBox ID="txtLastName" runat="server" class="input-medium"></asp:TextBox>
                                    <asp:CustomValidator ID="CustValLastName" ValidationGroup="signUpValidation" Text="*" runat="server" ForeColor="Red" ControlToValidate="txtLastName"></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Date Of Birth</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtDateOfBirth" runat="server" class="input-medium"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender
                                        ID="CalendarExtender1"
                                        TargetControlID="txtDateOfBirth"
                                        runat="server" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Gender</label>

                                <div class="controls">
                                    <div class="space-2"></div>

                                    <label class="inline">
                                        <asp:RadioButton ID="rbtnMale" Checked="true" GroupName="Gender" runat="server" />
                                        <span class="lbl">Male</span>
                                    </label>
                                    &nbsp; &nbsp; &nbsp;
							<label class="inline">
                                <asp:RadioButton ID="rbtnFemale" GroupName="Gender" runat="server" />
                                <span class="lbl">Female</span>

                            </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Phone Number</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtPhNumberPart1" runat="server" class="input-small"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                        TargetControlID="txtPhNumberPart1"
                                        Mask="999-999-9999"
                                        ClearMaskOnLostFocus="false"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="None"
                                        InputDirection="LeftToRight"
                                        AcceptNegative="Left"
                                        DisplayMoney="Left" Filtered="-"
                                        ErrorTooltipEnabled="True" />
                                    <asp:CustomValidator ID="CustPhoneNumber" ValidationGroup="signUpValidation" runat="server"  ForeColor="Red" ErrorMessage="Invalid phone number"></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Image</label>
                                <div class="controls">
                                    <asp:FileUpload ID="ImageUpload" runat="server" Width="320px" />
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <asp:Image ID="imgUser" runat="server" Height="62px" Width="62px" />
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <asp:ValidationSummary ID="signUpValidation" runat="server" CssClass="validationSummary" DisplayMode="BulletList" ForeColor="Red" ValidationGroup="signUpValidation" />
                                </div>
                            </div>
                            <div style="margin-left: 50px">
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonPink" ValidationGroup="signUpValidation" Text="Save" OnClick="btnSave_Click"  />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonPink" OnClick="btnCancel_Click" style="margin-left:30px" />
                            </div>
                            <tr>
                        </div>
                        <br />
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
