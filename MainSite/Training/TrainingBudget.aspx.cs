using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_TrainingBudget : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private int mid = 0;
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["EmpOption"] = "Employee";
        if (!IsPostBack)
        {
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {
                    DataTable dt = _trainingDal.GetTrainingBudgetByMaster(Convert.ToInt32(mid));

                    ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                    ddlCompany_SelectedIndexChanged(ddlCompany, (EventArgs)e);

                    ddlFinancialYear.SelectedValue = (dt.Rows[0]["FinancialYearId"].ToString());
                    ddlFinancialYear_SelectedIndexChanged(ddlFinancialYear, (EventArgs) e);
                    
                    
                    txt_TrainingTitle.Text = dt.Rows[0]["TrainingTitle"].ToString();
                    txt_ExpectedOutcome.Text = dt.Rows[0]["TrainingResult"].ToString();
                    txt_totalQty.Text = dt.Rows[0]["TotalParticipant"].ToString();
                    txt_Duration.Text = dt.Rows[0]["Duration"].ToString();
                    txt_budget.Text = dt.Rows[0]["Budget"].ToString();
                    txt_CostPer.Text = dt.Rows[0]["CostParticipant"].ToString();
                    txt_ref.Text = dt.Rows[0]["Referance"].ToString();
                    txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();



                    bool forDpt = dt.Rows[0]["ForDepartment"].ToString() == "True" ? true : false;
                    bool forGrd = dt.Rows[0]["ForGrade"].ToString() == "True" ? true : false;
                    bool forEmp = dt.Rows[0]["ForEmployee"].ToString() == "True" ? true : false;


                    bool external = Convert.ToBoolean(dt.Rows[0]["IsExternal"].ToString());
                    bool intarnal = Convert.ToBoolean(dt.Rows[0]["IsInternal"].ToString());

                    bool local = Convert.ToBoolean(dt.Rows[0]["IsLocal"].ToString());
                    bool foreign = Convert.ToBoolean(dt.Rows[0]["IsForeign"].ToString());



                    // set Dpt/grade/employee

                    if (forDpt == true)
                    {
                        chk_Perticioants.Items[0].Selected = true;
                        //  div_Deprtment.Visible = true;

                    }
                    if (forGrd == true)
                    {
                        chk_Perticioants.Items[1].Selected = true;
                    }
                    if (forEmp == true)
                    {
                        chk_Perticioants.Items[2].Selected = true;
                    }

                    chk_Perticioants_SelectedIndexChanged(chk_Perticioants, (EventArgs)e);

                    // set external internal
                    if (external == true)
                    {
                        radExIn.Items[0].Selected = true;
                    }
                    if (intarnal == true)
                    {
                        radExIn.Items[1].Selected = true;
                    }
                    if (foreign == true)
                    {
                        rad_fLocal.Items[0].Selected = true;
                    }

                    if (local == true)
                    {
                        rad_fLocal.Items[1].Selected = true;
                    }



                    if (forDpt == true)
                    {
                        //  gv_DptDetails.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
                        DataTable dtDpt = _trainingDal.GetTrainingDetailsDptByMaster(Convert.ToInt32(mid));
                        ViewState["DptDetails"] = dtDpt;
                        gv_DptDetails.DataSource = dtDpt;
                        gv_DptDetails.DataBind();
                    }


                    if (forGrd == true)
                    {
                        //  gv_DptDetails.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
                        DataTable dtDpt = _trainingDal.GetTrainingDetailsGrdByMaster(Convert.ToInt32(mid));
                        ViewState["GrdDetails"] = dtDpt;
                        gv_GradeDetails.DataSource = dtDpt;
                        gv_GradeDetails.DataBind();
                    }

                    if (forEmp == true)
                    {
                        DataTable dtDpt = _trainingDal.GetTrainingDetailsEmpByMaster(Convert.ToInt32(mid));



                        ViewState["EmpDetails"] = dtDpt;
                        gv_EmpDetails.DataSource = dtDpt;
                        gv_EmpDetails.DataBind();

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
        ClearGrid();
        Session["CompanyId"] = ddlCompany.SelectedValue;
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();


    }


    protected void chk_Perticioants_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
        List<string> selectedValues = chk_Perticioants.Items.Cast<ListItem>()
   .Where(li => li.Selected)
   .Select(li => li.Value)
   .ToList();

        div_Deprtment.Visible = false;
        div_Grade.Visible = false;
        foreach (string a in selectedValues)
        {
            if (a == "Department")
            {

                div_Deprtment.Visible = true;
                div_Employee.Visible = false;
                div_Grade.Visible = false;
                DataTable dptDt = _trainingDal.GetDepartmentddl();
                dptDt.Rows.RemoveAt(0);
                chk_Department.DataSource = dptDt;
                chk_Department.DataValueField = "Value";
                chk_Department.DataTextField = "TextField";
                chk_Department.DataBind();

                gv_EmpDetails.DataSource = null;
                ViewState["EmpDetails"] = null;
                gv_EmpDetails.DataBind();

                gv_GradeDetails.DataSource = null;
                ViewState["GrdDetails"] = null;
                gv_GradeDetails.DataBind();
                txt_qty.Visible = true;
            }

            if (a == "Grade")
            {
                div_Grade.Visible = true;
                div_Deprtment.Visible = false;
                div_Employee.Visible = false;
                DataTable gdDt = _trainingDal.GetGradeForddl();
                gdDt.Rows.RemoveAt(0);
                chk_Grade.DataSource = gdDt;
                chk_Grade.DataValueField = "Value";
                chk_Grade.DataTextField = "TextField";
                chk_Grade.DataBind();


                gv_EmpDetails.DataSource = null;
                ViewState["EmpDetails"] = null;
                gv_EmpDetails.DataBind();


                gv_DptDetails.DataSource = null;
                ViewState["DptDetails"] = null;
                gv_DptDetails.DataBind();
                txt_qty.Visible = true;
            }

            if (a == "Employee")
            {
                div_Employee.Visible = true;
                div_Grade.Visible = false;
                div_Deprtment.Visible = false;

                gv_GradeDetails.DataSource = null;
                ViewState["GrdDetails"] = null;
                gv_GradeDetails.DataBind();


                gv_DptDetails.DataSource = null;
                ViewState["DptDetails"] = null;
                gv_DptDetails.DataBind();

                txt_qty.Visible = false;

            }
        }


    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
        LoadQuater(Convert.ToInt32(ddlFinancialYear.SelectedValue));
    }

    protected void LoadQuater(int id)
    {
        //FinancialYear ayear = _trainingDal.GetFinancialYear(id);

        DataTable dt = _trainingDal.GetQuaterNew(Convert.ToInt32(ddlCompany.SelectedValue));
        //YearQuater aQuaterSelect = new YearQuater { QuarterDetails = "Select Quater", QuarterNum = "-1" };
        //quaters.Insert(0, aQuaterSelect);
        ddlQuater.DataSource = dt;
        ddlQuater.DataValueField = "Value";
        ddlQuater.DataTextField = "TextField";
        ddlQuater.DataBind();
    }
    //public List<YearQuater> GetAllQuarters(DateTime StartDate, DateTime EndDate)
    //{

    //    return _trainingDal.GetQuater(StartDate, EndDate);

    //}


    protected void ddlQuater_SelectedIndexChanged(object sender, EventArgs e)
    {
        string aValue = ddlQuater.SelectedValue.ToString();
        DataTable dt = _trainingDal.GetMonthByQuater(Convert.ToInt32(aValue));
        ddlMonth.DataSource = dt;
        ddlMonth.DataTextField = "TextField";
        ddlMonth.DataValueField =  "Value";
        ddlMonth.DataBind();
    }


    private void ClearGrid()
    {
            ViewState["EmpDetails"] = null;
            ViewState["GrdDetails"] = null;
            ViewState["DptDetails"] = null;
        gv_DptDetails.DataSource = null;
        gv_DptDetails.DataBind();

        gv_GradeDetails.DataSource = null;
        gv_GradeDetails.DataBind();
        gv_EmpDetails.DataSource = null;
        gv_EmpDetails.DataBind();
    }
    protected void addToList_Click(object sender, EventArgs e)
    {

        List<string> selectedValues = chk_Perticioants.Items.Cast<ListItem>()
 .Where(li => li.Selected)
 .Select(li => li.Value)
 .ToList();

        if (selectedValues.Count == 0)
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_Participant, this);
            return;
        }

        string option = selectedValues[0];
        switch (option)
        {
            case "Department":

                if (AddToDptValidation() == true)
                {
                    AddDepartmentDetailsToGrid();
                    CalculateTotalParticipant();
                }

                break;
            case "Grade":

                if (AddToGradeValidation() == true)
                {
                    AddGradeDetailsToGrid();
                    CalculateTotalParticipant();
                }

                break;
            case "Employee":

                if (AddToEmployeeValidation() == true)
                {
                    AddEmpDetailsToGrid();
                    CalculateTotalParticipant();
                }


                break;

        }



    }


    protected string CalculateTotalParticipant()
    {
        List<string> selectedValues = chk_Perticioants.Items.Cast<ListItem>()
  .Where(li => li.Selected)
  .Select(li => li.Value)
  .ToList();
        string option = selectedValues[0];

        string total = "0";
        string budget = "0";

        switch (option)
        {
            case "Department":
                if (ViewState["DptDetails"] != null)
                {
                    DataTable dt = (DataTable)ViewState["DptDetails"];

                    int runningTotal = 0;
                    for (int i = 0; i < gv_DptDetails.Rows.Count; i++)
                    {

                        TextBox txt_Qty = (TextBox)gv_DptDetails.Rows[i].FindControl("txt_Qty");
                        int rowQty = Convert.ToInt32(txt_Qty.Text.ToString() == "" ? "0" : txt_Qty.Text.ToString());
                        runningTotal += rowQty;
                    }



                    total = runningTotal.ToString();
                    // string costPer = txt_CostPer.Text.Trim();
                    txt_budget.Text = txt_CostPer.Text.Trim() == "" ? "0" : (Convert.ToDecimal(txt_CostPer.Text.Trim()) * runningTotal).ToString();

                }
                txt_totalQty.Text = total;
                break;

            case "Grade":
                if (ViewState["GrdDetails"] != null)
                {
                    DataTable dt = (DataTable)ViewState["GrdDetails"];

                    int runningTotal = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txt_Qty = (TextBox)gv_GradeDetails.Rows[i].FindControl("txt_Qty");
                        int rowQty = Convert.ToInt32(txt_Qty.Text.ToString());
                        runningTotal += rowQty;
                    }
                    total = runningTotal.ToString();
                    txt_totalQty.Text = total;
                    txt_budget.Text = txt_CostPer.Text.Trim() == "" ? "0" : (Convert.ToDecimal(txt_CostPer.Text.Trim()) * runningTotal).ToString();
                }
                break;


            case "Employee":
                if (ViewState["EmpDetails"] != null)
                {
                    DataTable dt = (DataTable)ViewState["EmpDetails"];
                    int runningTotal = 0;

                    total = dt.Rows.Count.ToString();
                    txt_budget.Text = txt_CostPer.Text.Trim() == "" ? "0" : (Convert.ToDecimal(txt_CostPer.Text.Trim()) * Convert.ToInt32(total)).ToString();

                }
                txt_totalQty.Text = total;
                break;
            default:
                txt_totalQty.Text = total;
                break;

        }

        return total;



    }

    protected void AddEmpDetailsToGrid()
    {
        if (ViewState["EmpDetails"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            string employee = txt_employee.Text.Trim().Substring(0, 6);

            int empId = _trainingDal.GetEmployeeIdByCode(employee);


            dt.Columns.Add(new DataColumn("quater", typeof(string)));
            dt.Columns.Add(new DataColumn("quaterId", typeof(string)));
            dt.Columns.Add(new DataColumn("month", typeof(string)));
            dt.Columns.Add(new DataColumn("monthId", typeof(string)));

            dt.Columns.Add(new DataColumn("fromDate", typeof(string)));
            dt.Columns.Add(new DataColumn("toDate", typeof(string)));
            dt.Columns.Add(new DataColumn("employee", typeof(string)));
            dt.Columns.Add(new DataColumn("employeeId", typeof(string)));
            dt.Columns.Add(new DataColumn("finYear", typeof(string)));
            dr = dt.NewRow();
            string a = ddlQuater.SelectedValue.ToString();
            dr["quater"] = ddlQuater.SelectedItem.Text;
            dr["quaterId"] = ddlQuater.SelectedValue.ToString();
            dr["monthId"] = ddlMonth.SelectedValue.ToString();
            dr["month"] = ddlMonth.SelectedItem.Text;
            dr["fromDate"] = txt_fromDate.Text.ToString();
            dr["toDate"] = txt_toDate.Text.ToString();
            dr["employee"] = txt_employee.Text.Trim();
            dr["employeeId"] = empId.ToString();
            dr["finYear"] = ddlFinancialYear.SelectedValue.ToString();

            dt.Rows.Add(dr);
            ViewState["EmpDetails"] = dt;
            txt_employee.Text = null;
            gv_EmpDetails.DataSource = dt;
            gv_EmpDetails.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["EmpDetails"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["quater"] = ddlQuater.SelectedItem.Text;
                drCurrentRow["quaterId"] = ddlQuater.SelectedValue.ToString();
                drCurrentRow["monthId"] = ddlMonth.SelectedValue.ToString();
                drCurrentRow["month"] = ddlMonth.SelectedItem.Text;
                // drCurrentRow["quantity"] = txt_qty.Text.ToString();
                drCurrentRow["fromDate"] = txt_fromDate.Text.ToString();
                drCurrentRow["toDate"] = txt_toDate.Text.ToString();
                drCurrentRow["employee"] = txt_employee.Text.Trim();
                drCurrentRow["employeeId"] = _trainingDal.GetEmployeeIdByCode(txt_employee.Text.Trim().Substring(0, 6)); ;
                drCurrentRow["finYear"] = ddlFinancialYear.SelectedValue.ToString();



                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["EmpDetails"] = dtCurrentTable;
                txt_employee.Text = null;
                gv_EmpDetails.DataSource = dtCurrentTable;
                gv_EmpDetails.DataBind();


            }
        }

       
    }
    protected void AddGradeDetailsToGrid()
    {
        if (ViewState["GrdDetails"] == null)
        {
            DataTable dt = new DataTable();

            List<string> selectedValues = chk_Grade.Items.Cast<ListItem>()
                                       .Where(li => li.Selected)
                                       .Select(li => li.Value + ":" + li.Text)
                                       .ToList();

            dt.Columns.Add(new DataColumn("quater", typeof(string)));
            dt.Columns.Add(new DataColumn("quaterId", typeof(string)));
            dt.Columns.Add(new DataColumn("month", typeof(string)));
            dt.Columns.Add(new DataColumn("monthId", typeof(string)));
            dt.Columns.Add(new DataColumn("quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("fromDate", typeof(string)));
            dt.Columns.Add(new DataColumn("toDate", typeof(string)));
            dt.Columns.Add(new DataColumn("grade", typeof(string)));
            dt.Columns.Add(new DataColumn("gradeId", typeof(string)));
            dt.Columns.Add(new DataColumn("finYear", typeof(string)));
            

            for (int i = 0; i < selectedValues.Count; i++)
            {

                string[] idText = selectedValues[i].ToString().Split(new char[] { ':' });

                string id = idText[0];
                string text = idText[1];
                DataRow dr = null;
                dr = dt.NewRow();

                string a = ddlQuater.SelectedValue.ToString();
                dr["quater"] = ddlQuater.SelectedItem.Text;
                dr["quaterId"] = ddlQuater.SelectedValue.ToString();
                dr["monthId"] = ddlMonth.SelectedValue.ToString();
                dr["month"] = ddlMonth.SelectedItem.Text;
                dr["quantity"] = _trainingDal.GetEmployeeCountByGradeId(Convert.ToInt32(id));
                dr["fromDate"] = txt_fromDate.Text.ToString();
                dr["toDate"] = txt_toDate.Text.ToString();
                dr["grade"] = text;
                dr["gradeId"] = id;
                dr["finYear"] = ddlFinancialYear.SelectedValue.ToString();

                dt.Rows.Add(dr);
            }
            

          
           
            ViewState["GrdDetails"] = dt;
            gv_GradeDetails.DataSource = dt;
            gv_GradeDetails.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["GrdDetails"];


            List<string> selectedValues = chk_Grade.Items.Cast<ListItem>()
                                      .Where(li => li.Selected)
                                      .Select(li => li.Value + ":" + li.Text)
                                      .ToList();


            for (int i = 0; i < selectedValues.Count; i++)
            {
                DataRow drCurrentRow = null;

                string[] idText = selectedValues[i].ToString().Split(new char[] { ':' });

                string id = idText[0];
                string text = idText[1];

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["quater"] = ddlQuater.SelectedItem.Text;
                    drCurrentRow["quaterId"] = ddlQuater.SelectedValue.ToString();
                    drCurrentRow["monthId"] = ddlMonth.SelectedValue.ToString();
                    drCurrentRow["month"] = ddlMonth.SelectedItem.Text;
                    drCurrentRow["quantity"] = _trainingDal.GetEmployeeCountByGradeId(Convert.ToInt32(id));
                    drCurrentRow["fromDate"] = txt_fromDate.Text.ToString();
                    drCurrentRow["toDate"] = txt_toDate.Text.ToString();
                    drCurrentRow["grade"] = text;
                    drCurrentRow["gradeId"] = id;
                    drCurrentRow["finYear"] = ddlFinancialYear.SelectedValue.ToString();



                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["GrdDetails"] = dtCurrentTable;

                    gv_GradeDetails.DataSource = dtCurrentTable;
                    gv_GradeDetails.DataBind();


                }
            }
            
        }
    }
    protected void AddDepartmentDetailsToGrid()
    {

        if (ViewState["DptDetails"] == null)
        {
            List<string> selectedValues = chk_Department.Items.Cast<ListItem>()
                                        .Where(li => li.Selected)
                                        .Select(li => li.Value + ":" + li.Text)
                                        .ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("quater", typeof(string)));
            dt.Columns.Add(new DataColumn("quaterId", typeof(string)));
            dt.Columns.Add(new DataColumn("month", typeof(string)));
            dt.Columns.Add(new DataColumn("monthId", typeof(string)));
            dt.Columns.Add(new DataColumn("quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("fromDate", typeof(string)));
            dt.Columns.Add(new DataColumn("toDate", typeof(string)));
            dt.Columns.Add(new DataColumn("department", typeof(string)));
            dt.Columns.Add(new DataColumn("departmentId", typeof(string)));
            dt.Columns.Add(new DataColumn("finYear", typeof(string)));
            for (int i = 0; i < selectedValues.Count; i++)
            {

                DataRow dr = null;

                string[] idText = selectedValues[i].ToString().Split(new char[] { ':' });

                string id = idText[0];
                string text = idText[1];



                dr = dt.NewRow();
                string a = ddlQuater.SelectedValue.ToString();
                dr["quater"] = ddlQuater.SelectedItem.Text;
                dr["quaterId"] = ddlQuater.SelectedValue.ToString();
                dr["monthId"] = ddlMonth.SelectedValue.ToString();
                dr["month"] = ddlMonth.SelectedItem.Text;
                dr["quantity"] = _trainingDal.GetEmployeeCountByDeptId(Convert.ToInt32(id));
                dr["fromDate"] = txt_fromDate.Text.ToString();
                dr["toDate"] = txt_toDate.Text.ToString();
                dr["department"] = text;
                dr["departmentId"] = id;
                dr["finYear"] = ddlFinancialYear.SelectedValue.ToString();

                dt.Rows.Add(dr);
            }





            ViewState["DptDetails"] = dt;
            gv_DptDetails.DataSource = dt;
            gv_DptDetails.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DptDetails"];


            List<string> selectedValues = chk_Department.Items.Cast<ListItem>()
                                        .Where(li => li.Selected)
                                        .Select(li => li.Value + ":" + li.Text)
                                        .ToList();

            for (int i = 0; i < selectedValues.Count; i++)
            {

                DataRow dr = null;

                string[] idText = selectedValues[i].ToString().Split(new char[] { ':' });

                string id = idText[0];
                string text = idText[1];


                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["quater"] = ddlQuater.SelectedItem.Text;
                    drCurrentRow["quaterId"] = ddlQuater.SelectedValue.ToString();
                    drCurrentRow["monthId"] = ddlMonth.SelectedValue.ToString();
                    drCurrentRow["month"] = ddlMonth.SelectedItem.Text;
                    drCurrentRow["quantity"] = _trainingDal.GetEmployeeCountByDeptId(Convert.ToInt32(id));
                    drCurrentRow["fromDate"] = txt_fromDate.Text.ToString();
                    drCurrentRow["toDate"] = txt_toDate.Text.ToString();
                    drCurrentRow["department"] = text;
                    drCurrentRow["departmentId"] = id;
                    drCurrentRow["finYear"] = ddlFinancialYear.SelectedValue.ToString();

                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
            }

            ViewState["DptDetails"] = dtCurrentTable;

            gv_DptDetails.DataSource = dtCurrentTable;
            gv_DptDetails.DataBind();


           
        }

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (SaveValidation() == true)
        {

            List<string> selectedValues = chk_Perticioants.Items.Cast<ListItem>()
      .Where(li => li.Selected)
      .Select(li => li.Value)
      .ToList();

            List<string> selectedExIn = radExIn.Items.Cast<ListItem>()
      .Where(li => li.Selected)
      .Select(li => li.Value)
      .ToList();

            List<string> selectedForLo = rad_fLocal.Items.Cast<ListItem>()
    .Where(li => li.Selected)
    .Select(li => li.Value)
    .ToList();

            string option = selectedValues[0];

            TrainingBudgetMaster aMaster = new TrainingBudgetMaster();
            string editId = hdpk.Value.ToString();
            if (editId != "")
            {
                aMaster.TrainingBudgetMasterId = Convert.ToInt32(editId);
            }
            aMaster.TrainingTitle = txt_TrainingTitle.Text.Trim();
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.Text.Trim());
            aMaster.TrainingResult = txt_ExpectedOutcome.Text.Trim();
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.TotalParticipant = Convert.ToDecimal(txt_totalQty.Text.Trim());
            aMaster.Duration = Convert.ToDecimal(txt_Duration.Text.Trim());
            aMaster.CostParticipant = Convert.ToDecimal(txt_CostPer.Text.Trim());
            aMaster.Budget = Convert.ToDecimal(txt_budget.Text.Trim());
            aMaster.Referance = txt_ref.Text.Trim();
            aMaster.Remarks = txt_remarks.Text.Trim();

            string exIn = selectedExIn[0];

            string forLocal = selectedForLo[0];
            if (exIn == "External")
            {
                aMaster.IsExternal = true;

            }
            else
            {
                aMaster.IsExternal = false;
            }

            if (exIn == "Internal")
            {
                aMaster.IsInternal = true;
            }
            else
            {
                aMaster.IsInternal = false;
            }

            if (forLocal == "Foeign")
            {
                aMaster.IsForeign = true;

            }
            else
            {
                aMaster.IsForeign = false;
            }

            if (forLocal == "Local")
            {
                aMaster.IsLocal = true;
            }
            else
            {
                aMaster.IsLocal = false;
            }



            switch (option)
            {
                case "Department":
                    aMaster.ForDepartment = true;
                    aMaster.ForGrade = false;
                    aMaster.ForEmployee = false;


                    int masterResult = _trainingDal.SaveTraininBidgetMaster(aMaster, Session["LoginName"].ToString());




                    List<TrainingBudgetDetailsDpt> aDptList = GetDptDetailsForSave(masterResult);
                    bool dptResult = false;
                    if (aDptList.Count > 0 && masterResult > 0)
                    {

                        if (aMaster.TrainingBudgetMasterId > 0)
                        {
                            bool deleteData = false;
                            deleteData = _trainingDal.DeleteTrainingDetailsDptByMaster(masterResult);



                        }
                        dptResult = _trainingDal.SaveTrainingBudgetDpt(aDptList, masterResult);


                    }
                    if (dptResult == true)
                    {
                        AlertMessageBoxShow("Operation Successful...");
                     //   Response.Redirect("TrainingBudgetList.aspx");
                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");

                    }

                    break;
                case "Grade":
                    aMaster.ForDepartment = false;
                    aMaster.ForGrade = true;
                    aMaster.ForEmployee = false;


                    int masterResultGrd = _trainingDal.SaveTraininBidgetMaster(aMaster, Session["LoginName"].ToString());
                    List<TrainingBudgetDetailsGrade> aGrdList = new List<TrainingBudgetDetailsGrade>();
                    if (masterResultGrd > 0)
                    {
                        aGrdList = GetGradeForSave(masterResultGrd);
                    }

                    bool grdResult = false;
                    if (aGrdList.Count > 0 && masterResultGrd > 0)
                    {
                        if (aMaster.TrainingBudgetMasterId > 0)
                        {
                            bool deleteData = false;
                            deleteData = _trainingDal.DeleteTrainingDetailsGrdByMaster(masterResultGrd);



                        }

                        grdResult = _trainingDal.SaveTrainingBudgetGrade(aGrdList, masterResultGrd);

                    }
                    if (grdResult == true)
                    {
                        AlertMessageBoxShow("Operation Successful...");
                    //  Response.Redirect("TrainingBudgetList.aspx");
                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");

                    }

                    break;
                case "Employee":
                    aMaster.ForDepartment = false;
                    aMaster.ForGrade = false;
                    aMaster.ForEmployee = true;


                    int masterResultEmp = _trainingDal.SaveTraininBidgetMaster(aMaster, Session["LoginName"].ToString());

                    List<TrainingBudgetDetailsEmployee> empList = new List<TrainingBudgetDetailsEmployee>();
                    if (masterResultEmp > 0)
                    {
                        empList = GetDptGradeForEmployee(masterResultEmp);
                    }

                    bool empResult = false;
                    if (empList.Count > 0 && masterResultEmp > 0)
                    {

                        if (aMaster.TrainingBudgetMasterId > 0)
                        {
                            bool deleteData = false;
                            deleteData = _trainingDal.DeleteTrainingDetailsEmpByMaster(masterResultEmp);



                        }
                        empResult = _trainingDal.SaveTrainingBudgetEmployee(empList, masterResultEmp);

                    }
                    if (empResult == true)
                    {
                        AlertMessageBoxShow("Operation Successful...");
                      //  Response.Redirect("TrainingBudgetList.aspx");
                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");

                    }

                    break;

            }
        }

    }


    protected bool SaveValidation()
    {
        if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox(aMessages.VCompany, this);
            return false;
        }
        if (txt_TrainingTitle.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.TTtriningTitle, this);
            return false;
        }



        List<string> selectedValues = chk_Perticioants.Items.Cast<ListItem>()
               .Where(li => li.Selected)
               .Select(li => li.Value)
               .ToList();

        List<string> selectedExIn = radExIn.Items.Cast<ListItem>()
  .Where(li => li.Selected)
  .Select(li => li.Value)
  .ToList();

        List<string> selectedForLo = rad_fLocal.Items.Cast<ListItem>()
.Where(li => li.Selected)
.Select(li => li.Value)
.ToList();

        if (selectedValues.Count == 0)
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_Participant, this);
            return false;
        }
        if (selectedValues[0] == "Departent")
        {
            if (gv_DptDetails.Rows.Count <= 0)
            {
                aShowMessage.ShowMessageBox(aMessages.Bud_Deprtment, this);
                return false;
            }
        }

        if (selectedValues[0] == "Grade")
        {
            if (gv_GradeDetails.Rows.Count <= 0)
            {
                aShowMessage.ShowMessageBox(aMessages.Bud_Grade, this);
                return false;
            }
        }

        if (selectedValues[0] == "Employee")
        {
            if (gv_EmpDetails.Rows.Count <= 0)
            {
                aShowMessage.ShowMessageBox(aMessages.Bud_Employee, this);
                return false;
            }
        }


        if (txt_totalQty.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_TotalParticipant, this);
            return false;
        }

        if (txt_CostPer.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_CostPer, this);
            return false;
        }

        if (txt_budget.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_Bud, this);
            return false;
        }





        return true;
    }

    protected bool AddToDptValidation()
    {
        string quater = ddlQuater.SelectedValue;
        string dptId = chk_Department.SelectedValue.ToString();

        if (dptId == "")
        {
            aShowMessage.ShowMessageBox("Please Select Department", this);
            return false;
        }
        if (ddlQuater.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_Quater, this);
            return false;
        }

        if (ddlMonth.SelectedValue == "" || ddlMonth.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Please Select Month", this);
            return false;
        }

        if (txt_fromDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_FromDate, this);
            return false;
        }

        if (txt_toDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_ToDate, this);
            return false;
        }
        //if (txt_qty.Text.Trim() == "" || txt_qty.Text.Trim() == "0")
        //{
        //    aShowMessage.ShowMessageBox("Quantity can not be empty !!!!", this);
        //    return false;
        //}


        if (ViewState["DptDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["DptDetails"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                if (quater == dt.Rows[i]["quaterId"].ToString().Trim() && dptId == dt.Rows[i]["departmentId"].ToString().Trim())
                {
                    aShowMessage.ShowMessageBox(aMessages.Bud_DptInGrid, this);

                    // break;
                    return false;
                }
            }
        }
        return true;
    }


    protected bool AddToGradeValidation()
    {
        string quater = ddlQuater.SelectedValue;
        string gradeId = chk_Grade.SelectedValue.ToString();

        if (gradeId == "")
        {
            aShowMessage.ShowMessageBox("Please Select Grade", this);
            return false;
        }
        if (ddlQuater.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_Quater, this);
            return false;
        }
        if (ddlMonth.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Please Select Month", this);
            return false;
        }

        if (txt_fromDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_FromDate, this);
            return false;
        }

        if (ddlMonth.SelectedValue == "" || ddlMonth.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Select Month", this);
            return false;
        }
        if (txt_toDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_ToDate, this);
            return false;
        }
       


        if (ViewState["GrdDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["GrdDetails"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                if (quater == dt.Rows[i]["quaterId"].ToString().Trim() && gradeId == dt.Rows[i]["gradeId"].ToString().Trim())
                {
                    aShowMessage.ShowMessageBox("Selected Grade Already  Exists in Same Quater !!!", this);

                    // break;
                    return false;
                }
            }
        }
        return true;
    }

    protected bool AddToEmployeeValidation()
    {
        string quater = ddlQuater.SelectedValue;
        string employee = txt_employee.Text.Trim();



        if (employee == "")
        {
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            txt_employee.Text = null;
            return false;
        }
        if (ddlQuater.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_Quater, this);
            txt_employee.Text = null;
            return false;
        }
        if (ddlMonth.SelectedValue == "" || ddlMonth.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Select Month", this);
            txt_employee.Text = null;
            return false;
        }
        if (ddlMonth.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Please Select Month", this);
            return false;
        }
        if (ddlMonth.SelectedValue == "" || ddlMonth.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Select Month", this);
            txt_employee.Text = null;
            return false;
        }

        if (txt_fromDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_FromDate, this);
            txt_employee.Text = null;
            return false;
        }

        if (txt_toDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.Bud_ToDate, this);
            txt_employee.Text = null;
            return false;
        }



        if (ViewState["EmpDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["EmpDetails"];
            int empId = _trainingDal.GetEmployeeIdByCode(employee.Substring(0, 6));
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                if (dt.Rows[i]["employeeId"].ToString().Trim() == empId.ToString())
                {
                    aShowMessage.ShowMessageBox("Employee Already  Exists  !!!", this);
                    txt_employee.Text = null;
                    // break;
                    return false;
                }
            }
        }
        return true;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected List<TrainingBudgetDetailsDpt> GetDptDetailsForSave(int masterId)
    {
        List<TrainingBudgetDetailsDpt> aDptList = new List<TrainingBudgetDetailsDpt>();
        if (ViewState["DptDetails"] != null && gv_DptDetails.Rows.Count > 0)
        {



            for (int i = 0; i < gv_DptDetails.Rows.Count; i++)
            {
                Label txt_Quater = (Label)gv_DptDetails.Rows[i].FindControl("txt_quaterId");
                Label txt_Month = (Label)gv_DptDetails.Rows[i].FindControl("txt_monthId");
                HiddenField deptId = (HiddenField)gv_DptDetails.Rows[i].FindControl("deptId");
                HiddenField finYear = (HiddenField)gv_DptDetails.Rows[i].FindControl("finYear");
                Label txt_fromDate = (Label)gv_DptDetails.Rows[i].FindControl("txt_fromDate");
                Label txt_toDate = (Label)gv_DptDetails.Rows[i].FindControl("txt_toDate");
                TextBox txt_Qty = (TextBox)gv_DptDetails.Rows[i].FindControl("txt_Qty");

                TrainingBudgetDetailsDpt aDptInfo = new TrainingBudgetDetailsDpt();
                aDptInfo.TrainingBudgetMasterId = masterId;
                aDptInfo.DepartmentId = Convert.ToInt32(deptId.Value.ToString());
                aDptInfo.FinancialYearId = Convert.ToInt32(finYear.Value.ToString());
                aDptInfo.Qty = Convert.ToDecimal(txt_Qty.Text.Trim().ToString());
                aDptInfo.Quater = Convert.ToInt32(txt_Quater.Text.Trim().ToString());
                aDptInfo.TrainingMonth = Convert.ToInt32(txt_Month.Text.Trim().ToString());
                aDptInfo.FromDate = Convert.ToDateTime(txt_fromDate.Text.Trim().ToString());
                aDptInfo.ToDate = Convert.ToDateTime(txt_toDate.Text.Trim().ToString());
                aDptList.Add(aDptInfo);


            }



        }

        return aDptList;
    }



    protected List<TrainingBudgetDetailsGrade> GetGradeForSave(int masterId)
    {
        List<TrainingBudgetDetailsGrade> aDptList = new List<TrainingBudgetDetailsGrade>();
        if (ViewState["GrdDetails"] != null && gv_GradeDetails.Rows.Count > 0)
        {



            for (int i = 0; i < gv_GradeDetails.Rows.Count; i++)
            {
                Label txt_Quater = (Label)gv_GradeDetails.Rows[i].FindControl("txt_quaterId");
                Label txt_Month = (Label)gv_GradeDetails.Rows[i].FindControl("txt_monthId");
                HiddenField deptId = (HiddenField)gv_GradeDetails.Rows[i].FindControl("deptId");
                HiddenField finYear = (HiddenField)gv_GradeDetails.Rows[i].FindControl("finYear");
                Label txt_fromDate = (Label)gv_GradeDetails.Rows[i].FindControl("txt_fromDate");
                Label txt_toDate = (Label)gv_GradeDetails.Rows[i].FindControl("txt_toDate");
                TextBox txt_Qty = (TextBox)gv_GradeDetails.Rows[i].FindControl("txt_Qty");

                TrainingBudgetDetailsGrade aDptInfo = new TrainingBudgetDetailsGrade();
                aDptInfo.TrainingBudgetMasterId = masterId;
                aDptInfo.GradeId = Convert.ToInt32(deptId.Value.ToString());
                aDptInfo.FinancialYearId = Convert.ToInt32(finYear.Value.ToString());
                aDptInfo.Qty = Convert.ToDecimal(txt_Qty.Text.Trim().ToString());
                aDptInfo.Quater = Convert.ToInt32(txt_Quater.Text.Trim().ToString());
                aDptInfo.TrainingMonth = Convert.ToInt32(txt_Month.Text.Trim().ToString());
                aDptInfo.FromDate = Convert.ToDateTime(txt_fromDate.Text.Trim().ToString());
                aDptInfo.ToDate = Convert.ToDateTime(txt_toDate.Text.Trim().ToString());
                aDptList.Add(aDptInfo);


            }



        }

        return aDptList;
    }


    protected List<TrainingBudgetDetailsEmployee> GetDptGradeForEmployee(int masterId)
    {
        List<TrainingBudgetDetailsEmployee> aDptList = new List<TrainingBudgetDetailsEmployee>();
        if (ViewState["EmpDetails"] != null && gv_EmpDetails.Rows.Count > 0)
        {



            for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
            {
                Label txt_Quater = (Label)gv_EmpDetails.Rows[i].FindControl("txt_quaterId");
                Label txt_Month = (Label)gv_EmpDetails.Rows[i].FindControl("txt_monthId");
                HiddenField deptId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("deptId");
                HiddenField finYear = (HiddenField)gv_EmpDetails.Rows[i].FindControl("finYear");
                Label txt_fromDate = (Label)gv_EmpDetails.Rows[i].FindControl("txt_fromDate");
                Label txt_toDate = (Label)gv_EmpDetails.Rows[i].FindControl("txt_toDate");


                TrainingBudgetDetailsEmployee aDptInfo = new TrainingBudgetDetailsEmployee();
                aDptInfo.TrainingBudgetMasterId = masterId;
                aDptInfo.EmployeeId = Convert.ToInt32(deptId.Value.ToString());
                aDptInfo.FinancialYearId = Convert.ToInt32(finYear.Value.ToString());

                aDptInfo.Quater = Convert.ToInt32(txt_Quater.Text.Trim().ToString());
                aDptInfo.TrainingMonth = Convert.ToInt32(txt_Month.Text.Trim().ToString());
                aDptInfo.FromDate = Convert.ToDateTime(txt_fromDate.Text.Trim().ToString());
                aDptInfo.ToDate = Convert.ToDateTime(txt_toDate.Text.Trim().ToString());
                aDptList.Add(aDptInfo);


            }



        }

        return aDptList;
    }





    protected void txt_CostPer_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalParticipant();
    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingBudgetList.aspx");
    }

    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        if (ViewState["DptDetails"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["DptDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["DptDetails"] = null;
            }
            else
            {
                ViewState["DptDetails"] = dt;
            }


            gv_DptDetails.DataSource = dt;
            gv_DptDetails.DataBind();
            CalculateTotalParticipant();
        }
    }

    protected void lb_RemoveGrd_Click(object sender, EventArgs e)
    {
        if (ViewState["GrdDetails"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["GrdDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["GrdDetails"] = null;
            }
            else
            {
                ViewState["GrdDetails"] = dt;
            }


            gv_GradeDetails.DataSource = dt;
            gv_GradeDetails.DataBind();
            CalculateTotalParticipant();
        }
    }
    protected void lb_RemoveEmp_Click(object sender, EventArgs e)
    {
        if (ViewState["EmpDetails"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["EmpDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["EmpDetails"] = null;
            }
            else
            {
                ViewState["EmpDetails"] = dt;
            }


            gv_EmpDetails.DataSource = dt;
            gv_EmpDetails.DataBind();
            CalculateTotalParticipant();
        }
    }
    protected void txt_Qty_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalParticipant();
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingBudget.aspx");
    }
    protected void chk_Department_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<string> selectedValues = chk_Perticioants.Items.Cast<ListItem>()
 .Where(li => li.Selected)
 .Select(li => li.Value)
 .ToList();
        string option = selectedValues[0];

        int total = 0;



        if (option == "Department")
        {
            List<string> selectedValuesDpt = chk_Department.Items.Cast<ListItem>()
                                        .Where(li => li.Selected)
                                        .Select(li => li.Value + ":" + li.Text)
                                        .ToList();

            foreach (var item in selectedValuesDpt)
            {


                string[] idText = item.ToString().Split(new char[] { ':' });

                string id = idText[0];
                string text = idText[1];
              total+=  _trainingDal.GetEmployeeCountByDeptId(Convert.ToInt32(id));
            }
        }
        if (option == "Grade")
        {
            List<string> selectedValuesDpt = chk_Grade.Items.Cast<ListItem>()
                                        .Where(li => li.Selected)
                                        .Select(li => li.Value + ":" + li.Text)
                                        .ToList();

            foreach (var item in selectedValuesDpt)
            {


                string[] idText = item.ToString().Split(new char[] { ':' });

                string id = idText[0];
                string text = idText[1];
                total += _trainingDal.GetEmployeeCountByGradeId(Convert.ToInt32(id));
            }
        }


        txt_qty.Text = total.ToString();

    }

   
}