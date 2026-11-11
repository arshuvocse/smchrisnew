using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class Transfer_UI_EmployeeReDesignationEntry : System.Web.UI.Page
{
    EmployeeReDesignationDAO aEmployeeReDesignationDAO = new EmployeeReDesignationDAO();
    EmployeeReDesignationDAL atblEmployeePromotionEntryDAL = new EmployeeReDesignationDAL();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ActiveDateTextBox.Attributes.Add("readonly", "readonly");
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
            catch (Exception)
            {
                
                //throw;
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
        else
        {
            Response.Redirect("EmployeeReDesignationView.aspx");
        }

    }

    private void GetOneRecord(string idd)
    {

        
        DataTable aDataTable = atblEmployeePromotionEntryDAL.GetEmployeePromotionEntryIdById(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {
            EmployeePromotionEntryIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeReDesignationId").ToString();
            ShowExistingAndNew.Visible = true;
            SearchViewPanel.Visible = true;

     
         
            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();
            
           
            //aDataTable.Rows[0]["IsReappointment"] = Chkreappointment.Checked;

           

            manualUpdateCheckBox.Enabled = false;

            HFDesgId.Value = aDataTable.Rows[0]["DesignationId"].ToString();
            HFDivID.Value = aDataTable.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = aDataTable.Rows[0]["DivisionWId"].ToString();
            HFDeptID.Value = aDataTable.Rows[0]["DepartmentId"].ToString();
            HFSecID.Value = aDataTable.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = aDataTable.Rows[0]["SubSectionId"].ToString();

            HFEmpCode.Value = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            HFEmpTypeID.Value = aDataTable.Rows[0]["EmpTypeId"].ToString();
            HFSalLocID.Value = aDataTable.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = aDataTable.Rows[0]["JobLocationId"].ToString();
         

            if (companyDropDownList.SelectedValue != "")
            {
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
          atblEmployeePromotionEntryDAL.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
            repEmpIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId").ToString();

            

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
            lblOffice.Text = aDataTable.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = aDataTable.Rows[0]["Location"].ToString();
            SearchEmployeeNameTextBoxTextBox.Text = aDataTable.Rows[0]["EmployeeName"].ToString();
            FinancialYearDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("FinancialYearId").ToString();
      
         
           
            atblEmployeePromotionEntryDAL.LoadOlddesignationDropDownList(PreviousDesignationDropDownList);
            atblEmployeePromotionEntryDAL.LoadPreSalaryGradeDropDownList(PreviouSalaryGradeDropDownList);

            
            //atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownListBySalaryId(NewDesignationDropDownList, NewSalaryGradeDropDownList.SelectedValue);
            NewDesignationDropDownList.SelectedValue = string.IsNullOrEmpty(aDataTable.Rows[rowIndex]["NDesignationId"].ToString()) ? "0" : aDataTable.Rows[rowIndex]["NDesignationId"].ToString();
            
            OtherRemarksTextBox.Text = aDataTable.Rows[rowIndex].Field<string>("Remarks").ToString();
            atblEmployeePromotionEntryDAL.EmployeeNameDropDown(PReportingBodyDropDownList, companyDropDownList.SelectedValue);
            if (aDataTable.Rows[0]["PDesignationId"] != DBNull.Value)
            {
                PreviousDesignationDropDownList.SelectedValue =
                    aDataTable.Rows[rowIndex].Field<Int32>("PDesignationId").ToString();
            }

            
            
            
          

           
            if (aDataTable.Rows[0]["Effectivedate"] != DBNull.Value)
            {

                ActiveDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("Effectivedate")
                    .ToString("dd-MMM-yyyy");
            }

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

        atblEmployeePromotionEntryDAL.LoadOlddesignationDropDownList (PreviousDesignationDropDownList);
      //  atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(NewDesignationDropDownList);
        atblEmployeePromotionEntryDAL.LoadNewSalaryGradeDropDownList(NewSalaryGradeDropDownList);
       atblEmployeePromotionEntryDAL.LoadPromotionTypeDropDownList(PromotionTypeDropDownList);
       atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(NewDesignationDropDownList);


       atblEmployeePromotionEntryDAL.LoadSalaryStepDropDownListOld(PreviouSalaryStepDropDownList);




    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
            
        }
        catch (Exception)
        {

        }
      
    }

    private void LoadData(int id)
    {



        try
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
                PreviouSalaryStepDropDownList.SelectedValue = dtdata.Rows[0]["SalaryStepId"].ToString();
                try
                {
                    PReportingBodyDropDownList.SelectedValue = dtdata.Rows[0]["ReportingEmpId"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }





            }

            DataTable LoadAllEmpInfo = new DataTable();
            LoadAllEmpInfo = atblEmployeePromotionEntryDAL.LoadEmpJInfoInTextBoxById(id);
            if (LoadAllEmpInfo.Rows.Count > 0)
            {
                HFDesgId.Value = LoadAllEmpInfo.Rows[0]["DesignationId"].ToString();
                HFDivID.Value = LoadAllEmpInfo.Rows[0]["DivisionId"].ToString();
                HFDivWingId.Value = LoadAllEmpInfo.Rows[0]["DivisionWId"].ToString();
                HFDeptID.Value = LoadAllEmpInfo.Rows[0]["DepartmentId"].ToString();
                HFSecID.Value = LoadAllEmpInfo.Rows[0]["SectionId"].ToString();
                HFSubSecID.Value = LoadAllEmpInfo.Rows[0]["SubSectionId"].ToString();

                HFEmpCode.Value = LoadAllEmpInfo.Rows[0]["EmpMasterCode"].ToString();
                HFEmpTypeID.Value = LoadAllEmpInfo.Rows[0]["EmpTypeId"].ToString();
                HFSalLocID.Value = LoadAllEmpInfo.Rows[0]["SalaryLoationId"].ToString();
                HFJobLocID.Value = LoadAllEmpInfo.Rows[0]["JobLocationId"].ToString();


            }
        }
        catch (Exception)
        {
            
            //throw;
        }

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeReDesignationEntry.aspx");
    }


    public void Save()
    {
        try
        {
             if (Validation())
            {
                DataTable aTable =
                               atblEmployeePromotionEntryDAL.ValidattionForEffectiveDate(
                                     repEmpIdHiddenField.Value, ActiveDateTextBox.Text);

                if (aTable.Rows.Count > 0)
                {
                    aShowMessage.ShowMessageBox("Data Can not be Inserted", this);
                }
                else
                {

                    EmployeeReDesignationDAO aEmployeeReDesignationDAO = new EmployeeReDesignationDAO();
                    aEmployeeReDesignationDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                    aEmployeeReDesignationDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                    aEmployeeReDesignationDAO.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
                    aEmployeeReDesignationDAO.Effectivedate = Convert.ToDateTime(ActiveDateTextBox.Text);
                


                    if (HFDivID.Value != "")
                    {
                        aEmployeeReDesignationDAO.DivisionId =
                            Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
                    }

                    if (HFDivWingId.Value != "")
                    {
                        aEmployeeReDesignationDAO.DivisionWId =
                            Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
                    }

                    if (HFDeptID.Value != "")
                    {
                        aEmployeeReDesignationDAO.DepartmentId =
                            Convert.ToInt32(HFDeptID.Value) > 0 ? int.Parse(HFDeptID.Value) : (int?)null;
                    }
                    if (HFSecID.Value != "")
                    {
                        aEmployeeReDesignationDAO.SectionId =
                            Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
                    }

                    if (HFSubSecID.Value != "")
                    {
                        aEmployeeReDesignationDAO.SubSectionId =
                            Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
                    }

                    aEmployeeReDesignationDAO.EmployeeCode =
                        Convert.ToInt32(HFEmpCode.Value) > 0 ? int.Parse(HFEmpCode.Value) : (int?)null;

                    if (HFEmpTypeID.Value != "")
                    {
                        aEmployeeReDesignationDAO.EmpTypeId =
                            Convert.ToInt32(HFEmpTypeID.Value) > 0 ? int.Parse(HFEmpTypeID.Value) : (int?)null;
                    }


                    if (HFSalLocID.Value != "")
                    {
                        aEmployeeReDesignationDAO.SalaryLoationId =
                            Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
                    }

                    if (HFJobLocID.Value != "")
                    {
                        aEmployeeReDesignationDAO.JobLocationId =
                            Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
                    }




                    aEmployeeReDesignationDAO.PDesignationId =
                        PreviousDesignationDropDownList.SelectedIndex > 0
                            ? int.Parse(PreviousDesignationDropDownList.SelectedValue)
                            : (int?)null;

                    

                    
 



                    aEmployeeReDesignationDAO.NDesignationId =
                        NewDesignationDropDownList.SelectedIndex > 0
                            ? int.Parse(NewDesignationDropDownList.SelectedValue)
                            : (int?)null;

                    
                    

                    

                   

                    aEmployeeReDesignationDAO.Remarks = Convert.ToString(OtherRemarksTextBox.Text);
                    aEmployeeReDesignationDAO.EntryBy = Session["UserId"].ToString();
                    aEmployeeReDesignationDAO.EntryDate = DateTime.Now;
                  
                    if (manualUpdateCheckBox.Checked)
                    {
                        aEmployeeReDesignationDAO.AutoProcess = "Manually Updated";
                    }

                    int id = atblEmployeePromotionEntryDAL.EmployeePromotionEntrySaveInfo(aEmployeeReDesignationDAO);


                    //For Employee Master Information update ------------------------------------------------------------------------

                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                      
                        Int32 desigId = 0;
                        

                        empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                       

                       
                        desigId = Convert.ToInt32(NewDesignationDropDownList.SelectedIndex > 0
                            ? int.Parse(NewDesignationDropDownList.SelectedValue)
                            : (int?)null);

                       


                        UpdateEmployeeStepId(empGenId,   desigId );
                    }

                    //--------------------------------------------------------------------------------------------------------------



                   
 
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation successfully done...');window.location ='EmployeeReDesignationView.aspx';",
                      true);
                    
                  
                }


            }
        }
        catch (Exception)
        {
            
            
        }
    }

    private void UpDateSuperVisorInfoByNULL(int empId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.EmpInfoId = empId;

        atblEmployeePromotionEntryDAL.UpdateEmployeeSuperVisorIdToNull(empId);
    }

    private void UpDateSuperVisorInfo(int empId, int reportingBodyId)
    {

        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.EmpInfoId = empId;

        if (reportingBodyId != 0)
        {
            aInfo.LineId = reportingBodyId;
        }
        
        atblEmployeePromotionEntryDAL.UpdateEmployeeSuperVisorId(aInfo);
    }

    private void UpdateEmployeeStepId(int empGenId,   int desigId )
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

       
        aInfo.DesigId = desigId;
       
        aInfo.EmpInfoId = empGenId;

        atblEmployeePromotionEntryDAL.UpdateEmployeeExitInfo(aInfo);

    }

    private bool Validation()
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


        

         


      
        

        if (ActiveDateTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Enter Effective Date !!!", this);
            ActiveDateTextBox.Focus();
            return false;
        }


        if (OtherRemarksTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Enter Remarks !!!", this);
            OtherRemarksTextBox.Focus();
            return false;
        }

      
        return true;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (EmployeePromotionEntryIdHiddenField.Value == string.Empty)
        {
            //DataTable dtdata = atblEmployeePromotionEntryDAL.GetEmployeePromotion(repEmpIdHiddenField.Value);
            //if (dtdata.Rows.Count < 1)
            //{
                Save();
            //}
            //else
            //{
            //    aShowMessage.ShowMessageBox("Employee Promotion Already Exist ", this);
            //}
        }

        
    }

    private void Update()
    {
        try
        {
          if (Validation())
            {
                EmployeeReDesignationDAO aEmployeeReDesignationDAO = new EmployeeReDesignationDAO();

                aEmployeeReDesignationDAO.EmployeeReDesignationId = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value);




                aEmployeeReDesignationDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                aEmployeeReDesignationDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                aEmployeeReDesignationDAO.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
                aEmployeeReDesignationDAO.Effectivedate = Convert.ToDateTime(ActiveDateTextBox.Text);
              
                if (PreviousDesignationDropDownList.SelectedIndex > 0)
                {
                    aEmployeeReDesignationDAO.PDesignationId =
                        Convert.ToInt32(PreviousDesignationDropDownList.SelectedValue);
                }

           
                
                if (NewDesignationDropDownList.SelectedIndex > 0)
                {
                    aEmployeeReDesignationDAO.NDesignationId = Convert.ToInt32(NewDesignationDropDownList.SelectedValue);
                }
               
                
                if (HFSalLocID.Value != "")
                {
                    aEmployeeReDesignationDAO.SalaryLoationId =
                        Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
                }

                if (HFJobLocID.Value != "")
                {
                    aEmployeeReDesignationDAO.JobLocationId =
                        Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
                }

              
                aEmployeeReDesignationDAO.Remarks = Convert.ToString(OtherRemarksTextBox.Text);
                aEmployeeReDesignationDAO.Effectivedate = Convert.ToDateTime(ActiveDateTextBox.Text);



                aEmployeeReDesignationDAO.UpdateBy = Session["UserId"].ToString();
                aEmployeeReDesignationDAO.UpdateDate = DateTime.Now;
                if (HFDesgId.Value != "")
                {
                    aEmployeeReDesignationDAO.DesignationId =
                        Convert.ToInt32(HFDesgId.Value) > 0 ? int.Parse(HFDesgId.Value) : (int?)null;
                }

                if (HFDivID.Value != "")
                {
                    aEmployeeReDesignationDAO.DivisionId =
                        Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
                }

                if (HFDivWingId.Value != "")
                {
                    aEmployeeReDesignationDAO.DivisionWId =
           Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
                }

                if (HFDeptID.Value != "")
                {
                    aEmployeeReDesignationDAO.DepartmentId =
                        Convert.ToInt32(HFDeptID.Value) > 0 ? int.Parse(HFDeptID.Value) : (int?)null;
                }
                if (HFSecID.Value != "")
                {
                    aEmployeeReDesignationDAO.SectionId =
                        Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
                }

                if (HFSubSecID.Value != "")
                {
                    aEmployeeReDesignationDAO.SubSectionId =
                        Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
                }

                aEmployeeReDesignationDAO.EmployeeCode =
                 Convert.ToInt32(HFEmpCode.Value) > 0 ? int.Parse(HFEmpCode.Value) : (int?)null;

                if (HFEmpTypeID.Value != "")
                {
                    aEmployeeReDesignationDAO.EmpTypeId =
                        Convert.ToInt32(HFEmpTypeID.Value) > 0 ? int.Parse(HFEmpTypeID.Value) : (int?)null;
                }

                atblEmployeePromotionEntryDAL.EmployeePromotionEntryUpsateInfo(aEmployeeReDesignationDAO);


                //Manual Update of Employee Information =====================================================================================

                if (manualUpdateCheckBox.Checked)
                {
                    Int32 empGenId = 0;

                    Int32 desigId = 0;


                    empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);



                    desigId = Convert.ToInt32(NewDesignationDropDownList.SelectedIndex > 0
                        ? int.Parse(NewDesignationDropDownList.SelectedValue)
                        : (int?)null);




                    UpdateEmployeeStepId(empGenId, desigId);
                }

                //============================================================================================================================


             
 

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Data Update Successfull...');window.location ='EmployeeReDesignationView.aspx';",
                             true);
            }

      
        }
        catch (Exception)
        {
            
            //throw;
        }
      
    }

    private void Clear()
    {
        PromotionTypeDropDownList.SelectedValue = "";
        NewDesignationDropDownList.SelectedValue = "";
        FinancialYearDropDownList.SelectedValue = "";
      
     
        companyDropDownList.SelectedValue = "";
        SearchEmployeeNameTextBoxTextBox.Text = string.Empty;
        ShowExistingAndNew.Visible = true;
        SearchViewPanel.Visible = true;
        HiddenFieldNewReportingBody.Value = "";
        EmployeePromotionEntryIdHiddenField.Value = "";
        SearchEmployeeNameTextBoxTextBox.Text = "";
        repEmpIdHiddenField.Value = "";
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
        Response.Redirect("EmployeeReDesignationView.aspx");
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
        if (companyDropDownList.SelectedValue != "")
        {
           
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            atblEmployeePromotionEntryDAL.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
            atblEmployeePromotionEntryDAL.EmployeeNameDropDown(PReportingBodyDropDownList, companyDropDownList.SelectedValue);

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
        if (companyDropDownList.SelectedValue!="")
         {
             string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

             if (empName.Contains(':'))
             {
                 string[] emp = empName.Split(':');

                 SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                 repEmpIdHiddenField.Value = emp[0];

                 LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));

                 LoadEmployeedataData(Convert.ToInt32(repEmpIdHiddenField.Value));
                 SearchViewPanel.Visible = true;
                 //SearchEmployeeNameTextBoxTextBox.Text = "";
                 try
                 {
                     ShowExistingAndNew.Visible = true;
                     LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                     ShowExistingAndNew.Visible = true;
                     ShowExistingAndNew.Visible = true;
                 }
                 catch (Exception)
                 {

                 }
                 //productNameTextBox.Text = productInfo[1];
                 //string productCode = productCodeTextBox.Text.Trim();

             }
             else
             {

                 SearchEmployeeNameTextBoxTextBox.Text = "";
                 repEmpIdHiddenField.Value = "";
                 aShowMessage.ShowMessageBox("Input Correct Data !!", this);
             }
            
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a Company !!", this);
            SearchEmployeeNameTextBoxTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            companyDropDownList.Focus();
        }
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
       atblEmployeePromotionEntryDAL.LoadSalaryStepDropDownListBySalaryGradeId(NstepDropDownList, NewSalaryGradeDropDownList.SelectedValue);
       
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



          if (FinancialYearDropDownList.SelectedValue!="")
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
        if (EmployeePromotionEntryIdHiddenField.Value != string.Empty)
       
        {
               DataTable aTable =
                             atblEmployeePromotionEntryDAL.DeleteValidattionForEffectiveDate(EmployeePromotionEntryIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    Update();
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
          DataTable aTable = atblEmployeePromotionEntryDAL.DeleteValidattionForEffectiveDate(EmployeePromotionEntryIdHiddenField.Value.ToString());
        if (aTable.Rows.Count > 0)
        {
            string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
            string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

            if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
            {
                if (
                    atblEmployeePromotionEntryDAL.DeleteUpdateEmployeePromotionEntryById(
                        EmployeePromotionEntryIdHiddenField.Value))
                {


                    DataTable dt = atblEmployeePromotionEntryDAL.GetEmployeePromotionEntryIdById(EmployeePromotionEntryIdHiddenField.Value);

                    Int32 rowIndex = 0;

                    if (dt.Rows.Count > 0)
                    {
                        int empGenId = dt.Rows[rowIndex].Field<Int32>("EmployeeId");
                    
                        int desigId = dt.Rows[rowIndex].Field<Int32>("PDesignationId");
                     

                        UpdateEmployeeStepId(empGenId,   desigId);

                        


 

                       
                    }

                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Data Delete Successfull...');window.location ='EmployeeReDesignationView.aspx';",
                        true);

                }

                else
                {
                    aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

                }
            }
            else
            {
                aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
            }
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

            DataTable aTable = atblEmployeePromotionEntryDAL.GetEmployeeReportingBodyInfo(Convert.ToInt32(directlyEmpIdHiddenField.Value));
            
            if (aTable.Rows.Count > 0)
            {

                if (aTable.Rows[0]["ReportingEmpId"] != DBNull.Value)
                {
                    rptHiddenField.Value = aTable.Rows[0]["ReportingEmpId"].ToString();
                }
                else
                {
                    rptHiddenField.Value = 0.ToString(CultureInfo.InvariantCulture);
                }
                
            }

            // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {
            rptHiddenField.Value = "";
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
        aDataTable.Columns.Add("PrevEmpReportingBodyId");

        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            dataRow["EmpName"] = loadGridView.Rows[i].Cells[0].Text;
            dataRow["EmpInfoId"] = loadGridView.DataKeys[i][0].ToString();
            dataRow["PrevEmpReportingBodyId"] = loadGridView.DataKeys[i][1].ToString();

            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["EmpName"] = directlySuperTextBox.Text;
        dataRow["EmpInfoId"] = directlyEmpIdHiddenField.Value;
        dataRow["PrevEmpReportingBodyId"] = rptHiddenField.Value;

        aDataTable.Rows.Add(dataRow);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        rptHiddenField.Value = string.Empty;
        directlySuperTextBox.Text = string.Empty;
        directlyEmpIdHiddenField.Value = string.Empty;
    }
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("PrevEmpReportingBodyId");


        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["EmpName"] = loadGridView.Rows[i].Cells[0].Text;
                dataRow["EmpInfoId"] = loadGridView.DataKeys[i][0].ToString();
                dataRow["PrevEmpReportingBodyId"] = loadGridView.DataKeys[i][1].ToString();

                aDataTable.Rows.Add(dataRow);
            }
        }
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

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


        DataTable dtdata = atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationDS(EmployeePromotionEntryIdHiddenField.Value);
        int EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString());

        if (dtdata.Rows.Count > 0)
        {
            for (int i = 0; i < dtdata.Rows.Count; i++)
            {
                if (dtdata.Rows[i].Field<Int32>("EmpInfoId") == EmpInfoId)
                {
                    int reptId = 0;

                    if (dtdata.Rows[0]["PrevEmpReportingBodyId"] != DBNull.Value)
                    {
                        reptId = dtdata.Rows[0].Field<Int32>("PrevEmpReportingBodyId");
                    }


                    int empId = dtdata.Rows[i].Field<Int32>("EmpInfoId");
                    UpDateSuperVisorInfo(empId, reptId);
                }
            }    
        }

        Remove(rowindex);
    }

    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ActiveDateTextBox.Text = "";
    }
}