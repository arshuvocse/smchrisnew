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
using DAL.TrainingDAL;
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
    {  Session["EmpOption"] = "Employee";
        if (!IsPostBack)
        {
            RadioTextValue();
            ButtonVisible();
            LoadInitialDDL();
            IniGrid();
         
            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                masterId.Value = mid.ToString();

                DataTable dt = _jdDal.GetJdByMaster(mid);
                empInfoId.Value = dt.Rows[0]["EmpInfoId"].ToString();
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs) e);
             //   ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                GetEmpInfoByEmpID(Convert.ToInt32(empInfoId.Value));
                txtJobSummary.Text = dt.Rows[0]["JdSummary"].ToString();
                DataTable dt2 = _jdDal.GetJdDetails(Convert.ToInt32(mid));
                gv_JdDetails.DataSource = dt2;
                gv_JdDetails.DataBind();
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


    public void ButtonVisible()
    {
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        submitButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        editButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        delButton.Visible = true;
        //    }
        //    Session["Status"] = null;
        //}

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
           
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    empInfoId.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
        
    }

    private void GetEmpInfoByEmpID( int empId)
    {
        empId = Convert.ToInt32(empId);
        DataTable dtEmp = _appPartA.GetEmployeeDetails(empId);
        if (dtEmp.Rows.Count > 0)
        {
            // LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));

          
            txt_employee.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
            lblEmployeeName.Text = txt_employee.Text;
            lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

            comNameLabel.Text = dtEmp.Rows[0]["CompanyName"].ToString().Trim();
            comIdHiddenField.Value = dtEmp.Rows[0]["CompanyId"].ToString().Trim();

            divisionNameLabel.Text = dtEmp.Rows[0]["DivisionName"].ToString().Trim();
            divitionIdHiddenField.Value = dtEmp.Rows[0]["DivisionId"].ToString().Trim();

            divWingNameLabel.Text = dtEmp.Rows[0]["DivisionWingName"].ToString().Trim();
            divWingIdHiddenField.Value = dtEmp.Rows[0]["DivisionWId"].ToString().Trim();


            deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = dtEmp.Rows[0]["DepartmentId"].ToString().Trim();

            secNameLabel.Text = dtEmp.Rows[0]["SectionName"].ToString().Trim();
            secIdHiddenField.Value = dtEmp.Rows[0]["SectionId"].ToString().Trim();

            subSectionLabel.Text = dtEmp.Rows[0]["SubSectionName"].ToString().Trim();
            subSectionHiddenField.Value = dtEmp.Rows[0]["SubSectionId"].ToString().Trim();

            desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = dtEmp.Rows[0]["DesignationId"].ToString().Trim();

            joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            LocationLabel.Text = dtEmp.Rows[0]["Location"].ToString();
            lblPlace.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

            ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


          


            empInfoId.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
            DataTable dtdatajd = _trainingDal.GetJobDescInfo(Convert.ToInt32(empInfoId.Value));


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

    private void IniGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("JdDetailsInfo", typeof(string)));
        dr = dt.NewRow();

        dr["JdDetailsInfo"] = "";
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
            dr = dt.NewRow();

            dr["JdDetailsInfo"] = "";
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


            ViewState["JdDetailsInfo"] = dtCurrentTable;
            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");
                dtCurrentTable.Rows[i]["JdDetailsInfo"] = tbKpi.Text.Trim().ToString() == "" ? "" : tbKpi.Text.Trim().ToString();
            }

            gv_JdDetails.DataSource = dtCurrentTable;
            gv_JdDetails.DataBind();

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
            // CalculateTotalParticipant();
        }
    }

    public void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }

        DataTable dtdata = _appPartA.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), Request.QueryString["EmpInfoId"]);

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["ForEmpInfoId"].ToString() != Session["EmpInfoId"].ToString())
        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();


        try
        {
            if (Session["ForEmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
            {
                actionRadioButtonList.Items[1].Enabled = false;
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        JDAppLogDAO aMaster = new JDAppLogDAO();
        aMaster.JdMasterId
            = Convert.ToInt32(masterId.Value);
        aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
        bool status = _jdDal.UpdateContractural(aMaster);
        if (status)
        {
            if (aMaster.ActionStatus == "Verified")
            {
                DataTable dtempdata = _jdDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                JDAppLogDAO appLogDao = new JDAppLogDAO()
                {
                    ActionStatus = actionRadioButtonList.SelectedValue,
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                    ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                    JdMasterId = aMaster.JdMasterId,
                    Comments = commentsTextBox.Text,
                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                };
                int id = _jdDal.SaveJdMasterLog(appLogDao);
            }
            else if (aMaster.ActionStatus == "Approved")
            {
                //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                JDAppLogDAO appLogDao = new JDAppLogDAO()
                {
                    ActionStatus = actionRadioButtonList.SelectedValue,
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                    ForEmpInfoId = 0,
                    JdMasterId = aMaster.JdMasterId,
                    Comments = commentsTextBox.Text,
                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                };
                int id = _jdDal.SaveJdMasterLog(appLogDao);
                
            }
            else if (aMaster.ActionStatus == "Review")
            {
                DataTable dtempdata = _jdDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), masterId.Value);
                DataTable dtempdata2 = _jdDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), masterId.Value);

                if (dtempdata2.Rows.Count > 0)
                {
                    JDAppLogDAO appLogDao = new JDAppLogDAO()
                    {
                        ActionStatus = "Verified",
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                        ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                        JdMasterId = aMaster.JdMasterId,
                        Comments = commentsTextBox.Text,
                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                    };
                    _jdDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                    int id = _jdDal.SaveJdMasterLog(appLogDao);
                }
                else
                {
                    ShowMessageBox("Please select Approval Status Approved  this!!!");
                }
            }

            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Data Saved Successfully...');window.location ='JdApprovalList.aspx';",
                       true);
        }

        //JdMaster aMaster = new JdMaster();
        //aMaster.JdMasterId = Convert.ToInt32(masterId.Value);
        //aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
        //bool status = _jdDal.UpdateJdMasterInfo(aMaster);
        //if (status)
        //{
        //    if (aMaster.ActionStatus == "Verified")
        //    {
        //        DataTable dtempdata = _jdDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
        //        JDAppLogDAO appLogDao = new JDAppLogDAO()
        //        {
        //            ActionStatus = actionRadioButtonList.SelectedValue,
        //            ApproveDate = DateTime.Now,
        //            ApproveBy = Session["UserId"].ToString(),
        //            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
        //            ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
        //            JdMasterId = aMaster.JdMasterId,
        //            Comments = commentsTextBox.Text,

        //        };
        //        int id = _jdDal.SaveJdMasterLog(appLogDao);
        //    }
        //    else if (aMaster.ActionStatus == "Approved")
        //    {
        //        //DataTable dtempdata = _jdDal.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
        //        JDAppLogDAO appLogDao = new JDAppLogDAO()
        //        {
        //            ActionStatus = actionRadioButtonList.SelectedValue,
        //            ApproveDate = DateTime.Now,
        //            ApproveBy = Session["UserId"].ToString(),
        //            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
        //            ForEmpInfoId = 0,
        //            JdMasterId = aMaster.JdMasterId,
        //            Comments = commentsTextBox.Text,

        //        };
        //        int id = _jdDal.SaveJdMasterLog(appLogDao);
        //    }
        //    else if (aMaster.ActionStatus == "Review")
        //    {
        //        DataTable dtempdata = _jdDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), masterId.Value);
        //        DataTable dtempdata2 = _jdDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), masterId.Value);
        //        JDAppLogDAO appLogDao = new JDAppLogDAO()
        //        {
        //            ActionStatus = "Verified",
        //            ApproveDate = DateTime.Now,
        //            ApproveBy = Session["UserId"].ToString(),
        //            PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
        //            ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
        //            JdMasterId = aMaster.JdMasterId,
        //            Comments = commentsTextBox.Text,

        //        };
        //        _jdDal.UpdateJDAppLog("Review", Session["JDAppLogId"].ToString());
        //        int id = _jdDal.SaveJdMasterLog(appLogDao);
        //    }


        //}
        //Session["JDAppLogId"] = null;
        //ScriptManager.RegisterStartupScript(this, this.GetType(),
        //           "alert",
        //           "alert('Data Saved Successfully...');window.location ='JdApprovalList.aspx';",
        //           true);
        
    }


    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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
        Response.Redirect("JdApprovalList.aspx");
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

                if (aList.Count > 0)
                {
                    result = _jdDal.SaveJdDetails(aList, pk);
                    if (result == true)
                    {
                        AlertMessageBoxShow("Operation Successful...");
                        Response.Redirect("JobDescription.aspx");
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
}