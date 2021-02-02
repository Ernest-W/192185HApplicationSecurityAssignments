<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuccessLogin.aspx.cs" Inherits="_192185HApplicationSecurityAssignments.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <fieldset>

                <legend>Successful Login Page</legend>
                <p />
                <asp:Label ID="LoginSuccessMessage" runat="server" EnableViewState="False" />
                <p />
                <asp:Button ID="LogOutButton" runat="server" Text="Log Out" OnClick="LogMeOut" Visible="false" />
            </fieldset>

        </div>
    </form>
</body>
</html>
