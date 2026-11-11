using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class ExitManagement_UI_EmployeeJobLeftEntry : System.Web.UI.Page
{
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL= new EmployeeJobLeftEntryDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();
    tblEmployeePromotionEntryDAL atblEmployeePromotionEntryDAL = new tblEmployeePromotionEntryDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReadonlyDateTime();
            ButtonVisible();
            LoadDropDownList();
            //LoadBenifitInformation();
          

            if (Session["EmployeeJobLeftId"] != null)
            {
                EmployeeJobLeftIdHiddenField.Value = Session["EmployeeJobLeftId"].ToString();
                GetOneRecord(Session["EmployeeJobLeftId"].ToString());
                Session["EmployeeJobLeftId"] = null;
            }


            string id2 = Request.QueryString["id2"];
            if (Session["EmployeeJobLeftEdit"] != null)
            {
                EmployeeJobLeftIdHiddenField.Value = id2;
                Session["EmployeeJobLeftEdit"] = null;

                GetOneRecord(EmployeeJobLeftIdHiddenField.Value.ToString());


            }
        }
    }
    private void ReadonlyDateTime()
    {
        SubmissionDateTextBox.Attributes.Add("readonly", "readonly");
        JobLeftDateTextBox.Attributes.Add("readonly", "readonly");
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

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();


    protected void chkIsB_ReportingBody_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox dropDown = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        CheckBox chkIsB_ReportingBody = (CheckBox)loadGridView.Rows[rowindex].Cells[0].FindControl("chkIsB_ReportingBody");
        DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("ddlEmpInfoList");
        if (chkIsB_ReportingBody.Checked)
        {
            using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsBoard(companyDropDownList.SelectedValue.ToString()))
            {







                ddlEmpInfoList.DataSource = dt222;
                ddlEmpInfoList.DataValueField = "EmpInfoId";
                ddlEmpInfoList.DataTextField = "EmpName";
                ddlEmpInfoList.DataBind();
                ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfoList.SelectedIndex = 0;


            }
        }
        else
        {
            using (DataTable dt222 = _commonDataLoad.GetEmpDDLForEntry(companyDropDownList.SelectedValue.ToString()))
            {








                if (companyDropDownList.SelectedValue == "1")
                {
                    ddlEmpInfoList.DataSource = dt222;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                    ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfoList.SelectedIndex = 0;
                }



                if (companyDropDownList.SelectedValue == "2")
                {
                    DataTable dtcom2 = _commonDataLoad.GetEmpDDLForEntry2("");
                    ddlEmpInfoList.DataSource = dtcom2;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                    ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfoList.SelectedIndex = 0;
                }

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
            Response.Redirect("EmployeeJobLeftEntryView.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private void GetOneRecord(string idd)
    {

        submitButton.Text = "Update";
        submitButton.BackColor = Color.DodgerBlue;

        DataTable aDataTable = aEmployeeJobLeftEntryDAL.GetEmployeeJobLeftEntryById(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {

            SearchEmployeeNameTextBoxTextBox.Enabled = true;
           
            

            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();

            if (companyDropDownList.SelectedValue != "")
            {
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
            //  atblEmployeePromotionEntryDAL.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
            ddlEmpInfo.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId").ToString();

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
            
            ClearanceFormCheckBoxList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsClearanceForm"].ToString());
            ClearanceFormCheckBoxList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsExitInterview"].ToString());

            SearchEmployeeNameTextBoxTextBox.Text = aDataTable.Rows[0]["EmployeeName"].ToString();

            if (aDataTable.Rows[0]["AutoProcess"] != null)
            {
                manualUpdateCheckBox.Checked = true;
            }
            manualUpdateCheckBox.Enabled = false;

            //NewGradeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NGradeId").ToString();
            JobLeftTypeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("JobLeftTypeId").ToString();
            JobLeftTypeDropDownList_OnSelectedIndexChanged(null, null);



            try
            {
                JobLeftDateTextBox.Text =
                  aDataTable.Rows[rowIndex].Field<DateTime>("JobLeftDate").ToString("dd-MMM-yyyy");

                
            }
            catch (Exception)
            {

                JobLeftDateTextBox.Text = "";
            }



            try
            {
                SubmissionDateTextBox.Text =
                  aDataTable.Rows[rowIndex].Field<DateTime>("SubmissionDate").ToString("dd-MMM-yyyy");

                JobLeftDateTextBox_TextChanged(null, null);
            }
            catch (Exception)
            {

                SubmissionDateTextBox.Text = "";
            }
          
         



            ReasonTextBox.Text = aDataTable.Rows[rowIndex].Field<string>("Reason").ToString();

            
            for (int i = 0; i < itemGridView.Rows.Count; i++)
            {
                DataTable dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJobleftBenefitByBenefit(EmployeeJobLeftIdHiddenField.Value, itemGridView.DataKeys[i][0].ToString());
                if (dtdata.Rows.Count>0)
                {
                    ((CheckBox)itemGridView.Rows[i].FindControl("isValueCheckBox")).Checked = Convert.ToBoolean(dtdata.Rows[0]["Active"].ToString()); ;
                    ((RadioButtonList) itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[0].Selected =
                        Convert.ToBoolean(dtdata.Rows[0]["IsAddition"].ToString());
                    ((RadioButtonList) itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[1].Selected =
                        Convert.ToBoolean(dtdata.Rows[0]["IsDeduction"].ToString());
                    ((TextBox) itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text =
                        dtdata.Rows[0]["Amount"].ToString();
                }
            }


        }
        manualUpdateCheckBox.Checked = false;
    }

    private void LoadBenifitInformation(string param)
    {


        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadBenifitInformation(param);
        if (dtdata.Rows.Count > 0)
        {
            itemGridView.DataSource = dtdata;
            itemGridView.DataBind();
        }
    }
    private void LoadDataGetOneRecord(int id)
    {
       

        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {


           
        }
    }



    private void LoadDropDownList()
    {
        aEmployeeJobLeftEntryDAL.LoadCompanyDropDownList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        aEmployeeJobLeftEntryDAL.LoadJobLeftTypeDropDownList(JobLeftTypeDropDownList);
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeJobLeftEntryView.aspx");
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
       
        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            SearchEmployeeNameTextBoxTextBox.Enabled = true;




            using (DataTable dt222 = _commonDataLoad.GetEmpDDLOnlyView(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;



            }

        }
        else
        {
            SearchEmployeeNameTextBoxTextBox.Enabled = false;
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
                //productNameTextBox.Text = productInfo[1];
                //string productCode = productCodeTextBox.Text.Trim();
               // SearchEmployeeNameTextBoxTextBox.Text = "";
                if (isprobHiddenField.Value=="1" )
                {
                    if (typeHiddenField.Value=="1")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND PermProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value=="2")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND ContProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value=="3")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND CasualProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                }
                if (string.IsNullOrEmpty(isprobHiddenField.Value) || isprobHiddenField.Value=="0")
                {
                    if (typeHiddenField.Value == "1")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND PermConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value == "2")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND ContConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value == "3")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND CasualConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }  
                }
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
            SearchEmployeeNameTextBoxTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("please Select a Company !!", this);
            companyDropDownList.Focus();
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (EmployeeJobLeftIdHiddenField.Value == string.Empty)
        {
            DataTable aTable =
                            aEmployeeJobLeftEntryDAL.ValidattionForEffectiveDate(
                                   ddlEmpInfo.SelectedValue, JobLeftDateTextBox.Text);

            if (aTable.Rows.Count > 0)
            {
                aShowMessage.ShowMessageBox("Already Job Left!!!", this);
            }
            else
            {
                Save();
            }
        }
        

    }


    private void Update()
    {
        if (Validation())
        {
            EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

            aEmployeeJobLeftEntryDAO.EmployeeJobLeftId = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);



            aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

            aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
            //atblEmployeePromotionEntryDAO.PGradeId = Convert.ToInt32(PreviousGradeDropDownList.SelectedValue);
            aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(JobLeftTypeDropDownList.SelectedValue);
           
            aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
            aEmployeeJobLeftEntryDAO.Reason = Convert.ToString(ReasonTextBox.Text);


            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = true;

            }
            else
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = false;
            }


            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {

                aEmployeeJobLeftEntryDAO.IsExitInterview = true;
            }

            else
            {
                aEmployeeJobLeftEntryDAO.IsExitInterview = false;
            }
            if (SubmissionDateTextBox.Text != string.Empty)
            {
                aEmployeeJobLeftEntryDAO.SubmissionDate = Convert.ToDateTime(SubmissionDateTextBox.Text);
            }
            aEmployeeJobLeftEntryDAO.UpdateBy   = Convert.ToInt32(Session["UserId"]);
            aEmployeeJobLeftEntryDAO.UpdateDate = DateTime.Now;

            aEmployeeJobLeftEntryDAL.EmployeeJobLeftUpsateInfo(aEmployeeJobLeftEntryDAO);

            //For Employee Master Information update ------------------------------------------------------------------------

            if (manualUpdateCheckBox.Checked)
            {

                Int32 empGenId = 0;
                string reason = "";

                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                reason = Convert.ToString(JobLeftTypeDropDownList.SelectedItem.Text);

                UpdateEmployeeStepId(empGenId, reason);
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");
                    UpDateSuperVisorInfo(
                      Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()), (Convert.ToInt32(ddlEmpInfoList.SelectedValue)));

                }
            }

            //--------------------------------------------------------------------------------------------------------------

            int id = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);
            if (id > 0)
            {
                aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftBenefitById(id.ToString());
                for (int i = 0; i < itemGridView.Rows.Count; i++)
                {
                    EmployeeJobLeftEntryDAO aJobLeftEntryDao = new EmployeeJobLeftEntryDAO()
                    {
                        EmployeeJobLeftId = id,
                        BenefitId = Convert.ToInt32(itemGridView.DataKeys[i][0].ToString()),
                        Amount = !string.IsNullOrEmpty(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text) ? 0 : Convert.ToDecimal(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text),
                        Active = ((CheckBox)itemGridView.Rows[i].FindControl("isValueCheckBox")).Checked,
                        IsAddition = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[0].Selected,
                        IsDeduction = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[1].Selected,

                    };
                    aEmployeeJobLeftEntryDAL.EmployeePromotionBenefitEntrySaveInfo(aJobLeftEntryDao);
                }
            }


            if (manualUpdateCheckBox.Checked == false)
            {
                aEmployeeJobLeftEntryDAL.UpdateSelfApprove(id, false);
                if (Session["EmpInfoId"].ToString() != "")
                {
                    EmployeeJobLeftEntryDAO aMaster = new EmployeeJobLeftEntryDAO();
                    aMaster.EmployeeJobLeftId
                        = Convert.ToInt32(id);
                    aMaster.ActionStatus = "Verified";
                    bool status = aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);



                    int commentid = aEmployeeJobLeftEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                        " ");

                    EmployeeJobLeftAppLogDAO appLogDaoa = new EmployeeJobLeftAppLogDAO();

                    appLogDaoa.ActionStatus = "Drafted";
                    appLogDaoa.ApproveDate = DateTime.Now;
                    appLogDaoa.ApproveBy = Session["UserId"].ToString();
                    appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDaoa.EmployeeJobLeftId = id;
                    appLogDaoa.Comments = "";
                    appLogDaoa.CommentsId = commentid;

                    int idd = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDaoa);


                    DataTable dtempdata =
                        aEmployeeJobLeftEntryDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                            "'");
                    EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                    {
                        appLogDao.ActionStatus = "Verified";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                        appLogDao.Comments = " ";
                        appLogDao.CommentsId = commentid;

                    }
                    ;
                    int iddddd = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                    aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Separation Approval ", @"  <br/> Dear Sir, <br/>
A Employee Separation is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Updated Successfully...');window.location ='EmployeeJobLeftEntryView.aspx';",
                   true);
        }



    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }


    public void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {

            DataTable dtadata = aEmpTransferAndRedesignationDal.LoadSuperviseEmployeeActive(id.ToString());
          

            loadGridView.DataSource = dtadata;
            loadGridView.DataBind();

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");
                using (DataTable dt222 = _commonDataLoad.GetEmpDDLForEntry(companyDropDownList.SelectedValue.ToString()))
                {








                    if (companyDropDownList.SelectedValue == "1")
                    {
                        ddlEmpInfoList.DataSource = dt222;
                        ddlEmpInfoList.DataValueField = "EmpInfoId";
                        ddlEmpInfoList.DataTextField = "EmpName";
                        ddlEmpInfoList.DataBind();
                        ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpInfoList.SelectedIndex = 0;
                    }



                    if (companyDropDownList.SelectedValue == "2")
                    {
                        DataTable dtcom2 = _commonDataLoad.GetEmpDDLForEntry2("");
                        ddlEmpInfoList.DataSource = dtcom2;
                        ddlEmpInfoList.DataValueField = "EmpInfoId";
                        ddlEmpInfoList.DataTextField = "EmpName";
                        ddlEmpInfoList.DataBind();
                        ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpInfoList.SelectedIndex = 0;
                    }

                }
            }
            //EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
            //DesignationTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            //JoiningDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            //SalaryGradeTextBox.Text = dtdata.Rows[0]["GradeName"].ToString();


            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();

            lblComName.Text = dtdata.Rows[0]["CompanyName"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
            typeHiddenField.Value = dtdata.Rows[0]["EmpTypeId"].ToString();
            isprobHiddenField.Value = dtdata.Rows[0]["IsProbationary"].ToString();
            cateHiddenField.Value = dtdata.Rows[0]["EmpCategoryId"].ToString();
            gradeHiddenField.Value = dtdata.Rows[0]["SalaryGradeId"].ToString();
            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = dtdata.Rows[0]["SalaryGrade"].ToString();
            lblDivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
            lblWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = dtdata.Rows[0]["EmployeeMentType"].ToString();


        }
    }


    private bool Validation()
    {
        //if (EmployeeDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Select Employe Search !!!", this);
        //    EmployeeDropDownList.Focus();
        //    return false;
        //}


        //if (NewGradeDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Select New Grade !!!", this);
        //    NewGradeDropDownList.Focus();
        //    return false;
        //}

        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select New Designation !!!", this);
            companyDropDownList.Focus();
            return false;
        }


      

       

        if (JobLeftTypeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
            JobLeftTypeDropDownList.Focus();
            return false;
        }

        if (ddlEmpInfo.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Employee  !!!", this);
            ddlEmpInfo.Focus();
            return false;
        }

        if (JobLeftDateTextBox.Text == String.Empty)
        {
            aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
            JobLeftDateTextBox.Focus();
            return false;
        }



        if (chkIsSubmissionDate.Checked)
        {
            if (SubmissionDateTextBox.Text == String.Empty)
            {
                aShowMessage.ShowMessageBox("Please Select Submission Date !!!", this);
                SubmissionDateTextBox.Focus();
                return false;
            }
        }

        if (loadGridView.Rows.Count >0)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");

                if (ddlEmpInfoList.SelectedValue=="")
                {
                    aShowMessage.ShowMessageBox("Directly Supervised Employee List must be Select !!!", this);
                   ddlEmpInfoList.Focus();
                    return false;
                }
            }
        }
        else
        {
          //  aShowMessage.ShowMessageBox("Directly Supervised Employee List must be Empty !!!", this);
          ////  JobLeftDateTextBox.Focus();
          //  return false;
        }
        

        return true;
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
    public void Save()
    {
        if (Validation())
        {

            EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

            aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(ddlEmpInfo.SelectedValue);

            aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(JobLeftTypeDropDownList.SelectedValue);
          
            // atblEmployeePromotionEntryDAO.PSetpId = Convert.ToInt32(PreviousStepDropDownList.SelectedValue);
            // atblEmployeePromotionEntryDAO.PRepEmpId = Convert.ToInt32(PReportingBodyDropDownList.SelectedValue);

            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = true;
               
            }
            else
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = false; 
            }


            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {
                
                aEmployeeJobLeftEntryDAO.IsExitInterview = true;
            }
           
              else
            {
                aEmployeeJobLeftEntryDAO.IsExitInterview = false;
            }
             
            aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
            aEmployeeJobLeftEntryDAO.Reason = Convert.ToString(ReasonTextBox.Text);

            if (SubmissionDateTextBox.Text!=string.Empty)
            {
                aEmployeeJobLeftEntryDAO.SubmissionDate = Convert.ToDateTime(SubmissionDateTextBox.Text);
            }
         

            if(manualUpdateCheckBox.Checked)
            {
                aEmployeeJobLeftEntryDAO.AutoProcess = "Manually Updated";
            }

            aEmployeeJobLeftEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
            aEmployeeJobLeftEntryDAO.EntryDate = DateTime.Now;

            int id=aEmployeeJobLeftEntryDAL.EmployeePromotionEntrySaveInfo(aEmployeeJobLeftEntryDAO);

            //For Employee Master Information update ------------------------------------------------------------------------

            if (manualUpdateCheckBox.Checked)
            {

                Int32 empGenId = 0;
                string reason = "";

                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                reason = Convert.ToString(JobLeftTypeDropDownList.SelectedItem.Text);

                UpdateEmployeeStepId(empGenId, reason);

                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");


                    UpDateSuperVisorInfo(
                       Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()),(Convert.ToInt32(ddlEmpInfoList.SelectedValue)));
                     
                   
                }

            }

            //--------------------------------------------------------------------------------------------------------------

            
         
                if (id > 0)
                {  
                    if (manualUpdateCheckBox.Checked==false)
            {
                aEmployeeJobLeftEntryDAL.UpdateSelfApprove(id, false);
                    if (Session["EmpInfoId"].ToString() != "")
                    {
                        DataTable dtempdata =
                          aEmployeeJobLeftEntryDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                              "'");

                        if (dtempdata.Rows.Count>0)
                        {
                            EmployeeJobLeftEntryDAO aMaster = new EmployeeJobLeftEntryDAO();
                            aMaster.EmployeeJobLeftId
                                = Convert.ToInt32(id);
                            aMaster.ActionStatus = "Verified";
                            bool status = aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);



                            int commentid = aEmployeeJobLeftEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                                " ");

                            EmployeeJobLeftAppLogDAO appLogDaoa = new EmployeeJobLeftAppLogDAO();

                            appLogDaoa.ActionStatus = "Drafted";
                            appLogDaoa.ApproveDate = DateTime.Now;
                            appLogDaoa.ApproveBy = Session["UserId"].ToString();
                            appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDaoa.EmployeeJobLeftId = id;
                            appLogDaoa.Comments = "";
                            appLogDaoa.CommentsId = commentid;

                            int idd = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDaoa);



                            EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                            {
                                appLogDao.ActionStatus = "Verified";
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                                appLogDao.Comments = " ";
                                appLogDao.CommentsId = commentid;

                            }
                            ;
                            int iddddd = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                            aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);

                            SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Separation Approval ", @"  <br/> Dear Sir, <br/>
A Employee Separation is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                        }

                      
                    }
                }
                aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftBenefitById(id.ToString());
                for (int i = 0; i < itemGridView.Rows.Count ; i++)
                {
                    EmployeeJobLeftEntryDAO aJobLeftEntryDao = new EmployeeJobLeftEntryDAO()
                    {
                        EmployeeJobLeftId = id,
                        BenefitId = Convert.ToInt32(itemGridView.DataKeys[i][0].ToString()),
                        Amount = !string.IsNullOrEmpty(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text) ? 0 : Convert.ToDecimal(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text),
                        Active = ((CheckBox)itemGridView.Rows[i].FindControl("isValueCheckBox")).Checked,
                        IsAddition = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[0].Selected,
                        IsDeduction = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[1].Selected,

                    };
                    aEmployeeJobLeftEntryDAL.EmployeePromotionBenefitEntrySaveInfo(aJobLeftEntryDao);
                }
            }
            

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Data Saved Successfully...');window.location ='EmployeeJobLeftEntryView.aspx';",
                  true);
            

        }
    }

    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }

    private void UpdateEmployeeStepId(int empGenId, string reason)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.InactiveReason = reason;
        aInfo.IsActive = false;
        aInfo.EmployeeStatus = "InActive";
        aInfo.EmpInfoId = empGenId;

        aEmployeeJobLeftEntryDAL.UpdateEmployeeExitInfo(aInfo);

    }
  

    private void Clear()
    {
        companyDropDownList.SelectedValue = "";
        SearchEmployeeNameTextBoxTextBox.Text = string.Empty;
        //EmployeeNameTextBox.Text = "";
        //JoiningDateTextBox.Text = string.Empty;
        //DesignationTextBox.Text = "";
        //SalaryGradeTextBox.Text = "";
        ReasonTextBox.Text = "";
        JobLeftDateTextBox.Text = "";
        JobLeftTypeDropDownList.SelectedValue = "";
        ClearanceFormCheckBoxList.Items[0].Selected = false;
        ClearanceFormCheckBoxList.Items[1].Selected = false;
        SubmissionDateTextBox.Text = "";
        lblComName.Text = "";
        lblDepartment.Text = "";
        lblDesignation.Text = "";
        lblDivision.Text = "";
        lblEmp.Text = "";
        lblEmployeeCode.Text = "";
        
        lblJdate.Text = "";
        lblSalaryGrade.Text = "";
        lblSection.Text = "";
        lblSubSection.Text = "";
        lblWing.Text = "";
        
    }

    protected void JobLeftDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (JobLeftDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(JobLeftDateTextBox.Text);
                DateTime d = Convert.ToDateTime(SubmissionDateTextBox.Text);
                DateTime c = Convert.ToDateTime(JobLeftDateTextBox.Text);
                DateTime.Parse(SubmissionDateTextBox.Text);
                TimeSpan ts = c-d;

                // Difference in days.

                int differenceInDays = ts.Days; // This is in int
                double differenceInDaysd = ts.TotalDays;
                DurationDateTextBox.Text = differenceInDaysd.ToString();
            }
            catch
            {
                JobLeftDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void SubmissionDateTextBox_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (EmployeeJobLeftIdHiddenField.Value == string.Empty)
        {
           // Save();
        }
        else
        {
               DataTable aTable =
                             aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(EmployeeJobLeftIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
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

        if (EmployeeJobLeftIdHiddenField.Value != string.Empty)
        {
             DataTable aTable =
                             aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(EmployeeJobLeftIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    Delete();
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
                }
            }
        }
      
    }


    private void Delete()
    {
        EmployeeJobLeftEntryDAO aJobCreationDao = new EmployeeJobLeftEntryDAO();


        aJobCreationDao.EmployeeJobLeftId = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);

        aJobCreationDao.IsDelete = true;


        aJobCreationDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aJobCreationDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftById(aJobCreationDao);

        if (status)
        {

            ResetEmpGeneralInfo(aJobCreationDao.EmployeeJobLeftId);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='EmployeeJobLeftEntryView.aspx';",
              true);
        }
    }

    private void ResetEmpGeneralInfo(int jobLeftId)
    {
        DataTable aTable = aEmployeeJobLeftEntryDAL.FetchEmployeeInfoById(jobLeftId);

        if (aTable.Rows.Count > 0)
        {
            Int32 employeeId = aTable.Rows[0].Field<Int32>("EmployeeId");

            EmpGeneralInfo aInfo = new EmpGeneralInfo();

            aInfo.InactiveReason = null;
            aInfo.IsActive = true;
            aInfo.EmployeeStatus = "Active";
            aInfo.EmpInfoId = employeeId;

            aEmployeeJobLeftEntryDAL.UpdateEmployeeExitInfo(aInfo);
            
        }
    }
    JobLeftTypeDAL aVaencyEntryDaL = new JobLeftTypeDAL();
    protected void JobLeftTypeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(JobLeftTypeDropDownList.SelectedValue);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            try
            {
                if (dataTable.Rows[rowIndex].Field<bool>("IsSubmissionDate"))
                {

                    chkIsSubmissionDate.Checked = true;

                }
                else
                {
                    chkIsSubmissionDate.Checked = false;
                }
            }
            catch (Exception)
            {

                chkIsSubmissionDate.Checked = false;
            }
        }
    }

    protected void ddlEmpInfo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData(Convert.ToInt32(ddlEmpInfo.SelectedValue));
        //productNameTextBox.Text = productInfo[1];
        //string productCode = productCodeTextBox.Text.Trim();
        // SearchEmployeeNameTextBoxTextBox.Text = "";
        if (isprobHiddenField.Value == "1")
        {
            if (typeHiddenField.Value == "1")
            {
                LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND PermProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
            }
            if (typeHiddenField.Value == "2")
            {
                LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND ContProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
            }
            if (typeHiddenField.Value == "3")
            {
                LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND CasualProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
            }
        }
        if (string.IsNullOrEmpty(isprobHiddenField.Value) || isprobHiddenField.Value == "0")
        {
            if (typeHiddenField.Value == "1")
            {
                LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND PermConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
            }
            if (typeHiddenField.Value == "2")
            {
                LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND ContConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
            }
            if (typeHiddenField.Value == "3")
            {
                LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND CasualConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
            }
        }
    }
}