<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="CheckIn.CPA.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table width="100%" style="background-color: #e6e6f5" cellpadding="10px">
                <tr>
                    <td width="10%">
                        &nbsp;</td>
                    <td width="15%">
                      
                        <input type="text" id="txtCity" runat="server" value="Enter City" onclick="if (this.value == 'Enter City') { this.value = '' }" 
                            onblur="if(this.value==''){this.value='Enter City'}">
                    </td>
                    <td width="45%">
                         <input type="text" id="txtZipCode" runat="server" value="Enter Zip Code" onclick="if (this.value == 'Enter Zip Code') { this.value = '' }" 
                            onblur="if(this.value==''){this.value='Enter Zip Code'}">
                        &nbsp;
                        <asp:Button ID="btnRefineSearch" runat="server" Text="Refine Search" OnClick="btnRefineSearch_Click" />
                    </td>
                    <td width="25%">
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" style="background-color: #e6e6f5" cellpadding="8px">
                <tr>
                    <td width="35%" style="background-color: #000079">
                        <asp:HyperLink ID="HyperLink3" runat="server"  NavigateUrl="~/CPA/ViewScheduledAppointments.aspx">View Scheduled Appointments</asp:HyperLink>
                    </td>
                    <td width="35%">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/CPA/ViewPastAppointment.aspx">View Past Appointments</asp:HyperLink>
                    </td>
                    <td width="30%">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/CPA/UserProfile.aspx">Account Settings</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False" AllowPaging="True"
               AllowSorting="True" OnPageIndexChanging="gvAppointments_PageIndexChanging"
                OnSorting="gvAppointments_Sorting" OnRowDataBound="gvAppointments_RowDataBound"
                OnDataBound="gvAppointments_DataBound" >
                <PagerSettings Mode="Numeric" Position="TopAndBottom" NextPageText="&gt;" PreviousPageText="&lt;">
                </PagerSettings>
                <PagerStyle BackColor="LightBlue" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                <Columns >
              
                   <%-- <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" Font-Size="Medium"
                                            Text='<%# Bind("CompanyName")%>' NavigateUrl='<%# "~/CPA/CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>'></asp:HyperLink>
                                        <asp:Label ID="lblCPAID" runat="server" Text='<%# Bind("CPAID")%>' Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="3">
                                        <a href='<%# "CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>'>
                                            <asp:Image ID="Image2" Height="150" Width="150" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'>
                                            </asp:Image></a>
                                        <%--<a href='<%# "CPADetails.aspx" %>'> <asp:Image ID="Image1" Height="200" Width="250" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'  ></asp:Image></a>--%>
                                    <%--</td>
                                </tr>
                              
                                <tr>
                                    <td>
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
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    --%>
                    <%--<asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="450px"  SortExpression="Make" ControlStyle-Width="450px" > <%--ControlStyle-Height="150px" ItemStyle-Height="150px"--%>
                     
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >
                          <ItemTemplate >
                           <%-- <asp:Repeater ID="rptPurposeOfAppointments"  runat="server">
                                <HeaderTemplate>--%>
                            <%--<table border="2px" width="450px"  >--%>
                        <%--</HeaderTemplate>
                                <ItemTemplate>--%>
                                  <%--  <tr >
                                        <td ><br />--%>
                                            
                                                 <table>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" Font-Size="Medium"
                                            Text='<%# Bind("CompanyName")%>' NavigateUrl='<%# "~/CPA/CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>'></asp:HyperLink>
                                        <asp:Label ID="lblCPAID" runat="server" Text='<%# Bind("CPAID")%>' Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="3">
                                        <a href='<%# "CPADetailsInfo.aspx?CPAID="+Eval("CPAID") %>'>
                                            <asp:Image ID="Image2" Height="150" Width="150" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'>
                                            </asp:Image></a>
                                        <%--<a href='<%# "CPADetails.aspx" %>'> <asp:Image ID="Image1" Height="200" Width="250" runat="server" ImageUrl='<%# Eval("CPAID", "ImageCSharp.aspx?QueryCPAID={0}")%>'  ></asp:Image></a>--%>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td>
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
                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >
                          <ItemTemplate > <%-- </td>
                                        
                                        <td>--%>
                                  <asp:Label ID="Label2" runat="server" Text="Schedule:"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblSchedule" runat="server" Text='<%# Bind("Time")%>'></asp:Label>
                                    <br />
                                    <asp:HyperLink ID="lnkCancel" runat="server" NavigateUrl='<%#"CancelCPAAppointment.aspx?ID=" +Eval("ID") %>' > Cancel&nbsp;  </asp:HyperLink>
                                  
                                   <asp:HyperLink ID="lnkReschedule" runat="server" NavigateUrl='<%# "UserReScheduleAppointment.aspx?CPAID="+Eval("CPAID")+"&Schedule="+Eval("Time") + "&ID=" +Eval("ID") %>' >
                                                 Reschedule </asp:HyperLink>
                                  
                                  
                              <%-- </td>
                                        </tr>--%>
                              </ItemTemplate>
                                            </asp:TemplateField>
                                <%--</ItemTemplate>
                                
                                   <FooterTemplate>--%>
                           <%-- </table>--%>
                     <%--   </FooterTemplate>

                            </asp:Repeater>--%>
                       <%-- </ItemTemplate>--%>

                      
                       <%-- <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                   
                   <%-- <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="400px" ItemStyle-VerticalAlign="Middle" SortExpression="Make">
                        <ItemTemplate>
                            <asp:Repeater ID="rptScheduleAppointments" runat="server">
                              <HeaderTemplate>
                            <table>
                        </HeaderTemplate>

                                <ItemTemplate>
                                    <tr>
                                        
                                        </tr>
                                </ItemTemplate>
                                
                                   <FooterTemplate>
                            </table>
                        </FooterTemplate>

                            </asp:Repeater>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
