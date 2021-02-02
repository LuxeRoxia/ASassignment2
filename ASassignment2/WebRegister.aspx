<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebRegister.aspx.cs" Inherits="ASassignment2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=PwTb.ClientID %>').value;
            if (str.length < 8) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password Length Must be at Least 8 Character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too_short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }
            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!";
            document.getElementById("lbl_pwdchecker").style.color = "Blue";
        }
    </script>
    <script>
        <!-- Client side captcha! -->
        grecaptcha.ready(function () {
            grecaptcha.execute('6LdIB0QaAAAAAI_bImCEzOAeyeVoElU9KTxDWLNn', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        }); 
    </script>
        


    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            
            
            <br />
            
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: center;">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/WebLogin.aspx">Log In</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WebRegister.aspx">Register</asp:HyperLink>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: center;">
            
            <asp:Label ID="Label1" runat="server" Text="Register Account" ></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="FNameTb" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label3" runat="server" Text="Last Name"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="LNameTb" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label4" runat="server" Text="Credit Card Info"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="CCITb" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label5" runat="server" Text="Email address"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="EmailTb" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            
            <asp:Label ID="Label6" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="PwTb" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
            <asp:Label ID="lbl_pwdchecker" runat="server" Text="pwdchecker"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label8" runat="server" Text="Confirm Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="CfmPwTb" runat="server" TextMode="Password" ></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            
            <asp:Label ID="Label7" runat="server" Text="Date of Birth"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="DobTb" runat="server" TextMode="Date" ></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Register" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
        </div>
    </form>
</body>
</html>
