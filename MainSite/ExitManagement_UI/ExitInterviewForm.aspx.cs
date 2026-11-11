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
using HELPER_FUNCTIONS.HELPERS;

public partial class Survey_ExitInterviewForm : System.Web.UI.Page
{
    ExitInterviewFormDal aFormDal = new ExitInterviewFormDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            LoadExitFormItem();
            //ButtonVisible();
            if (Session["EmpInfoId"] !=null)
            {
                hfEmpInfoId.Value = Session["EmpInfoId"].ToString();
                
                txt_EmpName_OnTextChanged(null,null);
            }

            if (Session["EmpInfoId"] != null)
            {
                GetEmpinfo(Session["EmpInfoId"].ToString());
            }

        }
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
            }
            //else if (Session["Status"].ToString() == "Edit")
            //{
            //    editButton.Visible = true;
            //}
            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("ExitInterviewFormView.aspx");
        }

    }

    private void LoadDropDownList()
    {
        aFormDal.LoadCompanyDropDownList(ddlCompany);
        
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveDataValidation())
        {
            Int32 masterId = aFormDal.SaveMasterInfo(PreaperDataForMasterSave());

            if (masterId > 0)
            {
                Int32 exitQs = SaveExitQsDetailInfo(PrepareDataForQsDetail(masterId));
                Int32 servayId = SaveServayDetailInfo(PrepareDataForServayDetail(masterId));

                if (exitQs > 0 && servayId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Data Saved Successfully...');window.location ='ExitInterviewFormView.aspx';",
                true);
                    Clear();
                }
            }
        }
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

        ddlDepartment.Text = "";
        hfDepartment.Value = "";

        ddlDesignation.Text = "";
        hfDesignation.Value = "";

        ddlSalaryGrade.Text = "";
        hfSalaryGrade.Value = "";

        otherOpinion.Text = "";

        dtJoining.Text = "";
        LoadExitFormItem();
        txt_EmpName.Enabled = false;


    }

    private int SaveServayDetailInfo(List<ExitServayDetail> aList)
    {
        Int32 id = 0;

        foreach (var aDao in aList)
        {
            id = aFormDal.SaveExitServayDetail(aDao);
        }

        return id;
    }

    private List<ExitServayDetail> PrepareDataForServayDetail(int masterId)
    {
        List<ExitServayDetail> aDetails = new List<ExitServayDetail>();
        ExitServayDetail aDetail;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            aDetail = new ExitServayDetail();

            aDetail.MasterId = masterId;
            var dataKey = loadGridView.DataKeys[i];
            if (dataKey != null)
                aDetail.ServyItemId = Convert.ToInt32(Convert.ToInt32(dataKey.Value));

           var buttonList = (RadioButtonList)loadGridView.Rows[i].FindControl("rad_RatingScale");

           for (int j = 0; j < buttonList.Items.Count; j++)
            {
                if (buttonList.Items[j].Selected)
                {
                    aDetail.SarveyRatingValue = Convert.ToString(buttonList.SelectedValue);
                }
            }

            aDetails.Add(aDetail);
           
        }

        return aDetails;

    }

    private int SaveExitQsDetailInfo(List<ExitReasonDetailDao> aReasonDetailDaos)
    {
        Int32 id = 0;

        foreach (var aDao in aReasonDetailDaos)
        {
            id = aFormDal.SaveExitReasonDetail(aDao);
        }

        return id;
    }

    private List<ExitReasonDetailDao> PrepareDataForQsDetail(int masterId)
    {
        var aDaos = new List<ExitReasonDetailDao>();

        ExitReasonDetailDao aDao;

        for (int i = 0; i < exitQuestionRadioButtonList.Items.Count; i++)
        {
            if (exitQuestionRadioButtonList.Items[i].Selected)
            {
                aDao = new ExitReasonDetailDao();

                aDao.MasterId = masterId;
                aDao.ReasonId = Convert.ToInt32(exitQuestionRadioButtonList.Items[i].Value);

                aDaos.Add(aDao);
            }
        }

        return aDaos;
    }

    private ExitFormMasterDao PreaperDataForMasterSave()
    {
       var aMasterDao = new ExitFormMasterDao();

        aMasterDao.CompanyId = Convert.ToInt32(0);
        aMasterDao.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);
        aMasterDao.EmpCode = empCode.Text.Trim();
        aMasterDao.EmpName = empName.Text.Trim();
        aMasterDao.JoiningDate = Convert.ToDateTime(dtJoining.Text.Trim());
        aMasterDao.DepartmentId = Convert.ToInt32(hfDepartment.Value);
        aMasterDao.DesignationId = Convert.ToInt32(hfDesignation.Value);
        aMasterDao.DivisionId = Convert.ToInt32(hfDivision.Value); 
        aMasterDao.SalaryGradeId = Convert.ToInt32(hfSalaryGrade.Value);
        aMasterDao.OtherOpenion = otherOpinion.Text.Trim();

        aMasterDao.ActionStatus = "Posted";

        aMasterDao.EntryBy = Session["LoginName"].ToString();
        aMasterDao.EntryDate = DateTime.Now;

       return aMasterDao;
    }

    private bool SaveDataValidation()
    {
        //if (ddlCompany.SelectedValue == "")
        //{
        //    ShowMessageBox("You have to select a company !!!");
        //    return false;
        //}

        if (hfEmpInfoId.Value == "")
        {
            ShowMessageBox("Employee name is required !!!");
            return false;
        }


        int count = 0;

        for (int i = 0; i < exitQuestionRadioButtonList.Items.Count; i++)
        {
            if (exitQuestionRadioButtonList.Items[i].Selected)
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
            ShowMessageBox("You have to select at least one reason !!");
            return false;
        }

        

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            Int32 rowCount = 0;

            var rbl = (RadioButtonList)loadGridView.Rows[i].FindControl("rad_RatingScale");

            for (int j = 0; j < rbl.Items.Count; j++)
            {
                if (rbl.Items[j].Selected)
                {
                    rowCount ++;
                }

                if (rowCount > 0)
                {
                    break;
                }
            }

            if (rowCount == 0)
            {
                ShowMessageBox("You have to select the circle of the row no - " + (i + 1));
                return false;
            }
        }

        return true;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            txt_EmpName.Enabled = true;

            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
        }
        else
        {
            Session["CompanyId"] = "";
            txt_EmpName.Enabled = false;
        }

       
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
                LocationLabel.Text = dtEmp.Rows[0]["Location"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

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
    public void LoadData(string id)
    {
        SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = aFormDal.LoadEmployeeInfo(id);

            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

                ddlDepartment.Text = aTable.Rows[0].Field<string>("DepartmentName");
                hfDepartment.Value = aTable.Rows[0].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);

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
    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        //SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = aFormDal.LoadEmployeeInfo(hfEmpInfoId.Value);

            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

                ddlDepartment.Text = aTable.Rows[0].Field<string>("DepartmentName");
                hfDepartment.Value = aTable.Rows[0].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);

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

    private void LoadExitFormItem()
    {
        DataTable aTable = aFormDal.LoadExitQuestion();

        exitQuestionRadioButtonList.DataValueField = "ExitQuestionId";
        exitQuestionRadioButtonList.DataTextField = "Question";
        exitQuestionRadioButtonList.DataSource = aTable;
        exitQuestionRadioButtonList.DataBind();


        DataTable dt = aFormDal.LoadExitServayQuestion();

        loadGridView.DataSource = dt;
        loadGridView.DataBind();
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
            //txt_EmpName.Text = emp[2];
        }
        else
        {
            hfEmpInfoId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

        txt_EmpName.Text = "";
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("ExitInterviewFormView.aspx");
    }
}