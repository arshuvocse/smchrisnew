using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.Permission_DAL;
using DAL.Report_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_MBSCOKRAppraisalSelfList : System.Web.UI.Page
{

    private  BSCAppraisalFunctionalPartDAL _aFincDal = new BSCAppraisalFunctionalPartDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ReportDAL aReportDal = new ReportDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListItem supervisorItem = radOp.Items.FindByValue("Supervisor");
            if (supervisorItem != null)
            {
                supervisorItem.Enabled = false; // Disables the item
                supervisorItem.Attributes.CssStyle.Add("display", "none"); // Hides the item visually
            }
            GetCompany();
          //  UserPersmissionValidation();
            aReportDal.LoadCompanyDropDownList(ddlCompany);
            ddlCompany.SelectedValue = Session["CompanyId"].ToString();
            
            ddlCompany_OnSelectedIndexChanged(null, null);

            FinancialYearDropDownList_OnSelectedIndexChanged(null, null);

            //DataTable dt = _aFincDal.GetSelfAppraisalList();
          

        }

    }
    private bool CheckStartEndDateExistOrNot(DateTime Start, DateTime End)
    {
      DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();

        bool status = false;
        string COMID = ddlCompany.SelectedValue;

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(Start, End, COMID);

        if (dataTable.Rows.Count > 0)
        {
            FinancialYearDropDownList.SelectedValue = dataTable.Rows[0]["FinancialYearId"].ToString();
            status = true;
        }

        return status;
    }


    


    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
        for (int i = 0; i < userdata.Rows.Count; i++)
        {
            for (int j = 0; j < lchk_Company.Items.Count; j++)
            {
                if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                {
                    lchk_Company.Items[j].Selected = true;


                }
            }
        }
    }

    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    //detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }

    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        LinkButton btn_edit = (LinkButton)gv_JdBoard.Rows[rowID].FindControl("btn_edit");
       
        DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();

        DataTable dataTable = _aFincDal.CheckSetKpiInAppraisal(Convert.ToInt32(EmpInfoId.Value), Convert.ToInt32(FinancialYearId.Value));

        //if (dataTable.Rows.Count > 0)
        //{
        //    aShowMessage.ShowMessageBox("Appraisal Already waiting for Approval", this);
        //    ((LinkButton)gv_JdBoard.Rows[rowID].FindControl("btn_edit")).Visible = false;
        //}
        //else

        Session["MO_ApproverEmpInfoId"] = null;
        Session["MO_ApproverEmpInfoId"] = Session["EmpInfoId"].ToString();
        string Button = "";
        if(btn_edit.Text=="Set BSC")
        {
            Button = "BSC";
        }
        else
        {
            Button = "OKR";

        }
        {
            Session["Status"] = "Add";
            Response.Redirect("MBSCOKRSelfKPI.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "" + "&M=" + Button + "");
        }


       
    }

    protected void btn_eview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        LinkButton btn_edit = (LinkButton)gv_JdBoard.Rows[rowID].FindControl("btn_edit");
        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");
        string Button = "";
        if (btn_edit.Text == "Set BSC")
        {
            Button = "BSC";
        }
        else
        {
            Button = "OKR";

        }

        if (radOp.Items[0].Selected)
        {

            Session["Status"] = "View";

            Response.Redirect("MBSCOKRSelfKPIDtls.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "" + "&M=" + Button + "");

        }
        else
        {

            Session["Status"] = "View";

            Response.Redirect("MBSCOKRSelfKPIDtls.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
   
        }

 }

    protected void gv_JdBoard_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    int i = e.Row.RowIndex;
        //    HiddenField idReport = (HiddenField)e.Row.FindControl("CurrentStatus");
        //    LinkButton a = (LinkButton)e.Row.FindControl("btn_edit");


        //    if (string.IsNullOrEmpty(idReport.Value.Trim()) || idReport.Value == "Return")
        //    {
        //        a.Visible = true;


        //    }
        //    else
        //    {
        //        a.Visible = false;

        //    }


        //}
    
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    EmployeeContractualReportDAL aEmployeeJobLeftEntryDAL = new EmployeeContractualReportDAL();
    protected void btn_print_OnClick(object sender, EventArgs e)
    {   
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        LinkButton btn_edit = (LinkButton)gv_JdBoard.Rows[rowID].FindControl("btn_edit");


        BSCJDDAL _jdDal = new BSCJDDAL();
        string Button = "";
        if (btn_edit.Text == "Set BSC")
        {
            Button = "BSC";
        }
        else
        {
            Button = "OKR";

        }
        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");

        DataTable aTable =
        _jdDal.CheckKepiSetpExist(EmpInfoId.Value, FinancialYearDropDownList.SelectedValue);
        if (aTable.Rows.Count > 0)
        {

            string url = "../Report_UI/BSCKPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearDropDownList.SelectedValue + "&M=" + Button;
            string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

        }
        else
        {

            AlertMessageBoxShow("No Data Found!");


            // Response.Redirect("../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
        }
    }
    protected void radOp_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (FinancialYearDropDownList.SelectedValue != "")
        {


            string selectedValue = radOp.SelectedValue;
            gv_JdBoard.DataSource = null;
            gv_JdBoard.DataBind();

            Session["Status"] = "";
            Session["Status"] = radOp.SelectedValue;
            if (selectedValue == "Own")
            {

                string btnText = "";
                DataTable dt22=new DataTable();
                DataTable dt = _aFincDal.GetAppraisalByKpiPermissionsss(Session["EmpInfoId"].ToString(),
                    FinancialYearDropDownList.SelectedItem.Text, "  and   tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer ", "  ");
                if (dt.Rows.Count > 0)
                {
                    gv_JdBoard.DataSource = dt;
                    gv_JdBoard.DataBind();
                    try { btnText = dt.Rows[0]["OptionInfo"].ToString(); } catch (Exception ex) { }

                }
                else
                {
                    dt22 = _aFincDal.GetAppraisalByKpiPermissionsss(Session["EmpInfoId"].ToString(),
                        FinancialYearDropDownList.SelectedItem.Text, "", "  ");
                    gv_JdBoard.DataSource = dt22;
                    gv_JdBoard.DataBind();
                    try { btnText = dt22.Rows[0]["OptionInfo"].ToString(); } catch (Exception ex) { }
                }


                DataTable dt2 = _aFincDal.GetAppraisalByKpiPermission2FinYear(Session["EmpInfoId"].ToString(), FinancialYearDropDownList.SelectedItem.Text);

                //for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
                //{
                //    ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Text = btnText;
                //    DataTable dt3 = new DataTable();
                //    if (dt.Rows.Count > 0)
                //    {


                //        dt3 =
                //          _aFincDal.GetAppraisalByPermission3(dt.Rows[0]["BSCAppraisalSelfMasterId"].ToString());
                //    }

                //    else
                //    {

                //    }
                //    string EmpID = "";
                //    string Actions = "";
                //    if (dt3.Rows.Count > 0)
                //    {
                //        EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                //        Actions = dt3.Rows[0]["ActionStatus"].ToString();
                //    }

                //    if (gv_JdBoard.DataKeys[i][0].ToString() == "Verified")
                //    {
                //        ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;
                //    }


                //    if (gv_JdBoard.DataKeys[i][0].ToString() == "Approved")
                //    {
                //        ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;
                //    }



                //    if (dt2.Rows.Count > 0 && Actions.ToString() == "Verified" && EmpID != Session["EmpInfoId"].ToString())
                //    {
                //        ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;
                //    }

                //    //if (gv_JdBoard.DataKeys[i][0].ToString() != "Approved" || dt2.Rows.Count == 0)
                //    //{
                //    //    ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;



                //    //}
                //    //if (dt2.Rows.Count != 0 || gv_JdBoard.DataKeys[i][0].ToString() != "Approved")
                //    //{
                //    //    ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = true;
                //    //}
                //    AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();

                //    DataTable dt2255 = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard(Session["EmpInfoId"].ToString(),
                //           "  ");
                //    if (dt2255.Rows.Count > 0)
                //    {
                //        if (dt2.Rows.Count == 0)
                //        {
                //            ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;
                //            ((Label)gv_JdBoard.Rows[i].FindControl("lblExpiere")).CssClass = "alert alert-warning";
                //            ((Label)gv_JdBoard.Rows[i].FindControl("lblExpiere")).Text = "Deadline already Expired!";

                //        }
                //        else
                //        {

                //        }
                //    }

                //}
            }
            else
            {

                DataTable dt = _aFincDal.GetAppraisalByKpiPermissionSupss(Session["EmpInfoId"].ToString(),
                    FinancialYearDropDownList.SelectedValue, " ");

                if (dt.Rows.Count > 0)
                {
                    gv_JdBoard.DataSource = dt;
                    gv_JdBoard.DataBind();






                    //for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
                    //{

                    //    ((LinkButton) gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;



                    //}

                }
                else
                {
                    gv_JdBoard.DataSource = null;
                    gv_JdBoard.DataBind();
                }


            }
        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue!="")
        {
            aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(FinancialYearDropDownList, ddlCompany.SelectedValue);


            if (DateTime.Now != null)
            {


                if (CheckStartEndDateExistOrNot(DateTime.Now, DateTime.Now) == true)
                {

                }

            }
            //if (ddlCompany.SelectedValue=="1")
            //{
            //    FinancialYearDropDownList.SelectedValue = "9";
            //}
            //else
            //{
            //    FinancialYearDropDownList.SelectedValue = "10";
                
            //}
        }
        else
        {
            FinancialYearDropDownList.Items.Clear();
        }
       
    }

    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        radOp_OnSelectedIndexChanged(null, null);
    }
}