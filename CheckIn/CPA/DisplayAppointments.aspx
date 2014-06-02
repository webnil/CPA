<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DisplayAppointments.aspx.cs" Inherits="CheckIn.Web_Pages.DisplayAppointments" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        ul ol {
            margin: 1px 0 0px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">
         
    </script>

    <script type="text/javascript">
        function initialize() {
            <%--var markers = JSON.parse('<%=ConvertDataTabletoString() %>');--%>
            var markers = JSON.parse('<%=GetCPALocations() %>');
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].Latitude, markers[0].Longitude),
            zoom: 5,
            mapTypeId: google.maps.MapTypeId.ROADMAP
            //  marker:true
        };
        var infoWindow = new google.maps.InfoWindow();
        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        for (i = 0; i < markers.length; i++) {
            var data = markers[i]
            var myLatlng = new google.maps.LatLng(data.Latitude, data.Longitude);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.CompanyName
            });
            (function (marker, data) {

                // Attaching a click event to the current marker
                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent(data.Description);
                    infoWindow.open(map, marker);
                });
            })(marker, data);
        }
    }
    </script>

   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
           <%-- <body>--%>

                <div class="breadcrumbs" id="Div1">
                    <ul class="breadcrumb1" style="margin: 1px 0 0px 5px;">
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
                <br />
                <div id="map_canvas" style="width: 500px; height: 400px"></div>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblSerchCriteria" runat="server">text</asp:Label>
                        </td>
                    </tr>
                </table>

                <br />
                <br />
                <div class="page-content">
                    <!--PAGE CONTENT BEGINS-->
                    <div class="page_border">
                        <h1>Book Appointment</h1>
                        <div class="left_corner"></div>
                        <div class="wizard_strip">
                            <ul>
                                <li><a href="#" class="active">Select CPA Appointment</a></li>
                                <li><a href="#">Appointment Information</a></li>
                                <li><a href="#">Personal Information</a></li>
                                <li><a href="#">Finished!</a></li>
                            </ul>
                        </div>

                        <div class="right_corner"></div>
                        <div class="AppintmentContainer">
                            <asp:GridView ID="gvCPASearchResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                PageSize="2" AllowSorting="True"
                                OnRowDataBound="gvCPASearchResult_RowDataBound"
                                OnDataBound="gvCPASearchResult_DataBound" CssClass="tableAppointment" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvCPASearchResult_PageIndexChanging" OnSorting="gvCPASearchResult_Sorting">
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Previous" />
                                <PagerStyle CssClass="gvwCasesPager" />

                                <EmptyDataTemplate>
                                    No CPA available in specified Zip Code or City
                                </EmptyDataTemplate>

                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            &nbsp;&nbsp;&nbsp;<h4>Certified Public Accountant</h4>
                                        </HeaderTemplate>
                                        <ItemTemplate>
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
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" SortExpression="Make">
                                        <HeaderTemplate>
                                            <div class="button">
                                                <%-- <asp:Button ID="btnPrevWeek" CssClass="prev inactive" runat="server" Text="" OnClick="btnPrevWeek_Click"  />--%>
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
                                            <div class="button">
                                                <asp:LinkButton CssClass="next" Text="" runat="server" ID="btnNextWeek" OnClick="btnNextWeek_Click" />
                                            </div>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="AppDayAlternate" />
                                        <ItemStyle CssClass="AppDayAlternate" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="pagination">
                                <a href="#" class="prev inactive">Prev</a>
                                <a href="#" class="next">Next</a>
                            </div>

                        </div>
                    </div>
                </div>
            <%--</body>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
