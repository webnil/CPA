<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BookAppointment.aspx.cs" Inherits="CheckIn.CPA.BookAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
          
                        <%--<asp:Label ID="Label3"   class="active" runat="server" Text="1. Select CPA Appointment"></asp:Label>
                   
                        <asp:Label ID="Label4" runat="server" Text="2. Appointment Information"></asp:Label>
                   
                        <asp:Label ID="Label5" runat="server" Text="3. Personal Information"></asp:Label>
                    
                    
                        <asp:Label ID="Label6" runat="server" Text="4. Finished!"  Font-Bold="true"
                            ForeColor="White"></asp:Label>--%>
                  
                        <ul>
                            <li><a href="#">Select CPA Appointment</a></li>
                            <li><a href="#"  class="active">Appointment Information</a></li>
                            <li><a href="#">Personal Information</a></li>
                            <li><a href="#">Finished!</a></li>
                        </ul>
                </div>
                    <div class="right_corner"></div>
       <%-- <table width="100%" style="background-color: #e6e6f5" cellpadding="8px">
                <tr>
                    <td >
                        <asp:Label ID="Label3" runat="server" Text="1. Select CPA Appointment" 
                            ></asp:Label>
                    </td>
                    <td style="background-color: #000079">
                        <asp:Label ID="Label4" runat="server" Text="2. Appointment Information" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="3. Personal Information"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="4. Finished!"></asp:Label>
                    </td>
                </tr>
            </table>--%>
            <table width="100%">
                <tr>
                    <td width="60%">
                        <table cellpadding="10px">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="What is the purpose of your visit"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPurposeOfMeeting" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Appointment Time"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkBtnAppointmentTime" runat="server" 
                                        onclick="lnkBtnAppointmentTime_Click" >Appointment Time</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="buttonPink"
                                        onclick="btnNext_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                       <%-- <table width="100%">
                            <tr>
                                <td width=30%>
                                    <%--<a href='<%# "CPADetailsInfo.aspx?QueryCPAID="+Eval("CPAID") %>'>--%>
                                    <%-- <a href="CPADetailsInfo.aspx?CPAID=<%=CPAIDLink %>">--%><%--
                                    <a href="CPADetailsInfo.aspx">
                                        <asp:Image ID="imgCPA" Height="150" Width="150" runat="server"></asp:Image></a>

                                </td>
                                <td>
                                    <asp:HyperLink ID="lnkCPADetail" runat="server" >CPA Company Name</asp:HyperLink>
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
                        </table>--%>
                         <table class="DocTable">
                                     <tr>
                                         <td rowspan="3">
                                             <br />
                                             <%--  <a href='<%# "CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>' class="">--%><a class="" href="CPADetailsInfo.aspx"><span class="profile-picture"><%-- <asp:Image ID="Image1" Height="150" Width="150" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'></asp:Image>--%>
                                             <asp:Image ID="imgCPA" runat="server" Height="150" Width="150" />
                                             </span></a>
                                             <br />
                                             <%--<a href='<%# "CPADetails.aspx" %>'> <asp:Image ID="Image1" Height="200" Width="250" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'  ></asp:Image></a>--%>

                                         </td>
                                    <%-- </tr>
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
                                 </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlAppointment" runat="server" Visible="False">


                            <asp:GridView ID="gvCPASearchResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                PageSize="2" AllowSorting="True" OnPageIndexChanging="gvCPASearchResult_PageIndexChanging"
                                OnSorting="gvCPASearchResult_Sorting" OnRowDataBound="gvCPASearchResult_RowDataBound"
                                OnDataBound="gvCPASearchResult_DataBound" CssClass="tableAppointment">
                                <PagerSettings Mode="Numeric" Position="TopAndBottom" NextPageText="&gt;" PreviousPageText="&lt;"></PagerSettings>
                                <PagerStyle BackColor="LightBlue" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <%-- <asp:Button ID="btnPrevWeek" runat="server" Text="<<" OnClick="btnPrevWeek_Click" />--%>
                                            <div class="button">

                                                <asp:LinkButton CssClass="prev" Text="" runat="server" ID="btnPrevWeek" OnClick="btnPrevWeek_Click" />
                                            </div>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                        <ItemStyle CssClass="AppDayAlternate" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label6" runat="server" Text="Mon"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblMondayDate" runat="server" Text="1/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoAppointment" runat="server" />
                                            <asp:Repeater ID="rptMondayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID") %>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDay" />
                                        <ItemStyle CssClass="AppDay" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTuesdayCaption" runat="server" Text="Tue"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTuesdayDate" runat="server" Text="2/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptTuesdayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID")%>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                        <ItemStyle CssClass="AppDayAlternate" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblWednesdayCaption" runat="server" Text="Wed"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblWednesdayDate" runat="server" Text="3/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptWednesdayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID")%>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDay" />
                                        <ItemStyle CssClass="AppDay" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblThursdayCaption" runat="server" Text="Thu"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblThursdayDate" runat="server" Text="4/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptThursdayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID")%>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                        <ItemStyle CssClass="AppDayAlternate" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFridayCaption" runat="server" Text="Fri"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFridayDate" runat="server" Text="5/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptFridayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID") %>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDay" />
                                        <ItemStyle CssClass="AppDay" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSaturdayCaption" runat="server" Text="Sat"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSaturdayDate" runat="server" Text="6/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptSaturdayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID") %>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                        <ItemStyle CssClass="AppDayAlternate" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSundayCaption" runat="server" Text="Sun"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSundayDate" runat="server" Text="6/1/2011" CssClass="AppDate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptSundayAppointments" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID") %>'>
                                                        <%# Eval("Time")%></a>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDay" />
                                        <ItemStyle CssClass="AppDay" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <%--  <asp:Button ID="btnNextWeek" runat="server" Text=">>" OnClick="btnNextWeek_Click" />--%>
                                            <div class="button">
                                                <%-- <asp:Button ID="btnNextWeek" CssClass="next" runat="server" Text="" OnClick="btnNextWeek_Click"  />--%>
                                                <asp:LinkButton CssClass="next" Text="" runat="server" ID="btnNextWeek" OnClick="btnNextWeek_Click" />
                                            </div>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                        <ItemStyle CssClass="AppDayAlternate" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>   
              
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
