<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FinishBookingApp.aspx.cs" Inherits="CheckIn.CPA.FinishBookingApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

           <%-- <table width="100%" style="background-color: #e6e6f5" cellpadding="8px">
                <tr>
                    <td width="10%">
                        <asp:Image ID="Image1" runat="server" Height="40" ImageUrl="~/Images/CompanyLogo.jpg" />
                    </td>
                    <%-- <td width="15%">
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
                    <td width="15%"><%-- <asp:DropDownList ID="ddlSpeciality" runat="server" Width="100%">
                        </asp:DropDownList>--%><%-- %>
                        <input id="txtCity" runat="server" onblur="if(this.value==''){this.value='Enter City'}" onclick="if (this.value == 'Enter City') { this.value = '' }" type="text" value="Enter City"> </input></td>
                    <td width="45%"><%-- <asp:TextBox ID="txtZipCode" runat="server" Width="160px" Text="Enter Zip Code\City"></asp:TextBox>--%><%--
                        <input id="txtZipCode" runat="server" onblur="if(this.value==''){this.value='Enter Zip Code'}" onclick="if (this.value == 'Enter Zip Code') { this.value = '' }" type="text" value="Enter Zip Code"> &nbsp;
                        <asp:Button ID="btnRefineSearch" runat="server" OnClick="btnRefineSearch_Click" Text="Refine Search" />
                        </input></td>
                    <td width="25%"></td>
            </table>
            <br />
            <table width="100%" style="background-color: #e6e6f5" cellpadding="8px">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="1. Select CPA Appointment"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="2. Appointment Information"></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="Label5" runat="server" Text="3. Personal Information"></asp:Label>
                    </td>
                    <td style="background-color: #000079">
                        <asp:Label ID="Label6" runat="server" Text="4. Finished!"  Font-Bold="true"
                            ForeColor="White"></asp:Label>
                    </td>
                </tr>
            </table>--%>
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
                    
                                 <input type="text" id="txtCity" runat="server" value="Enter City"  autocomplete="off" class="input-small nav-search-input" onclick="if (this.value == 'Enter City') { this.value = '' }" 
                            onblur="if(this.value==''){this.value='Enter City'}">
                           
                                <input type="text" id="txtZipCode" runat="server" value="Enter Zip Code"  autocomplete="off" class="input-small nav-search-input" onclick="if (this.value == 'Enter Zip Code') { this.value = '' }" 
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
                            <li><a href="#" >Select CPA Appointment</a></li>
                            <li><a href="#">Appointment Information</a></li>
                            <li><a href="#" >Personal Information</a></li>
                            <li><a href="#" class="active">Finished!</a></li>
                        </ul>
                </div>
            <table width="100%">
                <tr>
                    <td width="70%">
                        <asp:Panel ID="pnlCustomerDetails" runat="server">
                            <table cellpadding="5px" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Congrats! You are done!" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="lnkViewAllAppointment" runat="server">View all your scheduled appointments</asp:HyperLink>
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
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label2" runat="server" Text="Name:" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblCustomerName" runat="server" Text="[Customer Name]"></asp:Label>
                                </td>
                            </tr>
                              <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label9" runat="server" Text="Contact Number:" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblContactNumber" runat="server" Text="[Contact Number]"></asp:Label>
                                </td>
                            </tr>
                                 </table>
                            
                        <%--<table width="100%">
                            <tr>
                                <td width="30%">
                                     <a href="CPADetailsInfo.aspx">
                                        <asp:Image ID="imgCPA" Height="150" Width="150" runat="server"></asp:Image></a>
                                </td>
                                <td>
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
                             <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label2" runat="server" Text="Name:" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblCustomerName" runat="server" Text="[Customer Name]"></asp:Label>
                                </td>
                            </tr>
                              <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label9" runat="server" Text="Contact Number:" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblContactNumber" runat="server" Text="[Contact Number]"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
