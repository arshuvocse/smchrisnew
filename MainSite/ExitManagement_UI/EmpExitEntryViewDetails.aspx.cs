using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.Survey;
using DAO.HRIS_DAO;

public partial class ExitManagement_UI_EmpExitEntryViewDetails : System.Web.UI.Page
{
    EmpExitDal aExitDal = new EmpExitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();


            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                getOneRecord(Request.QueryString["MID"]);

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


    ClearenceFormDal aExitDalss = new ClearenceFormDal();

    private void getOneRecord(string ID)
    {
        DataTable dtExit = aExitDal.LoadExitMAster((ID));
        if (dtExit.Rows.Count > 0)
        {
            hfEmpInfoId.Value = dtExit.Rows[0]["EmployeeId"].ToString().Trim();



            DataTable dtSupervisor = aExitDal.LoadExitMAsterSupervisor((ID));
            if (dtSupervisor.Rows.Count > 0)
            {
                GridView1.DataSource = dtSupervisor;
                GridView1.DataBind();
            }
            DataTable dtDepartment = aExitDal.LoadExitMAsterDepartment((ID));
            if (dtDepartment.Rows.Count > 0)
            {
                loadGridView.DataSource = dtDepartment;
                loadGridView.DataBind();
            }

            GetEmpinfo(hfEmpInfoId.Value);


            DataTable dtDocNew = aExitDalss.GetDocNewDataById(hfEmpInfoId.Value.ToString());
            if (dtDocNew.Rows.Count > 0)
            {
                ViewState["gvDocGrid_List"] = dtDocNew;
                gv_Doc.DataSource = dtDocNew;
                gv_Doc.DataBind();
            }

            else
            {
                ViewState["gvDocGrid_List"] = null;
                gv_Doc.DataSource = null;
                gv_Doc.DataBind();
            }


        }
    }
  

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "View")
            {
                //btn_Save.Visible = true;
            }
            
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("EmpExitView.aspx");
        }

    }
    private void LoadDepartmentCheckBoxList()
    {
        DataTable aTable = aExitDal.LoadExitDepartment(ddlCompany.SelectedValue);

        loadGridView.DataSource = aTable;
        loadGridView.DataBind();
    }
    private void LoadDepartmentCheckBoxListNotInEmpDept(string empinfoId)
    {
        DataTable aTable = aExitDal.LoadExitDepartmentNotInEmployee(ddlCompany.SelectedValue,empinfoId);

        loadGridView.DataSource = aTable;
        loadGridView.DataBind();


        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            int DepartmentId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
            DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");

            using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMent(ddlCompany.SelectedValue, DepartmentId.ToString()))
            {



                ddlEmpInfoList.DataSource = dt222;
                ddlEmpInfoList.DataValueField = "EmpInfoId";
                ddlEmpInfoList.DataTextField = "EmpName";
                ddlEmpInfoList.DataBind();
                ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfoList.SelectedIndex = 0;
            }
        }
    }

    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveDataValidation())
        {
            Int32 masterId = aExitDal.SaveExitMasterInfo(PrepareDateForMasterSave());

            if (masterId > 0)
            {
                Int32 id = SaveExitDetailInfo(PrepareDateForDetailSave(masterId));
                
                if (id > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Data Saved Successfully...');window.location ='EmpExitEntry.aspx';",
             true);
                    Clear();
                }
            }
        }
    }

    public void LoadReportingEmpWithDept()
    {
        DataTable dtdata =
            aExitDal.LoadEmployeeInfoParameter("WHERE EmpInfoId IN (" +
                                               GetReportingEmpId(hfEmpInfoId.Value) + ")");
        GridView1.DataSource = dtdata;
        GridView1.DataBind();
    }
    public string GetReportingEmpId(string empinfoId)
    {
        DataTable dtdata = aExitDal.LoadEmployeeInfoDeptWise(empinfoId);
        if (dtdata.Rows.Count > 0)
        {
            return "'" + dtdata.Rows[0]["ReportingEmpId"].ToString() + "'";
        }
        else
        {
            return "'0'";
        }
    }
    private int SaveExitDetailInfo(List<EmpExitDetailDao> aList)
    {
        Int32 id = 0;

        foreach (var aDao in aList)
        {
            id = aExitDal.SaveExitDetailInfo(aDao);
        }

        return id;
    }

    private List<EmpExitDetailDao> PrepareDateForDetailSave(int masterId)
    {
        List<EmpExitDetailDao> aDaos = new List<EmpExitDetailDao>();
        EmpExitDetailDao aDao;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            aDao = new EmpExitDetailDao();
            aDao.MasterId = masterId;
            aDao.EmpInfoId = Convert.ToInt32(GridView1.DataKeys[i][1].ToString());
            aDao.DepartmentId = Convert.ToInt32(GridView1.DataKeys[i][0].ToString());
            aDao.EmployeeIdForClearance = Convert.ToInt32(hfEmpInfoId.Value);
            aDao.ApprovalStatus = "as Supervisor";
            aDaos.Add(aDao);
        }
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            aDao =  new EmpExitDetailDao();
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");

            if (chkBoxRows.Checked)
            {
                aDao.MasterId = masterId;
                aDao.EmpInfoId = Convert.ToInt32(ddlEmpInfoList.SelectedValue);
                aDao.EmployeeIdForClearance = Convert.ToInt32(hfEmpInfoId.Value);
                var dataKey = loadGridView.DataKeys[i];
                if (dataKey != null)
                    aDao.DepartmentId = Convert.ToInt32(Convert.ToInt32(dataKey.Value));
                aDao.ApprovalStatus = "as Department";
                aDaos.Add(aDao);
            }
        }

        return aDaos;
    }

    private EmpExitMasterDao PrepareDateForMasterSave()
    {
        EmpExitMasterDao aMasterDao = new EmpExitMasterDao();

        aMasterDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aMasterDao.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);
        aMasterDao.EmpCode = empCode.Text.Trim();
        aMasterDao.EmpName = empName.Text.Trim();
        aMasterDao.JoiningDate = Convert.ToDateTime(dtJoining.Text.Trim());
        aMasterDao.DesignationId = Convert.ToInt32(hfDesignation.Value);
        aMasterDao.DivisionId = Convert.ToInt32(hfDivision.Value);
        if (hfSalaryGrade.Value!="")
        {
            aMasterDao.SalaryGradeId = Convert.ToInt32(hfSalaryGrade.Value);
        }
     
        aMasterDao.Description = descriptionTextbox.Text.Trim();

        aMasterDao.ActionStatus = "Posted";

        aMasterDao.EntryBy = Session["LoginName"].ToString();
        aMasterDao.EntryDate = DateTime.Now;

        return aMasterDao;
    }

    private bool SaveDataValidation()
    {
        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("You have to select company !!!");
            return false;
        }

        if (hfEmpInfoId.Value == "")
        {
            ShowMessageBox("You have to select employee !!!");
            return false;
        }

        Int32 count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count ++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("You have to select at least one department !!!");
            return false;
        }

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");

            if (chkBoxRows.Checked)
            {
                if (ddlEmpInfoList.SelectedValue == "")
                {
                    ShowMessageBox("Employee is required !!!");
                    ddlEmpInfoList.Focus();
                    return false;
                }
            }
 
        }

        return true;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        txt_EmpName.Text = "";
        hfEmpInfoId.Value = "";
        empName.Text = "";
        empCode.Text = "";
        ddlDivision.Text = "";
        hfDivision.Value = "";
        ddlDesignation.Text = "";
        hfDesignation.Value = "";
        ddlSalaryGrade.Text = "";
        hfSalaryGrade.Value = "";

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        txt_EmpName.Enabled = false;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        using (DataTable dt222 = aExitDal.GetEmpDDL(ddlCompany.SelectedValue.ToString()))
        {



            ddlEmpInfo.DataSource = dt222;
            ddlEmpInfo.DataValueField = "EmpInfoId";
            ddlEmpInfo.DataTextField = "EmpName";
            ddlEmpInfo.DataBind();
            ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlEmpInfo.SelectedIndex = 0;
        }
        //if (ddlCompany.SelectedValue != "")
        //{
        //    txt_EmpName.Enabled = true;

        //    Session["CompanyId"] = "";
        //    Session["CompanyId"] = ddlCompany.SelectedValue;

        //    //LoadDepartmentCheckBoxList();
        //}
        //else
        //{
        //    Session["CompanyId"] = "";
        //    txt_EmpName.Enabled = false;
        //}


    }

    public void GetDepartmentContextKey()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            TextBox employeeTextBox = (TextBox)loadGridView.Rows[i].FindControl("employeeTextBox");

            AjaxControlToolkit.AutoCompleteExtender modal = (AjaxControlToolkit.AutoCompleteExtender)employeeTextBox.FindControl("empAutoCompleteExtender1");
            modal.ContextKey = loadGridView.DataKeys[i][0].ToString();
            //drCheckBox.Checked = false;
        }
    }
    public void GetDepartmentContextKey(int row)
    {
        int i = row;
        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            TextBox employeeTextBox = (TextBox)loadGridView.Rows[i].FindControl("employeeTextBox");

            AjaxControlToolkit.AutoCompleteExtender modal = (AjaxControlToolkit.AutoCompleteExtender)employeeTextBox.FindControl("empAutoCompleteExtender1");
            modal.ContextKey = loadGridView.DataKeys[i][0].ToString();
            //drCheckBox.Checked = false;
        }
    }
    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = aExitDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                try
                {
                    hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    hfDivision.Value = "0";
                    //throw;
                }
                deptHiddenField.Value = aTable.Rows[0].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
                ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                try
                {
                    hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    hfSalaryGrade.Value = "";
                    //throw;
                }

                empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                empName.Text = aTable.Rows[0].Field<string>("EmpName");

                dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");
                LoadDepartmentCheckBoxListNotInEmpDept(hfEmpInfoId.Value);
                //GetDepartmentContextKey();
                LoadReportingEmpWithDept();
            }
            else
            {
                txt_EmpName.Text = "";
                ShowMessageBox("No Information found !!!");
            }
        }
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    private void SetEmployeeInfo()
    {
        string empName = txt_EmpName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            hfEmpInfoId.Value = emp[2];
            txt_EmpName.Text = emp[1].Trim();
        }
        else
        {
            txt_EmpName.Text = "";
            hfEmpInfoId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

       // txt_EmpName.Text = "";
    }


    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
            var empName = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("employeeTextBox");

            if (chkBoxRows.Checked)
            {
                empName.ReadOnly = false;
            }
            else
            {
                empName.ReadOnly = true;
                empName.Text = "";
            }
        }
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox dropDown = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        
        var chkBoxRows = (CheckBox)loadGridView.Rows[rowindex].Cells[0].FindControl("chkSelect");
        DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("ddlEmpInfoList");

        if (chkBoxRows.Checked)
        {
            ddlEmpInfoList.Enabled = true;
        }
        else
        {
            ddlEmpInfoList.SelectedIndex = 0;
            ddlEmpInfoList.Enabled = false ;
        }
        GetDepartmentContextKey(rowindex);
    }

    protected void employeeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox dropDown = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;     
        
        SetEmployeeInfo(rowindex);

    }

    private void SetEmployeeInfo(int rowindex)
    {
        var empNameTextBox = (TextBox)loadGridView.Rows[rowindex].Cells[0].FindControl("employeeTextBox");
        var empIdTextBox = (HiddenField)loadGridView.Rows[rowindex].Cells[0].FindControl("hdfEmpInfoId");
        
        string empName = empNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            if (CheckEmpIdExistOrNot(emp[2], rowindex))
            {
                empIdTextBox.Value = emp[2];
                empNameTextBox.Text = emp[1];
            }
            else
            {
                empIdTextBox.Value = "";
                empNameTextBox.Text = "";
                ShowMessageBox("Employee already Exist !!");
            }
           
        }
        else
        {
            empIdTextBox.Value = "";
            empNameTextBox.Text = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    private bool CheckEmpIdExistOrNot(string empid, int rowindex)
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            if (i != rowindex)
            {
                //var empIdTextBox = (HiddenField)loadGridView.Rows[rowindex].Cells[0].FindControl("hdfEmpInfoId");
                var empIdTextBox1 = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hdfEmpInfoId");

                if (empIdTextBox1.Value == empid)
                {
                    return false;
                }
                
            }
        }

        return true;
    }
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();

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


                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();




                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();




            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    id_Empid.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    public bool CheckExistExitEmp(string id)
    {
        DataTable dtdata = aExitDal.CheckExistEmployee(id);
        if (dtdata.Rows.Count>0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    protected void ddlEmpInfo_OnTextChanged(object sender, EventArgs e)
    {
        if (CheckExistExitEmp(ddlEmpInfo.SelectedValue)==false)
        {


            hfEmpInfoId.Value = ddlEmpInfo.SelectedValue;

            if (hfEmpInfoId.Value != "")
            {
                GetEmpinfo(hfEmpInfoId.Value);
                DataTable aTable = aExitDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

                if (aTable.Rows.Count > 0)
                {
                    ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                    try
                    {
                        hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId")
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        hfDivision.Value = "0";
                        //throw;
                    }

                    deptHiddenField.Value = aTable.Rows[0].Field<Int32>("DepartmentId")
                        .ToString(CultureInfo.InvariantCulture);
                    ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                    try
                    {
                        hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId")
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                    try
                    {
                        hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId")
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        hfSalaryGrade.Value = "";
                        //throw;
                    }

                    empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                    empName.Text = aTable.Rows[0].Field<string>("EmpName");

                    dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");
                    LoadDepartmentCheckBoxListNotInEmpDept(hfEmpInfoId.Value);
                    //GetDepartmentContextKey();
                    LoadReportingEmpWithDept();
                }
                else
                {
                    txt_EmpName.Text = "";
                    ShowMessageBox("No Information found !!!");
                }
            }
        }
        else
        {
            ShowMessageBox("Employee Information Already Exist");
        }
    }

    protected void ddlEmpInfoList_OnTextChanged(object sender, EventArgs e)
    {
        
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
         Response.Redirect("EmpExitView.aspx");
    }
}