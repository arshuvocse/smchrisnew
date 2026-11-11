using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalOwnMarkSupApproveView : System.Web.UI.Page
{
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int masterID = int.Parse(Request.QueryString["masterId"]);
            appMaster.Value = masterID.ToString();
            DataTable dt3 = _appPartA.GetAppraisalPartB(Convert.ToInt32(masterID));
            DataTable dt2 = _appPartA.GetApraisalFunctionalByMaster(masterID);
            AppraisalOwnB.DataSource = dt3;
            AppraisalOwnB.DataBind();
            AppraisalOwnA.DataSource = dt2;
            AppraisalOwnA.DataBind();
        }
    }

    protected void Approve_OnClickClick(object sender, EventArgs e)
    {
        bool result = _appPartA.ApproveAppraisalBySup(Convert.ToInt32(Session["EmpInfoId"].ToString()),
                Convert.ToInt32(appMaster.Value), Session["LoginName"].ToString(), remarks.Text.Trim());
        if (result == true)
        {
            Response.Redirect("ApppraisalApprove.aspx");
        }
    }

    protected void Return_OnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(remarks.Text.Trim()))
        {
            aShowMessage.ShowMessageBox("Save The Functional Part First  ", this);
            return;
        }
        else
        {
            bool result = _appPartA.AppraisalRejectBySup(Convert.ToInt32(Session["EmpInfoId"].ToString()),
                Convert.ToInt32(appMaster.Value), Session["LoginName"].ToString(), remarks.Text.Trim());
        }
    }
}