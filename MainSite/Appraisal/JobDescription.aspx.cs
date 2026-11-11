using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_JobDescription : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    { 
        
        Session["EmpOption"] = "Employee";
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadInitialDDL();
            IniGrid();
            txt_employee_OnTextChanged(null,null);
            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                masterId.Value = mid.ToString();

                DataTable dt = _jdDal.GetJdByMaster(mid);
                empInfoId.Value = dt.Rows[0]["EmpInfoId"].ToString();
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs) e);
             //   ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                txt_employee.Text = dt.Rows[0]["employee"].ToString();
                GETEmployeeInfo(empInfoId.Value);
                txtJobSummary.Text = dt.Rows[0]["JdSummary"].ToString();
                DataTable dt2 = _jdDal.GetJdDetails(Convert.ToInt32(mid));
                gv_JdDetails.DataSource = dt2;
                gv_JdDetails.DataBind();
                ViewState["JdDetailsInfo"] = dt2;

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt2.Rows[i]["Active"].ToString()))
                    {
                        ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect")).Checked=true;
                    }
                    else
                    {
                        ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect")).Checked=false;
                    }
                }
            }
            
        }
       
    }

    private void GETEmployeeInfo(string value)
    {
        //string empid = txt_employee.Text.Trim();
        //if (empid.Contains(":"))
        {
            //   string[] strsp = empid.Split(':');
            // int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);
            int empId = Convert.ToInt32(value);
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empId);
            if (dtEmp.Rows.Count > 0)
            {
                // LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));
                lblPlace.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                comNameLabel.Text = dtEmp.Rows[0]["CompanyName"].ToString().Trim();
                comIdHiddenField.Value = dtEmp.Rows[0]["CompanyId"].ToString().Trim();
                LocationLabel.Text = dtEmp.Rows[0]["Location"].ToString();
                txt_employee.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                empName.Text = txt_employee.Text;
                divisionNameLabel.Text = dtEmp.Rows[0]["DivisionName"].ToString().Trim();
                divitionIdHiddenField.Value = dtEmp.Rows[0]["DivisionId"].ToString().Trim();

                divWingNameLabel.Text = dtEmp.Rows[0]["DivisionWingName"].ToString().Trim();
                divWingIdHiddenField.Value = dtEmp.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = dtEmp.Rows[0]["DepartmentId"].ToString().Trim();

                secNameLabel.Text = dtEmp.Rows[0]["SectionName"].ToString().Trim();
                secIdHiddenField.Value = dtEmp.Rows[0]["SectionId"].ToString().Trim();


                employeeType.Text = dtEmp.Rows[0]["EmpType"].ToString().Trim();
                empTypeHiddenField.Value = dtEmp.Rows[0]["EmpTypeId"].ToString().Trim();

                subSectionLabel.Text = dtEmp.Rows[0]["SubSectionName"].ToString().Trim();
                subSectionHiddenField.Value = dtEmp.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = dtEmp.Rows[0]["DesignationId"].ToString().Trim();

                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

                empInfoId.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                DataTable dtdatajd = _trainingDal.GetJobDescInfo(Convert.ToInt32(empInfoId.Value));

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();



                empInfoId.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                DataTable dtExistingJD = _trainingDal.GetExistingJobDescInfo(Convert.ToInt32(empInfoId.Value));




                if (dtExistingJD.Rows.Count > 0)
                {
                    gv_JdDetails.DataSource = dtExistingJD;
                    gv_JdDetails.DataBind();
                }
                else
                {
                    if (dtdatajd.Rows.Count > 0)
                    {
                        gv_JdDetails.DataSource = dtdatajd;
                        gv_JdDetails.DataBind();
                    }
                    else
                    {
                        IniGrid();
                    }
                }
            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    empInfoId.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
        
    }


    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("../Appraisal/JdList.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       string comp= ddlCompany.SelectedValue;
        Session["CompanyId"] = comp;

        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        //ddlFinancialYear.DataSource = dt;
        //ddlFinancialYear.DataValueField = "Value";
        //ddlFinancialYear.DataTextField = "TextField";
        //ddlFinancialYear.DataBind();
    }

    protected void txt_employee_OnTextChanged(object sender, EventArgs e)
    {
        //string empid = txt_employee.Text.Trim();
        //if (empid.Contains(":"))
        {
         //   string[] strsp = empid.Split(':');
         // int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);
            int empId = Convert.ToInt32(Session["EmpInfoId"].ToString());
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empId);
            if (dtEmp.Rows.Count > 0)
            {
               // LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));
                lblPlace.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                comNameLabel.Text = dtEmp.Rows[0]["CompanyName"].ToString().Trim();
                comIdHiddenField.Value = dtEmp.Rows[0]["CompanyId"].ToString().Trim();
                LocationLabel.Text = dtEmp.Rows[0]["Location"].ToString();
                txt_employee.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                empName.Text = txt_employee.Text;
                divisionNameLabel.Text = dtEmp.Rows[0]["DivisionName"].ToString().Trim();
                divitionIdHiddenField.Value = dtEmp.Rows[0]["DivisionId"].ToString().Trim();

                divWingNameLabel.Text = dtEmp.Rows[0]["DivisionWingName"].ToString().Trim();
                divWingIdHiddenField.Value = dtEmp.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = dtEmp.Rows[0]["DepartmentId"].ToString().Trim();

                secNameLabel.Text = dtEmp.Rows[0]["SectionName"].ToString().Trim();
                secIdHiddenField.Value = dtEmp.Rows[0]["SectionId"].ToString().Trim();


                employeeType.Text = dtEmp.Rows[0]["EmpType"].ToString().Trim();
                empTypeHiddenField.Value = dtEmp.Rows[0]["EmpTypeId"].ToString().Trim();

                subSectionLabel.Text = dtEmp.Rows[0]["SubSectionName"].ToString().Trim();
                subSectionHiddenField.Value = dtEmp.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = dtEmp.Rows[0]["DesignationId"].ToString().Trim();

                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

                empInfoId.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                DataTable dtdatajd = _trainingDal.GetJobDescInfo(Convert.ToInt32(empInfoId.Value));

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();



                empInfoId.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                DataTable dtExistingJD = _trainingDal.GetExistingJobDescInfo(Convert.ToInt32(empInfoId.Value));




                if (dtExistingJD.Rows.Count>0)
                {
                    gv_JdDetails.DataSource = dtExistingJD;
                    gv_JdDetails.DataBind();
                    ViewState["JdDetailsInfo"] = dtExistingJD;
                }
                else
                {
                    if (dtdatajd.Rows.Count > 0)
                    {
                        gv_JdDetails.DataSource = dtdatajd;
                        gv_JdDetails.DataBind();
                        ViewState["JdDetailsInfo"] = dtdatajd;

                    }
                    else
                    {
                        IniGrid();
                    }
                }

                for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
                {
                    CheckBox chkSelect = ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect"));

                    HiddenField hfIsActive = ((HiddenField)gv_JdDetails.Rows[i].FindControl("hfIsActive"));

                    if (hfIsActive.Value == "True")
                    {
                        chkSelect.Checked = true;
                    }
                    else
                    {
                        chkSelect.Checked = false;

                    }
                }
            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    empInfoId.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
        
    }

    private void IniGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("JdDetailsInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("IsActive", typeof(string)));
        dr = dt.NewRow();

        dr["JdDetailsInfo"] = "";
        dr["IsActive"] = "false";
        ViewState["JdDetailsInfo"] = dt;
        dt.Rows.Add(dr);
        gv_JdDetails.DataSource = dt;
        gv_JdDetails.DataBind();
    }

    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["JdDetailsInfo"] == null)
        {

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("JdDetailsInfo", typeof(string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(string)));

            dr = dt.NewRow();

            dr["JdDetailsInfo"] = "";
            dr["IsActive"] = "false";

            ViewState["JdDetailsInfo"] = dt;
            dt.Rows.Add(dr);
            gv_JdDetails.DataSource = dt;
            gv_JdDetails.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["JdDetailsInfo"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


         
            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");

                CheckBox chkSelect = ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect"));

                HiddenField hfIsActive = ((HiddenField)gv_JdDetails.Rows[i].FindControl("hfIsActive"));

                dtCurrentTable.Rows[i]["JdDetailsInfo"] = tbKpi.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["IsActive"] = chkSelect.Checked;
                hfIsActive.Value = chkSelect.Checked.ToString();
            }

            gv_JdDetails.DataSource = dtCurrentTable;
            gv_JdDetails.DataBind();
            ViewState["JdDetailsInfo"] = dtCurrentTable;

            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                CheckBox chkSelect = ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect"));

                HiddenField hfIsActive = ((HiddenField)gv_JdDetails.Rows[i].FindControl("hfIsActive"));

                if (hfIsActive.Value=="True")
                {
                    chkSelect.Checked=true;
                }
                else
                {
                    chkSelect.Checked=false;
                    
                }
            }
        }
    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["JdDetailsInfo"] != null && gv_JdDetails.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["JdDetailsInfo"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["JdDetailsInfo"] = null;
            }
            else
            {
                ViewState["JdDetailsInfo"] = dt;
            }


            gv_JdDetails.DataSource = dt;
            gv_JdDetails.DataBind();

            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                CheckBox chkSelect = ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect"));

                HiddenField hfIsActive = ((HiddenField)gv_JdDetails.Rows[i].FindControl("hfIsActive"));

                if (hfIsActive.Value == "True")
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Checked = false;

                }
            }

            // CalculateTotalParticipant();
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobDescription.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (ValidationExist())
            {
                
            
            JdMaster aMaster = new JdMaster();
            aMaster.JdMasterId = masterId.Value == "" ? 0 : Convert.ToInt32(masterId.Value);
            aMaster.EmpInfoId = Convert.ToInt32(empInfoId.Value);
                aMaster.ActionStatus = "Drafted";
          //  aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.JdSummary = txtJobSummary.Text.Trim();

            int pk = _jdDal.SaveJdMaster(aMaster, Session["LoginName"].ToString());
           
            ///masterId.Value
            bool result = false;
                if (pk > 0)
                {
                    List<JdDetails> aList = new List<JdDetails>();

                    for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
                    {
                        TextBox aBox = (TextBox) gv_JdDetails.Rows[i].FindControl("txtJdDetails");

                        if (aBox.Text.Trim() != "")
                        {
                            JdDetails aDetails = new JdDetails();
                            aDetails.JdDetailsInfo = aBox.Text.Trim();
                            aDetails.JdMasterId = pk;
                            aDetails.IsActive = ((CheckBox) gv_JdDetails.Rows[i].FindControl("chkSelect")).Checked;
                            
                            aList.Add(aDetails);
                        }
                    }
                    //DataTable dtempdata = _jdDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    //JDAppLogDAO appLogDao = new JDAppLogDAO()
                    //{
                    //    ActionStatus = "Drafted",
                    //    ApproveDate = DateTime.Now,
                    //    ApproveBy = Session["UserId"].ToString(),
                    //    PreEmpInfoId = Convert.ToInt32(0),
                    //    ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString()),
                    //    JdMasterId = pk,
                    //    Comments = " "

                    //};
                    //int id = _jdDal.SaveJdMasterLog(appLogDao); 

                    JDAppLogDAO appLogDao = new JDAppLogDAO();

                    appLogDao.ActionStatus = "Drafted";
                    appLogDao.ApproveDate = DateTime.Now;
                    appLogDao.ApproveBy = Session["UserId"].ToString();
                    appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                    appLogDao.JdMasterId = Convert.ToInt32(pk);
                    appLogDao.Comments = txt_Comments.Text;
                    appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                    int idd = _jdDal.SaveJdMasterLog(appLogDao);


                    JDAppLogDAO aMastera = new JDAppLogDAO();
                    aMastera.JdMasterId
                        = Convert.ToInt32(pk);
                    aMastera.ActionStatus = "Verified";
                    bool status = _jdDal.UpdateContractural(aMastera);
                    DataTable dtempdata = _jdDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    JDAppLogDAO appLogDao1 = new JDAppLogDAO()
                    {
                        ActionStatus = "Verified",
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                        JdMasterId = Convert.ToInt32(pk),
                        Comments = txt_Comments.Text,
                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                    };
                    int id = _jdDal.SaveJdMasterLog(appLogDao1);



                    if (aList.Count > 0)
                    {
                        result = _jdDal.SaveJdDetails(aList, pk);

                        if (result == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                               "alert",
                               "alert('Data Saved Successfully...');window.location ='JdList.aspx';",
                               true);
                           //Response.Redirect("JobDescription.aspx");
                        }
                        else
                        {
                            AlertMessageBoxShow("Operation Failed...");
                        }
                    }
                }
            }
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

    public bool ValidationExist()
    {
        DataTable dtdata = _jdDal.CheckEmpJDList(empInfoId.Value);
        if (dtdata.Rows.Count > 0)
        {
            aShowMessage.ShowMessageBox("Employee Job Description Already Exist", this);
            return false;
        }
        return true;
    }
    private bool Validation()
    {
        //if (ddlFinancialYear.SelectedValue == "" || ddlFinancialYear.SelectedValue == "-1")
        //{
        //    aShowMessage.ShowMessageBox("Financial Year Required ", this);
        //    return false;
        //}
        
        if (txtJobSummary.Text == "")
        {
            aShowMessage.ShowMessageBox("Summary Required ", this);
            return false;
        }
        if (gv_JdDetails.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Job Description Required ", this);
            return false;
        }

        if (gv_JdDetails.Rows.Count > 0)
        {
            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");
                if (tbKpi.Text.Trim() == "")
                {
                    aShowMessage.ShowMessageBox("Job Description Required ", this);
                    return false;
                }
            }
        }
        return true;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JdList.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            
            JdMaster aMaster = new JdMaster();
            aMaster.JdMasterId = masterId.Value == "" ? 0 : Convert.ToInt32(masterId.Value);
            aMaster.EmpInfoId = Convert.ToInt32(empInfoId.Value);
           // aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.JdSummary = txtJobSummary.Text.Trim();
            aMaster.ActionStatus = "Drafted";
            int pk = _jdDal.SaveJdMaster(aMaster, Session["LoginName"].ToString());

            ///masterId.Value
            bool result = false;
            if (pk > 0)
            {
                List<JdDetails> aList = new List<JdDetails>();

                for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
                {
                    TextBox aBox = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");

                    if (aBox.Text.Trim() != "")
                    {
                        JdDetails aDetails = new JdDetails();
                        aDetails.JdDetailsInfo = aBox.Text.Trim();
                        aDetails.JdMasterId = pk;
                        aDetails.IsActive = ((CheckBox)gv_JdDetails.Rows[i].FindControl("chkSelect")).Checked;
                        aList.Add(aDetails);

                    }
                }
                JDAppLogDAO appLogDao = new JDAppLogDAO()
                {
                    ActionStatus = "Drafted",
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(0),
                    ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString()),
                    JdMasterId = pk,
                    Comments = " "

                };
                int id = _jdDal.SaveJdMasterLog(appLogDao); 
                if (aList.Count > 0)
                {
                    result = _jdDal.SaveJdDetails(aList, pk);
                    if (result == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='JdList.aspx';",
                       true);
                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");

                    }
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        bool result = _jdDal.DeleteJd(Convert.ToInt32(masterId.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            AlertMessageBoxShow("Operation Successful...");
        }
        else
        {
            AlertMessageBoxShow("Operation Failed...");

        }
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkSelect = ((CheckBox)gv_JdDetails.Rows[rowIndex].FindControl("chkSelect"));

        HiddenField hfIsActive = ((HiddenField)gv_JdDetails.Rows[rowIndex].FindControl("hfIsActive"));

        if (chkSelect.Checked)
        {
            hfIsActive.Value = chkSelect.Checked.ToString();
        }
        else
        {
            hfIsActive.Value = chkSelect.Checked.ToString();
        }
    }
}