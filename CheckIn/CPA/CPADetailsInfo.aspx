<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CPADetailsInfo.aspx.cs" Inherits="CheckIn.CPA.CPADetailsInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 276px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding-left="10">
        <tr>
            <td style="padding-left: 20px;" width="18%">
                <asp:Label ID="Label4" runat="server" Text="CPA Information" Font-Bold="True" ></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label1" runat="server" Text="Company Name" class="control-label"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblCompanyName" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label2" runat="server" Text="Email"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblEmail" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label>
            </td>


            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblName" Width="40%" Text="First Name" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label7" runat="server" Text="Gender"></asp:Label>
            </td>
            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblGender" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label14" runat="server" Text="Image"></asp:Label>
            </td>
            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Image ID="Image1" runat="server" Height="62px" Width="62px" />
                <%--<asp:FileUpload ID="ImageUpload"   runat="server" Width="257px" />--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label8" runat="server" Text="Phone Number"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblPhoneNumber" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label11" runat="server" Text="Practicing Zip Code"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblZipCode" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label9" runat="server" Text="Office Address Line 1"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblOfficeAddress1" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label10" runat="server" Text="Office Address Line 2"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblOfficeAddress2" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label13" runat="server" Text="Practing City"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblPractingCity" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label12" runat="server" Text="Practing State"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblPractingState" Width="80%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:Label ID="Label17" runat="server" Text="Speciality"></asp:Label>
            </td>

            <td style="padding-left: 20px;" class="auto-style1">
                <asp:Label ID="lblSpeciality" Width="80%" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
