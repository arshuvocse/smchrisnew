using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;

public partial class Appraisal_AppraisalOwnMarkApproveView : System.Web.UI.Page
{
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
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


    protected void Approve_OnClick(object sender, EventArgs e)
    {
        bool result = _appPartA.ApproveAppraisalOwnMark(Convert.ToInt32(appMaster.Value));
        if (result == true)
        {
            AlertMessageBoxShow("Operation Successfull");
            Response.Redirect("AppraisalDashboard.aspx");
        }
        else
        {
            AlertMessageBoxShow("Operation Failed");
        }
    }


    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    
}