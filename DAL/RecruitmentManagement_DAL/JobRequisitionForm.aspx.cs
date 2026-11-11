using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using BLL.RecruitmentManagement_BLL;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class MasterSetup_UI_JobRequisitionForm : System.Web.UI.Page
{
    EmployeeRequsitionDAL aEmployeeRequsitionDal=new EmployeeRequsitionDAL();
    SubSectionInformationDal asSectionInformationDal = new SubSectionInformationDal();
   JobReqFormBll aJobReqFormBll = new JobReqFormBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            MethodAutoIncri();
            RadioListLoad();
            GetValue();
            HideDivs();
            SetSearchBoxStatus();
            LoadCircularSource();

            //BudgetCodeDropDownList.Visible = isBudgetedCheckBox.Checked;
            //LblOther.Visible = isBudgetedCheckBox.Checked;

            DropDownList();
            LoadEduGrid();
            LoadKeyResponse();
            if (Session["JobReqId"] !=null)
            {
                empIdHiddenField.Value = Session["JobReqId"].ToString();
                LoadData(Session["JobReqId"].ToString());
                Session["JobReqId"] = null;
            }

            else
            {
                MethodAutoIncri();
            }
         
        }
    }

    private void LoadCircularSource()
    {
        DataTable aTable = aEmployeeRequsitionDal.CurculationWayList();

        CheckBoxList1.DataValueField = "VacancyCirculationId";
        CheckBoxList1.DataTextField = "CirculationWay";
        CheckBoxList1.DataSource = aTable;
        CheckBoxList1.DataBind();
    }

    private void SetSearchBoxStatus()
    {
        codeLabel.Text = "";
        nameLabel.Text = "";
        desigLabel.Text = "";
        salgradLabel.Text = "";
        divLabel.Text = "";
        DateOfSeperationTextBox.Text = "";

        if (companyDropDownList.SelectedValue != "")
        {
            EmployeeNameTextBox.Enabled = true;
            EmployeeNameTextBox.ToolTip = "";
        }
        else
        {
            EmployeeNameTextBox.Enabled = false;
            EmployeeNameTextBox.ToolTip = "Select company for enabling textbox !!";
        }
    }

    private void HideDivs()
    {
        lblEmpName.Visible = false;
        lblDatSep.Visible = false;
        detail.Visible = false;

        //DateOfSeperationTextBox.Visible = false;
        //EmployeeNameTextBox.Visible = false;

        //DateOfSeperationTextBox.Text = String.Empty;
        EmployeeNameTextBox.Text = "";
        //jobDesc.Visible = false;
        descriptionTextBox.Text = "";
    }

    private void RadioListLoad()
    {
        string constr = ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRDB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"SELECT EmpTypeId, EmpType FROM tblEmployeeType";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                typeOfPosRadioButtonList.DataSource = cmd.ExecuteReader();
                typeOfPosRadioButtonList.DataTextField = "EmpType";
                typeOfPosRadioButtonList.DataValueField = "EmpTypeId";
                typeOfPosRadioButtonList.DataBind();
                con.Close();
            }
        }
    }

    private void GetValue()
    {
        //BudgetCodeDropDownList.Visible = isBudgetedCheckBox.Checked;
        //lblCodeBudget.Visible = isBudgetedCheckBox.Checked;


        //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
        //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;
        //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
        //EmployeeDropDownList.Visible = IsReplacementforCheckBox.Checked;
        //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;

        //lblDatSep.Visible = IsReplacementforCheckBox.Checked;
        //lblEmpName.Visible = IsReplacementforCheckBox.Checked;


       
        //ShowReplacementfor.Visible = false;

        otherTextBox.Visible = OtherCheckBox.Checked;
        LblOther.Visible = OtherCheckBox.Checked;
    }

    public void LoadData(string id)
    {
        DataTable dtdata=new DataTable();
        dtdata = aEmployeeRequsitionDal.LoadEmpJobRequisitionById(id);
        if (dtdata.Rows.Count>0)
        {

            empIdHiddenField.Value = dtdata.Rows[0].Field<Int32>("JobReqId").ToString();
            companyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            aEmployeeRequsitionDal.LoadFinancialYear(mainFinyearDropDownList,companyDropDownList.SelectedValue);
            mainFinyearDropDownList.SelectedValue = dtdata.Rows[0]["FinYearId"].ToString();
            //aEmployeeRequsitionDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
            //EmployeeDropDownList.SelectedValue = dtdata.Rows[0]["EmployeeId"].ToString();

            //if (typeOfPosRadioButtonList.Items[0].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            //}

            //if (typeOfPosRadioButtonList.Items[1].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            //}

            //if (typeOfPosRadioButtonList.Items[2].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[2].Value;
            //}
            reqDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["ReqDate"].ToString()).ToString("dd-MMM-yyyy");
            mainFinyearDropDownList.SelectedValue = dtdata.Rows[0]["FinYearId"].ToString();
            if ( dtdata.Rows[0]["EmpTypeId"] !=1.ToString())
            {
                typeOfPosRadioButtonList.Items[0].Selected=true;
            }
            if (typeOfPosRadioButtonList.Items[1].Value == dtdata.Rows[0]["EmpTypeId"])
            {
                typeOfPosRadioButtonList.Items[1].Selected = true;
            }

            if (typeOfPosRadioButtonList.Items[2].Value ==dtdata.Rows[0]["EmpTypeId"])
            {
                typeOfPosRadioButtonList.Items[2].Selected = true;
            }

            //if (typeOfPosRadioButtonList.Items[3].Value == dtdata.Rows[0]["EmpTypeId"])
            //{
            //    typeOfPosRadioButtonList.Items[3].Selected = true;
            //}

            jobtitleDropDownList.SelectedValue = dtdata.Rows[0]["JobTitleId"].ToString();
            gradeDropDownList.SelectedValue = dtdata.Rows[0]["GradeId"].ToString();
            nosTextBox.Text = dtdata.Rows[0]["Nos"].ToString();
            ReqCodetextBox.Text = dtdata.Rows[0]["ReqCode"].ToString();

            NoteTextBox.Text = dtdata.Rows[0]["Note"].ToString();
            otherTextBox.Text = dtdata.Rows[0]["Others"].ToString();
            RemarksTextBox.Text = dtdata.Rows[0]["Remarks"].ToString();
            placeOfPostingTextBox.Text = dtdata.Rows[0]["PlaceOfPosting"].ToString();

            //isBudgetedCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["ReportingTo"].ToString());

            reportToTextBox.Text = dtdata.Rows[0]["ReportingTo"].ToString();
            divisionDropDownList.SelectedValue = dtdata.Rows[0]["DivisionId"].ToString();
            aEmployeeRequsitionDal.LoadWingsByDivision(divWingDropDownList,divisionDropDownList.SelectedValue);
            divWingDropDownList.SelectedValue = dtdata.Rows[0]["WingId"].ToString();
            aEmployeeRequsitionDal.LoadDepartmentByWings(deptDropDownList,companyDropDownList.SelectedValue);
         
            deptDropDownList.SelectedValue = dtdata.Rows[0]["DeptId"].ToString();
            expDtJoinTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["ExpDateOfJoining"]).ToString("dd-MMM-yyyy");
           
            DataTable dtedu = aEmployeeRequsitionDal.LoadReqEducationById(id);
            //if (dtedu.Rows.Count>0)
            //{
                educationGridView.DataSource = dtedu;
                educationGridView.DataBind();
            //}
            DataTable dtkeyres = aEmployeeRequsitionDal.LoadKeyResponseById(id);
            if (dtkeyres.Rows.Count>0)
            {
                KeyResponGridView.DataSource = dtkeyres;
                KeyResponGridView.DataBind();
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsInternalCir"]) == true)
            {
                
                CheckBoxList1.Items[0].Selected = true;
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsInternalCir"]) == true)
            {
              
                CheckBoxList1.Items[1].Selected = true;
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsOnlineCir"]) == true)
            {
               
                CheckBoxList1.Items[2].Selected = true;
            }


            if (Convert.ToBoolean(dtdata.Rows[0]["IsSMCWeb"]) == true)
            {
               
                CheckBoxList1.Items[3].Selected = true;
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsHeadHuntFirm"]) == true)
            {
               
                CheckBoxList1.Items[4].Selected = true;
            }

            


       
            otherTextBox.Text = dtdata.Rows[0]["OtherCircula"].ToString();

            isBudgetedCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsBudgeted"].ToString());

            if (isBudgetedCheckBox.Checked)
            {
                ShowBudgetCodeDiv.Visible = true;
                aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList, companyDropDownList.SelectedValue);
                financialYearDropDownList.SelectedValue = dtdata.Rows[0]["FinancialYearId"].ToString();
            }
            else
            {
                financialYearDropDownList.Items.Clear();
            }

            if (financialYearDropDownList.SelectedValue != "")
            {
                //aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);
                aEmployeeRequsitionDal.LoadCodeBudgetYearWise(BudgetCodeDropDownList, financialYearDropDownList.SelectedValue, companyDropDownList.SelectedValue);
                BudgetCodeDropDownList.SelectedValue = dtdata.Rows[0]["BudgetId"].ToString();
            }
            else
            {
                BudgetCodeDropDownList.Items.Clear();
            }

            //if (isBudgetedCheckBox.Checked == true)
            //{
            //    ShowBudgetCodeDiv.Visible = true;
            //    BudgetCodeDropDownList.SelectedValue = dtdata.Rows[0]["BudgetId"].ToString();

            //}

            //IsReplacementforCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsReplacement"].ToString());
            
            //if (IsReplacementforCheckBox.Checked == true)
            //{
                lblDatSep.Visible = true;
                lblEmpName.Visible = true;
                DateOfSeperationTextBox.Visible = true;
                DateOfSeperationTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["SeparationDate"]).ToString("dd-MMM-yyyy");
                //DateOfSeperationTextBox.Text = dtdata.Rows[0]["SeparationDate"].ToString(); 
                EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
                repEmpIdHiddenField.Value = dtdata.Rows[0]["ReplaceEmpId"].ToString();
                EmployeeNameTextBox.Visible = true;

            //}

            OtherCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsOtherCircula"].ToString());
            if (OtherCheckBox.Checked == true)
            {
                otherTextBox.Visible = true;
                otherTextBox.Text = dtdata.Rows[0]["OtherCircula"].ToString();

            }
         
            experienceTextBox.Text = dtdata.Rows[0]["Experience"].ToString();
            skillTextBox.Text = dtdata.Rows[0]["Skills"].ToString();
            ageTextBox.Text = dtdata.Rows[0]["Age"].ToString();
            othersTextBox.Text = dtdata.Rows[0]["OtherExperience"].ToString();


            DataTable keyresPon = aJobReqFormBll.JobCreationKeyRespon(Convert.ToInt32(id));

            if (KeyResponGridView.Rows.Count > 0)
            {
                KeyResponGridView.DataSource = keyresPon;
                
                KeyResponGridView.DataBind();
            }



                DataTable educationalRequirements = aJobReqFormBll.JobEduReq(Convert.ToInt32(id));

                //if (EducationRequirementGridView.Rows.Count > 0)
                //{
                EducationRequirementGridView.DataSource = educationalRequirements;
                EducationRequirementGridView.DataBind();
                //}
                Button2.Text = "Update";
       
        }
    }
    public void DropDownList()

    {

        asSectionInformationDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        aEmployeeRequsitionDal.GetSalaryLocationOnOfficeDdl(officeDropDownList);

        //aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);

        //aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList);

        //aEmployeeRequsitionDal.KeyResponsibilites(KeyResponsibilitesDropDownList.Text);
        aEmployeeRequsitionDal.EducationRequirementDropDownList(EducationRequirementDropDownList);


        aEmployeeRequsitionDal.LoadDesignation(jobtitleDropDownList);
        aEmployeeRequsitionDal.LoadDivision(divisionDropDownList);
        //aEmployeeRequsitionDal.LoadEmpInfo(empCodeDropDownList);
        aEmployeeRequsitionDal.LoadGrade(gradeDropDownList);
        
    }

    protected void divisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aEmployeeRequsitionDal.LoadWingsByDivision(divWingDropDownList,divisionDropDownList.SelectedValue);
    }

    protected void divWingDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aEmployeeRequsitionDal.LoadDepartmentByWings(deptDropDownList,divWingDropDownList.SelectedValue);
    }

    public void Clear()
    {
        try
        {

        
        companyDropDownList.SelectedIndex = 0;
            codeLabel.Text = string.Empty;
            nameLabel.Text = string.Empty;
            desigLabel.Text = string.Empty;
            salgradLabel.Text = string.Empty;
            divLabel.Text = string.Empty;
            wingLabel.Text = string.Empty;
            deptLabel.Text = string.Empty;
            secLabel.Text = string.Empty;
            subsecLabel.Text = string.Empty;
        DateOfSeperationTextBox.Text =
            string.Empty;
        //divWingDropDownList.SelectedIndex = 0;

        jobtitleDropDownList.SelectedIndex = 0;
        gradeDropDownList.SelectedIndex = 0;
        nosTextBox.Text = string.Empty;
        NoteTextBox.Text = string.Empty;
        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {
            typeOfPosRadioButtonList.Items[i].Selected = false;
        }
        placeOfPostingTextBox.Text = string.Empty;
        reportToTextBox.Text = string.Empty;
     //   divisionDropDownList.SelectedIndex = 0;
        deptDropDownList.SelectedIndex = 0;
        expDtJoinTextBox.Text = string.Empty;
        loadGridView.DataSource = null;
        loadGridView.DataBind();


        KeyResponGridView.DataSource = null;
        KeyResponGridView.DataBind();

        EducationRequirementGridView.DataSource = null;
        EducationRequirementGridView.DataBind();

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }
        otherTextBox.Text = string.Empty;
        //replaceForCheckBox.Checked = false;
        //empCodeDropDownList.SelectedIndex = 0;
        //IsReplacementforCheckBox.Checked = false;
        OtherCheckBox.Checked = false;
        //EmployeeDropDownList.SelectedValue = "";
        EmployeeNameTextBox.Text = "";
        RemarksTextBox.Text = string.Empty;
        //justficationTextBox.Text = string.Empty;
        educationGridView.DataSource = null;
        educationGridView.DataBind();
        experienceTextBox.Text = string.Empty;
        skillTextBox.Text = string.Empty;
        ageTextBox.Text = string.Empty;
        othersTextBox.Text = string.Empty;
        //if (companyDropDownList.SelectedValue != "")
        {
            //lblEmpName.Visible = IsReplacementforCheckBox.Checked;
            //lblDatSep.Visible = IsReplacementforCheckBox.Checked;
            //detail.Visible = IsReplacementforCheckBox.Checked;

            //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeDropDownList.Visible = IsReplacementforCheckBox.Checked;
            //ShowReplacementfor.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;

            DateOfSeperationTextBox.Text = String.Empty;
            //EmployeeDropDownList.SelectedValue = String.Empty;
            EmployeeNameTextBox.Text = "";
        }
        //else
        //{
        //    IsReplacementforCheckBox.Checked = false;
        //    ShowMessageBox("Please select a company first !!!");
        //}
        BudgetCodeDropDownList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            
        }

    }
    public void Clear2()
    {
        try
        {

        
        
        //divWingDropDownList.SelectedIndex = 0;

        jobtitleDropDownList.SelectedIndex = 0;
        gradeDropDownList.SelectedIndex = 0;
        nosTextBox.Text = string.Empty;
        NoteTextBox.Text = string.Empty;
        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {
            typeOfPosRadioButtonList.Items[i].Selected = false;
        }
        placeOfPostingTextBox.Text = string.Empty;
        reportToTextBox.Text = string.Empty;
        //   divisionDropDownList.SelectedIndex = 0;
        deptDropDownList.SelectedIndex = 0;
        expDtJoinTextBox.Text = string.Empty;
        loadGridView.DataSource = null;
        loadGridView.DataBind();


        KeyResponGridView.DataSource = null;
        KeyResponGridView.DataBind();

        EducationRequirementGridView.DataSource = null;
        EducationRequirementGridView.DataBind();

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }
        otherTextBox.Text = string.Empty;
        //replaceForCheckBox.Checked = false;
        //empCodeDropDownList.SelectedIndex = 0;
        //IsReplacementforCheckBox.Checked = false;
        OtherCheckBox.Checked = false;
        //EmployeeDropDownList.SelectedValue = "";
        EmployeeNameTextBox.Text = "";
        RemarksTextBox.Text = string.Empty;
        //justficationTextBox.Text = string.Empty;
        educationGridView.DataSource = null;
        educationGridView.DataBind();
        experienceTextBox.Text = string.Empty;
        skillTextBox.Text = string.Empty;
        ageTextBox.Text = string.Empty;
        othersTextBox.Text = string.Empty;
        BudgetCodeDropDownList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            
        }

    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {

        //if (IsReplacementforCheckBox.Checked == true)
        //{
            if (DateOfSeperationTextBox.Text == string.Empty)
            {
                ShowMessageBox("Give Date Of Seperation");
                DateOfSeperationTextBox.Focus();
                return false;
            }

            //if (EmployeeDropDownList.SelectedValue == "")
            //{
            //    EmployeeDropDownList.Focus();
            //    ShowMessageBox("Give Employee Name");
            //    return false;
            //}
            
            if (EmployeeNameTextBox.Text == "")
            {
                EmployeeNameTextBox.Focus();
                ShowMessageBox("Give Employee Name");
                return false;
            }
        //    return true;
        //}
        if (reqDateTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give Date Of Requisition");
            reqDateTextBox.Focus();
            return false;
        }
        if (financialYearDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Give Financial Year");
            reqDateTextBox.Focus();
            return false;
        }
        if (jobtitleDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Choose Job Title");
            jobtitleDropDownList.Focus();
            return false;
        }


        if (companyDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Choose Company Name");
            companyDropDownList.Focus();
            return false;
        }

        if (isBudgetedCheckBox.Checked == true)
        {
            if (BudgetCodeDropDownList.SelectedIndex == 0)
            {
                BudgetCodeDropDownList.Focus();
                ShowMessageBox("Choose Budget Code");
                return false;
            }
            return true;
        }
        if (NoteTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give NOTE");
            NoteTextBox.Focus();
            return false;
        }
       
        if (gradeDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Choose Grade");
            gradeDropDownList.Focus();
            return false;
        }
       
        if (nosTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give NO. Of Vacency");
            nosTextBox.Focus();
            return false;
        }

      
        if (placeOfPostingTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give Place of posting");
            placeOfPostingTextBox.Focus();
            return false;
        }
        if (reportToTextBox.Text == string.Empty)
        {
            ShowMessageBox("Give Report to");
            reportToTextBox.Focus();
            return false;
        }
        //if (divisionDropDownList.SelectedIndex==0)
        //{
        //    ShowMessageBox("Choose Division ");
        //    divWingDropDownList.Focus();
        //    return false;
        //}
        //if (divWingDropDownList.SelectedIndex == 0)
        //{
        //    ShowMessageBox("Choose Division Wing");
        //    divWingDropDownList.Focus();
        //    return false;
        //}
        if (deptDropDownList.SelectedIndex == 0)
        {
            ShowMessageBox("Choose Department");
            deptDropDownList.Focus();
            return false;
        }
        if (expDtJoinTextBox.Text== string.Empty)
        {
            ShowMessageBox("Give Expected Date of Joining");
            expDtJoinTextBox.Focus();
            return false;
        }

    
        //if (justficationTextBox.Text == string.Empty)
        //{
        //    ShowMessageBox("Give Description");
        //    return false;
        //}
        


        return true;
    }

    public void Save()
    {
        if (Validation())
        {
            EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();

            aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            aEmployeeRequsitionDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
          //  aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            aEmployeeRequsitionDao.Note = NoteTextBox.Text.Trim();
            aEmployeeRequsitionDao.ReqCode = ReqCodetextBox.Text.Trim();
            aEmployeeRequsitionDao.ReqDate = Convert.ToDateTime(reqDateTextBox.Text);
            aEmployeeRequsitionDao.FinYearId = Convert.ToInt32(mainFinyearDropDownList.SelectedValue);
            aEmployeeRequsitionDao.GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue);
            aEmployeeRequsitionDao.Nos = Convert.ToInt32(nosTextBox.Text.Trim());
            aEmployeeRequsitionDao.OtherExperience = othersTextBox.Text.Trim();


            //New
            aEmployeeRequsitionDao.JobSummery = descriptionTextBox.Text;
            aEmployeeRequsitionDao.JobTitle = jobTitleTextBox.Text;

            if (projectDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.ProjectId = Convert.ToInt32(projectDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            aEmployeeRequsitionDao.InternalContact = internalConTextBox.Text;
            aEmployeeRequsitionDao.ExternalContact = externalConTextBox.Text;

            if (officeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.OfficeId = Convert.ToInt32(officeDropDownList.SelectedValue);
            }

            if (placeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.PlaceId = Convert.ToInt32(placeDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.ProfCertification = profCertificationTextBox.Text;
            aEmployeeRequsitionDao.CmpSkill = cmpSkillsTextBox.Text;

            //New

            if (typeOfPosRadioButtonList.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            }

            if (typeOfPosRadioButtonList.Items[1].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            }

            if (typeOfPosRadioButtonList.Items[2].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[2].Value;
            }

            //if (typeOfPosRadioButtonList.Items[3].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[3].Value;
            //}


          
           
                //aEmployeeRequsitionDao.IsPermanent = typeOfPosRadioButtonList.Items[0].Selected;
                //aEmployeeRequsitionDao.IsContract = typeOfPosRadioButtonList.Items[1].Selected;
                //aEmployeeRequsitionDao.IsCasual = typeOfPosRadioButtonList.Items[2].Selected;
                //aEmployeeRequsitionDao.IsOther = typeOfPosRadioButtonList.Items[3].Selected;
            aEmployeeRequsitionDao.PlaceOfPosting = placeOfPostingTextBox.Text.Trim();
            aEmployeeRequsitionDao.ReportingTo = reportToTextBox.Text.Trim();
                //aEmployeeRequsitionDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
                //aEmployeeRequsitionDao.WingId = Convert.ToInt32(divWingDropDownList.SelectedValue);

                aEmployeeRequsitionDao.DeptId = Convert.ToInt32(deptDropDownList.SelectedValue);
                aEmployeeRequsitionDao.ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text.Trim());

                //aEmployeeRequsitionDao.IsInternalCir = CheckBoxList1.Items[0].Selected;
                //aEmployeeRequsitionDao.IsOnlineCir = CheckBoxList1.Items[1].Selected;
                //aEmployeeRequsitionDao.IsSMCWeb = CheckBoxList1.Items[2].Selected;
                //aEmployeeRequsitionDao.IsNewsPaper = CheckBoxList1.Items[3].Selected;
                //aEmployeeRequsitionDao.IsHeadHuntFirm = CheckBoxList1.Items[3].Selected;
                //aEmployeeRequsitionDao.IsOtherCircula = OtherCheckBox.Checked;
         

                if (OtherCheckBox.Checked == true)
                {
                    aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text.Trim();
                }

       
                //aEmployeeRequsitionDao.IsReplacement = IsReplacementforCheckBox.Checked;


                //if (IsReplacementforCheckBox.Checked==true)
                {
                    aEmployeeRequsitionDao.SeparationDate = DateOfSeperationTextBox.Text.Trim();
                    //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
                    aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(repEmpIdHiddenField.Value);
                }

                //else
                //{
                //    aEmployeeRequsitionDao.SeparationDate = "";
                //}

               


         

            //aEmployeeRequsitionDao.SeparationDate = string.IsNullOrEmpty(DateOfSeperationTextBox.Text) ? (DateTime?)null : DateTime.Parse(DateOfSeperationTextBox.Text); ;
                //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(empCodeDropDownList.SelectedValue)
                aEmployeeRequsitionDao.IsBudgeted = isBudgetedCheckBox.Checked;
                aEmployeeRequsitionDao.Remarks = RemarksTextBox.Text;

            if (isBudgetedCheckBox.Checked==true)
            {
                aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
            }

                //Justification = justficationTextBox.Text,
                //SeparationDate = Convert.ToDateTime(seprationdateTextBox.Text),
                aEmployeeRequsitionDao.Experience = experienceTextBox.Text;
                aEmployeeRequsitionDao.Skills = skillTextBox.Text;
                aEmployeeRequsitionDao.Age = ageTextBox.Text;
                //aEmployeeRequsitionDao.IsOtherCircula = CheckBoxList1.Items[4].Selected;
                aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text;


       
            int id = aEmployeeRequsitionDal.SaveEmpReq(aEmployeeRequsitionDao);
            if (id > 0)
            {


                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    JobReqKeyRespon aJobReqKeyRespon = new JobReqKeyRespon();

                    aJobReqKeyRespon.JobReqFormId = id;
                    //aJobReqKeyRespon.KeyResId = Convert.ToInt32(KeyResponGridView.DataKeys[i][0].ToString().Trim());
                    aJobReqKeyRespon.JobReqKeyResName = KeyResponGridView.Rows[i].Cells[0].Text;
                       
                  
                    aEmployeeRequsitionDal.SaveJobReqKeyRespon(aJobReqKeyRespon);
                }
                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    EducationRequirDesJobReq aEducationRequirDesJobReq = new EducationRequirDesJobReq()
                    {
                        JobReqFormId = id,
                        EduRequirId = Convert.ToInt32(EducationRequirementGridView.DataKeys[i][0].ToString().Trim())

                    };
                    aEmployeeRequsitionDal.SaveEduRequirSave(aEducationRequirDesJobReq);
                }

                CirculationWayDao aDao;

                if (id > 0)
                {

                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        aDao = new CirculationWayDao();


                        if (CheckBoxList1.Items[i].Selected)
                        {
                             aDao.MasterId = id;
                             aDao.WayId = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                        }

                        aEmployeeRequsitionDal.SaveCirculationWayDetail(aDao);

                    }
                    

                    
                }
                ShowMessageBox("Data Saved Successfully !!!");
                MethodAutoIncri();
                Clear();

            }
        }
    }

    protected void adddImageButton_Click(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("KeyRespon");

        DataRow dataRow = null;
        if (loadGridView.Rows.Count > 0)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["KeyRespon"] =
                    ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("keyresponsTextBox")).Text.Trim();

                dataRow["JobReqKeyResNameTextBox"] =
                    ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("JobReqKeyResNameTextBox")).Text.Trim();

                aDataTable.Rows.Add(dataRow);
            }
        }
        dataRow = aDataTable.NewRow();


        dataRow["KeyRespon"] = "";

        aDataTable.Rows.Add(dataRow);

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    protected void delImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("KeyRespon");

        DataRow dataRow = null;

        if (loadGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["KeyRespon"] =
                        ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("keyresponsTextBox")).Text.Trim();

                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    public void Update()
    {
        if (Validation())
        {
            EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();

            aEmployeeRequsitionDao.JobReqId = Convert.ToInt32(empIdHiddenField.Value);

            aEmployeeRequsitionDao.JobTitleId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            aEmployeeRequsitionDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
            aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            aEmployeeRequsitionDao.Note = NoteTextBox.Text;
            aEmployeeRequsitionDao.ReqCode = ReqCodetextBox.Text;
            aEmployeeRequsitionDao.ReqDate = Convert.ToDateTime(reqDateTextBox.Text);
            aEmployeeRequsitionDao.FinYearId = Convert.ToInt32(mainFinyearDropDownList.SelectedValue);
            aEmployeeRequsitionDao.GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue);
            aEmployeeRequsitionDao.Nos = Convert.ToInt32(nosTextBox.Text);
            aEmployeeRequsitionDao.OtherExperience = othersTextBox.Text;

            if (typeOfPosRadioButtonList.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            }

            if (typeOfPosRadioButtonList.Items[1].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            }

            if (typeOfPosRadioButtonList.Items[2].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[2].Value;
            }

            //if (typeOfPosRadioButtonList.Items[3].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[3].Value;
            //}




            //aEmployeeRequsitionDao.IsPermanent = typeOfPosRadioButtonList.Items[0].Selected;
            //aEmployeeRequsitionDao.IsContract = typeOfPosRadioButtonList.Items[1].Selected;
            //aEmployeeRequsitionDao.IsCasual = typeOfPosRadioButtonList.Items[2].Selected;
            //aEmployeeRequsitionDao.IsOther = typeOfPosRadioButtonList.Items[3].Selected;
            aEmployeeRequsitionDao.PlaceOfPosting = placeOfPostingTextBox.Text;
            aEmployeeRequsitionDao.ReportingTo = reportToTextBox.Text;
           // aEmployeeRequsitionDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
           // aEmployeeRequsitionDao.WingId = Convert.ToInt32(divWingDropDownList.SelectedValue);

            aEmployeeRequsitionDao.DeptId = Convert.ToInt32(deptDropDownList.SelectedValue);
            aEmployeeRequsitionDao.ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text);
            aEmployeeRequsitionDao.IsInternalCir = CheckBoxList1.Items[0].Selected;
            aEmployeeRequsitionDao.IsOnlineCir = CheckBoxList1.Items[1].Selected;
            aEmployeeRequsitionDao.IsSMCWeb = CheckBoxList1.Items[2].Selected;
            aEmployeeRequsitionDao.IsNewsPaper = CheckBoxList1.Items[3].Selected;
            aEmployeeRequsitionDao.IsHeadHuntFirm = CheckBoxList1.Items[3].Selected;
            aEmployeeRequsitionDao.IsOtherCircula = OtherCheckBox.Checked;


            if (OtherCheckBox.Checked == true)
            {
                aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text;
            }


            //aEmployeeRequsitionDao.IsReplacement = IsReplacementforCheckBox.Checked;


            //if (IsReplacementforCheckBox.Checked == true)
            //{
                aEmployeeRequsitionDao.SeparationDate = DateOfSeperationTextBox.Text;
                //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
                aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(repEmpIdHiddenField.Value);
            //}

            //else
            //{
            //    aEmployeeRequsitionDao.SeparationDate = "";
            //}






            //aEmployeeRequsitionDao.SeparationDate = string.IsNullOrEmpty(DateOfSeperationTextBox.Text) ? (DateTime?)null : DateTime.Parse(DateOfSeperationTextBox.Text); ;
            //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(empCodeDropDownList.SelectedValue)
            aEmployeeRequsitionDao.IsBudgeted = isBudgetedCheckBox.Checked;
            aEmployeeRequsitionDao.Remarks = RemarksTextBox.Text;

            if (isBudgetedCheckBox.Checked == true)
            {
                aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
            }

            //Justification = justficationTextBox.Text,
            //SeparationDate = Convert.ToDateTime(seprationdateTextBox.Text),
            aEmployeeRequsitionDao.Experience = experienceTextBox.Text;
            aEmployeeRequsitionDao.Skills = skillTextBox.Text;
            aEmployeeRequsitionDao.Age = ageTextBox.Text;
            aEmployeeRequsitionDao.IsOtherCircula = CheckBoxList1.Items[4].Selected;
            aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text;



           bool  status= aEmployeeRequsitionDal.UpdateEmpReq(aEmployeeRequsitionDao);
           if (status == true)
            {


                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
       
                    aEmployeeRequsitionDal.DelJobReqKeyRespon(empIdHiddenField.Value);
                }

                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    JobReqKeyRespon aJobReqKeyRespon = new JobReqKeyRespon();

                    aJobReqKeyRespon.JobReqFormId = Convert.ToInt32(empIdHiddenField.Value);
                    aJobReqKeyRespon.JobReqKeyResName =
                        Convert.ToString(KeyResponGridView.DataKeys[i][0]);

                    aEmployeeRequsitionDal.SaveJobReqKeyRespon(aJobReqKeyRespon);
                }



                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    //JobReqKeyRespon aJobReqKeyRespon = new JobReqKeyRespon()
                    //{
                    //    JobReqFormId = Convert.ToInt32(empIdHiddenField.Value),
                    //    KeyResId = Convert.ToInt32(KeyResponGridView.DataKeys[i][0].ToString().Trim())

                    //};
                    aEmployeeRequsitionDal.DelJobReqEduREqui(empIdHiddenField.Value);
                }
                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    EducationRequirDesJobReq aEducationRequirDesJobReq = new EducationRequirDesJobReq()
                    {
                        JobReqFormId = Convert.ToInt32(empIdHiddenField.Value),
                        EduRequirId = Convert.ToInt32(EducationRequirementGridView.DataKeys[i][0].ToString().Trim())

                    };
                    aEmployeeRequsitionDal.SaveEduRequirSave(aEducationRequirDesJobReq);
                }
                ShowMessageBox("Data Updated Successfully !!!");
                Clear();


                //{
                //    JobReqId = Convert.ToInt32(empIdHiddenField.Value),
                //    CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue),
                //    JobTitleId = Convert.ToInt32(jobtitleDropDownList.SelectedValue),
                //    Note = Convert.ToString(NoteTextBox.Text),

                //    GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue),
                //    Nos = Convert.ToInt32(nosTextBox.Text),

                //    OtherExperience = othersTextBox.Text,
                //    IsPermanent = typeOfPosRadioButtonList.Items[0].Selected,
                //    IsContract = typeOfPosRadioButtonList.Items[1].Selected,
                //    IsCasual = typeOfPosRadioButtonList.Items[2].Selected,
                //    IsOther = typeOfPosRadioButtonList.Items[3].Selected,
                //    PlaceOfPosting = placeOfPostingTextBox.Text,
                //    ReportingTo = reportToTextBox.Text,
                //    DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue),
                //    WingId = Convert.ToInt32(divWingDropDownList.SelectedValue),

                //    DeptId = Convert.ToInt32(deptDropDownList.SelectedValue),
                //    ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text),
                //    IsInternalCir = CheckBoxList1.Items[0].Selected,
                //    IsOnlineCir = CheckBoxList1.Items[1].Selected,
                //    IsSMCWeb = CheckBoxList1.Items[2].Selected,
                //    IsNewsPaper = CheckBoxList1.Items[3].Selected,
                //    IsHeadHuntFirm = CheckBoxList1.Items[3].Selected,
                //    Others = othersTextBox.Text,
                //    //IsReplacement = replaceForCheckBox.Checked,
                //    //ReplaceEmpId = Convert.ToInt32(empCodeDropDownList.SelectedValue),
                //    //IsBudgeted = CheckBox1.Checked,
                //    //Justification = justficationTextBox.Text,
                //    //SeparationDate = Convert.ToDateTime(seprationdateTextBox.Text),
                //    Experience = experienceTextBox.Text,
                //    Skills = skillTextBox.Text,
                //    Age = ageTextBox.Text,
                //    IsOtherCircula = CheckBoxList1.Items[4].Selected,
                //    OtherCircula = otherTextBox.Text,


                //};
                //bool id = aEmployeeRequsitionDal.UpdateEmpReq(aEmployeeRequsitionDao);
                //if (id == true)
                //{

                //    aEmployeeRequsitionDal.DelEmpReqEducation(empIdHiddenField.Value);
                //    aEmployeeRequsitionDal.DelKeyResponsbility(empIdHiddenField.Value);
                //    for (int i = 0; i < loadGridView.Rows.Count; i++)
                //    {
                //        KeyResponsibilityDAO aKeyResponsibilityDao = new KeyResponsibilityDAO()
                //        {
                //            JobReqId = Convert.ToInt32(empIdHiddenField.Value),
                //            KeyResponsibility =
                //                ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("keyresponsTextBox")).Text.Trim()
                //        };
                //        //aEmployeeRequsitionDal.SaveKeyResponsbility(aKeyResponsibilityDao);
                //    }
                //    for (int i = 0; i < educationGridView.Rows.Count; i++)
                //    {
                //        ReqEducationDAO aReqEducationDao = new ReqEducationDAO()
                //        {
                //            JobReqId = Convert.ToInt32(empIdHiddenField.Value),
                //            Education =
                //                ((TextBox)educationGridView.Rows[i].Cells[1].FindControl("educationTextBox")).Text.Trim()
                //        };
                //        aEmployeeRequsitionDal.SaveEmpReqEducation(aReqEducationDao);
                //    }
                //    ShowMessageBox("Data Update Successfully !!!");
                //    Clear();

                //}
            }
        }
    }


    public void MethodAutoIncri()
    {
        //using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRDB"].ConnectionString))
        //{
        //    string s = "select max(JobReqId)+1 from tblJobReqForm";
        //    SqlCommand csm = new SqlCommand(s, connection);

        //    connection.Open();
        //    csm.ExecuteNonQuery();

        //    SqlDataReader dd = csm.ExecuteReader();

        //    while (dd.Read())
        //    {
        //       int n =  dd.GetInt32(0);
        //       ReqCodetextBox.Text =   ReqnNoGenerator(n);
        //    }

        //    connection.Close();
        //}

        DataTable dt = aJobReqFormBll.GetId();
        ReqCodetextBox.Text = ReqnNoGenerator(Convert.ToInt32(dt.Rows[0][0].ToString()));
    }


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
        code = "REQ-" + Id;
        return code;
    }

    protected void Button2_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value ==string.Empty)
        {
            Save();
        }
        else
        {
            Update();   
        }
        
    }

    public void LoadEduGrid()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Education");

        DataRow dataRow = null;
        dataRow = aDataTable.NewRow();


        dataRow["Education"] = "";
        aDataTable.Rows.Add(dataRow);

        educationGridView.DataSource = null;
        educationGridView.DataBind();
        educationGridView.DataSource = aDataTable;
        educationGridView.DataBind();
    }

    public void LoadKeyResponse()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow = null;
        dataRow = aDataTable.NewRow();
        dataRow["JobReqKeyResName"] = "";

        aDataTable.Rows.Add(dataRow);

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    protected void addeduImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Education");

        DataRow dataRow = null;
        if (educationGridView.Rows.Count > 0)
        {
            for (int i = 0; i < educationGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["Education"] =
                    ((TextBox)educationGridView.Rows[i].Cells[1].FindControl("educationTextBox")).Text.Trim();

                aDataTable.Rows.Add(dataRow);
            }
        }
        dataRow = aDataTable.NewRow();


        dataRow["Education"] = "";

        aDataTable.Rows.Add(dataRow);

        educationGridView.DataSource = null;
        educationGridView.DataBind();
        educationGridView.DataSource = aDataTable;
        educationGridView.DataBind();
    }

    protected void deleduImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Education");

        DataRow dataRow = null;

        if (educationGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < educationGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["Education"] =
                        ((TextBox)educationGridView.Rows[i].Cells[1].FindControl("educationTextBox")).Text.Trim();

                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        educationGridView.DataSource = null;
        educationGridView.DataBind();
        educationGridView.DataSource = aDataTable;
        educationGridView.DataBind();
    }
    protected void isBudgetedCheckBox_CheckedChanged(object sender, EventArgs e)
    {


        if (isBudgetedCheckBox.Checked)
        {
            if (companyDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList, companyDropDownList.SelectedValue);

                BudgetCodeDropDownList.Visible = isBudgetedCheckBox.Checked;
                //lblCodeBudget.Visible = isBudgetedCheckBox.Checked;
                ShowBudgetCodeDiv.Visible = isBudgetedCheckBox.Checked;
                //BudgetCodeDropDownList.SelectedIndex = 0;
                //if (isBudgetedCheckBox.Checked == true)
                //    BudgetCodeDropDownList.Visible = true;
                //else
                //    BudgetCodeDropDownList.Visible = false;
            }

            else
            {
                isBudgetedCheckBox.Checked = false;
                ShowMessageBox("Please Select a company first !!!!");
            }
        }
        else
        {
            ShowBudgetCodeDiv.Visible = false;
        }
        
    }
    protected void IsReplacementforCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            //lblEmpName.Visible = IsReplacementforCheckBox.Checked;
            //lblDatSep.Visible = IsReplacementforCheckBox.Checked;
            //detail.Visible = IsReplacementforCheckBox.Checked;

            //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeDropDownList.Visible = IsReplacementforCheckBox.Checked;
            //ShowReplacementfor.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;

            DateOfSeperationTextBox.Text = String.Empty;
            //EmployeeDropDownList.SelectedValue = String.Empty;
            EmployeeNameTextBox.Text = "";
        }
        else
        {
            //IsReplacementforCheckBox.Checked = false;
            ShowMessageBox("Please select a company first !!!");
        }


    }
    
    protected void OtherCheckBox_CheckedChanged(object sender, EventArgs e)
    {

        otherTextBox.Visible = OtherCheckBox.Checked;
        LblOther.Visible = OtherCheckBox.Checked;
        otherTextBox.Text = String.Empty;

    }

    private bool AddEventValidation()
    {

        int count = 0;

        for (int i = 0; i < jdCheckBoxList.Items.Count; i++)
        {
            if (jdCheckBoxList.Items[i].Selected)
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
            ShowMessageBox("Please Select at least one JD !!!");
            return false;
        }

        return true;
    }
    protected void addImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        if (AddEventValidation())
        {

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("JobReqKeyResName");
            DataRow dataRow;


            for (int j = 0; j < jdCheckBoxList.Items.Count; j++)
            {

                if (jdCheckBoxList.Items[j].Selected)
                {
                    if (CheckItemAlreadyExistOrNot(j))
                    {
                        if (KeyResponGridView.Rows.Count == 0)
                        {
                            dataRow = aDataTable.NewRow();

                            dataRow = aDataTable.NewRow();
                            dataRow["JobReqKeyResName"] = jdCheckBoxList.Items[j].Text;

                            aDataTable.Rows.Add(dataRow);

                            KeyResponGridView.DataSource = aDataTable;
                            KeyResponGridView.DataBind();
                        }

                        else
                        {
                            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                            {
                                dataRow = aDataTable.NewRow();
                                dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                                aDataTable.Rows.Add(dataRow);
                            }

                            dataRow = aDataTable.NewRow();
                            dataRow["JobReqKeyResName"] = jdCheckBoxList.Items[j].Text;

                            aDataTable.Rows.Add(dataRow);

                            KeyResponGridView.DataSource = aDataTable;
                            KeyResponGridView.DataBind();
                        }
                    }
                } 
                }
            }

            



        //string keyResponId = KeyResponsibilitesDropDownList.Text;
        //string keyRespon = KeyResponsibilitesDropDownList.Text.Trim();

            

            //aDataTable.Columns.Add("JobReqKeyResId");
            



            

            
}

    private bool CheckItemAlreadyExistOrNot(int i)
    {
        for (int j = 0; j < KeyResponGridView.Rows.Count; j++)
        {
            if (KeyResponGridView.Rows[j].Cells[0].Text == jdCheckBoxList.Items[i].Text)
            {
                return false;
            }
        }

        return true;
    }

    


    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();


        // SetState();

    }
    protected void EducationRequirementImageButton_Click(object sender, ImageClickEventArgs e)
    {
        if (AddEducationRequirementValidation())
        {
            string eRID = EducationRequirementDropDownList.SelectedValue;
            string educationRequirements = EducationRequirementDropDownList.SelectedItem.Text.Trim();

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("ERID");
            aDataTable.Columns.Add("EducationRequirements");



            DataRow dataRow;

            if (EducationRequirementGridView.Rows.Count > 0)
            {
                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    if (EducationRequirementGridView.Rows[i].Cells[0].Text != educationRequirements)
                    {
                        dataRow = aDataTable.NewRow();

                        dataRow["ERID"] = EducationRequirementGridView.DataKeys[i][0].ToString();
                        dataRow["EducationRequirements"] = EducationRequirementGridView.Rows[i].Cells[0].Text;

                        aDataTable.Rows.Add(dataRow);
                        EducationRequirementDropDownList.SelectedValue = "";
                    }

                    else
                    {
                        EducationRequirementDropDownList.SelectedValue = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();

                dataRow["ERID"] = eRID;
                dataRow["EducationRequirements"] = educationRequirements;

                aDataTable.Rows.Add(dataRow);

                EducationRequirementGridView.DataSource = aDataTable;
                EducationRequirementGridView.DataBind();

                EducationRequirementDropDownList.SelectedValue = "";

            }


            else
            {
                dataRow = aDataTable.NewRow();

                dataRow["ERID"] = eRID;
                dataRow["EducationRequirements"] = educationRequirements;

                aDataTable.Rows.Add(dataRow);

                EducationRequirementGridView.DataSource = aDataTable;
                EducationRequirementGridView.DataBind();

                EducationRequirementDropDownList.SelectedValue = "";
            }
        }

    }

    private bool AddEducationRequirementValidation()
    {
        if (EducationRequirementDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select Education Requirement !!!");
            return false;
        }

        return true;
    }

    protected void deleteImageButtonEducationRequirement_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("ERID");
        aDataTable.Columns.Add("EducationRequirements");

        DataRow dataRow;

        if (EducationRequirementGridView.Rows.Count > 0)
        {
            for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["ERID"] = EducationRequirementGridView.DataKeys[i][0].ToString();
                    dataRow["EducationRequirements"] = EducationRequirementGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        EducationRequirementGridView.DataSource = aDataTable;
        EducationRequirementGridView.DataBind();


        // SetState();
        
    }
    protected void ViewListButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobRequisitionFormView.aspx");
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aEmployeeRequsitionDal.LoadDepartmentByWings(deptDropDownList, companyDropDownList.SelectedValue);
        //aEmployeeRequsitionDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
        aEmployeeRequsitionDal.LoadFinancialYear(mainFinyearDropDownList,companyDropDownList.SelectedValue);

        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;

            SetSearchBoxStatus();

            LoadDivisionddl();
        }
        else
        {
            dsnDropDownList.Items.Clear();
        }

        Clear2();

       
    }

    private void LoadDivisionddl()
    {
        aEmployeeRequsitionDal.LoadDivisionDdl(dsnDropDownList,companyDropDownList.SelectedValue);
    }


    protected void EmployeeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {

        string empName = EmployeeNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            EmployeeNameTextBox.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];
            DataTable dtdata = aEmployeeRequsitionDal.GetEmpData(repEmpIdHiddenField.Value);
            if (dtdata.Rows.Count>0)
            {
                codeLabel.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
                nameLabel.Text = dtdata.Rows[0]["EmpName"].ToString();
                desigLabel.Text = dtdata.Rows[0]["Designation"].ToString();
                salgradLabel.Text = dtdata.Rows[0]["GradeName"].ToString();
                divLabel.Text = dtdata.Rows[0]["DivisionName"].ToString();
                //wingLabel.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
                //deptLabel.Text = dtdata.Rows[0]["DepartmentName"].ToString();
                //secLabel.Text = dtdata.Rows[0]["SectionName"].ToString();
                //subsecLabel.Text = dtdata.Rows[0]["SubSectionName"].ToString();
                DateOfSeperationTextBox.Text =
                    string.IsNullOrEmpty(dtdata.Rows[0]["JobLeftDate"].ToString()) ? "" : Convert.ToDateTime(dtdata.Rows[0]["JobLeftDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {
            
            EmployeeNameTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }


    protected void financialYearDropDownList_OnTextChanged(object sender, EventArgs e)
    {
        if (financialYearDropDownList.SelectedValue != "")
        {
            //aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);
            aEmployeeRequsitionDal.LoadCodeBudgetYearWise(BudgetCodeDropDownList,financialYearDropDownList.SelectedValue,companyDropDownList.SelectedValue);
        }
        else
        {
            BudgetCodeDropDownList.Items.Clear();
        }
    }

    protected void reportToTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = reportToTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            reportToTextBox.Text = emp[2];
            

        }
        else
        {

            reportToTextBox.Text = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    protected void jobtitleDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (jobtitleDropDownList.SelectedValue != "")
        {
            Session["DesignationId"] = "";
            Session["DesignationId"] = jobtitleDropDownList.SelectedValue;
        }
    }

    protected void jstRadioButtonList_OnTextChanged(object sender, EventArgs e)
    {
        lblEmpName.Visible = false;
        lblDatSep.Visible = false;
        detail.Visible = false;

        DateOfSeperationTextBox.Text = String.Empty;
        EmployeeNameTextBox.Text = "";
        //jobDesc.Visible = false;
        descriptionTextBox.Text = "";

        string btnText = jstRadioButtonList.SelectedItem.Text.Trim();

        if (btnText == "Replacement")
        {
            lblEmpName.Visible = true;
            lblDatSep.Visible = true;
            detail.Visible = true;

            DateOfSeperationTextBox.Text = String.Empty;
            EmployeeNameTextBox.Text = "";
        }

        
    }

    protected void dsnDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (dsnDropDownList.SelectedValue != "")
        {
            Session["DsnId"] = "";
            Session["DsnId"] = dsnDropDownList.SelectedValue;

            if (companyDropDownList.SelectedValue != "")
            {
                Session["DsnCmpId"] = "";
                Session["DsnCmpId"] = companyDropDownList.SelectedValue;
            }
            else
            {
                Session["DsnCmpId"] = "";
                ShowMessageBox("Please select company !!");
            }
        }

        else
        {
            Session["DsnId"] = "";
        }

        SetKeyEmpTextboxStatus();
    }

    private void SetKeyEmpTextboxStatus()
    {
        if (dsnDropDownList.SelectedValue != "" && companyDropDownList.SelectedValue != "")
        {
            keyEmpTextBox.Enabled = true;
            keyEmpTextBox.ToolTip = "You have to select both company & Division for enabling textBox !!!";
        }
        else
        {
            keyEmpTextBox.Enabled = false;
            keyEmpTextBox.ToolTip = "";
        }
    }

    protected void keyEmpTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = keyEmpTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            keyEmpTextBox.Text = emp[1];

            DataTable aTable = aEmployeeRequsitionDal.GetEmpJDInfoByEmpCode(emp[0]);

            if (aTable.Rows.Count > 0)
            {
                jdCheckBoxList.DataValueField = "JdDetailsId";
                jdCheckBoxList.DataTextField = "JdDetailsInfo";
                jdCheckBoxList.DataSource = aTable;
                jdCheckBoxList.DataBind();
            }

        }
        else
        {
            keyEmpTextBox.Text = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    protected void typeOfPosRadioButtonList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        project.Visible = false;

        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {

            if (typeOfPosRadioButtonList.Items[i].Selected)
            {
                if (typeOfPosRadioButtonList.Items[i].Text.Trim() == "Contractual")
                {

                    if (companyDropDownList.SelectedValue != "")
                    {
                        project.Visible = true;
                        aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, companyDropDownList.SelectedValue);
                    }
                    else
                    {
                        typeOfPosRadioButtonList.Items[i].Selected = false;
                        ShowMessageBox("Please select company!!!");
                    }
                   
                }
                else
                {
                    project.Visible = false;
                    projectDropDownList.Items.Clear();
                }
            }
            
        }
        

    }


    protected void officeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (officeDropDownList.SelectedValue != "")
        {
            aEmployeeRequsitionDal.GetJobLocationOnPlaceDdl(placeDropDownList, officeDropDownList.SelectedValue);
        }
        else
        {
            placeDropDownList.Items.Clear();
        }
    }
}
