using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_BudgetWiseAllocation : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private int mid = 0;
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {
                    DataTable dt = _trainingDal.GetBudgetAllocationMaster(Convert.ToInt32(mid));

                    ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                    ddlCompany_SelectedIndexChanged(ddlCompany, (EventArgs)e);
                    ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();

                    ddlFinancialYear_SelectedIndexChanged(ddlFinancialYear, (EventArgs)e);
                    ddlTraining.SelectedValue = dt.Rows[0]["TrainingBudgetMasterId"].ToString();

                    quater.Value = dt.Rows[0]["Quater"].ToString();
                    bool forDpt = dt.Rows[0]["ForDepartment"].ToString() == "True" ? true : false;
                    bool forGrd = dt.Rows[0]["ForGrade"].ToString() == "True" ? true : false;
                    bool forEmp = dt.Rows[0]["ForEmployee"].ToString() == "True" ? true : false;
                    if (forDpt == true)
                    {
                        //  gv_DptDetails.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
                        DataTable dtDpt = _trainingDal.GetTrainingDetailsDptByMasterQuater(Convert.ToInt32(dt.Rows[0]["TrainingBudgetMasterId"].ToString()), dt.Rows[0]["Quater"].ToString());
                        ViewState["DptDetails"] = dtDpt;
                        gv_DptDetails.DataSource = dtDpt;
                        gv_DptDetails.DataBind();
                        forSector.Value = "Department";
                       
                        DataTable dtemp = _trainingDal.GetEmployeeByAlloationMasterDpt(Convert.ToInt32(hdpk.Value));

                        ViewState["EmpAllocate"] = dtemp;
                        gv_allocateEmp.DataSource = dtemp;
                        gv_allocateEmp.DataBind();
                        dptAllocation.Visible = true;



                        //DataTable dtOld = _trainingDal.GetEmployeeByDptId(Convert.ToInt32(dtDpt.Rows[0]["departmentId"]));

                        //gv_EmpDetails.DataSource = dtOld;
                        //gv_EmpDetails.DataBind();




                      
                     


                    }

                    if (forGrd == true)
                    {
                        DataTable dtDpt = _trainingDal.GetTrainingDetailsGradeByMasterQuater(Convert.ToInt32(dt.Rows[0]["TrainingBudgetMasterId"].ToString()), dt.Rows[0]["Quater"].ToString());

                        ViewState["GrdDetails"] = dtDpt;
                        gv_GrdDetails.DataSource = dtDpt;
                        gv_GrdDetails.DataBind();
                        forSector.Value = "Grade";

                        //quater.Value = dt.Rows[0]["quater"].ToString();
                        DataTable dtemp = _trainingDal.GetEmployeeByAlloationMasterGrade(Convert.ToInt32(hdpk.Value));

                        ViewState["EmpAllocate"] = dtemp;
                        gv_allocateEmp.DataSource = dtemp;
                        gv_allocateEmp.DataBind();
                        dptAllocation.Visible = true;

                    }
                    if (forEmp == true)
                    {

                        DataTable dtDpt = _trainingDal.GetEmployeeByTrainingMasterandQuater(Convert.ToInt32(dt.Rows[0]["TrainingBudgetMasterId"].ToString()), dt.Rows[0]["Quater"].ToString());

                       // ViewState["GrdDetails"] = dtDpt;
                        gv_EmpDetails.DataSource = dtDpt;
                        gv_EmpDetails.DataBind();
                        forSector.Value = "Employee";
                        DataTable dtemp = _trainingDal.GetEmployeeByAlloationMasterDpt(Convert.ToInt32(hdpk.Value));

                        ViewState["EmpAllocate"] = dtemp;
                        gv_allocateEmp.DataSource = dtemp;
                        gv_allocateEmp.DataBind();
                        dptAllocation.Visible = true;
                    }


                }
            }
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
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }
    protected void lb_Allocate_Click(object sender, EventArgs e)
    {

        

        for (int i = 0; i < gv_DptDetails.Rows.Count; i++)
        {
            GridViewRow gvRowL = gv_DptDetails.Rows[i];
            gvRowL.BackColor = System.Drawing.Color.White;
        }

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Label txt_Quater = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_quaterId");
        Label txt_QuaterText = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_Quater");
        Label txt_Month = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_Month");
        Label txt_department = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_department");
        HiddenField deptId = (HiddenField)gv_DptDetails.Rows[rowID].FindControl("deptId");
        HiddenField finYear = (HiddenField)gv_DptDetails.Rows[rowID].FindControl("finYear");
        Label txt_fromDate = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_fromDate");
        Label txt_toDate = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_toDate");
        Label txt_Qty = (Label)gv_DptDetails.Rows[rowID].FindControl("txt_Qty");
        HiddenField txtDetailsId = (HiddenField)gv_DptDetails.Rows[rowID].FindControl("gv_budgetDetailsID");

        dptAllocation.Visible = true;
        lbl_DptName.Text = "Allcation For " + txt_department.Text + "  Department";
        lbl_maxQty.Text = "Maximum Qty: " + txt_Qty.Text + "";
        budgetDetailsID.Value = txtDetailsId.Value;
        maxQtyValue.Text = txt_Qty.Text;
        quater.Value = txt_Quater.Text;
        quaterText.Value = txt_QuaterText.Text;
        gvRow.BackColor = System.Drawing.Color.Cyan;


        if (ViewState["EmpAllocate"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["EmpAllocate"];
            string dtQuatr = dt1.Rows[0]["Quater"].ToString();
          

          
            if (quater.Value.ToString() != dtQuatr )
            {
                dt1 = null;
                ViewState["EmpAllocate"] = null;
                gv_allocateEmp.DataSource = dt1;
                gv_allocateEmp.DataBind();
            }
            else
            {
                int total = 0;
                for (int i = 0; i < gv_DptDetails.Rows.Count; i++)
                {
                    Label qtylbl = (Label)gv_DptDetails.Rows[i].FindControl("txt_Qty");
                    Label quaterLbl = (Label)gv_DptDetails.Rows[i].FindControl("txt_Quater");
                    string qty = qtylbl.Text.ToString();
                    string quaterDe = quaterLbl.Text.ToString();
                    if (quaterDe == dtQuatr)
                    {
                        total = total + Convert.ToInt32(qty);
                    }
                }
                lbl_maxQty.Text = "Maximum Qty: " + total.ToString() + "";
                maxQtyValue.Text = total.ToString();
            }
            
        }

        DataTable dt = _trainingDal.GetEmployeeByDptId(Convert.ToInt32(deptId.Value));

        gv_EmpDetails.DataSource = dt;
        gv_EmpDetails.DataBind();
    }
    protected void dpt_AddEmployee_Click(object sender, EventArgs e)
    {

        if (ViewState["EmpAllocate"] == null) {

         

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("quater", typeof(string)));
            dt.Columns.Add(new DataColumn("quaterId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpMasterCode", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
            dt.Columns.Add(new DataColumn("GradeName", typeof(string)));
            dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("DetailsId", typeof(string)));

            if (forSector.Value == "Employee") {
                for (int i = 0; i < gv_EmpDetails2.Rows.Count; i++)
                {

                    CheckBox lb_empCheck = (CheckBox)gv_EmpDetails2.Rows[i].FindControl("lb_empCheck");
                    Label txt_empCode = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_empCode");
                    Label txt_employee = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_employee");
                    Label txt_designation = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_dptName");
                    Label txt_grdName = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_grdName");
                    HiddenField empInfoId = (HiddenField)gv_EmpDetails2.Rows[i].FindControl("empInfoId");
                    HiddenField detailsId = (HiddenField)gv_EmpDetails2.Rows[i].FindControl("detailsId");




                    if (lb_empCheck.Checked == true)
                    {
                        DataRow dr = null;
                        dr = dt.NewRow();

                        dr["quater"] = quaterText.Value;
                        dr["quaterId"] = quater.Value;
                        dr["EmpMasterCode"] = txt_empCode.Text.ToString();
                        dr["EmpInfoId"] = empInfoId.Value;
                        dr["EmpName"] = txt_employee.Text.ToString();
                        dr["GradeName"] = txt_grdName.Text.ToString();
                        dr["DepartmentName"] = txt_dptName.Text.ToString();
                        dr["Designation"] = txt_designation.Text.ToString();
                        dr["DetailsId"] = detailsId.Value.ToString();
                        dt.Rows.Add(dr);

                    }



                }

                ViewState["EmpAllocate"] = dt;

                gv_allocateEmp.DataSource = dt;
                gv_allocateEmp.DataBind();
            }
            else
            {

                int totalCount = gv_EmpDetails.Rows.Cast<GridViewRow>()
  .Count(r => ((CheckBox)r.FindControl("lb_empCheck")).Checked);
                int maxQty = maxQtyValue.Text.ToString() == "" ? 0 : Convert.ToInt32(maxQtyValue.Text.ToString());

                if (totalCount > maxQty)
                {
                    aShowMessage.ShowMessageBox("According to budget  max " + maxQty + "  Employee  allowed for allocation", this);
                    return;
                }
                for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
                {

                    CheckBox lb_empCheck = (CheckBox)gv_EmpDetails.Rows[i].FindControl("lb_empCheck");
                    Label txt_empCode = (Label)gv_EmpDetails.Rows[i].FindControl("txt_empCode");
                    Label txt_employee = (Label)gv_EmpDetails.Rows[i].FindControl("txt_employee");
                    Label txt_designation = (Label)gv_EmpDetails.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_EmpDetails.Rows[i].FindControl("txt_dptName");
                    Label txt_grdName = (Label)gv_EmpDetails.Rows[i].FindControl("txt_grdName");
                    HiddenField empInfoId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("empInfoId");




                    if (lb_empCheck.Checked == true)
                    {
                        DataRow dr = null;
                        dr = dt.NewRow();

                        dr["quater"] = quaterText.Value;
                        dr["quaterId"] = quater.Value;
                        dr["EmpMasterCode"] = txt_empCode.Text.ToString();
                        dr["EmpInfoId"] = empInfoId.Value;
                        dr["EmpName"] = txt_employee.Text.ToString();
                        dr["GradeName"] = txt_grdName.Text.ToString();
                        dr["DepartmentName"] = txt_dptName.Text.ToString();
                        dr["Designation"] = txt_designation.Text.ToString();
                        dr["DetailsId"] = budgetDetailsID.Value.ToString();
                        dt.Rows.Add(dr);

                    }



                }

                ViewState["EmpAllocate"] = dt;

                gv_allocateEmp.DataSource = dt;
                gv_allocateEmp.DataBind();
            }
            
        }
        else
        {
        
            int maxQty = maxQtyValue.Text.ToString() == "" ? 0 : Convert.ToInt32(maxQtyValue.Text.ToString());
            int totalCount = gv_EmpDetails.Rows.Cast<GridViewRow>()
                    .Count(r => ((CheckBox)r.FindControl("lb_empCheck")).Checked);
            int rowCount = gv_allocateEmp.Rows.Count;
            int checkGrid = gv_allocateEmp.Rows.Count + 1;
            int checkCheckCount = gv_allocateEmp.Rows.Count  + totalCount;

            if ( gv_allocateEmp.Rows.Count + 1 > maxQty )
            {
                aShowMessage.ShowMessageBox("According to budget  max "+maxQty+"  Employee  allowed for allocation", this);
                return ;
            }

            //if ((gv_allocateEmp.Rows.Count + totalCount) > (gv_allocateEmp.Rows.Count + 1))
            //{
            //    aShowMessage.ShowMessageBox("According to budget  max " + maxQty + "  Employee  allowed for allocation", this);
            //    return;
            //}
            DataTable dt = (DataTable)ViewState["EmpAllocate"];
            if (forSector.Value == "Employee") {
              //  DataTable dt = (DataTable)ViewState["EmpAllocate"];

                for (int i = 0; i < gv_EmpDetails2.Rows.Count; i++)
                {



                    CheckBox lb_empCheck = (CheckBox)gv_EmpDetails2.Rows[i].FindControl("lb_empCheck");
                    Label txt_empCode = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_empCode");
                    Label txt_employee = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_employee");
                    Label txt_designation = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_dptName");
                    Label txt_grdName = (Label)gv_EmpDetails2.Rows[i].FindControl("txt_grdName");
                    HiddenField empInfoId = (HiddenField)gv_EmpDetails2.Rows[i].FindControl("empInfoId");
                    HiddenField detailsId = (HiddenField)gv_EmpDetails2.Rows[i].FindControl("detailsId");

                  
                    if (lb_empCheck.Checked == true)
                    {

                        if (EmployeeExistValidation(Convert.ToInt32(empInfoId.Value.ToString())) == false)
                        {
                            aShowMessage.ShowMessageBox("Employee  Already Exists in  List", this);
                            return;
                        }
                        DataRow dr = null;
                        dr = dt.NewRow();

                        dr["quater"] = quaterText.Value;
                        dr["quaterId"] = quater.Value;
                        dr["EmpMasterCode"] = txt_empCode.Text.ToString();
                        dr["EmpInfoId"] = empInfoId.Value;
                        dr["EmpName"] = txt_employee.Text.ToString();
                        dr["GradeName"] = txt_grdName.Text.ToString();
                        dr["DepartmentName"] = txt_dptName.Text.ToString();
                        dr["Designation"] = txt_designation.Text.ToString();
                        dr["DetailsId"] = detailsId.Value.ToString();
                        dt.Rows.Add(dr);

                    }
                    ViewState["EmpAllocate"] = dt;



                }
            
            }
            else {

              

                for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
                {



                    CheckBox lb_empCheck = (CheckBox)gv_EmpDetails.Rows[i].FindControl("lb_empCheck");
                    Label txt_empCode = (Label)gv_EmpDetails.Rows[i].FindControl("txt_empCode");
                    Label txt_employee = (Label)gv_EmpDetails.Rows[i].FindControl("txt_employee");
                    Label txt_designation = (Label)gv_EmpDetails.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_EmpDetails.Rows[i].FindControl("txt_dptName");
                    Label txt_grdName = (Label)gv_EmpDetails.Rows[i].FindControl("txt_grdName");
                    HiddenField empInfoId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("empInfoId");
                   
                    
                    
                    //if (EmployeeExistValidation(Convert.ToInt32(empInfoId.Value.ToString())) == false)
                    //{
                    //    aShowMessage.ShowMessageBox("Employee  Already Exists in  List", this);
                    //    return;
                    //}
                    if (lb_empCheck.Checked == true)
                    {

                        if (EmployeeExistValidation(Convert.ToInt32(empInfoId.Value.ToString())) == false)
                        {
                            aShowMessage.ShowMessageBox("Employee  Already Exists in  List", this);
                            return;
                        }
                        DataRow dr = null;
                        dr = dt.NewRow();

                        dr["quater"] = quaterText.Value;
                        dr["quaterId"] = quater.Value;
                        dr["EmpMasterCode"] = txt_empCode.Text.ToString();
                        dr["EmpInfoId"] = empInfoId.Value;
                        dr["EmpName"] = txt_employee.Text.ToString();
                        dr["GradeName"] = txt_grdName.Text.ToString();
                        dr["DepartmentName"] = txt_dptName.Text.ToString();
                        dr["Designation"] = txt_designation.Text.ToString();
                        dr["DetailsId"] = budgetDetailsID.Value.ToString();
                        dt.Rows.Add(dr);

                    }



                }
               

            }
            ViewState["EmpAllocate"] = dt;

            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();
            

           

        }
             
    }
    protected void grd_alloate_Click1(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_GrdDetails.Rows.Count; i++)
        {
            GridViewRow gvRowL = gv_GrdDetails.Rows[i];
            gvRowL.BackColor = System.Drawing.Color.White;
        }

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Label txt_Quater = (Label)gv_GrdDetails.Rows[rowID].FindControl("txt_quaterId");
        Label txt_QuaterText = (Label)gv_GrdDetails.Rows[rowID].FindControl("txt_Quater");
        HiddenField txtDetailsId = (HiddenField)gv_GrdDetails.Rows[rowID].FindControl("gv_budgetDetailsID");
        HiddenField gradeId = (HiddenField)gv_GrdDetails.Rows[rowID].FindControl("gradeId");
        Label txt_Qty = (Label)gv_GrdDetails.Rows[rowID].FindControl("txt_Qty");
        Label txt_department = (Label)gv_GrdDetails.Rows[rowID].FindControl("txt_department");

        string a = gradeId.Value;
        quater.Value = txt_Quater.Text.ToString();
        budgetDetailsID.Value = txtDetailsId.Value;
        if (ViewState["EmpAllocate"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["EmpAllocate"];
            string dtQuatr = dt1.Rows[0]["Quater"].ToString();
            if (quater.Value.ToString() != dtQuatr)
            {
                dt1 = null;
                ViewState["EmpAllocate"] = null;
                gv_allocateEmp.DataSource = dt1;
                gv_allocateEmp.DataBind();
            }
            else
            {
                int total = 0;
                for (int i = 0; i < gv_DptDetails.Rows.Count; i++)
                {
                    Label qtylbl = (Label)gv_GrdDetails.Rows[i].FindControl("txt_Qty");
                    Label quaterLbl = (Label)gv_GrdDetails.Rows[i].FindControl("txt_Quater");
                    string qty = qtylbl.Text.ToString();
                    string quaterDe = quaterLbl.Text.ToString();
                    if (quaterDe == dtQuatr)
                    {
                        total = total + Convert.ToInt32(qty);
                    }
                }
                lbl_maxQty.Text = "Maximum Qty: " + total.ToString() + "";
                maxQtyValue.Text = total.ToString();
            }

        }


        DataTable dt = _trainingDal.GetEmployeeByGradeId(Convert.ToInt32(gradeId.Value));

        gv_EmpDetails.DataSource = dt;
        gv_EmpDetails.DataBind();



        dptAllocation.Visible = true;
        lbl_DptName.Text = "Allcation For " + txt_department.Text + "  Grade";
        lbl_maxQty.Text = "Maximum Qty: " + txt_Qty.Text + "";
        maxQtyValue.Text = txt_Qty.Text.ToString(); ;
        budgetDetailsID.Value = txtDetailsId.Value;
        quater.Value = txt_Quater.Text;
        quaterText.Value = txt_QuaterText.Text;
        gvRow.BackColor = System.Drawing.Color.Cyan; ;
    }
    protected void emp_alloate_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_BudEmp.Rows.Count; i++)
        {
            GridViewRow gvRowL = gv_BudEmp.Rows[i];
            gvRowL.BackColor = System.Drawing.Color.White;
        }

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Label txt_Quater = (Label)gv_BudEmp.Rows[rowID].FindControl("txt_quaterId");
        Label txt_QuaterText = (Label)gv_BudEmp.Rows[rowID].FindControl("txt_Quater");

        HiddenField txtDetailsId = (HiddenField)gv_BudEmp.Rows[rowID].FindControl("trainingBudgetMasterId");
        Label txt_Qty = (Label)gv_BudEmp.Rows[rowID].FindControl("txt_Qty");


        string a = txt_Quater.Text;

        dptAllocation.Visible = true;
        lbl_DptName.Text = "Allcation For Employee ";
        lbl_maxQty.Text = "Maximum Qty: " + txt_Qty.Text + "";
        maxQtyValue.Text = txt_Qty.Text.ToString();
        budgetDetailsID.Value = txtDetailsId.Value;
        quater.Value = txt_Quater.Text;
        quaterText.Value = txt_QuaterText.Text;
        gvRow.BackColor = System.Drawing.Color.Cyan;

        if (ViewState["EmpAllocate"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["EmpAllocate"];
            string dtQuatr = dt1.Rows[0][0].ToString();
            if (quater.Value.ToString() != dtQuatr)
            {
                dt1 = null;
                ViewState["EmpAllocate"] = null;
                gv_allocateEmp.DataSource = dt1;
                gv_allocateEmp.DataBind();
            }

        }

        DataTable dt = _trainingDal.GetTrainingBudgetEmployeeDetails(Convert.ToInt32(budgetDetailsID.Value), txt_Quater.Text.ToString());

        gv_EmpDetails2.DataSource = dt;
        gv_EmpDetails2.DataBind();
    }
    private bool EmployeeExistValidation(int empId)
    {
        bool result = true;
        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            HiddenField empInfoId = (HiddenField)gv_allocateEmp.Rows[i].FindControl("empInfoId");

            int id = Convert.ToInt32(empInfoId.Value.ToString());
            if (id == empId)
            {

                result =  false;
                break;
            }
            

        }
        return result;
    }
    private void EmployeeAddValidation()
    {
        //string employee = txt_employee.Text.ToString().Trim();
        //if (employee == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Select Employee", this);

        //    return false;
        //}
        //if (ViewState["EmpDetails"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["EmpDetails"];
        //    DataTable emp = _trainingDal.GetEmployeeDetailsByMasterCode(employee.Substring(0,6));
        //    string empId = emp.Rows[0]["EmpInfoId"].ToString();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["EmpInfoId"].ToString() == empId)
        //        {
        //            aShowMessage.ShowMessageBox("Employee Already Exists", this);
        //            return false;
        //        }
        //    }
        //}
        //return true;
    }

    

   
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string option = forSector.Value;

        TrainingBudgetAllocationMaster aMaster = new TrainingBudgetAllocationMaster();
        aMaster.TrainingBudgetDetailsId =0;
        aMaster.Quater = quater.Value.ToString();
        aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
        aMaster.TrainingBudgetMasterId = Convert.ToInt32(ddlTraining.SelectedValue);

        aMaster.TrainingBudgetAllocationId = hdpk.Value == "" ? 0 : Convert.ToInt32(hdpk.Value);
        int result = 0;
        if (option == "Department")
        {
            aMaster.ForDepartment = true;
            aMaster.ForEmployee = false;
            aMaster.ForGrade = false;
            
        }

        if (option == "Grade")
        {
            aMaster.ForDepartment = false;
            aMaster.ForEmployee = false;
            aMaster.ForGrade = true;
           // result = _trainingDal.SaveBudgetAllocationMaster(aMaster, Session["LoginName"].ToString());
        }

        if (option == "Employee")
        {
            aMaster.ForDepartment = false;
            aMaster.ForEmployee = true;
            aMaster.ForGrade = false;
            aMaster.TrainingBudgetDetailsId =0;
           // result = _trainingDal.SaveBudgetAllocationMasterEmp(aMaster, Session["LoginName"].ToString());

        }
        result = _trainingDal.SaveBudgetAllocationMaster(aMaster, Session["UserId"].ToString());
        
       // int result = _trainingDal.SaveBudgetAllocationMaster(aMaster, Session["LoginName"].ToString());

        List<TrainingBudgetAllocationDetails> aDetailsList = new List<TrainingBudgetAllocationDetails>();
        if (result > 0)
        {
            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                TrainingBudgetAllocationDetails aDetails = new TrainingBudgetAllocationDetails();

                Label txt_Quater = (Label)gv_allocateEmp.Rows[i].FindControl("txt_quaterId");


                HiddenField empId = (HiddenField)gv_allocateEmp.Rows[i].FindControl("EmpInfoId");
                HiddenField detailsID = (HiddenField)gv_allocateEmp.Rows[i].FindControl("detailsID");


                if (option == "Department")
                {
                    aDetails.TrainingBudgetDetailsDptId = Convert.ToInt32(detailsID.Value);
                    aDetails.TrainingBudgetDetailsGradeId = 0;
                    aDetails.TrainingBudgetDetailsEmpId = 0;
                }

                if (option == "Grade")
                {
                    aDetails.TrainingBudgetDetailsDptId = 0;
                    aDetails.TrainingBudgetDetailsGradeId = Convert.ToInt32(detailsID.Value);
                    aDetails.TrainingBudgetDetailsEmpId = 0;
                }

                if (option == "Employee")
                {
                    aDetails.TrainingBudgetDetailsDptId = 0;
                    aDetails.TrainingBudgetDetailsGradeId = 0;
                    aDetails.TrainingBudgetDetailsEmpId = Convert.ToInt32(detailsID.Value);
                }
                aDetails.TrainingBudgetAllocationId = result;
               



                aDetails.EmpInfoId = Convert.ToInt32(empId.Value);
                aDetails.Quater = Convert.ToInt32(txt_Quater.Text.ToString().Trim());

                aDetailsList.Add(aDetails);
            }
        }
        bool detailsResult = false;
        if (aDetailsList.Count > 0)
        {
            detailsResult = _trainingDal.SaveBudgetAllocationDetails(aDetailsList);

            if (detailsResult == true)
            {
                AlertMessageBoxShow("Operation Successful...");
                Response.Redirect("BudgetWiseAllocationList.aspx");
            }
            else
            {
                AlertMessageBoxShow("Operation Failed...");

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
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable emptyData = null;
        ViewState["EmpAllocate"] = null;
        ViewState["DptDetails"] = null;
        ViewState["GrdDetails"] = null;
        ViewState["EmpBudget"] = null;

        gv_allocateEmp.DataSource = emptyData;
        gv_allocateEmp.DataBind();

        gv_BudEmp.DataSource = emptyData;
        gv_BudEmp.DataBind();

        gv_DptDetails.DataSource = emptyData;
        gv_DptDetails.DataBind();


        gv_EmpDetails.DataSource = emptyData;
        gv_EmpDetails.DataBind();

        gv_GrdDetails.DataSource = emptyData;
        gv_GrdDetails.DataBind();


        DataTable dt = _trainingDal.GetTraininByFinancialYearAndCompany(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlFinancialYear.SelectedValue));
        ddlTraining.DataSource = dt;
        ddlTraining.DataValueField = "Value";
        ddlTraining.DataTextField = "TextField";
        ddlTraining.DataBind();

    }
    protected void ddlTraining_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable emptyData = null;
        ViewState["EmpAllocate"] = null;
        ViewState["DptDetails"] = null;
        ViewState["GrdDetails"] = null;
        ViewState["EmpBudget"] = null;

        gv_allocateEmp.DataSource = emptyData;
        gv_allocateEmp.DataBind();

        gv_BudEmp.DataSource = emptyData;
        gv_BudEmp.DataBind();

        gv_DptDetails.DataSource = emptyData;
        gv_DptDetails.DataBind();


        gv_EmpDetails.DataSource = emptyData;
        gv_EmpDetails.DataBind();

        gv_GrdDetails.DataSource = emptyData;
        gv_GrdDetails.DataBind();


        DataTable dt = _trainingDal.GetTrainingBudgetByMaster(Convert.ToInt32(ddlTraining.SelectedValue));

        bool forDpt = dt.Rows[0]["ForDepartment"].ToString() == "True" ? true : false;
        bool forGrd = dt.Rows[0]["ForGrade"].ToString() == "True" ? true : false;
        bool forEmp = dt.Rows[0]["ForEmployee"].ToString() == "True" ? true : false;
        if (forDpt == true)
        {
            //  gv_DptDetails.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
            DataTable dtDpt = _trainingDal.GetTrainingDetailsDptByMaster(Convert.ToInt32(ddlTraining.SelectedValue));
            ViewState["DptDetails"] = dtDpt;
            gv_DptDetails.DataSource = dtDpt;
            gv_DptDetails.DataBind();
            forSector.Value = "Department";
        }

        if (forGrd == true)
        {
            DataTable dtDpt = _trainingDal.GetTrainingDetailsGrdByMaster(Convert.ToInt32(ddlTraining.SelectedValue));

            ViewState["GrdDetails"] = dtDpt;
            gv_GrdDetails.DataSource = dtDpt;
            gv_GrdDetails.DataBind();
            forSector.Value = "Grade";
        }

        if (forEmp == true)
        {
            DataTable dtDpt = _trainingDal.GetTrainingBudgetDetailsEmployee(Convert.ToInt32(ddlTraining.SelectedValue));

            ViewState["EmpBudget"] = dtDpt;
            gv_BudEmp.DataSource = dtDpt;
            gv_BudEmp.DataBind();
            forSector.Value = "Employee";
        }
    }
   
    protected void removeEmp_Click(object sender, EventArgs e)
    {


        string option = forSector.Value;

       
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = (DataTable)ViewState["EmpAllocate"];

        string empId = ((HiddenField)(gv_allocateEmp.Rows[rowID].FindControl("empInfoId"))).Value;
        dt.Rows.Remove(dt.Rows[rowID]);
        if (option == "Department" || option == "Grade")
        {
            for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
            {
                CheckBox check = (CheckBox) gv_EmpDetails.Rows[i].FindControl("lb_empCheck");
                HiddenField emp = (HiddenField) gv_EmpDetails.Rows[i].FindControl("empInfoId");
                if (emp.Value == empId)
                {
                    check.Checked = false;
                }



            }
        }
        else
        {
            for (int i = 0; i < gv_EmpDetails2.Rows.Count; i++)
            {
                CheckBox check = (CheckBox)gv_EmpDetails2.Rows[i].FindControl("lb_empCheck");
                HiddenField emp = (HiddenField)gv_EmpDetails2.Rows[i].FindControl("empInfoId");
                if (emp.Value == empId)
                {
                    check.Checked = false;
                }



            }
        }
        if (dt.Rows.Count == 0)
        {
            ViewState["EmpAllocate"] = null;
            dt = (DataTable)ViewState["EmpAllocate"];
        }
        else
        {
            ViewState["EmpAllocate"] = dt;
        }


        gv_allocateEmp.DataSource = dt;
        gv_allocateEmp.DataBind();
       
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetWiseAllocation.aspx");
    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetWiseAllocationList.aspx");
    }
}