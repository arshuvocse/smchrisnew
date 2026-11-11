using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using FreeTextBoxControls;
using HELPER_FUNCTIONS.HELPERS;

public partial class Survey_SurveyDeclaretionEntry : System.Web.UI.Page
{

    tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
    SurveyDeclaretionEntryDAL atblEmployeePromotionEntryDAL = new SurveyDeclaretionEntryDAL();
    IncrementDal aSuspendDal = new IncrementDal();

    JdDAL _jdDal = new JdDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            SetCheckBox();
            GetCompany();
         //   UserPersmissionValidation();
            LoadQuestionInformation();
           
            LoadDropDownList();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");


            if (Session["VacancyCirculationId"] != null)
            {
                GetOneRecord(Session["VacancyCirculationId"].ToString());
                Session["VacancyCirculationId"] = null;
            }
        }
    }

    private void GetOneRecord(string id)
    {
        DataTable dataTable = atblEmployeePromotionEntryDAL.GeInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SurveyMasterId").ToString(CultureInfo.InvariantCulture);
            ddlCompany.SelectedValue = dataTable.Rows[rowIndex].Field<int>("CompanyId").ToString();
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);
            ddlFinYear.SelectedValue = dataTable.Rows[rowIndex].Field<int>("FinancialYearId").ToString();

            SurveyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SurveyName");
            EffectiveDateTextBox.Text = dataTable.Rows[rowIndex].Field<DateTime>("SurveyFrom").ToString("dd-MMM-yyyy");
            EffectToDate.Text = dataTable.Rows[rowIndex].Field<DateTime>("SurveyTo").ToString("dd-MMM-yyyy");
            

           
            if (dataTable.Rows[rowIndex].Field<bool>("IsActive"))
            {
                if (!isActiveCheckBox.Checked)
                {
                    isActiveCheckBox.Checked = true;
                }
            }
            else
            {
                isActiveCheckBox.Checked = false;
            }



            DataTable QuestiondataTable = atblEmployeePromotionEntryDAL.GeQuestionById(id);
            //if (QuestiondataTable.Rows.Count > 0)
            //{


            //    GVQuestion.DataSource = QuestiondataTable;
            //    GVQuestion.DataBind();
            //}



            DataTable dd = atblEmployeePromotionEntryDAL.GetQuestionGVformation();

            if (dataTable.Rows.Count > 0)
            {
                GVQuestion.DataSource = dd;
                GVQuestion.DataBind();
            }


            for (int i = 0; i < QuestiondataTable.Rows.Count; i++)
            {
                for (int j = 0; j < GVQuestion.Rows.Count; j++)
                {

                  
                    CheckBox chkSelect = (CheckBox)GVQuestion.Rows[j].FindControl("chkSelect");
                    HiddenField txt_empInfoId = (HiddenField)GVQuestion.Rows[j].FindControl("txt_empInfoId");
                    if (txt_empInfoId.Value == QuestiondataTable.Rows[i]["SurveyQuestionId"].ToString())
                   
                    {
                        chkSelect.Checked = true;

                        GVQuestion.Rows[j].Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
                        GVQuestion.Rows[j].Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
                        GVQuestion.Rows[j].Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
                        GVQuestion.Rows[j].Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
                        GVQuestion.Rows[j].Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
                       
                    }

                }
            }


            //LoadEmpInformation();

            DataTable EmployeedataTable = atblEmployeePromotionEntryDAL.GeEmpGeneralInfoById(id);

            if (EmployeedataTable.Rows.Count > 0)
            {
                EmpSaveGridView1.DataSource = EmployeedataTable;
                EmpSaveGridView1.DataBind();
            }



            //for (int i = 0; i < EmployeedataTable.Rows.Count; i++)
            //{
            //    for (int j = 0; j < EmpInfoGridView.Rows.Count; j++)
            //    {


            //        CheckBox chkSelect = (CheckBox)EmpInfoGridView.Rows[j].FindControl("EmpchkSelect");
            //        HiddenField txt_empId = (HiddenField)EmpInfoGridView.Rows[j].FindControl("txt_empId");
            //        if (txt_empId.Value == EmployeedataTable.Rows[i]["EmpInfoId"].ToString())
            //        {


            //            EmpInfoGridView.Rows[j].Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
            //            EmpInfoGridView.Rows[j].Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
            //            EmpInfoGridView.Rows[j].Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
            //            EmpInfoGridView.Rows[j].Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
            //            EmpInfoGridView.Rows[j].Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
            //            EmpInfoGridView.Rows[j].Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
            //            EmpInfoGridView.Rows[j].Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFF0D8");
 
            //            chkSelect.Checked = true;

            //        }
                    

            //    }
            //}

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
            Response.Redirect("SurveyDeclaretionView.aspx");
        }

    }

    private void SetCheckBox()
    {
        if (!isActiveCheckBox.Checked)
        {
            isActiveCheckBox.Checked = true;
        }
    }

    private void LoadEmpInformation()
    {
        DataTable dataTable = atblEmployeePromotionEntryDAL.GetEMpInfos("  com.CompanyId=" + ddlCompany.SelectedValue + " " + Parameter());

        if (dataTable.Rows.Count > 0)
        {
            EmpInfoGridView.DataSource = dataTable;
            EmpInfoGridView.DataBind();
        }
    }

    public string Parameter()
    {
        string param = "";
        if (ddlCategory.Items.Count > 0)
        {
            if (ddlCategory.SelectedIndex > 0)
            {
                param = param + " AND e.EmpCategoryId='" + ddlCategory.SelectedValue + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }
        return param;

    }

    private void LoadQuestionInformation()
    {
        DataTable dataTable = atblEmployeePromotionEntryDAL.GetQuestionGVformation();

        if (dataTable.Rows.Count > 0)
        {
            GVQuestion.DataSource = dataTable;
            GVQuestion.DataBind();
        }
    }

    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompany(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
        _jdDal.LoadEmpCategory(ddlCategory);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);

             

         //   LoadEmpInformation();
            _jdDal.LoadDept(ddlDept, ddlCompany.SelectedValue);

        }
        else
        {
            ddlFinYear.Items.Clear();
        }
    }
    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();
        try
        {

            DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                for (int j = 0; j < lchk_Company.Items.Count; j++)
                {
                    if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                    {
                        lchk_Company.Items[j].Selected = true;


                    }
                }
            }
        
          }
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    public void UserPersmissionValidation()
    {
         
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }

    private void LoadInfo()
    {
        
    }

    private string GenerateParamiterList()
    {
 
        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }

       

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         
    }

    private void PopUp(Int32 EmployeePromotion)
    {
        string url = "../Report_UI/EmployeePromotionEntryViewReportViwer.aspx?rptType=" + EmployeePromotion;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
       
        Response.Redirect("SurveyDeclaretionView.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)GVQuestion.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < GVQuestion.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)GVQuestion.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }

    protected void EmpchkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)EmpInfoGridView.HeaderRow.FindControl("EmpchkSelectAll");

        for (int i = 0; i < EmpInfoGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)EmpInfoGridView.Rows[i].Cells[0].FindControl("EmpchkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {

       
                if (VacancyIdHiddenField.Value=="")
                { if (DataValidation())
            {
                     Save();
                }
            
            
            }
    }

    public void Save()
    {
        SurveyMasterDeclarationDAO aEntryDao = new SurveyMasterDeclarationDAO();
        
        aEntryDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aEntryDao.FinancialYearId = Convert.ToInt32(ddlFinYear.SelectedValue);
        aEntryDao.SurveyName = SurveyNameTextBox.Text.Trim();
        aEntryDao.SurveyFrom = Convert.ToDateTime(EffectiveDateTextBox.Text.Trim());
        aEntryDao.SurveyTo = Convert.ToDateTime(EffectToDate.Text.Trim());
        aEntryDao.IsActive = isActiveCheckBox.Checked;
        aEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);
        aEntryDao.EntryDate = DateTime.Now;
        int id = atblEmployeePromotionEntryDAL.SaveEntryInfo(aEntryDao);


        SurveyDetailsDeclarationDAO aDetailsDao;

        List<SurveyDetailsDeclarationDAO> aDaos = new List<SurveyDetailsDeclarationDAO>();



        for (int i = 0; i < GVQuestion.Rows.Count; i++)
        {
            aDetailsDao = new SurveyDetailsDeclarationDAO();
            var chkBoxRows = (CheckBox)GVQuestion.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                int SurveyMaster  = 0;
                int SurveyQuestion = 0;
                aDetailsDao = new SurveyDetailsDeclarationDAO();
                SurveyMaster = id;
                SurveyQuestion = Convert.ToInt32(GVQuestion.DataKeys[i].Value.ToString());
              //  id = SaveSurveyDetailsInfo(id);

                InsertSurveyDetailsByMasterId(SurveyMaster, SurveyQuestion);

            }
        }



        List<SurveyParticipateDeclarationDAO> SurveyDetails = new List<SurveyParticipateDeclarationDAO>();
        SurveyParticipateDeclarationDAO aSurveyDetails;

        for (int i = 0; i < EmpSaveGridView1.Rows.Count; i++)
        {
          


           // if (chkBoxRows.Checked)
            {

                int SurveyMaster = 0;
                int EmpID = 0;
                aSurveyDetails = new SurveyParticipateDeclarationDAO();
                SurveyMaster = id;
                EmpID = Convert.ToInt32(EmpSaveGridView1.DataKeys[i].Value.ToString());
                InsertSurveyParticipateByMasterId(SurveyMaster, EmpID);
          

            }
        }





        //if (id > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Saved Successfully...');window.location ='SurveyDeclaretionView.aspx';",
                   true);

        }
       
    }


    public void Update()
    {
        SurveyMasterDeclarationDAO aEntryDao = new SurveyMasterDeclarationDAO();

        aEntryDao.SurveyMasterId = Convert.ToInt32(VacancyIdHiddenField.Value);
        aEntryDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aEntryDao.FinancialYearId = Convert.ToInt32(ddlFinYear.SelectedValue);
        aEntryDao.SurveyName = SurveyNameTextBox.Text.Trim();
        aEntryDao.SurveyFrom = Convert.ToDateTime(EffectiveDateTextBox.Text.Trim());
        aEntryDao.SurveyTo = Convert.ToDateTime(EffectToDate.Text.Trim());
        aEntryDao.IsActive = isActiveCheckBox.Checked;
        aEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aEntryDao.UpdateDate = DateTime.Now;
        atblEmployeePromotionEntryDAL.UpdateEntryInfo(aEntryDao);


        SurveyDetailsDeclarationDAO aDetailsDao;

        List<SurveyDetailsDeclarationDAO> aDaos = new List<SurveyDetailsDeclarationDAO>();

        atblEmployeePromotionEntryDAL.DeleteQuestionByById((VacancyIdHiddenField.Value));

        for (int i = 0; i < GVQuestion.Rows.Count; i++)
        {
            aDetailsDao = new SurveyDetailsDeclarationDAO();
            var chkBoxRows = (CheckBox)GVQuestion.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                 int SurveyMaster  = 0;
                int SurveyQuestion = 0;
                aDetailsDao = new SurveyDetailsDeclarationDAO();
                SurveyMaster = Convert.ToInt32(VacancyIdHiddenField.Value);
                SurveyQuestion = Convert.ToInt32(GVQuestion.DataKeys[i].Value.ToString());
              //  id = SaveSurveyDetailsInfo(id);

               
               
                InsertSurveyDetailsByMasterId(SurveyMaster, SurveyQuestion);

            }
        }



        List<SurveyParticipateDeclarationDAO> SurveyDetails = new List<SurveyParticipateDeclarationDAO>();
        SurveyParticipateDeclarationDAO aSurveyDetails;
        atblEmployeePromotionEntryDAL.DeleteEmpParticipateByById((VacancyIdHiddenField.Value));
        for (int i = 0; i < EmpSaveGridView1.Rows.Count; i++)
        {
             




         //   if (chkBoxRows.Checked)
            {

                int SurveyMaster = 0;
                int EmpID = 0;
                aSurveyDetails = new SurveyParticipateDeclarationDAO();
                SurveyMaster = Convert.ToInt32(VacancyIdHiddenField.Value);
                EmpID = Convert.ToInt32(EmpSaveGridView1.DataKeys[i].Value.ToString());
                 

             
                InsertSurveyParticipateByMasterId(SurveyMaster, EmpID);
            }
        }


        

         
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Updated Successfully...');window.location ='SurveyDeclaretionView.aspx';",
                   true);

        
    }
    private void InsertSurveyDetailsByMasterId(int SurveyMaster, int SurveyQuestion)
    {
        SurveyDetailsDeclarationDAO aInfo = new SurveyDetailsDeclarationDAO();



        aInfo.SurveyMasterId = SurveyMaster;
        aInfo.SurveyQuestionId = SurveyQuestion;

        atblEmployeePromotionEntryDAL.SaveSurveyDetailsInfo(aInfo);

    }

    private void InsertSurveyParticipateByMasterId(int SurveyMaster, int EmpId)
    {
        SurveyParticipateDeclarationDAO aInfo = new SurveyParticipateDeclarationDAO();



        aInfo.SurveyMasterId = SurveyMaster;
        aInfo.EmployeeId = EmpId;

        atblEmployeePromotionEntryDAL.SaveSurveyParticipateInfo(aInfo);

    }
  
    private SurveyMasterDeclarationDAO PrepareDataForSave()
    {
        var aEntryDao = new SurveyMasterDeclarationDAO();
        aEntryDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aEntryDao.FinancialYearId = Convert.ToInt32(ddlFinYear.SelectedValue);
        aEntryDao.SurveyName = SurveyNameTextBox.Text.Trim();
        aEntryDao.SurveyFrom = Convert.ToDateTime(EffectiveDateTextBox.Text.Trim());
        aEntryDao.SurveyTo = Convert.ToDateTime(EffectToDate.Text.Trim());

        aEntryDao.IsActive = isActiveCheckBox.Checked;


        aEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        aEntryDao.EntryDate = DateTime.Now;

        return aEntryDao;
    }

    private Int32 SaveEntry()
    {
        Int32 retVal;
        try
        {
            retVal = atblEmployeePromotionEntryDAL.SaveEntryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    //private Int32 SaveSurveyDetailsInfo(int id)
    //{
    //     Int32 retVal;
    //    try

    //    {
    //        retVal = atblEmployeePromotionEntryDAL.SaveSurveyDetailsInfo(PrepareSurveyDetailsDataForSave(id));
    //    }

    //    catch (Exception ex)
    //    {
    //        retVal = 0;
    //        throw ex;
    //    }

    //    return retVal;
    //}

    private int SaveSurveyParticipateDetailsInfo(List<SurveyParticipateDeclarationDAO> aIncrementDaos)
    {
        Int32 id = 0;

        foreach (var aDao in aIncrementDaos)
        {


            id = atblEmployeePromotionEntryDAL.SaveSurveyParticipateInfo(aDao);
        }

        return id;
    }


    private List<SurveyDetailsDeclarationDAO> PrepareSurveyDetailsDataForSave(int PKId)
    {


        SurveyDetailsDeclarationDAO aDetailsDao;

        List<SurveyDetailsDeclarationDAO> aDaos = new List<SurveyDetailsDeclarationDAO>();

       

        for (int i = 0; i < GVQuestion.Rows.Count; i++)
        {
            aDetailsDao = new SurveyDetailsDeclarationDAO();
            var chkBoxRows = (CheckBox) GVQuestion.Rows[i].Cells[0].FindControl("chkSelect");




            if (chkBoxRows.Checked)
            {
                aDetailsDao = new SurveyDetailsDeclarationDAO();
                aDetailsDao.SurveyMasterId = PKId;
                aDetailsDao.SurveyQuestionId = Convert.ToInt32(GVQuestion.DataKeys[i].Value.ToString());
               
            }
        }

        return aDaos;
    }

    private List<SurveyParticipateDeclarationDAO> PrepareSurveyParticipateDetailsDataForSave()
    {




        List<SurveyParticipateDeclarationDAO> SurveyDetails = new List<SurveyParticipateDeclarationDAO>();
        SurveyParticipateDeclarationDAO aSurveyDetails;

        for (int i = 0; i < EmpInfoGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)EmpInfoGridView.Rows[i].Cells[0].FindControl("EmpchkSelect");




            if (chkBoxRows.Checked)
            {
                aSurveyDetails = new SurveyParticipateDeclarationDAO();
                Int32 SurveyQuestionId = Convert.ToInt32(EmpInfoGridView.DataKeys[i].Value.ToString());

            }
        }

        return SurveyDetails;
    }

    private bool DataValidation()
    {
        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("Please select a company !!!");
            ddlCompany.Focus();
            return false;
        }

        if (EffectiveDateTextBox.Text == "")
        {
            ShowMessageBox("Please select this !!!");
            EffectiveDateTextBox.Focus();
            return false;
        }


        if (EffectToDate.Text == "")
        {
            ShowMessageBox("Please select this !!!");
            EffectToDate.Focus();
            return false;
        }


        if (ddlFinYear.SelectedValue == "")
        {
            ShowMessageBox("Please select a financial year !!!");
            return false;
        }



        if (EmpSaveGridView1.Rows.Count==0)
        {
            ShowMessageBox("Please Add To List One employee !!!");
            return false;
        }

       

        Int32 Questioncount = 0;

        for (int i = 0; i < GVQuestion.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)GVQuestion.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                Questioncount++;
            }

            if (Questioncount > 0)
            {
                break;
            }
        }

        if (Questioncount == 0)
        {
            ShowMessageBox("Please select at least one Question !!!");
            return false;
        }
        return true;
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (VacancyIdHiddenField.Value != "")
        {
            if (DataValidation())
            {
                Update();
            }


        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {

        LoadEmpInformation();

    }

    protected void textButton_OnClick(object sender, EventArgs e)
    {
        if (CheckEmpList())
        {


            DataTable aDataTable = new DataTable();
            aDataTable.Columns.Add("EmpInfoId");
            aDataTable.Columns.Add("EmpMasterCode");
            aDataTable.Columns.Add("EmpName");
            aDataTable.Columns.Add("DepartmentName");
            aDataTable.Columns.Add("SalaryLocation");
            aDataTable.Columns.Add("EmpType");


            DataRow dataRow = null;

            for (int i = 0; i < EmpInfoGridView.Rows.Count; i++)
            {
                CheckBox ChkBoxRows = (CheckBox) EmpInfoGridView.Rows[i].Cells[0].FindControl("EmpchkSelect");
                if (ChkBoxRows.Checked)
                {
                    //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                    {
                        dataRow = aDataTable.NewRow();

                        dataRow["EmpInfoId"] = EmpInfoGridView.DataKeys[i][0].ToString();

                        dataRow["EmpMasterCode"] = EmpInfoGridView.Rows[i].Cells[2].Text;
                        dataRow["EmpName"] = EmpInfoGridView.Rows[i].Cells[3].Text;
                        dataRow["DepartmentName"] = EmpInfoGridView.Rows[i].Cells[4].Text;
                        dataRow["SalaryLocation"] = EmpInfoGridView.Rows[i].Cells[5].Text;
                        dataRow["EmpType"] = EmpInfoGridView.Rows[i].Cells[6].Text;


                        aDataTable.Rows.Add(dataRow);
                    }
                }
            }
            for (int i = 0; i < EmpSaveGridView1.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();
                dataRow["EmpInfoId"] = EmpSaveGridView1.DataKeys[i][0].ToString();
                dataRow["EmpMasterCode"] = EmpSaveGridView1.Rows[i].Cells[1].Text;
                dataRow["EmpName"] = EmpSaveGridView1.Rows[i].Cells[2].Text;
                dataRow["DepartmentName"] = EmpSaveGridView1.Rows[i].Cells[3].Text;
                dataRow["SalaryLocation"] = EmpSaveGridView1.Rows[i].Cells[4].Text;
                dataRow["EmpType"] = EmpSaveGridView1.Rows[i].Cells[5].Text;


                aDataTable.Rows.Add(dataRow);
            }

            EmpSaveGridView1.DataSource = aDataTable;
            EmpSaveGridView1.DataBind();
        }
        else
        {
            ShowMessageBox("Already Exist !!!");
        }
    }

    public bool CheckEmpList()
    {
        for (int i = 0; i < EmpInfoGridView.Rows.Count; i++)
        {
             var chkBoxRows = (CheckBox)EmpInfoGridView.Rows[i].Cells[0].FindControl("EmpchkSelect");
            for (int j = 0; j < EmpSaveGridView1.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                     if (EmpInfoGridView.DataKeys[i][0].ToString() == EmpSaveGridView1.DataKeys[j][0].ToString())
                {

                    return false;

                }
                     
                }
               
            }
            
        }
        Int32 Empcount = 0;

        for (int i = 0; i < EmpInfoGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)EmpInfoGridView.Rows[i].Cells[0].FindControl("EmpchkSelect");

            if (chkBoxRows.Checked)
            {
                Empcount++;
            }

            if (Empcount > 0)
            {
                break;
            }
        }

        if (Empcount == 0)
        {
            ShowMessageBox("Please select at least one employee !!!");
            return false;
        }

        return true;
    }
}