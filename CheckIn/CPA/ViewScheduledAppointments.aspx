<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewScheduledAppointments.aspx.cs" Inherits="CheckIn.Web_Pages.ViewScheduledAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
            width: 268435520px;
        }
    </style>
    <%--<script type="text/javascript" >
        Function CancelAppointment(){
            if(confirm('Are you sure you want to Cancel Appointment'))
            {
                return true;
            }
            else
                return false;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
           <%-- <table width="100%" style="background-color: #e6e6f5" cellpadding="10px">
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
                    <td width="25%">
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
                    <h1>View Appointment</h1>
                    <div class="left_corner"></div>
                    <div class="wizard_strip">
          
                       
                  
                        <ul>
                            <li><a href="ViewScheduledAppointments.aspx" class="active" >View Scheduled Appointments</a></li>
                            <li><a href="ViewPastAppointment.aspx">View Past Appointments</a></li>
                            <li><a href="ProfilePage.aspx" >Account Settings</a></li>
                            <%--<li><a href="#">Finished!</a></li>--%>
                        </ul>
                </div>
           <%-- <table width="100%" style="background-color: #e6e6f5" cellpadding="8px">
                <tr>
                    <td width="35%" style="background-color: #000079">
                        <asp:HyperLink ID="HyperLink3" runat="server"  NavigateUrl="~/CPA/ViewScheduledAppointments.aspx?">View Scheduled Appointments</asp:HyperLink>
                    </td>
                    <td width="35%">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/CPA/ViewPastAppointment.aspx?">View Past Appointments</asp:HyperLink>
                    </td>
                    <td width="30%">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/CPA/UserProfile.aspx?">Account Settings</asp:HyperLink>
                    </td>
                </tr>
            </table>--%>
         <div class="AppintmentContainer">
          <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False" AllowPaging="True"
               AllowSorting="True" OnPageIndexChanging="gvAppointments_PageIndexChanging"
                OnSorting="gvAppointments_Sorting" OnRowDataBound="gvAppointments_RowDataBound" CssClass="tableAppointment"
                OnDataBound="gvAppointments_DataBound" >
                <PagerSettings Mode="Numeric" Position="TopAndBottom" NextPageText="&gt;" PreviousPageText="&lt;">
                </PagerSettings>
                <PagerStyle BackColor="LightBlue" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                <Columns >
  
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >
                          <ItemTemplate >
                   
                                <table class="DocTable">
                                   <tr>
                                  <td rowspan="3">
                                        <a href='<%# "CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>' class="">
                                                 <span class="profile-picture">
                                                     <asp:Image ID="Image1" Height="150" Width="150" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'></asp:Image>
                                                 </span>
                                             </a>
                                        
                                  </td>
                                </tr>
                                <tr>
                                   
                                         <td>
                                             <br />
                                             <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" Font-Size="Medium" CssClass="pink"
                                                 Text='<%# Bind("CompanyName")%>' NavigateUrl='<%# "~/CPA/CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>'></asp:HyperLink>
                                             <asp:Label ID="lblCPAID" runat="server" Text='<%# Bind("CPAID")%>' Visible="false"></asp:Label>
                                             <%--</td>
                                </tr>
                               
                              
                                <tr>
                                    <td>--%>
                                             <br />
                                        <asp:Label ID="lblAddress1" runat="server" Text='<%# Bind("Address1")%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lblAddress2" runat="server" Text='<%# Bind("Address2")%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City")%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("State")%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lblZipCode" runat="server" Text='<%# Bind("ZipCode")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                              </ItemTemplate >
                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >
                          <ItemTemplate >       <%-- </td>
                                        <td>--%>
                                    <asp:Label ID="lbl1" runat="server" Text="Purpose of visit:"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblPurposeOfVisit" runat="server" Text='<%# Bind("PurposeOfVisit")%>'></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("CustomerName")%>'></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Contact Number:"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblContactNumber" runat="server" Text='<%# Bind("ContactNumber")%>'></asp:Label>
                                    <br />
                                    <br />
                              </ItemTemplate >
                                         <HeaderStyle HorizontalAlign="Left" CssClass="AppDay" />
                                <ItemStyle CssClass="AppDay" />              
                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >
                          <ItemTemplate > 
                                  <asp:Label ID="Label2" runat="server" Text="Schedule:"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblSchedule" runat="server" Text='<%# Bind("Time")%>'></asp:Label>
                                    <br />
                                 <a href='<%#"CancelCPAAppointment.aspx?ID="+Eval("ID")%>' onclick="return confirm('Are you sure you want to Cancel Appointment')"> Cancel&nbsp;  </a>
                                   <%-- <asp:HyperLink ID="lnkCancel" runat="server"  NavigateUrl='<%#"CancelCPAAppointment.aspx?ID=" +Eval("ID") %>'  > Cancel&nbsp;  </asp:HyperLink>
                                  --%>
                                  <%-- <asp:HyperLink ID="lnkReschedule" runat="server" NavigateUrl='<%# "UserReScheduleAppointment.aspx?CPAID="+Eval("CPAID")+"&Schedule="+Eval("Time") + "&ID=" +Eval("ID") %>' >
                                                 Reschedule </asp:HyperLink>--%>
                                  <a href='<%#"UserReScheduleAppointment.aspx?CPAID="+Eval("CPAID")+"&Schedule="+Eval("Time") + "&ID=" +Eval("ID")%>'  onclick="return confirm('Are you sure you want to Reschedule Appointment')"> Reschedule  </a>
                                  
                           
                              </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                       <ItemStyle CssClass="AppDayAlternate" />     
                                            </asp:TemplateField>
                            
                  
                 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
