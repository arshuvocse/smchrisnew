using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.MeetingMinorsDAL;
using DAL.Report_DAL;
using DAL.Transfer_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.UA_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpGeneral : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();

    private string _userId;
    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"]!="")
        {
         _userId=   Convert.ToString(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {
           // lbl_Mode.Text = "Entry Mode";
            startDate.Attributes.Add("readonly", "readonly");
            endDate.Attributes.Add("readonly", "readonly");
            DateReadOnly();
            ButtonVisible();
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {

                chkIsPreviousEmp.Enabled = false;
                chkSpecialTransfer.Enabled = false;
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {

                    GetEmpEditInfo(mid);
                }
            }
        }
    }

    private void GetEmpEditInfo(int midfff)
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDLForEdit())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        if (chkIsPreviousEmp.Checked==false)
        {
            lblNext.Visible = true;
            
        } 
        using (var db = new HRIS_SMCEntities())
        {
            var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == midfff select j).FirstOrDefault();

            empMasterCode.Text = emp.EmpMasterCode;
            lblEmpName.Text = emp.EmpName;

            using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(midfff))
            {
                lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();
            }
            //lblDesignation.Text = emp.DesignationId.ToString();

            #region 1. General Information
              try
            {
            ddlCompany.SelectedValue = emp.CompanyId.ToString();
            }
              catch (Exception)
              {

                  //throw;
              }
            ddlCompany_SelectedIndexChanged(null, null);
            if (chkIsPreviousEmp.Checked == false)
            {
                ddlCompany.Enabled = false;
            }
            txt_EmpName.Text = emp.EmpName;
            ddlGender.SelectedValue = emp.Gender;
            ddlBloodGroup.SelectedValue = emp.BloodGroup;
            txt_EmpTinNo.Text = emp.TinNo;

            txt_EmpFatherName.Text = emp.FatherName;
            try
            {
                ddlEmpFOccupation.SelectedValue = emp.FatherOccupation.ToString();

            }
            catch (Exception)
            {
                
                //throw;
            }

            try
            {

                bool _new = false;
                try
                {
                    _new = Convert.ToBoolean(emp.RecruitmentTypeNew);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                rbRecruitmentType.Items[0].Selected = _new;

            }
            catch (Exception)
            {

                //throw;
            }


            try
            {

                bool _Replacement = false;
                try
                {
                    _Replacement = Convert.ToBoolean(emp.RecruitmentTypeReplacement);
                }
                catch (Exception)
                {

                    //throw;
                }
                rbRecruitmentType.Items[1].Selected = _Replacement;

            }
            catch (Exception)
            {

                //throw;
            }


            txt_EmpMotherName.Text = emp.MotherName;
            try
            {
                ddlDivision.SelectedValue = emp.DivisionId.ToString();
            }
            catch (Exception)
            {
                ddlDivision.SelectedValue = null;
                //throw;
            }

            try
            {
                ddlleaveRecommender.SelectedValue = emp.leaveRecommenderId.ToString();
            }
            catch (Exception)
            {
                ddlleaveRecommender.SelectedValue = null;
                //throw;
            }
            try
            {
                ddlLeaveApproval.SelectedValue = emp.LeaveApprovalId.ToString();
            }
            catch (Exception)
            {
                ddlLeaveApproval.SelectedValue = null;
                //throw;
            }

            if (emp.ContractPeriod != null)
            {
                txtContractualPreiod.Text = emp.ContractPeriod.ToString();
            }

            try
            {
                ddlEmpMOccupation.SelectedValue = emp.MotherOccupation.ToString();
            }
            catch (Exception)
            {
            }

             try
            {
            txt_EmpDOB.Text = string.IsNullOrEmpty(emp.DateOfBirth.ToString())
                ? String.Empty
                : emp.DateOfBirth.Value.ToString("dd-MMM-yyyy");

            }
             catch (Exception)
             {
             }


            //SeparationDateTextBox.Text = string.IsNullOrEmpty(emp..ToString()) ? String.Empty : emp.DateOfBirth.Value.ToString("dd-MMM-yyyy");


            txt_EmpDOJ.Text = string.IsNullOrEmpty(emp.DateOfJoin.ToString())
                ? String.Empty
                : emp.DateOfJoin.Value.ToString("dd-MMM-yyyy");

            txtFatherDOB.Text = string.IsNullOrEmpty(emp.FatherDOB.ToString())
                ? String.Empty
                : emp.FatherDOB.Value.ToString("dd-MMM-yyyy");

            txtMotherDOB.Text = string.IsNullOrEmpty(emp.MotherDOB.ToString())
                ? String.Empty
                : emp.MotherDOB.Value.ToString("dd-MMM-yyyy");

               try
            {
            ddlEmpReligion.SelectedValue = emp.Religion;
                     }
             catch (Exception)
             {
             }
                      try
            {
            ddlEmpMaritalStatus.SelectedValue = emp.MaritalStatus;
                            }
             catch (Exception)
             {
             }
                             try
            {
            ddlEmpType.SelectedValue = emp.EmpTypeId.ToString();
                                   }
             catch (Exception)
             {
             }
            txt_EmpPlaceOfBirth.Text = emp.PlaceOfBirth;
                                    try
            {
            ddlEmpNationality.SelectedValue = emp.Nationality;
            }
                                    catch (Exception)
                                    {
                                    }
            txt_EmpNationalID.Text = emp.NationalIdNo;
            if (ddlEmpType.SelectedValue == "2")
            {
                txt_ContractEndDate.Text = string.IsNullOrEmpty(emp.ContractEndDate.ToString())
                    ? String.Empty
                    : emp.ContractEndDate.Value.ToString("dd-MMM-yyyy");
            }
            txt_EmpPassport.Text = emp.PassportNo;
            txt_EmpExpectedServiceLength.Text = emp.ExpectedServiceLength;
            txt_EmpDateOfRetirement.Text = string.IsNullOrEmpty(emp.DateOfRetirement.ToString())
                ? String.Empty
                : emp.DateOfRetirement.Value.ToString("dd-MMM-yyyy");

            txt_EmpDateOfConformation.Text = string.IsNullOrEmpty(emp.DateOfConformation.ToString())
                ? string.Empty
                : emp.DateOfConformation.Value.ToString("dd-MMM-yyyy");

            if (emp.ConformationStatus == "1")
            {
                ddlConformationStatus.SelectedIndex = 1;
            }


            if (emp.ConformationStatus == "0")
            {
                ddlConformationStatus.SelectedIndex = 2;
            }


            if (emp.ConformationStatus == "NULL")
            {
                ddlConformationStatus.SelectedValue = "-1";
            }


            if (!string.IsNullOrEmpty(emp.ReportingEmpId.ToString()))
            {
                try
                {
                    ddlReportingBoss.SelectedValue = emp.ReportingEmpId.ToString();
                }
                catch (Exception)
                {
                    ddlReportingBoss.SelectedValue = null;
                    //throw;
                }
                //int ReportingEmpId = int.Parse(emp.ReportingEmpId.ToString());

                //var reportingBoss =
                //    (from j in db.vw_EmpInfo where j.EmpInfoId == ReportingEmpId select j).FirstOrDefault();
                //txt_ReportingBoss.Text = reportingBoss.EmpName;
                //txt_ReportingBossDesig.Text = reportingBoss.Designation;
            }

            chkIsProbationary.Checked = string.IsNullOrEmpty(emp.IsProbationary.ToString())
                ? false
                : bool.Parse(emp.IsProbationary.ToString());
            chkIsProgramContractual.Checked = string.IsNullOrEmpty(emp.IsProgramContractual.ToString())
                ? false
                : bool.Parse(emp.IsProgramContractual.ToString());
            txt_ProbationaryEndDate.Text = string.IsNullOrEmpty(emp.ProbationEndDate.ToString())
                ? string.Empty
                : emp.ProbationEndDate.Value.ToString("dd-MMM-yyyy");
            hfempimg.Value = emp.EmpImage;
            img_emp.ImageUrl = "~/UploadImg/" + emp.EmpImage;

            hfNomineeImage.Value = emp.NomineeImage;
            img_NomineeImage.ImageUrl = "~/UploadImg/" + emp.NomineeImage;


            try
            {
                FundedProjectsCheckBox1.Checked = Convert.ToBoolean(emp.IsSMCFundedProjects);
            }
            catch (Exception)
            {
                FundedProjectsCheckBox1.Checked = false;
                //throw;
            }
            if (ddlEmpType.SelectedValue == "2")
            {
                chkIsProgramContractual.Enabled = true;


                FundedProjectsCheckBox1.Enabled = true;


                chkSmcContract.Enabled = true;
                chkIsCompanyDirector.Enabled = true;


                try
                {
                    chkIsCompanyDirector.Checked = Convert.ToBoolean(emp.IsCompanyDirector);
                }
                catch (Exception)
                {
                    chkIsCompanyDirector.Checked = false;
                    //throw;
                }


                if (FundedProjectsCheckBox1.Checked == false && chkIsProgramContractual.Checked == false &&
                    chkIsCompanyDirector.Checked == false)
                {
                    chkSmcContract.Checked = true;
                }
            }


            hfSignature.Value = emp.EmpSign;
            SignatureImage.ImageUrl = "~/UploadImg/" + emp.EmpSign;

            using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(midfff.ToString()))
            {
                if (dtreporting.Rows.Count > 0)
                {
                    loadGridView.DataSource = dtreporting;
                    loadGridView.DataBind();
                }
            }
            if (chkIsProgramContractual.Checked)
            {
                ProjectDropDownList.Enabled = true;
                dal.LoaProjectByCheckDropDownList(ProjectDropDownList, ddlCompany.SelectedValue, " and  IsOtherProject=1");
            }


            DataTable ExistingProjectdtdata = new DataTable();
            ExistingProjectdtdata = dal.LoadExistingProjectByTop1(midfff);
            if (ExistingProjectdtdata.Rows.Count > 0)
            {
                try
                {
                    ProjectDropDownList.SelectedValue = ExistingProjectdtdata.Rows[0]["ProjectId"].ToString();
                    HFproMasId.Value = ExistingProjectdtdata.Rows[0]["EmployeeWiseProjectAllocationMasterId"].ToString();
                    HFproDtlId.Value = ExistingProjectdtdata.Rows[0]["EmpWiseProjectDetailID"].ToString();
                }
                catch (Exception)
                {
                    //throw;
                }
            }

            try
            {
                DataTable dtdata = appDal.LoadEmpGenInfo(" and e.EmpInfoId=" + midfff.ToString());


                if (dtdata.Rows.Count > 0)
                {
                    if (dtdata.Rows[0]["SuperMenuAppId"].ToString() != "")
                    {
                        chkIsAllEmployee.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsAllEmployee"].ToString());
                        if (chkIsAllEmployee.Checked)
                        {
                            using (
                                DataTable dt =
                                    _commonDataLoad.GetEmpDDLForEntryByGrade()
                                )
                            {
                                try
                                {
                                    ddlFinalApprover.DataSource = dt;
                                    ddlFinalApprover.DataValueField = "EmpInfoId";
                                    ddlFinalApprover.DataTextField = "EmpName";
                                    ddlFinalApprover.DataBind();
                                    ddlFinalApprover.Items.Insert(0,
                                        new ListItem("Please Select an Employee.....", String.Empty));
                                    ddlFinalApprover.SelectedIndex = 0;
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                                try
                                {
                                    ddlFinalApprover.SelectedValue =
                                        dtdata.Rows[0]["EmpInfoId"].ToString();
                                }
                                catch (Exception)
                                {
                                    ddlFinalApprover.SelectedIndex = 0;
                                    //throw;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                DataTable aDataTable = new DataTable();
                                aDataTable.Columns.Add("EmpInfoId");
                                aDataTable.Columns.Add("EmpName");
                                aDataTable.Columns.Add("EmpMasterCode");
                                DataRow dataRow = null;
                                dataRow = aDataTable.NewRow();
                                dataRow["EmpInfoId"] = "0";
                                dataRow["EmpName"] = "Please Select an Employee.....";
                                dataRow["EmpMasterCode"] = "";
                                aDataTable.Rows.Add(dataRow);
                                try
                                {
                                    appDal.ReportingEmpData(midfff.ToString(), aDataTable);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }

                                ddlFinalApprover.DataValueField = "EmpInfoId";
                                ddlFinalApprover.DataTextField = "EmpName";
                                ddlFinalApprover.DataSource = aDataTable;
                                ddlFinalApprover.DataBind();
                            }
                            catch (Exception)
                            {
                                //throw;
                            }
                            try
                            {
                                ddlFinalApprover.SelectedValue = dtdata.Rows[0]["EmpInfoId"].ToString();
                            }
                            catch (Exception)
                            {
                                ddlFinalApprover.SelectedIndex = 0;
                                //throw;
                            }
                        }
                        hfSuperMenuAppId.Value = dtdata.Rows[0]["SuperMenuAppId"].ToString();
                        ddlFinalApprover.SelectedValue = dtdata.Rows[0]["EmpInfoId"].ToString();
                    }
                    else
                    {
                        if (ddlReportingBoss.SelectedValue != "")
                        {
                            try
                            {
                                DataTable aDataTable = new DataTable();
                                aDataTable.Columns.Add("EmpInfoId");
                                aDataTable.Columns.Add("EmpName");
                                aDataTable.Columns.Add("EmpMasterCode");
                                DataRow dataRow = null;
                                dataRow = aDataTable.NewRow();
                                dataRow["EmpInfoId"] = "0";
                                dataRow["EmpName"] = "Please Select an Employee.....";
                                dataRow["EmpMasterCode"] = "";
                                aDataTable.Rows.Add(dataRow);
                                appDal.ReportingEmpData(ddlReportingBoss.SelectedValue.ToString(), aDataTable);

                                ddlFinalApprover.DataValueField = "EmpInfoId";
                                ddlFinalApprover.DataTextField = "EmpName";
                                ddlFinalApprover.DataSource = aDataTable;
                                ddlFinalApprover.DataBind();
                            }
                            catch (Exception)
                            {
                                //throw;
                            }
                            try
                            {
                                ddlFinalApprover.SelectedValue =
                                    dtdata.Rows[0]["EmpInfoId"].ToString();
                            }
                            catch (Exception)
                            {
                                ddlFinalApprover.SelectedIndex = 0;
                                //throw;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // throw;
            }


            int empJobId = 0;
            if (int.TryParse(emp.JobID.ToString(), out empJobId))
            {
                hfJobID.Value = empJobId.ToString();
                var jobCreation = (from j in db.tblJobCreations where j.JobID == empJobId select j).FirstOrDefault();
                txt_JobCirculation.Text = jobCreation.Position;
            }

            #endregion end 
        }
    }

    private void DateReadOnly()
    {
      //  txt_EmpDOB.Attributes.Add("readonly", "readonly");
      //  txt_EmpDOJ.Attributes.Add("readonly", "readonly");
        txt_ContractEndDate.Attributes.Add("readonly", "readonly");
        txt_EmpDateOfRetirement.Attributes.Add("readonly", "readonly");
       // txt_EmpDateOfConformation.Attributes.Add("readonly", "readonly");

        txt_ProbationaryEndDate.Attributes.Add("readonly", "readonly");



    }

  
    public void ButtonVisible()
    {
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        btn_Save.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        btn_Edit.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        btn_Del.Visible = true;
        //    }
        //    Session["Status"] = null;

        //}
        //else
        //{
        //    Response.Redirect("EmployeeInfoList.aspx");
        //}

    }

    private void LoadInitialDDL()
    {


        try
        {
            DataTable dtcom2 = _commonDataLoad.GetEmpDDLForEntry2("");
            ddlReportingBoss.DataSource = dtcom2;
            ddlReportingBoss.DataValueField = "EmpInfoId";
            ddlReportingBoss.DataTextField = "EmpName";
            ddlReportingBoss.DataBind();
            ddlReportingBoss.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlReportingBoss.SelectedIndex = 0;

            ddlleaveRecommender.DataSource = dtcom2;
            ddlleaveRecommender.DataValueField = "EmpInfoId";
            ddlleaveRecommender.DataTextField = "EmpName";
            ddlleaveRecommender.DataBind();
            ddlleaveRecommender.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));


            ddlLeaveApproval.DataSource = dtcom2;
            ddlLeaveApproval.DataValueField = "EmpInfoId";
            ddlLeaveApproval.DataTextField = "EmpName";
            ddlLeaveApproval.DataBind();
            ddlLeaveApproval.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));

            using (DataTable dt = _commonDataLoad.GetEmpDDLForEntryByGrade())
            {



                ddlFinalApprover.DataSource = dt;
                ddlFinalApprover.DataValueField = "EmpInfoId";
                ddlFinalApprover.DataTextField = "EmpName";
                ddlFinalApprover.DataBind();
                ddlFinalApprover.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlFinalApprover.SelectedIndex = 0;
                try
                {
                    ddlFinalApprover.SelectedValue =
                          ddlReportingBoss.SelectedValue;
                }
                catch (Exception)
                {
                    ddlFinalApprover.SelectedIndex = 0;
                    //throw;
                }


           

            }

            using (DataTable dt = _commonDataLoad.GetCompanyDDL())
            {
                ddlCompany.DataSource = dt;
                ddlCompany.DataValueField = "Value";
                ddlCompany.DataTextField = "TextField";
                ddlCompany.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLEmpType())
            {
                ddlEmpType.DataSource = dt;
                ddlEmpType.DataValueField = "Value";
                ddlEmpType.DataTextField = "TextField";
                ddlEmpType.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLOccupation())
            {
                ddlEmpFOccupation.DataSource = dt;
                ddlEmpFOccupation.DataValueField = "Value";
                ddlEmpFOccupation.DataTextField = "TextField";
                ddlEmpFOccupation.DataBind();

                ddlEmpMOccupation.DataSource = dt;
                ddlEmpMOccupation.DataValueField = "Value";
                ddlEmpMOccupation.DataTextField = "TextField";
                ddlEmpMOccupation.DataBind();



            }

            using (DataTable dt = _commonDataLoad.GetDDLEmpType())
            {
                ddlEmpType.DataSource = dt;
                ddlEmpType.DataValueField = "Value";
                ddlEmpType.DataTextField = "TextField";
                ddlEmpType.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetNationality())
            {
                ddlEmpNationality.DataSource = dt;
                ddlEmpNationality.DataValueField = "Value";
                ddlEmpNationality.DataTextField = "TextField";
                ddlEmpNationality.DataBind();
            }
        }
        catch (Exception)
        {
            
            //throw;
        }

          

    }
    protected void txt_EmpExpectedServiceLength_OnTextChanged(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text))
        //{
        //    int esl = int.Parse(txt_EmpExpectedServiceLength.Text);
        //    DateTime doj ;
        //    if (DateTime.TryParse(txt_EmpDOJ.Text,out doj))
        //    {
        //        DateTime dor = doj.AddYears(esl);
        //        txt_EmpDateOfRetirement.Text = dor.ToString("dd-MMM-yyyy");
        //    }
        //}
    }

    protected void txt_EmpDOB_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            
           
            DataTable dt = _commonDataLoad.GetRetirementSetting();
            int esl = (int)dt.Rows[0]["RetirementLength"];
            DateTime doj;
            if (DateTime.TryParse(txt_EmpDOB.Text, out doj))
            {
                DateTime dor = doj.AddYears(esl);
                txt_EmpDateOfRetirement.Text = dor.ToString("dd-MMM-yyyy");
            }
            else
            {
                ShowMessageBox("Please Enter Valid Date!!!");
                txt_EmpDOB.Text = "";
            }
        }
        catch (Exception)
        {

            ShowMessageBox("Please Enter Valid Date!!!");
            txt_EmpDOB.Text = "";
        }
    }


    protected void txtFatherDOB_OnTextChanged(object sender, EventArgs e)
    {
        try
        {


             
            DateTime doj;
            if (DateTime.TryParse(txtFatherDOB.Text, out doj))
            {
              
            }
            else
            {
                ShowMessageBox("Please Enter Valid Date!!!");
                txtFatherDOB.Text = "";
            }
        }
        catch (Exception)
        {

            ShowMessageBox("Please Enter Valid Date!!!");
            txtFatherDOB.Text = "";
        }
    }


    protected void txtMotherDOB_OnTextChanged(object sender, EventArgs e)
    {
        try
        {



            DateTime doj;
            if (DateTime.TryParse(txtMotherDOB.Text, out doj))
            {

            }
            else
            {
                ShowMessageBox("Please Enter Valid Date!!!");
                txtMotherDOB.Text = "";
            }
        }
        catch (Exception)
        {

            ShowMessageBox("Please Enter Valid Date!!!");
            txtMotherDOB.Text = "";
        }
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = "";
        Session["cid"] = ddlCompany.SelectedValue;
        Session["CompanyId"] = "";
        Session["CompanyId"] = ddlCompany.SelectedValue;
       // ddlReportingBoss.Items.Clear();
      
      
        MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
        using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
        {
            ddlDivision.DataSource = dt;
            ddlDivision.DataValueField = "Value";
            ddlDivision.DataTextField = "TextField";
            ddlDivision.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue))
        {
           


            ddldirectlySuper.DataSource = dt;
            ddldirectlySuper.DataValueField = "EmpInfoId";
            ddldirectlySuper.DataTextField = "EmpName";
            ddldirectlySuper.DataBind();
            ddldirectlySuper.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));

          


            //if (ddlCompany.SelectedValue == "1")
            //{

            //    ddlReportingBoss.DataSource = dt;
            //    ddlReportingBoss.DataValueField = "EmpInfoId";
            //    ddlReportingBoss.DataTextField = "EmpName";
            //    ddlReportingBoss.DataBind();
            //    ddlReportingBoss.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            //}

            
        }
    }

    protected void txt_EmpDOJ_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime doj;
            if (DateTime.TryParse(txt_EmpDOJ.Text, out doj))
            {
                MonthCalculation();
            }
            else
            {
                ShowMessageBox("Please Enter Valid Date!!!");
                txt_EmpDOJ.Text = "";
            }
        }
        catch (Exception)
        {

            ShowMessageBox("Please Enter Valid Date!!!");
            txt_EmpDOJ.Text = "";
        }
    }



    protected void ddlEmpType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpType.SelectedIndex > 0)
        {
            ////ddlEmpType.SelectedValue == "1" is for Permanent
            if (ddlEmpType.SelectedValue == "1")
            {
                chkIsProgramContractual.Enabled = false;
                chkIsProgramContractual.Checked = false;
                FundedProjectsCheckBox1.Enabled = false;
                //chkIsCompanyDirector.Enabled = false;
                FundedProjectsCheckBox1.Checked = false;
                //chkIsCompanyDirector.Checked = false;

                chkSmcContract.Enabled = false;
                chkSmcContract.Checked = false;
                //chkIsCompanyDirector.Checked = false;

              //  ProjectDropDownList.Enabled = false;
                ProjectDropDownList.Items.Clear();

            }
            else
            {
                chkIsProgramContractual.Enabled = true;
                chkIsProgramContractual.Checked = false;
                //chkIsCompanyDirector.Enabled = true;

                FundedProjectsCheckBox1.Enabled = true;
                FundedProjectsCheckBox1.Checked = false;

                chkSmcContract.Enabled = true;
                chkSmcContract.Checked = false;
                //chkIsCompanyDirector.Checked = false;

              //  ProjectDropDownList.Enabled = false;
                ProjectDropDownList.Items.Clear();


            }


            //if (ddlEmpType.SelectedValue == "3")
            //{
            //    using (DataTable dt = _commonDataLoad.GetDDLSalFromProject(ddlCompany.SelectedValue))
            //    {
            //        ddlSalFromProject.DataSource = dt;
            //        ddlSalFromProject.DataValueField = "Value";
            //        ddlSalFromProject.DataTextField = "TextField";
            //        ddlSalFromProject.DataBind();
            //    }
            //    using (DataTable dt = _commonDataLoad.GetAllProject(ddlCompany.SelectedValue))
            //    {
            //        cbl_ContractProject.DataSource = dt;
            //        cbl_ContractProject.DataValueField = "Value";
            //        cbl_ContractProject.DataTextField = "TextField";
            //        cbl_ContractProject.DataBind();
            //    }
            //}
            iddivContract.Visible = ddlEmpType.SelectedValue == "2";
        }
        else
        {

            chkIsProgramContractual.Enabled = false;
            chkIsProgramContractual.Checked = false;

        }
    }

    protected void txt_ReportingBoss_OnTextChanged(object sender, EventArgs e)
    {
        //string Emp = txt_ReportingBoss.Text.Trim();
        //if (!string.IsNullOrEmpty(Emp))
        //{
        //    if (Emp.Contains('>'))
        //    {
        //        hdReportingBoss.Value = Emp.Split('>')[0];
        //        txt_ReportingBoss.Text = Emp.Split('>')[2];
        //        txt_ReportingBossDesig.Text = Emp.Split('>')[3];
        //    }
        //    else
        //    {

        //        txt_ReportingBoss.Text = "";
        //        ShowMessageBox("Input Correct Data !!");
        //    }
        //}
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void chkIsProbationary_OnCheckedChanged(object sender, EventArgs e)
    {

        if (chkIsProbationary.Checked)
        {
            txt_EmpDateOfConformation.Text = "";
        }
        else
        {
            txt_ProbationaryEndDate.Text = "";
        }
        //txt_ProbationaryEndDate.Enabled =  chkIsProbationary.Checked;
        //txt_ProbationaryEndDate.ReadOnly = !chkIsProbationary.Checked;
        //txt_EmpDateOfConformation.ReadOnly = chkIsProbationary.Checked;
       // ddlConformationStatus.Enabled = !chkIsProbationary.Checked;
    }

    protected void directlySuperTextBox_OnTextChanged(object sender, EventArgs e)
    {
        //string empName = directlySuperTextBox.Text.Trim();

        //if (empName.Contains(':'))
        //{
        //    string[] emp = empName.Split(':', ':');

        //    directlySuperTextBox.Text = emp[2];
        //    directlyEmpIdHiddenField.Value = emp[0];

        //    //DataTable aTable = atblEmployeePromotionEntryDAL.GetEmployeeReportingBodyInfo(Convert.ToInt32(directlyEmpIdHiddenField.Value));

        //    //if (aTable.Rows.Count > 0)
        //    //{

        //    //    if (aTable.Rows[0]["ReportingEmpId"] != DBNull.Value)
        //    //    {
        //    //        rptHiddenField.Value = aTable.Rows[0]["ReportingEmpId"].ToString();
        //    //    }
        //    //    else
        //    //    {
        //    //        rptHiddenField.Value = 0.ToString();
        //    //    }

        //    //}

        //    // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
        //    //productNameTextBox.Text = productInfo[1];
        //    //string productCode = productCodeTextBox.Text.Trim();

        //}
        //else
        //{
        //    rptHiddenField.Value = "";
        //    directlySuperTextBox.Text = "";
        //    directlyEmpIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (ddldirectlySuper.SelectedValue != "")
        {
               Add();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Fill This Field!!!", this);
            ddldirectlySuper.Focus();
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
            //dataRow["PrevEmpReportingBodyId"] = loadGridView.DataKeys[i][1].ToString();

            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["EmpName"] = ddldirectlySuper.SelectedItem.Text;
        dataRow["EmpInfoId"] = ddldirectlySuper.SelectedValue;
        dataRow["PrevEmpReportingBodyId"] = rptHiddenField.Value;

        aDataTable.Rows.Add(dataRow);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        rptHiddenField.Value = string.Empty;
        ddldirectlySuper.SelectedValue = null;
         
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButton = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        delEmpIdHiddenField.Value = delEmpIdHiddenField.Value + "," + loadGridView.DataKeys[rowindex][0].ToString();

        Remove(rowindex);
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
                //dataRow["PrevEmpReportingBodyId"] = loadGridView.DataKeys[i][1].ToString();

                aDataTable.Rows.Add(dataRow);
            }
        }
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

    }

    //protected void btn_Edit_OnClick(object sender, EventArgs e)
    //{
    //    if (Validation())
    //    {
    //        #region fff

    //        try
    //        {
    //            string EmpMasterCode = string.Empty;
    //            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
    //            tblEmpGeneralInfo emp = null;
    //            using (var db = new HRIS_SMCEntities())
    //            {
    //                if (mid > 0)
    //                {

    //                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
    //                    EmpMasterCode = emp.EmpMasterCode;

    //                    #region 1. General Information

    //                    emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
    //                    emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
    //                    emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
    //                    emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
    //                    emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

    //                    emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
    //                    emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0
    //                        ? int.Parse(ddlEmpFOccupation.SelectedValue)
    //                        : (int?)null;
    //                    emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
    //                    emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0
    //                        ? int.Parse(ddlEmpMOccupation.SelectedValue)
    //                        : (int?)null;

    //                    emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDOB.Text).Date;
    //                    emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDOJ.Text).Date;
    //                    emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
    //                    emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0
    //                        ? ddlEmpMaritalStatus.SelectedValue
    //                        : null;

    //                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
    //                    emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text)
    //                        ? null
    //                        : txt_EmpPlaceOfBirth.Text;
    //                    emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
    //                    emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
    //                    if (ddlEmpType.SelectedValue == "2")
    //                    {
    //                        emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
    //                            ? (DateTime?)null
    //                            : DateTime.Parse(txt_ContractEndDate.Text).Date;
    //                        //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;
    //                        ////TODO
    //                        //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
    //                        //{
    //                        //    if (cbl_ContractProject.Items[i].Selected)
    //                        //    {
    //                        //        var ContractProject = new tblEmpContractProject();
    //                        //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
    //                        //        db.tblEmpContractProjects.Add(ContractProject);
    //                        //    }
    //                        //}
    //                    }
    //                    emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
    //                    emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text)
    //                        ? null
    //                        : txt_EmpExpectedServiceLength.Text;
    //                    emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

    //                    emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;

    //                    //  emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;

    //                    if (ddlConformationStatus.SelectedItem.Text == "Yes")
    //                    {
    //                        emp.ConformationStatus = "1";
    //                    }

    //                    if (ddlConformationStatus.SelectedItem.Text == "No")
    //                    {
    //                        emp.ConformationStatus = "0";
    //                    }

    //                    if (ddlConformationStatus.SelectedIndex == 0)
    //                    {
    //                        emp.ConformationStatus = "Null";
    //                    }


    //                    emp.ReportingEmpId = string.IsNullOrEmpty(hdReportingBoss.Value)
    //                        ? (int?)null
    //                        : int.Parse(hdReportingBoss.Value);

    //                    emp.IsProbationary = chkIsProbationary.Checked;
    //                    emp.IsProgramContractual = chkIsProgramContractual.Checked;
    //                    emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
    //                    emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
    //                    emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
    //                    emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
    //                    emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?)null : int.Parse(hfJobID.Value);
    //                    #endregion end

    //                    db.SaveChanges();

    //                    for (int i = 0; i < loadGridView.Rows.Count; i++)
    //                    {
    //                        _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
    //                            emp.EmpInfoId.ToString());
    //                    }
    //                    string[] delid = delEmpIdHiddenField.Value.Split(',');
    //                    foreach (string id in delid)
    //                    {
    //                        _commonDataLoad.UpdateReportingEmpId(null,
    //                            id);
    //                    }
    //                }
    //                else
    //                {
    //                    ////Start New Mode
    //                    emp = new tblEmpGeneralInfo();

    //                    #region 1. General Information

    //                    emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
    //                    emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
    //                    emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
    //                    emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
    //                    emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

    //                    emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
    //                    emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpFOccupation.SelectedValue) : (int?)null;
    //                    emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
    //                    emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpMOccupation.SelectedValue) : (int?)null;

    //                    emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOB.Text).Date;
    //                    emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOJ.Text).Date;
    //                    emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
    //                    emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0 ? ddlEmpMaritalStatus.SelectedValue : null;

    //                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
    //                    emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text) ? null : txt_EmpPlaceOfBirth.Text;
    //                    emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
    //                    emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
    //                    if (ddlEmpType.SelectedValue == "2")
    //                    {
    //                        emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ContractEndDate.Text).Date;
    //                        //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;

    //                        //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
    //                        //{
    //                        //    if (cbl_ContractProject.Items[i].Selected)
    //                        //    {
    //                        //        var ContractProject = new tblEmpContractProject();
    //                        //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
    //                        //        db.tblEmpContractProjects.Add(ContractProject);
    //                        //    }
    //                        //}
    //                    }
    //                    emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
    //                    emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text) ? null : txt_EmpExpectedServiceLength.Text;
    //                    emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

    //                    emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;
    //                    //emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;


    //                    if (ddlConformationStatus.SelectedItem.Text == "Yes")
    //                    {
    //                        emp.ConformationStatus = "1";
    //                    }

    //                    if (ddlConformationStatus.SelectedItem.Text == "No")
    //                    {
    //                        emp.ConformationStatus = "0";
    //                    }

    //                    if (ddlConformationStatus.SelectedIndex == 0)
    //                    {
    //                        emp.ConformationStatus = "Null";
    //                    }

    //                    emp.ReportingEmpId = string.IsNullOrEmpty(hdReportingBoss.Value) ? (int?)null : int.Parse(hdReportingBoss.Value);

    //                    emp.IsProbationary = chkIsProbationary.Checked;
    //                    emp.IsProgramContractual = chkIsProgramContractual.Checked;
    //                    emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
    //                    emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
    //                    emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
    //                    emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
    //                    #endregion end 1. General Information
    //                    emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?)null : int.Parse(hfJobID.Value);
    //                    db.SaveChanges();
    //                    EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
    //                    ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
    //                    using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(emp.EmpInfoId))
    //                    {
    //                        if (dtEmpCode.Rows.Count > 0)
    //                        {
    //                            EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
    //                        }

    //                    }
    //                    for (int i = 0; i < loadGridView.Rows.Count; i++)
    //                    {
    //                        _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
    //                            emp.EmpInfoId.ToString());
    //                    }
    //                }
    //            }

    //            ScriptManager.RegisterStartupScript(this, this.GetType(),
    //               "alert",
    //               "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmployeeInfoEntry.aspx';",
    //               true);
    //        }
    //        catch (Exception ex)
    //        {
    //            AlertMessageBoxShow(ex.Message);
    //        }
    //        #endregion
    //    }
    //}
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            #region fff

            try
            {
                string EmpMasterCode = string.Empty;
                mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
                tblEmpGeneralInfo emp = null;
                using (var db = new HRIS_SMCEntities())
                {
                    if (mid > 0)
                    {

                        emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        EmpMasterCode = emp.EmpMasterCode;
                        emp.IsActive = true;
                        emp.EmployeeStatus = "Active";
                        emp.Updateby = _userId;
                        emp.UpdateDate = DateTime.Now;

                        #region 1. General Information

                        emp.RecruitmentTypeNew = false;
                        try
                        {
                            emp.RecruitmentTypeNew = rbRecruitmentType.Items[0].Selected;
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }

                        emp.RecruitmentTypeReplacement = false;
                        try
                        {
                            emp.RecruitmentTypeReplacement = rbRecruitmentType.Items[1].Selected;
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }

                        emp.DivisionId = ddlDivision.SelectedIndex > 0
                            ? int.Parse(ddlDivision.SelectedValue)
                            : (int?) null;

                        emp.leaveRecommenderId = ddlleaveRecommender.SelectedIndex > 0
                            ? int.Parse(ddlleaveRecommender.SelectedValue)
                            : (int?) null;
                        emp.LeaveApprovalId = ddlLeaveApproval.SelectedIndex > 0
                            ? int.Parse(ddlLeaveApproval.SelectedValue)
                            : (int?) null;

                        emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?) null;
                        emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
                        emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
                        emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
                        emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

                        emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
                        emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0
                            ? int.Parse(ddlEmpFOccupation.SelectedValue)
                            : (int?) null;
                        emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
                        emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0
                            ? int.Parse(ddlEmpMOccupation.SelectedValue)
                            : (int?) null;

                        emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDOB.Text).Date;
                        emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;
                        emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
                        emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0
                            ? ddlEmpMaritalStatus.SelectedValue
                            : null;

                        emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?) null;
                        emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text)
                            ? null
                            : txt_EmpPlaceOfBirth.Text;
                        emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
                        emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
                        if (txtContractualPreiod.Text != "")
                        {
                            emp.ContractPeriod = Convert.ToInt32(txtContractualPreiod.Text);
                        }
                        if (ddlEmpType.SelectedValue == "2")
                        {
                            emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?) null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;


                            try
                            {
                                emp.ContractStartDate = emp.DateOfJoin;
                            }
                            catch (Exception)
                            {
                                emp.ContractStartDate = null;
                                //throw;
                            }

                        }
                        emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
                        emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text)
                            ? null
                            : txt_EmpExpectedServiceLength.Text;
                        emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

                        emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;

                        //  emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;

                        if (ddlConformationStatus.SelectedItem.Text == "Yes")
                        {
                            emp.ConformationStatus = "1";
                        }

                        if (ddlConformationStatus.SelectedItem.Text == "No")
                        {
                            emp.ConformationStatus = "0";
                        }

                        if (ddlConformationStatus.SelectedIndex == 0)
                        {
                            emp.ConformationStatus = "Null";
                        }

                        if (ddlReportingBoss.SelectedValue != "")
                        {
                            emp.ReportingEmpId = Convert.ToInt32(ddlReportingBoss.SelectedValue);
                        }




                        emp.IsProbationary = chkIsProbationary.Checked;
                        emp.IsProgramContractual = chkIsProgramContractual.Checked;
                        emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
                        emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
                        emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
                        emp.ImagePath = string.IsNullOrEmpty(hfSignature.Value)
                            ? null
                            : "C:\\inetpub\\wwwroot\\SMCHRIS\\UploadImg\\" + hfSignature.Value;
                        emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
                        emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?) null : int.Parse(hfJobID.Value);
                        emp.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;
                        emp.IsCompanyDirector = chkIsCompanyDirector.Checked;


                        emp.FatherDOB = string.IsNullOrEmpty(txtFatherDOB.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txtFatherDOB.Text).Date;

                        emp.MotherDOB = string.IsNullOrEmpty(txtMotherDOB.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txtMotherDOB.Text).Date;

                        #endregion end 

                        db.SaveChanges();



                        if (ddlEmpType.SelectedValue == "2")
                        {
                            DateTime ? ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            DateTime? DateOfJoin = null;
                            try
                            {
                                DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;  
                            }
                            catch (Exception)
                            {
                                DateOfJoin = null;
                                //throw;
                            }


                            _commonDataLoad.InsertContractHistoryEmpId(mid,DateOfJoin, ContractEndDate);

                        }


                        if (ProjectDropDownList.SelectedValue != "")
                        {

                            if (HFproDtlId.Value != "")
                            {
                                bool area = UpdateAreaInformation(PrepareDataForUpdate());


                                dal.UpdatenfoDetail(HFproDtlId.Value, ProjectDropDownList.SelectedValue, true);
                            }
                            else
                            {
                                ProjectWiseEmployeeAllocationDAO aEmpTransferAndRedesignationDao =
                                    new ProjectWiseEmployeeAllocationDAO();

                                //    aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                                aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(emp.EmpInfoId);



                                aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                                aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;
                                int ProjectId = dal.SaveInfo(aEmpTransferAndRedesignationDao);



                                ProjectWiseEmployeeAllocationDetailDAO andRedesignationDao = new ProjectWiseEmployeeAllocationDetailDAO
                                    ()
                                {

                                    EmployeeWiseProjectAllocationMasterId = ProjectId,
                                    ProjectId = Convert.ToInt32(ProjectDropDownList.SelectedValue),
                                    IsActive = true,
                                    IsMaster = true,
                                };
                                int idd =
                                    dal.SaveInfoDetails(andRedesignationDao);
                            }

                        }



                        for (int i = 0; i < loadGridView.Rows.Count; i++)
                        {
                            _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                                emp.EmpInfoId.ToString());
                        }
                        string[] delid = delEmpIdHiddenField.Value.Split(',');
                        foreach (string id in delid)
                        {
                            _commonDataLoad.UpdateReportingEmpId(null,
                                id);
                        }


                        if (ddlFinalApprover.SelectedValue != "")
                        {
                            SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO();
                            {
                                appDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                appDao.EmpInfoId =
                                    Convert.ToInt32(
                                        ddlFinalApprover.SelectedValue);
                                //appDao.MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString());
                                appDao.SuperMenuAppId =
                                    string.IsNullOrEmpty(hfSuperMenuAppId.Value)
                                        ? 0
                                        : Convert.ToInt32(hfSuperMenuAppId.Value);

                                appDao.FromEmpInfoId = Convert.ToInt32(emp.EmpInfoId);


                                appDao.IsAllEmployee = chkIsAllEmployee.Checked;


                            }
                            int id = appDal.SaveSupervisorApp(appDao);
                        }
                    }
                    else
                    {
////Start New Mode
                        emp = new tblEmpGeneralInfo();

                        #region 1. General Information



                        emp.RecruitmentTypeNew = false;
                        try
                        {
                            emp.RecruitmentTypeNew = rbRecruitmentType.Items[0].Selected;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        emp.RecruitmentTypeReplacement = false;
                        try
                        {
                            emp.RecruitmentTypeReplacement = rbRecruitmentType.Items[1].Selected;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        emp.IsActive = true;
                        emp.EmployeeStatus = "Active";
                        emp.EntryBy = _userId;
                        emp.EntryDate = DateTime.Now;
                        emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?) null;
                        emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
                        emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
                        emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
                        emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;
                        emp.DivisionId = ddlDivision.SelectedIndex > 0
                            ? int.Parse(ddlDivision.SelectedValue)
                            : (int?) null;

                        emp.leaveRecommenderId = ddlleaveRecommender.SelectedIndex > 0
                            ? int.Parse(ddlleaveRecommender.SelectedValue)
                            : (int?) null;
                        emp.LeaveApprovalId = ddlLeaveApproval.SelectedIndex > 0
                            ? int.Parse(ddlLeaveApproval.SelectedValue)
                            : (int?) null;
                        emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
                        emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0
                            ? int.Parse(ddlEmpFOccupation.SelectedValue)
                            : (int?) null;
                        emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
                        emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0
                            ? int.Parse(ddlEmpMOccupation.SelectedValue)
                            : (int?) null;

                        emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDOB.Text).Date;
                        emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;
                        emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
                        emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0
                            ? ddlEmpMaritalStatus.SelectedValue
                            : null;

                        emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?) null;
                        emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text)
                            ? null
                            : txt_EmpPlaceOfBirth.Text;
                        emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;

                        if (chkIsPreviousEmp.Checked)
                        {
                            emp.ReferenceID = ddlEmpPrevious.SelectedIndex > 0 ? int.Parse(ddlEmpPrevious.SelectedValue) : (int?)null;
                             
                        }

                        emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
                        if (txtContractualPreiod.Text != "")
                        {
                            emp.ContractPeriod = Convert.ToInt32(txtContractualPreiod.Text);
                        }

                        emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?) null : int.Parse(hfJobID.Value);

                        if (ddlEmpType.SelectedValue == "2")
                        {
                            emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?) null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            try
                            {
                                emp.ContractStartDate = emp.DateOfJoin;
                            }
                            catch (Exception)
                            {
                                emp.ContractStartDate = null;
                                //throw;
                            }
                            //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;

                            //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
                            //{
                            //    if (cbl_ContractProject.Items[i].Selected)
                            //    {
                            //        var ContractProject = new tblEmpContractProject();
                            //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
                            //        db.tblEmpContractProjects.Add(ContractProject);
                            //    }
                            //}
                        }
                        emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
                        emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text)
                            ? null
                            : txt_EmpExpectedServiceLength.Text;
                        emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

                        emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;
                        //emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;


                        if (ddlConformationStatus.SelectedItem.Text == "Yes")
                        {
                            emp.ConformationStatus = "1";
                        }

                        if (ddlConformationStatus.SelectedItem.Text == "No")
                        {
                            emp.ConformationStatus = "0";
                        }

                        if (ddlConformationStatus.SelectedIndex == 0)
                        {
                            emp.ConformationStatus = "Null";
                        }

                        if (ddlReportingBoss.SelectedValue != "")
                        {
                            emp.ReportingEmpId = Convert.ToInt32(ddlReportingBoss.SelectedValue);
                        }



                        emp.IsProbationary = chkIsProbationary.Checked;
                        emp.IsProgramContractual = chkIsProgramContractual.Checked;
                        emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
                        emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
                        emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
                        emp.ImagePath = string.IsNullOrEmpty(hfSignature.Value)
                            ? null
                            : "C:\\inetpub\\wwwroot\\SMCHRIS\\UploadImg\\" + hfSignature.Value;
                        emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
                        emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?) null : int.Parse(hfJobID.Value);
                        emp.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;

                        emp.FatherDOB = string.IsNullOrEmpty(txtFatherDOB.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txtFatherDOB.Text).Date;

                        emp.MotherDOB = string.IsNullOrEmpty(txtMotherDOB.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txtMotherDOB.Text).Date;

                        #endregion end 1. General Information




                        db.tblEmpGeneralInfoes.Add(emp);
                        db.SaveChanges();

                        if (chkIsPreviousEmp.Checked)
                        {
                            if (chkSpecialTransfer.Checked)
                            {
                                try
                                {
                                    SPTransferInsert((emp));
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                            }
                        }
                        else
                        {
                            if (chkSpecialTransfer.Checked)
                            {
                                try
                                {
                                    SPTransferInsert((emp));
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                            }
                        }

                        // Project

                        if (ProjectDropDownList.SelectedValue != "")
                        {


                            ProjectWiseEmployeeAllocationDAO aEmpTransferAndRedesignationDao =
                                new ProjectWiseEmployeeAllocationDAO();

                            //    aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                            aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(emp.EmpInfoId);



                            aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                            aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;
                            int ProjectId = dal.SaveInfo(aEmpTransferAndRedesignationDao);



                            ProjectWiseEmployeeAllocationDetailDAO andRedesignationDao = new ProjectWiseEmployeeAllocationDetailDAO
                                ()
                            {

                                EmployeeWiseProjectAllocationMasterId = ProjectId,
                                ProjectId = Convert.ToInt32(ProjectDropDownList.SelectedValue),
                                IsActive = true,
                                IsMaster = true,
                            };
                            int idd =
                                dal.SaveInfoDetails(andRedesignationDao);
                        }
                        EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                        ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                        /// 
                        /// 
                        /// 


                        if (ddlEmpType.SelectedValue == "2")
                        {
                            DateTime? ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            DateTime? DateOfJoin = null;
                            try
                            {
                                DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;
                            }
                            catch (Exception)
                            {
                                DateOfJoin = null;
                                //throw;
                            }


                            _commonDataLoad.InsertContractHistoryEmpId(Convert.ToInt32(emp.EmpInfoId), DateOfJoin, ContractEndDate);

                        }

                        if (ddlCompany.SelectedValue == "1")
                        {
                            if (chkIsCompanyDirector.Checked)
                            {
                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForsCompanyDirector(emp.EmpInfoId))
                                {
                                    if (dtEmpCode.Rows.Count > 0)
                                    {
                                        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                    }
                                }
                            }
                            else if (FundedProjectsCheckBox1.Checked)
                            {
                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForIsSMCFundedProjects(emp.EmpInfoId))
                                {
                                    if (dtEmpCode.Rows.Count > 0)
                                    {
                                        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(emp.EmpInfoId))
                                {
                                    if (dtEmpCode.Rows.Count > 0)
                                    {
                                        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(emp.EmpInfoId))
                            {
                                if (dtEmpCode.Rows.Count > 0)
                                {
                                    EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                }
                            }
                        }
                       
                        for (int i = 0; i < loadGridView.Rows.Count; i++)
                        {
                            _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                                emp.EmpInfoId.ToString());
                        }

                        if (ddlFinalApprover.SelectedValue!="")
                        {
                            SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO();
                            {
                                appDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                appDao.EmpInfoId =
                                    Convert.ToInt32(
                                        ddlFinalApprover.SelectedValue);
                                //appDao.MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString());
                                appDao.SuperMenuAppId =
                               string.IsNullOrEmpty(hfSuperMenuAppId.Value)
                                   ? 0
                                   : Convert.ToInt32(hfSuperMenuAppId.Value);
                        
                                appDao.FromEmpInfoId = Convert.ToInt32(emp.EmpInfoId);


                                appDao.IsAllEmployee = chkIsAllEmployee.Checked;


                            }
                            int id = appDal.SaveSupervisorApp(appDao);
                        }
                      
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmployeeInfoList.aspx';",
                   true);
            }
            catch (Exception ex)
            {
                AlertMessageBoxShow(ex.Message);
            }
               #endregion
        }
    }


    EmployeeProfileDAL ddd = new EmployeeProfileDAL();

    SupervisorMenuAppDAL ddddd = new SupervisorMenuAppDAL();
    public void ReportingEmpData(string empinfoid, DataTable aDataTable)
    {
        DataRow dataRow = null;
        DataTable dtdata1 = ddd.GetRefEmpInfoDAL2(empinfoid);
        DataTable dtdata = ddddd.LoadEmpGenInfoGetRef(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReferenceID"].ToString() + "' ");

        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["ReferenceID"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();

            aDataTable.Rows.Add(dataRow);

            ReportingEmpData(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable);
        }

    }
    private void SPTransferInsert(tblEmpGeneralInfo hhhh)
    {
        try
        {
            DataTable dtSpCheck =new DataTable();
            try
            {
                dtSpCheck = aContractualEmpManageDAL.GetSpTransferCheckByEmpId(ddlEmpPrevious.SelectedValue);
            }
            catch (Exception)
            {
                
                //throw;
            }

            if (dtSpCheck.Rows.Count > 0)
            {
                int smc = 0;
                int smcEL = 0;

                try
                {
                    smc = Convert.ToInt32(dtSpCheck.Rows[0]["IsSMCRecordView"].ToString());
                }
                catch (Exception)
                {
                    //throw;
                }

                try
                {
                    smcEL = Convert.ToInt32(dtSpCheck.Rows[0]["IsELRecordView"].ToString());
                }
                catch (Exception)
                {
                    //throw;
                }

                aEmpTransferAndRedesignationDal.EmpSpecialTransferInsertSelect(Convert.ToInt32(hhhh.EmpInfoId),
                    Convert.ToInt32(ddlEmpPrevious.SelectedValue));
                EmployeeProfileDAL aEmployeeInfoListReportDAL = new EmployeeProfileDAL();


                string rptTypeIdMul = "";
                DataTable dtref = aEmployeeInfoListReportDAL.GetRefEmpInfoDAL(hhhh.EmpInfoId.ToString());
                if (dtref.Rows.Count > 0)
                {
                    DataTable aDataTable = new DataTable();
                    aDataTable.Columns.Add("EmpInfoId");

                    DataRow dataRow = null;
                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = "0";

                    aDataTable.Rows.Add(dataRow);
                    ReportingEmpData(hhhh.EmpInfoId.ToString(), dtref);
                    string myId = "";
                    for (int i = 0; i < dtref.Rows.Count; i++)
                    {
                        myId += dtref.Rows[i]["ReferenceID"].ToString().Trim() + ",";
                    }


                    myId = myId.Trim().TrimEnd(',');
                    rptTypeIdMul = hhhh.EmpInfoId.ToString() + "," + myId.Trim();
                }


                string[] instituteList = rptTypeIdMul.Split(',');


                if (instituteList.Length > 0)
                {
                    foreach (String item in instituteList)
                    {
                        if (item != "")
                        {
                            if (smc == 1)
                            {
                                aEmpTransferAndRedesignationDal.InsertMappinigEmpRefferId(item, hhhh.EmpInfoId.ToString(), 1);
                            }

                            if (smcEL == 1)
                            {
                                aEmpTransferAndRedesignationDal.InsertMappinigEmpRefferId(item, hhhh.EmpInfoId.ToString(), 2);
                            }
                        }
                    }
                }
            }


            else
            {

                int? OldEmployeeId = null; int? NewEmployeeId = null; int? NewComId = null;

                try
                {
                    NewEmployeeId = Convert.ToInt32(hhhh.EmpInfoId);
                    try
                    {
                        OldEmployeeId = Convert.ToInt32(ddlEmpPrevious.SelectedValue);
                    }
                    catch (Exception)
                    {
                        OldEmployeeId = NewEmployeeId;
                        //throw;
                    }
                    

                    NewComId = Convert.ToInt32(ddlCompany.SelectedValue);
                }
                catch (Exception)
                {

                    //throw;
                }

                int smc = 0;
                int smcEL = 0;

                try
                {
                    if (ddlCompany.SelectedValue == "1")
                    {
                        smc = 1;
                    }

                }
                catch (Exception)
                {
                    //throw;
                }

                try
                {
                    if (ddlCompany.SelectedValue == "2")
                    {

                        smcEL =1;
                    }
                }
                catch (Exception)
                {
                    //throw;
                }

                aEmpTransferAndRedesignationDal.WithoutEmpSpecialTransferEmpGen(OldEmployeeId, NewEmployeeId, NewComId, smc, smcEL);

                if (smc == 1)
                {
                    aEmpTransferAndRedesignationDal.InsertMappinigEmpRefferId(NewEmployeeId.ToString(), hhhh.EmpInfoId.ToString(), 1);
                }

                if (smcEL == 1)
                {
                    aEmpTransferAndRedesignationDal.InsertMappinigEmpRefferId(NewEmployeeId.ToString(), hhhh.EmpInfoId.ToString(), 2);
                }
            }

        }
        catch (Exception)
        {
            //throw;
        }
    }
    private ProjectWiseEmployeeAllocationDAO PrepareDataForUpdate()
    {
        var aEmpTransferAndRedesignationDao = new ProjectWiseEmployeeAllocationDAO();

        aEmpTransferAndRedesignationDao.EmployeeWiseProjectAllocationMasterId = Convert.ToInt32(HFproMasId.Value);
        aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(mid);



        aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
        aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;

        return aEmpTransferAndRedesignationDao;
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
    ProjectWiseEmployeeAllocationEntryDAL dal = new ProjectWiseEmployeeAllocationEntryDAL();

    private bool Validation()
    {

        if (ddlCompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company Name", this);
            ddlCompany.Focus();
            return false;
        }
        if (ddlDivision.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Division", this);
            ddlDivision.Focus();
            return false;
        }
        if (txt_EmpName.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Employee Name", this);
            txt_EmpName.Focus();
            return false;
        }
        //if (txt_EmpExpectedServiceLength.Text == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Enter Service Length ", this);
        //    txt_EmpName.Focus();
        //    return false;
        //}


        if (txt_EmpDOB.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Select Date of Birth", this);
            txt_EmpDOB.Focus();
            return false;
        }

        if (txt_EmpDOJ.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Select Joining Date ", this);
            txt_EmpDOJ.Focus();
            return false;
        }
        if (ddlConformationStatus.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Confirmation Status", this);
            ddlConformationStatus.Focus();
            return false;
        }







        if (ddlEmpType.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Empployee Type", this);
            ddlEmpType.Focus();
            return false;
        }


        if (chkIsProbationary.Checked)
        {

            if (txt_ProbationaryEndDate.Text=="") 
            {
                aShowMessage.ShowMessageBox("Probationary End Date is Required!!", this);
                txt_ProbationaryEndDate.Focus();
                return false;     
            }
           
        }
        //if (ddlFinalApprover.SelectedIndex <= 0)
        //{
        //    aShowMessage.ShowMessageBox("Please Select Leave Approval", this);
        //    ddlFinalApprover.Focus();
        //    return false;
        //}
 

        return true;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void btn_Next_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            #region fff

            try
            {
                string MasterId = string.Empty;
                string EmpMasterCode = string.Empty;
                mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
                tblEmpGeneralInfo emp = null;
                using (var db = new HRIS_SMCEntities())
                {
                    if (mid > 0)
                    {

                        emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        EmpMasterCode = emp.EmpMasterCode;

                        MasterId = emp.EmpInfoId.ToString();
                        emp.IsActive = true;
                        emp.EmployeeStatus = "Active";
                        emp.Updateby = _userId;
                        emp.UpdateDate = DateTime.Now;


                        emp.RecruitmentTypeNew = false;
                        try
                        {
                            emp.RecruitmentTypeNew = rbRecruitmentType.Items[0].Selected;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        emp.RecruitmentTypeReplacement = false;
                        try
                        {
                            emp.RecruitmentTypeReplacement = rbRecruitmentType.Items[1].Selected;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        #region 1. General Information
                        emp.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;

                        emp.leaveRecommenderId = ddlleaveRecommender.SelectedIndex > 0 ? int.Parse(ddlleaveRecommender.SelectedValue) : (int?)null;
                        emp.LeaveApprovalId = ddlLeaveApproval.SelectedIndex > 0 ? int.Parse(ddlLeaveApproval.SelectedValue) : (int?)null;
                        emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
                        emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
                        emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
                        emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

                        emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
                        emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0
                            ? int.Parse(ddlEmpFOccupation.SelectedValue)
                            : (int?)null;
                        emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
                        emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0
                            ? int.Parse(ddlEmpMOccupation.SelectedValue)
                            : (int?)null;

                        emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDOB.Text).Date;
                        emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;
                        emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
                        emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0
                            ? ddlEmpMaritalStatus.SelectedValue
                            : null;

                        emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                        emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text)
                            ? null
                            : txt_EmpPlaceOfBirth.Text;
                        emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
                        emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;

                        if (txtContractualPreiod.Text != "")
                        {
                            emp.ContractPeriod = Convert.ToInt32(txtContractualPreiod.Text);
                        }

                        if (ddlEmpType.SelectedValue == "2")
                        {
                            emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            try
                            {
                                emp.ContractStartDate = emp.DateOfJoin;
                            }
                            catch (Exception)
                            {
                                emp.ContractStartDate = null;
                                //throw;
                            }
                            //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;
                            ////TODO
                            //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
                            //{
                            //    if (cbl_ContractProject.Items[i].Selected)
                            //    {
                            //        var ContractProject = new tblEmpContractProject();
                            //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
                            //        db.tblEmpContractProjects.Add(ContractProject);
                            //    }
                            //}
                        }
                        emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
                        emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text)
                            ? null
                            : txt_EmpExpectedServiceLength.Text;
                        emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

                        emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;

                        //  emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;

                        if (ddlConformationStatus.SelectedItem.Text == "Yes")
                        {
                            emp.ConformationStatus = "1";
                        }

                        if (ddlConformationStatus.SelectedItem.Text == "No")
                        {
                            emp.ConformationStatus = "0";
                        }

                        if (ddlConformationStatus.SelectedIndex == 0)
                        {
                            emp.ConformationStatus = "Null";
                        }


                        if (ddlReportingBoss.SelectedValue != "")
                        {
                            emp.ReportingEmpId = Convert.ToInt32(ddlReportingBoss.SelectedValue);
                        }
                    
                      

                        emp.IsProbationary = chkIsProbationary.Checked;
                        emp.IsProgramContractual = chkIsProgramContractual.Checked;
                        emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
                        emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
                        emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
                        emp.ImagePath = string.IsNullOrEmpty(hfSignature.Value) ? null : "C:\\inetpub\\wwwroot\\SMCHRIS\\UploadImg\\" + hfSignature.Value;
                        emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
                        emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?)null : int.Parse(hfJobID.Value);
                        emp.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;
                        emp.IsCompanyDirector = chkIsCompanyDirector.Checked;

                        emp.FatherDOB = string.IsNullOrEmpty(txtFatherDOB.Text)
                         ? (DateTime?)null
                         : DateTime.Parse(txtFatherDOB.Text).Date;

                        emp.MotherDOB = string.IsNullOrEmpty(txtMotherDOB.Text)
                         ? (DateTime?)null
                         : DateTime.Parse(txtMotherDOB.Text).Date;
                        #endregion end

                        db.SaveChanges();


                        if (ddlEmpType.SelectedValue == "2")
                        {
                            DateTime? ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            DateTime? DateOfJoin = null;
                            try
                            {
                                DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;
                            }
                            catch (Exception)
                            {
                                DateOfJoin = null;
                                //throw;
                            }


                            _commonDataLoad.InsertContractHistoryEmpId(mid, DateOfJoin, ContractEndDate);

                        }
                        if (ProjectDropDownList.SelectedValue != "")
                        {

                            if (HFproDtlId.Value!="")
                            {
                                bool area = UpdateAreaInformation(PrepareDataForUpdate());


                                dal.UpdatenfoDetail(HFproDtlId.Value, ProjectDropDownList.SelectedValue, true);
                            }
                            else
                            {
                                ProjectWiseEmployeeAllocationDAO aEmpTransferAndRedesignationDao = new ProjectWiseEmployeeAllocationDAO();

                                //    aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                                aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(emp.EmpInfoId);



                                aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                                aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;
                                int ProjectId = dal.SaveInfo(aEmpTransferAndRedesignationDao);



                                ProjectWiseEmployeeAllocationDetailDAO andRedesignationDao = new ProjectWiseEmployeeAllocationDetailDAO
                                    ()
                                {

                                    EmployeeWiseProjectAllocationMasterId = ProjectId,
                                    ProjectId = Convert.ToInt32(ProjectDropDownList.SelectedValue),
                                    IsActive = true,
                                    IsMaster = true,

                                };
                                int idd =
                                    dal.SaveInfoDetails(andRedesignationDao);
                            }
                          
                        }

                        for (int i = 0; i < loadGridView.Rows.Count; i++)
                        {
                            _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                                emp.EmpInfoId.ToString());
                        }
                        string[] delid = delEmpIdHiddenField.Value.Split(',');
                        foreach (string id in delid)
                        {
                            _commonDataLoad.UpdateReportingEmpId(null,
                                id);
                        }

                        if (ddlFinalApprover.SelectedValue != "")
                        {
                            SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO();
                            {
                                appDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                appDao.EmpInfoId =
                                    Convert.ToInt32(
                                        ddlFinalApprover.SelectedValue);
                                //appDao.MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString());
                                appDao.SuperMenuAppId =
                               string.IsNullOrEmpty(hfSuperMenuAppId.Value)
                                   ? 0
                                   : Convert.ToInt32(hfSuperMenuAppId.Value);

                                appDao.FromEmpInfoId = Convert.ToInt32(emp.EmpInfoId);


                                appDao.IsAllEmployee = chkIsAllEmployee.Checked;


                            }
                            int id = appDal.SaveSupervisorApp(appDao);
                        }
                    }
                    else
                    {
                      
                        ////Start New Mode
                        emp = new tblEmpGeneralInfo();
                        
                        #region 1. General Information
                        emp.IsActive = true;
                        emp.EmployeeStatus = "Active";
                        emp.EntryBy = _userId;
                        emp.EntryDate = DateTime.Now;


                        emp.RecruitmentTypeNew = false;
                        try
                        {
                            emp.RecruitmentTypeNew = rbRecruitmentType.Items[0].Selected;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        emp.RecruitmentTypeReplacement = false;
                        try
                        {
                            emp.RecruitmentTypeReplacement = rbRecruitmentType.Items[1].Selected;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
                        emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
                        emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
                        emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;
                        emp.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;

                        emp.leaveRecommenderId = ddlleaveRecommender.SelectedIndex > 0 ? int.Parse(ddlleaveRecommender.SelectedValue) : (int?)null;
                        emp.LeaveApprovalId = ddlLeaveApproval.SelectedIndex > 0 ? int.Parse(ddlLeaveApproval.SelectedValue) : (int?)null;

                        emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
                        emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpFOccupation.SelectedValue) : (int?)null;
                        emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
                        emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpMOccupation.SelectedValue) : (int?)null;

                        emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOB.Text).Date;
                        emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOJ.Text).Date;
                        emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
                        emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0 ? ddlEmpMaritalStatus.SelectedValue : null;

                        emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                        emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text) ? null : txt_EmpPlaceOfBirth.Text;
                        emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
                        emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
                        if (txtContractualPreiod.Text != "")
                        {
                            emp.ContractPeriod = Convert.ToInt32(txtContractualPreiod.Text);
                        }
                        if (ddlEmpType.SelectedValue == "2")
                        {
                            emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            try
                            {
                                emp.ContractStartDate = emp.DateOfJoin;
                            }
                            catch (Exception)
                            {
                                emp.ContractStartDate = null;
                                //throw;
                            }
                            //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;

                            //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
                            //{
                            //    if (cbl_ContractProject.Items[i].Selected)
                            //    {
                            //        var ContractProject = new tblEmpContractProject();
                            //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
                            //        db.tblEmpContractProjects.Add(ContractProject);
                            //    }
                            //}
                        }
                        emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
                        emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text) ? null : txt_EmpExpectedServiceLength.Text;
                        emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

                        emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;
                        //emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;


                        if (ddlConformationStatus.SelectedItem.Text == "Yes")
                        {
                            emp.ConformationStatus = "1";
                        }

                        if (ddlConformationStatus.SelectedItem.Text == "No")
                        {
                            emp.ConformationStatus = "0";
                        }

                        if (ddlConformationStatus.SelectedIndex == 0)
                        {
                            emp.ConformationStatus = "Null";
                        }

                        if (ddlReportingBoss.SelectedValue != "")
                        {
                            emp.ReportingEmpId = Convert.ToInt32(ddlReportingBoss.SelectedValue);
                        }
                    
                      

                        emp.IsProbationary = chkIsProbationary.Checked;
                        emp.IsProgramContractual = chkIsProgramContractual.Checked;
                        emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
                        emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
                        emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
                        emp.ImagePath = string.IsNullOrEmpty(hfSignature.Value) ? null : "C:\\inetpub\\wwwroot\\SMCHRIS\\UploadImg\\" + hfSignature.Value;
                        emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
                        emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?)null : int.Parse(hfJobID.Value);
                        emp.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;

                        emp.FatherDOB = string.IsNullOrEmpty(txtFatherDOB.Text)
                         ? (DateTime?)null
                         : DateTime.Parse(txtFatherDOB.Text).Date;

                        emp.MotherDOB = string.IsNullOrEmpty(txtMotherDOB.Text)
                         ? (DateTime?)null
                         : DateTime.Parse(txtMotherDOB.Text).Date;

                        if (chkIsPreviousEmp.Checked)
                        {
                            emp.ReferenceID = ddlEmpPrevious.SelectedIndex > 0 ? int.Parse(ddlEmpPrevious.SelectedValue) : (int?)null;



                        }
                        #endregion end 1. General Information
                        db.tblEmpGeneralInfoes.Add(emp);
                        db.SaveChanges();

                        if (ddlEmpType.SelectedValue == "2")
                        {
                            DateTime? ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txt_ContractEndDate.Text).Date;

                            DateTime? DateOfJoin = null;
                            try
                            {
                                DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txt_EmpDOJ.Text).Date;
                            }
                            catch (Exception)
                            {
                                DateOfJoin = null;
                                //throw;
                            }


                            _commonDataLoad.InsertContractHistoryEmpId(Convert.ToInt32(emp.EmpInfoId), DateOfJoin, ContractEndDate);

                        }
                        if (chkIsPreviousEmp.Checked)
                        {
                            if (chkSpecialTransfer.Checked)
                            {
                                try
                                {
                                    SPTransferInsert((emp));
                                }
                                catch (Exception)
                                {
                                    
                                    //throw;
                                }
                            }
                        }
                        else
                        {
                            if (chkSpecialTransfer.Checked)
                            {
                                try
                                {
                                    SPTransferInsert((emp));
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                            }
                        }

                        EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                        ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                        /// 
                        /// 
                        /// 


                        if (ProjectDropDownList.SelectedValue != "")
                        {


                            ProjectWiseEmployeeAllocationDAO aEmpTransferAndRedesignationDao = new ProjectWiseEmployeeAllocationDAO();

                            //    aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                            aEmpTransferAndRedesignationDao.EmpInfoId = Convert.ToInt32(emp.EmpInfoId);



                            aEmpTransferAndRedesignationDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                            aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;
                            int ProjectId = dal.SaveInfo(aEmpTransferAndRedesignationDao);



                            ProjectWiseEmployeeAllocationDetailDAO andRedesignationDao = new ProjectWiseEmployeeAllocationDetailDAO
                                ()
                            {

                                EmployeeWiseProjectAllocationMasterId = ProjectId,
                                ProjectId = Convert.ToInt32(ProjectDropDownList.SelectedValue),
                                IsActive = true,
                                IsMaster = true,
                            };
                            int idd =
                                dal.SaveInfoDetails(andRedesignationDao);
                        }

                         MasterId = emp.EmpInfoId.ToString();

                        if (ddlCompany.SelectedValue=="1")
                        {
                            if (chkIsCompanyDirector.Checked)
                            {
                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForsCompanyDirector(emp.EmpInfoId))
                                {
                                    if (dtEmpCode.Rows.Count > 0)
                                    {
                                        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                    }
                                }
                            }
                            else if (FundedProjectsCheckBox1.Checked)
                            {
                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForIsSMCFundedProjects(emp.EmpInfoId))
                                {
                                    if (dtEmpCode.Rows.Count > 0)
                                    {
                                        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(emp.EmpInfoId))
                                {
                                    if (dtEmpCode.Rows.Count > 0)
                                    {
                                        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(emp.EmpInfoId))
                            {
                                if (dtEmpCode.Rows.Count > 0)
                                {
                                    EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                                }
                            }
                        }
                        
                       
                        for (int i = 0; i < loadGridView.Rows.Count; i++)
                        {
                            _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                                emp.EmpInfoId.ToString());
                        }


                        if (ddlFinalApprover.SelectedValue != "")
                        {
                            SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO();
                            {
                                appDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                appDao.EmpInfoId =
                                    Convert.ToInt32(
                                        ddlFinalApprover.SelectedValue);
                                //appDao.MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString());
                                appDao.SuperMenuAppId =
                               string.IsNullOrEmpty(hfSuperMenuAppId.Value)
                                   ? 0
                                   : Convert.ToInt32(hfSuperMenuAppId.Value);

                                appDao.FromEmpInfoId = Convert.ToInt32(emp.EmpInfoId);


                                appDao.IsAllEmployee = chkIsAllEmployee.Checked;


                            }
                            int id = appDal.SaveSupervisorApp(appDao);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpEmploymentInformation.aspx?mid=" + MasterId + "';",
                   true);
            }
            catch (Exception ex)
            {
                AlertMessageBoxShow(ex.Message);
            }
            #endregion
        }
    }
    protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txt_JobCirculation.Text))
            {
                string job = txt_JobCirculation.Text;
                hfJobID.Value = job.Split(':')[0];
                txt_JobCirculation.Text = job.Split(':')[2];
                //txt_JobTitle.Text = job.Split(':')[2];
            }
        }
        catch (Exception ex)
        {
            txt_JobCirculation.Text = "";

        }
    }
    //protected void btnSaveExitt_OnClick(object sender, EventArgs e)
    //{
    //    if (Validation())
    //    {
    //        #region fff

    //        try
    //        {
    //            string MasterId = string.Empty;
    //            string EmpMasterCode = string.Empty;
    //            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
    //            tblEmpGeneralInfo emp = null;
    //            using (var db = new HRIS_SMCEntities())
    //            {
    //                if (mid > 0)
    //                {

    //                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
    //                    EmpMasterCode = emp.EmpMasterCode;

    //                    #region 1. General Information

    //                    emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
    //                    emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
    //                    emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
    //                    emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
    //                    emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

    //                    emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
    //                    emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0
    //                        ? int.Parse(ddlEmpFOccupation.SelectedValue)
    //                        : (int?)null;
    //                    emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
    //                    emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0
    //                        ? int.Parse(ddlEmpMOccupation.SelectedValue)
    //                        : (int?)null;

    //                    emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDOB.Text).Date;
    //                    emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDOJ.Text).Date;
    //                    emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
    //                    emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0
    //                        ? ddlEmpMaritalStatus.SelectedValue
    //                        : null;

    //                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
    //                    emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text)
    //                        ? null
    //                        : txt_EmpPlaceOfBirth.Text;
    //                    emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
    //                    emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
    //                    if (ddlEmpType.SelectedValue == "2")
    //                    {
    //                        emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text)
    //                            ? (DateTime?)null
    //                            : DateTime.Parse(txt_ContractEndDate.Text).Date;
    //                        //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;
    //                        ////TODO
    //                        //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
    //                        //{
    //                        //    if (cbl_ContractProject.Items[i].Selected)
    //                        //    {
    //                        //        var ContractProject = new tblEmpContractProject();
    //                        //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
    //                        //        db.tblEmpContractProjects.Add(ContractProject);
    //                        //    }
    //                        //}
    //                    }
    //                    emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
    //                    emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text)
    //                        ? null
    //                        : txt_EmpExpectedServiceLength.Text;
    //                    emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

    //                    emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;

    //                    //  emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;

    //                    if (ddlConformationStatus.SelectedItem.Text == "Yes")
    //                    {
    //                        emp.ConformationStatus = "1";
    //                    }

    //                    if (ddlConformationStatus.SelectedItem.Text == "No")
    //                    {
    //                        emp.ConformationStatus = "0";
    //                    }

    //                    if (ddlConformationStatus.SelectedIndex == 0)
    //                    {
    //                        emp.ConformationStatus = "Null";
    //                    }


    //                    emp.ReportingEmpId = string.IsNullOrEmpty(hdReportingBoss.Value)
    //                        ? (int?)null
    //                        : int.Parse(hdReportingBoss.Value);

    //                    emp.IsProbationary = chkIsProbationary.Checked;
    //                    emp.IsProgramContractual = chkIsProgramContractual.Checked;
    //                    emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
    //                    emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
    //                    emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
    //                    emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
    //                    emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?)null : int.Parse(hfJobID.Value);
    //                    #endregion end

    //                    db.SaveChanges();

    //                    for (int i = 0; i < loadGridView.Rows.Count; i++)
    //                    {
    //                        _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
    //                            emp.EmpInfoId.ToString());
    //                    }
    //                    string[] delid = delEmpIdHiddenField.Value.Split(',');
    //                    foreach (string id in delid)
    //                    {
    //                        _commonDataLoad.UpdateReportingEmpId(null,
    //                            id);
    //                    }
    //                }
    //                else
    //                {

    //                    ////Start New Mode
    //                    emp = new tblEmpGeneralInfo();
    //                    MasterId = emp.EmpInfoId.ToString();
    //                    #region 1. General Information
    //                    emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
    //                    emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
    //                    emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
    //                    emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

    //                    emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
    //                    emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpFOccupation.SelectedValue) : (int?)null;
    //                    emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
    //                    emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpMOccupation.SelectedValue) : (int?)null;

    //                    emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOB.Text).Date;
    //                    emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOJ.Text).Date;
    //                    emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
    //                    emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0 ? ddlEmpMaritalStatus.SelectedValue : null;

    //                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
    //                    emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text) ? null : txt_EmpPlaceOfBirth.Text;
    //                    emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
    //                    emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
    //                    if (ddlEmpType.SelectedValue == "2")
    //                    {
    //                        emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ContractEndDate.Text).Date;
    //                        //emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;

    //                        //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
    //                        //{
    //                        //    if (cbl_ContractProject.Items[i].Selected)
    //                        //    {
    //                        //        var ContractProject = new tblEmpContractProject();
    //                        //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
    //                        //        db.tblEmpContractProjects.Add(ContractProject);
    //                        //    }
    //                        //}
    //                    }
    //                    emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
    //                    emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text) ? null : txt_EmpExpectedServiceLength.Text;
    //                    emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

    //                    emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;
    //                    //emp.ConformationStatus = ddlConformationStatus.SelectedIndex > 0 ? ddlConformationStatus.SelectedValue : null;


    //                    if (ddlConformationStatus.SelectedItem.Text == "Yes")
    //                    {
    //                        emp.ConformationStatus = "1";
    //                    }

    //                    if (ddlConformationStatus.SelectedItem.Text == "No")
    //                    {
    //                        emp.ConformationStatus = "0";
    //                    }

    //                    if (ddlConformationStatus.SelectedIndex == 0)
    //                    {
    //                        emp.ConformationStatus = "Null";
    //                    }

    //                    emp.ReportingEmpId = string.IsNullOrEmpty(hdReportingBoss.Value) ? (int?)null : int.Parse(hdReportingBoss.Value);

    //                    emp.IsProbationary = chkIsProbationary.Checked;
    //                    emp.IsProgramContractual = chkIsProgramContractual.Checked;
    //                    emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
    //                    emp.EmpImage = string.IsNullOrEmpty(hfempimg.Value) ? null : hfempimg.Value;
    //                    emp.NomineeImage = string.IsNullOrEmpty(hfNomineeImage.Value) ? null : hfNomineeImage.Value;
    //                    emp.EmpSign = string.IsNullOrEmpty(hfSignature.Value) ? null : hfSignature.Value;
    //                    #endregion end 1. General Information
    //                    emp.JobID = string.IsNullOrEmpty(hfJobID.Value) ? (int?)null : int.Parse(hfJobID.Value);
    //                    db.tblEmpGeneralInfoes.Add(emp);
    //                    db.SaveChanges();
    //                    EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
    //                    ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
    //                    using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(emp.EmpInfoId))
    //                    {
    //                        if (dtEmpCode.Rows.Count > 0)
    //                        {
    //                            EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
    //                        }

    //                    }
    //                    for (int i = 0; i < loadGridView.Rows.Count; i++)
    //                    {
    //                        _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
    //                            emp.EmpInfoId.ToString());
    //                    }
    //                }
    //            }

    //            ScriptManager.RegisterStartupScript(this, this.GetType(),
    //               "alert",
    //               "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmployeeInfoList.aspx';",
    //               true);
    //        }
    //        catch (Exception ex)
    //        {
    //            AlertMessageBoxShow(ex.Message);
    //        }
    //        #endregion
    //    }
    //}

    protected void txt_EmpDateOfConformation_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime doj;
            if (DateTime.TryParse(txt_EmpDateOfConformation.Text, out doj))
            {
                if (chkIsProbationary.Checked)
                {
                    txt_EmpDateOfConformation.Text = "";

                    ShowMessageBox("Please Uncheck Is Probationary!!!!");
                }
              


            }
            else
            {
                ShowMessageBox("Please Enter Valid Date!!!");
                txt_EmpDateOfConformation.Text = "";
            }
        }
        catch (Exception)
        {

            ShowMessageBox("Please Enter Valid Date!!!");
            txt_EmpDateOfConformation.Text = "";
        }
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            lblNext.Visible = true;
            Response.Redirect("EmpEmploymentInformation?mid=" + EmpId);
        }
        else
        {
            lblNext.Visible = false;
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }

    protected void txt_ProbationaryEndDate_OnTextChanged(object sender, EventArgs e)
    {
        if (!chkIsProbationary.Checked)
        {
            txt_ProbationaryEndDate.Text = "";

            ShowMessageBox("Please check Is Probationary!!!!");
        }
        
    }

    protected void txt_ContractEndDate_OnTextChanged(object sender, EventArgs e)
    {
        MonthCalculation();


    }

    private void MonthCalculation()
    {

        try
        {

            if (txt_EmpDOJ.Text!="" && txt_ContractEndDate.Text!="")
            {

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ar-EG");

                DateTime Birth = Convert.ToDateTime((txt_ContractEndDate.Text.ToString())).AddDays(1);
                DateTime Today = Convert.ToDateTime(txt_EmpDOJ.Text);


                TimeSpan Span = Birth - Today;


                DateTime Age = DateTime.MinValue + Span;


                // note: MinValue is 1/1/1 so we have to subtract...
                int Years = Age.Year - 1;
                int Months = Age.Month - 1;
                int Days = Age.Day - 1;

                int calforYeartToMonth = 0;
                if (Years > 0)
                {
                    calforYeartToMonth = 12 * Years;
                }


                if (calforYeartToMonth > 0)
                {
                    int fMonth = Months + calforYeartToMonth;
                    txtContractualPreiod.Text = fMonth.ToString();
                }
                else
                {
                    txtContractualPreiod.Text = Months.ToString();
                }
            }
        }
        catch (Exception)
        {
            
            //throw;
        }

    }

    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void btn_New_OnClick(object sender, EventArgs e)
    {
       
    }
 

     

    private void CheckboxList()
    {
      
        

         

        
    }

    protected void chkIsProgramContractual_OnCheckedChanged(object sender, EventArgs e)
    {
       
        if (chkIsProgramContractual.Checked)
        {
            ProjectDropDownList.Enabled = true;
            dal.LoaProjectByCheckDropDownList(ProjectDropDownList, ddlCompany.SelectedValue, " and  IsOtherProject=1");
        }
        else
        {
           // ProjectDropDownList.Enabled = false;
           //ProjectDropDownList.Items.Clear();
        }
    }

    protected void FundedProjectsCheckBox1_OnCheckedChanged(object sender, EventArgs e)
    {
        if (FundedProjectsCheckBox1.Checked)
        {
            ProjectDropDownList.Enabled = true;
            dal.LoaProjectByCheckDropDownList(ProjectDropDownList, ddlCompany.SelectedValue, " and  IsSMCFundedProjects=1");
        }
        else
        {
            // ProjectDropDownList.Enabled = false;
            //ProjectDropDownList.Items.Clear();
        }
    }

    protected void chkSmcContract_OnCheckedChanged(object sender, EventArgs e)
    {

        if (chkSmcContract.Checked)
        {
            ProjectDropDownList.Enabled = false;

            ProjectDropDownList.Items.Clear();
        }
        
    }

    protected void chkIsCompanyDirector_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkIsCompanyDirector.Checked)
        {
            ProjectDropDownList.Enabled = false;

            ProjectDropDownList.Items.Clear();
        }
                
         
    }
    SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();

    protected void chkIsAllEmployee_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtdata = appDal.LoadEmpGenInfo(" and e.EmpInfoId=" + Request.QueryString["mid"].ToString());


            if (dtdata.Rows.Count > 0)
            {

                if (dtdata.Rows[0]["SuperMenuAppId"].ToString() != "")
                {
                    // chkIsAllEmployee.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsAllEmployee"].ToString());
                    if (chkIsAllEmployee.Checked)
                    {
                        using (
                            DataTable dt =
                                _commonDataLoad.GetEmpDDLForEntryByGrade()
                            )
                        {



                            try
                            {
                                ddlFinalApprover.DataSource = dt;
                                ddlFinalApprover.DataValueField = "EmpInfoId";
                                ddlFinalApprover.DataTextField = "EmpName";
                                ddlFinalApprover.DataBind();
                                ddlFinalApprover.Items.Insert(0,
                                    new ListItem("Please Select an Employee.....", String.Empty));
                                ddlFinalApprover.SelectedIndex = 0;
                            }
                            catch (Exception)
                            {

                                //throw;
                            }
                            try
                            {
                                ddlFinalApprover.SelectedValue =
                                                   dtdata.Rows[0]["EmpInfoId"].ToString();
                            }
                            catch (Exception)
                            {
                                ddlFinalApprover.SelectedIndex = 0;
                                //throw;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            DataTable aDataTable = new DataTable();
                            aDataTable.Columns.Add("EmpInfoId");
                            aDataTable.Columns.Add("EmpName");
                            aDataTable.Columns.Add("EmpMasterCode");
                            DataRow dataRow = null;
                            dataRow = aDataTable.NewRow();
                            dataRow["EmpInfoId"] = "0";
                            dataRow["EmpName"] = "Please Select an Employee.....";
                            dataRow["EmpMasterCode"] = "";
                            aDataTable.Rows.Add(dataRow);
                            try
                            {
                                appDal.ReportingEmpData(Request.QueryString["mid"].ToString(), aDataTable);
                            }
                            catch (Exception)
                            {

                                //throw;
                            }

                            ddlFinalApprover.DataValueField = "EmpInfoId";
                            ddlFinalApprover.DataTextField = "EmpName";
                            ddlFinalApprover.DataSource = aDataTable;
                            ddlFinalApprover.DataBind();
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                        try
                        {
                            ddlFinalApprover.SelectedValue = dtdata.Rows[0]["EmpInfoId"].ToString();
                        }
                        catch (Exception)
                        {
                            ddlFinalApprover.SelectedIndex = 0;
                            //throw;
                        }
                    }
                    hfSuperMenuAppId.Value = dtdata.Rows[0]["SuperMenuAppId"].ToString();
                    try
                    {
                        ddlFinalApprover.SelectedValue = dtdata.Rows[0]["EmpInfoId"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                }
                else
                {

                    try
                    {
                        DataTable aDataTable = new DataTable();
                        aDataTable.Columns.Add("EmpInfoId");
                        aDataTable.Columns.Add("EmpName");
                        aDataTable.Columns.Add("EmpMasterCode");
                        DataRow dataRow = null;
                        dataRow = aDataTable.NewRow();
                        dataRow["EmpInfoId"] = "0";
                        dataRow["EmpName"] = "Please Select an Employee.....";
                        dataRow["EmpMasterCode"] = "";
                        aDataTable.Rows.Add(dataRow);
                        appDal.ReportingEmpData(Request.QueryString["mid"].ToString().ToString(), aDataTable);

                        ddlFinalApprover.DataValueField = "EmpInfoId";
                        ddlFinalApprover.DataTextField = "EmpName";
                        ddlFinalApprover.DataSource = aDataTable;
                        ddlFinalApprover.DataBind();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    try
                    {
                        ddlFinalApprover.SelectedValue =
                               dtdata.Rows[0]["EmpInfoId"].ToString();
                    }
                    catch (Exception)
                    {
                        ddlFinalApprover.SelectedIndex = 0;
                        //throw;
                    }

                }

            }

            else
            {

                if (ddlReportingBoss.SelectedValue != "")
                {
                    try
                    {
                        DataTable aDataTable = new DataTable();
                        aDataTable.Columns.Add("EmpInfoId");
                        aDataTable.Columns.Add("EmpName");
                        aDataTable.Columns.Add("EmpMasterCode");
                        DataRow dataRow = null;
                        dataRow = aDataTable.NewRow();
                        dataRow["EmpInfoId"] = "0";
                        dataRow["EmpName"] = "Please Select an Employee.....";
                        dataRow["EmpMasterCode"] = "";
                        aDataTable.Rows.Add(dataRow);
                        appDal.ReportingEmpData(ddlReportingBoss.SelectedValue, aDataTable);

                        ddlFinalApprover.DataValueField = "EmpInfoId";
                        ddlFinalApprover.DataTextField = "EmpName";
                        ddlFinalApprover.DataSource = aDataTable;
                        ddlFinalApprover.DataBind();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    try
                    {
                        ddlFinalApprover.SelectedValue =
                              "";
                    }
                    catch (Exception)
                    {
                        ddlFinalApprover.SelectedIndex = 0;
                        //throw;
                    }
                }

            }
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    protected void ddlReportingBoss_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        chkIsAllEmployee_CheckedChanged(null, null);
    }


    protected void chkIsPreviousEmp_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkIsPreviousEmp.Checked)
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveISPre())
            {

                ddlEmpPrevious.Enabled = true;

                ddlEmpPrevious.DataSource = dt;
                ddlEmpPrevious.DataValueField = "EmpInfoId";
                ddlEmpPrevious.DataTextField = "EmpName";
                ddlEmpPrevious.DataBind();
                ddlEmpPrevious.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpPrevious.SelectedIndex = 0;

            }
        }
        else
        {
            ddlEmpPrevious.Enabled = false;
            chkSpecialTransfer.Enabled = false;
            ddlEmpPrevious.SelectedValue = null;
            chkSpecialTransfer.Checked = false;

        }
    }

    protected void ddlEmpPrevious_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpPrevious.SelectedValue!="")
        {
            GetEmpEditInfo(Convert.ToInt32(ddlEmpPrevious.SelectedValue));
        }
        else
        {
            
        }
     
    }
}