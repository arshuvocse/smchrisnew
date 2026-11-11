using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_EmpDetails_Sap : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();


    EMpInfoSapDal aDal = new EMpInfoSapDal();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // IniKpiTable();
           // IniTable();
            if (Request.QueryString["EmpInfoId"] != null)
            {

               // EmpCode.Value = Request.QueryString["EmpInfoId"].ToString();

                GetEmpinfo(Request.QueryString["EmpInfoId"]);
            }          
        }
    }

    public void GetEmpinfo(string id)
    {
        DataTable dtEmp = aDal.Get_EmpAll_ById(id);
        if (dtEmp.Rows.Count > 0)
        {

            EmpCode.Value = dtEmp.Rows[0]["Pernr"].ToString().Trim();

            SapAction.Value = dtEmp.Rows[0]["Action"].ToString().Trim();
       
            lblEmployee.Text = dtEmp.Rows[0]["DivId"].ToString().Trim();
            deptNameLabel.Text = dtEmp.Rows[0]["DeptId"].ToString().Trim();
       
            desigNameLabel.Text = dtEmp.Rows[0]["DesigId"].ToString().Trim();

            EmpCat.Text = dtEmp.Rows[0]["EmpCat"].ToString().Trim();
            FName.Text = dtEmp.Rows[0]["FName"].ToString().Trim();

            FDob.Text = dtEmp.Rows[0]["FDob"].ToString().Trim();

            FDType.Text = dtEmp.Rows[0]["FDType"].ToString().Trim();
            MName.Text = dtEmp.Rows[0]["MName"].ToString().Trim();

            MDob.Text = dtEmp.Rows[0]["MDob"].ToString().Trim();

            FDod.Text = dtEmp.Rows[0]["FDod"].ToString().Trim();
            MDod.Text = dtEmp.Rows[0]["MDod"].ToString().Trim();


            MDType.Text = dtEmp.Rows[0]["MDType"].ToString().Trim();
            Mobile.Text = dtEmp.Rows[0]["Mobile"].ToString().Trim();

            Email.Text = dtEmp.Rows[0]["Email"].ToString().Trim();
            JobLocId.Text = dtEmp.Rows[0]["JobLocId"].ToString().Trim();

            Office.Text = dtEmp.Rows[0]["Office"].ToString().Trim();
            SalGrade.Text = dtEmp.Rows[0]["SalGrade"].ToString();
            SalStep.Text = dtEmp.Rows[0]["SalStep"].ToString();

            EmpTtype.Text = dtEmp.Rows[0]["EmpTypeId"].ToString().Trim();
            SectionId.Text = dtEmp.Rows[0]["SectionId"].ToString().Trim();
            ReportId.Text = dtEmp.Rows[0]["ReportId"].ToString().Trim();

            kpiApprId.Text = dtEmp.Rows[0]["kpiApprId"].ToString().Trim();
            PreviousEmpCode.Text = dtEmp.Rows[0]["PreviousEmpCode"].ToString().Trim();
  
            gv_AppraisalFunc.DataSource = dtEmp;
            gv_AppraisalFunc.DataBind();


            DataTable myTable = aDal.Check_EmpAll_ById(id);
            if (myTable.Rows.Count > 0)
            {
                SapDivi.Value = myTable.Rows[0]["SAP_Code"].ToString().Trim();
                SapDepartment.Value = myTable.Rows[0]["DepartmentId"].ToString().Trim();
                SapSection.Value = myTable.Rows[0]["sapSectionId"].ToString().Trim();
                SapDesig.Value = myTable.Rows[0]["sapDesignationId"].ToString().Trim();
                SapJobLocationID.Value = myTable.Rows[0]["SapJobLocationID"].ToString().Trim();
                SapSalaryLoationId.Value = myTable.Rows[0]["SapSalaryLoationId"].ToString().Trim();
                SapSalGrade.Value = myTable.Rows[0]["SaPSalGrade"].ToString().Trim();
                SapEmpCategoryId.Value = myTable.Rows[0]["Empcategory"].ToString().Trim();
                SApEmpTypeId.Value = myTable.Rows[0]["EmpType"].ToString().Trim();   
            }


            DataTable aTable = aDal.Get_Child_ById(dtEmp.Rows[0]["EmpId"].ToString());
            if (aTable.Rows.Count > 0)
            {
                GridView1.DataSource = aTable;
                GridView1.DataBind();
            }
       
        }

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
    private void CalculateBPartBSUP()
    {
        decimal TWeight = 0;
        decimal TSetScore = 0;
        decimal TSelfScore = 0;
        decimal TSupervisorScore = 0;

        
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                
            }
            else if (Session["Status"].ToString() == "Edit")
            {
            }
            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }

    }


    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] == null)
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
            dr["KpiWeightPer"] = "";
            dr["Target"] = "";
            dr["TargetPer"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";

            dr["SelfMark"] = "";
            dt.Rows.Add(dr);
            ViewState["KPIFUNC"] = dt;

            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["KPIFUNC"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["KPIFUNC"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

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

              //  joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");



            }
        }
        else
        {
            txt_employee.Text = "";

           
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

            //throw;
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
        dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
        dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        dr = dt.NewRow();

        dr["KpiInfo"] = "";
        dr["KpiWeight"] = "";
        dr["Target"] = "";
        dr["Deadline"] = "";
        dr["MidYearStatus"] = "";
        dr["IsActive"] = "False";

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


        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["PARTB"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["PARTB"] = dtCurrentTable;


            CalculateB();

        }
    }

    private void CalculateB()
    {
        decimal weightTotal = 0;
        decimal SetScore = 0;

    

    }
    protected void CalculateTotal()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                Label txtWeight = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

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
      
    }



    public bool Validation()
    {
        bool isVAlid = true;
        if (gv_AppraisalFunc.Rows.Count <= 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Kpi Info Required ", this);

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
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
            TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
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
    

        Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");

        decimal weightTotal = tst.Text == "" ? 0 : Convert.ToDecimal(tst.Text.ToString());
        if (weightTotal > 75)
        {
            aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than 75 In Part A ", this);
            isVAlid = false;
        }
       
        return isVAlid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfo_Sap.aspx");
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


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                   
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

    private decimal TotalWeight()
    {
        decimal totalWeight = 0;
     
        return totalWeight;
    }
    private decimal TotalResultEnd()
    {
        decimal result = 0;
   
        return result;
    }
    private decimal TotalSelfMark()
    {
        decimal result = 0;
   
        return result;
    }
    private decimal TotalsupppMark()
    {
        decimal result = 0;
   
        return result;
    }
    private decimal TotalTarget()
    {
        decimal result = 0;
    
        return result;
    }
    protected void gv_AppraisalFunc_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalWeight");
            //Label lblresult = (Label)e.Row.FindControl("lblresultend");
            Label lblselfMark = (Label)e.Row.FindControl("lblselfMark");
            Label lbltarget = (Label)e.Row.FindControl("lbltarget");
            Label lblTotalMark = (Label)e.Row.FindControl("lblTotalMark");
            lbl.Text = TotalWeight().ToString();
            //lblresult.Text = TotalResultEnd().ToString();
            lblselfMark.Text = TotalSelfMark().ToString();
            lbltarget.Text = TotalTarget().ToString();


            lblTotalMark.Text = TotalsupppMark().ToString();

        }
    }

    private decimal TotalWeight1()
    {
        decimal result = 0;
    
        return result;
    }
    protected void gv_AppraisalFunc1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblselfscrore = (Label)e.Row.FindControl("lblselfscrore");

            lblselfscrore.Text = TotalWeight1().ToString();

        }
    }

    protected void recommend_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }



    protected void btn_save_OnClick(object sender, EventArgs e)
    {

        if (SapAction.Value == "HR")
        {
                   
            if (Validation_Sap())
            {
                bool Status = aDal.Get_Add_ToSystem(EmpCode.Value);

                if (Status)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                               "alert",
                               "alert('Operation Successfully Done...');window.location ='EmployeeInfo_Sap.aspx';",
                               true);
                }
            }
        }
        else
        {
            AlertMessageBoxShow("This is not proper action to submit");
        }
    }


    private bool Validation_Sap()
    {
      

        if (SapDivi.Value == "")
        {
            AlertMessageBoxShow("Division Not Match");
            return false;
        }

        if (SapDepartment.Value == "")
        {
            AlertMessageBoxShow("Department Not Match");
            return false;
        }

        //if (SapSection.Value == "")
        //{
        //    AlertMessageBoxShow("Section Not Match");
        //    return false;
        //}

        //if (SapDesig.Value == "")
        //{
        //    AlertMessageBoxShow("Designation Not Match");
        //    return false;
        //}

        //if (SapJobLocationID.Value == "")
        //{
        //    AlertMessageBoxShow("JOb Location Not Match");
        //    return false;
        //}

        if (SapSalaryLoationId.Value == "")
        {
            AlertMessageBoxShow("Salary Location Not Match");
            return false;
        }


        //if (SapSalGrade.Value == "")
        //{
        //    AlertMessageBoxShow("Salary Grade Not Match");
        //    return false;
        //}


        //if (SapEmpCategoryId.Value == "")
        //{
        //    AlertMessageBoxShow("Emp Category Not Match");
        //    return false;
        //}

        if (SApEmpTypeId.Value  == "")
        {
            AlertMessageBoxShow("Emp Type Not Match");
            return false;
        }
      
        return true;
    }

}