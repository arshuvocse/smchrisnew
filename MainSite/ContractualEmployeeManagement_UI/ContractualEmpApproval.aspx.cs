using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.ExitManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.Survey;
using DAL.Transfer_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class ContractualEmployeeManagement_UI_ContractualEmpApproval : System.Web.UI.Page
{

    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ProbationperiodDAL aProbationperiodDal = new ProbationperiodDAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        ExtentionRenewRadioButtonList.Items[4].Attributes.Add("hidden", "hidden");
        ExtentionRenewRadioButtonList.Items[5].Attributes.Add("hidden", "hidden");

        if (!IsPostBack)
        {
            try
            {

                string id = Request.QueryString["id"];

                if (Session["Date"] != null)
                {
                    Calendar1.StartDate = Convert.ToDateTime(Session["Date"].ToString());
                    CalendarExtender2.StartDate = Convert.ToDateTime(Session["Date"].ToString());
                    CalendarExtender3.StartDate = Convert.ToDateTime(Session["Date"].ToString());
                    CalendarExtender4.StartDate = Convert.ToDateTime(Session["Date"].ToString());
                    ExtentionRenewRadioButtonList.Items[2].Enabled = false;
                    ShowPanel.Visible = true;
                    Session["Date"] = null;
                }
            }
            catch (Exception)
            {


            }


            ButtonVisible();
            LoadDropDownList();
            LoadInitialDDL();

            if (Session["ContractualEmpManageId"] != null)
            {


                DataTable dtMemoInfo = aContractualEmpManageDAL.getMemoData(Session["ContractualEmpManageId"].ToString());
                if (dtMemoInfo.Rows.Count > 0)
                    {
                        try
                        {
                            txtHeader1.Text = dtMemoInfo.Rows[0]["CompanyName"].ToString();
                            txtTo.Text = dtMemoInfo.Rows[0]["TO_"].ToString();
                            txtFrom.Text = dtMemoInfo.Rows[0]["From_"].ToString();
                            txtSubject.Text = dtMemoInfo.Rows[0]["Subject_"].ToString();
                            txtDate.Text = dtMemoInfo.Rows[0]["EffectiveDate"].ToString();
                            txtBodyofletter.Text = dtMemoInfo.Rows[0]["BodyLetter"].ToString();
                            txtGridviewBefore.Text = dtMemoInfo.Rows[0]["GridviewBefore"].ToString();
                            txtFooter01.Text = dtMemoInfo.Rows[0]["Footer01"].ToString();
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                ContractualEmpManageIdHiddenField.Value = Session["ContractualEmpManageId"].ToString();
                GetOneRecord(Session["ContractualEmpManageId"].ToString());
                DataTable dtdata =
                    aContractualEmpManageDAL.GetApprovalComments(Session["ContractualEmpManageId"].ToString());
                AppLogCommentGridView.DataSource = dtdata;
                AppLogCommentGridView.DataBind();
                bool isselfapp = false;
                DataTable dtNext = new DataTable();
                   DataTable dtdatainfo =
                        aContractualEmpManageDAL.GetContractualDataInfo((Session["ContractualEmpManageId"].ToString()));
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        try
                        {
                            isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                    }
                    DataTable aDataTable = aContractualEmpManageDAL.GetContractualEmpManageById(Session["ContractualEmpManageId"].ToString());
                    lblConToPerPreiod.Text = dtdatainfo.Rows[0]["ContractPreiod"].ToString();
                    int empid = 0;
                    if (isselfapp)
                    {
                        empid = Convert.ToInt32(aDataTable.Rows[0]["UserEmpInfoId"].ToString());
                        ExtensionPanelView.Visible = false;
                        RenewPanelView.Visible = false;
                        PermanentToContractualPanelView.Visible = false;

                        ContractualToPermanentPanelView.Visible = false;

                        DataTable dtUpLift = aContractualEmpManageDAL.getUpLiftSuppervisorData(Session["ContractualEmpManageId"].ToString());

                        if (dtUpLift.Rows.Count > 0)
                        {
                            bool IsSeparation = Convert.ToBoolean(dtUpLift.Rows[0]["IsSeparation"].ToString());
                            bool IsOrganization = Convert.ToBoolean(dtUpLift.Rows[0]["IsOrganization"].ToString());
                            bool IsDesignation = Convert.ToBoolean(dtUpLift.Rows[0]["IsDesignation"].ToString());
                            bool IsSalary = Convert.ToBoolean(dtUpLift.Rows[0]["IsSalary"].ToString());
                            bool IsPlace = Convert.ToBoolean(dtUpLift.Rows[0]["IsPlace"].ToString());

                            if (IsSeparation)
                            {
                                trSeparate.Visible = true;
                                mem_Separate.Visible = true;
                                tr_JobLeftType.Text = dtUpLift.Rows[0]["JobLeftType"].ToString();
                                tr_SeparationDate.Text = dtUpLift.Rows[0]["SeparationDate"].ToString();

                                mem_JobLeftType.Text = dtUpLift.Rows[0]["JobLeftType"].ToString();
                                mem_SeparationDate.Text = dtUpLift.Rows[0]["SeparationDate"].ToString();

                               
                            }

                            if (IsOrganization)
                            {
                                trOrganization.Visible = true;
                                mem_Organization.Visible = true;
                                tr_Division.Text = dtUpLift.Rows[0]["DivisionName"].ToString();
                                tr_Wing.Text = dtUpLift.Rows[0]["DivisionWingName"].ToString();
                                tr_Department.Text = dtUpLift.Rows[0]["DepartmentName"].ToString();
                                tr_Section.Text = dtUpLift.Rows[0]["SectionName"].ToString();
                                tr_SubSection.Text = dtUpLift.Rows[0]["SubSectionName"].ToString();

                                mem_Division.Text = dtUpLift.Rows[0]["DivisionName"].ToString();
                                mem_Wing.Text = dtUpLift.Rows[0]["DivisionWingName"].ToString();
                                mem_Department.Text = dtUpLift.Rows[0]["DepartmentName"].ToString();
                                mem_Section.Text = dtUpLift.Rows[0]["SectionName"].ToString();
                                mem_SubSection.Text = dtUpLift.Rows[0]["SubSectionName"].ToString();

                                Pmem_Division.Text = dtUpLift.Rows[0]["PDivisionName"].ToString();
                                Pmem_Wing.Text = dtUpLift.Rows[0]["PDivisionWingName"].ToString();
                                Pmem_Department.Text = dtUpLift.Rows[0]["PDepartmentName"].ToString();
                                Pmem_Section.Text = dtUpLift.Rows[0]["PSectionName"].ToString();
                                Pmem_SubSection.Text = dtUpLift.Rows[0]["PSubSectionName"].ToString();
                            }

                            if (IsDesignation)
                            {
                                trDesignation.Visible = true;
                                tr_Designation.Text = dtUpLift.Rows[0]["Designation"].ToString();
                                tr_DesignationType.Text = dtUpLift.Rows[0]["DesigTypeName"].ToString();


                                mem_Designation.Visible = true;
                                mem_Designation.Text = dtUpLift.Rows[0]["Designation"].ToString();
                                mem_DesignationType.Text = dtUpLift.Rows[0]["DesigTypeName"].ToString();

                                mem_Designation.Text = dtUpLift.Rows[0]["Designation"].ToString();
                                mem_DesignationType.Text = dtUpLift.Rows[0]["DesigTypeName"].ToString();
                                
                            }

                            if (IsSalary)
                            {
                                trSalary.Visible = true;
                                tr_SalaryGrade.Text = dtUpLift.Rows[0]["SalaryGrade"].ToString();
                                tr_SalaryStep.Text = dtUpLift.Rows[0]["SalaryStepName"].ToString();

                                mem_Salary.Visible = true;
                                Pmem_SalaryGrade.Text = dtUpLift.Rows[0]["PSalaryGrade"].ToString();
                                mem_SalaryGrade.Text = dtUpLift.Rows[0]["SalaryGrade"].ToString();
                                Pmem_SalaryStep.Text = dtUpLift.Rows[0]["PSalaryStepName"].ToString();
                                 mem_SalaryStep.Text = dtUpLift.Rows[0]["SalaryStepName"].ToString();

                                mem_P_salary.Text = dtUpLift.Rows[0]["PBasicAmount"].ToString();
                                mem_N_salary.Text = dtUpLift.Rows[0]["BasicAmount"].ToString();
                                mem_Percent.Text = dtUpLift.Rows[0]["PercentAmount"].ToString();


                            }

                            if (IsPlace)
                            {
                                trPlace.Visible = true;
                                tr_SalaryLocation.Text = dtUpLift.Rows[0]["Office"].ToString();
                                tr_Place.Text = dtUpLift.Rows[0]["Place"].ToString();
                                tr_Floor.Text = dtUpLift.Rows[0]["New_Floor"].ToString();


                                divmem_Place.Visible = true;
                                mem_SalaryLocation.Text = dtUpLift.Rows[0]["Office"].ToString();
                                mem_Place.Text = dtUpLift.Rows[0]["Place"].ToString();
                                mem_Floor.Text = dtUpLift.Rows[0]["New_Floor"].ToString();


                                Pmem_SalaryLocation.Text = dtUpLift.Rows[0]["POffice"].ToString();
                                Pmem_Place.Text = dtUpLift.Rows[0]["PPlace"].ToString();
                                Pmem_Floor.Text = dtUpLift.Rows[0]["Old_Floor"].ToString();

                            }
                        }

                      
                    }
                    else
                    {
                        
                        empid = Convert.ToInt32(aDataTable.Rows[0]["EmpInfoId"].ToString());

                        dtNext =
                      aContractualEmpManageDAL.GetEmpInfoNextApprover(" WHERE emp.EmpInfoId='" +
                                                                      empid + "'");
                    }

                if (isselfapp)
                {

                   

                    DataTable dtempdata =
                           aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" +
                                                                            Session["AppPage"].ToString() +
                                                                            "' AND Serial='1' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                            Session["CompanyId"].ToString() + "'");


                    if (dtempdata.Rows.Count > 0)
                    {
                        empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        dtNext =
                      aContractualEmpManageDAL.GetEmpInfoNextApproverEmp(" WHERE emp.EmpInfoId='" +
                                                                      empid + "'");

                    }
                    else
                    {
                        try
                        {
                            empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }
                }
                else
                {
                    //try
                    //{
                    //    empid = Convert.ToInt32(aDataTable.Rows[0]["EmpInfoId"].ToString());

                    //}
                    //catch (Exception)
                    //{

                    //    //throw;
                    //}
                }
                

                
                  
                


                if (dtNext.Rows.Count > 0)
                {
                    lblNextApp.Text = dtNext.Rows[0]["AwEmpName"].ToString();

                }
                else
                {
                    lblNextApp.Text = "You are the final Approver";
                }
                Session["ContractualEmpManageId"] = null;



            }
            else
            {
                Response.Redirect("ContractualEmpApprovalList.aspx");
            }
            RadioTextValue();
            actionRadioButtonList.SelectedIndex = 0;

            using (DataTable dt = aContractualEmpManageDAL.GetContractualEvaluationRating())
            {
                gv_ProbationEvaluation.DataSource = dt;
                gv_ProbationEvaluation.DataBind();
            }


            VisibleGrid(ContractualEmpManageIdHiddenField.Value);

            if (ReportingBoss.Value.ToString() != "0")
            {
                if (ReportingBoss.Value != Session["EmpInfoId"].ToString())
                {
                    rdshoe.Visible = false;
                    fASSHOE.Visible = false;

                    slaShoe.Visible = false;


                    divEffectivedate.Visible = false;
                    divForm.Visible = false;

                   

                }
            }

            try
            {

                DataTable dtUpLift =
                    aContractualEmpManageDAL.getUpLiftSuppervisorDataOne(ContractualEmpManageIdHiddenField.Value);

                if (dtUpLift.Rows.Count > 0)
                {
                     try
                {
                    JobLeftDateTextBox.Text = dtUpLift.Rows[0]["SeparationDate"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }

                try
                {
                    JobLeftTypeDropDownList.SelectedValue = dtUpLift.Rows[0]["JobLeftTypeId"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }

                ddlCompany.SelectedValue = dtUpLift.Rows[0]["New_CompanyId"].ToString();
                ddlCompany_SelectedIndexChanged(null, null);
                try
                {
                    ddlDivision.SelectedValue = dtUpLift.Rows[0]["New_DivisionId"].ToString();
                }
                catch (Exception)
                {
                }
                ddlDivision_OnSelectedIndexChanged(null, null);


                ddlDepartment.SelectedValue = dtUpLift.Rows[0]["New_DepartmentId"].ToString();
                ddlDepartment_OnSelectedIndexChanged(null, null);
                try
                {
                    ddlWing.SelectedValue = dtUpLift.Rows[0]["New_DivisionWId"].ToString();
                }
                catch (Exception)
                {
                    ddlWing.SelectedValue = null;
                    //throw;
                }

                try
                {
                    ddlSection.SelectedValue = dtUpLift.Rows[0]["New_SectionId"].ToString();
                }
                catch (Exception)
                {
                    ddlSection.SelectedValue = null;
                    //throw;
                }

                try
                {
                    ddlSubSection.SelectedValue = dtUpLift.Rows[0]["New_SubSectionId"].ToString();
                }
                catch (Exception)
                {
                    ddlSubSection.SelectedValue = null;
                    //throw;
                }

                try
                {
                    ddlEmpCategory.SelectedValue = dtUpLift.Rows[0]["New_EmpCategoryId"].ToString();
                }
                catch (Exception)
                {
                    ddlEmpCategory.SelectedValue = null;
                    //throw;
                }
                ddlEmpCategory_OnSelectedIndexChanged(null, null);
                ddlSalaryGrade.SelectedValue = dtUpLift.Rows[0]["New_SalaryGradeId"].ToString();
                ddlSalaryGrade_OnSelectedIndexChanged(null, null);

                ddlSalaryStep.SelectedValue = dtUpLift.Rows[0]["New_SalaryStepId"].ToString();

                ddlDesignation.SelectedValue = dtUpLift.Rows[0]["New_DesignationId"].ToString();
                //   NewDesignationDropDownList.SelectedValue = emp.DesignationId.ToString();
                try
                {
                    ddlDesignationType.SelectedValue = dtUpLift.Rows[0]["New_DesignationTypeId"].ToString();
                }
                catch (Exception)
                {
                    ddlDesignationType.SelectedValue = null;
                    //throw;
                }
                try
                {
                    txtFloor.Text = dtUpLift.Rows[0]["New_Floor"].ToString();
                }
                catch (Exception)
                {

                    throw;
                }
                {

                }


                //ddlEmpType.SelectedValue = emp.EmpTypeId.ToString();
                //ddlEmpType_OnSelectedIndexChanged(null, null);

                try
                {
                    ddlSalaryLocation.SelectedValue = dtUpLift.Rows[0]["New_SalaryLoationId"].ToString();
                }
                catch (Exception)
                {
                    ddlSalaryLocation.SelectedValue = null;
                    //throw;
                }
                using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlSalaryLocation.SelectedValue))
                {
                    ddlJobLocation.DataSource = dt;
                    ddlJobLocation.DataValueField = "Value";
                    ddlJobLocation.DataTextField = "TextField";
                    ddlJobLocation.DataBind();
                }
                try
                {
                    ddlJobLocation.SelectedValue = dtUpLift.Rows[0]["New_JobLocationId"].ToString();
                }
                catch (Exception)
                {
                    ddlJobLocation.SelectedValue = null;
                    //throw;
                }


            }

            }
            catch (Exception)
            {

                //throw;
            }
            
           
        }
    }


    //private void LoadData(int id)
    //{
    //    DataTable dtdata = new DataTable();
    //    dtdata = aContractualEmpManageDAL.LoadEmpJInfoInTextBoxById(id);
    //    if (dtdata.Rows.Count > 0)
    //    {
    //       // ddlEmpInfo.SelectedValue = id.ToString();
    //        //if (dtdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
    //        //{
    //        //    //ExtentionRenewRadioButtonList.Visible = false;
    //        //    //SalaryIncrementRadioButtonList1.Visible = false;
    //        //    //FacilityRadioButtonList.Visible = false;
    //        //    //ContractPreiod.Visible = false;
    //        //    //manualUpdateCheckBox.Visible = false;
    //        //    //evgrid.Visible = false;

    //        //}

    //        //if (dtdata.Rows[0]["ReportingEmpId"].ToString() == Session["EmpInfoId"].ToString())
    //        //{
    //        //    evgrid.Visible = false;
    //        //    using (DataTable dt = aContractualEmpManageDAL.GetContractualEvaluationRating())
    //        //    {
    //        //        gv_ProbationEvaluation.DataSource = dt;
    //        //        gv_ProbationEvaluation.DataBind();
    //        //    }

    //        //}


    //        //using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(id.ToString()))
    //        //{
    //        //    if (dtreporting.Rows.Count > 0)
    //        //    {

    //        //        loadGridView.DataSource = dtreporting;
    //        //        loadGridView.DataBind();
    //        //    }
    //        //    else
    //        //    {
    //        //        loadGridView.DataSource = null;
    //        //        loadGridView.DataBind();
    //        //    }

    //        //}


    //        companyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
    //        ddlCompany.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
    //        ddlCompany_SelectedIndexChanged(null, null);
    //        SearchEmployeeNameTextBoxTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
    //        //lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
    //        //lblProject.Text = dtdata.Rows[0]["ProjectName"].ToString();
    //        //if ((dtdata.Rows[0]["ContractEndDate"] != DBNull.Value))
    //        //{
    //        //    ContactualEndDateHiddenField.Value = dtdata.Rows[0]["ContractEndDate"].ToString();
    //        //    lblContractEndDate.Text = string.IsNullOrEmpty(dtdata.Rows[0]["ContractEndDate"].ToString()) ? "" : Convert.ToDateTime(dtdata.Rows[0]["ContractEndDate"].ToString()).ToString("dd-MMM-yyyy");
    //        //}
    //        //if ((dtdata.Rows[0]["ContractPeriod"] != DBNull.Value))
    //        //{
    //        //    ContractPeriodHF.Value = dtdata.Rows[0]["ContractPeriod"].ToString();
    //        //}

    //        //lblComName.Text = dtdata.Rows[0]["CompanyName"].ToString();
    //        //hfIsProgramContractualOP.Value = dtdata.Rows[0]["IsProgramContractual"].ToString();
    //        //hfIsSMCFundedProjects.Value = dtdata.Rows[0]["IsSMCFundedProjects"].ToString();
    //        //lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
    //        //lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
    //        //lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
    //        //lblEmpType.Text = dtdata.Rows[0]["EmpType"].ToString();
    //        ////    PReportingBodyDropDownList.SelectedValue = 1.ToString();

    //        //lblSalaryGrade.Text = dtdata.Rows[0]["SalaryGrade"].ToString();
    //        //lblDivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
    //        //lblWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
    //        //lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
    //        //lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
    //        //lblSubSection.Text = dtdata.Rows[0]["SubSectionName"].ToString();

    //        //HFDivID.Value = dtdata.Rows[0]["DivisionId"].ToString();
    //        //HFDivWingId.Value = dtdata.Rows[0]["DivisionWId"].ToString();
    //        //HFDeptID.Value = dtdata.Rows[0]["DepartmentId"].ToString();
    //        //HFSecID.Value = dtdata.Rows[0]["SectionId"].ToString();
    //        //HFSubSecID.Value = dtdata.Rows[0]["SubSectionId"].ToString();

    //        //HFEmpCode.Value = dtdata.Rows[0]["EmpMasterCode"].ToString();
    //        //HFEmpTypeID.Value = dtdata.Rows[0]["EmpTypeId"].ToString();
    //        //HFSalLocID.Value = dtdata.Rows[0]["SalaryLoationId"].ToString();
    //        //HFJobLocID.Value = dtdata.Rows[0]["JobLocationId"].ToString();
    //        //HFDesgId.Value = dtdata.Rows[0]["DesignationId"].ToString();

    //        //hfPreviousPreoject.Value = dtdata.Rows[0]["ProjectType"].ToString();

    //        //SGradeFF.Value = dtdata.Rows[0]["SalaryGradeId"].ToString();
    //        //SStepHF.Value = dtdata.Rows[0]["SalaryStepId"].ToString();



    //        using (var db = new HRIS_SMCEntities())
    //        {
    //            try
    //            {
    //                var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == id select j).FirstOrDefault();


    //                using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(id))
    //                {
    //                    lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

    //                }



    //                ddlCompany.SelectedValue = emp.CompanyId.ToString();
    //                ddlCompany_SelectedIndexChanged(null, null);
    //                ddlDivision.SelectedValue = emp.DivisionId.ToString();
    //                ddlDivision_OnSelectedIndexChanged(null, null);


    //                ddlDepartment.SelectedValue = emp.DepartmentId.ToString();
    //                ddlDepartment_OnSelectedIndexChanged(null, null);
    //                try
    //                {
    //                    ddlWing.SelectedValue = emp.DivisionWId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlWing.SelectedValue = null;
    //                    //throw;
    //                }

    //                try
    //                {
    //                    ddlSection.SelectedValue = emp.SectionId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlSection.SelectedValue = null;
    //                    //throw;
    //                }

    //                try
    //                {
    //                    ddlSubSection.SelectedValue = emp.SubSectionId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlSubSection.SelectedValue = null;
    //                    //throw;
    //                }

    //                try
    //                {
    //                    ddlEmpCategory.SelectedValue = emp.EmpCategoryId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlEmpCategory.SelectedValue = null;
    //                    //throw;
    //                }
    //                ddlEmpCategory_OnSelectedIndexChanged(null, null);
    //                ddlSalaryGrade.SelectedValue = emp.SalaryGradeId.ToString();
    //                ddlSalaryGrade_OnSelectedIndexChanged(null, null);

    //                ddlSalaryStep.SelectedValue = emp.SalaryStepId.ToString();

    //                ddlDesignation.SelectedValue = emp.DesignationId.ToString();
    //             //   NewDesignationDropDownList.SelectedValue = emp.DesignationId.ToString();
    //                try
    //                {
    //                    ddlDesignationType.SelectedValue = emp.DesignationTypeId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlDesignationType.SelectedValue = null;
    //                    //throw;
    //                }
    //                if (emp.Floor != null)
    //                {
    //                    txtFloor.Text = emp.Floor.ToString();
    //                }


    //                //ddlEmpType.SelectedValue = emp.EmpTypeId.ToString();
    //                //ddlEmpType_OnSelectedIndexChanged(null, null);

    //                try
    //                {
    //                    ddlSalaryLocation.SelectedValue = emp.SalaryLoationId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlSalaryLocation.SelectedValue = null;
    //                    //throw;
    //                }
    //                using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlSalaryLocation.SelectedValue))
    //                {
    //                    ddlJobLocation.DataSource = dt;
    //                    ddlJobLocation.DataValueField = "Value";
    //                    ddlJobLocation.DataTextField = "TextField";
    //                    ddlJobLocation.DataBind();
    //                }
    //                try
    //                {
    //                    ddlJobLocation.SelectedValue = emp.JobLocationId.ToString();
    //                }
    //                catch (Exception)
    //                {
    //                    ddlJobLocation.SelectedValue = null;
    //                    //throw;
    //                }




    //            }
    //            catch (Exception)
    //            {

    //                //throw;
    //            }
    //        }

    //    }
    //}



    private void RadioTextValue()
    {
        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
       
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            dtdata = aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");


        }
        else
        {
            dtdata = aContractualEmpManageDAL.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


      

        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

            DataTable dtdatainfo =
                 aContractualEmpManageDAL.GetContractualDataInfo((ContractualEmpManageIdHiddenField.Value.ToString()));

            bool isselfapp = false;


            if (dtdatainfo.Rows.Count > 0)
            {
                try
                {
                    isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                }
                catch (Exception)
                {

                    //throw;
                }
            }


            if (isselfapp)
            {
                string filepath2 = "../ContractualEmployeeManagement_UI/ContractualEmpApprovalList.aspx";
            DataTable     DTTT = aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath2 + "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");


            if (DTTT.Rows.Count > 0)
                {
                    lblNextApp.Text = "You are the final Approver";

                }
            else
            {
                
            }
            }
              


        }
        else if (hfIsForward.Value == "Forward")
        {
            DivMemo.Visible = true;

            DivAppStatus.Visible = false;
            divApp01.Visible = false;
            divApp02.Visible = false;


            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
             
            try
            {
            DataTable dtForward = aContractualEmpManageDAL.GetContractualSequence(ContractualEmpManageIdHiddenField.Value, " and EmpInfoId=" + Session["EmpInfoId"].ToString() + " and ((Isapproved is null)or (Isapproved =0))");
            hfForwardMainId.Value = dtForward.Rows[0]["ContractualRoutingPathID"].ToString();
            hfGetPreviousMainId.Value = dtForward.Rows[0]["ContractualRoutingPathID"].ToString();

            int serial = Convert.ToInt32(dtForward.Rows[0]["Seq_No"].ToString()) + 1;

            DataTable dtForward2 = aContractualEmpManageDAL.GetContractualSequence(ContractualEmpManageIdHiddenField.Value,   "  and Seq_No=" + serial);

            if (dtForward2.Rows.Count > 0)
            {
                 
            
            try
            {
                DataTable dtNext =
                   aContractualEmpManageDAL.GetEmpInfoNextApproverEmp(" WHERE emp.EmpInfoId='" +
                                                                   dtForward2.Rows[0]["EmpInfoId"].ToString() + "'");

                if (dtNext.Rows.Count > 0)
                {
                    lblNextApp.Text = dtNext.Rows[0]["AwEmpName"].ToString();

                }
                else
                {
                    lblNextApp.Text = "You are the final Approver";
                }
            }
            catch (Exception)
            {

                //throw;
            }
            }
            else
            {
                lblNextApp.Text = "You are the final Approver";

            }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        else
        {



            DataTable dtdatainfo =
                aContractualEmpManageDAL.GetContractualDataInfo((ContractualEmpManageIdHiddenField.Value.ToString()));

            bool isselfapp = false;


            if (dtdatainfo.Rows.Count > 0)
            {
                try
                {
                    isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                }
                catch (Exception)
                {

                    //throw;
                }
            }


            if (isselfapp)
            {



                try
                {
                    DataTable dtempdata =
                        aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                         "' AND EmpInfoId='" +
                                                                         Session["EmpInfoId"].ToString() +
                                                                         "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                         Session["CompanyId"].ToString() + "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                         "' AND Serial='" + serial +
                                                                         "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                         Session["CompanyId"].ToString() + "' ");
                    DataTable dtNext =
                        aContractualEmpManageDAL.GetEmpInfoNextApproverEmp(" WHERE emp.EmpInfoId='" +
                                                                           dtempdata2.Rows[0]["EmpInfoId"].ToString() +
                                                                           "'");

                    if (dtNext.Rows.Count > 0)
                    {
                        lblNextApp.Text = dtNext.Rows[0]["AwEmpName"].ToString();

                    }
                    else
                    {
                        lblNextApp.Text = "You are the final Approver";
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
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
            Button1.Visible = true;


        }
        else
        {
            submitButton.Visible = true;
            Button1.Visible = false;
        }
    }


    private void LoadInitialDDL()
    {

       
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        using (DataTable dt = _commonDataLoad.GetDDLDesignation())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";
            ddlDesignation.DataBind();
        }

        
        //  atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(NewDesignationDropDownList);
        aEmployeeJobLeftEntryDAL.LoadJobLeftTypeDropDownList(JobLeftTypeDropDownList);
    }

    EmployeeReDesignationDAL atblEmployeePromotionEntryDAL = new EmployeeReDesignationDAL();
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();
    public void ButtonVisible()
    {
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        submitButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        editButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        delButton.Visible = true;
        //    }
        //    Session["Status"] = null;
        //}

    }


    private void GetOneRecord(string idd)
    {

        //submitButton.Text = "Update";
        //submitButton.BackColor = Color.DodgerBlue;

        DataTable aDataTable = aContractualEmpManageDAL.GetContractualEmpManageById(idd);
        DataTable dtForward = aContractualEmpManageDAL.GetContractualApplogForwordById(idd);
        if (dtForward.Rows.Count>0)
        {
            hfIsForward.Value = "Forward";

             
        }

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {


            //Tareq


            EmpId.Value = aDataTable.Rows[0]["EmpInfoId"].ToString();
            LoadData(Convert.ToInt32(aDataTable.Rows[0]["EmpInfoId"].ToString()));

            try
            {
                EffectiveDate.Value = aDataTable.Rows[0]["EffectiveDate"].ToString();
            }
            catch (Exception)
            {

            }


            bool isselfapp = Convert.ToBoolean(aDataTable.Rows[0]["IsSelfApp"].ToString());
            if (isselfapp)
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["EmpInfoId"].ToString();
            }
            else
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["UserEmpInfoId"].ToString();
            }

            txtEffectiveDate.Text = string.IsNullOrEmpty(aDataTable.Rows[0]["EffectiveDate"].ToString()) ? "" : Convert.ToDateTime(aDataTable.Rows[0]["EffectiveDate"].ToString()).ToString("dd-MMM-yyyy");
            mainEmpId.Value = aDataTable.Rows[0]["MainEmpId"].ToString();

            actionstatusHiddenField.Value = aDataTable.Rows[0].Field<String>("ActionStatus").ToString();

            HFEmpTypeID.Value = aDataTable.Rows[0]["EmpTypeId"].ToString();
            hfFromProject.Value = aDataTable.Rows[0]["FromProject"].ToString();
            hfToProject.Value = aDataTable.Rows[0]["ToProject"].ToString();


            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();

            if (companyDropDownList.SelectedValue != "")
            {
                lblCompany.Text = companyDropDownList.SelectedItem.Text;
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
            repEmpIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId").ToString();

            lblEmp.Text = aDataTable.Rows[0]["EmployeeName"].ToString();
            mem_EmployeeName.Text = aDataTable.Rows[0]["EmployeeName"].ToString();
            SearchEmployeeNameTextBoxTextBox.Text = lblEmp.Text;

            if (aDataTable.Rows[0]["ContractPreiod"].ToString() != "")
            {
                txtContractualPreiod.Text = aDataTable.Rows[0]["ContractPreiod"].ToString();

                lblExtensionContractPreiod.Text = aDataTable.Rows[0]["ContractPreiod"].ToString();

                lblRenewContractPreiod.Text = aDataTable.Rows[0]["ContractPreiod"].ToString();
            }
            else
            {
                txtContractualPreiod.Text = "0";
            }




            lblEmployeeCode.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            mem_EmployeeID.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = string.IsNullOrEmpty(aDataTable.Rows[0]["DateOfJoin"].ToString()) ? "" : Convert.ToDateTime(aDataTable.Rows[0]["DateOfJoin"].ToString()).ToString("dd-MMM-yyyy");
            lblDesignation.Text = aDataTable.Rows[0]["Designation"].ToString();
            lblDesignationType.Text = aDataTable.Rows[0]["DesigTypeName"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = aDataTable.Rows[0]["GradeName"].ToString();
            lblSalaryStep.Text = aDataTable.Rows[0]["SalaryStepName"].ToString();
            lblDivision.Text = aDataTable.Rows[0]["DivisionName"].ToString();
            lblWing.Text = aDataTable.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = aDataTable.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = aDataTable.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = aDataTable.Rows[0]["SubSectionName"].ToString();
            lblOffice.Text = aDataTable.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = aDataTable.Rows[0]["Location"].ToString();

            ShowPanel.Visible = true;

            try
            {

                ExtentionRenewRadioButtonList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsExtension"].ToString());
                ExtentionRenewRadioButtonList.Items[1].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsRenew"].ToString());
                ExtentionRenewRadioButtonList.Items[2].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsPermanentToContractual"].ToString());
                ExtentionRenewRadioButtonList.Items[3].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsContractualToPermanent"].ToString());
            }
            catch (Exception e)
            {

            }

            if (ExtentionRenewRadioButtonList.Items[0].Selected)
            {


                ExtensionPanelView.Visible = true;
                ExtensionFromDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ExtensionFromDate").ToString("dd-MMM-yyyy");
                ExtensionToDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ExtensionToDate").ToString("dd-MMM-yyyy");

                lblExtensionFromDate.Text = ExtensionFromDateTextBox.Text;

                lblExtensionToDate.Text = ExtensionToDateTextBox.Text;
                lblExtention.Text = "Extension";
                PtblExtension.Visible = true;
            }


            if (ExtentionRenewRadioButtonList.Items[1].Selected)
            {
                PtblRenew.Visible = true;
                RenewPanelView.Visible = true;
                RenewStartDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("RenewStartDate").ToString("dd-MMM-yyyy");
                RenewToDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("RenewToDate").ToString("dd-MMM-yyyy");

                lblRenewStartDate.Text = RenewStartDateTextBox.Text;

                lblRenewEndDate.Text = RenewToDateTextBox.Text;
            }


            if (ExtentionRenewRadioButtonList.Items[2].Selected)
            {
                PermanentToContractualPanelView.Visible = true;
                PermanentToContractualEffectiveDaeTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEffectiveDate").ToString("dd-MMM-yyyy");


                try
                {
                    PermanentToContractualEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEndDate").ToString("dd-MMM-yyyy");
                }
                catch (Exception)
                {

                    //throw;
                }

                PtblPermanentToContractual.Visible = true;
                lblPermanentToContractualStartDate.Text = PermanentToContractualEffectiveDaeTextBox.Text;

                lblPermanentToContractualEndDate.Text = PermanentToContractualEndDateTextBox.Text;
                lblPertoConPreiod.Text = "0";
                txtContractualPreiod.Text = "0";
            }

            if (ExtentionRenewRadioButtonList.Items[3].Selected)
            {
                ContractualToPermanentPanelView.Visible = true;
                PtblContractualToPermanent.Visible = true;
                ContractualToPermanentDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ContractualToPermanentDate").ToString("dd-MMM-yyyy");
                lblContractualToPermanentDateTextBox.Text =
                    ContractualToPermanentDateTextBox.Text;

                lblConToPerPreiod.Text = "0";
                txtContractualPreiod.Text = "0";

            }



            try
            {
                SalaryIncrementRadioButtonList1.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsSalaryIncrement"].ToString());

                if (SalaryIncrementRadioButtonList1.Items[0].Selected == true)
                {
                    lblIncrementInfo.Text = "Salary Increment";
                }
            }
            catch (Exception)
            {


            }
            try
            {
                SalaryIncrementRadioButtonList1.Items[1].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsNoIncrement"].ToString());


                if (SalaryIncrementRadioButtonList1.Items[1].Selected == true)
                {
                    lblIncrementInfo.Text = "No Increment";
                }
            }
            catch (Exception)
            {


            }
            try
            {
                FacilityRadioButtonList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsFacilityIncluded"].ToString());

                if (FacilityRadioButtonList.Items[0].Selected == true)
                {
                    lblFacilityInfo.Text = "Facility Included";
                }

            }
            catch (Exception)
            {


            }
            try
            {
                FacilityRadioButtonList.Items[1].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsNoFacility"].ToString());

                if (FacilityRadioButtonList.Items[1].Selected == true)
                {
                    lblFacilityInfo.Text = "No Facility";
                }
            }
            catch (Exception)
            {


            }




            RemarksTextBox.Text = aDataTable.Rows[0]["Remarks"].ToString();

            lblRemarks.InnerHtml = RemarksTextBox.Text;
            using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(mainEmpId.Value.ToString()))
            {
                if (dtreporting.Rows.Count > 0)
                {

                    loadGridView.DataSource = dtreporting;
                    loadGridView.DataBind();
                }
                else
                {
                    loadGridView.DataSource = null;
                    loadGridView.DataBind();
                }

            }
        }

    }

    private void LoadDropDownList()
    {
        aContractualEmpManageDAL.LoadCompanyDropDownListNew(companyDropDownList);
    }

    protected void ExtentionRenewRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {

        ExtentionRenewRadioButtonList.Items[4].Attributes.Add("hidden", "hidden");
        ExtentionRenewRadioButtonList.Items[5].Attributes.Add("hidden", "hidden");
        ExtensionPanelView.Visible = false;
        RenewPanelView.Visible = false;
        PermanentToContractualPanelView.Visible = false;
        ContractualToPermanentPanelView.Visible = false;
        rbOther.Visible = false;
        ExtensionFromDateTextBox.Text = "";
        ExtensionToDateTextBox.Text = "";
        RenewStartDateTextBox.Text = "";
        RenewToDateTextBox.Text = "";
        PermanentToContractualEffectiveDaeTextBox.Text = "";
        ContractualToPermanentDateTextBox.Text = "";
        divReappointment.Visible = false;
      
        chkReappointment.Checked = false;
        chkRedesignation.Checked = false;
        ContractPreiod.Visible = false;

        if (ExtentionRenewRadioButtonList.Items[0].Selected == true)
        {
            ExtensionPanelView.Visible = true;
            divReappointment.Visible = true;
            ContractPreiod.Visible = true;
        }

        if (ExtentionRenewRadioButtonList.Items[1].Selected == true)
        {
            RenewPanelView.Visible = true;
            divReappointment.Visible = true;
            ContractPreiod.Visible = true;

        }

        if (ExtentionRenewRadioButtonList.Items[2].Selected == true)
        {
            PermanentToContractualPanelView.Visible = true;
            divReappointment.Visible = true;
            ContractPreiod.Visible = true;

            //  ShowExistingAndNew.Visible = true;
        }

        if (ExtentionRenewRadioButtonList.Items[3].Selected == true)
        {
            ContractualToPermanentPanelView.Visible = true;
            PermanentToContractualPanelView.Visible = false;
            divReappointment.Visible = true;
            // ShowExistingAndNew.Visible = true;
        }



        if (ExtentionRenewRadioButtonList.Items[4].Selected == true)
        {
            PermanentToContractualPanelView.Visible = true;

            //  ShowExistingAndNew.Visible = true;
        }
        if (ExtentionRenewRadioButtonList.Items[5].Selected == true)
        {
            PermanentToContractualPanelView.Visible = true;
        }

        if (ExtentionRenewRadioButtonList.Items[6].Selected == true)
        {
            rbOther.Visible = true;
            PermanentToContractualPanelView.Visible = true;

            ContractPreiod.Visible = true;


            divReappointment.Visible = true;


        }

        txtContractualPreiod.Text = "0";
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ContractualEmpApprovalList.aspx");
    }

   
    protected void ExtensionToDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (ExtensionToDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(ExtensionToDateTextBox.Text);


                ExtensionMonthCalculation();
            }
            catch
            {
                ExtensionToDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }
    protected void ExtensionFromDateTextBox_TextChanged(object sender, EventArgs e)
    {

        if (ExtensionFromDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(ExtensionFromDateTextBox.Text);


                ExtensionMonthCalculation();
            }
            catch
            {
                ExtensionFromDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    private void ExtensionMonthCalculation()
    {

        try
        {

            if (ExtensionFromDateTextBox.Text != "" && ExtensionToDateTextBox.Text != "")
            {

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ar-EG");

                DateTime Birth = Convert.ToDateTime((ExtensionToDateTextBox.Text.ToString())).AddDays(1);
                DateTime Today = Convert.ToDateTime(ExtensionFromDateTextBox.Text);


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

                if (Months > 0)
                {
                    
                }
                else
                {
                    ExtensionToDateTextBox.Text = "";
                    ExtensionFromDateTextBox.Text = "";
                }
            }
        }
        catch (Exception)
        {
            ExtensionToDateTextBox.Text = "";
            ExtensionFromDateTextBox.Text = "";
            //throw;
        }

    }

    protected void RenewToDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (RenewToDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(RenewToDateTextBox.Text);

                RenewMonthCalculation();
            }
            catch
            {
                RenewToDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }
    protected void RenewStartDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (RenewStartDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(RenewStartDateTextBox.Text);

                RenewMonthCalculation();
            }
            catch
            {
                RenewStartDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }
    private void RenewMonthCalculation()
    {

        try
        {

            if (RenewStartDateTextBox.Text != "" && RenewToDateTextBox.Text != "")
            {

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ar-EG");

                DateTime Birth = Convert.ToDateTime((RenewToDateTextBox.Text.ToString())).AddDays(1);
                DateTime Today = Convert.ToDateTime(RenewStartDateTextBox.Text);


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

                if (Months > 0)
                {
                    
                }
                else
                {
                    RenewToDateTextBox.Text = "";
                    RenewStartDateTextBox.Text = "";
                }
            }
        }
        catch (Exception)
        {
            RenewToDateTextBox.Text = "";
            RenewStartDateTextBox.Text = "";
            //throw;
        }

    }
    protected void EffectiveDaeTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void PermanenttoContractualMonthCalculation()
    {

        try
        {

            if (PermanentToContractualEffectiveDaeTextBox.Text != "" && PermanentToContractualEndDateTextBox.Text != "")
            {

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ar-EG");

                DateTime Birth = Convert.ToDateTime((PermanentToContractualEndDateTextBox.Text.ToString())).AddDays(1);
                DateTime Today = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text);


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

                if (Months>0)
                {
                    
                }
                else
                {
                    PermanentToContractualEndDateTextBox.Text = "";
                    PermanentToContractualEffectiveDaeTextBox.Text = "";
                }
            }
        }
        catch (Exception)
        {
            PermanentToContractualEndDateTextBox.Text = "";
            PermanentToContractualEffectiveDaeTextBox.Text = "";
            //throw;
        }

    }
    

    protected void PermanentToContractualEffectiveDaeTextBox_TextChanged(object sender, EventArgs e)
    {
        if (PermanentToContractualEffectiveDaeTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(PermanentToContractualEffectiveDaeTextBox.Text);

                PermanenttoContractualMonthCalculation();

            }
            catch
            {
                PermanentToContractualEffectiveDaeTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }


    protected void ContractualToPermanentTextBox_TextChanged(object sender, EventArgs e)
    {
        if (ContractualToPermanentDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(ContractualToPermanentDateTextBox.Text);
            }
            catch
            {
                ContractualToPermanentDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }


    //public void RadioTextValue()
    //{
    //    //string filepath = Path.GetDirectoryName(Request.Path);
    //    //filepath = filepath.TrimStart('\\');
    //    //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
    //    string filepath = "";
    //    if (Session["AppPage"] != null)
    //    {
    //        filepath = Session["AppPage"].ToString();
    //    }

    //    DataTable dtdata = aContractualEmpManageDAL.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

    //    DataTable aDataTable = new DataTable();
    //    aDataTable.Columns.Add("Value");
    //    aDataTable.Columns.Add("Text");

    //    DataRow dataRow = null;



    //    if (dtdata.Rows.Count > 0)
    //    {
    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Approved";
    //        dataRow["Value"] = "Approved";
    //        aDataTable.Rows.Add(dataRow);

    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Review";
    //        dataRow["Value"] = "Review";
    //        aDataTable.Rows.Add(dataRow);

    //    }
    //    else
    //    {
    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Approved";
    //        dataRow["Value"] = "Verified";
    //        aDataTable.Rows.Add(dataRow);

    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Review";
    //        dataRow["Value"] = "Review";
    //        aDataTable.Rows.Add(dataRow);
    //    }

    //    actionRadioButtonList.DataValueField = "Value";
    //    actionRadioButtonList.DataTextField = "Text";
    //    actionRadioButtonList.DataSource = aDataTable;
    //    actionRadioButtonList.DataBind();
    //}


    protected void chkReappointment_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {


            if (chkReappointment.Checked)
            {
                MPBehavioral.Show();

            }
            else
            {
                MPBehavioral.Hide();

            }
        }
        catch (Exception)
        {


        }
    }


    public bool SelectValidation()
    {

        if (ExtentionRenewRadioButtonList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select State Change From Radio Button!!!", this);

            return false;
        }


        if (ExtentionRenewRadioButtonList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select State Change From Radio Button!!!", this);

            return false;
        }



        if (ExtentionRenewRadioButtonList.Items[0].Selected == true)
        {



            if (ExtensionFromDateTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                ExtensionFromDateTextBox.Focus();
                return false;
            }
            if (ExtensionToDateTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                ExtensionToDateTextBox.Focus();
                return false;
            }


            return true;
        }

        if (ExtentionRenewRadioButtonList.Items[1].Selected == true)
        {
            if (RenewStartDateTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                RenewStartDateTextBox.Focus();
                return false;
            }

            if (RenewToDateTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                RenewToDateTextBox.Focus();
                return false;
            }


        }

        
        return true;
    }





    protected void Button2_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {

            Session["CheckReporting"] = "";

            ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
            aMaster.ContractualEmpManageId
                = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;


            bool status = aContractualEmpManageDAL.UpdateContractural(aMaster);
            if (status)
            {
                int commentid = aContractualEmpManageDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                    aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);


                }
                else if (aMaster.ActionStatus == "Approved")
                {

                    int empid = 0;
                    bool isselfapp = false;
                    DataTable dtdatainfo =
                        aContractualEmpManageDAL.GetContractualDataInfo(aMaster.ContractualEmpManageId.ToString());
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    }

                    if (isselfapp)
                    {


                        DataTable dtempdata =
                            aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" +
                                                                             Session["AppPage"].ToString() +
                                                                             "' AND Serial='1' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                             Session["CompanyId"].ToString() + "'");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        //aMaster.ActionStatus = "Verified";
                        //aContractualEmpManageDAL.UpdateContractural(aMaster);
                        aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);

                        int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Contractual Employee Form Approval ", @"  <br/> Dear Sir, <br/>
A Contractual Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/>   http://182.160.103.234:8088/ <br/>
Thank you.
");
                    }
                    else
                    {
                        try
                        {
                            empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        if (empid > 0)
                        {



                            ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                            {
                                appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = empid;
                                appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                                appLogDao.Comments = commentsTextBox.Text;
                                appLogDao.CommentsId = commentid;
                            };
                            aMaster.ActionStatus = "Verified";
                            aContractualEmpManageDAL.UpdateContractural(aMaster);
                            aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
                            aContractualEmpManageDAL.UpdateSelfApprove(aMaster.ContractualEmpManageId, true);
                            int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);

                        }
                        else
                        {
                            ShowMessageBox("Reporting Body can not be Emptry!!!");
                        }


                    }
                    //ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                    //{
                    //    appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                    //    appLogDao.ApproveDate = DateTime.Now;
                    //    appLogDao.ApproveBy = Session["UserId"].ToString();
                    //    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    //    appLogDao.ForEmpInfoId = empid;
                    //    appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                    //    appLogDao.Comments = commentsTextBox.Text;
                    //    appLogDao.CommentsId = commentid;
                    //};
                    //aMaster.ActionStatus = "Verified";
                    //aContractualEmpManageDAL.UpdateContractural(aMaster);
                    //aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);

                    //int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), ContractualEmpManageIdHiddenField.Value);
                    DataTable dtempdata2 = aContractualEmpManageDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), ContractualEmpManageIdHiddenField.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        aContractualEmpManageDAL.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                        int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                        aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                    DataTable dtdata = aContractualEmpManageDAL.GetDataReviewEntryBy(
                      ContractualEmpManageIdHiddenField.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count > 0)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Edit";
                        Session["ContractualEmpManageId"] = aMaster.ContractualEmpManageId.ToString();
                        Response.Redirect("ContractualEmpManagement.aspx?id2=" + aMaster.ContractualEmpManageId.ToString());
                    }

                }


            }

        

            if (ReportingBoss.Value.ToString() != "0")
            {
                if (ReportingBoss.Value == Session["EmpInfoId"].ToString())
                {
                    Update();
                }
            }


            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully Done......');window.location ='ContractualEmpApprovalList.aspx';",
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


    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {

            if (hfIsForward.Value == "Forward")
            {

               
                ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
                aMaster.ContractualEmpManageId
                    = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
                bool status = aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
                int iddd = 0;
                if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;

                    DataTable dtForward = aContractualEmpManageDAL.GetContractualSequence(ContractualEmpManageIdHiddenField.Value, " and EmpInfoId=" + Session["EmpInfoId"].ToString() + " and ((Isapproved is null)or (Isapproved =0))");


                    int serial = Convert.ToInt32(dtForward.Rows[0]["Seq_No"].ToString()) + 1;

                    DataTable dtForward2 = aContractualEmpManageDAL.GetContractualSequence(ContractualEmpManageIdHiddenField.Value, "  and Seq_No=" + serial);


                    if (dtForward2.Rows.Count > 0)
                    {
                        empid = Convert.ToInt32(dtForward2.Rows[0]["EmpInfoId"].ToString());

                        ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.ContractualEmpManageId = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = 0;
                            appLogDao.IsForward = true;
                        }
                        ;
                        iddd = aContractualEmpManageDAL.SaveEmpContractAppLogForward(appLogDao);

                        aContractualEmpManageDAL.UpdateEmpSequence(hfForwardMainId.Value,true);
                         
                    }

                    else
                    {
                        ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Approved";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.ContractualEmpManageId = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = 0;
                            appLogDao.IsForward = true;
                        }
                        ;
                        iddd = aContractualEmpManageDAL.SaveEmpContractAppLogForward(appLogDao);

                        aContractualEmpManageDAL.UpdateEmpSequence(hfForwardMainId.Value,true);


                        ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();

                        aContractualEmpManageDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

                        aContractualEmpManageDAO.EmployeeId = Convert.ToInt32(EmpId.Value);

                        aContractualEmpManageDAO.ContractEndDate = DateTime.Now;
                        aContractualEmpManageDAO.ContractPreiod = 0;



                        for (int i = 0; i < ExtentionRenewRadioButtonList.Items.Count; i++)
                        {
                            if (ExtentionRenewRadioButtonList.Items[i].Selected)
                            {
                                string str = ExtentionRenewRadioButtonList.Items[i].Text.Trim();

                                Extension_Func(str, aContractualEmpManageDAO);

                                Renew_Func(str, aContractualEmpManageDAO);

                                Permanent_TO_Contractual(str, aContractualEmpManageDAO);

                                Contractual_To_Permanent(str, aContractualEmpManageDAO);
                                // /./  SMCFundedProjects_to_SMCContract(str, aContractualEmpManageDAO);
                                ToProjects(str, aContractualEmpManageDAO);
                                //SMCContract_to_SMCFundedProjects(str, aContractualEmpManageDAO);
                                //    aContractualEmpManageDAO.AutoProcess = manualUpdateCheckBox.Checked;
                            }
                        }
                    }





                    if (iddd > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfully Done......');window.location ='ContractualEmpApprovalList.aspx';",
                            true);
                    }
                    else
                    {
                        showMessageBox("Operation Faild!");
                    }

                }

                else if (aMaster.ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(),
                        ContractualEmpManageIdHiddenField.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata2 =
                        aContractualEmpManageDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(),
                            ContractualEmpManageIdHiddenField.Value);


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
                        


                        DataTable dtForward = aContractualEmpManageDAL.GetContractualSequence(ContractualEmpManageIdHiddenField.Value, " and EmpInfoId=" + dtempdata.Rows[0]["PreEmpInfoId"].ToString() + " and ( (Isapproved =1))");

                        if (dtForward.Rows.Count>0 )
                        {
                            aContractualEmpManageDAL.UpdateEmpSequence(hfForwardMainId.Value, false);
                            aContractualEmpManageDAL.UpdateEmpSequence(dtForward.Rows[0]["ContractualRoutingPathID"].ToString(), false);
                            ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                            {
                                //appLogDao.ActionStatus = "Verified";
                                appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                                appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                                appLogDao.Comments = commentsTextBox.Text;
                                appLogDao.CommentsId = 0;
                                appLogDao.IsForward = true;
                            }
                            if (actionst == "Approved")
                            {
                                aMaster.ActionStatus = "Verified";
                                aContractualEmpManageDAL.UpdateContractural(aMaster);
                            }
                            aContractualEmpManageDAL.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                            aContractualEmpManageDAL.UpdateContactAppLog("Review", dtempdata2.Rows[a][0].ToString());
                            int id = aContractualEmpManageDAL.SaveEmpContractAppLogForward(appLogDao);
                        }

                        else
                        {
                            ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                            {
                                //appLogDao.ActionStatus = "Verified";
                                appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                                appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                                appLogDao.Comments = commentsTextBox.Text;
                                appLogDao.CommentsId = 0;
                            }
                            if (actionst == "Approved")
                            {
                                aMaster.ActionStatus = "Verified";
                                aContractualEmpManageDAL.UpdateContractural(aMaster);
                            }

                            aContractualEmpManageDAL.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                            aContractualEmpManageDAL.UpdateContactAppLog("Review", dtempdata2.Rows[a][0].ToString());
                            int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                        }
                      
                    

                    
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfully Done......');window.location ='ContractualEmpApprovalList.aspx';",
                            true);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


            }

            else
            {
                ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
                aMaster.ContractualEmpManageId
                    = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
                bool status = aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
                if (status)
                {
                    int commentid = aContractualEmpManageDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                        commentsTextBox.Text);
                    if (aMaster.ActionStatus == "Verified")
                    {
                        DataTable dtempdata =
                            aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                           "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                           "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                        int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                        DataTable dtempdata2 =
                            aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                           "' AND Serial='" + serial + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                        //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                        ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                            appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;

                        };
                        int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);

                    }
                    else if (aMaster.ActionStatus == "Approved")
                    {
                        int empid = 0;
                        //DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                        //if (dtempdata.Rows.Count>0)
                        //{
                        //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        //}
                        ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };


                        int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Contractual Employee Form Approval ", @"  <br/> Dear Sir, <br/>
A Contractual Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/>   http://182.160.103.234:8088/ <br/>
Thank you.
");
                        ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();


                        for (int i = 0; i < ExtentionRenewRadioButtonList.Items.Count; i++)
                        {
                            if (ExtentionRenewRadioButtonList.Items[i].Selected)
                            {
                                string str = ExtentionRenewRadioButtonList.Items[i].Text.Trim();

                                Extension_Func(str, aContractualEmpManageDAO);

                                Renew_Func(str, aContractualEmpManageDAO);

                                Permanent_TO_Contractual(str, aContractualEmpManageDAO);

                                Contractual_To_Permanent(str, aContractualEmpManageDAO);
                                // /./  SMCFundedProjects_to_SMCContract(str, aContractualEmpManageDAO);
                                ToProjects(str, aContractualEmpManageDAO);
                                //SMCContract_to_SMCFundedProjects(str, aContractualEmpManageDAO); 
                            }
                        }

                        // DataTable dtdata = new DataTable();
                        // dtdata = aContractualEmpManageDAL.LoadEmpJInfoInTextBoxById(id);


                        // //if ((dtdata.Rows[0]["ContractEndDate"] != DBNull.Value))
                        // //{
                        // //    ContactualEndDateHiddenField.Value = dtdata.Rows[0]["ContractEndDate"].ToString();
                        // //}


                        // ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();

                        // aContractualEmpManageDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                        // aContractualEmpManageDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        // aContractualEmpManageDAO.ContractEndDate// = Convert.ToDateTime(ContactualEndDateHiddenField.Value);

                        //= string.IsNullOrEmpty(dtdata.Rows[0]["ContractEndDate"].ToString()) ? (DateTime?)null : DateTime.Parse(dtdata.Rows[0]["ContractEndDate"].ToString()).Date;
                        // for (int i = 0; i < ExtentionRenewRadioButtonList.Items.Count; i++)
                        // {
                        //     if (ExtentionRenewRadioButtonList.Items[i].Selected)
                        //     {
                        //         string str = ExtentionRenewRadioButtonList.Items[i].Text.Trim();

                        //         if (str == "Extension")
                        //         {
                        //             aContractualEmpManageDAO.IsExtension = true;
                        //             aContractualEmpManageDAO.IsRenew = false;
                        //             aContractualEmpManageDAO.IsPermanentToContractual = false;
                        //             aContractualEmpManageDAO.IsContractualToPermanent = false;
                        //             aContractualEmpManageDAO.ExtensionFromDate = Convert.ToDateTime(ExtensionFromDateTextBox.Text.Trim());
                        //             aContractualEmpManageDAO.ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim());

                        //             //if (manualUpdateCheckBox.Checked)
                        //             {
                        //                 Int32 empGenId = 0;
                        //                 string ExtensionToDate = "";
                        //                 empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);

                        //                 ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim()).ToString();
                        //           //      UpdateEmployeeContractualDeate(empGenId, ExtensionToDate);
                        //             }
                        //         }

                        //         if (str == "Renew")
                        //         {
                        //             aContractualEmpManageDAO.IsExtension = false;
                        //             aContractualEmpManageDAO.IsRenew = true;
                        //             aContractualEmpManageDAO.IsPermanentToContractual = false;
                        //             aContractualEmpManageDAO.IsContractualToPermanent = false;

                        //             aContractualEmpManageDAO.RenewStartDate = Convert.ToDateTime(RenewStartDateTextBox.Text.Trim());
                        //             aContractualEmpManageDAO.RenewToDate = Convert.ToDateTime(RenewToDateTextBox.Text.Trim());

                        //             //if (manualUpdateCheckBox.Checked)
                        //             {
                        //                 Int32 empGenId = 0;
                        //                 string ExtensionToDate = "";

                        //                 empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);

                        //                 ExtensionToDate = Convert.ToDateTime(RenewToDateTextBox.Text.Trim()).ToString();

                        //                 EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                        //                 DataTable dtCEndDate = _empdal.GetContractEndDate(empGenId);
                        //                 DataTable dtEmpCode = _empdal.GetEmpMasterCode(empGenId);

                        //                 //UpdateEmployeeContractualDeate(empGenId, ExtensionToDate);



                        //                 //// day gap betewwn 2 contract **
                        //                 //if (Convert.ToDateTime(dtCEndDate.Rows[0]["ContractEndDate"].ToString()).AddDays(1) <
                        //                 //    Convert.ToDateTime(RenewStartDateTextBox.Text.Trim()))
                        //                 //{
                        //                 //    _empdal.GetEmpMasterCode(empGenId);
                        //                 //}

                        //             }
                        //         }

                        //         if (str == "Permanent to Contractual")
                        //         {
                        //             aContractualEmpManageDAO.IsPermanentToContractual = true;

                        //             aContractualEmpManageDAO.IsExtension = false;
                        //             aContractualEmpManageDAO.IsRenew = false;
                        //             aContractualEmpManageDAO.IsContractualToPermanent = false;
                        //             aContractualEmpManageDAO.PermanentToContractualEffectiveDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim());
                        //             aContractualEmpManageDAO.PermanentToContractualEndDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim());

                        //             //if (manualUpdateCheckBox.Checked)
                        //             {
                        //                 Int32 empGenId = 0;
                        //                 Int32 EmpTypeId = 0;
                        //                 string ExtensionToDate = "";

                        //                 empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        //                 EmpTypeId = 2;

                        //                 ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();



                        //                 //UpdateEmployePermanenttoContractualInfoEmpTypeID(empGenId, ExtensionToDate, EmpTypeId);
                        //                 //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                        //                 //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(empGenId))
                        //                 //{

                        //                 //}
                        //             }
                        //         }

                        //         if (str == "Contractual to Permanent")
                        //         {
                        //             aContractualEmpManageDAO.IsContractualToPermanent = true;

                        //             aContractualEmpManageDAO.IsPermanentToContractual = false;

                        //             aContractualEmpManageDAO.IsExtension = false;
                        //             aContractualEmpManageDAO.IsRenew = false;
                        //             aContractualEmpManageDAO.ContractualToPermanentDate = DateTime.Now;

                        //             //if (manualUpdateCheckBox.Checked)
                        //             {
                        //                 Int32 empGenId = 0;
                        //                 Int32 EmpTypeId = 0;

                        //                 empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        //                 EmpTypeId = 1;

                        //                 //UpdateEmployeEmpTypeID(empGenId, EmpTypeId);
                        //                 //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                        //                 //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(empGenId))
                        //                 //{
                        //                 //    //if (dtEmpCode.Rows.Count > 0)
                        //                 //    //{
                        //                 //    //    EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                        //                 //    //}
                        //                 //}
                        //             }
                        //         }

                        //        // aContractualEmpManageDAO.AutoProcess = manualUpdateCheckBox.Checked;
                        //     }
                        // }
                    }
                    else if (aMaster.ActionStatus == "Review")
                    {
                        string actionst = "";
                        DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), ContractualEmpManageIdHiddenField.Value);
                        if (dtempdata.Rows.Count > 0)
                        {
                            actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                        }
                        DataTable dtempdata2 = aContractualEmpManageDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), ContractualEmpManageIdHiddenField.Value);
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
                            ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                            {
                                //appLogDao.ActionStatus = "Verified";
                                appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                                appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                                appLogDao.Comments = commentsTextBox.Text;
                                appLogDao.CommentsId = commentid;
                            }
                            if (actionst == "Approved")
                            {
                                aMaster.ActionStatus = "Verified";
                                aContractualEmpManageDAL.UpdateContractural(aMaster);
                            }
                            aContractualEmpManageDAL.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                            aContractualEmpManageDAL.UpdateContactAppLog("Review", dtempdata2.Rows[a][0].ToString());
                            int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                        }
                        else
                        {
                            ShowMessageBox("Please select Approval Status Approved  this!!!");
                        }

                    }


                }


                if (ReportingBoss.Value.ToString() != "0")
                {
                    if (ReportingBoss.Value == Session["EmpInfoId"].ToString())
                    {
                        Update();
                    }
                }


                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successfully Done......');window.location ='ContractualEmpApprovalList.aspx';",
                           true);
            }

           
        }
    }

    private void ToProjects(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "Project Change")
        {

            if (hfToProject.Value != "")
            {
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;

                if (hfToProject.Value == "Niltara")
                {
                    IsSMCFundedProjects = true;
                }
                else if (hfToProject.Value == "Niltara")
                {

                }

                else
                {
                    IsProgramContractual = true;

                }



                aContractualEmpManageDAO.IsPermanentToContractual = false;
                aContractualEmpManageDAO.isToProject = true;

                aContractualEmpManageDAO.IsExtension = false;
                aContractualEmpManageDAO.IsRenew = false;
                aContractualEmpManageDAO.IsContractualToPermanent = false;

                aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
                aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;


                aContractualEmpManageDAO.PermanentToContractualEffectiveDate =
                    Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim());
                aContractualEmpManageDAO.PermanentToContractualEndDate =
                    Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim());

                aContractualEmpManageDAO.FromProject = hfFromProject.Value;
                aContractualEmpManageDAO.ToProject = hfToProject.Value;


                Int32 empGenId = 0;
                Int32 EmpTypeId = 0;
                string ExtensionToDate = "";
                string RenewStartDate = "";

                empGenId = Convert.ToInt32(mainEmpId.Value);
                EmpTypeId = 2;

                RenewStartDate =
                    Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);


                DataTable aDataTable = aContractualEmpManageDAL.GetUpliftingDate(EmpId.Value);

                if (aDataTable.Rows.Count > 0)
                {


                    int? CompanyId = null;
                    try
                    {
                        CompanyId = (int?)aDataTable.Rows[0]["New_CompanyId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    int? DivisionId = null;
                    try
                    {
                        DivisionId = (int?)aDataTable.Rows[0]["New_DivisionId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? DivisionWId = null;
                    try
                    {
                        DivisionWId = (int?)aDataTable.Rows[0]["New_DivisionWId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? DepartmentId = null;
                    try
                    {
                        DepartmentId = (int?)aDataTable.Rows[0]["New_DepartmentId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    int? SectionId = null;
                    try
                    {
                        SectionId = (int?)aDataTable.Rows[0]["New_SectionId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? SubSectionId = null;
                    try
                    {
                        SubSectionId = (int?)aDataTable.Rows[0]["New_SubSectionId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? EmpCategoryId = null;
                    try
                    {
                        EmpCategoryId = (int?)aDataTable.Rows[0]["New_EmpCategoryId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? SalaryGradeId = null;
                    try
                    {
                        SalaryGradeId = (int?)aDataTable.Rows[0]["New_SalaryGradeId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    int? SalaryStepId = null;
                    try
                    {
                        SalaryStepId = (int?)aDataTable.Rows[0]["New_SalaryStepId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? DesignationId = null;
                    try
                    {
                        DesignationId = (int?)aDataTable.Rows[0]["New_DesignationId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    int? DesignationTypeId = null;
                    try
                    {
                        DesignationTypeId = (int?)aDataTable.Rows[0]["New_DesignationTypeId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                    int? JobLocationId = null;
                    try
                    {
                        JobLocationId = (int?)aDataTable.Rows[0]["New_JobLocationId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    int? SalaryLoationId = null;
                    try
                    {
                        SalaryLoationId = (int?)aDataTable.Rows[0]["New_SalaryLoationId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    string Floor = aDataTable.Rows[0]["New_Floor"].ToString();


                    bool IsSeparation = false;
                    try
                    {
                        IsSeparation = Convert.ToBoolean(aDataTable.Rows[0]["New_IsSeparation"].ToString());
                    }
                    catch (Exception)
                    {

                        //throw;
                    }


              



                    if (IsSeparation)
                    {
                        aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpIDReappointment(str,
                            IsSMCFundedProjects, IsProgramContractual,
                            aContractualEmpManageDAO.EmployeeId.ToString(),
                            string.IsNullOrEmpty(txtEffectiveDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                    ? (DateTime?)null
                                    : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                        ? (DateTime?)null
                                        : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId,
                            Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId,
                            SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                            DesignationTypeId, JobLocationId, SalaryLoationId, Floor);
                    }
                    else
                    {
                        aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects,
                            IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                            string.IsNullOrEmpty(txtEffectiveDate.Text)
                                ? (DateTime?)null
                                : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                    ? (DateTime?)null
                                    : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                        ? (DateTime?)null
                                        : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId,
                            Convert.ToInt32(Session["UserId"]));
                    }

                }

                EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                /// 

                using (var db = new HRIS_SMCEntities())
                {
                    var hhhh = db.tblEmpGeneralInfoes.OrderByDescending(u => u.EmpInfoId).FirstOrDefault();

                    CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
                    for (int i = 0; i < loadGridView.Rows.Count; i++)
                    {
                        _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                            hhhh.EmpInfoId.ToString());
                    }
                    if (IsSMCFundedProjects == true)
                    {
                        using (
                            DataTable dtEmpCode = _empdal.GetEmpMasterCodeForIsSMCFundedProjects(hhhh.EmpInfoId)
                            )
                        {
                            if (dtEmpCode.Rows.Count > 0)
                            {
                                // EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                            }
                        }
                    }
                    else
                    {
                        using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(hhhh.EmpInfoId))
                        {
                            if (dtEmpCode.Rows.Count > 0)
                            {
                                // EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                            }
                        }
                    }
                }
            }





        }
    }
    private void Contractual_To_Permanent(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {

       
        if (str == "Contractual to Permanent")
        {
            aContractualEmpManageDAO.IsContractualToPermanent = true;

            aContractualEmpManageDAO.IsPermanentToContractual = false;
            aContractualEmpManageDAO.isToProject = false;

            aContractualEmpManageDAO.IsExtension = false;
            aContractualEmpManageDAO.IsRenew = false;
            aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;
            aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
            aContractualEmpManageDAO.ContractualToPermanentDate = DateTime.Now;
            bool IsSMCFundedProjects = false;
            bool IsProgramContractual = false;

            int EmpTypeId = 1;

            string ExtensionToDate = "";
            string RenewStartDate = "";
            Int32 ContractPreiod = 0;
            ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);

            DataTable aDataTable = aContractualEmpManageDAL.GetUpliftingDate(EmpId.Value);

            if (aDataTable.Rows.Count > 0)
            {


                int? CompanyId = null;
                try
                {
                    CompanyId = (int?)aDataTable.Rows[0]["New_CompanyId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? DivisionId = null;
                try
                {
                    DivisionId = (int?)aDataTable.Rows[0]["New_DivisionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DivisionWId = null;
                try
                {
                    DivisionWId = (int?)aDataTable.Rows[0]["New_DivisionWId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DepartmentId = null;
                try
                {
                    DepartmentId = (int?)aDataTable.Rows[0]["New_DepartmentId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SectionId = null;
                try
                {
                    SectionId = (int?)aDataTable.Rows[0]["New_SectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SubSectionId = null;
                try
                {
                    SubSectionId = (int?)aDataTable.Rows[0]["New_SubSectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? EmpCategoryId = null;
                try
                {
                    EmpCategoryId = (int?)aDataTable.Rows[0]["New_EmpCategoryId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SalaryGradeId = null;
                try
                {
                    SalaryGradeId = (int?)aDataTable.Rows[0]["New_SalaryGradeId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryStepId = null;
                try
                {
                    SalaryStepId = (int?)aDataTable.Rows[0]["New_SalaryStepId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationId = null;
                try
                {
                    DesignationId = (int?)aDataTable.Rows[0]["New_DesignationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationTypeId = null;
                try
                {
                    DesignationTypeId = (int?)aDataTable.Rows[0]["New_DesignationTypeId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                int? JobLocationId = null;
                try
                {
                    JobLocationId = (int?)aDataTable.Rows[0]["New_JobLocationId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryLoationId = null;
                try
                {
                    SalaryLoationId = (int?)aDataTable.Rows[0]["New_SalaryLoationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                string Floor = aDataTable.Rows[0]["New_Floor"].ToString();


                bool IsSeparation = false;
                try
                {
                    IsSeparation = Convert.ToBoolean(aDataTable.Rows[0]["New_IsSeparation"].ToString());
                }
                catch (Exception)
                {

                    //throw;
                }


              


                if (IsSeparation)
                {
                    aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpIDReappointment(str,
                        IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                        string.IsNullOrEmpty(txtEffectiveDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                ? (DateTime?)null
                                : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                    ? (DateTime?)null
                                    : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId,
                        Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId,
                        SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                        DesignationTypeId, JobLocationId, SalaryLoationId, Floor);
                }
                else
                {
                    aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects,
                        IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                        string.IsNullOrEmpty(txtEffectiveDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txtEffectiveDate.Text).Date, null, null, 0, 1,
                        Convert.ToInt32(Session["UserId"]));

                }


            }
            else
            {
                aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects,
                        IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                        string.IsNullOrEmpty(txtEffectiveDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txtEffectiveDate.Text).Date, null, null, 0, 1,
                        Convert.ToInt32(Session["UserId"]));
            }




          

            using (var db = new HRIS_SMCEntities())
            {
                var hhhh = db.tblEmpGeneralInfoes.OrderByDescending(u => u.EmpInfoId).FirstOrDefault();


                CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                        hhhh.EmpInfoId.ToString());
                }

                EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeContracttoParmanent(hhhh.EmpInfoId))
                {

                }
            }

        }
    }

    private void Permanent_TO_Contractual(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "Permanent to Contractual")
        {


            aContractualEmpManageDAO.IsPermanentToContractual = true;
            aContractualEmpManageDAO.isToProject = false;

            aContractualEmpManageDAO.IsExtension = false;
            aContractualEmpManageDAO.IsRenew = false;
            aContractualEmpManageDAO.IsContractualToPermanent = false;
            aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;
            aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
            aContractualEmpManageDAO.PermanentToContractualEffectiveDate =
                Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim());
            aContractualEmpManageDAO.PermanentToContractualEndDate =
                Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim());

            Int32 empGenId = 0;
            Int32 EmpTypeId = 0;
            string ExtensionToDate = "";
            string RenewStartDate = "";

            empGenId = Convert.ToInt32(EmpId.Value);
            EmpTypeId = 2;

            RenewStartDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim()).ToString();
            ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();
            Int32 ContractPreiod = 0;
            ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
            bool IsSMCFundedProjects = false;
            bool IsProgramContractual = false;
            DataTable aDataTable = aContractualEmpManageDAL.GetUpliftingDate(EmpId.Value);

            if (aDataTable.Rows.Count > 0)
            {


                int? CompanyId = null;
                try
                {
                    CompanyId = (int?)aDataTable.Rows[0]["New_CompanyId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? DivisionId = null;
                try
                {
                    DivisionId = (int?)aDataTable.Rows[0]["New_DivisionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DivisionWId = null;
                try
                {
                    DivisionWId = (int?)aDataTable.Rows[0]["New_DivisionWId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DepartmentId = null;
                try
                {
                    DepartmentId = (int?)aDataTable.Rows[0]["New_DepartmentId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SectionId = null;
                try
                {
                    SectionId = (int?)aDataTable.Rows[0]["New_SectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SubSectionId = null;
                try
                {
                    SubSectionId = (int?)aDataTable.Rows[0]["New_SubSectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? EmpCategoryId = null;
                try
                {
                    EmpCategoryId = (int?)aDataTable.Rows[0]["New_EmpCategoryId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SalaryGradeId = null;
                try
                {
                    SalaryGradeId = (int?)aDataTable.Rows[0]["New_SalaryGradeId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryStepId = null;
                try
                {
                    SalaryStepId = (int?)aDataTable.Rows[0]["New_SalaryStepId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationId = null;
                try
                {
                    DesignationId = (int?)aDataTable.Rows[0]["New_DesignationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationTypeId = null;
                try
                {
                    DesignationTypeId = (int?)aDataTable.Rows[0]["New_DesignationTypeId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                int? JobLocationId = null;
                try
                {
                    JobLocationId = (int?)aDataTable.Rows[0]["New_JobLocationId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryLoationId = null;
                try
                {
                    SalaryLoationId = (int?)aDataTable.Rows[0]["New_SalaryLoationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                string Floor = aDataTable.Rows[0]["New_Floor"].ToString();


                bool IsSeparation = false;
                try
                {
                    IsSeparation = Convert.ToBoolean(aDataTable.Rows[0]["New_IsSeparation"].ToString());
                }
                catch (Exception)
                {

                    //throw;
                }


                if (chkReappointment.Checked)
                {
                    aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpIDReappointment(str,
                        IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                        string.IsNullOrEmpty(txtEffectiveDate.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                ? (DateTime?) null
                                : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                    ? (DateTime?) null
                                    : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId,
                        Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId,
                        SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                        DesignationTypeId, JobLocationId, SalaryLoationId, Floor);
                }
                else
                {
                    aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects,
                        IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                        string.IsNullOrEmpty(txtEffectiveDate.Text)
                            ? (DateTime?) null
                            : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                ? (DateTime?) null
                                : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                    ? (DateTime?) null
                                    : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId,
                        Convert.ToInt32(Session["UserId"]));
                }

            }
            else
            {
                aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects,
                      IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(),
                      string.IsNullOrEmpty(txtEffectiveDate.Text)
                          ? (DateTime?)null
                          : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                              ? (DateTime?)null
                              : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                  ? (DateTime?)null
                                  : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId,
                      Convert.ToInt32(Session["UserId"]));
            }



            

            EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
            ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
            /// 

            using (var db = new HRIS_SMCEntities())
            {
                var hhhh = db.tblEmpGeneralInfoes.OrderByDescending(u => u.EmpInfoId).FirstOrDefault();

                CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                        hhhh.EmpInfoId.ToString());
                }
                using (
                    DataTable dtEmpCode =
                        _empdal.GetEmpMasterCodeParmanenttoContractual(hhhh.EmpInfoId))
                {

                }
            }
        }
    }
 
    private void Renew_Func(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "Renew")
        {
            aContractualEmpManageDAO.IsExtension = false;
            aContractualEmpManageDAO.IsRenew = true;
            aContractualEmpManageDAO.IsPermanentToContractual = false;
            aContractualEmpManageDAO.IsContractualToPermanent = false;
            aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;
            aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
            aContractualEmpManageDAO.RenewStartDate = Convert.ToDateTime(RenewStartDateTextBox.Text.Trim());
            aContractualEmpManageDAO.RenewToDate = Convert.ToDateTime(RenewToDateTextBox.Text.Trim());
            aContractualEmpManageDAO.isToProject = false;


            
                Int32 empGenId = 0;
                string RenewStartDate = "";
                string RenewToDate = "";


                int? EmpTypeId = Convert.ToInt32(HFEmpTypeID.Value);

                empGenId = Convert.ToInt32(EmpId.Value);

                RenewStartDate = Convert.ToDateTime(RenewStartDateTextBox.Text.Trim()).ToString();
                RenewToDate = Convert.ToDateTime(RenewToDateTextBox.Text.Trim()).ToString();


                EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                DataTable dtCEndDate = _empdal.GetContractEndDate(empGenId);
                //DataTable dtEmpCode = _empdal.GetEmpMasterCode(empGenId);
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;

              

             DataTable aDataTable = aContractualEmpManageDAL.GetUpliftingDate(EmpId.Value);

            if (aDataTable.Rows.Count > 0)
            {


                int? CompanyId = null;
                try
                {
                    CompanyId = (int?) aDataTable.Rows[0]["New_CompanyId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? DivisionId = null;
                try
                {
                    DivisionId = (int?) aDataTable.Rows[0]["New_DivisionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DivisionWId = null;
                try
                {
                    DivisionWId = (int?) aDataTable.Rows[0]["New_DivisionWId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DepartmentId = null;
                try
                {
                    DepartmentId = (int?) aDataTable.Rows[0]["New_DepartmentId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SectionId = null;
                try
                {
                    SectionId = (int?) aDataTable.Rows[0]["New_SectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SubSectionId = null;
                try
                {
                    SubSectionId = (int?) aDataTable.Rows[0]["New_SubSectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? EmpCategoryId = null;
                try
                {
                    EmpCategoryId = (int?) aDataTable.Rows[0]["New_EmpCategoryId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SalaryGradeId = null;
                try
                {
                    SalaryGradeId = (int?) aDataTable.Rows[0]["New_SalaryGradeId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryStepId = null;
                try
                {
                    SalaryStepId = (int?) aDataTable.Rows[0]["New_SalaryStepId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationId = null;
                try
                {
                    DesignationId = (int?) aDataTable.Rows[0]["New_DesignationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationTypeId = null;
                try
                {
                    DesignationTypeId = (int?) aDataTable.Rows[0]["New_DesignationTypeId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                int? JobLocationId = null;
                try
                {
                    JobLocationId = (int?) aDataTable.Rows[0]["New_JobLocationId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryLoationId = null;
                try
                {
                    SalaryLoationId = (int?) aDataTable.Rows[0]["New_SalaryLoationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                string Floor = aDataTable.Rows[0]["New_Floor"].ToString();


                bool IsSeparation = false;
                try
                {
                    IsSeparation = Convert.ToBoolean(aDataTable.Rows[0]["New_IsSeparation"].ToString());
                }
                catch (Exception)
                {

                    //throw;
                }





                //if (IsSeparation)
                //{
                aContractualEmpManageDAL.UpdateUplifitingInfo(EmpId.Value,

                    Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId,
                    SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                    DesignationTypeId, JobLocationId, SalaryLoationId, Floor);

            }
            // day gap betewwn 2 contract **
                try
                {
                    if (Convert.ToDateTime(dtCEndDate.Rows[0]["ContractEndDate"].ToString()).AddDays(1) <
                        Convert.ToDateTime(RenewStartDateTextBox.Text.Trim()))
                    {

                        try
                        {
                            aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(), string.IsNullOrEmpty(txtEffectiveDate.Text)
                                              ? (DateTime?)null
                                              : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                              ? (DateTime?)null
                                              : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(RenewToDate)
                                              ? (DateTime?)null
                                              : DateTime.Parse(RenewToDate).Date, ContractPreiod, EmpTypeId, Convert.ToInt32(Session["UserId"]));
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value

                        using (var db = new HRIS_SMCEntities())
                        {
                            var hhhh = db.tblEmpGeneralInfoes.OrderByDescending(u => u.EmpInfoId).FirstOrDefault();

                            CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
                            for (int i = 0; i < loadGridView.Rows.Count; i++)
                            {
                                _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                                    hhhh.EmpInfoId.ToString());
                            }
                            using (
                                DataTable dtEmpCode =
                                    _empdal.GetEmpMasterCodeForNewEntry(hhhh.EmpInfoId))
                            {

                            }
                        }
                    }
                    else
                    {
                        aContractualEmpManageDAL.UpdateRenewEndDateChange(aContractualEmpManageDAO.EmployeeId, string.IsNullOrEmpty(RenewStartDate)
                                               ? (DateTime?)null
                                               : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(RenewToDate)
                                               ? (DateTime?)null
                                               : DateTime.Parse(RenewToDate).Date, ContractPreiod, Convert.ToInt32(Session["UserId"]));
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            
        }
    }


    private void Extension_Func(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "Extension")
        {
            aContractualEmpManageDAO.IsExtension = true;
            aContractualEmpManageDAO.IsRenew = false;
            aContractualEmpManageDAO.IsPermanentToContractual = false;
            aContractualEmpManageDAO.IsContractualToPermanent = false;
            aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;
            aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
            aContractualEmpManageDAO.isToProject = false;
            aContractualEmpManageDAO.ExtensionFromDate = Convert.ToDateTime(ExtensionFromDateTextBox.Text.Trim());
            aContractualEmpManageDAO.ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim());

            
                Int32 empGenId = 0;

            DataTable aDataTable = aContractualEmpManageDAL.GetUpliftingDate(EmpId.Value);

            if (aDataTable.Rows.Count > 0)
            {


                int? CompanyId = null;
                try
                {
                    CompanyId = (int?) aDataTable.Rows[0]["New_CompanyId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? DivisionId = null;
                try
                {
                    DivisionId = (int?) aDataTable.Rows[0]["New_DivisionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DivisionWId = null;
                try
                {
                    DivisionWId = (int?) aDataTable.Rows[0]["New_DivisionWId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DepartmentId = null;
                try
                {
                    DepartmentId = (int?) aDataTable.Rows[0]["New_DepartmentId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SectionId = null;
                try
                {
                    SectionId = (int?) aDataTable.Rows[0]["New_SectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SubSectionId = null;
                try
                {
                    SubSectionId = (int?) aDataTable.Rows[0]["New_SubSectionId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? EmpCategoryId = null;
                try
                {
                    EmpCategoryId = (int?) aDataTable.Rows[0]["New_EmpCategoryId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? SalaryGradeId = null;
                try
                {
                    SalaryGradeId = (int?) aDataTable.Rows[0]["New_SalaryGradeId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryStepId = null;
                try
                {
                    SalaryStepId = (int?) aDataTable.Rows[0]["New_SalaryStepId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationId = null;
                try
                {
                    DesignationId = (int?) aDataTable.Rows[0]["New_DesignationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                int? DesignationTypeId = null;
                try
                {
                    DesignationTypeId = (int?) aDataTable.Rows[0]["New_DesignationTypeId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                int? JobLocationId = null;
                try
                {
                    JobLocationId = (int?) aDataTable.Rows[0]["New_JobLocationId"];
                }
                catch (Exception)
                {

                    //throw;
                }

                int? SalaryLoationId = null;
                try
                {
                    SalaryLoationId = (int?) aDataTable.Rows[0]["New_SalaryLoationId"];
                }
                catch (Exception)
                {

                    //throw;
                }
                string Floor = aDataTable.Rows[0]["New_Floor"].ToString();


                bool IsSeparation = false;
                try
                {
                    IsSeparation = Convert.ToBoolean(aDataTable.Rows[0]["New_IsSeparation"].ToString());
                }
                catch (Exception)
                {

                    //throw;
                }





                //if (IsSeparation)
                //{
                aContractualEmpManageDAL.UpdateUplifitingInfo(EmpId.Value,

                    Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId,
                    SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                    DesignationTypeId, JobLocationId, SalaryLoationId, Floor);

                string ExtensionStartDate = "";
                string ExtensionToDate = "";
                empGenId = Convert.ToInt32(EmpId.Value);
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                ExtensionStartDate = Convert.ToDateTime(ExtensionFromDateTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim()).ToString();
                UpdateEmployeeContractualDeate(empGenId, ExtensionStartDate, ExtensionToDate, ContractPreiod);
                //}
                //else
                //{
                //    aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(EmpId.Value,
                //        string.IsNullOrEmpty(txtEffectiveDate.Text)
                //            ? (DateTime?)null
                //            : DateTime.Parse(txtEffectiveDate.Text).Date, null, null, 0, 1,
                //        Convert.ToInt32(Session["UserId"]));

                //}
            }
            else
            {
                string ExtensionStartDate = "";
                string ExtensionToDate = "";
                empGenId = Convert.ToInt32(EmpId.Value);
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                ExtensionStartDate = Convert.ToDateTime(ExtensionFromDateTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim()).ToString();
                UpdateEmployeeContractualDeate(empGenId, ExtensionStartDate, ExtensionToDate, ContractPreiod);
            }

          

              

             
            

        }
    }
    private void UpdateEmployeEmpTypeID()
    {
        DataTable aDataTable = aContractualEmpManageDAL.GetUpliftingDate(EmpId.Value);

        if (aDataTable.Rows.Count > 0)
        {


            int? CompanyId = null;
            try
            {
                CompanyId = (int?)aDataTable.Rows[0]["New_CompanyId"];
            }
            catch (Exception)
            {

                //throw;
            }

            int? DivisionId = null;
            try
            {
                DivisionId = (int?)aDataTable.Rows[0]["New_DivisionId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? DivisionWId = null;
            try
            {
                DivisionWId = (int?)aDataTable.Rows[0]["New_DivisionWId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? DepartmentId = null;
            try
            {
                DepartmentId = (int?)aDataTable.Rows[0]["New_DepartmentId"];
            }
            catch (Exception)
            {

                //throw;
            }

            int? SectionId = null;
            try
            {
                SectionId = (int?)aDataTable.Rows[0]["New_SectionId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? SubSectionId = null;
            try
            {
                SubSectionId = (int?)aDataTable.Rows[0]["New_SubSectionId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? EmpCategoryId = null;
            try
            {
                EmpCategoryId = (int?)aDataTable.Rows[0]["New_EmpCategoryId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? SalaryGradeId = null;
            try
            {
                SalaryGradeId = (int?)aDataTable.Rows[0]["New_SalaryGradeId"];
            }
            catch (Exception)
            {

                //throw;
            }

            int? SalaryStepId = null;
            try
            {
                SalaryStepId = (int?)aDataTable.Rows[0]["New_SalaryStepId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? DesignationId = null;
            try
            {
                DesignationId = (int?)aDataTable.Rows[0]["New_DesignationId"];
            }
            catch (Exception)
            {

                //throw;
            }
            int? DesignationTypeId = null;
            try
            {
                DesignationTypeId = (int?)aDataTable.Rows[0]["New_DesignationTypeId"];
            }
            catch (Exception)
            {

                //throw;
            }
            //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
            int? JobLocationId = null;
            try
            {
                JobLocationId = (int?)aDataTable.Rows[0]["New_JobLocationId"];
            }
            catch (Exception)
            {

                //throw;
            }

            int? SalaryLoationId = null;
            try
            {
                SalaryLoationId = (int?)aDataTable.Rows[0]["New_SalaryLoationId"];
            }
            catch (Exception)
            {

                //throw;
            }
            string Floor = aDataTable.Rows[0]["New_Floor"].ToString();


            bool IsSeparation = false;
            try
            {
                IsSeparation = Convert.ToBoolean(aDataTable.Rows[0]["New_IsSeparation"].ToString());
            }
            catch (Exception)
            {

                //throw;
            }





            //if (IsSeparation)
            //{
                //aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpIDReappointment(EmpId.Value,
                    
                //    Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId,
                //    SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                //    DesignationTypeId, JobLocationId, SalaryLoationId, Floor);
            //}
            //else
            //{
            //    aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(EmpId.Value,
            //        string.IsNullOrEmpty(txtEffectiveDate.Text)
            //            ? (DateTime?)null
            //            : DateTime.Parse(txtEffectiveDate.Text).Date, null, null, 0, 1,
            //        Convert.ToInt32(Session["UserId"]));

            //}


        }
        


    }
    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
        aMaster.ContractualEmpManageId
            = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = aContractualEmpManageDAL.UpdateContractural(aMaster);
        int commentid = aContractualEmpManageDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
            aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='ContractualEmpApprovalList.aspx';",
                   true);
    }
    private void UpdateEmployeeContractualDeate(int empGenId, string ExtensionStartDate, string ExtensionToDate, int? ContractPreiod)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractStartDate = string.IsNullOrEmpty(ExtensionStartDate)
                            ? (DateTime?)null
                            : DateTime.Parse(ExtensionStartDate).Date;


        aInfo.ContractEndDate = string.IsNullOrEmpty(ExtensionToDate)
                           ? (DateTime?)null
                           : DateTime.Parse(ExtensionToDate).Date;

        aInfo.EmpInfoId = empGenId;
        aInfo.ContractPeriod = ContractPreiod;

        aContractualEmpManageDAL.UpdateEmployeeContractEndDateInfo(aInfo);

    }

    private void UpLiftingDatainEmpUpdate(int empGenId, string ExtensionToDate, int EmpTypeId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();
        aInfo.EmpInfoId = Convert.ToInt32(EmpId.Value);
        aInfo.CompanyInfoId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?) null;
        aInfo.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?) null;
        aInfo.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?) null;
        aInfo.DepId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?) null;
        aInfo.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?) null;
        aInfo.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?) null;
        aInfo.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?) null;
        aInfo.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?) null;
        aInfo.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?) null;
        aInfo.DesigId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?) null;
        aInfo.DesignationTypeId = ddlDesignationType.SelectedIndex > 0
            ? int.Parse(ddlDesignationType.SelectedValue)
            : (int?) null;
        aInfo.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?) null;
        aInfo.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0
            ? int.Parse(ddlSalaryLocation.SelectedValue)
            : (int?) null;
        aInfo.Floor = txtFloor.Text;
    }

    private void UpdateEmployePermanenttoContractualInfoEmpTypeID(int empGenId, string ExtensionToDate, int EmpTypeId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractEndDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;
        aInfo.EmpTypeId = EmpTypeId;

        aContractualEmpManageDAL.UpdateEmployePermanenttoContractualInfoEmpTypeID(aInfo);

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
        aMaster.ContractualEmpManageId
            = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
        aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
        bool status = aContractualEmpManageDAL.UpdateContractural(aMaster);
        if (status)
        {
            if (aMaster.ActionStatus == "Verified")
            {
                DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO()
                {
                    ActionStatus = actionRadioButtonList.SelectedValue,
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                    ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                    ContractualEmpManageId = aMaster.ContractualEmpManageId,
                    Comments = commentsTextBox.Text,

                };
                int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
            }
            else if (aMaster.ActionStatus == "Approved")
            {
                //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO()
                {
                    ActionStatus = actionRadioButtonList.SelectedValue,
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                    ForEmpInfoId = 0,
                    ContractualEmpManageId = aMaster.ContractualEmpManageId,
                    Comments = commentsTextBox.Text,

                };
                int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
            }
            else if (aMaster.ActionStatus == "Review")
            {
                DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), ContractualEmpManageIdHiddenField.Value);
                DataTable dtempdata2 = aContractualEmpManageDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), ContractualEmpManageIdHiddenField.Value);

                if (dtempdata2.Rows.Count > 0)
                {
                    ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO()
                    {
                        ActionStatus = "Verified",
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                        ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                        ContractualEmpManageId = aMaster.ContractualEmpManageId,
                        Comments = commentsTextBox.Text,

                    };
                    aContractualEmpManageDAL.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                    int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                }
                else
                {
                    ShowMessageBox("Please select Approval Status Approved  this!!!");
                }
            }


        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Saved Successfully...');window.location ='JdApprovalList.aspx';",
                   true);


    }
    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }



    public void VisibleGrid(string mid)
    {
        DataTable dtdataDetails = aProbationperiodDal.GetContractTualEvulationInfo(mid);
        if (dtdataDetails.Rows.Count > 0)
        {

            evgrid.Visible = true;

            for (int j = 0; j < gv_ProbationEvaluation.Rows.Count; j++)
            {
                for (int i = 0; i < dtdataDetails.Rows.Count; i++)
                {
                    if (((HiddenField) gv_ProbationEvaluation.Rows[j].FindControl("hdpkd")).Value ==
                        dtdataDetails.Rows[i]["ValueField"].ToString())
                    {

                        if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsExcellent"].ToString()) == true)
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                0]
                                .Selected = true;
                        }
                        else
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                0]
                                .Selected = false;
                        }

                        if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsGood"].ToString()) == true)
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                1]
                                .Selected = true;
                        }
                        else
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                1]
                                .Selected = false;
                        }

                        if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsSatisfactory"].ToString()) == true)
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                2]
                                .Selected = true;
                        }
                        else
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                2]
                                .Selected = false;
                        }

                        if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsNotSatisfactory"].ToString()) == true)
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                3]
                                .Selected = true;
                        }
                        else
                        {
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                3]
                                .Selected = false;
                        }

                    }
                }





            }
        }
    

if (ReportingBoss.Value.ToString() != "0")
        {
            if (ReportingBoss.Value == Session["EmpInfoId"].ToString())
            {
                for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                {


                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                        .Enabled = true;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                        .Enabled = true;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                        .Enabled = true;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                        .Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                {


                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                        .Enabled = false;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                        .Enabled = false;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                        .Enabled = false;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                        .Enabled = false;
                }
            }
        }
    }

    protected void btnBehavioralClose_Click(object sender, EventArgs e)
    {
        MPBehavioral.Hide();
        //chkReappointment.Checked = false;
    }


    private bool Validation()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a company!!!", this);
            companyDropDownList.Focus();
            return false;
        }



        //if (Session["CheckReporting"] == "ReportingEmpId")
        //{
        //    if (SelectValidation() == false)
        //    {
        //        return false;
        //    }
        //}


        if (actionRadioButtonList.SelectedValue != "Review")
        {
        
        if (ReportingBoss.Value.ToString() != "0")
        {
            if (ReportingBoss.Value == Session["EmpInfoId"].ToString())
            {

                if (txtEffectiveDate.Text == "")
                {
                    aShowMessage.ShowMessageBox("Please Enter Effective Date!!!", this);
                    txtEffectiveDate.Focus();
                    return false;
                }

                if (ExtentionRenewRadioButtonList.SelectedValue == "")
                {
                    aShowMessage.ShowMessageBox("Please Select State Change From Radio Button!!!", this);

                    return false;
                }



                if (ExtentionRenewRadioButtonList.Items[0].Selected == true)
                {



                    if (ExtensionFromDateTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        ExtensionFromDateTextBox.Focus();
                        return false;
                    }
                    if (ExtensionToDateTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        ExtensionToDateTextBox.Focus();
                        return false;
                    }


                   
                }

                if (ExtentionRenewRadioButtonList.Items[1].Selected == true)
                {
                    if (RenewStartDateTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        RenewStartDateTextBox.Focus();
                        return false;
                    }
                    if (RenewToDateTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        RenewToDateTextBox.Focus();
                        return false;
                    }

                    
                        
                   

                 

                }


                if (ExtentionRenewRadioButtonList.Items[2].Selected == true)
                {




                    if (PermanentToContractualEffectiveDaeTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        PermanentToContractualEffectiveDaeTextBox.Focus();
                        return false;
                    }


                    if (PermanentToContractualEndDateTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        PermanentToContractualEndDateTextBox.Focus();
                        return false;
                    }

 



                }


                if (ExtentionRenewRadioButtonList.Items[3].Selected == true)
                {



                   

                }


                if (txtEffectiveDate.Text == "")
                {
                    aShowMessage.ShowMessageBox("Please Enter Effective Date !!!", this);
                    txtEffectiveDate.Focus();
                    return false;
                }

                if (ExtentionRenewRadioButtonList.Items[6].Selected == true)
                {

                    if (PermanentToContractualEffectiveDaeTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        PermanentToContractualEffectiveDaeTextBox.Focus();
                        return false;
                    }


                    if (PermanentToContractualEndDateTextBox.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                        PermanentToContractualEndDateTextBox.Focus();
                        return false;
                    }

                    if (ddlProject.SelectedValue == "0")
                    {

                        aShowMessage.ShowMessageBox("Please Select Project Name", this);
                        ddlProject.Focus();

                        return false;
                    }
                }

                if (chkIsSeparation.Checked)
                {
                    if (JobLeftTypeDropDownList.SelectedValue == "")
                    {
                 
                        aShowMessage.ShowMessageBox("Please Select Job Left Type !!!", this);
                        JobLeftTypeDropDownList.Focus();
                        return false;
                    }

                    if (JobLeftDateTextBox.Text == String.Empty)
                    {
                        
                        aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
                        JobLeftDateTextBox.Focus();
                        return false;
                    }

                }

                if (chkOrganization.Checked)
                {
                    if (ddlDivision.SelectedIndex <= 0)
                    {

                        aShowMessage.ShowMessageBox("Please Select Division", this);
                        ddlDivision.Focus();
                        return false;
                    }
                    if (ddlDepartment.SelectedIndex <= 0)
                    {

                        aShowMessage.ShowMessageBox("Please Select Department", this);
                        ddlDepartment.Focus();
                        return false;
                    }
                }


                if (chkSalary.Checked)
                {
                    if (ddlSalaryGrade.SelectedIndex <= 0)
                    {

                        aShowMessage.ShowMessageBox("Please Select Salary Grade", this);
                        ddlSalaryGrade.Focus();
                        return false;
                    }
                    if (ddlSalaryStep.SelectedIndex <= 0)
                    {

                        aShowMessage.ShowMessageBox("Please Select Salary Step", this);
                        ddlSalaryStep.Focus();
                        return false;
                    }
                }


                if (chkPlace.Checked)
                {
                    if (ddlSalaryLocation.SelectedIndex <= 0)
                    {

                        aShowMessage.ShowMessageBox("Please Select Office", this);
                        ddlSalaryLocation.Focus();
                        return false;
                    }
                    if (ddlJobLocation.SelectedIndex <= 0)
                    {

                        aShowMessage.ShowMessageBox("Please Select Place", this);
                        ddlJobLocation.Focus();
                        return false;
                    }

                    if (txtFloor.Text == String.Empty)
                    {

                        aShowMessage.ShowMessageBox("Please Select Floor !!!", this);
                        txtFloor.Focus();
                        return false;
                    }
                }

            for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                {
                    int a = 0;
                    for (int j = 0;
                        j <
                        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items.Count;
                        j++)
                    {
                        if (
                            ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[j]
                                .Selected == false)
                        {
                            a++;
                        }
                    }

                    if (a ==
                        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items.Count)
                    {
                        ShowMessageBox("Please evaluate !!");
                        return false;
                    }
                }



                //if (ExtentionRenewRadioButtonList.SelectedValue == "")
                //{
                //    aShowMessage.ShowMessageBox("Please Select State Change From Radio Button!!!", this);

                //    return false;
                //}



                //if (ExtentionRenewRadioButtonList.Items[0].Selected == true)
                //{



                //    if (ExtensionFromDateTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        ExtensionFromDateTextBox.Focus();
                //        return false;
                //    }
                //    if (ExtensionToDateTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        ExtensionToDateTextBox.Focus();
                //        return false;
                //    }


                //    return true;
                //}

                //if (ExtentionRenewRadioButtonList.Items[1].Selected == true)
                //{
                //    if (RenewStartDateTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        RenewStartDateTextBox.Focus();
                //        return false;
                //    }
                //    if (RenewToDateTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        RenewToDateTextBox.Focus();
                //        return false;
                //    }

                //    if (RenewReturn() == true)
                //    {
                //        if (chkIsSeparation.Checked)
                //        {
                //            if (JobLeftTypeDropDownList.SelectedValue == "")
                //            {
                //                MPBehavioral.Show();
                //                aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
                //                JobLeftTypeDropDownList.Focus();
                //                return false;
                //            }

                //            if (JobLeftDateTextBox.Text == String.Empty)
                //            {
                //                MPBehavioral.Show();
                //                aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
                //                JobLeftDateTextBox.Focus();
                //                return false;
                //            }

                //        }
                //        if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
                //        {
                //            mp1.Show();
                //            return false;
                //        }

                //        if (RemarksTextBox.Text == "")
                //        {
                //            aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //            RemarksTextBox.Focus();
                //            return false;
                //        }
                //    }


                //    return true;

                //}


                //if (ExtentionRenewRadioButtonList.Items[2].Selected == true)
                //{




                //    if (PermanentToContractualEffectiveDaeTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        PermanentToContractualEffectiveDaeTextBox.Focus();
                //        return false;
                //    }


                //    if (PermanentToContractualEndDateTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        PermanentToContractualEndDateTextBox.Focus();
                //        return false;
                //    }


                //    if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
                //    {
                //        mp1.Show();
                //        return false;
                //    }

                //    if (RemarksTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        RemarksTextBox.Focus();
                //        return false;
                //    }




                //    return true;
                //}


                //if (ExtentionRenewRadioButtonList.Items[3].Selected == true)
                //{



                //    if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
                //    {
                //        mp1.Show();
                //        return false;
                //    }

                //    if (RemarksTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        RemarksTextBox.Focus();
                //        return false;
                //    }



                //    return true;
                //}


                //if (txtEffectiveDate.Text == "")
                //{
                //    aShowMessage.ShowMessageBox("Please Enter Effective Date !!!", this);
                //    txtEffectiveDate.Focus();
                //    return false;
                //}

                //if (ExtentionRenewRadioButtonList.Items[6].Selected == true)
                //{

                //    if (PermanentToContractualEffectiveDaeTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        PermanentToContractualEffectiveDaeTextBox.Focus();
                //        return false;
                //    }


                //    if (PermanentToContractualEndDateTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        PermanentToContractualEndDateTextBox.Focus();
                //        return false;
                //    }

                //    if (ddlProject.SelectedValue == "0")
                //    {

                //        aShowMessage.ShowMessageBox("Please Select Project Name", this);
                //        ddlProject.Focus();

                //        return false;
                //    }

                //    if (chkIsSeparation.Checked)
                //    {
                //        if (JobLeftTypeDropDownList.SelectedValue == "")
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
                //            JobLeftTypeDropDownList.Focus();
                //            return false;
                //        }

                //        if (JobLeftDateTextBox.Text == String.Empty)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
                //            JobLeftDateTextBox.Focus();
                //            return false;
                //        }

                //    }
                //    if (chkReappointment.Checked)
                //    {



                //        if (ddlCompany.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Company", this);
                //            ddlCompany.Focus();

                //            return false;
                //        }
                //        if (ddlDivision.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Division", this);
                //            ddlDivision.Focus();
                //            return false;
                //        }
                //        if (ddlDepartment.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Department", this);
                //            ddlDepartment.Focus();
                //            return false;
                //        }

                //        if (ddlEmpCategory.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Employee Category", this);
                //            ddlEmpCategory.Focus();
                //            return false;
                //        }

                //        if (ddlSalaryGrade.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Salary Grade", this);
                //            ddlSalaryGrade.Focus();
                //            return false;
                //        }
                //        if (ddlSalaryStep.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Salary Step", this);
                //            ddlSalaryStep.Focus();
                //            return false;
                //        }

                //        if (ddlDesignation.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Designation", this);
                //            ddlDesignation.Focus();
                //            return false;
                //        }


                //        if (ddlSalaryLocation.SelectedIndex <= 0)
                //        {
                //            MPBehavioral.Show();
                //            aShowMessage.ShowMessageBox("Please Select Office", this);
                //            ddlSalaryLocation.Focus();
                //            return false;
                //        }



                //    }




                //    if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
                //    {
                //        mp1.Show();
                //        return false;
                //    }

                //    if (RemarksTextBox.Text == "")
                //    {
                //        aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                //        RemarksTextBox.Focus();
                //        return false;
                //    }
                //}

          
            }

        }
    }

return true;
    }







    public void Update()
    {
        if (SelectValidation())
        {
            ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();

            aContractualEmpManageDAO.ContractualEmpManageId = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
            aContractualEmpManageDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            aContractualEmpManageDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            for (int i = 0; i < ExtentionRenewRadioButtonList.Items.Count; i++)
            {
                if (ExtentionRenewRadioButtonList.Items[i].Selected)
                {
                    string str = ExtentionRenewRadioButtonList.Items[i].Text.Trim();

                    if (str == "Extension")
                    {
                        aContractualEmpManageDAO.IsExtension = true;
                        aContractualEmpManageDAO.IsRenew = false;
                        aContractualEmpManageDAO.IsPermanentToContractual = false;
                        aContractualEmpManageDAO.IsContractualToPermanent = false;
                        aContractualEmpManageDAO.ExtensionFromDate = Convert.ToDateTime(ExtensionFromDateTextBox.Text.Trim());
                        aContractualEmpManageDAO.ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim());
                      
                        Up_lift_SaveForSuppervisor((ContractualEmpManageIdHiddenField.Value));
                    }

                    if (str == "Renew")
                    {
                        aContractualEmpManageDAO.IsExtension = false;
                        aContractualEmpManageDAO.IsRenew = true;
                        aContractualEmpManageDAO.IsPermanentToContractual = false;
                        aContractualEmpManageDAO.IsContractualToPermanent = false;

                        aContractualEmpManageDAO.RenewStartDate = Convert.ToDateTime(RenewStartDateTextBox.Text.Trim());
                        aContractualEmpManageDAO.RenewToDate = Convert.ToDateTime(RenewToDateTextBox.Text.Trim());
                        Up_lift_SaveForSuppervisor((ContractualEmpManageIdHiddenField.Value));

                    }

                    if (str == "Permanent to Contractual")
                    {
                        aContractualEmpManageDAO.IsPermanentToContractual = true;

                        aContractualEmpManageDAO.IsExtension = false;
                        aContractualEmpManageDAO.IsRenew = false;
                        aContractualEmpManageDAO.IsContractualToPermanent = false;
                        aContractualEmpManageDAO.PermanentToContractualEffectiveDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim());
                        Up_lift_SaveForSuppervisor((ContractualEmpManageIdHiddenField.Value));

                    }

                    if (str == "Contractual to Permanent")
                    {
                        aContractualEmpManageDAO.IsContractualToPermanent = true;

                        aContractualEmpManageDAO.IsPermanentToContractual = false;

                        aContractualEmpManageDAO.IsExtension = false;
                        aContractualEmpManageDAO.IsRenew = false;
                        aContractualEmpManageDAO.ContractualToPermanentDate = Convert.ToDateTime(EffectiveDate.Value);
                        Up_lift_SaveForSuppervisor((ContractualEmpManageIdHiddenField.Value));

                    }
                }
            }

            for (int i = 0; i < SalaryIncrementRadioButtonList1.Items.Count; i++)
            {
                if (SalaryIncrementRadioButtonList1.Items[i].Selected)
                {
                    string str = SalaryIncrementRadioButtonList1.Items[i].Text.Trim();

                    if (str == "Salary Increment")
                    {
                        aContractualEmpManageDAO.IsSalaryIncrement = true;
                        aContractualEmpManageDAO.IsNoIncrement = false;
                    }

                    if (str == "No Increment")
                    {
                        aContractualEmpManageDAO.IsNoIncrement = true;
                        aContractualEmpManageDAO.IsSalaryIncrement = false;
                    }
                }
            }


            for (int i = 0; i < FacilityRadioButtonList.Items.Count; i++)
            {
                if (FacilityRadioButtonList.Items[i].Selected)
                {
                    string str = FacilityRadioButtonList.Items[i].Text.Trim();

                    if (str == "Facility Included")
                    {
                        aContractualEmpManageDAO.IsFacilityIncluded = true;
                        aContractualEmpManageDAO.IsNoFacility = false;

                    }

                    if (str == "No Facility")
                    {
                        aContractualEmpManageDAO.IsFacilityIncluded = false;
                        aContractualEmpManageDAO.IsNoFacility = true;
                    }
                }
            }

            aContractualEmpManageDAO.EffectiveDate = string.IsNullOrEmpty(txtEffectiveDate.Text) ? (DateTime?)null : DateTime.Parse(txtEffectiveDate.Text);
            aContractualEmpManageDAO.Remarks = Convert.ToString(RemarksTextBox.Text.Trim());
            aContractualEmpManageDAO.UpdateBy = Session["LoginName"].ToString();
            aContractualEmpManageDAO.UpdateDate = DateTime.Now;
            aContractualEmpManageDAO.ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text.Trim());
            if (aContractualEmpManageDAL.ContractualEmpManageUpdateInfo(aContractualEmpManageDAO))
            {

                SaveProbationDetail(int.Parse(EmpId.Value));

            }

        }
    }



    private void Clear()
    {
        for (int i = 0; i < ExtentionRenewRadioButtonList.Items.Count; i++)
        {
            if (ExtentionRenewRadioButtonList.Items[i].Selected)
            {
                ExtentionRenewRadioButtonList.Items[i].Selected = false;
            }
        }

        for (int i = 0; i < SalaryIncrementRadioButtonList1.Items.Count; i++)
        {
            if (SalaryIncrementRadioButtonList1.Items[i].Selected)
            {
                SalaryIncrementRadioButtonList1.Items[i].Selected = false;
            }
        }

        for (int i = 0; i < FacilityRadioButtonList.Items.Count; i++)
        {
            if (FacilityRadioButtonList.Items[i].Selected)
            {
                FacilityRadioButtonList.Items[i].Selected = false;
            }
        }

        ExtensionFromDateTextBox.Text = "";
        ExtensionToDateTextBox.Text = "";
        PermanentToContractualEffectiveDaeTextBox.Text = "";
        ContractualToPermanentDateTextBox.Text = "";
        RemarksTextBox.Text = "";

        RenewPanelView.Visible = false;
        PermanentToContractualPanelView.Visible = false;
        ExtensionPanelView.Visible = false;
        ContractualToPermanentPanelView.Visible = false;
        ShowPanel.Visible = false;
        companyDropDownList.SelectedValue = "";
        SearchEmployeeNameTextBoxTextBox.Text = "";
        repEmpIdHiddenField.Value = "";
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = companyDropDownList.SelectedValue;
        }
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                repEmpIdHiddenField.Value = emp[0];

                LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                ShowPanel.Visible = true;

            }
            else
            {

                SearchEmployeeNameTextBoxTextBox.Text = "";
                repEmpIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
                ShowPanel.Visible = false;
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select A Company !!", this);
        }
    }

    private void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aContractualEmpManageDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {


            Session["CheckReporting"] = "";
            Session["CheckReporting"] = "ReportingEmpId";

            ReportingBoss.Value = dtdata.Rows[0]["ReportingEmpId"].ToString();

            if (dtdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                ExtentionRenewRadioButtonList.Visible = false;
                SalaryIncrementRadioButtonList1.Visible = false;
                FacilityRadioButtonList.Visible = false;
                ContractPreiod.Visible = false;
                evgrid.Visible = false;
            }

            if (dtdata.Rows[0]["ReportingEmpId"].ToString() == Session["EmpInfoId"].ToString())
            {
                evgrid.Visible = true;
                using (DataTable dt = aContractualEmpManageDAL.GetContractualEvaluationRating())
                {
                    gv_ProbationEvaluation.DataSource = dt;
                    gv_ProbationEvaluation.DataBind();
                }

            }

            repEmpIdHiddenField.Value = id.ToString();
            SearchEmployeeNameTextBoxTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
            mem_EmployeeName.Text = dtdata.Rows[0]["EmpName"].ToString();

            lblComName.Text = dtdata.Rows[0]["CompanyName"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            mem_EmployeeID.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = dtdata.Rows[0]["SalaryGrade"].ToString();
            lblDivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
            lblWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = dtdata.Rows[0]["SubSectionName"].ToString();

            // ddlCompany.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            //ddlCompany_SelectedIndexChanged(null, null);

            using (var db = new HRIS_SMCEntities())
            {
                try
                {
                    var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == id select j).FirstOrDefault();


                    using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(id))
                    {
                        lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                    }



                    ddlCompany.SelectedValue = emp.CompanyId.ToString();
                    ddlCompany_SelectedIndexChanged(null, null);
                    ddlDivision.SelectedValue = emp.DivisionId.ToString();
                    ddlDivision_OnSelectedIndexChanged(null, null);


                    ddlDepartment.SelectedValue = emp.DepartmentId.ToString();
                    ddlDepartment_OnSelectedIndexChanged(null, null);
                    try
                    {
                        ddlWing.SelectedValue = emp.DivisionWId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlWing.SelectedValue = null;
                        //throw;
                    }

                    try
                    {
                        ddlSection.SelectedValue = emp.SectionId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlSection.SelectedValue = null;
                        //throw;
                    }

                    try
                    {
                        ddlSubSection.SelectedValue = emp.SubSectionId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlSubSection.SelectedValue = null;
                        //throw;
                    }

                    try
                    {
                        ddlEmpCategory.SelectedValue = emp.EmpCategoryId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlEmpCategory.SelectedValue = null;
                        //throw;
                    }
                    ddlEmpCategory_OnSelectedIndexChanged(null, null);
                    ddlSalaryGrade.SelectedValue = emp.SalaryGradeId.ToString();
                    ddlSalaryGrade_OnSelectedIndexChanged(null, null);

                    ddlSalaryStep.SelectedValue = emp.SalaryStepId.ToString();

                    ddlDesignation.SelectedValue = emp.DesignationId.ToString();
                    //   NewDesignationDropDownList.SelectedValue = emp.DesignationId.ToString();
                    try
                    {
                        ddlDesignationType.SelectedValue = emp.DesignationTypeId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlDesignationType.SelectedValue = null;
                        //throw;
                    }
                    if (emp.Floor != null)
                    {
                        txtFloor.Text = emp.Floor.ToString();
                    }


                    //ddlEmpType.SelectedValue = emp.EmpTypeId.ToString();
                    //ddlEmpType_OnSelectedIndexChanged(null, null);

                    try
                    {
                        ddlSalaryLocation.SelectedValue = emp.SalaryLoationId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlSalaryLocation.SelectedValue = null;
                        //throw;
                    }
                    using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlSalaryLocation.SelectedValue))
                    {
                        ddlJobLocation.DataSource = dt;
                        ddlJobLocation.DataValueField = "Value";
                        ddlJobLocation.DataTextField = "TextField";
                        ddlJobLocation.DataBind();
                    }
                    try
                    {
                        ddlJobLocation.SelectedValue = emp.JobLocationId.ToString();
                    }
                    catch (Exception)
                    {
                        ddlJobLocation.SelectedValue = null;
                        //throw;
                    }




                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (ContractualEmpManageIdHiddenField.Value != string.Empty)
        {
            //  Update();
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string EmployeeJobLeftId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (aContractualEmpManageDAL.DeleteContractualEmpManageById(EmployeeJobLeftId))
        //    {
        //        LoadInfo();
        //        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);

        //    }
        //}



        if (aContractualEmpManageDAL.DeleteContractualEmpManageById(ContractualEmpManageIdHiddenField.Value))
        {
            aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
            Clear();

        }

        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }

    //tareq

    public void SaveProbationDetail(int id)
    {

        aContractualEmpManageDAL.DeleteEvalution(Convert.ToInt32(ContractualEmpManageIdHiddenField.Value));

        for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
        {
            ProbationEvaluationDetailsDAO aProbationEvaluationDetailsDao = new ProbationEvaluationDetailsDAO();

            aProbationEvaluationDetailsDao.EmpInfoId = id;
            aProbationEvaluationDetailsDao.ContractualEmpManageId = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);

            aProbationEvaluationDetailsDao.ValueField =
                Convert.ToInt32(((HiddenField)gv_ProbationEvaluation.Rows[i].FindControl("hdpkd")).Value);
            aProbationEvaluationDetailsDao.KeyRatingCri =
                ((Label)gv_ProbationEvaluation.Rows[i].FindControl("txt_RatingCriterions")).Text;
            aProbationEvaluationDetailsDao.IsExcellent =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                .Selected;
            aProbationEvaluationDetailsDao.IsGood =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                .Selected;
            aProbationEvaluationDetailsDao.IsSatisfactory =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                .Selected;
            aProbationEvaluationDetailsDao.IsNotSatisfactory =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                .Selected;

            int ida = aContractualEmpManageDAL.SaveContractualtionDetail(aProbationEvaluationDetailsDao);
        }
    }


    
    protected void chkIsSeparation_OnCheckedChanged(object sender, EventArgs e)
    {
        //septype.Visible = false;
        //sepDate.Visible = false;
        //if (chkIsSeparation.Checked)
        //{
        //    septype.Visible = true;
        //    sepDate.Visible = true;
        //}
      //  speparatecheck();
        divSeparate.Visible = false;
        if (chkIsSeparation.Checked)
        {
            divSeparate.Visible = true;
            speparatecheck();
        }

       
    }

    private void speparatecheck()
    {
        if (chkIsSeparation.Checked)
        {
            chkDesignation.Checked = false;
            divDesignation.Visible = false;

            chkPlace.Checked = false;
            divPlace.Visible = false;

            chkSalary.Checked = false;
            divSalary.Visible = false;

            chkOrganization.Checked = false;

            divOrganization.Visible = false;
             
        }
    }

    private void Othercheck()
    {

        chkIsSeparation.Checked = false;
            divSeparate.Visible = false;
         
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

    protected void JobLeftDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (JobLeftDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(JobLeftDateTextBox.Text);

            }
            catch
            {
                JobLeftDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            Session["CompanyId"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(ddlCompany.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(ddlCompany.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(ddlCompany.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComCategory())
            {
                ddlEmpCategory.DataSource = dt;
                ddlEmpCategory.DataValueField = "Value";
                ddlEmpCategory.DataTextField = "TextField";
                ddlEmpCategory.DataBind();
            }


            using (DataTable dt = _commonDataLoad.GetDDLDesignationType())
            {
                ddlDesignationType.DataSource = dt;
                ddlDesignationType.DataValueField = "Value";
                ddlDesignationType.DataTextField = "TextField";
                ddlDesignationType.DataBind();
            }

            //using (DataTable dt = _commonDataLoad.GetDDLJobLocation())
            //{
            //    ddlJobLocation.DataSource = dt;
            //    ddlJobLocation.DataValueField = "Value";
            //    ddlJobLocation.DataTextField = "TextField";
            //    ddlJobLocation.DataBind();
            //}
            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }



        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            ddlWing.Enabled = true;
            try
            {
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            }
            catch (Exception)
            {

                //throw;
            }



            try
            {
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
            catch (Exception)
            {

                //throw;
            }

        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
        }
    }


    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlDepartment, ddlWing.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }


    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "")
            {

                DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
                if (dtgetdata.Rows.Count > 0)
                {
                    if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                    {
                        //  ddlWing.Enabled = false;
                        ddlWing.CssClass = "form-control form-control-sm";
                        ddlWing.Items.Clear();
                        try
                        {
                            _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                        // ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                    else
                    {
                        ddlWing.Enabled = true;
                        ddlWing.CssClass = "form-control form-control-sm";
                        ddlWing.Items.Clear();
                        try
                        {
                            _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }
                }
            }
            else
            {

            }
            if (ddlDepartment.SelectedIndex == 0)
            {
                ddlWing.Enabled = false;
                ddlWing.CssClass = "form-control form-control-sm";
                ddlWing.SelectedValue = "";
                //  ddlWing.DataBind();
                try
                {
                    _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                }
                catch (Exception)
                {
                    ddlWing.SelectedValue = null;
                    //throw;
                }
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }


    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata1 = _commonDataLoad.GetSectionRelaton(ddlSection.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivListAll(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
                try
                {
                    ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }
        DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                try
                {
                    ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                catch (Exception)
                {
                    ddlWing.SelectedValue = null;
                    //throw;
                }
            }
            else
            {
                wing.Visible = true;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
        if (ddlSection.SelectedIndex == 0)
        {
            if (wing.Visible == false)
            {
                wing.Visible = true;
                ddlWing.SelectedValue = null;
                ddlWing.DataBind();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);

            }
            if (dept.Visible == false)
            {
                dept.Visible = true;
                ddlDepartment.SelectedValue = null;
                ddlDepartment.DataBind();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
        }
    }
    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata2 = _commonDataLoad.GetSubSectionRelaton(ddlSubSection.SelectedValue, "");
        if (dtgetdata2.Rows.Count > 0)
        {
            if (dtgetdata2.Rows[0]["Invisible"].ToString() == "True")
            {
                sec.Visible = false;
                ddlSection.Items.Clear();
                _commonDataLoad.GetSectionByDivListAll(ddlSection, ddlDivision.SelectedValue);
                ddlSection.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
            else
            {
                sec.Visible = true;
                ddlSection.Items.Clear();
                _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
                try
                {
                    ddlSection.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
                }
                catch (Exception)
                {
                    ddlSection.SelectedValue = null;
                    //throw;
                }
            }
        }
        DataTable dtgetdata1 = _commonDataLoad.GetSectionRelaton(ddlSection.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivListAll(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                ddlWing.Items.Clear();
                try
                {
                    _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                    ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                catch (Exception)
                {

                    ddlWing.SelectedValue = null;
                }
            }
            else
            {
                wing.Visible = true;
                ddlWing.Items.Clear();
                try
                {
                    _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                    ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                catch (Exception)
                {
                    ddlWing.SelectedValue = null;
                    //throw;
                }
            }
        }

        if (ddlSubSection.SelectedIndex == 0)
        {
            if (wing.Visible == false)
            {
                wing.Visible = true;
                ddlWing.SelectedValue = null;
                ddlWing.DataBind();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);

            }
            if (dept.Visible == false)
            {
                dept.Visible = true;
                ddlDepartment.SelectedValue = null;
                ddlDepartment.DataBind();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
            if (sec.Visible == false)
            {
                sec.Visible = true;
                ddlSection.SelectedValue = null;
                ddlSection.DataBind();
                _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            }
        }
    }
    protected void ddlSalaryGrade_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalaryGrade.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLSalaryStep(ddlSalaryGrade.SelectedValue))
            {
                ddlSalaryStep.DataSource = dt;
                ddlSalaryStep.DataValueField = "Value";
                ddlSalaryStep.DataTextField = "TextField";
                ddlSalaryStep.DataBind();
            }

            //using (DataTable dt = _commonDataLoad.GetDDLDesignationByGrade(int.Parse(ddlSalaryGrade.SelectedValue)))
            //{
            //    ddlDesignation.DataSource = dt;
            //    ddlDesignation.DataValueField = "Value";
            //    ddlDesignation.DataTextField = "TextField";
            //    ddlDesignation.DataBind();
            //}
        }
    }

    protected void ddlSalaryLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlSalaryLocation.SelectedValue))
            {
                ddlJobLocation.DataSource = dt;
                ddlJobLocation.DataValueField = "Value";
                ddlJobLocation.DataTextField = "TextField";
                ddlJobLocation.DataBind();
            }
        }
    }
    protected void ddlEmpCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategory.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLSalaryGrade(ddlEmpCategory.SelectedValue))
            {
                ddlSalaryGrade.DataSource = dt;
                ddlSalaryGrade.DataValueField = "Value";
                ddlSalaryGrade.DataTextField = "TextField";
                ddlSalaryGrade.DataBind();
            }
        }
    }



    private void Up_lift_Save()
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();
        aInfo.EmpInfoId = Convert.ToInt32(EmpId.Value);
        aInfo.CompanyInfoId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
        aInfo.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
        aInfo.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
        aInfo.DepId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;
        aInfo.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
        aInfo.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
        aInfo.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
        aInfo.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;
        aInfo.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
        aInfo.DesigId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
        aInfo.DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
        aInfo.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;
        aInfo.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
        aInfo.Floor = txtFloor.Text;
        aInfo.IsSeparation = chkIsSeparation.Checked;

        if (chkIsSeparation.Checked)
        {
            aInfo.JobLeftTypeId = JobLeftTypeDropDownList.SelectedIndex > 0 ? int.Parse(JobLeftTypeDropDownList.SelectedValue) : (int?)null;
            aInfo.SeparationDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
        }
        else
        {

            aInfo.JobLeftTypeId = null;
            aInfo.SeparationDate = null;

        }
        aContractualEmpManageDAL.Save_Uplift(aInfo);
    }


    private void Up_lift_SaveForSuppervisor(string   ContractualEmpManageIdHid)
    {
        UpLiftEmpDAO aInfo = new UpLiftEmpDAO();
        aInfo.EmpInfoId = Convert.ToInt32(EmpId.Value);
        aInfo.CompanyInfoId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
       
        aInfo.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
      
       
       
       

        aInfo.IsOrganization = chkOrganization.Checked;
        if (chkOrganization.Checked)
        {
            aInfo.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
            aInfo.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
            aInfo.DepId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;
            aInfo.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
            aInfo.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
        }
        else
        {
            aInfo.DivisionId = null;
            aInfo.DivisionWId =null;
            aInfo.DepId = null;
            aInfo.SectionId = null;
            aInfo.SubSectionId = null;
        }

      
        aInfo.IsPlace = chkPlace.Checked;
        if (chkPlace.Checked)
        {
            aInfo.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;
            aInfo.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
            aInfo.Floor = txtFloor.Text;

        }
        else
        {
            aInfo.JobLocationId = null;
            aInfo.SalaryLoationId = null;
            aInfo.Floor = null;
        }

        aInfo.IsSalary = chkSalary.Checked;
        if (chkSalary.Checked)
        {
            aInfo.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;
            aInfo.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
        }
        else
        {
            aInfo.SalaryGradeId = null;
            aInfo.SalaryStepId = null;
        }

        aInfo.IsDesignation = chkDesignation.Checked;
        if (chkDesignation.Checked)
        {
            aInfo.DesigId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
            aInfo.DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
        }
        else
        {
            aInfo.DesigId = null;
            aInfo.DesignationTypeId = null;
        }

        aInfo.IsSeparation = chkIsSeparation.Checked;


        if (chkIsSeparation.Checked)
        {
            aInfo.JobLeftTypeId = JobLeftTypeDropDownList.SelectedIndex > 0 ? int.Parse(JobLeftTypeDropDownList.SelectedValue) : (int?)null;
            aInfo.SeparationDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
        }
        else
        {

            aInfo.JobLeftTypeId = null;
            aInfo.SeparationDate = null;

        }
        aContractualEmpManageDAL.Save_UpliftForSupervisor(aInfo, ContractualEmpManageIdHid);
    }


    protected void chkRedesignation_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkRedesignation.Checked)
        {
            ddlDesignation.Enabled = true;

        }
        else
        {
            ddlDesignation.Enabled = false;


        }
    }


    protected void chkOrganization_OnCheckedChanged(object sender, EventArgs e)
    {
        divOrganization.Visible = false;
        if (chkOrganization.Checked)
        {
            Othercheck();
            divOrganization.Visible = true;

        }
    }

    protected void chkSalary_OnCheckedChanged(object sender, EventArgs e)
    {
        divSalary.Visible = false;
        if (chkSalary.Checked)
        {
            Othercheck();

            divSalary.Visible = true;

        }
    }

    protected void chkPlace_OnCheckedChanged(object sender, EventArgs e)
    {
        divPlace.Visible = false;
        if (chkPlace.Checked)
        {
            Othercheck();

            divPlace.Visible = true;

        }
    }

    protected void chkDesignation_OnCheckedChanged(object sender, EventArgs e)
    {
        divDesignation.Visible = false;
        if (chkDesignation.Checked)
        {
            Othercheck();

            divDesignation.Visible = true;

        }
    }
}