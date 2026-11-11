using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MasterSetup_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_ProjectWiseEmployeeAllocationEntry : System.Web.UI.Page
{
    tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
    tblEmployeePromotionEntryDAL atblEmployeePromotionEntryDAL = new tblEmployeePromotionEntryDAL();
    ProjectWiseEmployeeAllocationEntryDAL dal=new ProjectWiseEmployeeAllocationEntryDAL();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ActiveDateTextBox.Attributes.Add("readonly","readonly");
            ButtonVisible();
            SearchViewPanel.Visible = true;
            ShowExistingAndNew.Visible = true;
            LoadDropDownList();

            if (Session["EmployeePromotionEntryId"] != null)
            {
                EmployeePromotionEntryIdHiddenField.Value = Session["EmployeePromotionEntryId"].ToString();
                GetOneRecord(Session["EmployeePromotionEntryId"].ToString());
                Session["EmployeePromotionEntryId"] = null;
            }
        }
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
       

    }

    private void GetOneRecord(string idd)
    {

         submitButton.Text = "Update";
        submitButton.BackColor = Color.DodgerBlue;

        DataTable aDataTable = atblEmployeePromotionEntryDAL.GetEmployeePromotionEntryIdById(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {
            EmployeePromotionEntryIdHiddenField.Value = Session["EmployeePromotionEntryId"].ToString();
            ShowExistingAndNew.Visible = true;
            SearchViewPanel.Visible = true;

            DataTable dtdata =
                atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationDS(
                    EmployeePromotionEntryIdHiddenField.Value);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();
         

            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();

            if (companyDropDownList.SelectedValue != "")
            {
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
          atblEmployeePromotionEntryDAL.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
          ddlEmpInfo.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId").ToString();

          DataTable dtadata = atblEmployeePromotionEntryDAL.LoadSuperviseEmployee(ddlEmpInfo.SelectedValue.ToString());
            presuperGridView.DataSource = dtadata;
            presuperGridView.DataBind();

            lblEmp.Text = aDataTable.Rows[0]["EmployeeName"].ToString();


            lblEmployeeCode.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(aDataTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = aDataTable.Rows[0]["Designation"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = aDataTable.Rows[0]["GradeName"].ToString();
            lblDivision.Text = aDataTable.Rows[0]["DivisionName"].ToString();
            lblWing.Text = aDataTable.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = aDataTable.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = aDataTable.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = aDataTable.Rows[0]["SubSectionName"].ToString();
 
            
            FinancialYearDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("FinancialYearId").ToString();
      
         
            NewReportingBodyTextBox.Text = aDataTable.Rows[0]["NewReportingEmployeeName"].ToString();
            atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(PreviousDesignationDropDownList);
            atblEmployeePromotionEntryDAL.LoadPreSalaryGradeDropDownList(PreviouSalaryGradeDropDownList);
            NewSalaryGradeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NSalGradeId").ToString();
            atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownListBySalaryId(NewDesignationDropDownList, NewSalaryGradeDropDownList.SelectedValue);
            NewDesignationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NDesignationId").ToString();
            PromotionTypeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NPromoTypeId").ToString();
            OtherRemarksTextBox.Text = aDataTable.Rows[rowIndex].Field<string>("Remarks").ToString();           
            atblEmployeePromotionEntryDAL.EmployeeNameDropDown(PReportingBodyDropDownList, companyDropDownList.SelectedValue);
            PreviousDesignationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("PDesignationId").ToString();
            PreviouSalaryGradeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("PSalGradeId").ToString();
            PReportingBodyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("PRepEmpId").ToString();
            HiddenFieldNewReportingBody.Value = aDataTable.Rows[rowIndex].Field<Int32>("NRepEmpId").ToString();


            ActiveDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("NActiveDate").ToString("dd-MMM-yyyy");
            EntryDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("NEntryDate").ToString("dd-MMM-yyyy");
        }

    }

    private void LoadDataGetOneRecord(int id)
    {
            atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(PreviousDesignationDropDownList);
      
        atblEmployeePromotionEntryDAL.LoadPreSalaryGradeDropDownList(PreviouSalaryGradeDropDownList);
     

        DataTable dtdata = new DataTable();
        dtdata = atblEmployeePromotionEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {
           


       
        }
    }


    private void LoadDropDownList()
    {

        atblEmployeePromotionEntryDAL.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(PreviousDesignationDropDownList);
      //  atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(NewDesignationDropDownList);
        atblEmployeePromotionEntryDAL.LoadPreSalaryGradeDropDownList(NewSalaryGradeDropDownList);
        atblEmployeePromotionEntryDAL.LoadPromotionTypeDropDownList(PromotionTypeDropDownList);
       


    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            LoadData(Convert.ToInt32(ddlEmpInfo.SelectedValue));
            
        }
        catch (Exception)
        {

        }
      
    }

    private void LoadData(int id)
    {


        
      atblEmployeePromotionEntryDAL.LoadPreSalaryGradeDropDownList(PreviouSalaryGradeDropDownList);
      

        DataTable dtdata = new DataTable();
        dtdata = atblEmployeePromotionEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {


            DataTable dtadata = atblEmployeePromotionEntryDAL.LoadSuperviseEmployee(id.ToString());
            presuperGridView.DataSource = dtadata;
            presuperGridView.DataBind();

    

            
          PreviousDesignationDropDownList.SelectedValue = dtdata.Rows[0]["DesignationId"].ToString();
           
       PreviouSalaryGradeDropDownList.SelectedValue = dtdata.Rows[0]["SalaryGradeId"].ToString();
       PReportingBodyDropDownList.SelectedValue = dtdata.Rows[0]["ReportingEmpId"].ToString();
            
           
   


        }


          DataTable ExistingProjectdtdata = new DataTable();
          ExistingProjectdtdata = dal.LoadExistingProject(id);
          if (ExistingProjectdtdata.Rows.Count > 0)
        {
            GVExistingProject.DataSource = ExistingProjectdtdata;
            GVExistingProject.DataBind();

            string masterId = ExistingProjectdtdata.Rows[0]["EmployeeWiseProjectAllocationMasterId"].ToString();

            HfMasterID.Value = masterId;

                     for (int i = 0; i < GVExistingProject.Rows.Count; i++)
            {
                string dd = ExistingProjectdtdata.Rows[i]["IsActive"].ToString();

                if (dd == "True")
                {

                    CheckBox RadioButtonList1 =
                        ((CheckBox)GVExistingProject.Rows[i].Cells[0].FindControl("txt_check")) as
                            CheckBox;


                    RadioButtonList1.Checked = true;

                }

                if (dd == "False")
                {

                    CheckBox RadioButtonList1 =
                          ((CheckBox)GVExistingProject.Rows[i].Cells[0].FindControl("txt_check")) as
                              CheckBox;


                    RadioButtonList1.Checked = false;

                }
            }

        }
          else
          {
              GVExistingProject.DataSource = null;
              GVExistingProject.DataBind();
          }
          

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionEntryView.aspx");
    }


    


    private void LoadDataFoRAddData(int id)
    {



      
    }

    private
         bool Validation()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
            companyDropDownList.Focus();
            return false;
        }


        if (FinancialYearDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Financial Year!!!", this);
            FinancialYearDropDownList.Focus();
            return false;
        }

        if (NewDesignationDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select New Designation !!!", this);
            NewDesignationDropDownList.Focus();
            return false;
        }


        if (NewSalaryGradeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select New Salary Grade !!!", this);
            NewSalaryGradeDropDownList.Focus();
            return false;
        }

    


        if (PromotionTypeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
            PromotionTypeDropDownList.Focus();
            return false;
        }

        

        if (ActiveDateTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Enter Active Date !!!", this);
            ActiveDateTextBox.Focus();
            return false;
        }

      
        return true;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (GVExistingProject.Rows.Count!=0)
        {
            Save();
        }
        else
        {
            ShowMessageBox("Please Select At Least One Project!!!");
        }
        

    }
    private bool UpdateAreaInformation(ProjectWiseEmployeeAllocationDAO prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = dal.UpdateVacancyEntryInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private ProjectWiseEmployeeAllocationDAO PrepareDataForUpdate()
    {
        var aEmpTransferAndRedesignationDao = new ProjectWiseEmployeeAllocationDAO();

        aEmpTransferAndRedesignationDao.EmployeeWiseProjectAllocationMasterId = Convert.ToInt32(HfMasterID.Value);
        aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);



        aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
        aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;

        return aEmpTransferAndRedesignationDao;
    }
    public void Save()
    {
        

            ProjectWiseEmployeeAllocationDAO aEmpTransferAndRedesignationDao = new ProjectWiseEmployeeAllocationDAO();

        //    aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);



            aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
            aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;

            int id =0;

            if (HfMasterID.Value!="")
            {
                id = Convert.ToInt32(HfMasterID.Value);
                bool area = UpdateAreaInformation(PrepareDataForUpdate());
                //dal.UpdateVacancyEntryInfo(aEmpTransferAndRedesignationDao);
            }
            else
            {

                id = dal.SaveInfo(aEmpTransferAndRedesignationDao);
            }
            if (id > 0)
            {
                dal.DeleteDEtails(id.ToString());
                for (int i = 0; i < GVExistingProject.Rows.Count; i++)
                {
                    CheckBox ChkBoxRows = (CheckBox)GVExistingProject.Rows[i].Cells[0].FindControl("txt_check");
                    ProjectWiseEmployeeAllocationDetailDAO andRedesignationDao = new ProjectWiseEmployeeAllocationDetailDAO()
                    {

                        EmployeeWiseProjectAllocationMasterId = id,
                        ProjectId = Convert.ToInt32(GVExistingProject.DataKeys[i][1].ToString()),
                        IsActive = ChkBoxRows.Checked
                    };
                    int idd =
                        dal.SaveInfoDetails(andRedesignationDao);
                }
                dal.DeleteDEtailsProjectId(id.ToString());

                ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Operation successfully done...');window.location ='ProjectWiseEmployeeAllocationEntry.aspx';",
             true);



            }



 
    }

  
    private void Clear()
    {
        PromotionTypeDropDownList.SelectedValue = "";
        NewDesignationDropDownList.SelectedValue = "";
        FinancialYearDropDownList.SelectedValue = "";
      
     
        companyDropDownList.SelectedValue = "";
        ddlEmpInfo.SelectedValue = string.Empty;
        ShowExistingAndNew.Visible = true;
        SearchViewPanel.Visible = true;
        HiddenFieldNewReportingBody.Value = "";
        EmployeePromotionEntryIdHiddenField.Value = "";
        
        EntryDateTextBox.Text = "";
        ActiveDateTextBox.Text = "";
        OtherRemarksTextBox.Text = "";
        NewReportingBodyTextBox.Text = "";
        NewSalaryGradeDropDownList.SelectedValue = "";
        PReportingBodyDropDownList.SelectedValue = "";
        PreviousDesignationDropDownList.SelectedValue = "";
        PreviouSalaryGradeDropDownList.SelectedValue = "";




        lblEmp.Text = "";


        lblEmployeeCode.Text = "";
        lblJdate.Text = "";
        lblDesignation.Text = "";

        //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

        lblSalaryGrade.Text = "";
        lblDivision.Text = "";
        lblWing.Text = "";
        lblDepartment.Text = "";
        lblSection.Text = "";
        lblSubSection.Text = "";

    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionEntryView.aspx");
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
        if (companyDropDownList.SelectedValue != "")
        {
           
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            atblEmployeePromotionEntryDAL.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
            atblEmployeePromotionEntryDAL.EmployeeNameDropDown(PReportingBodyDropDownList, companyDropDownList.SelectedValue);

            dal.LoaProjectDropDownList(ProjectDropDownList, companyDropDownList.SelectedValue);

            using (DataTable dt222 = _commonDataLoad.GetEmpDDLForEntry(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;



            }

        }
        
        
        

       
    }

    private void LoadEmployeedataData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = atblEmployeePromotionEntryDAL.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();

           
            lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = dtdata.Rows[0]["SalaryGrade"].ToString();
            lblDivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
            lblWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = dtdata.Rows[0]["SubSectionName"].ToString();


            lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = dtdata.Rows[0]["SubSectionName"].ToString();


            lblOffice.Text = dtdata.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = dtdata.Rows[0]["Location"].ToString();



        }
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
       
    }

    protected void NewReportingBodyTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = companyDropDownList.SelectedValue;

            string empName = NewReportingBodyTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':', ':');

                NewReportingBodyTextBox.Text = emp[2];
                HiddenFieldNewReportingBody.Value = emp[0];

                // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                //productNameTextBox.Text = productInfo[1];
                //string productCode = productCodeTextBox.Text.Trim();

            }
            else
            {

                NewReportingBodyTextBox.Text = "";
                HiddenFieldNewReportingBody.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }
        }
       
    }

    protected void NewSalaryGradeDropDownList_Changed(object sender, EventArgs e)
    {
        atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownListBySalaryId(NewDesignationDropDownList, NewSalaryGradeDropDownList.SelectedValue);
       

    }

    protected void EntryDateTextBox_Changed(object sender, EventArgs e)
    {
        if (EntryDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(EntryDateTextBox.Text);
            }
            catch
            {
                EntryDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void ActiveDateTextBox_Changed(object sender, EventArgs e)
    {



        if (FinancialYearDropDownList.SelectedValue != "")
        {
            if (ActiveDateTextBox.Text != "")
            {


                if (CheckStartEndDateExistOrNot(ActiveDateTextBox.Text, ActiveDateTextBox.Text) == true)
                {

                }
                if (CheckStartEndDateExistOrNot(ActiveDateTextBox.Text, ActiveDateTextBox.Text) == false)
                {
                    aShowMessage.ShowMessageBox("Effective date must be within the finnancial year!!", this);
                    ActiveDateTextBox.Text = "";
                    ActiveDateTextBox.Focus();

                }


            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this.", this);
            ActiveDateTextBox.Text = "";
            FinancialYearDropDownList.Focus();
        }




    }


    private bool CheckStartEndDateExistOrNot(string Start , string End)
    {
        bool status = false;

        DataTable dataTable = atblEmployeePromotionEntryDAL.CheckStartEndDateExistOrNotDAL(FinancialYearDropDownList.SelectedValue, Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
       
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (atblEmployeePromotionEntryDAL.DeleteUpdateEmployeePromotionEntryById(EmployeePromotionEntryIdHiddenField.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                         "alert",
                         "alert('Data Delete Successfull...');window.location ='EmployeePromotionEntryView.aspx';",
                         true);

        }

        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }
    protected void directlySuperTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = directlySuperTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':', ':');

            directlySuperTextBox.Text = emp[2];
            directlyEmpIdHiddenField.Value = emp[0];

            // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {

            directlySuperTextBox.Text = "";
            directlyEmpIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    public void Add()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            dataRow["EmpName"] = loadGridView.Rows[i].Cells[0].Text;
            dataRow["EmpInfoId"] = loadGridView.DataKeys[i][0].ToString();
            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["EmpName"] = directlySuperTextBox.Text;
        dataRow["EmpInfoId"] = directlyEmpIdHiddenField.Value;
        aDataTable.Rows.Add(dataRow);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
        directlySuperTextBox.Text = string.Empty;
        directlyEmpIdHiddenField.Value = string.Empty;
    }
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
      
        aDataTable.Columns.Add("ProjectId");
        aDataTable.Columns.Add("EmpWiseProjectDetailID");

        aDataTable.Columns.Add("ProjectName");
        aDataTable.Columns.Add("ProjectStartDate");
        aDataTable.Columns.Add("ProjectEndDate");
        DataRow dataRow = null;
        for (int i = 0; i < GVExistingProject.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["ProjectId"] = GVExistingProject.DataKeys[i][1].ToString();
                dataRow["EmpWiseProjectDetailID"] = GVExistingProject.DataKeys[i][0].ToString();
                dataRow["ProjectName"] = GVExistingProject.Rows[i].Cells[1].Text;
                try
                {
                    dataRow["ProjectStartDate"] = ((TextBox)GVExistingProject.Rows[i].FindControl("ProjectStartDate")).Text;
                    dataRow["ProjectEndDate"] = ((TextBox)GVExistingProject.Rows[i].FindControl("ProjectEndDate")).Text;
                }
                catch (Exception)
                {

                    //throw;
                }
                aDataTable.Rows.Add(dataRow);
            }
        }
        GVExistingProject.DataSource = aDataTable;
        GVExistingProject.DataBind();

    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Add();
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButton = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        Remove(rowindex);
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void btnAddTolist_OnClick(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {

            if (ddlEmpInfo.SelectedValue != "")
            {


                if (AddJobTitleValidation())
                {

                    //DataTable ExistingProjectdtdata = new DataTable();
                    //ExistingProjectdtdata = dal.LoadExistingProject(Convert.ToInt32(repEmpIdHiddenField.Value));
                    //if (ExistingProjectdtdata.Rows.Count > 0)
                    //{
                    //    GVExistingProject.DataSource = ExistingProjectdtdata;
                    //    GVExistingProject.DataBind();

                    //}

                    string eRID = ProjectDropDownList.SelectedValue;
                    string educationRequirements = ProjectDropDownList.SelectedItem.Text.Trim();


                    var aDataTable = new DataTable();

                    aDataTable.Columns.Add("ProjectId");
                    aDataTable.Columns.Add("EmpWiseProjectDetailID");

                    aDataTable.Columns.Add("ProjectName");
                    aDataTable.Columns.Add("ProjectStartDate");
                    aDataTable.Columns.Add("ProjectEndDate");




                    DataRow dataRow;


                    for (int i = 0; i < GVExistingProject.Rows.Count; i++)
                    {
                        if (GVExistingProject.Rows[i].Cells[1].Text != educationRequirements)
                        {
                            dataRow = aDataTable.NewRow();

                            dataRow["ProjectId"] = GVExistingProject.DataKeys[i][1].ToString();
                            dataRow["EmpWiseProjectDetailID"] = GVExistingProject.DataKeys[i][0].ToString();
                            dataRow["ProjectName"] = GVExistingProject.Rows[i].Cells[1].Text;
                            try
                            {

                                if (GVExistingProject.Rows[i].Cells[2].Text.Trim() != "")
                                {
                                    dataRow["ProjectStartDate"] =
                                        Convert.ToDateTime(GVExistingProject.Rows[i].Cells[2].Text.Trim())
                                            .ToString("dd-MMM-yyyy");

                                }

                                if (GVExistingProject.Rows[i].Cells[3].Text.Trim() != "")
                                {
                                    dataRow["ProjectEndDate"] =
                                        Convert.ToDateTime(GVExistingProject.Rows[i].Cells[3].Text.Trim())
                                            .ToString("dd-MMM-yyyy");

                                }


                            }
                            catch (Exception)
                            {

                                //throw;
                            }


                            aDataTable.Rows.Add(dataRow);
                        }
                        else
                        {
                            ProjectDropDownList.SelectedValue = "";
                            ShowMessageBox("Data already Exist !!");
                        }
                    }

                    dataRow = aDataTable.NewRow();

                    dataRow["ProjectId"] = eRID;
                    dataRow["EmpWiseProjectDetailID"] = DBNull.Value;
                    dataRow["ProjectName"] = educationRequirements;

                    DataTable dtdataNEW = dal.LoadNewProject(Convert.ToInt32(eRID));



                    if (dtdataNEW.Rows.Count > 0)
                    {

                        dataRow["ProjectStartDate"] =
                            Convert.ToDateTime(dtdataNEW.Rows[0]["ProjectStartDate"].ToString().Trim())
                                .ToString("dd-MMM-yyyy");

                        try
                        {

                            dataRow["ProjectEndDate"] =
                                Convert.ToDateTime(dtdataNEW.Rows[0]["ProjectEndDate"].ToString().Trim())
                                    .ToString("dd-MMM-yyyy");
                        }
                        catch (Exception)
                        {

                            //  throw;
                        }

                    }


                    aDataTable.Rows.Add(dataRow);





                    GVExistingProject.DataSource = aDataTable;
                    GVExistingProject.DataBind();

                    DataTable ExistingProjectdtdata = new DataTable();
                    ExistingProjectdtdata = dal.LoadExistingProject(Convert.ToInt32(ddlEmpInfo.SelectedValue));
                    if (ExistingProjectdtdata.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVExistingProject.Rows.Count; i++)
                        {
                            string dd = "False";
                            try
                            {
                                dd = ExistingProjectdtdata.Rows[i]["IsActive"].ToString();
                            }
                            catch (Exception)
                            {


                            }

                            if (dd == "True")
                            {

                                CheckBox RadioButtonList1 =
                                    ((CheckBox) GVExistingProject.Rows[i].Cells[0].FindControl("txt_check")) as
                                        CheckBox;


                                RadioButtonList1.Checked = true;

                            }

                            if (dd == "False")
                            {

                                CheckBox RadioButtonList1 =
                                    ((CheckBox) GVExistingProject.Rows[i].Cells[0].FindControl("txt_check")) as
                                        CheckBox;


                                RadioButtonList1.Checked = false;

                            }
                        }
                    }
                    ProjectDropDownList.SelectedValue = "";

                }
            }
            else
            {
                ShowMessageBox("Please Select a Employee!!!");
            }
        }
         else
        {
            ShowMessageBox("Please Select a Company!!!");
        }
    }
    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool AddJobTitleValidation()
    {
        if (ProjectDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select this !!!");
            ProjectDropDownList.Focus();
            return false;
        }

        return true;
    }
    protected void Project_Sel(object sender, EventArgs e)
    {
        LoadDataFoRAddData(Convert.ToInt32(ProjectDropDownList.SelectedValue));
    }

    protected void deleteImageButtonPro_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButton = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        Remove(rowindex);
    }

    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ActiveDateTextBox.Text = "";
    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
            {
                CheckBox ChkBoxHeader = (CheckBox)GVExistingProject.HeaderRow.FindControl("txt_checkAll");
                bool result = ChkBoxHeader.Checked == true ? true : false;

                for (int i = 0; i < GVExistingProject.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GVExistingProject.Rows[i].FindControl("txt_check");
                    chk.Checked = result;
                }
    }

    protected void ddlEmpInfo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpInfo.SelectedValue != "")
        {



            LoadData(Convert.ToInt32(ddlEmpInfo.SelectedValue));

            LoadEmployeedataData(Convert.ToInt32(ddlEmpInfo.SelectedValue));
                SearchViewPanel.Visible = true;

                try
                {
                    ShowExistingAndNew.Visible = true;
                    LoadData(Convert.ToInt32(ddlEmpInfo.SelectedValue));



                    ShowExistingAndNew.Visible = true;
                    ShowExistingAndNew.Visible = true;
                }
                catch (Exception)
                {

                }

            }
             

     
        
    }
}