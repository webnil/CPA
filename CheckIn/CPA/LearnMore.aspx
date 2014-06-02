<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LearnMore.aspx.cs" Inherits="CheckIn.Web_Pages.LearnMore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td>&nbsp;</td>
            <td align="right">
                <asp:Button ID="Button1" runat="server" Text="Sign Up Now" CssClass="buttonPink"
                    OnClick="Button1_Click" />
            </td>
        </tr>
        <tr align="center">
            <td width="60%" colspan="2">
                <asp:Label ID="Label9" runat="server" Text="How it works?" Font-Bold="True" Font-Size="XX-Large" ForeColor="Maroon"></asp:Label>
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td width="40%"></td>
            <td width="70%">
                <ul style="font-size: large; color:maroon">
                    <li>Your clients find you on our site
                    </li>
                    <br />
                    <li>Your clients book an appointment from your schedule
                    </li>
                    <br />
                    <li>Your receive the appointment details
                    </li>
                </ul>
                    <br />
                    <br />

            </td>
        </tr>
    </table>
</asp:Content>
