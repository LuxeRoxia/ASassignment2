<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebOutput.aspx.cs" Inherits="ASassignment2.WebOutput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: center;">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/WebLogin.aspx">Log In</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WebRegister.aspx">Register</asp:HyperLink>
                        
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="First Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelFname" runat="server" Text="Fname"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Last Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelLname" runat="server" Text="Lname"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelEmail" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Date of Birth: "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelDOB" runat="server" Text="DOB"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Credit Card Information: "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelCC" runat="server" Text="CC"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="ButtonaaaLogout" runat="server" Text="Log Out" OnClick="ButtonaaaLogout_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
