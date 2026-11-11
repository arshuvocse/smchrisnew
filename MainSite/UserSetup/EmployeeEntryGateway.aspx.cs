using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserSetup_EmployeeEntryGateway : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lb_ExistingEmp_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoEntry.aspx");
    }

    protected void lb_NewEmp_OnClick(object sender, EventArgs e)
    {
        ////Todo Load Grid with Interview Candidate
    }
}