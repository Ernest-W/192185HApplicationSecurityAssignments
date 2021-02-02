<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="_192185HApplicationSecurityAssignments.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>

    <script src="https://www.google.com/recaptcha/api.js?render=6LfbjDsaAAAAAFa7qfslF584HTHVvKtinZcqE08P"></script>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=password.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("pwdchecker").innerHTML = "Password Length Must Be At Least 8 Characters";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("too_short")
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_number");
            }

            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password require at least 1 Uppercase Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password require at least 1 Lowercase Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password require at least 1 Uppercase Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }

            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password require at least 1 Special Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }

            document.getElementById("pwdchecker").innerHTML = "Excellent!"
            document.getElementById("pwdchecker").style.color = "Blue";
        }
    </script>

</head>
<body>
    <form id="registrationForm" runat="server">

        <asp:Table ID="registrationTable" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><b>Registration</b></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server">First Name:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="firstName" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server"><asp:RequiredFieldValidator runat="server" ID="fnameRequired" ErrorMessage="First Name is required" ControlToValidate="firstName" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
                <asp:TableCell runat="server"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="First Name is not valid" ControlToValidate="firstName" ForeColor="DarkMagenta" SetFocusOnError="True" ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Last Name:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="lastName" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server"><asp:RequiredFieldValidator runat="server" ID="lnameRequired" ErrorMessage="Last Name is required" ControlToValidate="lastName" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
                <asp:TableCell runat="server"><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Last Name is not valid" ControlToValidate="lastName" ForeColor="DarkMagenta" SetFocusOnError="True" ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Credit Card Information</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="CreditCardInfo" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server"><asp:RequiredFieldValidator runat="server" ID="ccRequired" ErrorMessage="Credit Card Information is required" ControlToValidate="CreditCardInfo" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
                <asp:TableCell runat="server"><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Credit Card Information is not valid" ControlToValidate="CreditCardInfo" ForeColor="DarkMagenta" SetFocusOnError="True" ValidationExpression="^[0-9]\d$"></asp:RegularExpressionValidator></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Email Address</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="emailAddress" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server"><asp:RequiredFieldValidator runat="server" ID="emailRequired" ErrorMessage="Email Address is required" ControlToValidate="emailAddress" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
                <asp:TableCell runat="server"><asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="Email is not valid" ControlToValidate="emailAddress" ForeColor="DarkMagenta" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Password:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="password" runat="server" onkeyup="javascript:validate()" TextMode="Password"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="pwdchecker" runat="server" Text="Password Check"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:RequiredFieldValidator runat="server" ID="passwordRequired" ErrorMessage="Password is required" ControlToValidate="password" ForeColor="DarkMagenta"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Date of Birth:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="birthDate" runat="server" TextMode="Date"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server"><asp:RequiredFieldValidator runat="server" ID="birthDateRequired" ErrorMessage="Date of birth is required" ControlToValidate="birthDate" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
        

        <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" Width="544px" />
        <asp:Label ID="lbl_score" runat="server"></asp:Label>
        <div class="g-recaptcha-response" data-sitekey="6LfbjDsaAAAAAFa7qfslF584HTHVvKtinZcqE08P"></div>

    </form>


    <script>
       grecaptcha.ready(function () {
            grecaptcha.execute('6LfbjDsaAAAAAFa7qfslF584HTHVvKtinZcqE08P', { action: 'Registration' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>


</body>
</html>
