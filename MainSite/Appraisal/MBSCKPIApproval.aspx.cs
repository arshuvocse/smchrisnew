using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.InternalCls;

public partial class Appraisal_MBSCKPIApproval : System.Web.UI.Page
{
    private BSCAppraisalFunctionalPartDAL _aFincDal = new BSCAppraisalFunctionalPartDAL();
    ClsApprovalAction approvalAction = new ClsApprovalAction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             DataLoad();

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    public void DataLoad()
    {
        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        string exten = Path.GetExtension(Request.Path);
        if (exten == string.Empty)
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
        }
        else
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        }

        string userName = Session["UserId"].ToString();
        DataLoadByCondition(filepath, userName);
        //approvalAction.LoadActionControlByUser(jobreqRadioButtonList, filepath, userName);

    }

    private void DataLoadByCondition(string pageName, string user)
    {
        DataTable aDataTable = new DataTable();
        string ActionStatus = approvalAction.LoadForApprovalByUserCondition(pageName, user);
        DataTable dt = _aFincDal.GetSelfAppraisalListApprove("", Session["EmpInfoId"].ToString());
        gv_JdBoard.DataSource = dt;
        gv_JdBoard.DataBind();
       
        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("EmpInfoId");

            if (Session["EmpInfoId"].ToString() == EmpInfoId.Value.Trim())
            {
                gv_JdBoard.Rows[i].Visible = false;
            }

            DataTable dt2 = _aFincDal.GetAppraisalByKpiPermission2(EmpInfoId.Value.ToString());
            if (dt2.Rows.Count == 0)
            {
                ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_View")).Visible = false;

                ((Label)gv_JdBoard.Rows[i].FindControl("lblExpireStatus")).Text = "Deadline Already expired.";


            }
            else
            {
                ((Label)gv_JdBoard.Rows[i].FindControl("lblExpireStatus")).Text = "";
                ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_View")).Visible = true;
            }

        }
   
}

    protected void btn_View_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("BSCAppraisalSelfMasterId");


        //Session["ApprovalPage"] = "../Appraisal/AppraisalSupApprove.aspx";

        //Response.Redirect("AppraisalView.aspx?masterId=" + mastrId.Value + "");



        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");
        
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");


      

        
        Session["AppLogId"] = gv_JdBoard.DataKeys[rowID][1].ToString();

        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        Session["AppPage"] = filepath;
        //Session["ForEmpInfoId"] = gv_JdBoard.DataKeys[rowID][2].ToString();
        Session["ForEmpInfoId"] = EmpInfoId.Value;

        var datKey = gv_JdBoard.DataKeys[rowID];
        if (datKey != null)
        {
            string Idd = datKey[0].ToString();
            Session["Status"] = "Edit";
            Session["BSCAppraisalSelfMasterId"] = "";
            Session["BSCAppraisalSelfMasterId"] = Idd;

        }
        Session["AO_ApproverEmpInfoId"] = null;
        Session["AO_ApproverEmpInfoId"] = Session["EmpInfoId"].ToString();

        Response.Redirect("BSCSelfFunctionalApproval.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "" + "&M=" + OptionInfo.Text + "");
    }

}