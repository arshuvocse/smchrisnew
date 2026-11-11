using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class Report_Pages_ReportEmpComparison : System.Web.UI.Page
{

    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
         //   SearchEmployeeNameTextBoxTextBox.ReadOnly = true;
            //ButtonVisible();
            ReadOnltDate();
            LoadDropDownList();
            
           
          
            if (Session["ContractualEmpManageId"] != null)
            {
             
                GetOneRecord(Session["ContractualEmpManageId"].ToString());
                Session["ContractualEmpManageId"] = null;
            }
           

        }
    }
    private void ReadOnltDate()
    {
       
    }
    public void ButtonVisible()
    {
        

    }


    private void GetOneRecord(string idd)
    {

        

        DataTable aDataTable = aContractualEmpManageDAL.GetContractualEmpManageById(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {  

          
            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();

           

            if (companyDropDownList.SelectedValue != "")
            {
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
           

            

          
            lblEmp.Text = aDataTable.Rows[0]["EmployeeName"].ToString();
        


            lblEmployeeCode.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text =string.IsNullOrEmpty(aDataTable.Rows[0]["DateOfJoin"].ToString())?"":Convert.ToDateTime(aDataTable.Rows[0]["DateOfJoin"].ToString()).ToString("dd-MMM-yyyy");
            lblDesignation.Text = aDataTable.Rows[0]["Designation"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = aDataTable.Rows[0]["GradeName"].ToString();
            lblDivision.Text = aDataTable.Rows[0]["DivisionName"].ToString();
         
            lblDepartment.Text = aDataTable.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = aDataTable.Rows[0]["SectionName"].ToString();
       
           
            
     

        

          
        }

    }

    private void LoadDropDownList()
    {
        aContractualEmpManageDAL.LoadCompanyDropDownList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);

       

    }
    protected void ExtentionRenewRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ContractualEmpManagementView.aspx");
    }

    protected void ExtensionToDateTextBox_TextChanged(object sender, EventArgs e)
    {
       
    }

    protected void ExtensionFromDateTextBox_TextChanged(object sender, EventArgs e)
    {

        
    }

    protected void RenewToDateTextBox_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void RenewStartDateTextBox_TextChanged(object sender, EventArgs e)
    { 
    }

    protected void EffectiveDaeTextBox_TextChanged(object sender, EventArgs e)
    {
       
    }

    protected void PermanentToContractualEffectiveDaeTextBox_TextChanged(object sender, EventArgs e)
    {
         
    }

    protected void ContractualToPermanentTextBox_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
       

       
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
      Response.Redirect("ContractualEmpList.aspx");
    }


    private bool Validation()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a company!!!", this);
            companyDropDownList.Focus();
            return false;
        }


        

        return true;
    }


     

    


    private void UpdateEmployeeContractualDeate(int empGenId, string ExtensionToDate)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractEndDate = Convert.ToDateTime(ExtensionToDate.ToString());
       
        aInfo.EmpInfoId = empGenId;

        aContractualEmpManageDAL.UpdateEmployeeContractEndDateInfo(aInfo);

    }

    private void UpdateEmployePermanenttoContractualInfoEmpTypeID(int empGenId, string ExtensionToDate, int EmpTypeId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractEndDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;
        aInfo.EmpTypeId = EmpTypeId;

        aContractualEmpManageDAL.UpdateEmployePermanenttoContractualInfoEmpTypeID(aInfo);

    }

    private void UpdateEmployeEmpTypeID(int empGenId, int EmpType)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();



        aInfo.EmpInfoId = empGenId;
        aInfo.EmpTypeId = EmpType;

        aContractualEmpManageDAL.UpdateEmployeEmpTypeID(aInfo);

    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
 

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (companyDropDownList.SelectedValue != "")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(companyDropDownList.SelectedValue.ToString())
                )
            {



                ddlEmp1.DataSource = dt;
                ddlEmp1.DataValueField = "EmpInfoId";
                ddlEmp1.DataTextField = "EmpName";
                ddlEmp1.DataBind();
                ddlEmp1.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmp1.SelectedIndex = 0;



                ddlEmp2.DataSource = dt;
                ddlEmp2.DataValueField = "EmpInfoId";
                ddlEmp2.DataTextField = "EmpName";
                ddlEmp2.DataBind();
                ddlEmp2.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmp2.SelectedIndex = 0;
            }

        }
        else
        {
            ddlEmp1.Items.Clear();
            ddlEmp2.Items.Clear();
            
        }

       
      
        Clear();


 
        
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        //if (companyDropDownList.SelectedValue != "")
        //{
        //    string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

        //    if (empName.Contains(':'))
        //    {
        //        string[] emp = empName.Split(':');

        //        SearchEmployeeNameTextBoxTextBox.Text = emp[2];
        //        repEmpIdHiddenField.Value = emp[0];
               
        //        LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                

        //    }
        //    else
        //    {

        //        SearchEmployeeNameTextBoxTextBox.Text = "";
        //        repEmpIdHiddenField.Value = "";
        //        aShowMessage.ShowMessageBox("Input Correct Data !!", this);
               
        //    }
        //}
        //else
        //{
        //    SearchEmployeeNameTextBoxTextBox.Text = "";
        //    aShowMessage.ShowMessageBox("Please Select A Company !!", this);
        //}
    }

 

    private void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aContractualEmpManageDAL.LoadEmppInfo01(id);
        if (dtdata.Rows.Count > 0)
        {
             


            lblComName.Text = dtdata.Rows[0]["ShortName"].ToString();
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
             lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            try
            {
                lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                lblJdate.Text = "";
                //throw;
            }
            lblRepBoss.Text = dtdata.Rows[0]["Supervisor"].ToString();
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
            lblSalaryGrade.Text = dtdata.Rows[0]["Grade"].ToString();
            lblDivision.Text = dtdata.Rows[0]["Division"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = dtdata.Rows[0]["Section"].ToString();

            lblCat.Text = dtdata.Rows[0]["Category"].ToString();


            //...


            lblSalaryStep.Text = dtdata.Rows[0]["Step"].ToString();
            lblOffice.Text = dtdata.Rows[0]["Office"].ToString();
            lblPlace.Text = dtdata.Rows[0]["Place"].ToString();
            lblGrossSalary.Text = dtdata.Rows[0]["GrossAmount"].ToString();
            lblExp.Text = dtdata.Rows[0]["EmpExperiece"].ToString();
            lblOrg.Text = dtdata.Rows[0]["ExperieceCompany"].ToString();
            

            try
            {
                lblLastPromDate.Text = Convert.ToDateTime(dtdata.Rows[0]["LastPromotion"]).ToString("dd-MMM-yyyy");  
            }
            catch (Exception)
            {
                lblLastPromDate.Text = "";
                //throw;
            }

           
        }
    }


    void Clear()
    {
        lblComName.Text ="";
        lblEmp.Text = "";
        lblEmployeeCode.Text = "";
        lblJdate.Text = "";
        lblRepBoss.Text = "";
        lblDesignation.Text = "";
        lblSalaryGrade.Text = "";
        lblDivision.Text = "";
        lblDepartment.Text = "";
        lblSection.Text = "";

        lblCat.Text = "";


        //...


        lblSalaryStep.Text = "";
        lblOffice.Text = "";
        lblPlace.Text ="";


        lblComName2.Text = "";
        lblEmp2.Text = "";
        lblEmployeeCode2.Text = "";
        lblJdate2.Text = "";
        lblRepBoss2.Text = "";
        lblDesignation2.Text = "";
        lblSalaryGrade2.Text = "";
        lblDivision2.Text = "";
        lblDepartment2.Text = "";
        lblSection2.Text = "";

        lblCat2.Text = "";


        //...


        lblSalaryStep2.Text = "";
        lblOffice2.Text = "";
        lblPlace2.Text = "";
    }

    private void LoadData2(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aContractualEmpManageDAL.LoadEmppInfo01(id);
        if (dtdata.Rows.Count > 0)
        {
         


            lblComName2.Text = dtdata.Rows[0]["ShortName"].ToString();
            lblEmp2.Text = dtdata.Rows[0]["EmpName"].ToString();
            lblEmployeeCode2.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            try
            {
                lblJdate2.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                lblJdate2.Text = "";
                //throw;
            }
            lblRepBoss2.Text = dtdata.Rows[0]["Supervisor"].ToString();
            lblDesignation2.Text = dtdata.Rows[0]["Designation"].ToString();
            lblSalaryGrade2.Text = dtdata.Rows[0]["Grade"].ToString();
            lblDivision2.Text = dtdata.Rows[0]["Division"].ToString();
            lblDepartment2.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection2.Text = dtdata.Rows[0]["Section"].ToString();

            lblCat2.Text = dtdata.Rows[0]["Category"].ToString();


            //...


            lblSalaryStep2.Text = dtdata.Rows[0]["Step"].ToString();
            lblOffice2.Text = dtdata.Rows[0]["Office"].ToString();
            lblPlace2.Text = dtdata.Rows[0]["Place"].ToString();

            lblGrossSalary2.Text = dtdata.Rows[0]["GrossAmount"].ToString();
            lblExp2.Text = dtdata.Rows[0]["EmpExperiece"].ToString();
            lblOrg2.Text = dtdata.Rows[0]["ExperieceCompany"].ToString();


            try
            {
                lblLastPromDate2.Text = Convert.ToDateTime(dtdata.Rows[0]["LastPromotion"]).ToString("dd-MMM-yyyy");  
            }
            catch (Exception)
            {
                lblLastPromDate2.Text = "";
                //throw;
            }

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

 

 
    

    protected void txtEffectiveDate_TextChanged(object sender, EventArgs e)
    {
          
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void ShowPopup(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

    }

    protected void companyDropDownList2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void SearchEmployeeNameTextBoxTextBox2_OnTextChanged(object sender, EventArgs e)
    {
        //if (companyDropDownList.SelectedValue != "")
        //{
        //    string empName = SearchEmployeeNameTextBoxTextBox2.Text.Trim();

        //    if (empName.Contains(':'))
        //    {
        //        string[] emp = empName.Split(':');

        //        SearchEmployeeNameTextBoxTextBox2.Text = emp[2];
        //        repEmpIdHiddenField2.Value = emp[0];

        //        LoadData2(Convert.ToInt32(repEmpIdHiddenField2.Value));


        //    }
        //    else
        //    {

        //        SearchEmployeeNameTextBoxTextBox2.Text = "";
        //        repEmpIdHiddenField2.Value = "";
        //        aShowMessage.ShowMessageBox("Input Correct Data !!", this);

        //    }
        //}
        //else
        //{
        //    SearchEmployeeNameTextBoxTextBox2.Text = "";
        //    aShowMessage.ShowMessageBox("Please Select A Company !!", this);
        //}
    }
    UserProfileDAL aUserProfileDAL = new UserProfileDAL();

    protected void ddlEmp1_OnTextChanged(object sender, EventArgs e)
    {
        if (ddlEmp1.SelectedValue!="")
        {

            LoadData(Convert.ToInt32(ddlEmp1.SelectedValue));


            DataTable EducationInformationdataTable = aUserProfileDAL.GetEmpEducationInfoDAL(ddlEmp1.SelectedValue);



            if (EducationInformationdataTable.Rows.Count > 0)
            {




                gv_Education.DataSource = EducationInformationdataTable;
                gv_Education.DataBind();

            }
            else
            {
                gv_Education.DataSource = null;
                gv_Education.DataBind();
            }

        }
        else
        {
            LoadData(Convert.ToInt32(0));

            gv_Education.DataSource = null;
            gv_Education.DataBind();

        }
       
    }

    protected void ddlEmp2_OnTextChanged(object sender, EventArgs e)
    {
         if (ddlEmp2.SelectedValue!="")
        {
            LoadData2(Convert.ToInt32(ddlEmp2.SelectedValue));

            DataTable EducationInformationdataTable = aUserProfileDAL.GetEmpEducationInfoDAL(ddlEmp2.SelectedValue);



            if (EducationInformationdataTable.Rows.Count > 0)
            {




                gv_Education2.DataSource = EducationInformationdataTable;
                gv_Education2.DataBind();

            }
            else
            {
                gv_Education2.DataSource = null;
                gv_Education2.DataBind();
            }
        }
         else
         {
             LoadData2(Convert.ToInt32(0));
             gv_Education2.DataSource = null;
             gv_Education2.DataBind();
         }
    }
}