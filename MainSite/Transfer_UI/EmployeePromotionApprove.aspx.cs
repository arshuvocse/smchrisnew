using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class Transfer_UI_EmployeePromotionApprove : System.Web.UI.Page
{
    tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
    tblEmployeePromotionEntryDAL atblEmployeePromotionEntryDAL = new tblEmployeePromotionEntryDAL();

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
             //   ButtonVisible();
                SearchViewPanel.Visible = true;
                ShowExistingAndNew.Visible = true;
                LoadDropDownList();

                if (Session["EmployeePromotionEntryId"] != null)
                {
                    EmployeePromotionEntryIdHiddenField.Value = Session["EmployeePromotionEntryId"].ToString();
                    GetOneRecord(Session["EmployeePromotionEntryId"].ToString());
                Session["EmployeePromotionEntryId"] = null;
                }
                else
                {
                  Response.Redirect("EmployeePromotionApprovalList.aspx");
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
    }

    private void GetOneRecord(string idd)
    {



        DataTable aDataTable = atblEmployeePromotionEntryDAL.GetEmployeePromotionEntryIdByIdApp(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {


            bool isselfapp = Convert.ToBoolean(aDataTable.Rows[0]["IsSelfApp"].ToString());
            if (isselfapp)
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["EmpInfoId"].ToString();
            }
            else
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["UserEmpInfoId"].ToString();
            }



            actionstatusHiddenField.Value = aDataTable.Rows[0].Field<String>("ActionStatus").ToString();
            
            EmployeePromotionEntryIdHiddenField.Value = Session["EmployeePromotionEntryId"].ToString();
            ShowExistingAndNew.Visible = true;
            SearchViewPanel.Visible = true;

            DataTable dtdata =
                atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationDS(
                    EmployeePromotionEntryIdHiddenField.Value);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();
         
            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();

            lblCompany.Text = companyDropDownList.SelectedItem.Text;

            if (aDataTable.Rows[0]["AutoProcess"] != null)
            {
                manualUpdateCheckBox.Checked = true;
            }
            //aDataTable.Rows[0]["IsReappointment"] = Chkreappointment.Checked;

            try
            {
                if (Convert.ToBoolean(aDataTable.Rows[0]["IsReappointment"]) == true)
                {
                    Chkreappointment.Checked = true;
                    lblReappointment.Text = "Yes";
                }

                if (Convert.ToBoolean(aDataTable.Rows[0]["IsReappointment"]) == false)
                {
                    Chkreappointment.Checked = false;
                    lblReappointment.Text = "No";

                }
            }
            catch (Exception)
            {
                
                //throw;
            }

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

            DataTable dtadata = atblEmployeePromotionEntryDAL.LoadSuperviseEmployee(repEmpIdHiddenField.Value.ToString());
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
            lblOffice.Text = aDataTable.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = aDataTable.Rows[0]["Location"].ToString();
            SearchEmployeeNameTextBoxTextBox.Text = "";
            FinancialYearDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("FinancialYearId").ToString();

            lblFinancialYearDesc.Text = FinancialYearDropDownList.SelectedItem.Text;            NewReportingBodyTextBox.Text = aDataTable.Rows[0]["NewReportingEmployeeName"].ToString();
            atblEmployeePromotionEntryDAL.LoadOlddesignationDropDownList(PreviousDesignationDropDownList);
            atblEmployeePromotionEntryDAL.LoadPreSalaryGradeDropDownList(PreviouSalaryGradeDropDownList);

            if (aDataTable.Rows[0]["NSalGradeId"] != DBNull.Value)
            {

                NewSalaryGradeDropDownList.SelectedValue =
                    aDataTable.Rows[rowIndex].Field<Int32>("NSalGradeId").ToString();
                lblNSalaryGrade.Text = NewSalaryGradeDropDownList.SelectedItem.Text;
            }
            //atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownListBySalaryId(NewDesignationDropDownList, NewSalaryGradeDropDownList.SelectedValue);
            NewDesignationDropDownList.SelectedValue = string.IsNullOrEmpty(aDataTable.Rows[rowIndex]["NDesignationId"].ToString()) ? "0" : aDataTable.Rows[rowIndex]["NDesignationId"].ToString();
            lblNDesignation.Text = NewDesignationDropDownList.SelectedItem.Text;

            try
            {
                PromotionTypeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NPromoTypeId").ToString();
                lblPromotionType.Text = PromotionTypeDropDownList.SelectedItem.Text;
            }
            catch (Exception)
            {
                PromotionTypeDropDownList.SelectedValue = "0";
                //throw;
            }

            OtherRemarksTextBox.Text = aDataTable.Rows[rowIndex].Field<string>("Remarks").ToString();
            lblRemarks.Text = OtherRemarksTextBox.Text;
            atblEmployeePromotionEntryDAL.EmployeeNameDropDown(PReportingBodyDropDownList, companyDropDownList.SelectedValue);
            if (aDataTable.Rows[0]["PDesignationId"] != DBNull.Value)
            {
                PreviousDesignationDropDownList.SelectedValue =
                    aDataTable.Rows[rowIndex].Field<Int32>("PDesignationId").ToString();
                lblPDesignation.Text = PreviousDesignationDropDownList.SelectedItem.Text;
            }

            
            
            if (aDataTable.Rows[0]["PSalGradeId"] != DBNull.Value)
            {
                PreviouSalaryGradeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("PSalGradeId").ToString();
                lblPSalaryGrade.Text = PreviouSalaryGradeDropDownList.SelectedItem.Text;
            }
            if (aDataTable.Rows[0]["PRepEmpId"] != DBNull.Value)
            {
                PReportingBodyDropDownList.SelectedValue =
                    aDataTable.Rows[rowIndex].Field<Int32>("PRepEmpId").ToString();
                lblPReportingBody.Text = PReportingBodyDropDownList.SelectedItem.Text;
            }
            if (aDataTable.Rows[0]["NRepEmpId"] != DBNull.Value)
            {
                HiddenFieldNewReportingBody.Value = aDataTable.Rows[rowIndex].Field<Int32>("NRepEmpId").ToString();
            }

            atblEmployeePromotionEntryDAL.LoadSalaryStepDropDownListBySalaryGradeId(NstepDropDownList, NewSalaryGradeDropDownList.SelectedValue);
            if (aDataTable.Rows[0]["NSalaryStepId"] != DBNull.Value)
            {
                NstepDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NSalaryStepId").ToString();
                lblNSalaryStep.Text = NstepDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["PStepId"] != DBNull.Value)
            {
                PreviouSalaryStepDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("PStepId").ToString();
                lblPSalaryStep.Text = PreviouSalaryStepDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["Effectivedate"] != DBNull.Value)
            {

                ActiveDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("Effectivedate")
                    .ToString("dd-MMM-yyyy");

                lblEffDate.Text = ActiveDateTextBox.Text;
            }


            DataTable AppLogComm = atblEmployeePromotionEntryDAL.GetAppLogCommByJobId(Convert.ToInt32(idd));

            if (AppLogComm.Rows.Count > 0)
            {

                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
            }
            else
            {
                AppLogCommentGridView.DataSource = null;
                AppLogCommentGridView.DataBind();
            }
            RadioTextValue();
        }

    }
    private void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            dtdata = atblEmployeePromotionEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = atblEmployeePromotionEntryDAL.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["EmpInfoId"].ToString() != Session["ForEmpInfoId"].ToString())


        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();


        if (actionstatusHiddenField.Value == "Approved")
        {
            submitButton.Visible = false;
            LinkButton1.Visible = true;
        }
        else
        {
            submitButton.Visible = true;
            LinkButton1.Visible = false;
        }
    }


    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        tblEmployeePromotionEntryDAO aMaster = new tblEmployeePromotionEntryDAO();
        aMaster.EmployeePromotionEntryId
            = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = atblEmployeePromotionEntryDAL.UpdateContractural(aMaster);
        int commentid = atblEmployeePromotionEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);
            atblEmployeePromotionEntryDAL.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='IncrementApprovalView.aspx';",
                   true);
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
                PReportingBodyDropDownList.SelectedValue = "";
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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionApprovalList.aspx");
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

                    tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
                    atblEmployeePromotionEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                    atblEmployeePromotionEntryDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                    atblEmployeePromotionEntryDAO.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
                    atblEmployeePromotionEntryDAO.Effectivedate = Convert.ToDateTime(ActiveDateTextBox.Text);
                    atblEmployeePromotionEntryDAO.NEntryDate = DateTime.Now;
                    atblEmployeePromotionEntryDAO.IsReappointment = Chkreappointment.Checked;


                    if (HFDivID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.DivisionId =
                            Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
                    }

                    if (HFDivWingId.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.DivisionWId =
                            Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
                    }

                    if (HFDeptID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.DepartmentId =
                            Convert.ToInt32(HFDeptID.Value) > 0 ? int.Parse(HFDeptID.Value) : (int?)null;
                    }
                    if (HFSecID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.SectionId =
                            Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
                    }

                    if (HFSubSecID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.SubSectionId =
                            Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
                    }

                    atblEmployeePromotionEntryDAO.EmployeeCode =
                        Convert.ToInt32(HFEmpCode.Value) > 0 ? int.Parse(HFEmpCode.Value) : (int?)null;

                    if (HFEmpTypeID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.EmpTypeId =
                            Convert.ToInt32(HFEmpTypeID.Value) > 0 ? int.Parse(HFEmpTypeID.Value) : (int?)null;
                    }


                    if (HFSalLocID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.SalaryLoationId =
                            Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
                    }

                    if (HFJobLocID.Value != "")
                    {
                        atblEmployeePromotionEntryDAO.JobLocationId =
                            Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
                    }




                    atblEmployeePromotionEntryDAO.PDesignationId =
                        PreviousDesignationDropDownList.SelectedIndex > 0
                            ? int.Parse(PreviousDesignationDropDownList.SelectedValue)
                            : (int?)null;

                    atblEmployeePromotionEntryDAO.PSalGradeId = Convert.ToInt32(PreviouSalaryGradeDropDownList.SelectedValue);
                    //  PreviouSalaryGradeDropDownList.SelectedIndex > 0
                    //    ? int.Parse(PreviouSalaryGradeDropDownList.SelectedValue)
                    //   : (int?) null;

                    atblEmployeePromotionEntryDAO.PStepId =
                        PreviouSalaryStepDropDownList.SelectedIndex > 0
                            ? int.Parse(PreviouSalaryStepDropDownList.SelectedValue)
                            : (int?)null;

                    atblEmployeePromotionEntryDAO.NSalaryStepId =
                       NewSalaryGradeDropDownList.SelectedIndex > 0
                           ? int.Parse(NstepDropDownList.SelectedValue)
                           : (int?)null;



                    atblEmployeePromotionEntryDAO.NDesignationId =
                        NewDesignationDropDownList.SelectedIndex > 0
                            ? int.Parse(NewDesignationDropDownList.SelectedValue)
                            : (int?)null;

                    atblEmployeePromotionEntryDAO.NSalGradeId =
                        NewSalaryGradeDropDownList.SelectedIndex > 0
                            ? int.Parse(NewSalaryGradeDropDownList.SelectedValue)
                            : (int?)null;
                    atblEmployeePromotionEntryDAO.NPromoTypeId =
                        PromotionTypeDropDownList.SelectedIndex > 0
                            ? int.Parse(PromotionTypeDropDownList.SelectedValue)
                            : (int?)null;


                    atblEmployeePromotionEntryDAO.PRepEmpId =
                        PReportingBodyDropDownList.SelectedIndex > 0
                            ? int.Parse(PReportingBodyDropDownList.SelectedValue)
                            : (int?)null;

                    if (HiddenFieldNewReportingBody.Value!="")
                    {
                        
                   
                    atblEmployeePromotionEntryDAO.NRepEmpId = //Convert.ToInt32(HiddenFieldNewReportingBody.Value);
                        Convert.ToInt32(HiddenFieldNewReportingBody.Value) > 0
                            ? int.Parse(HiddenFieldNewReportingBody.Value)
                            : (int?)null;
 }
                    atblEmployeePromotionEntryDAO.Remarks = Convert.ToString(OtherRemarksTextBox.Text);
                    atblEmployeePromotionEntryDAO.EntryBy = Session["UserId"].ToString();
                    atblEmployeePromotionEntryDAO.EntryDate = DateTime.Now;
                    atblEmployeePromotionEntryDAO.IsPromotion = false;

                    if (manualUpdateCheckBox.Checked)
                    {
                        atblEmployeePromotionEntryDAO.AutoProcess = "Manually Updated";
                    }

                    int id = atblEmployeePromotionEntryDAL.EmployeePromotionEntrySaveInfo(atblEmployeePromotionEntryDAO);


                    //For Employee Master Information update ------------------------------------------------------------------------

                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                        Int32 salaryGradeId = 0;
                        Int32 salaryStepId = 0;
                        Int32 desigId = 0;
                        Int32 reportingBodyId = 0;

                        empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        salaryGradeId = Convert.ToInt32(NewSalaryGradeDropDownList.SelectedIndex > 0
                            ? int.Parse(NewSalaryGradeDropDownList.SelectedValue)
                            : (int?)null);

                        salaryStepId = Convert.ToInt32(NewSalaryGradeDropDownList.SelectedIndex > 0
                           ? int.Parse(NstepDropDownList.SelectedValue)
                           : (int?)null);
                        desigId = Convert.ToInt32(NewDesignationDropDownList.SelectedIndex > 0
                            ? int.Parse(NewDesignationDropDownList.SelectedValue)
                            : (int?)null);

                        reportingBodyId = Convert.ToInt32(Convert.ToInt32(HiddenFieldNewReportingBody.Value) > 0
                            ? int.Parse(HiddenFieldNewReportingBody.Value)
                            : (int?)null);


                        UpdateEmployeeStepId(empGenId, salaryGradeId, salaryStepId, desigId, reportingBodyId);
                    }

                    //--------------------------------------------------------------------------------------------------------------



                    atblEmployeePromotionEntryDAL.DeleteDirectlyS(id.ToString());

                    for (int i = 0; i < loadGridView.Rows.Count; i++)
                    {
                        EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao();

                        andRedesignationDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                        andRedesignationDao.EmpTransferAndRedesignationId = id;

                        if (Convert.ToInt32(loadGridView.DataKeys[i][1].ToString()) != 0)
                        {
                            andRedesignationDao.PrevEmpReportingBodyId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                        }


                        //DataTable prevRPTBody = atblEmployeePromotionEntryDAL.GetEmployeeReportingBodyInfo(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()));

                        //if (prevRPTBody.Rows.Count > 0)
                        //{

                        //    if (prevRPTBody.Rows[0]["ReportingEmpId"] != DBNull.Value)
                        //    {
                        //        andRedesignationDao.PrevEmpReportingBodyId = Convert.ToInt32(prevRPTBody.Rows[0]["ReportingEmpId"]);
                        //    }

                        //}

                        int idd =
                            atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationDSSaveInfo(
                                andRedesignationDao);

                        //------------------------------------- Update supervised info -------------------------------------------------------------------------

                        if (manualUpdateCheckBox.Checked)
                        {
                            UpDateSuperVisorInfo(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()), Convert.ToInt32(repEmpIdHiddenField.Value));
                        }

                    }
                    atblEmployeePromotionEntryDAL.DeletePS(id.ToString());
                    for (int i = 0; i < presuperGridView.Rows.Count; i++)
                    {
                        EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao()
                        {
                            EmpTransferAndRedesignationId = id,
                            EmpInfoId = Convert.ToInt32(presuperGridView.DataKeys[i][0].ToString())
                        };
                        int idd =
                            atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationPSSaveInfo(
                                andRedesignationDao);

                        //------------------------------------- Update supervised info -------------------------------------------------------------------------

                        if (manualUpdateCheckBox.Checked)
                        {
                            UpDateSuperVisorInfoByNULL(Convert.ToInt32(presuperGridView.DataKeys[i][0].ToString()));
                        }
                    }

                    if (Chkreappointment.Checked)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Add";
                        Session["comID"] = "";
                        Session["FinYear"] = "";
                        Session["EffDate"] = "";
                        Session["EmpId"] = "";
                        Session["PromotionToTransfer"] = "";
                        Session["comID"] = companyDropDownList.SelectedValue;
                        Session["FinYear"] = FinancialYearDropDownList.SelectedValue;
                        Session["EffDate"] = ActiveDateTextBox.Text;
                        Session["EmpId"] = repEmpIdHiddenField.Value;

                        Session["PromotionToTransfer"] = "PromotionToTransfersession";
                      


                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation successfully done...');window.location ='EmpTransferAndRedesignation.aspx';",
                     true);
 
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation successfully done...');window.location ='EmployeePromotionEntryView.aspx';",
                      true);
                    }
                  
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

    private void UpdateEmployeeStepId(int empGenId, int salaryGradeId, int salaryStepId, int desigId, int reportingBodyId)
    {
        //EmpGeneralInfo aInfo = new EmpGeneralInfo();

        //aInfo.EmpGradeId = salaryGradeId;
        //aInfo.SalScaleId = salaryStepId;
        //aInfo.DesigId = desigId;
        //aInfo.LineId = reportingBodyId;
        //aInfo.EmpInfoId = empGenId;

        atblEmployeePromotionEntryDAL.UpdateEmployeeExitInfo(empGenId, salaryGradeId, salaryStepId, desigId, reportingBodyId);

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


        if (NewSalaryGradeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select New Salary Grade !!!", this);
            NewSalaryGradeDropDownList.Focus();
            return false;
        }

        if (NstepDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select New Salary Step !!!", this);
            NstepDropDownList.Focus();
            return false;
        }

        if (HiddenFieldNewReportingBody.Value=="")
        {
            aShowMessage.ShowMessageBox("Please Select New Reporting Body !!!", this);
            NewReportingBodyTextBox.Focus();
            return false;
        }
       
        

        if (ActiveDateTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Enter Effective Date !!!", this);
            ActiveDateTextBox.Focus();
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
                tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();

                atblEmployeePromotionEntryDAO.EmployeePromotionEntryId = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value);




                atblEmployeePromotionEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                atblEmployeePromotionEntryDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                atblEmployeePromotionEntryDAO.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
                atblEmployeePromotionEntryDAO.Effectivedate = Convert.ToDateTime(ActiveDateTextBox.Text);
                atblEmployeePromotionEntryDAO.NEntryDate = DateTime.Now;
                atblEmployeePromotionEntryDAO.IsReappointment = Chkreappointment.Checked;
                atblEmployeePromotionEntryDAO.NSalaryStepId =
                           NewSalaryGradeDropDownList.SelectedIndex > 0
                               ? int.Parse(NstepDropDownList.SelectedValue)
                               : (int?)null;
                if (PreviousDesignationDropDownList.SelectedIndex > 0)
                {
                    atblEmployeePromotionEntryDAO.PDesignationId =
                        Convert.ToInt32(PreviousDesignationDropDownList.SelectedValue);
                }

                atblEmployeePromotionEntryDAO.IsReappointment = Chkreappointment.Checked;
                if (PreviouSalaryGradeDropDownList.SelectedIndex > 0)
                {
                    atblEmployeePromotionEntryDAO.PSalGradeId = Convert.ToInt32(PreviouSalaryGradeDropDownList.SelectedValue);
                }
                if (PreviouSalaryStepDropDownList.SelectedIndex > 0)
                {
                    atblEmployeePromotionEntryDAO.PStepId = Convert.ToInt32(PreviouSalaryStepDropDownList.SelectedValue);
                }
                if (NewDesignationDropDownList.SelectedIndex > 0)
                {
                    atblEmployeePromotionEntryDAO.NDesignationId = Convert.ToInt32(NewDesignationDropDownList.SelectedValue);
                }
                atblEmployeePromotionEntryDAO.NSalGradeId = Convert.ToInt32(NewSalaryGradeDropDownList.SelectedValue);
                atblEmployeePromotionEntryDAO.NPromoTypeId = Convert.ToInt32(PromotionTypeDropDownList.SelectedValue);
                try
                {
                    atblEmployeePromotionEntryDAO.PRepEmpId = Convert.ToInt32(PReportingBodyDropDownList.SelectedValue);
                }
                catch (Exception)
                {

                    atblEmployeePromotionEntryDAO.PRepEmpId = Convert.ToInt32(0);
                }
                if (HFSalLocID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.SalaryLoationId =
                        Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
                }

                if (HFJobLocID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.JobLocationId =
                        Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
                }

                atblEmployeePromotionEntryDAO.NRepEmpId = Convert.ToInt32(HiddenFieldNewReportingBody.Value);
                atblEmployeePromotionEntryDAO.Remarks = Convert.ToString(OtherRemarksTextBox.Text);
                atblEmployeePromotionEntryDAO.Effectivedate = Convert.ToDateTime(ActiveDateTextBox.Text);



                atblEmployeePromotionEntryDAO.UpdateBy = Session["UserId"].ToString();
                atblEmployeePromotionEntryDAO.UpdateDate = DateTime.Now;
                if (HFDesgId.Value != "")
                {
                    atblEmployeePromotionEntryDAO.DesignationId =
                        Convert.ToInt32(HFDesgId.Value) > 0 ? int.Parse(HFDesgId.Value) : (int?)null;
                }

                if (HFDivID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.DivisionId =
                        Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
                }

                if (HFDivWingId.Value != "")
                {
                    atblEmployeePromotionEntryDAO.DivisionWId =
           Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
                }

                if (HFDeptID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.DepartmentId =
                        Convert.ToInt32(HFDeptID.Value) > 0 ? int.Parse(HFDeptID.Value) : (int?)null;
                }
                if (HFSecID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.SectionId =
                        Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
                }

                if (HFSubSecID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.SubSectionId =
                        Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
                }

                atblEmployeePromotionEntryDAO.EmployeeCode =
                 Convert.ToInt32(HFEmpCode.Value) > 0 ? int.Parse(HFEmpCode.Value) : (int?)null;

                if (HFEmpTypeID.Value != "")
                {
                    atblEmployeePromotionEntryDAO.EmpTypeId =
                        Convert.ToInt32(HFEmpTypeID.Value) > 0 ? int.Parse(HFEmpTypeID.Value) : (int?)null;
                }

                atblEmployeePromotionEntryDAL.EmployeePromotionEntryUpsateInfo(atblEmployeePromotionEntryDAO);


                //Manual Update of Employee Information =====================================================================================

                if (manualUpdateCheckBox.Checked)
                {
                    Int32 empGenId = 0;
                    Int32 salaryGradeId = 0;
                    Int32 salaryStepId = 0;
                    Int32 desigId = 0;
                    Int32 reportingBodyId = 0;

                    empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                    salaryGradeId = Convert.ToInt32(NewSalaryGradeDropDownList.SelectedIndex > 0
                        ? int.Parse(NewSalaryGradeDropDownList.SelectedValue)
                        : (int?)null);

                    salaryStepId = Convert.ToInt32(NewSalaryGradeDropDownList.SelectedIndex > 0
                       ? int.Parse(NstepDropDownList.SelectedValue)
                       : (int?)null);
                    desigId = Convert.ToInt32(NewDesignationDropDownList.SelectedIndex > 0
                        ? int.Parse(NewDesignationDropDownList.SelectedValue)
                        : (int?)null);

                    reportingBodyId = Convert.ToInt32(Convert.ToInt32(HiddenFieldNewReportingBody.Value) > 0
                        ? int.Parse(HiddenFieldNewReportingBody.Value)
                        : (int?)null);


                    UpdateEmployeeStepId(empGenId, salaryGradeId, salaryStepId, desigId, reportingBodyId);
                }

                //============================================================================================================================


                atblEmployeePromotionEntryDAL.DeleteDirectlyS(EmployeePromotionEntryIdHiddenField.Value.ToString());
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {

                    EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao();

                    andRedesignationDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                    andRedesignationDao.EmpTransferAndRedesignationId = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value);

                    if (Convert.ToInt32(loadGridView.DataKeys[i][1].ToString()) != 0)
                    {
                        andRedesignationDao.PrevEmpReportingBodyId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                    }

                    //DataTable prevRPTBody = atblEmployeePromotionEntryDAL.GetEmployeeReportingBodyInfo(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()));

                    //if (prevRPTBody.Rows.Count > 0)
                    //{

                    //    if (prevRPTBody.Rows[0]["ReportingEmpId"] != DBNull.Value)
                    //    {
                    //        andRedesignationDao.PrevEmpReportingBodyId =
                    //            Convert.ToInt32(prevRPTBody.Rows[0]["ReportingEmpId"]);
                    //    }

                    //}

                    int idd =
                        atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationDSSaveInfo(
                            andRedesignationDao);

                    if (manualUpdateCheckBox.Checked)
                    {
                        UpDateSuperVisorInfo(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()), Convert.ToInt32(repEmpIdHiddenField.Value));
                    }


                }

                atblEmployeePromotionEntryDAL.DeletePS(EmployeePromotionEntryIdHiddenField.Value.ToString());
                for (int i = 0; i < presuperGridView.Rows.Count; i++)
                {
                    EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao()
                    {
                        EmpTransferAndRedesignationId = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value),
                        EmpInfoId = Convert.ToInt32(presuperGridView.DataKeys[i][0].ToString())
                    };
                    int idd =
                        atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationPSSaveInfo(
                            andRedesignationDao);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Data Update Successfull...');window.location ='EmployeePromotionEntryView.aspx';",
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
        Response.Redirect("EmployeePromotionEntryView.aspx");
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
                        int salaryGradeId = dt.Rows[rowIndex].Field<Int32>("PSalGradeId");
                        int salaryStepId = dt.Rows[rowIndex].Field<Int32>("PStepId");
                        int desigId = dt.Rows[rowIndex].Field<Int32>("PDesignationId");
                        int reportingBodyId = dt.Rows[rowIndex].Field<Int32>("PRepEmpId");

                        UpdateEmployeeStepId(empGenId, salaryGradeId, salaryStepId, desigId,reportingBodyId);

                        DataTable dtdata = atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationDS(EmployeePromotionEntryIdHiddenField.Value);

                       

                        if (dtdata.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtdata.Rows.Count; i++)
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



                        DataTable psuperVisorDtTable = atblEmployeePromotionEntryDAL.EmpTransferAndRedesignationPS(EmployeePromotionEntryIdHiddenField.Value);

                        if (psuperVisorDtTable.Rows.Count > 0)
                        {

                            for (int i = 0; i < psuperVisorDtTable.Rows.Count; i++)
                            {
                                int reptId = dt.Rows[0].Field<Int32>("EmployeeId");
                                int empId = psuperVisorDtTable.Rows[i].Field<Int32>("EmpInfoId");
                                UpDateSuperVisorInfo(empId, reptId);
                            } 
                        }

                       
                    }

                    
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

    protected void Button2_OnClick(object sender, EventArgs e)
    {
        tblEmployeePromotionEntryDAO aMaster = new tblEmployeePromotionEntryDAO();
            aMaster.EmployeePromotionEntryId
                = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = atblEmployeePromotionEntryDAL.UpdateContractural(aMaster);
            if (status)
            {
                int commentid = atblEmployeePromotionEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = atblEmployeePromotionEntryDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);
                    atblEmployeePromotionEntryDAL.UpdateJobReqStatus2(aMaster);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;

                    bool isselfapp = false;
                    DataTable dtdatainfo =
                        atblEmployeePromotionEntryDAL.GetContractualDataInfo(aMaster.EmployeePromotionEntryId.ToString());
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    }
                    if (isselfapp)
                    {
                        DataTable dtempdata = atblEmployeePromotionEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                             Session["CompanyId"].ToString() + "'");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        //aMaster.ActionStatus = "Verified";
                        atblEmployeePromotionEntryDAL.UpdateJobReqStatus2(aMaster);
                        int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Promotion Approval ", @"  <br/> Dear Sir, <br/>
A Employee Promotion is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }
                    else
                    {
                        empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        aMaster.ActionStatus = "Verified";
                        atblEmployeePromotionEntryDAL.UpdateContractural(aMaster);
                        atblEmployeePromotionEntryDAL.UpdateJobReqStatus2(aMaster);
                        atblEmployeePromotionEntryDAL.UpdateSelfApprove(aMaster.EmployeePromotionEntryId, true);
                     
                        //aIncrementDal.UpdateJobReqStatus2(aMaster);
                        int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);
                    }

                    
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = atblEmployeePromotionEntryDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), EmployeePromotionEntryIdHiddenField.Value);
                    DataTable dtempdata2 = atblEmployeePromotionEntryDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), EmployeePromotionEntryIdHiddenField.Value);


                    if (dtempdata2.Rows.Count > 0)
                    {
                        EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        atblEmployeePromotionEntryDAL.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);
                        atblEmployeePromotionEntryDAL.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                    DataTable dtdata = atblEmployeePromotionEntryDAL.GetDataReviewEntryBy(
                      EmployeePromotionEntryIdHiddenField.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count > 0)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Edit";
                        Session["PromotionEdit"] = aMaster.EmployeePromotionEntryId.ToString();
                        Response.Redirect("EmployeePromotionEntry.aspx?id2=" + aMaster.EmployeePromotionEntryId.ToString());
                    }

                }
                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successfully Done...');window.location ='EmployeePromotionApprovalList.aspx';",
                           true);

            }
           
         
    }


    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (dynamic)null;
            if (forEmpID > 0)
            {
                GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();
            }
            else
            {
                int EntryEmpID = Convert.ToInt32(entryempinfoIdHiddenField.Value);


                GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == EntryEmpID
                                  select t).FirstOrDefault();
            }


            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
            try
            {
                mail.To.Add(ForMailAddress.Trim());
            }
            catch (Exception)
            {
                //throw;
            }
            mail.Subject = mSubject;
            mail.Body =
                "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                WebUtility.HtmlDecode(mBody)
                +
                "</div>";

            //Attach file using FileUpload Control and put the file in memory stream

            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(),
                Session["AppPass"].ToString());
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {

            }
            catch (Exception exe)
            {

            }


            System.Threading.Thread.Sleep(100);
        }



    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void Button10_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {

            tblEmployeePromotionEntryDAO aMaster = new tblEmployeePromotionEntryDAO();
            aMaster.EmployeePromotionEntryId
                = Convert.ToInt32(EmployeePromotionEntryIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = atblEmployeePromotionEntryDAL.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = atblEmployeePromotionEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        atblEmployeePromotionEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +

                  "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        atblEmployeePromotionEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    //DataTable dtempdata = aIncrementDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = aIncrementDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Promotion Approval ", @"  <br/> Dear Sir, <br/>
A Employee Promotion is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtempdata = atblEmployeePromotionEntryDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), EmployeePromotionEntryIdHiddenField.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata2 = atblEmployeePromotionEntryDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), EmployeePromotionEntryIdHiddenField.Value);
                    int a = 0;
                    for (int i = 0; i < dtempdata2.Rows.Count; i++)
                    {
                        if (dtempdata.Rows[i]["PreEmpInfoId"].ToString() != dtempdata.Rows[i]["ForEmpInfoId"].ToString())
                        {
                            a = i;
                            break;
                        }
                    }
                    if (dtempdata2.Rows.Count > 0)
                    {
                        EmployeePromotionEntryAppLogDAO appLogDao = new EmployeePromotionEntryAppLogDAO();
                        {
                            //appLogDao.ActionStatus = "Verified";
                            appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                            appLogDao.EmployeePromotionEntryId = aMaster.EmployeePromotionEntryId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        if (actionst == "Approved")
                        {
                            aMaster.ActionStatus = "Verified";
                            atblEmployeePromotionEntryDAL.UpdateContractural(aMaster);
                        }
                        atblEmployeePromotionEntryDAL.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        atblEmployeePromotionEntryDAL.UpdateAppLog("Review", dtempdata2.Rows[a][0].ToString());
                        int id = atblEmployeePromotionEntryDAL.SavAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }
                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successfully Done...');window.location ='EmployeePromotionApprovalList.aspx';",
                           true);

            }
           
        }
    }
}