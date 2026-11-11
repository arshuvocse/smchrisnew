using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Survey;
using DAO.HRIS_DAO;

public partial class Survey_EmpExitEntry : System.Web.UI.Page
{
    EmpExitDal aExitDal = new EmpExitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
        }
        
    }

    private void LoadDepartmentCheckBoxList()
    {
        //DataTable aTable = aExitDal.LoadExitDepartment(ddlCompany.SelectedValue);

        //loadGridView.DataSource = aTable;
        //loadGridView.DataBind();
        Initial();
    }

    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);

    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveDataValidation())
        {
            Int32 masterId = aExitDal.SaveExitMasterInfoSales(PrepareDateForMasterSave());

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

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            aDao =  new EmpExitDetailDao();
            //var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            var empIdTextBox = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hdfEmpInfoId");

            //if (chkBoxRows.Checked)
            {
                aDao.MasterId = masterId;
                aDao.EmpInfoId = Convert.ToInt32(empIdTextBox.Value);
                var dataKey = loadGridView.DataKeys[i];
                if (dataKey != null)
                    aDao.DepartmentId = Convert.ToInt32(Convert.ToInt32(dataKey.Value));

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
        aMasterDao.SalaryGradeId = Convert.ToInt32(hfSalaryGrade.Value);
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

        //Int32 count = 0;

        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        //{
        //    var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

        //    if (chkBoxRows.Checked)
        //    {
        //        count ++;
        //    }

        //    if (count > 0)
        //    {
        //        break;
        //    }
        //}

        //if (count == 0)
        //{
        //    ShowMessageBox("You have to select at least one department !!!");
        //    return false;
        //}

        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        //{
        //    var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
        //    var empIdTextBox = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hdfEmpInfoId");

        //    if (chkBoxRows.Checked)
        //    {
        //        if (empIdTextBox.Value == "")
        //        {
        //            ShowMessageBox("Employee is required !!!");
        //            return false;
        //        }
        //    }

        //    if (count > 0)
        //    {
        //        break;
        //    }
        //}

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
        //txt_EmpName.Enabled = false;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            txt_EmpName.Enabled = true;

            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;

            LoadDepartmentCheckBoxList();
        }
        else
        {
            Session["CompanyId"] = "";
            ///txt_EmpName.Enabled = false;
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
                hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

                ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);

                empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                empName.Text = aTable.Rows[0].Field<string>("EmpName");

                dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");

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
        }
        else
        {
            hfEmpInfoId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

        txt_EmpName.Text = "";
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
        var empName = (TextBox)loadGridView.Rows[rowindex].Cells[0].FindControl("employeeTextBox");

        if (chkBoxRows.Checked)
        {
            empName.ReadOnly = false;
        }
        else
        {
            empName.Text = "";
            empName.ReadOnly = true;
        }
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
    public void Initial()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("DepartmentId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("EmpInfoId");
        DataRow dataRow = null;

        
        dataRow = aDataTable.NewRow();
        dataRow["DepartmentId"] = "3";
        dataRow["EmpName"] = "";
        dataRow["EmpInfoId"] = "";
        aDataTable.Rows.Add(dataRow);
        
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

    }
    public void Add(int row)
    {
        DataTable aDataTable=new DataTable();
        aDataTable.Columns.Add("DepartmentId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("EmpInfoId");
        DataRow dataRow = null;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            dataRow["DepartmentId"] = "3";
            dataRow["EmpName"] = ((TextBox) loadGridView.Rows[i].FindControl("employeeTextBox")).Text;
            dataRow["EmpInfoId"] = ((HiddenField)loadGridView.Rows[i].FindControl("hdfEmpInfoId")).Value;
            aDataTable.Rows.Add(dataRow);
            if (i==row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["DepartmentId"] = "3";
                dataRow["EmpName"] = "";
                dataRow["EmpInfoId"] = "";
                aDataTable.Rows.Add(dataRow);        
            }
        }

        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

    }
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("DepartmentId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("EmpInfoId");
        DataRow dataRow = null;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["DepartmentId"] = "3";
                dataRow["EmpName"] = ((TextBox)loadGridView.Rows[i].FindControl("employeeTextBox")).Text;
                dataRow["EmpInfoId"] = ((HiddenField)loadGridView.Rows[i].FindControl("hdfEmpInfoId")).Value;
                aDataTable.Rows.Add(dataRow);
            
                
            }
        }

        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

    }
    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButton = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        Remove(rowindex);
    }

    protected void addImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButton = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        Add(rowindex);
    }
}