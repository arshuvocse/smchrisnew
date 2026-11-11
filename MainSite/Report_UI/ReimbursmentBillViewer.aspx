<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReimbursmentBillViewer.aspx.cs" Inherits="Report_UI_ReimbursmentBillViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Assets/assets/js/jquery-ui/css/no-theme/jquery-ui-1.10.3.custom.min.css" />
    <link rel="stylesheet" href="../Assets/assets/css/font-icons/entypo/css/entypo.css" />
    <link href="../Assets/assets/css/font-icons/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Noto+Sans:400,700,400italic" />
    <link rel="stylesheet" href="../Assets/assets/css/bootstrap.css" />
    <link rel="stylesheet" href="../Assets/assets/css/neon-core.css" />
    <link rel="stylesheet" href="../Assets/assets/css/neon-theme.css" />
    <link rel="stylesheet" href="../Assets/assets/css/neon-forms.css" />
    <link rel="stylesheet" href="../Assets/assets/css/custom.css" />
    <link href="../Assets/main.min.css" rel="stylesheet" />
    <link href="../Assets/Calendar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="box">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <CR:CrystalReportViewer ID="crReportViewer" runat="server" AutoDataBind="true"
                                EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ReuseParameterValuesOnRefresh="True"
                                ToolPanelView="None" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
