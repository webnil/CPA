<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaUserControl.ascx.cs" Inherits="CheckIn.CaptchaUserControl" %>
<asp:Image ID="Image1"  ImageUrl="~/CPA/GenerateCaptcha.ashx"  runat="server"/>
                            <asp:Button ID="btnReGenerate" runat="server"
                                text=" Regenrate captch" OnClick="btnReGenerate_Click" />