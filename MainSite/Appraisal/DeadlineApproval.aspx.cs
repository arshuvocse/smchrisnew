using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_DeadlineApproval : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private KPIInformationViewDAL _aFincDal = new KPIInformationViewDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();

            LoadInitialDDL();

          //  UserPersmissionValidation();

            //DataTable dt = _aFincDal.GetSelfAppraisalList();
            //DataTable dt = _aFincDal.GetAppraisalByKpiPermission( );

            //gv_JdBoard.DataSource = dt;
            //gv_JdBoard.DataBind();

        }

    }


    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;

            ddlCompany_OnSelectedIndexChanged(null, null);
        }
        
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

                    detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("AppraisalSelfFunctional.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";



        Response.Redirect("AppraisalSelfFunctional.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void btn_eview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";



        Response.Redirect("KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        DataTable dt = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();

        _aFincDal.LoadDept(ddlDept, ddlCompany.SelectedValue);
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        DataTable dt = _aFincDal.GetDeadlineApproval(Parameter(), Parameter_SP());
     // DataTable dt = _aFincDal.GetEmployeeForKpiSetUpNew((ddlCompany.SelectedValue.ToString()),   Parameter());

        if (dt.Rows.Count>0)
        {
            gv_JdBoard.DataSource = dt;
            gv_JdBoard.DataBind();

            ViewState["EmpSetup"] = dt;
        }

        else
        {
            gv_JdBoard.DataSource = null;
            gv_JdBoard.DataBind();

            ViewState["EmpSetup"] = null;
            ShowMessageBox("No Data Found!!!");
        }

    }

    public string Parameter()
    {
        string param = "";
        if (ddlCompany.Items.Count > 0)
        {
            if (ddlCompany.SelectedIndex > 0)
            {
                param = param + " AND A.CompanyId='" + ddlCompany.SelectedValue + "' ";
            }
        }
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND A.FinYearId='" + ddlFinancialYear.SelectedValue + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }
        if (OperationDropDownList.Items.Count > 0)
        {
            if (OperationDropDownList.SelectedIndex > 0)
            {
                param = param + " AND A.Operation='" + OperationDropDownList.SelectedValue + "' ";
            }
        }
        return param;

    }


    public string Parameter_SP()
    {
        string param = "";
       
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND y.FinancialYearDesc='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }
        if (OperationDropDownList.Items.Count > 0)
        {
            if (OperationDropDownList.SelectedIndex > 0)
            {
                param = param + " AND A.Operation='" + OperationDropDownList.SelectedValue + "' ";
            }
        }
        return param;

    }


    public bool Validation()
    {
        

        Int32 count = 0;

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("txt_check");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please Select at least one employee !!!");
            return false;
        }


        if (gv_JdBoard.Rows.Count==0)
        {
            aShowMessage.ShowMessageBox("Please Select At Least One Row", this);
            return false;
        }




        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {


            var chkBoxRows = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("txt_check");

            if (chkBoxRows.Checked)
            {
                // if (ddlIncrementType.SelectedItem.Text != "Step Adjustment")
                {
                    TextBox extdate = (TextBox)gv_JdBoard.Rows[i].FindControl("txt_appextdate");
                    if (extdate.Text == "")
                    {
                        ShowMessageBox("Extended Must Be Needed !!!");
                        return false;
                    }
                }

            }


        }


        
        return true;
    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {


            for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox) gv_JdBoard.Rows[i].FindControl("txt_check");
                TextBox extdate = (TextBox) gv_JdBoard.Rows[i].FindControl("txt_appextdate");
                if (chk.Checked && !string.IsNullOrEmpty(extdate.Text))
                {
                    HiddenField hfFinancialYearDesc = (HiddenField)gv_JdBoard.Rows[i].FindControl("hfFinancialYearDesc");
                    DeadlineExtendedEntryDAL aDeadlineExtendedEntryDal = new DeadlineExtendedEntryDAL();
                    aDeadlineExtendedEntryDal.UpdateDeadline("Approved", Session["UserId"].ToString(), DateTime.Now,
                        extdate.Text, gv_JdBoard.DataKeys[i][0].ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                    if (gv_JdBoard.DataKeys[i][4].ToString() == "KPI")
                    {
                        DataTable dtkpideadline =
                            aDeadlineExtendedEntryDal.GetKPIDeadlineInfo(gv_JdBoard.DataKeys[i][2].ToString(),
                               hfFinancialYearDesc.Value.ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                        if (dtkpideadline.Rows.Count > 0)
                        {
                            aDeadlineExtendedEntryDal.UpdateKpiDetail(
                                dtkpideadline.Rows[0]["KPIDeadLineDetailsId"].ToString(), extdate.Text);
                        }
                        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("EmpInfoId");
                        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[i].FindControl("FinancialYearId");

                        aDeadlineExtendedEntryDal.UpdateKpiSelfInfoStatus(FinancialYearId.Value, EmpInfoId.Value);

                    }


                   else if (gv_JdBoard.DataKeys[i][4].ToString() == "BSC/OKR")
                    {
                        DataTable dtkpideadline =
                            aDeadlineExtendedEntryDal.GetBSCKPIDeadlineInfo(gv_JdBoard.DataKeys[i][2].ToString(),
                               hfFinancialYearDesc.Value.ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                        if (dtkpideadline.Rows.Count > 0)
                        {
                            aDeadlineExtendedEntryDal.UpdateBSCKpiDetail(
                                dtkpideadline.Rows[0]["KPIDeadLineDetailsId"].ToString(), extdate.Text);
                        }
                        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("EmpInfoId");
                        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[i].FindControl("FinancialYearId");

                        aDeadlineExtendedEntryDal.UpdateBSCKpiSelfInfoStatus(FinancialYearId.Value, EmpInfoId.Value);

                    }
                    else if (gv_JdBoard.DataKeys[i][4].ToString() == "Apprisal")
                    {
                        DataTable dtappdeadline =
                            aDeadlineExtendedEntryDal.GetAPPDeadlineInfo(gv_JdBoard.DataKeys[i][2].ToString(),
                                gv_JdBoard.DataKeys[i][3].ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                        if (dtappdeadline.Rows.Count > 0)
                        {
                            aDeadlineExtendedEntryDal.UpdateAppraisalDetail(
                                dtappdeadline.Rows[0]["AppraisalDeadLineDetailsId"].ToString(), extdate.Text);
                        }
                    }
                    else if (gv_JdBoard.DataKeys[i][4].ToString() == "BSC/OKRApprisal")
                    {
                        DataTable dtappdeadline =
                            aDeadlineExtendedEntryDal.GetOKRBSCAppraisalPDeadlineInfoNew(gv_JdBoard.DataKeys[i][2].ToString(),
                                gv_JdBoard.DataKeys[i][3].ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                        if (dtappdeadline.Rows.Count > 0)
                        {
                            aDeadlineExtendedEntryDal.UpdateBSCAppraisalDetail(
                                dtappdeadline.Rows[0]["BSCAppraisalDeadLineDetailsId"].ToString(), extdate.Text);
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='DeadlineApproval.aspx';",
                        true);
        }
        

    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gv_JdBoard.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked == true ? true : false;

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gv_JdBoard.Rows[i].FindControl("txt_check");
            chk.Checked = result;
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {

        if (Validation())
        {
            for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox) gv_JdBoard.Rows[i].FindControl("txt_check");
                TextBox extdate = (TextBox) gv_JdBoard.Rows[i].FindControl("txt_appextdate");
                if (chk.Checked)
                {
                    DeadlineExtendedEntryDAL aDeadlineExtendedEntryDal = new DeadlineExtendedEntryDAL();
                    aDeadlineExtendedEntryDal.UpdateDeadline("Reject", Session["UserId"].ToString(), DateTime.Now,
                        extdate.Text, gv_JdBoard.DataKeys[i][0].ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                    //if (gv_JdBoard.DataKeys[i][4].ToString() == "KPI")
                    //{
                    //    DataTable dtkpideadline =
                    //        aDeadlineExtendedEntryDal.GetKPIDeadlineInfo(gv_JdBoard.DataKeys[i][2].ToString(),
                    //            gv_JdBoard.DataKeys[i][3].ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                    //    if (dtkpideadline.Rows.Count > 0)
                    //    {
                    //        aDeadlineExtendedEntryDal.UpdateKpiDetail(dtkpideadline.Rows[0]["KPIDeadLineDetailsId"].ToString(), extdate.Text);
                    //    }

                    //}
                    //else if (gv_JdBoard.DataKeys[i][4].ToString() == "Apprisal")
                    //{
                    //    DataTable dtappdeadline =
                    //        aDeadlineExtendedEntryDal.GetAPPDeadlineInfo(gv_JdBoard.DataKeys[i][2].ToString(),
                    //            gv_JdBoard.DataKeys[i][3].ToString(), gv_JdBoard.DataKeys[i][1].ToString());
                    //    if (dtappdeadline.Rows.Count > 0)
                    //    {
                    //        aDeadlineExtendedEntryDal.UpdateAppraisalDetail(dtappdeadline.Rows[0]["AppraisalDeadLineDetailsId"].ToString(), extdate.Text);
                    //    }
                    //}
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='DeadlineApproval.aspx';",
                true);
        }
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DeadlineApproval.aspx");
    }
}