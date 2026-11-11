<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultSender.aspx.cs" Inherits="DefaultSender" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Send Email</h2>
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Button ID="btnSendEmail" runat="server" Text="Send Test Email" OnClick="btnSendEmail_Click" />
        </div>
    </form>
</body>
</html>
