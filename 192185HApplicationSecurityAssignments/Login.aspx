<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_192185HApplicationSecurityAssignments.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://www.google.com/recaptcha/api.js?render=6LfbjDsaAAAAAFa7qfslF584HTHVvKtinZcqE08P"></script>

</head>
<body>

    <form id="loginForm" runat="server">

        <fieldset>

            <asp:Table ID="loginTable" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><b>Login</b></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Email</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="login_emailAddress" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Password</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="login_password" runat="server" TextMode="Password"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <asp:Button ID="btnLogin" runat="server" Text="Login" Width="544px" OnClick="btnLogin_Click" />&nbsp;&nbsp;&nbsp;
        <div class="g-recaptcha-response" data-sitekey="6LfbjDsaAAAAAFa7qfslF584HTHVvKtinZcqE08P"></div>
        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
        <br />

        <asp:Label ID="errorMsg" runat="server" Text="Error Message"></asp:Label>
        


        </fieldset>

    </form>

    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfbjDsaAAAAAFa7qfslF584HTHVvKtinZcqE08P', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>

</body>
</html>
