<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CheckNewUser.aspx.cs" Inherits="CheckIn.CPA.CheckNewUser" %>

<%@ Register Src="~/CaptchaUserControl.ascx" TagPrefix="uc1" TagName="CaptchaUserControl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="breadcrumbs" id="Div1">
                <ul class="breadcrumb">
                    <li class="breadcrumb">
                        <i class="icon-home home-icon"></i>
                        <a href="../Default.aspx" class="pink">Home</a>

                        <span class="divider">
                            <i class="icon-angle-right arrow-icon pink-Icon"></i>
                        </span>
                    </li>
                </ul>
                <!--.breadcrumb-->

                <div class="nav-search" id="nav-search">

                    <input type="text" id="txtCity" runat="server" value="Enter City" autocomplete="off" class="input-small nav-search-input" onclick="if (this.value == 'Enter City') { this.value = '' }"
                        onblur="if(this.value==''){this.value='Enter City'}">

                    <input type="text" id="txtZipCode" runat="server" value="Enter Zip Code" autocomplete="off" class="input-small nav-search-input" onclick="if (this.value == 'Enter Zip Code') { this.value = '' }"
                        onblur="if(this.value==''){this.value='Enter Zip Code'}">
                    <asp:LinkButton class="search_btn" ID="btnRefineSearch" runat="server" Text="" OnClick="btnRefineSearch_Click" />
                </div>
            </div>
            <!--.breadcrumb-->


            <%-- <asp:LinkButton class="search_btn" ID="LinkButton2" runat="server" Text="" OnClick="btnRefineSearch_Click" />--%>
            </div>
            <br />
            <%--<table width="100%" style="background-color: #e6e6f5" cellpadding="10px">
        <tr>
            <td width="10%">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/CompanyLogo.jpg" Height="40" />
            </td>
            <%--<td width="15%">
                        <asp:DropDownList ID="ddlSpeciality" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td width="45%">
                        <asp:TextBox ID="txtZipCode" runat="server" Width="160px" Text="Enter Zip Code\City"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnRefineSearch" runat="server" Text="Refine Search" OnClick="btnRefineSearch_Click" />
                    </td>
                    <td width="25%">
                    </td>--%><%-- %>
            <td width="15%">
                <%-- <asp:DropDownList ID="ddlSpeciality" runat="server" Width="100%">
                        </asp:DropDownList>--%><%-- %>
                <input type="text" id="txtCity" runat="server" value="Enter City" onclick="if (this.value == 'Enter City') { this.value = '' }"
                    onblur="if(this.value==''){this.value='Enter City'}">
            </td>
            <td width="45%">
                <%-- <asp:TextBox ID="txtZipCode" runat="server" Width="160px" Text="Enter Zip Code\City"></asp:TextBox>--%><%--
                <input type="text" id="txtZipCode" runat="server" value="Enter Zip Code" onclick="if (this.value == 'Enter Zip Code') { this.value = '' }"
                    onblur="if(this.value==''){this.value='Enter Zip Code'}">
                &nbsp;
                        <asp:Button ID="btnRefineSearch" runat="server" Text="Refine Search" OnClick="btnRefineSearch_Click" />
            </td>
            <td width="25%"></td>
        </tr>
    </table>--%>

            <%--<table width="100%" style="background-color: #e6e6f5" cellpadding="8px">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="1. Select CPA Appointment"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="2. Appointment Information" Font-Bold="true"></asp:Label>
            </td>
            <td style="background-color: #000079">
                <asp:Label ID="Label5" runat="server" ForeColor="White" Text="3. Personal Information"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="4. Finished!"></asp:Label>
            </td>
        </tr>
    </table>--%>
            <div class="page-content">
                <!--PAGE CONTENT BEGINS-->
                <div class="page_border">
                    <h1>Book Appointment</h1>
                    <div class="left_corner"></div>
                    <div class="wizard_strip">



                        <ul>
                            <li><a href="#">Select CPA Appointment</a></li>
                            <li><a href="#">Appointment Information</a></li>
                            <li><a href="#" class="active">Personal Information</a></li>
                            <li><a href="#">Finished!</a></li>
                        </ul>
                    </div>
                    <table width="100%">
                        <tr>
                            <td width="70%">
                                <asp:Panel ID="pnlLoggedIn" runat="server" Visible="False">
                                    <table cellpadding="5px" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblUserName" runat="server" Text="LoggedIn User" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="buttonPink" OnClick="btnContinue_Click" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkBtnLoggedOut" runat="server" OnClick="lnkBtnLoggedOut_Click"></asp:LinkButton>
                                            </td>

                                        </tr>

                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlNotLoggedIn" runat="server">
                                    <table cellpadding="5px" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Have you visited this site before?" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnNewUser" runat="server" Text="I am new to this site" CssClass="buttonPink" Width="222px" OnClick="btnNewUser_Click1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">
                                                <asp:Button ID="btnRegisteredUser" runat="server" Text="I have used this site before" CssClass="buttonPink" OnClick="btnRegisteredUser_Click1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnBack" runat="server" Text="<< Back" CssClass="buttonPink"
                                                    OnClick="btnBack_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlNewUser" runat="server" Visible="false">
                                                    <div class="page-content">
                                                        <div class="page_border">
                                                            <div class="form-horizontal">
                                                                <h1>Create New Account </h1>
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
                                                                        <label class="control-label" for="txtPassword">Generate Password</label>
                                                                        <div class="controls">
                                                                            <asp:TextBox ID="txtPassword" CssClass="input-xlarge" TextMode="Password" runat="server"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password can not be empty" Text="*"
                                                                                ControlToValidate="txtPassword" ValidationGroup="signUpValidation" ForeColor="Red"></asp:RequiredFieldValidator>

                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ControlToValidate="txtPassword"
                                                                                ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" ValidationGroup="signUpValidation" Text="*"
                                                                                ErrorMessage="Password should contain 8 and 10 characters, one digit and one alphabetic character, and must not contains any special character."></asp:RegularExpressionValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="control-group">
                                                                        <label class="control-label">Name</label>
                                                                        <div class="controls">
                                                                            <asp:TextBox ID="txtFirstName" Style="width: 35%" runat="server" class="input-medium"></asp:TextBox>
                                                                            <asp:CustomValidator ID="CustValFisrtName" ValidationGroup="signUpValidation" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" Text="*"></asp:CustomValidator>
                                                                            <asp:TextBox ID="txtLastName" Style="width: 35%" runat="server" class="input-medium"></asp:TextBox>
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
                                                                        <div class="controls">
                                                                            <asp:CheckBox ID="cbAcceptTerms" Text="" runat="server" />
                                                                            <label class="lbl" for="cbAcceptTerms">I accept Terms and Conditions</label>
                                                                            <asp:CustomValidator runat="server" ID="CheckBoxRequired" ErrorMessage="Please accept terms and condition." ValidationGroup="signUpValidation" Text="*" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="control-group">
                                                                        <div class="controls">
                                                                            <asp:ValidationSummary ID="signUpValidation" runat="server" CssClass="validationSummary" DisplayMode="BulletList" ForeColor="Red" ValidationGroup="signUpValidation" />
                                                                        </div>
                                                                    </div>
                                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                                    <div align="center">
                                                                        <asp:Button ID="btnSignUp" CssClass="buttonPink" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" ValidationGroup="signUpValidation" CausesValidation="true" />
                                                                        <asp:UpdateProgress ID="updProgress"
                                                                            AssociatedUpdatePanelID="UpdatePanel1"
                                                                            runat="server">
                                                                            <ProgressTemplate>
                                                                                <img alt="progress" src="../Images/progress.gif" />
                                                                                Please wait while we are processing your request...          
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlRegisteredUser" runat="server" Visible="false">
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
                                                                                <asp:TextBox ID="txtLoginEmail" CssClass="input-xlarge" runat="server"></asp:TextBox>
                                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                    ErrorMessage="Email can not be empty" ControlToValidate="txtLoginEmail" Text="*"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                                    ControlToValidate="txtLoginEmail" Text="*" ForeColor="Red" ErrorMessage="Invalid Email Address " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                              
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
                                                                            <asp:TextBox ID="txtLoginPassword" CssClass="input-xlarge" TextMode="Password" runat="server"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password can not be empty" Text="*"
                                                                                ControlToValidate="txtLoginPassword" ValidationGroup="signUpValidation" ForeColor="Red"></asp:RequiredFieldValidator>

                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ControlToValidate="txtLoginPassword"
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
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </td>
                            <td valign="top">
                                <table class="DocTable">
                                    <tr>
                                        <td rowspan="1">
                                            <br />
                                            <%--  <a href='<%# "CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>' class="">--%>
                                            <a class="" href="CPADetailsInfo.aspx"><span class="profile-picture"><%-- <asp:Image ID="Image1" Height="150" Width="150" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'></asp:Image>--%>
                                                <asp:Image ID="imgCPA" runat="server" Height="150" Width="150" />
                                            </span></a>
                                            <br />
                                            <%--<a href='<%# "CPADetails.aspx" %>'> <asp:Image ID="Image1" Height="200" Width="250" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'  ></asp:Image></a>--%></td>
                                        <%--</tr>
                                     <tr>--%>
                                        <td>
                                            <asp:HyperLink ID="lnkCPADetail" runat="server" CssClass="pink" Font-Bold="true" Font-Size="Medium">CPA Company Name</asp:HyperLink>
                                            <br />
                                            <asp:Label ID="lblName" runat="server" Text="CPA Name"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblAddress1" runat="server" Text="Address 1"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblAddress2" runat="server" Text="Address 2"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Schedule" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblScheduleDate" runat="server" Text="[Schedule Date]"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label8" runat="server" Text="Purpose of Visit" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblPurposeOfVisit" runat="server" Text="[Purpose of Visit]"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
