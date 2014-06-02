<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeAppointment.aspx.cs" Inherits="CheckIn.CPA.ChangeAppointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <asp:GridView ID="gvCPASearchResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                PageSize="2" AllowSorting="True" OnPageIndexChanging="gvCPASearchResult_PageIndexChanging"
                OnSorting="gvCPASearchResult_Sorting" OnRowDataBound="gvCPASearchResult_RowDataBound"
                OnDataBound="gvCPASearchResult_DataBound">
                <PagerSettings Mode="Numeric" Position="TopAndBottom" NextPageText="&gt;" PreviousPageText="&lt;">
                </PagerSettings>
                <PagerStyle BackColor="LightBlue" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Button ID="btnPrevWeek" runat="server" Text="<<" OnClick="btnPrevWeek_Click" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="Label6" runat="server" Text="Monday"></asp:Label>
                            <br />
                            <asp:Label ID="lblMondayDate" runat="server" Text="1/1/2011"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Repeater ID="rptMondayAppointments" runat="server">
                                <ItemTemplate>
                                    <a href='<%# "BookAppointment.aspx?CPAID="+Eval("CPAID") + "&ScheduledTime=" +Eval("Time") + "&ScheduledDate=" +Eval("DateOnly") + "&ID=" +Eval("ID") %>'>
                                        <%# Eval("Time")%></a>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="lblTuesdayCaption" runat="server" Text="Tuesday"></asp:Label>
                            <br />
                            <asp:Label ID="lblTuesdayDate" runat="server" Text="2/1/2011"></asp:Label>
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="lblWednesdayCaption" runat="server" Text="Wednesday"></asp:Label>
                            <br />
                            <asp:Label ID="lblWednesdayDate" runat="server" Text="3/1/2011"></asp:Label>
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="lblThursdayCaption" runat="server" Text="Thursday"></asp:Label>
                            <br />
                            <asp:Label ID="lblThursdayDate" runat="server" Text="4/1/2011"></asp:Label>
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="lblFridayCaption" runat="server" Text="Friday"></asp:Label>
                            <br />
                            <asp:Label ID="lblFridayDate" runat="server" Text="5/1/2011"></asp:Label>
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="lblSaturdayCaption" runat="server" Text="Saturday"></asp:Label>
                            <br />
                            <asp:Label ID="lblSaturdayDate" runat="server" Text="6/1/2011"></asp:Label>
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Label ID="lblSundayCaption" runat="server" Text="Sunday"></asp:Label>
                            <br />
                            <asp:Label ID="lblSundayDate" runat="server" Text="6/1/2011"></asp:Label>
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                        <HeaderTemplate>
                            <asp:Button ID="btnNextWeek" runat="server" Text=">>" OnClick="btnNextWeek_Click" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
 </asp:Content>
