using System;
using System.Web.UI;

public partial class DashBoard_UI_ApprovalNotificationHome : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }

        if (Session["LoginName"].ToString() != "50088")
        {
            Response.Redirect("DashBoard.aspx");
        }
    }

    protected void approvalNotificationButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DashBoard.aspx");
    }
}
