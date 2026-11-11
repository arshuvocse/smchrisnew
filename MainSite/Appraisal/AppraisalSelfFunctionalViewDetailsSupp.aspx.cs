using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalSelfFunctionalViewDetailsSupp : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {  ButtonVisible();
                IniKpiTable();
                IniTable();

                if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
                {
                    GetEmpinfo(Request.QueryString["EmpInfoId"].ToString());
                }
                if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
                {
                    int empId = int.Parse(Request.QueryString["EmpInfoId"]);
                    int finYear = int.Parse(Request.QueryString["financialYearId"]);
                    ddlFinancialYear.SelectedIndex = 1;
                    DataTable dtfin = _appPartA.GetFinYear(Convert.ToInt32(finYear));
                    string finYeartxt = "";
                    if (dtfin.Rows.Count > 0)
                    {
                        finYeartxt = dtfin.Rows[0]["FinancialYearDesc"].ToString();
                    }


                    DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(empId, finYeartxt);
                    if (dtaa.Rows.Count > 0)
                    {

                        int mid = Convert.ToInt32(dtaa.Rows[0][0]);
                        DataTable dtdata = _appPartA.GetPreComments(mid.ToString());
                        GridView1.DataSource = dtdata;
                        GridView1.DataBind();

                        id_mastetID.Value = mid.ToString();
                        DataTable dt = _appPartA.GetAppraisalSelf(mid);

                        txt_employee.Text = dt.Rows[0]["employee"].ToString();
                        ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();


                        //  txt_employee_OnTextChanged(txt_employee, (EventArgs) e);
                        DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid);
                        DataTable dt3 = _appPartA.GetAppraisalSelfB(mid);
                        gv_AppraisalFunc.DataSource = dt2;
                        ViewState["KPIFUNC"] = dt2;
                        ViewState["PARTB"] = dt3;
                        gv_AppraisalFunc.DataBind();
                        gv_AppraisalPartB.DataSource = dt3;
                        gv_AppraisalPartB.DataBind();


                        CalculateBEHAVIORALTotal();
                        CalculateTotal();
                        CalculateB();
                        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
                        {
                            CheckBox isActiveCheckBox = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                            if (isActiveCheckBox.Checked)
                            {
                                //isActiveCheckBox.ForeColor = Color.Blue;
                                isActiveCheckBox.BackColor = Color.Blue;
                                isActiveCheckBox.BorderColor = Color.Blue;
                                isActiveCheckBox.Font.Bold = true;
                            }
                        }

                        //Get Versions
                        if (mid > 0)
                        {
                            DataTable versions = _appPartA.GetApproveLogBySelfMaster(mid);

                            if (versions.Rows.Count > 0)
                            {
                                gv_Versions.DataSource = versions;
                                gv_Versions.DataBind();
                            }
                        }
                    }

                    else
                    {
                        GetEmpinfo(empId.ToString());
                        // ddlFinancialYear.SelectedValue = int.Parse(Request.QueryString["financialYearId"]);
                    }
                }
            }
        }
        catch (Exception)
        {
            
            
        }
       
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
                submitVerifyButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = false;
                submitVerifyButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "View")
            {
                editButton.Visible = false;
                submitButton.Visible = false;
                btn_Save.Visible = false;
                orBTN.Visible = false;
            }
         
            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("AppraisalSelfList.aspx");
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("KpiInfo", typeof (string)));
            dt.Columns.Add(new DataColumn("KpiWeight", typeof (string)));
            dt.Columns.Add(new DataColumn("KpiWeightPer", typeof (string)));
            dt.Columns.Add(new DataColumn("Target", typeof (string)));
            dt.Columns.Add(new DataColumn("TargetPer", typeof (string)));
            dt.Columns.Add(new DataColumn("Deadline", typeof (string)));
            dt.Columns.Add(new DataColumn("MidYearStatus", typeof (string)));
            
            dt.Columns.Add(new DataColumn("SelfMark", typeof (string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof (bool)));
            dr = dt.NewRow();

            dr["KpiInfo"] = "";
            dr["KpiWeight"] = "";
            dr["KpiWeightPer"] = "";
            dr["Target"] = "";
            dr["TargetPer"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";
            dr["IsActive"] = "False";
           
            dr["SelfMark"] = "";
            dt.Rows.Add(dr);
            ViewState["KPIFUNC"] = dt;

            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable) ViewState["KPIFUNC"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();

            drCurrentRow["IsActive"] = "False";

            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["KPIFUNC"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox) gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
               
                TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                dtCurrentTable.Rows[i]["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                    ? ""
                    : tbKpi.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                    ? ""
                    : txtWeight.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                   ? ""
                   : txtWeightPer.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["Target"] = txtTarget.Text.Trim().ToString() == ""
                    ? ""
                    : txtTarget.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                    ? ""
                    : txtTargetPer.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
           
                dtCurrentTable.Rows[i]["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                    ? 0
                    : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                dtCurrentTable.Rows[i]["IsActive"] = chkisactive.Checked;
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

            }

            gv_AppraisalFunc.DataSource = dtCurrentTable;
            gv_AppraisalFunc.DataBind();

            CalculateTotal();
        }
    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] != null && gv_AppraisalFunc.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["KPIFUNC"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["KPIFUNC"] = null;
            }
            else
            {
                ViewState["KPIFUNC"] = dt;
            }


            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
            CalculateTotal();
            // CalculateTotalParticipant();
        }
    }


   
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        if (Validation() == true) { 
        List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
            TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
            CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

            if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
            {
                AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                area.KpiInfo = tbKpi.Text.Trim().ToString();
                area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                area.IsActive = chkisactive.Checked;
                area.SupervisorMark = 0;
                area.MidYearStatus = " ";

                functional.Add(area);
            }

        }


        AppraisalMaster aMaster = new AppraisalMaster();

        aMaster.AppraisalMasterId = id_mastetID.Value==""?0: Convert.ToInt32(id_mastetID.Value);
        aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
        aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


        bool result = false;
        if (functional.Count > 0)
        {
            int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
            if (pk > 0)
            {

                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                appLogDao.ActionStatus = "Drafted";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                appLogDao.AppraisalSelfMasterId = Convert.ToInt32(pk);
                appLogDao.Comments = txt_Comments.Text;

                appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());


                int idd = _appPartA.SaveEmpAppLog(appLogDao);  






                result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                result = SaveAppraisalSelfB( pk);
            }
        }
        else
        {
            result = false;
        }

        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Operation Successful...');window.location ='AppraisalSelfList.aspx';",
               true);
        }
        else
        {
            AlertMessageBoxShow("Operation Failed");
        }
        }
    }


    private bool SaveAppraisalSelfB(int pk)
    {

        bool result = false;
        List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
            TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
            TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");

            if (txtSkillInfo.Text.Trim().ToString() != "")
            {
                AppraisalBehaveArea area = new AppraisalBehaveArea();
                area.AppraisalMasterId = pk;
                area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();
                area.Score = Convert.ToDecimal(string.IsNullOrEmpty(txtScore.Text.Trim()) ? "0" : txtScore.Text.Trim());
                aList.Add(area);

            }


        }
        if (aList.Count > 0)
        {
            result = _appraisalPartBdal.SaveAppraisalSelfPartB(aList);
        }
        return result;
    }
    private void IniTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("SkillInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("SupportingEmp", typeof(string)));
        dt.Columns.Add(new DataColumn("Score", typeof(string)));
        dt.Columns.Add(new DataColumn("SetScore", typeof(string)));


        for (int i = 0; i < 5; i++)
        {
            dr = dt.NewRow();

            dr["SkillInfo"] = "";
            dr["SupportingEmp"] = "";
            dr["Score"] = "5";
            dr["SetScore"] = "";

            dt.Rows.Add(dr);
        }

        ViewState["PARTB"] = dt;

        gv_AppraisalPartB.DataSource = dt;
        gv_AppraisalPartB.DataBind();

        CalculateB();
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    public void GetEmpinfo(string id)
    {
        //string empid = txt_employee.Text.Trim();
        //if (empid.Contains(":"))
        {
            //string[] strsp = empid.Split(':');
            //int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);

            DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(id));
            if (dtEmp.Rows.Count > 0)
            {
                LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));
                txt_employee.Text=dtEmp.Rows[0]["EmpName"].ToString().Trim();
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
                 lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();
                lblOffice.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    id_Empid.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }
    protected void txt_employee_OnTextChanged(object sender, EventArgs e)
    {


        string empid = txt_employee.Text.Trim();
        if (empid.Contains(":"))
        {
            string[] strsp = empid.Split(':');
            int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);
          
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empId);
            if (dtEmp.Rows.Count > 0)
            {
                LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));

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

                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

            }
        }
        else
        {
            txt_employee.Text = "";

            id_Empid.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
        
    }


    private void LoadFinancialYear(int comp)
    {
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(comp);
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }


    private void IniKpiTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("KpiInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("KpiWeight", typeof(string)));
        dt.Columns.Add(new DataColumn("KpiWeightPer", typeof(string)));
        dt.Columns.Add(new DataColumn("Target", typeof(string)));
        dt.Columns.Add(new DataColumn("TargetPer", typeof(string)));
        dt.Columns.Add(new DataColumn("Deadline", typeof(string)));
        dt.Columns.Add(new DataColumn("MidYearStatus", typeof(string)));
        dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
        dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        dr = dt.NewRow();

        dr["KpiInfo"] = "";
        dr["KpiWeight"] = "";
        dr["Target"] = "";
        dr["Deadline"] = "";
        dr["MidYearStatus"] = "";
       
        dr["SelfMark"] = "";
        dr["IsActive"] = "False";
        dt.Rows.Add(dr);
        ViewState["KPIFUNC"] = dt;

        gv_AppraisalFunc.DataSource = dt;
        gv_AppraisalFunc.DataBind();
    }


    protected void btn_Add_B_OnClick(object sender, EventArgs e)
    {
        if (ViewState["PARTB"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("SkillInfo", typeof(string)));
            dt.Columns.Add(new DataColumn("SupportingEmp", typeof(string)));
            dt.Columns.Add(new DataColumn("Score", typeof(string)));

            dr = dt.NewRow();

            dr["SkillInfo"] = "";
            dr["SupportingEmp"] = "";
            dr["Score"] = "";
            ViewState["PARTB"] = dt;

            gv_AppraisalPartB.DataSource = dt;
            gv_AppraisalPartB.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["PARTB"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["PARTB"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");



                dtCurrentTable.Rows[i]["SkillInfo"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["SupportingEmp"] = txtSupportingEmp.Text.Trim().ToString() == "" ? "" : txtSupportingEmp.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["Score"] = txtScore.Text.Trim().ToString() == "" ? "" : txtScore.Text.Trim().ToString();


            }

            gv_AppraisalPartB.DataSource = dtCurrentTable;
            gv_AppraisalPartB.DataBind();
            CalculateB();

        }
    }

    private void CalculateB()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");
            tst2.Text = weightTotal.ToString();
        }
        
    }



    protected void CalculateBEHAVIORALTotal()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {



                TextBox ddlWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SetScore");




                if (ddlWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(ddlWeight.Text.ToString());
                }





            }
            Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("ddllblTotalWeight");
            tst.Text = weightTotal.ToString();


        }
    } 
 
    protected void CalculateTotal()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
            //    if (chkIsActive.Checked)
                {
                    chkIsActive.Checked = true;

                    TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

                    TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");
                    chkIsActive.Checked = true;

                    if (txtWeight.Text == "")
                    {
                        weightTotal = weightTotal + 0;
                    }
                    else
                    {
                        weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                    }
                    if (txtMark.Text == "")
                    {
                        markTotal = markTotal + 0;
                    }
                    else
                    {
                        markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                    }


                }

            }

            Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
            tst.Text = weightTotal.ToString();

            Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst2.Text = markTotal.ToString();
        }
    }
    protected void lb_Remove_b_OnClick(object sender, EventArgs e)
    {
        if (ViewState["PARTB"] != null && gv_AppraisalPartB.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["PARTB"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["PARTB"] = null;
            }
            else
            {
                ViewState["PARTB"] = dt;
            }


            gv_AppraisalPartB.DataSource = dt;
            gv_AppraisalPartB.DataBind();
            CalculateB();

        }
    }



    public bool Validation()
    {
        bool isVAlid = true;
        if (gv_AppraisalFunc.Rows.Count <= 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Kpi Info Required ", this);
           
        }
        if (gv_AppraisalPartB.Rows.Count <= 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
           
        }

        if (id_Empid.Value == "")
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Select Employee ", this);
        }

        if (ddlFinancialYear.SelectedValue == "")
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Select Fianncial Year ", this);
        }
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
            TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
            if (string.IsNullOrEmpty(txtWeightPer.Text))
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Weight(%) Required ", this);
                break;
            }

            if (tbKpi.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Info Required ", this);
                break;
            }

            if (txtTarget.Text == "")
            {
                 isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Target Required ", this);
                break;
            }
            if (txtWeight.Text == "")
            {
                 isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Weight Required ", this);
                break;
            }

            //if (txtMark.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Kpi Mark Required ", this);
            //    break;
            //}
            if (txtDeadLine.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Deadline Required ", this);
                break;
            }

            //if (txtMidStatus.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Mid Year Status Required ", this);
            //    break;
            //}
        }
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
            TextBox txtSupportingInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
            if (txtSkillInfo.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
                break;
            }
           
        }

        Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");

        decimal weightTotal = tst.Text == "" ? 0 : Convert.ToDecimal(tst.Text.ToString());
        if (weightTotal > 75)
        {
            aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than 75 In Part A ", this);
            isVAlid = false;
        }


        Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");

        decimal weightTotal2 = tst.Text == "" ? 0 : Convert.ToDecimal(tst2.Text.ToString());
        if (weightTotal2 > 25)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Bigger than 25 In Part B ", this);
            isVAlid = false;
        }
        return isVAlid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalSelfList.aspx");
    }

    protected void txtWeight_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
          CalculateTotal();


    }

    protected void txtMark_OnTextChanged(object sender, EventArgs e)
    {
        //double total = 0;

        //for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        //{
        //    TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
        //    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
        //    double a = txtWeight.Text == "" ? 0 : Convert.ToDouble(txtWeight.Text.Trim());
        //    double b = txtMark.Text == "" ? 0 : Convert.ToDouble(txtMark.Text.Trim());
        //    if (b>a)
        //    {
        //        txtMark.Text = a.ToString();
        //        total += a;
        //    }
        //    else
        //    {
        //        total += b;
        //    }
           

        //}
        //Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
        //tst.Text = total.ToString();


    

    }

    protected void Score_OnTextChanged(object sender, EventArgs e)
    {
        double total = 0;

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtMark = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");



            total += txtMark.Text == "" ? 0 : Convert.ToDouble(txtMark.Text);
            


        }

        Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");
        tst.Text = total.ToString();
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());

                    area.SupervisorMark = Convert.ToDecimal(txtMark.Text.Trim().ToString());
                    area.MidYearStatus = txtMidStatus.Text.Trim().ToString();

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                    result = SaveAppraisalSelfB(pk);
                }
            }
            else
            {
                result = false;
            }

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Operation Successful...');window.location ='AppraisalSelfList.aspx';",
               true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }

    protected void txtWeightPer_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thenum = (75.00 / 100.00) * weightper;
        txtweight.Text = thenum.ToString("#,##0.00");
        CalculateTotal();
    }

    protected void txtTarget_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTarget");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTargetPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
    }

    protected void txtTargetPer_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTarget");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTargetPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thenum = (75.00 / 100.00) * weightper;
        txtweight.Text = thenum.ToString("#,##0.00");
    }

    protected void isActiveCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateTotal();
        }
        catch (Exception)
        {
            
            
        }
        
    }

    protected void submitVerifyButton_OnClick(object sender, EventArgs e)
    {
        if (Validation() == true)
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                    area.IsActive = chkisactive.Checked;
                    area.SupervisorMark = 0;
                    area.MidYearStatus = " ";

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {

                    AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                    appLogDao.ActionStatus = "Drafted";
                    appLogDao.ApproveDate = DateTime.Now;
                    appLogDao.ApproveBy = Session["UserId"].ToString();
                    appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                    appLogDao.AppraisalSelfMasterId = Convert.ToInt32(pk);
                    appLogDao.Comments = txt_Comments.Text;
                    appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                    int idd = _appPartA.SaveEmpAppLog(appLogDao);


                    AppraisalSelfAppLogDAO aMastera = new AppraisalSelfAppLogDAO();
                    aMastera.AppraisalSelfMasterId
                        = Convert.ToInt32(pk);
                    aMastera.ActionStatus = "Verified";
                    bool status = _appPartA.UpdateContractural(aMastera);
                    DataTable dtempdata = _appPartA.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    AppraisalSelfAppLogDAO appLogDao1 = new AppraisalSelfAppLogDAO()
                    {
                        ActionStatus = "Verified",
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                        AppraisalSelfMasterId = Convert.ToInt32(pk),
                        Comments = txt_Comments.Text,
                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                    };
                    int id = _appPartA.SaveEmpAppLog(appLogDao1);




                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                    result = SaveAppraisalSelfB(pk);
                }
            }
            else
            {
                result = false;
            }

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='AppraisalSelfList.aspx';",
                   true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }
}