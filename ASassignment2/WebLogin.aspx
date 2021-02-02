<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebLogin.aspx.cs" Inherits="ASassignment2.WebLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
     <script>
        <!-- Client side captcha! -->
        grecaptcha.ready(function () {
            grecaptcha.execute('6LdIB0QaAAAAAI_bImCEzOAeyeVoElU9KTxDWLNn', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
<body>
    <form id="form1" runat="server">
        <div>
            
            <br />
            <table style="width:100%;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: center;">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/WebLogin.aspx">Log In</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WebRegister.aspx">Register</asp:HyperLink>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="TextBoxPw" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Button ID="Button1" runat="server" Text="Log In" OnClick="Button1_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="errorlabel" runat="server" Text=" "></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
