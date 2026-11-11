using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_KPIInformationView : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private KPIInformationViewDAL _aFincDal = new KPIInformationViewDAL();
    ShowMessage aShowMessage = new ShowMessage();
    private HealthCareListDal aListDal = new HealthCareListDal();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private ShowMessage aMessage = new ShowMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();

            LoadInitialDDL();

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

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_JdBoard.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var ckSelect = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("ckSelect");
            ckSelect.Checked = chkBoxHeader.Checked;
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

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";


 
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "' ,'_blank');", true);
    }


    protected void btn_print_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");

        string url = "../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value ;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
       
       // Response.Redirect("../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
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

      
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadGridInfo();
    }

    private void LoadGridInfo()
    {
        try
        {
            //and  OnlyViewComId=" + ddlCompany.SelectedValuem"
            DataTable dt = _aFincDal.GetAppraisalByKpiPermission_New__SAP(ddlCompany.SelectedValue.ToString(), Parameter(), " and  tblAppraisalSelfAppLog.Version=CELog.MaxVer " + Parameter_JJJ());

            if (dt.Rows.Count > 0)
            {
                gv_JdBoard.DataSource = dt;
                gv_JdBoard.DataBind();
                gv_JdBoard.UseAccessibleHeader = true;
                gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;
                for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
                {
                    bool status = Convert.ToBoolean(gv_JdBoard.DataKeys[i]["IsSapApproved"].ToString());

                    CheckBox checkBox = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckSelect");

                    if (status == true)
                    {
                        checkBox.Checked = true;
                        checkBox.Enabled = false;
                    }
                    else
                    {
                        checkBox.Checked = false;
                    }
                }

                ViewState["EmpSetup"] = dt;
            }
            else
            {
                DataTable dt3 = _aFincDal.GetAppraisalByKpiPermission_New__SAP(ddlCompany.SelectedValue.ToString(), Parameter2(), " and  tblAppraisalSelfAppLog.Version=CELog.MaxVer " + Parameter_JJJ());

                try
                {
                    gv_JdBoard.DataSource = dt3;
                    gv_JdBoard.DataBind();
                    gv_JdBoard.UseAccessibleHeader = true;
                    gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;

                    for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
                    {
                        bool status = Convert.ToBoolean(gv_JdBoard.DataKeys[i]["IsSapApproved"].ToString());

                        CheckBox checkBox = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckSelect");

                        if (status == true)
                        {
                            checkBox.Checked = true;
                            checkBox.Enabled = false;
                        }
                        else
                        {
                            checkBox.Checked = false;
                        }
                    }
                    ViewState["EmpSetup"] = dt;
                }
                catch (Exception)
                {

                    gv_JdBoard.DataSource = null;
                    gv_JdBoard.DataBind();
                    ViewState["EmpSetup"] = null;
                }
            }
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

    private bool Validation()
    {

        int count = 0;

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            CheckBox checkBox = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckSelect");

            if (checkBox.Checked)
            {
                count++;
            }
        }

        if (count == 0)
        {
            aMessage.ShowMessageBox("Please Select At least One", this);
            return false;
        }

        return true;
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            int Id = 0;

            for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
            {
                CheckBox checkBox = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckSelect");

                if (checkBox.Checked)
                {
                    Id++;
                    HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[i].FindControl("hfAppraisalMasterId");

                    aListDal.UPDATE_Apprisal_SAP_Intregration(hfAppraisalMasterId.Value);

                }
            }

            if (Id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');",
                    true);
                LoadGridInfo();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Something went wrong...');window.location ='SAPIntegrationPoint.aspx';",
                    true);
            }

        }

    }
    public string Parameter()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FinancialYearId='" + ddlFinancialYear.SelectedValue + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            param = param + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND    dpt.DepartmentId in( 140, 141)";
        }

        param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer  ";    //AND tblAppraisalMasterAppLog.Version=ALog.MaxVer

        return param;

    }

    public string Parameter_JJJ()
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

        if (ddlDivision.SelectedIndex > 0)
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
                param = param + " AND y.FinancialYearDesc='" + ddlFinancialYear.SelectedValue + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }


        if (Session["EmpInfoId"].ToString() == "3920")
        {
            param = param + " AND (dpt.DepartmentName LIKE '%Sales%' OR dpt.DepartmentName  LIKE '%Marketing%')";
        }

         param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";
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

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");
        
        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");


        Label ActionStatus = (Label)gv_JdBoard.Rows[rowID].FindControl("ActionStatus");

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

        Label Awaitingf = (Label)gv_JdBoard.Rows[rowID].FindControl("Awaitingf");

        isKPI.Checked = false;
        if (Awaitingf.Text.Trim() == "Approved")
        {
            aShowMessage.ShowMessageBox("Already Approved!!", this);
        }
        else if (Awaitingf.Text.Trim() == "Not Approved")
        {
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

            DataTable dtapprove = _aFincDal.GetApprisalApprovaer(Convert.ToInt32(hfAppraisalMasterId.Value));
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
            if (isKPI.Checked)
            {
                try
                {
                    DataTable dtKPISamePer = _aFincDal.GetKPIIfSamePerson(Convert.ToInt32(hfMainId.Value));

                    if (ddlForwordEmp.SelectedValue == dtKPISamePer.Rows[0]["EmpInfoId"].ToString())
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
                    
                    //throw;
                }
            }
            else
            {

                DataTable dtKPISamePer = _aFincDal.GetAppraisalIfSamePerson(Convert.ToInt32(hfMainId.Value));

                if (ddlForwordEmp.SelectedValue == dtKPISamePer.Rows[0]["EmpInfoId"].ToString())
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

            if (status)
            {

                aShowMessage.ShowMessageBox("Operation Successfully...",this);

                SearchButton_OnClick(null, null);
                mpFunctionalSup.Hide();
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
       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'CheckupGridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
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

       
        if (Awaitingf.Text.Trim() == "Approved")
        {



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

            DataTable dtapprove = _aFincDal.GetApprisalApprovaer(Convert.ToInt32(hfAppraisalMasterId.Value));
            if (dtapprove.Rows.Count > 0)
            {
               
                hfMasterId.Value = dtapprove.Rows[0]["AppraisalMasterAppLogId"].ToString();
            }
            using (DataTable dtEmp = _commonDataLoad.EmpforCancelApproveAppraisal(hfAppraisalMasterId.Value.ToString())
                )
            {
                hfMainAppraisalMasterId.Value = hfAppraisalMasterId.Value;


                ddlEmpforCancelApproveAppraisal.DataSource = dtEmp;
                ddlEmpforCancelApproveAppraisal.DataValueField = "EmpId";
                ddlEmpforCancelApproveAppraisal.DataTextField = "empName";
                ddlEmpforCancelApproveAppraisal.DataBind();
                ddlEmpforCancelApproveAppraisal.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpforCancelApproveAppraisal.SelectedIndex = 0;
            }


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

    protected void lbCancelAppraisalSubmit_OnClick(object sender, EventArgs e)
    {
        if (ddlEmpforCancelApproveAppraisal.SelectedValue != "")
        {
            bool status = false;

            status = _aFincDal.CancelAppprisalApprovePersonContractural(hfMainAppraisalMasterId.Value.ToString(), hfMasterId.Value, ddlEmpforCancelApproveAppraisal.SelectedValue);

          

            if (status)
            {
                aShowMessage.ShowMessageBox("Operation Successfully...", this);

                SearchButton_OnClick(null, null);
                MPAppraisalApproval.Hide();
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Forward Approval Person", this);
            ddlEmpforCancelApproveAppraisal.Focus();
        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
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
            //ddlDivision.Items.Clear();
            //ddlDivision.Items.Clear();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SAPKPIInformation.aspx");
    }
}