<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CPAProfile.aspx.cs" Inherits="CheckIn.CPA.CPAProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="page-content">
                <div class="page_border">
                    <h1>Account Information </h1>

                    <div class="form-horizontal">
                        <div class="tabbable">
                            <ul class="nav nav-tabs padding-16">
                                <li class="active">
                                    <a data-toggle="tab" href="#edit-basic">
                                        <i class="green icon-edit bigger-125"></i>
                                        Basic Info
                                    </a>
                                </li>

                                <li>
                                    <a data-toggle="tab" href="#edit-settings">
                                        <i class="icon-envelope"></i>
                                        Address
                                    </a>
                                </li>


                            </ul>
                            <div class="tab-content profile-edit-tab-content">
                                <div id="edit-basic" class="tab-pane in active">
                                    <div class="control-group">
                                        <label class="control-label">Image </label>
                                        <div class="row-fluid">
                                            <div class=" controls">
                                                <asp:Image ID="imgCPA" runat="server" Height="62px" Width="62px" />
                                                <br />
                                                <asp:FileUpload ID="ImageUpload" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label" for="txtCompanyName">Company Name</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
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
                                        <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" CausesValidation="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqValCity" ValidationGroup="signUpValidation" runat="server" ErrorMessage="Please Select City"
                                            ControlToValidate="ddlCity" InitialValue="--Please Select City--" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Practicing Zip Code</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label" for="txtEmail">Email</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Name</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtLastName" Text="Last Name" runat="server" class="input-large"></asp:TextBox>

                                            <asp:TextBox ID="txtFirstName" Text="First Name" runat="server" class="input-large"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Allow Only 1-25 Alphabet " ControlToValidate="txtFirstName"
                                                ValidationExpression="^[a-zA-Z]{1,25}$" ForeColor="Red"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="CustValFisrtName" runat="server" ForeColor="Red" ControlToValidate="txtFirstName"></asp:CustomValidator>
                                            <asp:RegularExpressionValidator ValidationExpression="^[a-zA-z]{1,25}$"
                                                ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ErrorMessage="Allow Only 1-25 Alphabet" ControlToValidate="txtLastName"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="CustValLastName" runat="server" ForeColor="Red" ControlToValidate="txtLastName"></asp:CustomValidator>
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
                                </div>
                                <div id="edit-settings" class="tab-pane">
                                    <div class="control-group">
                                        <label class="control-label">Phone Number</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustPhoneNumber" runat="server" ForeColor="Red" ErrorMessage="Invalid Phone number"></asp:CustomValidator>
                                        </div>
                                    </div>


                                    <div class="control-group">
                                        <label class="control-label">Office Address Line 1</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtOfficeAddress1" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Office Address Line 2</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtOfficeAddress2" runat="server"></asp:TextBox>

                                        </div>
                                    </div>


                                    <div class="control-group">
                                        <label class="control-label">Speciality</label>
                                        <asp:TextBox ID="txtSpeciality" runat="server"></asp:TextBox>
                                        <asp:DropDownList ID="ddlSpeciality" runat="server" AppendDataBoundItems="true" CausesValidation="true">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div style="margin-left: 40px">

                                    <asp:Button CssClass="buttonPink" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" CssClass="buttonPink" runat="server" Text="Cancel" OnClick="btnCancel_Click" Style="margin-left: 50px" />
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
