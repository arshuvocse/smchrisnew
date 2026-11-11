using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;

public partial class Appraisal_AppraisalOwnMarkApprove : System.Web.UI.Page
{
    private AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  DataTable dt = _appDashboard.GetAppraisalDashboardOwn(Convert.ToInt32(Session["EmpInfoId"]));
            AppraisalOwn.DataSource = _appDashboard.GetAppraisalDashboardOwnMarkApprove(Convert.ToInt32(Session["EmpInfoId"]));
            AppraisalOwn.DataBind();
        }

    }

    protected void View_OnClickClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");


        Response.Redirect("AppraisalOwnMarkApproveView.aspx?masterId=" + mastrId.Value + "");
        
    }

   
}