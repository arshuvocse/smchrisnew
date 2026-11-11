using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Security;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalSelfFunctionalApproval : System.Web.UI.Page
{
    private AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();

    protected void Page_Load(object sender, EventArgs e)
    {



        
        if (!IsPostBack)
        {
            ButtonVisible();
            RadioTextValue();
            IniKpiTable();
            IniTable();
            if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
            {
                GetEmpinfo(Request.QueryString["EmpInfoId"]);
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
                    id_mastetID.Value = mid.ToString();
                    DataTable dt = _appPartA.GetAppraisalSelf(mid);

                    txt_employee.Text = dt.Rows[0]["employee"].ToString();
                    ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();


               //     txt_employee_OnTextChanged(txt_employee, (EventArgs)e);
                    DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid);
                    DataTable dt3 = _appPartA.GetAppraisalSelfB(mid);
                    gv_AppraisalFunc.DataSource = dt2;
                    ViewState["KPIFUNC"] = dt2;
                    ViewState["PARTB"] = dt3;
                    gv_AppraisalFunc.DataBind();
                    gv_AppraisalPartB.DataSource = dt3;
                    gv_AppraisalPartB.DataBind();
                    if (gv_AppraisalPartB.Rows.Count > 0)
                     {
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {



                            DropDownList ddlWeight =
                                (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");

                            ddlWeight.SelectedValue = dt3.Rows[i]["SetScore"].ToString();

                        }
                    }
                    CalculateTotal();
                    CalculateB();
                    CalculateBEHAVIORALTotal();

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
                    id_mastetID.Value = mid.ToString();
                    DataTable dt = _appPartA.GetAppraisalSelf(mid);

                //    txt_employee.Text = dt.Rows[0]["employee"].ToString();
                    ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                    HFCompanyId.Value = dt.Rows[0]["CompanyId"].ToString();

                    txt_employee_OnTextChanged(txt_employee, (EventArgs) e);
                    DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid);
                    DataTable dt3 = _appPartA.GetAppraisalSelfB(mid);
                    gv_AppraisalFunc.DataSource = dt2;
                    ViewState["KPIFUNC"] = dt2;
                    ViewState["PARTB"] = dt3;
                    gv_AppraisalFunc.DataBind();
                    gv_AppraisalPartB.DataSource = dt3;
                    gv_AppraisalPartB.DataBind();
                    if (gv_AppraisalPartB.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {



                            DropDownList ddlWeight =
                                (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");

                            ddlWeight.SelectedValue = dt3.Rows[i]["SetScore"].ToString();

                        }
                    }
                    CalculateTotal();
                    CalculateB(); CalculateBEHAVIORALTotal();
                }

                else
                {
                    GetEmpinfo(empId.ToString());
                   // ddlFinancialYear.SelectedValue = int.Parse(Request.QueryString["financialYearId"]);
                }
            }
        }
    }
    protected void CalculateBEHAVIORALTotal()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {



                DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");




                if (ddlWeight.SelectedValue == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(ddlWeight.SelectedValue.ToString());
                }





            }
            Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("ddllblTotalWeight");
            tst.Text = weightTotal.ToString();


        }
    } 
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


          
          
          
          
             if (Session["Status"].ToString() == "Edit")
             {
               
                //editButton.Visible = true;
            }
            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }
        else { Response.Redirect("AppraisalSupApprove.aspx"); }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
            dataRow["Text"] = "Return";
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
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();
        //actionRadioButtonList.Items[0].Selected = true;

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

                try
                {
                    dtCurrentTable.Rows[i]["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                 ? ""
                 : txtWeight.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeight"] = "0";

                }



                try
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = "0";
                    //throw;
                }

                try
                {
                    dtCurrentTable.Rows[i]["Target"] = txtTarget.Text.Trim().ToString() == ""
                 ? ""
                 : txtTarget.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["Target"] = "0";
                }

                try
                {
                    dtCurrentTable.Rows[i]["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtTargetPer.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["TargetPer"] = "0";
                }

                dtCurrentTable.Rows[i]["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
                try
                {
                    dtCurrentTable.Rows[i]["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["SelfMark"] = "0";
                }
            
            
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

            }

            gv_AppraisalFunc.DataSource = dtCurrentTable;
            gv_AppraisalFunc.DataBind();

            CalculateTotal();
            chkFunc_OnCheckedChanged(null, null);
        }
    }
     protected void ddlWeight_OnTextChanged(object sender, EventArgs e)
     {
         CalculateBEHAVIORALTotal();
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

        chkFunc_OnCheckedChanged(null, null);
    }



    //protected void btn_Save_OnClick(object sender, EventArgs e)
    //{

    //    if (Validation() == true)
    //    {
    //        List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

    //        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
    //        {
    //            TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
    //            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
    //            TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
    //            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
    //            TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
    //            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
    //            TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

    //            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

    //            if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
    //            {
    //                AppraisalFunctionalArea area = new AppraisalFunctionalArea();

    //                area.KpiInfo = tbKpi.Text.Trim().ToString();
    //                area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
    //                area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
    //                area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
    //                area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
    //                area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());

    //                area.SupervisorMark = 0;
    //                area.MidYearStatus = " ";

    //                functional.Add(area);
    //            }

    //        }


    //        AppraisalMaster aMaster = new AppraisalMaster();

    //        aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
    //        aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
    //        aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


    //        bool result = false;
    //        if (functional.Count > 0)
    //        {


    //            int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
    //            if (pk > 0)
    //            {
    //                result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
    //                result = SaveAppraisalSelfB(pk);
    //            }
    //        }
    //        else
    //        {
    //            result = false;
    //        }

    //        if (result == true)
    //        {
    //            _appPartA.UpdateStatus(id_mastetID.Value,
    //                "Approved", Session["UserId"].ToString(), DateTime.Now);
    //            //DataLoad();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(),
    //                    "alert",
    //                    "alert('" + "Approved" + " Successfully Done');window.location ='AppraisalSupApprove.aspx';",
    //                    true);
    //            //ScriptManager.RegisterStartupScript(this, this.GetType(),
    //            //   "alert",
    //            //   "alert('Operation Successful...');window.location ='AppraisalSelfList.aspx';",
    //            //   true);
    //        }
    //        else
    //        {
    //            AlertMessageBoxShow("Operation Failed");
    //        }
    //    }
    //}


    private bool SaveAppraisalSelfB(int pk)
    {

        bool result = false;
        List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            HiddenField hfSkillType = (HiddenField)gv_AppraisalPartB.Rows[i].FindControl("hfSkillType");
            TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
            TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
            TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");
            DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");


            if (txtSkillInfo.Text.Trim().ToString() != "")
            {
                AppraisalBehaveArea area = new AppraisalBehaveArea();
                area.AppraisalMasterId = pk;
                area.SkillType = hfSkillType.Value.Trim().ToString();
                area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();

                area.Score = Convert.ToDecimal(string.IsNullOrEmpty(txtScore.Text.Trim()) ? "0" : txtScore.Text.Trim());

                area.SetScore = Convert.ToDecimal(ddlWeight.SelectedValue);
                aList.Add(area);

            }


        }
        if (aList.Count > 0)
        {
            result = _appraisalPartBdal.SaveAppraisalSelfPartBSkillType(aList);
        }
        return result;
    }
    private void IniTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("SkillInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("SkillType", typeof(string)));
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
            dr["SkillType"] = "";

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
                HFCompanyId.Value = dtEmp.Rows[0]["CompanyId"].ToString();
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
                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

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
        try
        {
            DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(comp);
            ddlFinancialYear.DataSource = dt;
            ddlFinancialYear.DataValueField = "Value";
            ddlFinancialYear.DataTextField = "TextField";
            ddlFinancialYear.DataBind();
        }
        catch (Exception)
        {
            
           // throw;
        }
      
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
        
        dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        dr = dt.NewRow();

        dr["KpiInfo"] = "";
        dr["KpiWeight"] = "";
        dr["Target"] = "";
        dr["Deadline"] = "";
        dr["MidYearStatus"] = "";
       
        dr["SelfMark"] = "";
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
    protected void CalculateTotal()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
               
                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");


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
                aShowMessage.ShowMessageBox("Expected Number Required ", this);
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
        if (weightTotal < 75)
        {
            aShowMessage.ShowMessageBox("Total Weight Can Not be Smaller than 75 In Part A ", this);
            isVAlid = false;
        }

        Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");

        decimal weightTotal2 = tst.Text == "" ? 0 : Convert.ToDecimal(tst2.Text.ToString());
        if (weightTotal2 > 25)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Bigger than 25 In Part B ", this);
            isVAlid = false;
        }

        if (weightTotal2 < 25)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Smaller than 25 In Part B ", this);
            isVAlid = false;
        }


        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {



            DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");




            if (ddlWeight.SelectedValue == "")
            {

                aShowMessage.ShowMessageBox("Expected Number Can not be Empty!!! ", this);
                ddlWeight.Focus();
                isVAlid = false;
            }

        }


        return isVAlid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalSupApprove.aspx");
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

    protected void btn_Return_OnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
        {
            aShowMessage.ShowMessageBox("Return Remarks Required", this);
            ;
        }
        else
        {
            bool result = false;
            result = _appPartA.SaveAppraisalSelfApprove(Convert.ToInt32(id_mastetID.Value.Trim()), "Return",
                Session["LoginName"].ToString(), txtRemarks.Text.Trim());
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
                   true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        if(Session["EmpInfoId"].ToString()!= Session["ApproverEmpInfoId"].ToString())
        {
           
            Response.Redirect("../Default.aspx");

        }
        Session["ApproverEmpInfoId"] = null;
        if (Validation())
        {

            if (actionRadioButtonList.SelectedValue != "")
            {
                try
                {


                    DataTable dtFinalApprovalSubmit = new DataTable();
                    DataTable dtSuppervisorSubmit = new DataTable();
                    int FinalApproveCount = 0;

                    DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(id_Empid.Value);


                    DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + id_Empid.Value + "'");


                    String ddd = "";
                    try
                    {
                        ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    if (ddd == "True")
                    {


                    }
                    else if (dtempdataSup.Rows.Count > 0)
                    {


                        DataTable aDataTable = new DataTable();
                        aDataTable.Columns.Add("EmpInfoId");
                        aDataTable.Columns.Add("EmpName");
                        aDataTable.Columns.Add("EmpMasterCode");
                        //DataRow dataRow = null;
                        //dataRow = aDataTable.NewRow();
                        //dataRow["EmpInfoId"] = "0";
                        //dataRow["EmpName"] = "Please Select an Employee.....";
                        //dataRow["EmpMasterCode"] = "";
                        //aDataTable.Rows.Add(dataRow);
                        appDal.ReportingEmpData(id_Empid.Value, aDataTable);


                        dtSuppervisorSubmit = aDataTable;

                        for (int i = 0; i < dtSuppervisorSubmit.Rows.Count; i++)
                        {

                            dtFinalApprovalSubmit = _appPartA.GetFinalApproveByEmpId(id_Empid.Value, dtSuppervisorSubmit.Rows[i]["EmpInfoId"].ToString());
                            if (dtFinalApprovalSubmit.Rows.Count > 0)
                            {
                                FinalApproveCount = FinalApproveCount + 1;
                            }

                        }


                    }

                    DataTable dtdata = _appDashboard.GetSupervisorEmployeeAppIdCheck(id_Empid.Value);



                    if (dtdata.Rows.Count > 0 && ddd == "True")
                    {
                        AppraisalSelfAppLogDAO aMasterApp = new AppraisalSelfAppLogDAO();
                        aMasterApp.AppraisalSelfMasterId
                            = Convert.ToInt32(id_mastetID.Value);

                        if (actionRadioButtonList.SelectedValue == "Review")
                        {
                            aMasterApp.ActionStatus = "Review";

                            // Wrap the status update together with all downstream writes in this branch in one transaction
                            // (TransactionScope() defaults to joining the ambient transaction, so the nested scopes below
                            // enlist here too) so a failure anywhere rolls back the ActionStatus change as well.
                            using (TransactionScope outerScopeReview = new TransactionScope())
                            {
                            bool status = _appPartA.UpdateContractural(aMasterApp);
                            if (status)
                            {
                                if (aMasterApp.ActionStatus == "Review")
                                {
                                    if (status)
                                    {
                                        if (chkFunc.Checked && chkBehavioral.Checked)
                                        {
                                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                                                @"  <br/> Dear Sir, <br/>
Your KPI has been modified by Your Supervisor. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                                        }


                                        else if (chkFunc.Checked && chkBehavioral.Checked == false)
                                        {
                                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                                                @"  <br/> Dear Sir, <br/>
Your KPI has been modified by Your Supervisor. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                                        }


                                        else if (chkFunc.Checked == false && chkBehavioral.Checked)
                                        {
                                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                                                @"  <br/> Dear Sir, <br/>
Your KPI has been modified by Your Supervisor. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                                        }




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
                                                area.IsActive = true;
                                                area.SupervisorMark = 0;
                                                area.MidYearStatus = " ";

                                                functional.Add(area);
                                            }

                                        }


                                        AppraisalMaster aMaster = new AppraisalMaster();

                                        aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                                        aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                                        aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);
                                        aMaster.IsFunctionalArea = chkFunc.Checked;
                                        aMaster.IsBehavioralArea = chkBehavioral.Checked;


                                        bool result = false;
                                        if (functional.Count > 0)
                                        {
                                            int pk = 0;
                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                            pk = _appPartA.SaveAppraisalSelfMasterforSupper(aMaster, Session["UserId"].ToString());
                                            if (pk > 0)
                                            {








                                                result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                                                result = SaveAppraisalSelfB(pk);


                                                DataTable dtaa =
                                                    _appPartA.GetCheckApprisalAlreadyExist(Convert.ToInt32(id_mastetID.Value));
                                                if (dtaa.Rows.Count > 0)
                                                {
                                                    int AppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["AppraisalMasterId"].ToString());

                                                    _appPartA.DeleteAppraisalSetupNew(Convert.ToInt32(AppraisalMasterId));

                                                }


                                                //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                                                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                                {
                                                    ActionStatus = actionRadioButtonList.SelectedValue,
                                                    ApproveDate = DateTime.Now,
                                                    ApproveBy = Session["UserId"].ToString(),
                                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                                    ForEmpInfoId = 0,
                                                    AppraisalSelfMasterId = aMasterApp.AppraisalSelfMasterId,
                                                    Comments = commentsTextBox.Text,
                                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                                };
                                                int id = _appPartA.SaveEmpAppLog(appLogDao);
                                                _appPartA.SaveAppraisalMasterFromAppraisalSelf(
                                                    aMasterApp.AppraisalSelfMasterId.ToString());

                                                scope.Complete();
                                            }
                                            }

                                            if (pk > 0)
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                    "alert",
                                                    "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
                                                    true);
                                            }
                                        }


                                    }
                                }
                            }
                            outerScopeReview.Complete();
                            }
                        }
                        else
                        {

                            aMasterApp.ActionStatus = "Approved";

                            // Same reasoning as the Review branch above: wrap the status update with the rest of this
                            // branch's writes in one transaction so a failure anywhere rolls back everything together.
                            using (TransactionScope outerScopeApproved = new TransactionScope())
                            {
                            bool status = _appPartA.UpdateContractural(aMasterApp);

                            if (status)
                            {
                                //                        if (chkFunc.Checked && chkBehavioral.Checked)
                                //                        {
                                //                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                                //                                @"  <br/> Dear Sir, <br/>
                                //Your KPI has been modified by Your Supervisor. <br/><br/>
                                //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                                //<br/> Thank You.");

                                //                        }


                                //                        else if (chkFunc.Checked && chkBehavioral.Checked == false)
                                //                        {
                                //                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                                //                                @"  <br/> Dear Sir, <br/>
                                //Your KPI has been modified by Your Supervisor. <br/><br/>
                                //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                                //<br/> Thank You.");

                                //                        }


                                //                        else if (chkFunc.Checked == false && chkBehavioral.Checked)
                                //                        {
                                //                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                                //                                @"  <br/> Dear Sir, <br/>
                                //Your KPI has been modified by Your Supervisor. <br/><br/>
                                //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                                //<br/> Thank You.");

                                //                        }




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
                                        area.IsActive = true;
                                        area.SupervisorMark = 0;
                                        area.MidYearStatus = " ";

                                        functional.Add(area);
                                    }

                                }


                                AppraisalMaster aMaster = new AppraisalMaster();

                                aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                                aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                                aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);
                                aMaster.IsFunctionalArea = chkFunc.Checked;
                                aMaster.IsBehavioralArea = chkBehavioral.Checked;


                                bool result = false;
                                if (functional.Count > 0)
                                {
                                    int pk = 1;
                                    if (pk > 0)
                                    {








                                        //result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                                        //result = SaveAppraisalSelfB(pk);





                                        DataTable dtempdata = _appPartA.GetEmpInfoPrevious(Session["EmpInfoId"].ToString(),
                                            id_mastetID.Value);
                                        DataTable dtempdata2 =
                                            _appPartA.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(),
                                                id_mastetID.Value);

                                        DataTable dtempdata2empid = _appDashboard.GetEmpIdfromKPIInfo(
                             id_mastetID.Value);

                                        if (dtempdata2empid.Rows.Count > 0)
                                        {
                                            AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                            {
                                                ActionStatus = "Verified",
                                                ApproveDate = DateTime.Now,
                                                ApproveBy = Session["UserId"].ToString(),
                                                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                                ForEmpInfoId = Convert.ToInt32(dtempdata2empid.Rows[0]["EmpInfoId"].ToString()),
                                                AppraisalSelfMasterId = aMasterApp.AppraisalSelfMasterId,
                                                Comments = commentsTextBox.Text,
                                                CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                            };
                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                _appPartA.UpdateAppLog("Review", Session["AppLogId"].ToString());
                                                int id = _appPartA.SaveEmpAppLog(appLogDao);
                                                scope.Complete();
                                            }



                                            SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ",
                                                @"  <br/> Dear Sir, <br/>
Review your KPI. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");


                                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                "alert",
                                                "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
                                                true);
                                        }
                                    }
                                }

                            }
                            outerScopeApproved.Complete();
                            }
                        }
                    }
                    else if (dtdata.Rows.Count > 0 && FinalApproveCount == 1 && dtSuppervisorSubmit.Rows.Count > 0)
                    {
                        AppraisalSelfAppLogDAO aMasterApp = new AppraisalSelfAppLogDAO();
                        aMasterApp.AppraisalSelfMasterId
                            = Convert.ToInt32(id_mastetID.Value);




                        aMasterApp.ActionStatus = actionRadioButtonList.SelectedValue;

                        // Same reasoning as the two branches above: wrap the status update with the rest of this
                        // branch's writes in one transaction so a failure anywhere rolls back everything together.
                        using (TransactionScope outerScopeFinal = new TransactionScope())
                        {
                        bool status = _appPartA.UpdateContractural(aMasterApp);





                        if (status)
                        {
                            if (chkFunc.Checked && chkBehavioral.Checked)
                            {
                                SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ", @"  <br/> Dear Sir, <br/>
Your KPI has been modified by Your Supervisor. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                            }


                            else if (chkFunc.Checked && chkBehavioral.Checked == false)
                            {
                                SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ", @"  <br/> Dear Sir, <br/>
Your KPI has been modified by Your Supervisor. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                            }


                            else if (chkFunc.Checked == false && chkBehavioral.Checked)
                            {
                                SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ", @"  <br/> Dear Sir, <br/>
Your KPI has been modified by Your Supervisor. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                            }




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
                                    area.IsActive = true;
                                    area.SupervisorMark = 0;
                                    area.MidYearStatus = " ";

                                    functional.Add(area);
                                }

                            }


                            AppraisalMaster aMaster = new AppraisalMaster();

                            aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                            aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);
                            aMaster.IsFunctionalArea = chkFunc.Checked;
                            aMaster.IsBehavioralArea = chkBehavioral.Checked;


                            bool result = false;
                            if (functional.Count > 0)
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                int pk = _appPartA.SaveAppraisalSelfMasterforSupper(aMaster, Session["UserId"].ToString());
                                if (pk > 0)
                                {








                                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                                    result = SaveAppraisalSelfB(pk);
                                }
                                scope.Complete();
                                }
                            }


                            if (aMasterApp.ActionStatus == "Verified")
                            {
                                DataTable dtempdata = _appPartA.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                {
                                    ActionStatus = actionRadioButtonList.SelectedValue,
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                    AppraisalSelfMasterId = aMasterApp.AppraisalSelfMasterId,
                                    Comments = commentsTextBox.Text,
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                };
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    int id = _appPartA.SaveEmpAppLog(appLogDao);
                                    scope.Complete();
                                }
                            }
                            else if (aMasterApp.ActionStatus == "Approved")
                            {

                                DataTable dtaa = _appPartA.GetCheckApprisalAlreadyExist(Convert.ToInt32(id_mastetID.Value));


                                //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                {
                                    ActionStatus = actionRadioButtonList.SelectedValue,
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = 0,
                                    AppraisalSelfMasterId = aMasterApp.AppraisalSelfMasterId,
                                    Comments = commentsTextBox.Text,
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                };
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    if (dtaa.Rows.Count > 0)
                                    {
                                        int AppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["AppraisalMasterId"].ToString());

                                        _appPartA.DeleteAppraisalSetupNew(Convert.ToInt32(AppraisalMasterId));

                                    }

                                    int id = _appPartA.SaveEmpAppLog(appLogDao);
                                    _appPartA.SaveAppraisalMasterFromAppraisalSelf(aMasterApp.AppraisalSelfMasterId.ToString());
                                    scope.Complete();
                                }

                                SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ", @"  <br/> Dear Sir, <br/>
An Employee's KPI is waiting for your approval. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");


                            }
                            else if (aMasterApp.ActionStatus == "Review")
                            {
                                DataTable dtempdata = _appPartA.GetEmpInfoPrevious(Session["EmpInfoId"].ToString(), id_mastetID.Value);
                                DataTable dtempdata2 = _appPartA.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), id_mastetID.Value);

                                if (dtempdata2.Rows.Count > 0)
                                {
                                    AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                    {
                                        ActionStatus = "Verified",
                                        ApproveDate = DateTime.Now,
                                        ApproveBy = Session["UserId"].ToString(),
                                        PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                                        ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                                        AppraisalSelfMasterId = aMasterApp.AppraisalSelfMasterId,
                                        Comments = commentsTextBox.Text,
                                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                    };
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        _appPartA.UpdateAppLog("Review", Session["AppLogId"].ToString());
                                        int id = _appPartA.SaveEmpAppLog(appLogDao);
                                        scope.Complete();
                                    }



                                    SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ", @"  <br/> Dear Sir, <br/>
Review your KPI. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");
                                }
                                else
                                {
                                    ShowMessageBox("Please select Approval Status to Approved !!!");
                                }
                            }


                        }
                        outerScopeFinal.Complete();
                        }
                        Session["AppLogId"] = null;
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                   "alert",
                                   "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
                                   true);
                    }
                    else
                    {
                        AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");


                    }

                    //bool result = false;
                    //result = _appPartA.SaveAppraisalSelfApprove(Convert.ToInt32(id_mastetID.Value.Trim()), "Approved",
                    //    Session["LoginName"].ToString(), txtRemarks.Text.Trim());
                    //result = SaveApprovedAppraisal();
                    //if (result == true)
                    //{



                    //   // result = SaveAppraisalSelfApprove()


                    //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //       "alert",
                    //       "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
                    //       true);
                    //}
                    //else
                    //{
                    //    AlertMessageBoxShow("Operation Failed");
                    //}
                }
                catch (Exception)
                {
                    ShowMessageBox("This Employees final Approval Person has not been set yet. Please contact with HR Department.!!!");

                    //throw;
                }
            }
            else
            {
                ShowMessageBox("Please Select Approval Status!");
            }

        }
    }


    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }


    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    public bool SaveApprovedAppraisal()
    {
        bool result = false;
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
            TextBox selfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

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
                area.SelfMark = string.IsNullOrEmpty(selfMark.Text.Trim()) ? 0 : Convert.ToDecimal(selfMark.Text.Trim());
                area.SupervisorMark = 0;
                area.MidYearStatus = " ";

                functional.Add(area);
            }

        }


        AppraisalMaster aMaster = new AppraisalMaster();

        aMaster.AppraisalSelfMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
        aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
        aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
        int pk = _appPartA.SaveAppraisalMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialFunctionalDetails(functional, pk, aMaster.AppraisalSelfMasterId);
                   
                }
        return result;
    }


    protected void chkFunc_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkFunc.Checked)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                CalendarExtender CalendarExtender2 = (CalendarExtender)gv_AppraisalFunc.Rows[i].FindControl("CalendarExtender2");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
                LinkButton btn_Add = (LinkButton)gv_AppraisalFunc.Rows[i].FindControl("btn_Add");
                LinkButton lb_Remove = (LinkButton)gv_AppraisalFunc.Rows[i].FindControl("lb_Remove");

                tbKpi.ReadOnly = false;
                txtWeight.ReadOnly = false;
                txtWeightPer.ReadOnly = false;
                txtTarget.ReadOnly = false;
                txtTargetPer.ReadOnly = false;
                txtDeadLine.ReadOnly = false;
                txtMidStatus.ReadOnly = false;
              //  CalendarExtender2.Enabled = true;
                txtDeadLine.Enabled = true;
                btn_Add.Enabled = true;
                lb_Remove.Enabled = true;

            }
      
        }
        else
        {
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
                CalendarExtender CalendarExtender2 = (CalendarExtender)gv_AppraisalFunc.Rows[i].FindControl("CalendarExtender2");
                LinkButton btn_Add = (LinkButton)gv_AppraisalFunc.Rows[i].FindControl("btn_Add");
                LinkButton lb_Remove = (LinkButton)gv_AppraisalFunc.Rows[i].FindControl("lb_Remove");
                tbKpi.ReadOnly = true;
                txtWeight.ReadOnly = true;
                txtWeightPer.ReadOnly = true;
                txtTarget.ReadOnly = true;
                txtTargetPer.ReadOnly = true;
                txtDeadLine.ReadOnly = true;
                txtDeadLine.Enabled = false;
                txtMidStatus.ReadOnly = true;
                btn_Add.Enabled = false;
                lb_Remove.Enabled = false;

            }
        }
     
    }

    protected void chkBehavioral_OnCheckedChanged(object sender, EventArgs e)
    {

        if (chkBehavioral.Checked)
        {

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox) gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox) gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");

                txtSkillInfo.ReadOnly = false;
                txtSupportingEmp.ReadOnly = false;
                ddlWeight.Enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");

                txtSkillInfo.ReadOnly = true;
                txtSupportingEmp.ReadOnly = true;
                ddlWeight.Enabled = false;
            }
        }
    }

    protected void btn_Draft_OnClick(object sender, EventArgs e)
    {

        if (Validation() == true)
        {

            int empId = int.Parse(Request.QueryString["EmpInfoId"]);

            int FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


            //DataTable dtEmp = _appPartA.CheckAlreadyExist(Convert.ToInt32(empId), FinancialYearId,
            //    Convert.ToInt32(HFCompanyId.Value));
            //if (dtEmp.Rows.Count > 0)
            //{
            //    aShowMessage.ShowMessageBox("Already Exist !!!", this);

            //}
            //else
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
                        area.IsActive = true;
                        area.SupervisorMark = 0;
                        area.MidYearStatus = " ";

                        functional.Add(area);
                    }

                }


                AppraisalMaster aMaster = new AppraisalMaster();

                aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);
                aMaster.IsFunctionalArea = chkFunc.Checked;
                aMaster.IsBehavioralArea = chkBehavioral.Checked;


                bool result = false;
                if (functional.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                    int pk = _appPartA.SaveAppraisalSelfMasterforSupper(aMaster, Session["UserId"].ToString());
                    if (pk > 0)
                    {








                        result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                        result = SaveAppraisalSelfB(pk);
                    }

                    if (result)
                    {
                        scope.Complete();
                    }
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
                        "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
                        true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }
    }
}