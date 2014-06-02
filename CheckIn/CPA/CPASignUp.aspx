<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CPASignUp.aspx.cs" Inherits="CheckIn.Web_Pages.CPASignUp" %>

<%@ Register Src="../CaptchaUserControl.ascx" TagName="CaptchaUserControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-content">
                <div class="page_border">
                    <div class="form-horizontal">
                        <h1>Create New Account </h1>
                        <%--   <asp:Label ID="Label1" runat="server" Text="It only takes few minutes"></asp:Label>    --%>
                        <div class="tabbable">
                            <div class="control-group">
                                <label class="control-label">Company Name</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtCompanyName" CssClass="input-xlarge" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqValCompanyName" runat="server" ErrorMessage="Company Name can not be empty" SetFocusOnError="true" Text="*"
                                        ControlToValidate="txtCompanyName" ForeColor="Red" ValidationGroup="signUpValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
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
                                <label class="control-label" for="txtPassword">Generate Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtPassword" CssClass="input-xlarge" TextMode="Password" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password can not be empty" Text="*"
                                        ControlToValidate="txtPassword" ValidationGroup="signUpValidation" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ControlToValidate="txtPassword"
                                        ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" ValidationGroup="signUpValidation" Text="*"
                                        ErrorMessage="Password should contain 8 and 10 characters, one digit and one alphabetic character, and must not contains any special character."></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Name</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFirstName" runat="server" class="input-medium"></asp:TextBox>
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
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Image</label>
                                <div class="controls">
                                    <asp:FileUpload ID="ImageUpload" runat="server" Width="320px" />
                                     <asp:Label ID="lblFile" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Practicing Zip Code</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtZipCode" runat="server" class="input-medium"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqValZipcode" runat="server" ErrorMessage="Zip code can not be empty" SetFocusOnError="true" Text="*"
                                        ControlToValidate="txtZipCode" ForeColor="Red" ValidationGroup="signUpValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Office Address Line 1</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtOfficeAddress1" runat="server" class="input-medium"></asp:TextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Office Address Line 2</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtOfficeAddress2" runat="server" class="input-medium"></asp:TextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Practicing State</label>
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CausesValidation="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AppendDataBoundItems="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValState" runat="server" ErrorMessage="Please Select State" ControlToValidate="ddlState"
                                    ValidationGroup="signUpValidation" ForeColor="Red" InitialValue="--Please Select State--" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Practicing City</label>
                                <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true"  CausesValidation="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValCity" ValidationGroup="signUpValidation" runat="server" ErrorMessage="Please Select City"
                                    ControlToValidate="ddlCity" InitialValue="--Please Select City--" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Speciality</label>
                                <asp:TextBox ID="txtSpeciality" runat="server"></asp:TextBox>
                                <asp:DropDownList ID="ddlSpeciality" runat="server" AppendDataBoundItems="true" CausesValidation="true">
                                </asp:DropDownList>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <asp:CheckBox ID="cbTermCondition" Text="" runat="server" />
                                    <label class="lbl" for="cbTermCondition">I accept Terms and Conditions</label>
                                    <asp:CustomValidator runat="server" ID="CheckBoxRequired" ErrorMessage="Please accept terms and condition." ValidationGroup="signUpValidation" Text="*" />
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <asp:ValidationSummary ID="signUpValidation" runat="server" CssClass="validationSummary" DisplayMode="BulletList" ForeColor="Red" ValidationGroup="signUpValidation" />
                                </div>
                            </div>
                            <div align="center" width="40%" >
                                <asp:Button ID="btnSignUp" CssClass="buttonPink" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" ValidationGroup="signUpValidation" CausesValidation="true" />
                                <asp:UpdateProgress ID="updProgress"
                                                    AssociatedUpdatePanelID="UpdatePanel1"
                                                    runat="server">
                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/progress.gif" />
                                                        Please wait while we are creating your account...          
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                            </div>
                        </div>
                    <br />
                </div>

            </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
