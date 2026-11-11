using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.RecruitmentManagement_BLL;
using DAL.DataManager;
using DAL.InternalCls;
using DAL.Permission_DAL;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class RecruitmentManagement_UI_JobCreation : System.Web.UI.Page
{
    JobCreationBll aJobCreationBll = new JobCreationBll();
    JobCreationDal aJobCreationDal = new JobCreationDal();

    PermissionDAL aPermissionDal = new PermissionDAL();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
  ButtonVisible();
            ReadonlyDateTime();
            LoadDropDownList();
            LoadCircularSource();
          


            if (Session["JobId"] != null)
            {
              
                JobCreationIdHiddenField.Value = Session["JobId"].ToString();
                GetOneRecord(Session["JobId"].ToString());
               // 
                Session["JobId"] = null;
            }
            else
            {
                MethodAutoIncri();
            }
        }
    }
    private void ReadonlyDateTime()
    {
        circulationStartDateTextBox.Attributes.Add("readonly", "readonly");
        circulationEndDateTextBox.Attributes.Add("readonly", "readonly");
        probableInterviewDateTextBox.Attributes.Add("readonly", "readonly");
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {

            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
                Button1.Visible = true;
                orBTN.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
                btnUpdateforSubmit.Visible = true;
                orUp.Visible = true;

            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("JobCreationView.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }



    private void LoadCircularSource()
    {
        DataTable aTable = aJobCreationDal.CurculationWayList();

        CheckBoxList1.DataValueField = "VacancyCirculationId";
        CheckBoxList1.DataTextField = "CirculationWay";
        CheckBoxList1.DataSource = aTable;
        CheckBoxList1.DataBind();
    }


    private void GetOneRecord(string jobId)
    {
        submitButton.Text = "Update";
        submitButton.BackColor = Color.DodgerBlue;

        DataTable aDataTable = aJobCreationDal.GetJobCreationInformationById(jobId);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {
            JobCreationIdHiddenField.Value = Session["JobId"].ToString();
                //JobCreationIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<String>("JobID").ToString(CultureInfo.InvariantCulture);
            //jobIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("JobID").ToString(CultureInfo.InvariantCulture);
            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

          
             JobCodetextBox.Text = aDataTable.Rows[0]["JobCode"].ToString();
           
            aJobCreationDal.GetRequisitionCodeList(RequisitionDropDownList, companyDropDownList.SelectedValue);
            RequisitionDropDownList.SelectedValue = //aDataTable.Rows[0]["ReqCodeId"].ToString();
                aDataTable.Rows[rowIndex].Field<Int32>("ReqCodeId").ToString(CultureInfo.InvariantCulture);

            if (RequisitionDropDownList.SelectedValue != "")
            {
                LoadData(Convert.ToInt32(RequisitionDropDownList.SelectedValue));
            }
           
            otherBenefitTextBox.Text = aDataTable.Rows[rowIndex].Field<String>("CompensationandOtherBenefits");
            circulationStartDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("CirculationStartDate").ToString("dd-MMM-yyyy");
            circulationEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("CirculationsdeadlineDate").ToString("dd-MMM-yyyy");
            try
            {
                if ((aDataTable.Rows[rowIndex].Field<DateTime>("ProbableInterviewDate").ToString() != null))
                {
                    probableInterviewDateTextBox.Text =
           aDataTable.Rows[rowIndex].Field<DateTime>("ProbableInterviewDate").ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception)
            {

                probableInterviewDateTextBox.Text = "";
            }
          
            
          
            //probableRecruitmentDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ProbableIRecruitmentDate").ToString("dd-MMM-yyyy");
            remarksTextBox.Text = aDataTable.Rows[rowIndex].Field<String>("Remarks");
            jobContextTextBox.Text = aDataTable.Rows[rowIndex].Field<String>("JobContext");

            IsSalary.Checked = aDataTable.Rows[rowIndex].Field<Boolean>("IsSalary");


            DataTable keyresPon = aJobCreationBll.JobLocationKey(Convert.ToInt32(jobId));

            //if (jobLocationGridView.Rows.Count > 0)
            //{
          jobLocationGridView.DataSource = keyresPon;
          jobLocationGridView.DataBind();
            //}



          DataTable aTable = aJobCreationDal.GetJCPreferedWayOfCircular(JobCreationIdHiddenField.Value);

          for (int i = 0; i < CheckBoxList1.Items.Count; i++)
          {
              CheckBoxList1.Items[i].Selected = false;
          }

          if (aTable.Rows.Count > 0)
          {
              for (int i = 0; i < CheckBoxList1.Items.Count; i++)
              {
                  for (int j = 0; j < aTable.Rows.Count; j++)
                  {
                      if (CheckBoxList1.Items[i].Value == aTable.Rows[j].Field<Int32>("WayId").ToString(CultureInfo.InvariantCulture))
                      {
                          CheckBoxList1.Items[i].Selected = true;
                      }
                  }
              }
          }
        }

    }

    private void LoadDropDownList()
    {

        aJobCreationDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);

        aJobCreationDal.KeyJobCircularLocationDropDown(KeyjobLocationDropDownList);
        //aJobCreationBll.LoadDepartmentList(departmentDropDownList);
        //aJobCreationBll.LoadSectionList(sectionDropDownList);

        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void saveButton_OnClick(object sender, EventArgs e)
    {

        if (JobCreationIdHiddenField.Value == string.Empty)
        {
            string ApprovalStatus = "Submitted";
            Save(ApprovalStatus);
        }
  
    }

 

    private Int64 UpdateDetailInformation(long jobId)
    {
        Int64 retVal = 0;
        try
        {
            if (aJobCreationBll.DeleteJobCreationDetailInformation(jobId))
            {
                retVal = aJobCreationBll.SaveEducationalReqDetail(PrepareEducationalReqDataForSaveDetail(jobId));
                retVal = aJobCreationBll.SaveJobLocationDetail(PrepareJobLocationDataForSaveDetail(jobId));
            }
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }

    private bool UpdateInformation()
    {
        Boolean retVal;
        try
        {
            retVal = aJobCreationBll.UpdateDataBll(PrepareDataForUpdate());

        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }



    public void Update(string Status)
    {


        //JobCreationDao aJobCreationDao = new JobCreationDao();

        //aJobCreationDao.JobID = Convert.ToInt32(JobCreationIdHiddenField.Value);
        // aJobCreationDao.JobCode = JobCodetextBox.Text;
        //aJobCreationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        //aJobCreationDao.ReqCodeId = Convert.ToInt32(RequisitionDropDownList.SelectedValue);

        ////aJobCreationDao.DepartmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
        ////aJobCreationDao.SectionID = Convert.ToInt32(sectionDropDownList.SelectedValue);
      
        //aJobCreationDao.JobContext = jobContextTextBox.Text.Trim();
        //aJobCreationDao.Position = lblJobTitle.Text.Trim();

     

      
        //aJobCreationDao.CompensationandOtherBenefits = otherBenefitTextBox.Text.Trim();

       

        //aJobCreationDao.IsSalary = IsSalary.Checked;

        
        //aJobCreationDao.CirculationStartDate = Convert.ToDateTime(circulationStartDateTextBox.Text.Trim());
        //aJobCreationDao.CirculationsdeadlineDate = Convert.ToDateTime(circulationEndDateTextBox.Text.Trim());
        //aJobCreationDao.ProbableInterviewDate = Convert.ToDateTime(probableInterviewDateTextBox.Text.Trim());
     
        //aJobCreationDao.Remarks = remarksTextBox.Text.Trim();

        //aJobCreationDao.Updateby = Session["LoginName"].ToString();
        //aJobCreationDao.UpdateDate = DateTime.Now;


        JobCreationDao aJobCreationDao = new JobCreationDao();


        aJobCreationDao.JobID = Convert.ToInt32(JobCreationIdHiddenField.Value);
        aJobCreationDao.JobCode = JobCodetextBox.Text;
        aJobCreationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aJobCreationDao.ReqCodeId = Convert.ToInt32(RequisitionDropDownList.SelectedValue);

        //aJobCreationDao.DepartmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
        //aJobCreationDao.SectionID = Convert.ToInt32(sectionDropDownList.SelectedValue);

        aJobCreationDao.JobContext = jobContextTextBox.Text.Trim();

        aJobCreationDao.CompensationandOtherBenefits = otherBenefitTextBox.Text.Trim();

        aJobCreationDao.IsSalary = IsSalary.Checked;

        aJobCreationDao.CirculationStartDate = Convert.ToDateTime(circulationStartDateTextBox.Text.Trim());
        aJobCreationDao.CirculationsdeadlineDate = Convert.ToDateTime(circulationEndDateTextBox.Text.Trim());
        try
        {
            aJobCreationDao.ProbableInterviewDate =// Convert.ToDateTime(probableInterviewDateTextBox.Text.Trim());

       string.IsNullOrEmpty(probableInterviewDateTextBox.Text) ? (DateTime?)null : DateTime.Parse(probableInterviewDateTextBox.Text).Date;

        }
        catch (Exception)
        {


        }

        aJobCreationDao.Remarks = remarksTextBox.Text.Trim();
        aJobCreationDao.Position = lblJobTitle.Text.Trim();

      
        aJobCreationDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aJobCreationDao.UpdateDate = DateTime.Now;
        aJobCreationDao.ActionStatus = Status;


       bool  status= aJobCreationDal.UpdateJobCreationInformation(aJobCreationDao);
           if (status == true)
            {


            //    for (int i = 0; i < jobLocationGridView.Rows.Count; i++)
            //    {
            //        aJobCreationDal.DelJobReqKeyRespon(JobCreationIdHiddenField.Value);
            //    }
        

            //for (int i = 0; i < jobLocationGridView.Rows.Count; i++)
            //{
            //    KeyJobLocationCirculationDao aJobReqKeyRespon = new KeyJobLocationCirculationDao()
            //    {
            //        JobCreationId = Convert.ToInt32(JobCreationIdHiddenField.Value),
            //        JobLocationId = Convert.ToInt32(jobLocationGridView.DataKeys[i][0].ToString().Trim())

            //    };
            //    aJobCreationDal.SaveKeyJobLocationCirculation(aJobReqKeyRespon);
            //}


                aJobCreationDal.DeleteCirculationWayDetail(JobCreationIdHiddenField.Value);

                JCCirculationWayDao aDao;

                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    aDao = new JCCirculationWayDao();

                   
                        if (CheckBoxList1.Items[i].Selected)
                        {
                            aDao.MasterId = Convert.ToInt32(JobCreationIdHiddenField.Value);
                            aDao.WayId = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                            aJobCreationDal.SaveCirculationWayDetail(aDao);
                        }
                   
                }

          
            //ShowMessageBox("Data Updated Successfully !!!");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Data Updated Successfull...');window.location ='JobCreationView.aspx';",
                     true);
            Clear();

        
        }

    }

    private JobCreationDao PrepareDataForUpdate()
    {
        JobCreationDao aJobCreationDao = new JobCreationDao();

        aJobCreationDao.JobID = Convert.ToInt64(jobIdHiddenField.Value);
        //aJobCreationDao.JobCode = "JOB-01";
        aJobCreationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        //aJobCreationDao.DepartmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
        //aJobCreationDao.SectionID = Convert.ToInt32(sectionDropDownList.SelectedValue);
        aJobCreationDao.Position = positionTextBox.Text.Trim();
        aJobCreationDao.Vacancy = vacancyTextBox.Text.Trim();
        aJobCreationDao.JobContext = jobContextTextBox.Text.Trim();
        aJobCreationDao.JobResponsibilities = jobResponsibilityTextBox.Text.Trim();


        foreach (ListItem item in employmentTypeCheckBoxList.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "parmanent")
                {
                    aJobCreationDao.PermanenteEmp = true;
                }

                if (item.Value == "contractual")
                {
                    aJobCreationDao.ContractualEmp = true;
                }

                if (item.Value == "trainee")
                {
                    aJobCreationDao.TraineeEmp = true;
                }

                if (item.Value == "casual")
                {
                    aJobCreationDao.CasualEmp = true;
                }
            }
        }

        aJobCreationDao.ExperienceRequirements = experienceRequirementsTextBox.Text.Trim();
        aJobCreationDao.AdditionalRequirements = additionalRequirementsTextBox.Text.Trim();
        aJobCreationDao.CompensationandOtherBenefits = otherBenefitTextBox.Text.Trim();

        foreach (ListItem item in declarationSourceCheckBoxList.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "newspaper")
                {
                    aJobCreationDao.NewspaperDS = true;
                }

                if (item.Value == "tv")
                {
                    aJobCreationDao.TVDS = true;
                }

                if (item.Value == "other")
                {
                    aJobCreationDao.OtherDS = true;
                }
            }
        }

        aJobCreationDao.Salary = salaryTextBox.Text.Trim();
        aJobCreationDao.Other = otherdeclarationSourceTextBox.Text.Trim();
        aJobCreationDao.CirculationStartDate = Convert.ToDateTime(circulationStartDateTextBox.Text.Trim());
        aJobCreationDao.CirculationsdeadlineDate = Convert.ToDateTime(circulationEndDateTextBox.Text.Trim());
        aJobCreationDao.ProbableInterviewDate = Convert.ToDateTime(probableInterviewDateTextBox.Text.Trim());
        aJobCreationDao.ProbableIRecruitmentDate = Convert.ToDateTime(probableRecruitmentDateTextBox.Text.Trim());
        aJobCreationDao.Remarks = remarksTextBox.Text.Trim();

        aJobCreationDao.Status = "Posted";
        aJobCreationDao.Progress = "Posted";
        aJobCreationDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aJobCreationDao.UpdateDate = DateTime.Now;

        return aJobCreationDao;
    }

    //private Int64 SaveInformation()
    //{
    //    //Int64 retVal;
    //    //try
    //    //{
    //    //    retVal = aJobCreationBll.SaveDataBll(PrepareDataForSave());

    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    retVal = 0;
    //    //    throw ex;
    //    //}

    //    return retVal;
    //}

    private Int64 SaveDetailInformation(Int64 val)
    {
        Int64 retVal;
        try
        {
            retVal = aJobCreationBll.SaveEducationalReqDetail(PrepareEducationalReqDataForSaveDetail(val));
            retVal = aJobCreationBll.SaveJobLocationDetail(PrepareJobLocationDataForSaveDetail(val));
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }

    private List<JobCreationLocationDao> PrepareJobLocationDataForSaveDetail(long val)
    {
        List<JobCreationLocationDao> aLocationDaos = new List<JobCreationLocationDao>();
        JobCreationLocationDao aLocationDao;

        if (educationalRequirementsGridView.Rows.Count > 0)
        {
            aLocationDao = new JobCreationLocationDao();

            for (int i = 0; i < educationalRequirementsGridView.Rows.Count; i++)
            {
                aLocationDao = new JobCreationLocationDao();
                aLocationDao.JobID = val;
                aLocationDao.JobLocationID = 2;

                aLocationDaos.Add(aLocationDao);
            }
        }

        return aLocationDaos;
    }

    private List<JobCreationEdReqDao> PrepareEducationalReqDataForSaveDetail(long val)
    {
        List<JobCreationEdReqDao> aCreationEdReqDaos = new List<JobCreationEdReqDao>();
        JobCreationEdReqDao aCreationEdReqDao;

        if (educationalRequirementsGridView.Rows.Count > 0)
        {
            aCreationEdReqDao = new JobCreationEdReqDao();

            for (int i = 0; i < educationalRequirementsGridView.Rows.Count; i++)
            {
                aCreationEdReqDao = new JobCreationEdReqDao();
                aCreationEdReqDao.JobID = val;
                aCreationEdReqDao.ERID = 2;

                aCreationEdReqDaos.Add(aCreationEdReqDao);
            }
        }

        return aCreationEdReqDaos;
    }

    public void MethodAutoIncri()
    {

     //   companyDropDownList_SelectedIndexChanged(null, null);
         string code = aJobCreationDal.GenerateAutoNumber("tblJobCreation", "JobCode", DateTime.Now);
        int Code2 = Convert.ToInt32(code);
         if (companyDropDownList.SelectedValue != "")
         {
             if (companyDropDownList.SelectedValue == 1.ToString(CultureInfo.InvariantCulture))
             {
                 Code2 = Code2 ;
             }
             else if (companyDropDownList.SelectedValue == 2.ToString(CultureInfo.InvariantCulture))
             {
                 Code2 = Code2 +20001  ;
             }

             JobCodetextBox.Text = Code2.ToString();
         }
        
        
    }

//    public string GenerateAutoNumber(string table, string column, DateTime date)
//    {
//        try
//        {
//            string query = @"SELECT (((SUBSTRING((CONVERT(NVARCHAR(5),YEAR(GETDATE()))),3,2)+(CASE WHEN LEN(MONTH(GETDATE()))=1 
//		THEN  '0'+CONVERT(NVARCHAR(5),MONTH(GETDATE())) ELSE CONVERT(NVARCHAR(5),MONTH(GETDATE())) END)+
//		(CASE WHEN LEN(DAY(GETDATE()))=1 THEN  '0'+CONVERT(NVARCHAR(5),DAY(GETDATE())) ELSE CONVERT(NVARCHAR(5),DAY(GETDATE())) END)))+
//		CONVERT(NVARCHAR(5),(ISNULL((MAX(CONVERT(INT,(SUBSTRING( " + column + " ,10,4))))),1000)+1))) as AutoNumber FROM  " + table + "  WHERE  CONVERT(NVARCHAR(11),EntryDate,106)= CONVERT(NVARCHAR(11),'" + (System.DateTime.Now).ToString("dd-MMM-yyyy").Replace("-", " ") + "',106)";
//            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//            Decimal asd = Convert.ToDecimal(dt.Rows[0][0].ToString()); ;
//            Decimal asd1 = asd + 1;

//            return dt.Rows[0][0].ToString();

//        }
//        catch (Exception ex)
//        {

//            throw ex;
//        }
//    }


    private string ReqnNoGenerator(int id)
    {
        string code = string.Empty;
        string Id = id.ToString();

        if (Id.Length == 1)
        {
            Id = "000000" + Id;
        }
        if (Id.Length == 2)
        {
            Id = "00000" + Id;
        }
        if (Id.Length == 3)
        {
            Id = "0000" + Id;
        }
        if (Id.Length == 4)
        {
            Id = "000" + Id;
        }
        if (Id.Length == 5)
        {
            Id = "00" + Id;
        }
        if (Id.Length == 6)
        {
            Id = "0" + Id;
        }

        if (companyDropDownList.SelectedValue != "")
        {
            if (companyDropDownList.SelectedValue == 1.ToString(CultureInfo.InvariantCulture))
            {
                code = "SMC/CIR/" + Id;
            }
            else if (companyDropDownList.SelectedValue == 2.ToString(CultureInfo.InvariantCulture))
            {
                code = "SMC EL/CIR/" + Id;
            }
        }
       
        return code;
    }

    public void Save( string Status)
    {
        if (Validation())
        {
            JobCreationDao aJobCreationDao = new JobCreationDao();

            aJobCreationDao.JobCode = JobCodetextBox.Text;
            aJobCreationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            aJobCreationDao.ReqCodeId = Convert.ToInt32(RequisitionDropDownList.SelectedValue);

            //aJobCreationDao.DepartmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
            //aJobCreationDao.SectionID = Convert.ToInt32(sectionDropDownList.SelectedValue);

            aJobCreationDao.JobContext = jobContextTextBox.Text.Trim();

            aJobCreationDao.CompensationandOtherBenefits = otherBenefitTextBox.Text.Trim();

            aJobCreationDao.IsSalary = IsSalary.Checked;

            aJobCreationDao.CirculationStartDate = Convert.ToDateTime(circulationStartDateTextBox.Text.Trim());
            aJobCreationDao.CirculationsdeadlineDate = Convert.ToDateTime(circulationEndDateTextBox.Text.Trim());
            try
            {
                aJobCreationDao.ProbableInterviewDate =// Convert.ToDateTime(probableInterviewDateTextBox.Text.Trim());

           string.IsNullOrEmpty(probableInterviewDateTextBox.Text) ? (DateTime?)null : DateTime.Parse(probableInterviewDateTextBox.Text).Date;
                                
            }
            catch (Exception)
            {
                
                
            }

            aJobCreationDao.Remarks = remarksTextBox.Text.Trim();
            aJobCreationDao.Position = lblJobTitle.Text.Trim();

            aJobCreationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
            aJobCreationDao.EntryDate = DateTime.Now;
            aJobCreationDao.ActionStatus = Status;

            int id = aJobCreationDal.SaveJobCreationInfo(aJobCreationDao);
           
            if (id > 0)
            {

                JCCirculationWayDao aDao;

                //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    aDao = new JCCirculationWayDao();
                    for (int j = 0; j < CheckBoxList1.Items.Count; j++)
                    {
                        if (CheckBoxList1.Items[j].Selected == true)
                        {
                            aDao.MasterId = id;
                            aDao.WayId = Convert.ToInt32(CheckBoxList1.Items[j].Value);
                            aJobCreationDal.SaveCirculationWayDetail(aDao);
                        }
                    }
                }
                //for (int i = 0; i < jobLocationGridView.Rows.Count; i++)
                //{
                //    KeyJobLocationCirculationDao aJobReqKeyRespon = new KeyJobLocationCirculationDao()
                //    {
                //        JobCreationId = id,
                //        JobLocationId = Convert.ToInt32(jobLocationGridView.DataKeys[i][0].ToString().Trim())

                //    };
                //    aJobCreationDal.SaveKeyJobLocationCirculation(aJobReqKeyRespon);
                //}

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation successfully done...');window.location ='JobCreationView.aspx';",
                    true);
                Clear();
                MethodAutoIncri();

            }
            


         

        }
    }

    private bool Validation()
    {
        if (companyDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Choose Company Name");
            companyDropDownList.Focus();
            return false;
        }

        if (RequisitionDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Choose Requisition");
            RequisitionDropDownList.Focus();
            return false;
        }

        //if (jobContextTextBox.Text == string.Empty)
        //{
        //    ShowMessageBox("Give job Context");
        //   jobContextTextBox.Focus();
        //    return false;
        //}

        //if (jobLocationGridView.Rows.Count == 0)
        //{
        //    ShowMessageBox("Please Add Job Location!!");
        //    KeyjobLocationDropDownList.Focus();
        //    return false;
        //}

        //if (otherBenefitTextBox.Text == string.Empty)
        //{
        //    ShowMessageBox("Give other Benefit");
        //    otherBenefitTextBox.Focus();
        //    return false;
        //}

        if (circulationStartDateTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give circulation Start Date");
            circulationStartDateTextBox.Focus();
            return false;
        }

        if (circulationEndDateTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give circulation End Date");
            circulationEndDateTextBox.Focus();
            return false;
        }

        //if (probableInterviewDateTextBox.Text == string.Empty)
        //{
        //    ShowMessageBox("Give probable Interview Date");
        //    probableInterviewDateTextBox.Focus();
        //    return false;
        //}
        int i = 0;
        for ( i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if (CheckBoxList1.Items[i].Selected)
            {
                break;
            }   
        }
        if (i==CheckBoxList1.Items.Count)
        {
            ShowMessageBox("Select Circulation Way");
            
            return false;
        }
        return true;
    }

    protected void eduReqButton_OnClick(object sender, EventArgs e)
    {
        if (educationalRequirementsTextBox.Text != "")
        {
            string educationalRequirements = educationalRequirementsTextBox.Text;
            
            DataTable aDataTable = new DataTable();

            aDataTable.Columns.Add("EducationRequirements");

            DataRow dataRow;

            if (educationalRequirementsGridView.Rows.Count > 0)
            {
                for (int i = 0; i < educationalRequirementsGridView.Rows.Count; i++)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["EducationRequirements"] = educationalRequirementsGridView.Rows[i].Cells[0].Text;

                    aDataTable.Rows.Add(dataRow);
                }

                dataRow = aDataTable.NewRow();
                dataRow["EducationRequirements"] = educationalRequirements;

                aDataTable.Rows.Add(dataRow);
                educationalRequirementsGridView.DataSource = aDataTable;
                educationalRequirementsGridView.DataBind();
            }
            else
            {
                dataRow = aDataTable.NewRow();
                dataRow["EducationRequirements"] = educationalRequirements;

                aDataTable.Rows.Add(dataRow);
                educationalRequirementsGridView.DataSource = aDataTable;
                educationalRequirementsGridView.DataBind();
            }
        }
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EducationRequirements");

        DataRow dataRow;

        if (educationalRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < educationalRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["EducationRequirements"] = educationalRequirementsGridView.Rows[i].Cells[0].Text;

                    aDataTable.Rows.Add(dataRow);

                }
            }
        }

        educationalRequirementsGridView.DataSource = aDataTable;
        educationalRequirementsGridView.DataBind();
    }

 
    protected void locationDeleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobLocCirId");
        aDataTable.Columns.Add("LocationMame");

        DataRow dataRow;

        if (jobLocationGridView.Rows.Count > 0)
        {
            for (int i = 0; i < jobLocationGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["JobLocCirId"] = jobLocationGridView.DataKeys[i][0].ToString();
                    dataRow["LocationMame"] = jobLocationGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        jobLocationGridView.DataSource = aDataTable;
        jobLocationGridView.DataBind();
    }

    private void Clear()
    {
        jobIdHiddenField.Value = "";
        companyDropDownList.SelectedValue = "";
        RequisitionDropDownList.SelectedValue = "";
        lblAge.Text = "";
        lblDepartment.Text = "";
        lblEmpType.Text = "";
        lblExpDtJoin.Text = "";
        lblExperience.Text = "";
        lblGradeTitile.Text = "";
        lblJobTitle.Text = "";
        lblOthers.Text = "";
        lblReportTo.Text = "";
        lblSkill.Text = "";
        lblTotalVacency.Text = "";
        lbldivWing.Text = "";
        lbldivision.Text = "";
        lblplaceOfPosting.Text = "";


        otherBenefitTextBox.Text = "";
        IsSalary.Checked = false;
        circulationStartDateTextBox.Text = "";
        circulationEndDateTextBox.Text = "";
        probableInterviewDateTextBox.Text = "";
        remarksTextBox.Text = "";

        jobContextTextBox.Text = "";

        jobLocationGridView.DataSource = null;
        jobLocationGridView.DataBind();

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }

    }

    protected void addDegreeButton_OnClick(object sender, EventArgs e)
    {
        if (degree.Text != "")
        {
            if (aJobCreationBll.SaveDegreeInfo(degree.Text) > 0)
            {
                degree.Text = "";
            }
        }
    }

 
    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aJobCreationDal.GetRequisitionCodeList(RequisitionDropDownList, companyDropDownList.SelectedValue);
            MethodAutoIncri();
        }

        else
        {
            RequisitionDropDownList.Items.Clear();
            //Clear();

        }

    }
    protected void RequisitionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (RequisitionDropDownList.SelectedValue != "")
            {

                DataTable aDataTable = aJobCreationDal.GetJobReqInformationById(RequisitionDropDownList.SelectedValue);



        const int rowIndex = 0;

               // if (aDataTable.Rows.Count ==0)
                {
                    


                    LoadData(Convert.ToInt32(RequisitionDropDownList.SelectedValue));

                    DataTable aTable = aJobCreationDal.GetPreferedWayOfCircular(RequisitionDropDownList.SelectedValue);

                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        CheckBoxList1.Items[i].Selected = false;
                    }

                    if (aTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                        {
                            for (int j = 0; j < aTable.Rows.Count; j++)
                            {
                                if (CheckBoxList1.Items[i].Value == aTable.Rows[j].Field<Int32>("WayId").ToString(CultureInfo.InvariantCulture))
                                {
                                    CheckBoxList1.Items[i].Selected = true;
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    RequisitionDropDownList.SelectedValue = "";
                //    aShowMessage.ShowMessageBox("This Requisition Already Circulated!!!", this);
                //}

               
            }

            else
            {
                lblGradeTitile.Text = string.Empty;
                lblJobTitle.Text = string.Empty;
                lblTotalVacency.Text = string.Empty;
                lblEmpType.Text = string.Empty;
                //lbldivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
                //lbldivWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
                lblDepartment.Text = string.Empty;
                lblplaceOfPosting.Text = string.Empty;
                lblReportTo.Text = string.Empty;
                lblExpDtJoin.Text = string.Empty;



                lblExperience.Text = string.Empty;
                lblSkill.Text = string.Empty;
                lblAge.Text = string.Empty;
                lblOthers.Text = string.Empty;
            }
        }
        catch (Exception)
        {
           
        }
       
    }


    public void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aJobCreationDal.LoadEmpJobRequisitionById(id);
        if (dtdata.Rows.Count > 0)
        {

          
            lblGradeTitile.Text = dtdata.Rows[0]["JobTitle"].ToString();
            lblJobTitle.Text = dtdata.Rows[0]["JobTitle"].ToString();
            lblTotalVacency.Text = dtdata.Rows[0]["Nos"].ToString();
            lblEmpType.Text = dtdata.Rows[0]["EmpType"].ToString();
            //lbldivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
            //lbldivWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblplaceOfPosting.Text = dtdata.Rows[0]["PlaceOfPosting"].ToString();
            lblReportTo.Text = dtdata.Rows[0]["ReportingTo"].ToString();

            try
            {
                lblExpDtJoin.Text = Convert.ToDateTime(dtdata.Rows[0]["ExpDateOfJoining"]).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {

                lblExpDtJoin.Text = "";
            }



            lblExperience.Text = dtdata.Rows[0]["Experience"].ToString();
            lblSkill.Text = dtdata.Rows[0]["Skills"].ToString();
            lblAge.Text = dtdata.Rows[0]["Age"].ToString();
            lblOthers.Text = dtdata.Rows[0]["OtherExperience"].ToString();
          


            
        }
    }

    protected void divisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void divWingDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void addImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        //var LocName = txtJobLocation.Text.Trim();
    }

    private bool AddEventValidation()
    {

        if (KeyjobLocationDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select job Location !!!");
            return false;
        }

        return true;
    }
    protected void jobLocationButton_OnClick(object sender, ImageClickEventArgs e)
    {
        if (AddEventValidation())
        {
            string jobLocCirId = KeyjobLocationDropDownList.SelectedValue;
            string locationMame = KeyjobLocationDropDownList.SelectedItem.Text.Trim();

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("JobLocationID");
            aDataTable.Columns.Add("Location");



            DataRow dataRow;

            if (jobLocationGridView.Rows.Count > 0)
            {
                for (int i = 0; i < jobLocationGridView.Rows.Count; i++)
                {
                    if (jobLocationGridView.Rows[i].Cells[0].Text != locationMame)
                    {
                        dataRow = aDataTable.NewRow();

                        dataRow["JobLocationID"] = jobLocationGridView.DataKeys[i][0].ToString();
                        dataRow["Location"] = jobLocationGridView.Rows[i].Cells[0].Text;

                        aDataTable.Rows.Add(dataRow);
                        KeyjobLocationDropDownList.SelectedValue = "";
                    }

                    else
                    {
                        KeyjobLocationDropDownList.SelectedValue = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();

                dataRow["JobLocationID"] = jobLocCirId;
                dataRow["Location"] = locationMame;


                aDataTable.Rows.Add(dataRow);

                jobLocationGridView.DataSource = aDataTable;
                jobLocationGridView.DataBind();

                KeyjobLocationDropDownList.SelectedValue = "";

            }


            else
            {
                dataRow = aDataTable.NewRow();

                dataRow["JobLocationID"] = jobLocCirId;
                dataRow["Location"] = locationMame;

                aDataTable.Rows.Add(dataRow);

                jobLocationGridView.DataSource = aDataTable;
                jobLocationGridView.DataBind();

                KeyjobLocationDropDownList.SelectedValue = "";
            }
        }
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobCreationView.aspx");
    }

    protected void probableInterviewDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (probableInterviewDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(probableInterviewDateTextBox.Text);
            }
            catch
            {
                probableInterviewDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void circulationEndDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (circulationEndDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(circulationEndDateTextBox.Text);
            }
            catch
            {
                circulationEndDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void circulationStartDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (circulationStartDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(circulationStartDateTextBox.Text);
            }
            catch
            {
                circulationStartDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (JobCreationIdHiddenField.Value != string.Empty)
        {
            string ApprovalStatus = "Drafted";
            Update(ApprovalStatus);
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {


        if (JobCreationIdHiddenField.Value != string.Empty)
        {
            Delete();
        }
      // / bool masterStatus = aJobCreationDal.DeleteJobCreationById(JobCreationIdHiddenField.Value);
      //  bool detailStatus = aJobCreationDal.DeleteJobCreationDetailById(JobCreationIdHiddenField.Value);

        //if (masterStatus && detailStatus)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(),
        //            "alert",
        //            "alert('Data Save Successfull...');window.location ='JobCreationView.aspx';",
        //            true);
        //    //LoadJobCreationInfo();
        //}
    }

    private void Delete()
    {
        JobCreationDao aJobCreationDao = new JobCreationDao();


        aJobCreationDao.JobID = Convert.ToInt32(JobCreationIdHiddenField.Value);

        aJobCreationDao.IsDelete = true;


        aJobCreationDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aJobCreationDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aJobCreationDal.DeleteJobCreationById(aJobCreationDao);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='JobCreationView.aspx';",
              true);
        }
    }

    protected void Button2_OnClick(object sender, EventArgs e)
    {
        if (JobCreationIdHiddenField.Value == string.Empty)
        {
            string ApprovalStatus = "Drafted";
            Save(ApprovalStatus);
        }
    }

    protected void btnUpdateforSubmit_OnClick(object sender, EventArgs e)
    {
        if (JobCreationIdHiddenField.Value != string.Empty)
        {
            string ApprovalStatus = "Submitted";
            Update(ApprovalStatus);
        }
    }
}