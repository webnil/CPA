<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PersonalInformation.aspx.cs" Inherits="CheckIn.CPA.PersonalInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<style type="text/css">
        .style1
        {
             height: 154px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">

        function pageLoad(sender, args) {
            var sm = Sys.WebForms.PageRequestManager.getInstance();
            if (!sm.get_isInAsyncPostBack()) {
                sm.add_beginRequest(onBeginRequest);
                sm.add_endRequest(onRequestDone);
            }
        }

        function onBeginRequest(sender, args) {
            var send = args.get_postBackElement().value;
            if (displayWait(send) == "yes") {
                $find('PleaseWaitPopup').show();
            }
        }

        function onRequestDone() {
            $find('PleaseWaitPopup').hide();
        }

        function displayWait(send) {
            switch (send) {
                case "Save":
                    return ("yes");
                    break;
                case "Save and Close":
                    return ("yes");
                    break;
                default:
                    return ("no");
                    break;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveContactInformation" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
        <ContentTemplate>


            <div class="breadcrumbs" id="Div1">
                <ul class="breadcrumb">
                    <li>
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
                    <%--  <asp:Button ID="Button1" class="search_btn" runat="server" Text="" OnClick="btnRefineSearch_Click" />--%>
                </div>

                <%-- <asp:LinkButton class="search_btn" ID="LinkButton2" runat="server" Text="" OnClick="btnRefineSearch_Click" />--%>
            </div>


            <br />
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
                                <asp:Panel ID="pnlCustomerDetails" runat="server">
                                    <table cellpadding="5px" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Who will be seeing the CPA" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCustomerName" runat="server" Text="[Customer Name]"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPhoneNumber" runat="server" Text="[Phone Number]"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEditContact" runat="server" Text="Edit Contact Information" CssClass="buttonPink"
                                                    OnClick="btnEditContact_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <asp:UpdatePanel runat="server" ID="Panel">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnBookAppointment" runat="server" Text="Book Appointment" CssClass="buttonPink"
                                                            OnClick="btnBookAppointment_Click" Width="163px" />
                                                        <asp:Label ID="lblEmail" runat="server" Visible="false"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <%--  <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                                                    <ProgressTemplate>
                                                        Please wait while we are booking appointment for you...
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>--%>
                                                <asp:UpdateProgress ID="updProgress"
                                                    AssociatedUpdatePanelID="UpdatePanel1"
                                                    runat="server">
                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/progress.gif" />
                                                        Please wait while we are booking appointment for you...          
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlMaxBook" runat="server">

                                    <table cellpadding="5px" width="100%">
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMaxAppointment" runat="server" Text="Restricted to book Maximum 5 Appointment"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnOK" runat="server" Text="OK" Height="23px" OnClick="BtnOK_Click" Width="75px" CssClass="buttonPink" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlBookedAppointment" runat="server">

                                    <table cellpadding="5px" width="100%">
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label13" runat="server"
                                                    Text="This appointment has been already booked by somebody else
                                                            please select other appointment"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAlreadyBook" runat="server" CssClass="buttonPink" Text="OK" Height="23px" Width="75px" OnClick="btnAlreadyBook_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlEditCustomerDetails" runat="server" Visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFirstName" Text="First Name" runat="server"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="FirstName not Empty"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Allow Only 1-25 Alphabet " ControlToValidate="txtFirstName"
                                            ValidationExpression="^[a-zA-Z]{1,25}$"  ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                                <%--<asp:CustomValidator ID="CustValFisrtName" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" Text="Invalid First Name."></asp:CustomValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Last Name"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <asp:TextBox ID="txtLastName" Text="Last Name" runat="server"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="LastName Not Empty"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ValidationExpression="^[a-zA-z]{1,25}$"
                                            ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ErrorMessage="Allow Only 1-25 Alphabet" ControlToValidate="txtLastName"></asp:RegularExpressionValidator></td>--%>
                                                <%--<asp:CustomValidator ID="CustValLastName" runat="server" ForeColor="Red" ControlToValidate="txtLastName" Text="Invalid Last Name."></asp:CustomValidator>--%>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Contact Number"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td align="right">
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--                                                <td align="center">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhNumberPart1" ForeColor="Red" ErrorMessage="not empty"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="Regtxtph1"   runat="server"  ForeColor="Red" ErrorMessage="3 digits  start from 2" ControlToValidate="txtPhNumberPart1"
                                        ValidationExpression="^[2-9]\d{2}$"></asp:RegularExpressionValidator>

                                                </td> 
                                                 <td align="center">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="not empty" ControlToValidate="txtPhNumberPart2" ></asp:RequiredFieldValidator>
                                                     <br />
                                                      <asp:RegularExpressionValidator ID="Regtxtph2" runat="server"  ForeColor="Red" ErrorMessage="3 digits" ControlToValidate="txtPhNumberPart2"
                                        ValidationExpression="^\d{3}$"></asp:RegularExpressionValidator> 
                                               </td>

                                               <td align="center">
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ErrorMessage="not empty" ControlToValidate="txtPhNumberPart3"></asp:RequiredFieldValidator>
                                                   <br />
                                                        <asp:RegularExpressionValidator ID="regtxtph3" runat="server"  ForeColor="Red" ErrorMessage="4 digits" ControlToValidate="txtPhNumberPart3"
                                        ValidationExpression="^\d{4}$"></asp:RegularExpressionValidator>
                                                </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--<asp:CustomValidator ID="CustPhoneNumber" runat="server" ForeColor="Red" ErrorMessage="All Character are digit"></asp:CustomValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSaveContactInformation" runat="server" CssClass="buttonPink" 
                                                    Text="Save Contact Information" OnClick="btnSaveContactInformation_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonPink"
                                                    OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td valign="top">
                                <asp:Panel ID="pnlCPADetails" runat="server">
                                    <%--<table width="100%">
                            <tr>
                                <td width="30%">
                                     <a href="CPADetailsInfo.aspx">
                                        <asp:Image ID="imgCPA" Height="150" Width="150" runat="server"></asp:Image></a>
                                </td>
                                <td class="style1">
                                    <asp:HyperLink ID="lnkCPADetail" runat="server">CPA Company Name</asp:HyperLink>
                                    <br />
                                    <asp:Label ID="lblName" runat="server" Text="CPA Name"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAddress1" runat="server" Text="Address 1"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAddress2" runat="server" Text="Address 2"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCPAName" runat="server" Text="CPANAME" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCity" runat="server" Text="City" Visible="false"></asp:Label>
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
                        </table>--%>
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
                                                <asp:Label ID="lblCPAName" runat="server" Text="CPANAME" Visible="false"></asp:Label>
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
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
