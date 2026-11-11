using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Workflow.Activities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_KPIInformationView : System.Web.UI.Page
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
            UserPersmissionValidation();

            //UserPersmissionValidation();

            //DataTable dt = _aFincDal.GetSelfAppraisalList();
            //DataTable dt = _aFincDal.GetAppraisalByKpiPermission( );

            //gv_JdBoard.DataSource = dt;
            //gv_JdBoard.DataBind();

        }
        try
        {

            gv_JdBoard.UseAccessibleHeader = true;
            gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {

            //throw;
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


                  
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    bool canEdit = Convert.ToBoolean(ViewState["Edit"].ToString());
                    SetGridColumnVisible("Cancel Appraisal Approval", canEdit);
                    SetGridColumnVisible("Draft Info", canEdit);
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

    private void SetGridColumnVisible(string headerText, bool visible)
    {
        foreach (DataControlField column in gv_JdBoard.Columns)
        {
            if (string.Equals(column.HeaderText, headerText, StringComparison.OrdinalIgnoreCase))
            {
                column.Visible = visible;
                break;
            }
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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

        if (FinancialYearId == null || string.IsNullOrWhiteSpace(FinancialYearId.Value))
        {
            aShowMessage.ShowMessageBox("No Data Found!", this);
            return;
        }

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";

        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");

        string url = "";
        if (OptionInfo.Text == "BSC" || OptionInfo.Text == "OKR")
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('BSCOKRDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "" + "&M=" + OptionInfo.Text + "' ,'_blank');", true);
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "' ,'_blank');", true);
        }

           
    }


    protected void btn_print_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");

        string url = "";
        if (OptionInfo.Text == "BSC" || OptionInfo.Text == "OKR")
        {
            url = "../Report_UI/BSCKPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "&M=" + OptionInfo.Text;
        }
        else
        {



            url = "../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value;
        }



        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
       
       // Response.Redirect("../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void btn_midYearPrint_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");

        string url = "";
        if (OptionInfo.Text == "BSC" || OptionInfo.Text == "OKR")
        {
            url = "../Report_UI/MOKRBSCInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "&M=" + OptionInfo.Text;
        }
        else
        {
            url = "../Report_UI/KPIMidYearInformationView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value;
        }

        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void btn_view_OnClick(object sender, EventArgs e)
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


        //Emp

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            using (DataTable dt2 = _commonDataLoad.GetDDLComDepartmentFor52409(ddlCompany.SelectedValue))
            {
                ddlDept.DataSource = dt2;
                ddlDept.DataValueField = "Value";
                ddlDept.DataTextField = "TextField";
                ddlDept.DataBind();
            }
        }
        else
        {
            using (DataTable dt2 = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDept.DataSource = dt2;
                ddlDept.DataValueField = "Value";
                ddlDept.DataTextField = "TextField";
                ddlDept.DataBind();
            }
        }

           
        using (DataTable dt33 = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
        {
            ddlDivision.DataSource = dt33;
            ddlDivision.DataValueField = "Value";
            ddlDivision.DataTextField = "TextField";
            ddlDivision.DataBind();
        }

            try
            {
                if (Session["EmpInfoId"].ToString() == "3920")
                {
                    ddlDivision.SelectedValue = "38";

                    ddlDivision.Enabled = false;
                }
            }
            catch
            {

            }
        }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        try
        {

            if (ddl_options.SelectedValue == "0")
            {
                gv_JdBoard.DataSource = null;
                gv_JdBoard.DataBind();
                ViewState["EmpSetup"] = null;
                aShowMessage.ShowMessageBox("Please select Operation!", this);
                ddl_options.Focus();
            }
            else
            {
                DataTable dt = _aFincDal.GetAppraisalByKpiPermission_New__(ddlCompany.SelectedValue.ToString(), Parameter(), "    AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ(),

               ParameterOKR(), " AND  (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ()
               , ddl_options.SelectedValue, ddlDisagree.SelectedValue);

                if (dt.Rows.Count > 0)
                {
                    gv_JdBoard.DataSource = dt;
                    gv_JdBoard.DataBind();

                    Label HTActionStatus = (Label)gv_JdBoard.HeaderRow.FindControl("HTActionStatus");
                    Label HTAwaitingActionStatus = (Label)gv_JdBoard.HeaderRow.FindControl("HTAwaitingActionStatus");
                    HTActionStatus.Text = ddl_options.SelectedItem.Text + " Approval Status";
                    HTAwaitingActionStatus.Text = ddl_options.SelectedItem.Text + " Awaiting Employee";

                    gv_JdBoard.UseAccessibleHeader = true;
                    gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;

                    //if (ddl_options.SelectedItem.Text == "KPI")
                    //{
                    //    gv_JdBoard.Columns[11].Visible = false;
                    //    gv_JdBoard.Columns[12].Visible = false;
                    //}
                    //else
                    //{
                    //    gv_JdBoard.Columns[11].Visible = true;
                    //    gv_JdBoard.Columns[12].Visible = true;
                    //}

                    ViewState["EmpSetup"] = dt;
                }
                else
                {
                    DataTable dt3 = _aFincDal.GetAppraisalByKpiPermission_New__(ddlCompany.SelectedValue.ToString(), Parameter2(), "  AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    )  " + Parameter_JJJ()
                        , Parameter2OKR(), "  AND  (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    )  " + Parameter_JJJ(), ddl_options.SelectedValue, ddlDisagree.SelectedValue
                        );

                    try
                    {
                        gv_JdBoard.DataSource = dt3;
                        gv_JdBoard.DataBind();
                        Label HTActionStatus = (Label)gv_JdBoard.HeaderRow.FindControl("HTActionStatus");
                        Label HTAwaitingActionStatus = (Label)gv_JdBoard.HeaderRow.FindControl("HTAwaitingActionStatus");
                        HTActionStatus.Text = ddl_options.SelectedItem.Text + " Approval Status";
                        HTAwaitingActionStatus.Text = ddl_options.SelectedItem.Text + " Awaiting Employee";
                        gv_JdBoard.UseAccessibleHeader = true;
                        gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
                        gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;

                        //if (ddl_options.SelectedItem.Text == "KPI")
                        //{
                        //    gv_JdBoard.Columns[11].Visible = false;
                        //    gv_JdBoard.Columns[12].Visible = false;
                        //}
                        //else
                        //{
                        //    gv_JdBoard.Columns[11].Visible = true;
                        //    gv_JdBoard.Columns[12].Visible = true;
                        //}
                        ViewState["EmpSetup"] = dt;
                    }
                    catch (Exception)
                    {

                        gv_JdBoard.DataSource = null;
                        gv_JdBoard.DataBind();
                        ViewState["EmpSetup"] = null;
                    }
                }
            }
            //and  OnlyViewComId=" + ddlCompany.SelectedValuem"
           
            // DataTable dt = _aFincDal.GetEmployeeForKpiSetUpNew((ddlCompany.SelectedValue.ToString()),   Parameter());
          
        }
        catch (Exception)
        {
            gv_JdBoard.DataSource = null;
            gv_JdBoard.DataBind();
            ViewState["EmpSetup"] = null;
            //throw;
        }
    }


    
    public string Parameter()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FYDes_BSCDec='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (ddlDivision.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            param = param + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND    dpt.DepartmentId in( 140, 141)";
        }

        param = param + " AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    )  ";    //AND tblAppraisalMasterAppLog.Version=ALog.MaxVer

        return param;

    }
    public string ParameterOKR()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FYDes_BSCDec='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (ddlDivision.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            param = param + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND    dpt.DepartmentId in( 140, 141)";
        }

        param = param + "  and (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    ) ";    //AND tblAppraisalMasterAppLog.Version=ALog.MaxVer

        return param;

    }

    public string Parameter_JJJ()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FYDes_BSCDec='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (ddlDivision.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            param = param + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND    dpt.DepartmentId in( 140, 141)";
        }

        return param;

    }

    public string Parameter2()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FYDes_BSCDec='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (ddlDivision.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            param = param + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND (dpt.DepartmentName LIKE '%Sales%' OR dpt.DepartmentName  LIKE '%Marketing%')";
        }

         param = param + "  and (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    )  ";
       // param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";
       // param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";

        return param;

    }
    
    public string Parameter2OKR()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FYDes_BSCDec='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (ddlDivision.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            param = param + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND (dpt.DepartmentName LIKE '%Sales%' OR dpt.DepartmentName  LIKE '%Marketing%')";
        }

         param = param + "  and (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    )  ";
       // param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";
       // param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";

        return param;

    }


    protected void gv_JdBoard_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="A")
        {
            
        }
    }

   

    protected void btn_KPI_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HFOperationInfo.Value = "";
        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");
        
        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");

        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");
        Label ActionStatus = (Label)gv_JdBoard.Rows[rowID].FindControl("ActionStatus");
        HFOperationInfo.Value = OptionInfo.Text;
        if (OptionInfo.Text == "BSC" || OptionInfo.Text == "OKR")
        {
            isKPI.Checked = false;

            if (ActionStatus.Text.Trim() == "Approved")
            {
                aShowMessage.ShowMessageBox("Already Approved!!", this);
            }
            else if (ActionStatus.Text.Trim() == "Not Approved")
            {
                mpFunctionalSup.Show();
                isKPI.Checked = true;

                using (DataTable dt = _commonDataLoad.GetEmpDDLALLwwwwwww(ddlCompany.SelectedValue.ToString())
                    )
                {



                    ddlEmpInfo.DataSource = dt;
                    ddlEmpInfo.DataValueField = "EmpInfoId";
                    ddlEmpInfo.DataTextField = "EmpName";
                    ddlEmpInfo.DataBind();
                    ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfo.SelectedIndex = 0;
                }
                using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString())
                  )
                {

                    ddlForwordEmp.DataSource = dt;
                    ddlForwordEmp.DataValueField = "EmpInfoId";
                    ddlForwordEmp.DataTextField = "EmpName";
                    ddlForwordEmp.DataBind();
                    ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlForwordEmp.SelectedIndex = 0;
                }

                DataTable dtapprove = _aFincDal.GetKPIApprovaerBSCOKR(Convert.ToInt32(hfAppraisalSelfMasterId.Value));
                if (dtapprove.Rows.Count > 0)
                {
                    try
                    {
                        hfMainId.Value = hfAppraisalSelfMasterId.Value;
                        hfMasterId.Value = dtapprove.Rows[0]["BSCAppraisalSelfAppLogId"].ToString();

                        ddlEmpInfo.SelectedValue = dtapprove.Rows[0]["ForEmpInfoId"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
                lblHeader.InnerText = OptionInfo.Text + " Reset Approval Person";

            }
            else
            {
                aShowMessage.ShowMessageBox(OptionInfo.Text + " has not set yet", this);

            }

        }
        else
        {

            isKPI.Checked = false;

            if (ActionStatus.Text.Trim() == "Approved")
            {
                aShowMessage.ShowMessageBox("Already Approved!!", this);
            }
            else if (ActionStatus.Text.Trim() == "Not Approved")
            {
                mpFunctionalSup.Show();
                isKPI.Checked = true;

                using (DataTable dt = _commonDataLoad.GetEmpDDLALLwwwwwww(ddlCompany.SelectedValue.ToString())
                    )
                {



                    ddlEmpInfo.DataSource = dt;
                    ddlEmpInfo.DataValueField = "EmpInfoId";
                    ddlEmpInfo.DataTextField = "EmpName";
                    ddlEmpInfo.DataBind();
                    ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfo.SelectedIndex = 0;
                }
                using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString())
                  )
                {

                    ddlForwordEmp.DataSource = dt;
                    ddlForwordEmp.DataValueField = "EmpInfoId";
                    ddlForwordEmp.DataTextField = "EmpName";
                    ddlForwordEmp.DataBind();
                    ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlForwordEmp.SelectedIndex = 0;
                }

                DataTable dtapprove = _aFincDal.GetKPIApprovaer(Convert.ToInt32(hfAppraisalSelfMasterId.Value));
                if (dtapprove.Rows.Count > 0)
                {
                    try
                    {
                        hfMainId.Value = hfAppraisalSelfMasterId.Value;
                        hfMasterId.Value = dtapprove.Rows[0]["AppraisalSelfAppLogId"].ToString();

                        ddlEmpInfo.SelectedValue = dtapprove.Rows[0]["ForEmpInfoId"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
                lblHeader.InnerText = " KPI Reset Approval Person";

            }
            else
            {
                aShowMessage.ShowMessageBox("KPI has not set yet", this);

            }
        }


           
    }

    protected void btnApprisal_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");


        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");

        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");

        Label Awaitingf = (Label)gv_JdBoard.Rows[rowID].FindControl("Awaitingf");

        isKPI.Checked = false;
        if (Awaitingf.Text.Trim() == "Approved")
        {
            aShowMessage.ShowMessageBox("Already Approved!!", this);
        }
        else if (Awaitingf.Text.Trim() == "Not Approved")
        {

            HFOperationInfo.Value = OptionInfo.Text;

            isKPI.Checked = false;



            using (DataTable dt = _commonDataLoad.GetEmpDDLALLwwwwwww(ddlCompany.SelectedValue.ToString())
                )
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }

            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString())
                )
            {



                ddlForwordEmp.DataSource = dt;
                ddlForwordEmp.DataValueField = "EmpInfoId";
                ddlForwordEmp.DataTextField = "EmpName";
                ddlForwordEmp.DataBind();
                ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlForwordEmp.SelectedIndex = 0;
            }

            DataTable dtapprove = _aFincDal.GetApprisalApprovaerNew(Convert.ToInt32(hfAppraisalMasterId.Value), OptionInfo.Text);
              if (dtapprove.Rows.Count > 0)
            {
                try
                {
                    ddlEmpInfo.SelectedValue = dtapprove.Rows[0]["ForEmpInfoId"].ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                hfMainId.Value = hfAppraisalMasterId.Value;

                hfMasterId.Value = dtapprove.Rows[0]["AppraisalMasterAppLogId"].ToString();
            }
              lblHeader.InnerText = " Appraisal Reset Approval Person";  
            mpFunctionalSup.Show();

        }
        else
        {
            aShowMessage.ShowMessageBox("Appraisal has not set yet", this);
            
        }
    }

    protected void btnFunctionalSupClose_Click(object sender, EventArgs e)
    {
      mpFunctionalSup.Hide();
    }

    protected void btnAppraisalFuncSUPSave_OnClick(object sender, EventArgs e)
    {
        if (ddlEmpInfo.SelectedValue != "")
        {
        if (ddlForwordEmp.SelectedValue!="")
        {
            bool status = false;

                if(HFOperationInfo.Value=="BSC" || HFOperationInfo.Value == "OKR")
                {
                    if (isKPI.Checked)
                    {
                        try
                        {
                            DataTable dtKPISamePer = _aFincDal.GetKPIIfSamePersonBSC(Convert.ToInt32(hfMainId.Value));

                            if (dtKPISamePer.Rows.Count > 0 && ddlForwordEmp.SelectedValue == dtKPISamePer.Rows[0]["EmpInfoId"].ToString())
                            {
                                status = _aFincDal.UpdateKPIApprovePersonContracturalSameBSC(hfMasterId.Value, ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue, hfMainId.Value);
                            }
                            else
                            {
                                status = _aFincDal.UpdateKPIApprovePersonContracturalBSC(hfMasterId.Value, ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue);

                            }
                        }
                        catch (Exception)
                        {

                            status = false;
                        }
                    }
                    else
                    {

                        try
                        {
                            DataTable dtKPISamePer = _aFincDal.GetAppraisalIfSamePersonBSC(Convert.ToInt32(hfMainId.Value));

                            if (dtKPISamePer.Rows.Count > 0 && ddlForwordEmp.SelectedValue == dtKPISamePer.Rows[0]["EmpInfoId"].ToString())
                            {
                                status = _aFincDal.UpdateAppprisalApprovePersonContracturalSamePerBSC(hfMasterId.Value,
                                    ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue, hfMainId.Value);
                            }
                            else
                            {
                                status = _aFincDal.UpdateAppprisalApprovePersonContracturalBSC(hfMasterId.Value,
                                    ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue);
                            }
                        }
                        catch (Exception)
                        {

                            status = false;
                        }

                    }
                }
                else
                {

                
                    if (isKPI.Checked)
            {
                try
                {
                    DataTable dtKPISamePer = _aFincDal.GetKPIIfSamePerson(Convert.ToInt32(hfMainId.Value));

                    if (dtKPISamePer.Rows.Count > 0 && ddlForwordEmp.SelectedValue == dtKPISamePer.Rows[0]["EmpInfoId"].ToString())
                    {
                        status = _aFincDal.UpdateKPIApprovePersonContracturalSame(hfMasterId.Value, ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue, hfMainId.Value);
                    }
                    else
                    {
                        status = _aFincDal.UpdateKPIApprovePersonContractural(hfMasterId.Value, ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue);

                    }
                }
                catch (Exception)
                {

                    status = false;
                }
            }
            else
            {

                try
                {
                    DataTable dtKPISamePer = _aFincDal.GetAppraisalIfSamePerson(Convert.ToInt32(hfMainId.Value));

                    if (dtKPISamePer.Rows.Count > 0 && ddlForwordEmp.SelectedValue == dtKPISamePer.Rows[0]["EmpInfoId"].ToString())
                    {
                        status = _aFincDal.UpdateAppprisalApprovePersonContracturalSamePer(hfMasterId.Value,
                            ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue, hfMainId.Value);
                    }
                    else
                    {
                        status = _aFincDal.UpdateAppprisalApprovePersonContractural(hfMasterId.Value,
                            ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue);
                    }
                }
                catch (Exception)
                {

                    status = false;
                }

            }
                   }
            if (status)
            {

                aShowMessage.ShowMessageBox("Operation Successfully...",this);

                SearchButton_OnClick(null, null);
                mpFunctionalSup.Hide();
            }
            else
            {

                aShowMessage.ShowMessageBox("Something went wrong!", this);
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Forward Approval Person", this);
            ddlForwordEmp.Focus();
        }

        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Present Approval Person", this);
            ddlEmpInfo.Focus();
        }
        HFOperationInfo.Value = "";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'CheckupGridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (ddl_options.SelectedValue == "KPI")
        {

       
        if (gv_JdBoard.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Employee_kpi_Appraisal_List_Info.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

                gv_ExcelKPI.AllowPaging = false;

            DataTable dt = _aFincDal.GetAppraisalByKpiPermission_New__(ddlCompany.SelectedValue.ToString(), Parameter(), " AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ(),

                 ParameterOKR(), " AND  (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ()
                 , ddl_options.SelectedValue, ddlDisagree.SelectedValue);

            if (dt.Rows.Count > 0)
            {

              
                gv_ExcelKPI.DataSource = dt;
                    gv_ExcelKPI.DataBind();
                    gv_ExcelKPI.UseAccessibleHeader = true;
                    gv_ExcelKPI.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gv_ExcelKPI.FooterRow.TableSection = TableRowSection.TableFooter;

 
            }
            else
            {
                DataTable dt3 = _aFincDal.GetAppraisalByKpiPermission_New__(ddlCompany.SelectedValue.ToString(), Parameter2(), " AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ()
                   , Parameter2OKR(), " AND  (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ(), ddl_options.SelectedValue, ddlDisagree.SelectedValue
                   );

                try
                {
                        gv_ExcelKPI.DataSource = dt3;
                        gv_ExcelKPI.DataBind();
                        gv_ExcelKPI.UseAccessibleHeader = true;
                        gv_ExcelKPI.HeaderRow.TableSection = TableRowSection.TableHeader;
                        gv_ExcelKPI.FooterRow.TableSection = TableRowSection.TableFooter;

 
                }
                catch (Exception)
                {

                        gv_ExcelKPI.DataSource = null;
                        gv_ExcelKPI.DataBind();
                  
                }
            }


            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
                gv_ExcelKPI.Parent.Controls.Add(frm);
                //frm.Attributes["runat"] = "server";
                //frm.Controls.Add(loadGridView);
                //frm.RenderControl(htw);

                gv_ExcelKPI.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_ExcelKPI.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_ExcelKPI.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }
            string com = "";
            if(ddlCompany.SelectedValue=="1")
            {
                com = "Social Marketing Company";
            }
            if (ddlCompany.SelectedValue == "2")
            {
                com = "SMC Enterprise Ltd.";
            }
                gv_ExcelKPI.RenderControl(htw);
            string headerTable = @"<span   style='text-align:center'><h3>" + com +
                                 " </h3><h4>Employee KPI & Appraisal List</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
                                 DateTime.Now.ToString("dd-MMM-yyyy") + "</h5></span>";

           

            HttpContext.Current.Response.Write(headerTable);
           
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
        }
        else
        {
            if (gv_JdBoard.Rows.Count > 0)
            {
                string attachment = "attachment; filename=Employee_kpi_Appraisal_List_Info.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                gv_Excel.AllowPaging = false;

                DataTable dt = _aFincDal.GetAppraisalByKpiPermission_New__(ddlCompany.SelectedValue.ToString(), Parameter(), " and  AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ(),

                     ParameterOKR(), " AND  (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ()
                     , ddl_options.SelectedValue, ddlDisagree.SelectedValue);

                if (dt.Rows.Count > 0)
                {


                    gv_Excel.DataSource = dt;
                    gv_Excel.DataBind();
                    gv_Excel.UseAccessibleHeader = true;
                    gv_Excel.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gv_Excel.FooterRow.TableSection = TableRowSection.TableFooter;


                }
                else
                {
                    DataTable dt3 = _aFincDal.GetAppraisalByKpiPermission_New__(ddlCompany.SelectedValue.ToString(), Parameter2(), " AND  (tblAppraisalSelfAppLog.Version is null or tblAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ()
                       , Parameter2OKR(), " AND  (tblBSCAppraisalSelfAppLog.Version is null or tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer    ) " + Parameter_JJJ(), ddl_options.SelectedValue, ddlDisagree.SelectedValue
                       );

                    try
                    {
                        gv_Excel.DataSource = dt3;
                        gv_Excel.DataBind();
                        gv_Excel.UseAccessibleHeader = true;
                        gv_Excel.HeaderRow.TableSection = TableRowSection.TableHeader;
                        gv_Excel.FooterRow.TableSection = TableRowSection.TableFooter;


                    }
                    catch (Exception)
                    {

                        gv_Excel.DataSource = null;
                        gv_Excel.DataBind();

                    }
                }


                // Create a form to contain the grid  
                HtmlForm frm = new HtmlForm();
                gv_Excel.Parent.Controls.Add(frm);
                //frm.Attributes["runat"] = "server";
                //frm.Controls.Add(loadGridView);
                //frm.RenderControl(htw);

                gv_Excel.HeaderRow.Style.Add("background-color", "#E5EEF1");

                // Set background color of each cell of GridView1 header row
                foreach (TableCell tableCell in gv_Excel.HeaderRow.Cells)
                {
                    tableCell.Style["background-color"] = "#E5EEF1";
                }

                // Set background color of each cell of each data row of GridView1
                foreach (GridViewRow gridViewRow in gv_Excel.Rows)
                {
                    gridViewRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                    {
                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                    }
                }
                string com = "";
                if (ddlCompany.SelectedValue == "1")
                {
                    com = "Social Marketing Company";
                }
                if (ddlCompany.SelectedValue == "2")
                {
                    com = "SMC Enterprise Ltd.";
                }
                gv_Excel.RenderControl(htw);
                string headerTable = @"<span   style='text-align:center'><h3>" + com +
                                     " </h3><h4>Employee KPI & Appraisal List</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
                                     DateTime.Now.ToString("dd-MMM-yyyy") + "</h5></span>";



                HttpContext.Current.Response.Write(headerTable);

                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!", this);
            }
        }
    }
    protected void gv_JdBoard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        DataRowView drv = e.Row.DataItem as DataRowView;
        if (drv == null)
        {
            return;
        }

        e.Row.Attributes["data-empinfoid"] = drv["EmpInfoId"] == DBNull.Value ? "" : Convert.ToString(drv["EmpInfoId"]);
        e.Row.Attributes["data-financialyearid"] = drv["FinancialYearId"] == DBNull.Value ? "" : Convert.ToString(drv["FinancialYearId"]);
        e.Row.Attributes["data-optioninfo"] = drv["OptionInfo"] == DBNull.Value ? "" : Convert.ToString(drv["OptionInfo"]);
        e.Row.Attributes["data-financialyeardesc"] = drv["FinancialYearDesc"] == DBNull.Value ? "" : Convert.ToString(drv["FinancialYearDesc"]);
        e.Row.Attributes["data-empcode"] = drv["EmpMasterCode"] == DBNull.Value ? "" : Convert.ToString(drv["EmpMasterCode"]);
        e.Row.Attributes["data-empname"] = drv["EmpllName"] == DBNull.Value ? "" : Convert.ToString(drv["EmpllName"]);
    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (gv.ID == "gv_JdBoard" && gv.HeaderRow != null)
        {
            CheckBox chkSelectAll = (CheckBox)gv.HeaderRow.FindControl("chkSelectAll");
            if (chkSelectAll != null)
            {
                chkSelectAll.Attributes["onclick"] =
                    "var cbs = document.getElementsByClassName('pdfRowCheckBox'); for (var i = 0; i < cbs.length; i++) { cbs[i].checked = this.checked; }";
            }
        }
    }
    protected void btnApprisalCancel_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");


        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");

        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");

        Label Awaitingf = (Label)gv_JdBoard.Rows[rowID].FindControl("Awaitingf");
        Label emeeployee = (Label)gv_JdBoard.Rows[rowID].FindControl("emeeployee");
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");

       
        if (Awaitingf.Text.Trim() == "Approved")
        {

            cnPoplblHeadeInfo.Text = OptionInfo.Text;
            hfOptionType_Cnl.Value = OptionInfo.Text;
            hfEmpInfoId_Cnl.Value = EmpInfoId.Value;
            hfMasterId.Value =hfAppraisalMasterId.Value;


            lblpop_forCancelInfo.Text = "<span style='font-weight:bold; color:Black; font-size:18px;'>Do you want to cancel and send the draft "
     + "<span style='font-weight:bold; color:Green; font-size:18px;'>"+ OptionInfo.Text+ "</span>"
     + " to</span> <span style='font-weight:bold; color:blue; font-size:22px;'>"
     + emeeployee.Text
     + "</span>";

            DataTable dtapprove = new DataTable();

          DataTable dt = _aFincDal.GetApprisalApprovaerNameTop(Convert.ToInt32(hfAppraisalMasterId.Value));
            if (dt.Rows.Count > 0)
            {
                gv_Versions.DataSource = dt;
                gv_Versions.DataBind();
            }
            else
            {
                gv_Versions.DataSource = null;
                gv_Versions.DataBind();
            }

            hfMainAppraisalMasterId.Value = hfAppraisalMasterId.Value;
            //if (cnPoplblHeadeInfo.Text == "KPI")
            //{

            //      dtapprove = _aFincDal.GetApprisalApprovaer(Convert.ToInt32(hfAppraisalMasterId.Value));
            //    if (dtapprove.Rows.Count > 0)
            //    {

            //        hfMasterId.Value = dtapprove.Rows[0]["AppraisalMasterAppLogId"].ToString();
            //    }
            //}
            //else
            //{
            //      dtapprove = _aFincDal.GetApprisalApprovaerOKR(Convert.ToInt32(hfAppraisalMasterId.Value));
            //    if (dtapprove.Rows.Count > 0)
            //    {

            //        hfMasterId.Value = dtapprove.Rows[0]["BSCAppraisalMasterAppLogId"].ToString();
            //    }
            //}


            //try
            //{
            //    using (DataTable dtEmp = _commonDataLoad.EmpforCancelApproveAppraisal(hfAppraisalMasterId.Value.ToString())
            //  )
            //    {
            //        hfMainAppraisalMasterId.Value = hfAppraisalMasterId.Value;


            //        ddlEmpforCancelApproveAppraisal.DataSource = dtEmp;
            //        ddlEmpforCancelApproveAppraisal.DataValueField = "EmpId";
            //        ddlEmpforCancelApproveAppraisal.DataTextField = "empName";
            //        ddlEmpforCancelApproveAppraisal.DataBind();
            //        ddlEmpforCancelApproveAppraisal.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            //        ddlEmpforCancelApproveAppraisal.SelectedIndex = 0;
            //    }

            //}
            //catch
            //{

            //}

            MPAppraisalApproval.Show();
        }
        
        else
        {
            aShowMessage.ShowMessageBox("Appraisal Approval Status must be Approved", this);

        }
    }

    protected void btnApprasalResetClose_Click(object sender, EventArgs e)
    {
        MPAppraisalApproval.Hide();

    }
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    protected void lbCancelAppraisalSubmit_OnClick(object sender, EventArgs e)
    {
        //if (ddlEmpforCancelApproveAppraisal.SelectedValue != "")
        {
            bool status = false;

          

            //OKR


            int idd = 0;
            if (hfOptionType_Cnl.Value=="OKR" || hfOptionType_Cnl.Value == "BSC")
            {


                status = _aFincDal.KPIDelPreviosuLog(hfMainAppraisalMasterId.Value.ToString(), hfMasterId.Value, ddlEmpforCancelApproveAppraisal.SelectedValue, hfOptionType_Cnl.Value);


                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                appLogDao.ActionStatus = "Drafted";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                appLogDao.ForEmpInfoId = Convert.ToInt32(hfEmpInfoId_Cnl.Value);
                appLogDao.AppraisalSelfMasterId = Convert.ToInt32(hfMainAppraisalMasterId.Value);
                appLogDao.Comments = "Draft From HR";
                appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                  idd = _appPartA.SaveEmpAppLogOKRBSC(appLogDao);
            }
            
            if(hfOptionType_Cnl.Value=="KPI" )
            {


                status = _aFincDal.KPIDelPreviosuLog(hfMainAppraisalMasterId.Value.ToString(), hfMasterId.Value, ddlEmpforCancelApproveAppraisal.SelectedValue, hfOptionType_Cnl.Value);
                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                appLogDao.ActionStatus = "Drafted";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                appLogDao.ForEmpInfoId = Convert.ToInt32(hfEmpInfoId_Cnl.Value);
                appLogDao.AppraisalSelfMasterId = Convert.ToInt32(hfMainAppraisalMasterId.Value);
                appLogDao.Comments = "Draft From HR";
                appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                idd = _appPartA.SaveEmpAppLogKPI(appLogDao);
            }

            


            if (idd>0)
            {
                aShowMessage.ShowMessageBox("Operation Successfully...", this);

                SearchButton_OnClick(null, null);
                MPAppraisalApproval.Hide();
            }
        }
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Select Forward Approval Person", this);
        //    ddlEmpforCancelApproveAppraisal.Focus();
        //}
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0" && ddlDivision.SelectedValue != "-1")
        {

            if (Session["EmpInfoId"].ToString() == "3920")
            {
                _commonDataLoad.GetDepartmentByDivListFor52409(ddlDept, ddlDivision.SelectedValue);
            }
            else
            {
                _commonDataLoad.GetDepartmentByDivList(ddlDept, ddlDivision.SelectedValue);
            }                          
        }
        else
        {
            if (Session["EmpInfoId"].ToString() == "3920")
            {
                using (DataTable dt2 = _commonDataLoad.GetDDLComDepartmentFor52409(ddlCompany.SelectedValue))
                {
                    ddlDept.DataSource = dt2;
                    ddlDept.DataValueField = "Value";
                    ddlDept.DataTextField = "TextField";
                    ddlDept.DataBind();
                }
            }
            else
            {
                using (DataTable dt2 = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
                {
                    ddlDept.DataSource = dt2;
                    ddlDept.DataValueField = "Value";
                    ddlDept.DataTextField = "TextField";
                    ddlDept.DataBind();
                }
            }
        }
    }

    protected void btn_ChangeMarks_Click(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        Label FinancialYearDesc = (Label)gv_JdBoard.Rows[rowID].FindControl("FinancialYearDesc");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");

        if (OptionInfo.Text == "KPI")
        {
            Response.Redirect("KPIApppraisalChangeMarkforHR.aspx?EmpInfoId=" + EmpInfoId.Value + "&FinancialYearDesc=" + FinancialYearDesc.Text + "" + "&M=" + OptionInfo.Text + "");
         
        }

        else
        {
            Response.Redirect("OKRBSCApppraisalForHR.aspx?EmpInfoId=" + EmpInfoId.Value + "&FinancialYearDesc=" + FinancialYearDesc.Text + "" + "&M=" + OptionInfo.Text + "");
        }
    }

    protected void btnDraftKPI_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");
        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");
        bool status;
        int idd = 0;
        if (OptionInfo.Text == "OKR" || OptionInfo.Text == "BSC")
        {


         
            status = _aFincDal.KPIDelPreviosuLogApproved(hfAppraisalMasterId.Value.ToString(), hfAppraisalSelfMasterId.Value, EmpInfoId.Value, FinancialYearId.Value, OptionInfo.Text);


            AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();
            appLogDao.ActionStatus = "Drafted";
            appLogDao.ShowStatus = "Drafted";
            appLogDao.ApproveDate = DateTime.Now;
            appLogDao.ApproveBy = Session["UserId"].ToString();
            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
            appLogDao.ForEmpInfoId = Convert.ToInt32(EmpInfoId.Value);
            appLogDao.AppraisalSelfMasterId = Convert.ToInt32(hfAppraisalSelfMasterId.Value);
            appLogDao.BSCAppraisalSelfMasterId = Convert.ToInt32(hfAppraisalSelfMasterId.Value);
            appLogDao.Comments = "Draft from HR";
            appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




              idd = _appPartA.SaveEmpAppLogBSCSelf(appLogDao);

        }

        if (OptionInfo.Text == "KPI")
        {

            status = _aFincDal.KPIDelPreviosuLogApproved(hfAppraisalMasterId.Value.ToString(), hfAppraisalSelfMasterId.Value, EmpInfoId.Value, FinancialYearId.Value, OptionInfo.Text);

            AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

            appLogDao.ActionStatus = "Drafted";
            appLogDao.ShowStatus = "Drafted";
            appLogDao.ApproveDate = DateTime.Now;
            appLogDao.ApproveBy = Session["UserId"].ToString();
            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
            appLogDao.ForEmpInfoId = Convert.ToInt32(EmpInfoId.Value);
            appLogDao.AppraisalSelfMasterId = Convert.ToInt32(hfAppraisalSelfMasterId.Value);
            appLogDao.Comments = "Draft from HR";
            appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




            idd = _appPartA.SaveEmpAppSelfHRLog(appLogDao);
        }

        if (idd > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Done!');", true);

            SearchButton_OnClick(null, null);
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something went wrong!');", true);
        }

    }

    protected void btnDraftAppraisal_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");
        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");
        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");
         
        Label OptionInfo = (Label)gv_JdBoard.Rows[rowID].FindControl("OptionInfo");
        bool status;
        int idd = 0;

        if (hfAppraisalMasterId.Value.ToString() == "" || hfAppraisalMasterId.Value.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Appraisal Not found!');", true);
            return;
        }
        if (OptionInfo.Text == "OKR" || OptionInfo.Text == "BSC")
        {


            status = _aFincDal.AppraisalDelPreviosuLogApproved(hfAppraisalMasterId.Value.ToString(), hfAppraisalMasterId.Value, EmpInfoId.Value, OptionInfo.Text);


            AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

            appLogDao.ActionStatus = "Drafted";
            appLogDao.ApproveDate = DateTime.Now;
            appLogDao.ApproveBy = Session["UserId"].ToString();
            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
            appLogDao.ForEmpInfoId = Convert.ToInt32(EmpInfoId.Value);
            appLogDao.AppraisalSelfMasterId = Convert.ToInt32(hfAppraisalMasterId.Value);
            appLogDao.Comments = "Draft From HR";
            appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




            idd = _appPartA.SaveEmpAppLogOKRBSC(appLogDao);
        }

        if (OptionInfo.Text == "KPI")
        {


            status = _aFincDal.AppraisalDelPreviosuLogApproved(hfAppraisalMasterId.Value.ToString(), hfAppraisalMasterId.Value, EmpInfoId.Value, OptionInfo.Text);
            AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

            appLogDao.ActionStatus = "Drafted";
            appLogDao.ApproveDate = DateTime.Now;
            appLogDao.ApproveBy = Session["UserId"].ToString();
            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
            appLogDao.ForEmpInfoId = Convert.ToInt32(EmpInfoId.Value);
            appLogDao.AppraisalSelfMasterId = Convert.ToInt32(hfAppraisalMasterId.Value);
            appLogDao.Comments = "Draft From HR";
            appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




            idd = _appPartA.SaveEmpAppLogKPI(appLogDao);
        }

        if (idd > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Done!');", true);

            SearchButton_OnClick(null, null);
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something went wrong!');", true);
        }
    }
}
