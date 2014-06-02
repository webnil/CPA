<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CheckIn.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div style="width:900px;margin:auto 200px;">
                <table width="100%" style="margin: auto;">
                <tr valign="middle">
                    <td colspan="4" align="center">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr style="vertical-align:top;">
                    <td align="center" width="25%">
                        <asp:DropDownList ID="ddlState" CssClass="select2" Style="margin-bottom: 7px;" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="15%">
                        <asp:DropDownList ID="ddlCity" CssClass="select2" Style="margin-bottom: 7px;" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="True">
                            <asp:ListItem>Select City</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td class="auto-style1" align="center" width="5%">OR\AND</td>
                    <td width="45%">
                        <input type="text" id="txtZipCode" runat="server" value="" placeholder="Enter Zip Code" >                       
                    </td>
                </tr>

                <tr>
                <td  align="right" width="25%">
                    <asp:CustomValidator ID="CustomValidator2" ForeColor="Red" runat="server" ErrorMessage="" ControlToValidate=""  OnServerValidate="validateFields"></asp:CustomValidator>
                </td>
                <td width="15%"></td>
                <td width="5%"></td>
                <td align="left" width="45%">
                    <asp:CustomValidator ID="CustomValidator1" ForeColor="Red" runat="server" ErrorMessage="Enter correct Zip Code" ControlToValidate="" OnServerValidate="validateZipCode"></asp:CustomValidator>
                </td>                
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <br />
                        <asp:Button ID="btnSearchCPA" runat="server" Text="Search CPA" CssClass="buttonPink" OnClick="btnSearchCPA_Click" />
                        <br />
                </tr>
            </table>
            </div>            
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$='ddlState']").css("width", "150px").select2({

        });

        $("[id$='ddlCity']").css("width", "150px").select2({

        });

        $("[id$='txtZipCode']").css("width", "150px").select2({
        placeholder:'Enter Zip Coding'
        });

    });
</script>
</asp:Content>