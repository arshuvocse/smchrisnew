using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.ExitManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.Report_DAL;
using DAL.Transfer_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class ContractualEmployeeManagement_UI_ContractualEmpManagement : System.Web.UI.Page
{

    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        ExtentionRenewRadioButtonList.Items[4].Attributes.Add("hidden", "hidden");
        ExtentionRenewRadioButtonList.Items[5].Attributes.Add("hidden", "hidden");
        if (!IsPostBack)
        {

            //   SearchEmployeeNameTextBoxTextBox.ReadOnly = true;
            ButtonVisible();
            ReadOnltDate();
            LoadDropDownList();

            LoadInitialDDL();


            try
            {

                if (Session["ContractualEmpManageId"] != null)
                {
                    ContractualEmpManageIdHiddenField.Value = Session["ContractualEmpManageId"].ToString();
                    GetOneRecord(Session["ContractualEmpManageId"].ToString());
                    Session["ContractualEmpManageId"] = null;
                }

                else
                {
                    string id = Request.QueryString["id"];

                    if (id != null)
                    {

                        if (Convert.ToInt32(id) > 0)
                        {
                            LoadData(Convert.ToInt32(id));
                        }

                    }
                    else
                    {

                    }

                }

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



            string id2 = Request.QueryString["id2"];
            if (Session["PromotionEdit"] != null)
            {
                ContractualEmpManageIdHiddenField.Value = id2;
                Session["PromotionEdit"] = null;

                GetOneRecord(ContractualEmpManageIdHiddenField.Value.ToString());


            }

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
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();

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
        atblEmployeePromotionEntryDAL.LoadNewdesignationDropDownList(NewDesignationDropDownList);
        aEmployeeJobLeftEntryDAL.LoadJobLeftTypeDropDownList(JobLeftTypeDropDownList);
    }
    EmployeeReDesignationDAL atblEmployeePromotionEntryDAL = new EmployeeReDesignationDAL();

    private void ReadOnltDate()
    {
        ExtensionFromDateTextBox.Attributes.Add("readonly", "readonly");
        ExtensionToDateTextBox.Attributes.Add("readonly", "readonly");
        RenewStartDateTextBox.Attributes.Add("readonly", "readonly");
        RenewToDateTextBox.Attributes.Add("readonly", "readonly");
        ContractualToPermanentDateTextBox.Attributes.Add("readonly", "readonly");
        PermanentToContractualEffectiveDaeTextBox.Attributes.Add("readonly", "readonly");
        txtEffectiveDate.Attributes.Add("readonly", "readonly");
        PermanentToContractualEndDateTextBox.Attributes.Add("readonly", "readonly");

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
            Response.Redirect("ContractualEmpList.aspx");
        }

    }


    private void GetOneRecord(string idd)
    {

        submitButton.Text = "Update";
        submitButton.BackColor = Color.DodgerBlue;

        DataTable aDataTable = aContractualEmpManageDAL.GetContractualEmpManageById(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {
            ContractualEmpManageIdHiddenField.Value = idd.ToString();

            SearchEmployeeNameTextBoxTextBox.ReadOnly = false;
            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();



            if (companyDropDownList.SelectedValue != "")
            {
                companyDropDownList_OnSelectedIndexChanged(null, null);
            }
            int empID = 0;
            try
            {
                empID = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId");
            }
            catch (Exception)
            {

                //throw;
            }
            ddlEmpInfo.SelectedValue = empID.ToString();

            using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(ddlEmpInfo.SelectedValue.ToString()))
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

            HFDivID.Value = aDataTable.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = aDataTable.Rows[0]["DivisionWId"].ToString();
            HFDeptID.Value = aDataTable.Rows[0]["DepartmentId"].ToString();
            HFSecID.Value = aDataTable.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = aDataTable.Rows[0]["SubSectionId"].ToString();

            HFEmpCode.Value = aDataTable.Rows[0]["EmployeeCode"].ToString();
            HFEmpTypeID.Value = aDataTable.Rows[0]["EmpTypeId"].ToString();
            HFSalLocID.Value = aDataTable.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = aDataTable.Rows[0]["JobLocationId"].ToString();
            HFDesgId.Value = aDataTable.Rows[0]["DesignationId"].ToString();

            SGradeFF.Value = aDataTable.Rows[0]["SalaryGradeId"].ToString();
            SStepHF.Value = aDataTable.Rows[0]["SalaryStepId"].ToString();

            lblEmpType.Text = aDataTable.Rows[0]["EmpType"].ToString();

            hfIsProgramContractualOP.Value = aDataTable.Rows[0]["IsProgramContractual"].ToString();
            hfIsSMCFundedProjects.Value = aDataTable.Rows[0]["IsSMCFundedProjects"].ToString();

            try
            {
                LoadProjectData(Convert.ToInt32(ddlEmpInfo.SelectedValue));
            }
            catch (Exception)
            {

                //throw;
            }
            lblEmp.Text = aDataTable.Rows[0]["EmployeeName"].ToString();
            SearchEmployeeNameTextBoxTextBox.Text = aDataTable.Rows[0]["EmployeeName"].ToString();


            lblEmployeeCode.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = string.IsNullOrEmpty(aDataTable.Rows[0]["DateOfJoin"].ToString()) ? "" : Convert.ToDateTime(aDataTable.Rows[0]["DateOfJoin"].ToString()).ToString("dd-MMM-yyyy");
            lblDesignation.Text = aDataTable.Rows[0]["Designation"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = aDataTable.Rows[0]["GradeName"].ToString();
            lblDivision.Text = aDataTable.Rows[0]["DivisionName"].ToString();
            lblWing.Text = aDataTable.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = aDataTable.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = aDataTable.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = aDataTable.Rows[0]["SubSectionName"].ToString();

            if ((aDataTable.Rows[0]["ContractEndDate"] != DBNull.Value))
            {
                ContactualEndDateHiddenField.Value = aDataTable.Rows[0]["ContractEndDate"].ToString();
                lblContractEndDate.Text = string.IsNullOrEmpty(aDataTable.Rows[0]["ContractEndDate"].ToString()) ? "" : Convert.ToDateTime(aDataTable.Rows[0]["ContractEndDate"].ToString()).ToString("dd-MMM-yyyy");
            }

            if ((aDataTable.Rows[0]["ContractPreiod"] != DBNull.Value))
            {
                ContractPeriodHF.Value = aDataTable.Rows[0]["ContractPreiod"].ToString();
            }

            ShowPanel.Visible = true;


            ExtentionRenewRadioButtonList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsExtension"].ToString());
            ExtentionRenewRadioButtonList.Items[1].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsRenew"].ToString());
            ExtentionRenewRadioButtonList.Items[2].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsPermanentToContractual"].ToString());
            ExtentionRenewRadioButtonList.Items[3].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsContractualToPermanent"].ToString());

            try
            {
                ExtentionRenewRadioButtonList.Items[4].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsSMCFundedProjectstoSMCContract"].ToString());
            }
            catch (Exception)
            {
                ExtentionRenewRadioButtonList.Items[4].Selected = false;
                //throw;
            }

            try
            {
                ExtentionRenewRadioButtonList.Items[5].Selected = Convert.ToBoolean(aDataTable.Rows[0]["isToProject"].ToString());
            }
            catch (Exception)
            {
                ExtentionRenewRadioButtonList.Items[5].Selected = false;
                //throw;
            }

            try
            {
                ExtentionRenewRadioButtonList.Items[6].Selected = Convert.ToBoolean(aDataTable.Rows[0]["isToProject"].ToString());
            }
            catch (Exception)
            {
                ExtentionRenewRadioButtonList.Items[6].Selected = false;
                //throw;
            }



            if (ExtentionRenewRadioButtonList.Items[0].Selected)
            {
                ExtensionPanelView.Visible = true;
                ExtensionFromDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ExtensionFromDate").ToString("dd-MMM-yyyy");
                ExtensionToDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ExtensionToDate").ToString("dd-MMM-yyyy");



                ExtensionMonthCalculation();
            }

            if (ExtentionRenewRadioButtonList.Items[1].Selected)
            {
                RenewPanelView.Visible = true;
                RenewStartDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("RenewStartDate").ToString("dd-MMM-yyyy");
                RenewToDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("RenewToDate").ToString("dd-MMM-yyyy");
                RenewMonthCalculation();
            }


            if (ExtentionRenewRadioButtonList.Items[2].Selected)
            {
                divReappointment.Visible = true;
                PermanentToContractualPanelView.Visible = true;
                PermanentToContractualEffectiveDaeTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEffectiveDate").ToString("dd-MMM-yyyy");
                PermanentToContractualEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEndDate").ToString("dd-MMM-yyyy");
                PermanenttoContractualMonthCalculation();


            }

            if (ExtentionRenewRadioButtonList.Items[3].Selected)
            {
                divReappointment.Visible = true;
                ContractualToPermanentPanelView.Visible = true;
                ContractualToPermanentDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ContractualToPermanentDate").ToString("dd-MMM-yyyy");
            }
            if (ExtentionRenewRadioButtonList.Items[4].Selected)
            {
                PermanentToContractualPanelView.Visible = true;
                PermanentToContractualEffectiveDaeTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEffectiveDate").ToString("dd-MMM-yyyy");
                PermanentToContractualEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEndDate").ToString("dd-MMM-yyyy");
                PermanenttoContractualMonthCalculation();


            }

            if (ExtentionRenewRadioButtonList.Items[5].Selected)
            {
                PermanentToContractualPanelView.Visible = true;
                PermanentToContractualEffectiveDaeTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEffectiveDate").ToString("dd-MMM-yyyy");
                PermanentToContractualEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEndDate").ToString("dd-MMM-yyyy");
                PermanenttoContractualMonthCalculation();


            }


            if (ExtentionRenewRadioButtonList.Items[6].Selected)
            {
                divReappointment.Visible = true;
                PermanentToContractualPanelView.Visible = true;
                PermanentToContractualEffectiveDaeTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEffectiveDate").ToString("dd-MMM-yyyy");
                PermanentToContractualEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("PermanentToContractualEndDate").ToString("dd-MMM-yyyy");
                PermanenttoContractualMonthCalculation();


            }


            try
            {
                chkReappointment.Checked = Convert.ToBoolean(aDataTable.Rows[0]["isReappointment"].ToString());
            }
            catch (Exception)
            {
                chkReappointment.Checked = false;
                //throw;


            }

            try
            {
                chkRedesignation.Checked = Convert.ToBoolean(aDataTable.Rows[0]["IsRedesignation"].ToString());
                chkRedesignation_OnCheckedChanged(null, null);
            }
            catch (Exception)
            {
                chkRedesignation.Checked = false;
                //throw;
            }
            //  ExtentionRenewRadioButtonList_SelectedIndexChanged(null, null);
            try
            {
                if (aDataTable.Rows[0]["AutoProcess"] != null)
                {


                    if (Convert.ToBoolean(aDataTable.Rows[0]["AutoProcess"]) == true)
                    {
                        manualUpdateCheckBox.Checked = true;
                    }
                    else
                    {
                        manualUpdateCheckBox.Checked = false;
                    }
                }
                else
                {
                    manualUpdateCheckBox.Checked = false;
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            manualUpdateCheckBox.Enabled = false;

            try
            {
                SalaryIncrementRadioButtonList1.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsSalaryIncrement"].ToString());

            }
            catch (Exception)
            {
                
                //throw;
            }
            try
            {
                SalaryIncrementRadioButtonList1.Items[1].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsNoIncrement"].ToString());

            }
            catch (Exception)
            {
                
                //throw;
            }
            try
            {
                txtEffectiveDate.Text = string.IsNullOrEmpty(aDataTable.Rows[0]["EffectiveDate"].ToString()) ? "" : Convert.ToDateTime(aDataTable.Rows[0]["EffectiveDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                
                //throw;
            }

            try
            {
                if (aDataTable.Rows[0]["IsFacilityIncluded"].ToString() != "")
                {
                    FacilityRadioButtonList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsFacilityIncluded"].ToString());

                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            try
            {
                if (aDataTable.Rows[0]["IsNoFacility"].ToString() != "")
                {
                    FacilityRadioButtonList.Items[1].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsNoFacility"].ToString());
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            RemarksTextBox.Text = aDataTable.Rows[0]["Remarks"].ToString();

        }

    }

    private void LoadDropDownList()
    {
        aContractualEmpManageDAL.LoadCompanyDropDownList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        //using (DataTable dt222 = _commonDataLoad.GetEmpDDLForWithoutCompany())
        //{



        //    ddlEmpInfo.DataSource = dt222;
        //    ddlEmpInfo.DataValueField = "EmpInfoId";
        //    ddlEmpInfo.DataTextField = "EmpName";
        //    ddlEmpInfo.DataBind();
        //    ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        //    ddlEmpInfo.SelectedIndex = 0;
        //}
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
        divRedesignation.Visible = false;
        chkReappointment.Checked = false;
        chkRedesignation.Checked = false;
        if (ExtentionRenewRadioButtonList.Items[0].Selected == true)
        {
            ExtensionPanelView.Visible = true;
            divReappointment.Visible = true;
        }

        if (ExtentionRenewRadioButtonList.Items[1].Selected == true)
        {
            RenewPanelView.Visible = true;
            divReappointment.Visible = true;
        }

        if (ExtentionRenewRadioButtonList.Items[2].Selected == true)
        {
            PermanentToContractualPanelView.Visible = true;
            divReappointment.Visible = true;

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



            divReappointment.Visible = true;


        }

        txtContractualPreiod.Text = "0";
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ContractualEmpManagementView.aspx");
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
                int mMonth = 0;
                try
                {
                    mMonth = Convert.ToInt32(txtContractualPreiod.Text);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                if (mMonth <= 36)
                {
                    
                } else
                {
                    showMessageBox("The contract period cannot exceed 36 months!!");
                    ExtensionToDateTextBox.Text = "";
                    txtContractualPreiod.Text = "0";
                }
            }
        }
        catch (Exception)
        {

            //throw;
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


                int mMonth = 0;
                try
                {
                    mMonth = Convert.ToInt32(txtContractualPreiod.Text);
                }
                catch (Exception)
                {

                    //throw;
                }
                if (mMonth <= 36)
                {

                }
                else
                {
                    showMessageBox("The contract period cannot exceed 36 months!!");
                    RenewToDateTextBox.Text = "";
                    txtContractualPreiod.Text = "0";
                }
            }
        }
        catch (Exception)
        {

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

    protected void EffectiveDaeTextBox_TextChanged(object sender, EventArgs e)
    {

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


                int mMonth = 0;
                try
                {
                    mMonth = Convert.ToInt32(txtContractualPreiod.Text);
                }
                catch (Exception)
                {

                    //throw;
                }
                if (mMonth <= 36)
                {

                }
                else
                {
                    showMessageBox("The contract period cannot exceed 36 months!!");
                    PermanentToContractualEndDateTextBox.Text = "";
                    txtContractualPreiod.Text = "0";
                }
            }
        }
        catch (Exception)
        {

            //throw;
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

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (ContractualEmpManageIdHiddenField.Value == string.Empty)
        {
            Save();
        }
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

          if (ddlEmpInfo.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select an Employee!!!", this);
            ddlEmpInfo.Focus();
            return false;
        }
    

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

            if (RenewReturn() == true)
            {
                if (chkIsSeparation.Checked)
                {
                    if (JobLeftTypeDropDownList.SelectedValue == "")
                    {
                        MPBehavioral.Show();
                        aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
                        JobLeftTypeDropDownList.Focus();
                        return false;
                    }

                    if (JobLeftDateTextBox.Text == String.Empty)
                    {
                        MPBehavioral.Show();
                        aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
                        JobLeftDateTextBox.Focus();
                        return false;
                    }

                }
                if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
                {
                    mp1.Show();
                    return false;
                }

                if (RemarksTextBox.Text == "")
                {
                    aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                    RemarksTextBox.Focus();
                    return false;
                }
            }


            return true;

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


            if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
            {
                mp1.Show();
                return false;
            }

            if (RemarksTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                RemarksTextBox.Focus();
                return false;
            }




            return true;
        }


        if (ExtentionRenewRadioButtonList.Items[3].Selected == true)
        {



            if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
            {
                mp1.Show();
                return false;
            }

            if (RemarksTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                RemarksTextBox.Focus();
                return false;
            }



            return true;
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

            if (chkIsSeparation.Checked)
            {
                if (JobLeftTypeDropDownList.SelectedValue == "")
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
                    JobLeftTypeDropDownList.Focus();
                    return false;
                }

                if (JobLeftDateTextBox.Text == String.Empty)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
                    JobLeftDateTextBox.Focus();
                    return false;
                }

            }
            if (chkReappointment.Checked)
            {



                if (ddlCompany.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Company", this);
                    ddlCompany.Focus();

                    return false;
                }
                if (ddlDivision.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Division", this);
                    ddlDivision.Focus();
                    return false;
                }
                if (ddlDepartment.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Department", this);
                    ddlDepartment.Focus();
                    return false;
                }

                if (ddlEmpCategory.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Employee Category", this);
                    ddlEmpCategory.Focus();
                    return false;
                }

                if (ddlSalaryGrade.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Salary Grade", this);
                    ddlSalaryGrade.Focus();
                    return false;
                }
                if (ddlSalaryStep.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Salary Step", this);
                    ddlSalaryStep.Focus();
                    return false;
                }

                if (ddlDesignation.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Designation", this);
                    ddlDesignation.Focus();
                    return false;
                }


                if (ddlSalaryLocation.SelectedIndex <= 0)
                {
                    MPBehavioral.Show();
                    aShowMessage.ShowMessageBox("Please Select Office", this);
                    ddlSalaryLocation.Focus();
                    return false;
                }



            }




            if (isAceptTerm.Items[0].Selected == false || isAceptTerm.Items[1].Selected)
            {
                mp1.Show();
                return false;
            }

            if (RemarksTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Input This Field !!!", this);
                RemarksTextBox.Focus();
                return false;
            }
        }

        if (chkIsSeparation.Checked)
        {
            if (JobLeftTypeDropDownList.SelectedValue == "")
            {
                MPBehavioral.Show();
                aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
                JobLeftTypeDropDownList.Focus();
                return false;
            }

            if (JobLeftDateTextBox.Text == String.Empty)
            {
                MPBehavioral.Show();
                aShowMessage.ShowMessageBox("Please Select Separation Date !!!", this);
                JobLeftDateTextBox.Focus();
                return false;
            }

            if (gv_NewSupp.Rows.Count > 0)
            {
                for (int i = 0; i < gv_NewSupp.Rows.Count; i++)
                {
                    DropDownList ddlEmpInfoList = (DropDownList)gv_NewSupp.Rows[i].Cells[0].FindControl("ddlEmpInfoList");

                    if (ddlEmpInfoList.SelectedValue == "")
                    {
                        aShowMessage.ShowMessageBox("Directly Supervised Employee List must be Select !!!", this);
                        MPBehavioral.Show();
                        ddlEmpInfoList.Focus();

                        return false;
                    }
                }
            }

        }

        try
        {

            for (int i = 0; i < ExtentionRenewRadioButtonList.Items.Count; i++)
            {
                if (ExtentionRenewRadioButtonList.Items[i].Selected)
                {
                    string str = ExtentionRenewRadioButtonList.Items[i].Text.Trim();

                    if (str == "Renew")
                    {

                        EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();

                        DataTable dtCEndDate = _empdal.GetContractEndDate(Convert.ToInt64(ddlEmpInfo.SelectedValue));
                        if (Convert.ToDateTime(dtCEndDate.Rows[0]["ContractEndDate"].ToString()).AddDays(1) <
                            Convert.ToDateTime(RenewStartDateTextBox.Text.Trim()))
                        {
                            AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();


                            DataTable dtSum = _aFincDalrr.GetApprovalDependencySum(ddlEmpInfo.SelectedValue.ToString());

                            int iii = 0;

                            try
                            {
                                iii = Convert.ToInt32(dtSum.Rows[0]["EmpCount"].ToString());
                            }
                            catch (Exception)
                            {

                                //throw;
                            }

                            if (iii != 0)
                            {
                                aShowMessage.ShowMessageBox("Please check all  Dependencies !!!", this);
                                ddlEmpInfo.Focus();
                                return false;
                            }
                        }
                    }


                    if (str == "Permanent to Contractual")
                    {
                        AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();


                        DataTable dtSum = _aFincDalrr.GetApprovalDependencySum(ddlEmpInfo.SelectedValue.ToString());

                        int iii = 0;

                        try
                        {
                            iii = Convert.ToInt32(dtSum.Rows[0]["EmpCount"].ToString());
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        if (iii != 0)
                        {
                            aShowMessage.ShowMessageBox("Please check all  Dependencies !!!", this);
                            ddlEmpInfo.Focus();
                            return false;
                        }

                    }

                    if (str == "Contractual to Permanent")
                    {
                        AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();


                        DataTable dtSum = _aFincDalrr.GetApprovalDependencySum(ddlEmpInfo.SelectedValue.ToString());

                        int iii = 0;

                        try
                        {
                            iii = Convert.ToInt32(dtSum.Rows[0]["EmpCount"].ToString());
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        if (iii != 0)
                        {
                            aShowMessage.ShowMessageBox("Please check all  Dependencies !!!", this);
                            ddlEmpInfo.Focus();
                            return false;
                        }

                    }


                    if (str == "Project Change")
                    {
                        AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();


                        DataTable dtSum = _aFincDalrr.GetApprovalDependencySum(ddlEmpInfo.SelectedValue.ToString());

                        int iii = 0;

                        try
                        {
                            iii = Convert.ToInt32(dtSum.Rows[0]["EmpCount"].ToString());
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        if (iii != 0)
                        {
                            aShowMessage.ShowMessageBox("Please check all  Dependencies !!!", this);
                            ddlEmpInfo.Focus();
                            return false;
                        }

                    }
                }
            }

        }
        catch { }

        return true;
    }


    public bool RenewReturn()
    {
        Int32 empGenId = 0;

        empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
        EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
        DataTable dtCEndDate = _empdal.GetContractEndDate(empGenId);



        try
        {

            if (Convert.ToDateTime(dtCEndDate.Rows[0]["ContractEndDate"].ToString()).AddDays(1) <
                                  Convert.ToDateTime(RenewStartDateTextBox.Text.Trim()))
            {
                return true;
            }
        }
        catch (Exception)
        {

            return false;
        }

        return false;
    }

    public void Update()
    {
         if (Validation())
        {
        DataTable aTable =
                        aContractualEmpManageDAL.editValidattionForEffectiveDate(
                              ddlEmpInfo.SelectedValue, txtEffectiveDate.Text, ContractualEmpManageIdHiddenField.Value);

            if (aTable.Rows.Count > 0)
            {
                aShowMessage.ShowMessageBox("Data Can not be Inserted", this);
            }
            else
            {
                ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();
                aContractualEmpManageDAO.ContractualEmpManageId = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                aContractualEmpManageDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
              
                    aContractualEmpManageDAO.EmployeeId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
              
                aContractualEmpManageDAO.ContractEndDate = string.IsNullOrEmpty(ContactualEndDateHiddenField.Value)
                    ? (DateTime?) null
                    : DateTime.Parse(ContactualEndDateHiddenField.Value).Date;
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
                        aContractualEmpManageDAO.AutoProcess = manualUpdateCheckBox.Checked;
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
                aContractualEmpManageDAO.Remarks = Convert.ToString(RemarksTextBox.Text.Trim());
                aContractualEmpManageDAO.EntryBy = Session["UserId"].ToString();
                aContractualEmpManageDAO.EntryDate = DateTime.Now;
                aContractualEmpManageDAO.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text);

                if (HFDesgId.Value != "")
                {
                    aContractualEmpManageDAO.DesignationId =
                        Convert.ToInt32(HFDesgId.Value) > 0 ? int.Parse(HFDesgId.Value) : (int?) null;
                }

                if (HFDivID.Value != "")
                {
                    aContractualEmpManageDAO.DivisionId =
                        Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?) null;
                }

                if (HFDivWingId.Value != "")
                {
                    aContractualEmpManageDAO.DivisionWId =
                        Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?) null;
                }
                if (HFDivWingId.Value != "")
                {
                    aContractualEmpManageDAO.DivisionWId =
                        Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?) null;
                }

                if (HFDeptID.Value != "")
                {
                    aContractualEmpManageDAO.DepartmentId =
                        Convert.ToInt32(HFDeptID.Value) > 0 ? int.Parse(HFDeptID.Value) : (int?) null;
                }
                if (HFSecID.Value != "")
                {
                    aContractualEmpManageDAO.SectionId =
                        Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?) null;
                }

                if (HFSubSecID.Value != "")
                {
                    aContractualEmpManageDAO.SubSectionId =
                        Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?) null;
                }

                if (HFEmpCode.Value != "")
                {
                    aContractualEmpManageDAO.EmployeeCode =
                        Convert.ToInt32(HFEmpCode.Value) > 0 ? int.Parse(HFEmpCode.Value) : (int?) null;
                }


                if (HFEmpTypeID.Value != "")
                {
                    aContractualEmpManageDAO.EmpTypeId =
                        Convert.ToInt32(HFEmpTypeID.Value) > 0 ? int.Parse(HFEmpTypeID.Value) : (int?) null;
                }


                if (HFSalLocID.Value != "")
                {
                    aContractualEmpManageDAO.SalaryLoationId = Convert.ToInt32(HFSalLocID.Value) > 0
                        ? int.Parse(HFSalLocID.Value)
                        : (int?) null;
                }

                if (HFJobLocID.Value != "")
                {

                    aContractualEmpManageDAO.JobLocationId = Convert.ToInt32(HFJobLocID.Value) > 0
                        ? int.Parse(HFJobLocID.Value)
                        : (int?) null;
                }

                if (HFSubSecID.Value != "")
                {

                    aContractualEmpManageDAO.SubSectionId = Convert.ToInt32(HFSubSecID.Value) > 0
                        ? int.Parse(HFSubSecID.Value)
                        : (int?) null;
                }

                if (SGradeFF.Value != "")
                {
                    aContractualEmpManageDAO.SalaryGradeId = Convert.ToInt32(SGradeFF.Value) > 0
                        ? int.Parse(SGradeFF.Value)
                        : (int?) null;
                }

                if (SStepHF.Value != "")
                {
                    aContractualEmpManageDAO.SalaryStepId = Convert.ToInt32(SStepHF.Value) > 0
                        ? int.Parse(SStepHF.Value)
                        : (int?) null;
                }
                aContractualEmpManageDAO.isReappointment = chkReappointment.Checked;
                aContractualEmpManageDAO.IsRedesignation = chkRedesignation.Checked;
                aContractualEmpManageDAO.TypeOfPromotion = null;
                if (ExtentionRenewRadioButtonList.Items[2].Selected == true)
                {
                    if (chkReappointment.Checked)
                    {
                        aContractualEmpManageDAO.TypeOfPromotion = "Joining";
                    }

                }

                if (ExtentionRenewRadioButtonList.Items[3].Selected == true)
                {
                    if (chkReappointment.Checked)
                    {
                        aContractualEmpManageDAO.TypeOfPromotion = "Joining";
                    }
                }

                if (ExtentionRenewRadioButtonList.Items[6].Selected == true)
                {
                    if (chkReappointment.Checked)
                    {
                        aContractualEmpManageDAO.TypeOfPromotion = "Reappointment";

                    }
                }

                if (aContractualEmpManageDAL.ContractualEmpManageUpdateInfo(aContractualEmpManageDAO))
                {
                    try
                    {
                        aContractualEmpManageDAL.UpdateSelfApprove(
                            Convert.ToInt32(ContractualEmpManageIdHiddenField.Value), false);
                        if (manualUpdateCheckBox.Checked == false)
                        {


                            if (Session["EmpInfoid"].ToString() != "")
                            {
                                ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
                                aMaster.ContractualEmpManageId
                                    = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                                aMaster.ActionStatus = "Verified";
                                bool status = aContractualEmpManageDAL.UpdateContractural(aMaster);


                                int commentid = aContractualEmpManageDAL.SaveComment("0",
                                    Session["EmpInfoId"].ToString(), "");


                                ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();

                                appLogDao.ActionStatus = "Drafted";
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                                appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                appLogDao.ContractualEmpManageId =
                                    Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
                                appLogDao.Comments = "";
                                appLogDao.CommentsId = commentid;
                                int idd = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);


                                DataTable dtempdata =
                                    aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" +
                                                                        Session["EmpInfoid"].ToString() + "'");
                                ContractualEmpManageAppLogDAO appLogDaoa = new ContractualEmpManageAppLogDAO();
                                {
                                    appLogDaoa.ActionStatus = "Verified";
                                    appLogDaoa.ApproveDate = DateTime.Now;
                                    appLogDaoa.ApproveBy = Session["UserId"].ToString();
                                    appLogDaoa.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                    appLogDaoa.ForEmpInfoId =
                                        Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                    appLogDaoa.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                                    appLogDaoa.Comments = "";
                                    appLogDaoa.CommentsId = commentid;

                                }
                                ;
                                int ida = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDaoa);
                                aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);

                                SenMailForApprved(appLogDao.ForEmpInfoId, " Contractual Employee Form Approval ",
                                    @"  <br/> Dear Sir, <br/>
A Contractual Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/ <br/>
Thank you.
");
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Data Updated Successfull...');window.location ='ContractualEmpManagementView.aspx';",
                                    true);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }
    }

    public void Save()
    {
        if (Validation())
        {
        DataTable aTable =
                        aContractualEmpManageDAL.ValidattionForEffectiveDate(
                              ddlEmpInfo.SelectedValue, txtEffectiveDate.Text);

        if (aTable.Rows.Count > 0)
        {
            aShowMessage.ShowMessageBox("Data Can not be Inserted", this);
        }
        else
        {
            ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();

            aContractualEmpManageDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                aContractualEmpManageDAO.EmployeeId = Convert.ToInt32(Request.QueryString["ID"]);

            }
            else
            {
                aContractualEmpManageDAO.EmployeeId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
            }
            aContractualEmpManageDAO.ContractEndDate = string.IsNullOrEmpty(ContactualEndDateHiddenField.Value) ? (DateTime?)null : DateTime.Parse(ContactualEndDateHiddenField.Value).Date;
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
                    aContractualEmpManageDAO.AutoProcess = manualUpdateCheckBox.Checked;
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
            aContractualEmpManageDAO.Remarks = Convert.ToString(RemarksTextBox.Text.Trim());
            aContractualEmpManageDAO.EntryBy = Session["UserId"].ToString();
            aContractualEmpManageDAO.EntryDate = DateTime.Now;
            aContractualEmpManageDAO.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text);

            if (HFDesgId.Value != "")
            {
                aContractualEmpManageDAO.DesignationId =
                    Convert.ToInt32(HFDesgId.Value) > 0 ? int.Parse(HFDesgId.Value) : (int?)null;
            }

            if (HFDivID.Value != "")
            {
                aContractualEmpManageDAO.DivisionId =
                    Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
            }

            if (HFDivWingId.Value != "")
            {
                aContractualEmpManageDAO.DivisionWId =
       Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
            }
            if (HFDivWingId.Value != "")
            {
                aContractualEmpManageDAO.DivisionWId =
       Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
            }

            if (HFDeptID.Value != "")
            {
                aContractualEmpManageDAO.DepartmentId =
                    Convert.ToInt32(HFDeptID.Value) > 0 ? int.Parse(HFDeptID.Value) : (int?)null;
            }
            if (HFSecID.Value != "")
            {
                aContractualEmpManageDAO.SectionId =
                    Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
            }

            if (HFSubSecID.Value != "")
            {
                aContractualEmpManageDAO.SubSectionId =
                    Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
            }

            if (HFEmpCode.Value != "")
            {
                aContractualEmpManageDAO.EmployeeCode =
            Convert.ToInt32(HFEmpCode.Value) > 0 ? int.Parse(HFEmpCode.Value) : (int?)null;
            }


            if (HFEmpTypeID.Value != "")
            {
                aContractualEmpManageDAO.EmpTypeId =
                    Convert.ToInt32(HFEmpTypeID.Value) > 0 ? int.Parse(HFEmpTypeID.Value) : (int?)null;
            }


            if (HFSalLocID.Value != "")
            {
                aContractualEmpManageDAO.SalaryLoationId = Convert.ToInt32(HFSalLocID.Value) > 0
                    ? int.Parse(HFSalLocID.Value)
                    : (int?)null;
            }

            if (HFJobLocID.Value != "")
            {

                aContractualEmpManageDAO.JobLocationId = Convert.ToInt32(HFJobLocID.Value) > 0
                    ? int.Parse(HFJobLocID.Value)
                    : (int?)null;
            }

            if (HFSubSecID.Value != "")
            {

                aContractualEmpManageDAO.SubSectionId = Convert.ToInt32(HFSubSecID.Value) > 0
                    ? int.Parse(HFSubSecID.Value)
                    : (int?)null;
            }

            if (SGradeFF.Value != "")
            {
                aContractualEmpManageDAO.SalaryGradeId = Convert.ToInt32(SGradeFF.Value) > 0
                    ? int.Parse(SGradeFF.Value)
                    : (int?)null;
            }

            if (SStepHF.Value != "")
            {
                aContractualEmpManageDAO.SalaryStepId = Convert.ToInt32(SStepHF.Value) > 0
                    ? int.Parse(SStepHF.Value)
                    : (int?)null;
            }
            aContractualEmpManageDAO.isReappointment = chkReappointment.Checked;
            aContractualEmpManageDAO.IsRedesignation = chkRedesignation.Checked;
            aContractualEmpManageDAO.TypeOfPromotion = null;
            if (ExtentionRenewRadioButtonList.Items[2].Selected == true)
            {
                if (chkReappointment.Checked)
                {
                    aContractualEmpManageDAO.TypeOfPromotion = "Joining";
                }

            }

            if (ExtentionRenewRadioButtonList.Items[3].Selected == true)
            {
                if (chkReappointment.Checked)
                {
                    aContractualEmpManageDAO.TypeOfPromotion = "Joining";
                }
            }

            if (ExtentionRenewRadioButtonList.Items[6].Selected == true)
            {
                if (chkReappointment.Checked)
                {
                    aContractualEmpManageDAO.TypeOfPromotion = "Reappointment";

                }
            }

            int id = aContractualEmpManageDAL.ExtensionSaveInfo(aContractualEmpManageDAO);


            if (chkIsSeparation.Checked)
            {
                EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

                aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(ddlEmpInfo.SelectedValue);

                aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(JobLeftTypeDropDownList.SelectedValue);

                // atblEmployeePromotionEntryDAO.PSetpId = Convert.ToInt32(PreviousStepDropDownList.SelectedValue);
                // atblEmployeePromotionEntryDAO.PRepEmpId = Convert.ToInt32(PReportingBodyDropDownList.SelectedValue);


                aEmployeeJobLeftEntryDAO.IsClearanceForm = false;



                aEmployeeJobLeftEntryDAO.IsExitInterview = false;


                aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
                aEmployeeJobLeftEntryDAO.Reason = "";


                aEmployeeJobLeftEntryDAO.SubmissionDate = null;





                aEmployeeJobLeftEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
                aEmployeeJobLeftEntryDAO.EntryDate = DateTime.Now;

                int id22 = aEmployeeJobLeftEntryDAL.EmployeePromotionEntrySaveInfo(aEmployeeJobLeftEntryDAO);


                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    DropDownList ddlEmpInfoList = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlEmpInfoList");


                    UpDateSuperVisorInfo(
                       Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()), (Convert.ToInt32(ddlEmpInfoList.SelectedValue)));


                }
            }

            if (id > 0)
            {
                try
                {
                    aContractualEmpManageDAL.UpdateSelfApprove(id, false);
                    if (manualUpdateCheckBox.Checked == false)
                    {



                        Int32 empGenId = 0;
                        Int32 EmpTypeId = 0;
                        string ExtensionToDate = "";
                        string RenewStartDate = "";

                        empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);





                        int? CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        int? DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                        int? DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                        int? DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                        int? SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                        int? SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                        int? EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                        int? SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                        int? SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                        int? DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                        int? DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                        //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                        int? JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                        int? SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;

                        int? JobLeftTypeId = JobLeftTypeDropDownList.SelectedIndex > 0 ? int.Parse(JobLeftTypeDropDownList.SelectedValue) : (int?)null;
                        string Floor = txtFloor.Text;

                        string jobleftDate = null;
                        try
                        {
                            jobleftDate = (JobLeftDateTextBox.Text);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }



                        if (chkReappointment.Checked == true)
                        {
                            aContractualEmpManageDAL.InsertNewContractEmpChange(id, empGenId.ToString(), CompanyId, DivisionId, DivisionWId, DepartmentId,
                                SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId,
                                DesignationTypeId, JobLocationId, SalaryLoationId, Floor, chkIsSeparation.Checked, JobLeftTypeId, jobleftDate);
                        }

                        if (Session["EmpInfoid"].ToString() != "")
                        {
                            ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
                            aMaster.ContractualEmpManageId
                                = Convert.ToInt32(id);
                            aMaster.ActionStatus = "Verified";
                            bool status = aContractualEmpManageDAL.UpdateContractural(aMaster);


                            int commentid = aContractualEmpManageDAL.SaveComment("0",
                                Session["EmpInfoId"].ToString(), "");


                            ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();

                            appLogDao.ActionStatus = "Drafted";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                            appLogDao.ContractualEmpManageId = id;
                            appLogDao.Comments = "";
                            appLogDao.CommentsId = commentid;
                            int idd = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);


                            DataTable dtempdata =
                                aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" +
                                                                    Session["EmpInfoid"].ToString() + "'");
                            ContractualEmpManageAppLogDAO appLogDaoa = new ContractualEmpManageAppLogDAO();
                            {
                                appLogDaoa.ActionStatus = "Verified";
                                appLogDaoa.ApproveDate = DateTime.Now;
                                appLogDaoa.ApproveBy = Session["UserId"].ToString();
                                appLogDaoa.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                appLogDaoa.ForEmpInfoId =
                                    Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                appLogDaoa.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                                appLogDaoa.Comments = "";
                                appLogDaoa.CommentsId = commentid;

                            }
                            ;
                            int ida = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDaoa);
                            aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
                            SenMailForApprved(appLogDao.ForEmpInfoId, " Contractual Employee Form Approval ", @"  <br/> Dear Sir, <br/>
A Contractual Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/>  http://182.160.103.234:8088/ <br/> Thank You.
");
                        }
                    }
                }
                catch (Exception)
                {

                    //throw;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='ContractualEmpManagementView.aspx';",
                   true);

            }




        }


         }
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

            if (manualUpdateCheckBox.Checked)
            {
                Int32 empGenId = 0;

                string ExtensionStartDate = "";
                string ExtensionToDate = "";
                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                ExtensionStartDate = Convert.ToDateTime(ExtensionFromDateTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(ExtensionToDateTextBox.Text.Trim()).ToString();
                UpdateEmployeeContractualDeate(empGenId, ExtensionStartDate, ExtensionToDate, ContractPreiod);

                //Up_lift_Save();

            }
        }
    }


    //Tareq_St

    private void Up_lift_Save()
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();
        aInfo.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
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

        aContractualEmpManageDAL.Save_Uplift(aInfo);
    }

    //Tareq_End

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


            if (manualUpdateCheckBox.Checked)
            {
                Int32 empGenId = 0;
                string RenewStartDate = "";
                string RenewToDate = "";


                int? EmpTypeId = Convert.ToInt32(HFEmpTypeID.Value);

                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);

                RenewStartDate = Convert.ToDateTime(RenewStartDateTextBox.Text.Trim()).ToString();
                RenewToDate = Convert.ToDateTime(RenewToDateTextBox.Text.Trim()).ToString();


                EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                DataTable dtCEndDate = _empdal.GetContractEndDate(empGenId);
                //DataTable dtEmpCode = _empdal.GetEmpMasterCode(empGenId);
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;

              //  Up_lift_Save();

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
                                SPTransferInsert(hhhh);

                              
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
    }

    private void SPTransferInsert(tblEmpGeneralInfo hhhh)
    {
        try
        {
            DataTable dtSpCheck = aContractualEmpManageDAL.GetSpTransferCheckByEmpId(ddlEmpInfo.SelectedValue);

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
                    Convert.ToInt32(ddlEmpInfo.SelectedValue));
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
                            if (smc==1)
                            {
                                aEmpTransferAndRedesignationDal.InsertMappinigEmpRefferId(item, hhhh.EmpInfoId.ToString(), 1);
                            }

                            if (smcEL==1)
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
                    OldEmployeeId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                    NewEmployeeId = Convert.ToInt32(hhhh.EmpInfoId);

                    NewComId = Convert.ToInt32(companyDropDownList.SelectedValue);
                }
                catch (Exception)
                {

                    //throw;
                }
                aEmpTransferAndRedesignationDal.WithoutEmpSpecialTransfer(OldEmployeeId, NewEmployeeId, NewComId);
            }

        }
        catch (Exception ex)
        {
            //throw;
        }
    }


    EmployeeProfileDAL ddd = new EmployeeProfileDAL();

    SupervisorMenuAppDAL ddddd = new SupervisorMenuAppDAL();

    protected void chkIsB_ReportingBody_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox dropDown = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        CheckBox chkIsB_ReportingBody = (CheckBox)gv_NewSupp.Rows[rowindex].Cells[0].FindControl("chkIsB_ReportingBody");
        DropDownList ddlEmpInfoList = (DropDownList)gv_NewSupp.Rows[rowindex].Cells[0].FindControl("ddlEmpInfoList");
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

            if (manualUpdateCheckBox.Checked)
            {
                Int32 empGenId = 0;
                Int32 EmpTypeId = 0;
                string ExtensionToDate = "";
                string RenewStartDate = "";

                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                EmpTypeId = 2;

                RenewStartDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;


                int? CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                int? DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                int? DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                int? DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                int? SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                int? SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                int? EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                int? SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                int? SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                int? DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                int? DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                int? JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                int? SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                string Floor = txtFloor.Text;



                if (chkReappointment.Checked)
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
                        SPTransferInsert(hhhh);
                    }
                }
            }





        }
    }


    private void ToProjects(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "Project Change")
        {

            if (ddlProject.SelectedValue != "0")
            {
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;

                if (ddlProject.SelectedValue == "1")
                {
                    IsSMCFundedProjects = true;
                }
                else if (ddlProject.SelectedValue == "2")
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

                aContractualEmpManageDAO.FromProject = hfPreviousPreoject.Value;
                aContractualEmpManageDAO.ToProject = ddlProject.SelectedItem.Text;

                if (manualUpdateCheckBox.Checked)
                {
                    Int32 empGenId = 0;
                    Int32 EmpTypeId = 0;
                    string ExtensionToDate = "";
                    string RenewStartDate = "";

                    empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                    EmpTypeId = 2;

                    RenewStartDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim()).ToString();
                    ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();
                    Int32 ContractPreiod = 0;
                    ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);


                    int? CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                    int? DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                    int? DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                    int? DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                    int? SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                    int? SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                    int? EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                    int? SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                    int? SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                    int? DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                    int? DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                    //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                    int? JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                    int? SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                    string Floor = txtFloor.Text;



                    if (chkReappointment.Checked)
                    {
                        aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpIDReappointment(str, IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(), string.IsNullOrEmpty(txtEffectiveDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                             ? (DateTime?)null
                                             : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                            ? (DateTime?)null
                                            : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId, Convert.ToInt32(Session["UserId"]), CompanyId, DivisionId, DivisionWId, DepartmentId, SectionId, SubSectionId, EmpCategoryId, SalaryGradeId, SalaryStepId, DesignationId, DesignationTypeId, JobLocationId, SalaryLoationId, Floor);
                    }
                    else
                    {
                        aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(), string.IsNullOrEmpty(txtEffectiveDate.Text)
                                             ? (DateTime?)null
                                             : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                              ? (DateTime?)null
                                              : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                             ? (DateTime?)null
                                             : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId, Convert.ToInt32(Session["UserId"]));
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
                            using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForIsSMCFundedProjects(hhhh.EmpInfoId))
                            {
                                if (dtEmpCode.Rows.Count > 0)
                                {
                                    SPTransferInsert(hhhh);
                                }
                            }
                        }
                        else
                        {
                            using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(hhhh.EmpInfoId))
                            {
                                if (dtEmpCode.Rows.Count > 0)
                                {
                                    SPTransferInsert(hhhh);
                                }
                            }
                        }
                    }
                }



            }

        }
    }

    private void UpdateEmployeeStepId(int empGenId, int desigId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();


        aInfo.DesigId = desigId;

        aInfo.EmpInfoId = empGenId;

        atblEmployeePromotionEntryDAL.UpdateEmployeeExitInfo(aInfo);

    }
    private void SMCFundedProjects_to_SMCContract(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "SMC Funded Projects to SMC Contract")
        {


            aContractualEmpManageDAO.IsPermanentToContractual = false;

            aContractualEmpManageDAO.IsExtension = false;
            aContractualEmpManageDAO.IsRenew = false;
            aContractualEmpManageDAO.IsContractualToPermanent = false;

            aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = true;
            aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;


            aContractualEmpManageDAO.PermanentToContractualEffectiveDate =
                Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim());
            aContractualEmpManageDAO.PermanentToContractualEndDate =
                Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim());

            if (manualUpdateCheckBox.Checked)
            {
                Int32 empGenId = 0;
                Int32 EmpTypeId = 0;
                string ExtensionToDate = "";
                string RenewStartDate = "";

                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                EmpTypeId = 2;

                RenewStartDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;

                aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(), string.IsNullOrEmpty(txtEffectiveDate.Text)
                                          ? (DateTime?)null
                                          : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                           ? (DateTime?)null
                                           : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                          ? (DateTime?)null
                                          : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId, Convert.ToInt32(Session["UserId"]));

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
                            _empdal.GetEmpMasterCodeForIsSMCFundedProjects(hhhh.EmpInfoId))
                    {
                        SPTransferInsert(hhhh);
                    }
                }
            }





        }
    }


    private void SMCContract_to_SMCFundedProjects(string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    {
        if (str == "SMC Contract to SMC Funded Projects")
        {


            aContractualEmpManageDAO.IsPermanentToContractual = false;

            aContractualEmpManageDAO.IsExtension = false;
            aContractualEmpManageDAO.IsRenew = false;
            aContractualEmpManageDAO.IsContractualToPermanent = false;

            aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
            aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = true;


            aContractualEmpManageDAO.PermanentToContractualEffectiveDate =
                Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim());
            aContractualEmpManageDAO.PermanentToContractualEndDate =
                Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim());

            if (manualUpdateCheckBox.Checked)
            {
                Int32 empGenId = 0;
                Int32 EmpTypeId = 0;
                string ExtensionToDate = "";
                string RenewStartDate = "";

                empGenId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
                EmpTypeId = 2;

                RenewStartDate = Convert.ToDateTime(PermanentToContractualEffectiveDaeTextBox.Text.Trim()).ToString();
                ExtensionToDate = Convert.ToDateTime(PermanentToContractualEndDateTextBox.Text.Trim()).ToString();
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);
                bool IsSMCFundedProjects = false;
                bool IsProgramContractual = false;
                aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, IsSMCFundedProjects, IsProgramContractual, aContractualEmpManageDAO.EmployeeId.ToString(), string.IsNullOrEmpty(txtEffectiveDate.Text)
                                          ? (DateTime?)null
                                          : DateTime.Parse(txtEffectiveDate.Text).Date, string.IsNullOrEmpty(RenewStartDate)
                                           ? (DateTime?)null
                                           : DateTime.Parse(RenewStartDate).Date, string.IsNullOrEmpty(ExtensionToDate)
                                          ? (DateTime?)null
                                          : DateTime.Parse(ExtensionToDate).Date, ContractPreiod, EmpTypeId, Convert.ToInt32(Session["UserId"]));

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
                            _empdal.GetEmpMasterCodeForIsSMCFundedProjects(hhhh.EmpInfoId))
                    {
                        SPTransferInsert(hhhh);
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
            if (manualUpdateCheckBox.Checked)
            {
                int EmpTypeId = 1;

                string ExtensionToDate = "";
                string RenewStartDate = "";
                Int32 ContractPreiod = 0;
                ContractPreiod = Convert.ToInt32(txtContractualPreiod.Text);


                int? CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                int? DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                int? DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                int? DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                int? SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                int? SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                int? EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                int? SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                int? SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                int? DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                int? DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                int? JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                int? SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                string Floor = txtFloor.Text;



                if (chkReappointment.Checked)
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
                        SPTransferInsert(hhhh);
                    }
                }



            }
        }
    }


    //private void  SMCFundedProjects_to_SMCContract (string str, ContractualEmpManageDAO aContractualEmpManageDAO)
    //{


    //    if (str == "SMC Funded Projects to SMC Contract")
    //    {
    //        aContractualEmpManageDAO.IsContractualToPermanent = true;

    //        aContractualEmpManageDAO.IsPermanentToContractual = false;

    //        aContractualEmpManageDAO.IsExtension = false;
    //        aContractualEmpManageDAO.IsRenew = false;

    //        aContractualEmpManageDAO.IsRenew = false;
    //        aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract = false;
    //        aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects = false;



    //        if (manualUpdateCheckBox.Checked)
    //        {
    //            aContractualEmpManageDAL.InsertNewColumnInEmpGeneralTableByEmpID(str, aContractualEmpManageDAO.EmployeeId.ToString(), string.IsNullOrEmpty(txtEffectiveDate.Text)
    //                        ? (DateTime?)null
    //                        : DateTime.Parse(txtEffectiveDate.Text).Date, null, null, 0, 1, Convert.ToInt32(Session["UserId"]));



    //            using (var db = new HRIS_SMCEntities())
    //            {
    //                var hhhh = db.tblEmpGeneralInfoes.OrderByDescending(u => u.EmpInfoId).FirstOrDefault();


    //                CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    //                for (int i = 0; i < loadGridView.Rows.Count; i++)
    //                {
    //                    _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
    //                        hhhh.EmpInfoId.ToString());
    //                }

    //                EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
    //                ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
    //                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeContracttoParmanent(hhhh.EmpInfoId))
    //                {

    //                }
    //            }



    //        }
    //    }
    //}
    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //        "alert",
                //        "alert('Email has not Sent, Try Once More time...');window.location ='ContractualEmpManagementView.aspx';",
                //        true);
            }
            catch (Exception exe)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //        "alert",
                //        "alert('Email has not Sent, Try Once More time...');window.location ='ContractualEmpManagementView.aspx';",
                //        true);
            }


            System.Threading.Thread.Sleep(100);
        }



    }


    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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

    private void UpdateEmployePermanenttoContractualInfoEmpTypeID(int empGenId, string ExtensionToDate, int EmpTypeId, int ContractPreiod)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractEndDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;
        aInfo.EmpTypeId = EmpTypeId;
        aInfo.ContractPeriod = ContractPreiod;


        aContractualEmpManageDAL.UpdateEmployePermanenttoContractualInfoEmpTypeID(aInfo);

    }

    private void UpdateEmployeEmpTypeID(int empGenId, int EmpType)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();



        aInfo.EmpInfoId = empGenId;
        aInfo.EmpTypeId = EmpType;

        aContractualEmpManageDAL.UpdateEmployeEmpTypeID(aInfo);

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
        ddlEmpInfo.Items.Clear();
        if (companyDropDownList.SelectedValue != "")
        {

            

            Session["CompanyId"] = companyDropDownList.SelectedValue;
            SearchEmployeeNameTextBoxTextBox.ReadOnly = false;

            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else
        {
            SearchEmployeeNameTextBoxTextBox.ReadOnly = true;

            ddlEmpInfo.DataSource = null;

            ddlEmpInfo.DataBind();
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
                LoadProjectData(Convert.ToInt32(repEmpIdHiddenField.Value));
                LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                ShowPanel.Visible = true;

                using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(repEmpIdHiddenField.Value.ToString()))
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
            SearchEmployeeNameTextBoxTextBox.Text = "";
            aShowMessage.ShowMessageBox("Please Select A Company !!", this);
        }
    }

    private void LoadProjectData(int id)
    {




        DataTable Projectdtdata = new DataTable();
        Projectdtdata = aContractualEmpManageDAL.LoadProject(id);
        if (Projectdtdata.Rows.Count > 0)
        {
            GVExistingProject.DataSource = Projectdtdata;
            GVExistingProject.DataBind();

        }


    }

    private void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aContractualEmpManageDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {
      
            if (dtdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                
               // ExtentionRenewRadioButtonList.Visible = false;
            //    SalaryIncrementRadioButtonList1.Visible = false;
            //    FacilityRadioButtonList.Visible = false;
            //    ContractPreiod.Visible = false;
             //   manualUpdateCheckBox.Visible = false;
                evgrid.Visible = false;

            }

            if (dtdata.Rows[0]["ReportingEmpId"].ToString() == Session["EmpInfoId"].ToString())
            {
                //evgrid.Visible = false;
                //using (DataTable dt = aContractualEmpManageDAL.GetContractualEvaluationRating())
                //{
                //  gv_ProbationEvaluation.DataSource = dt;
                //  gv_ProbationEvaluation.DataBind();ContractPreiod
                //}

             //   ExtentionRenewRadioButtonList.Visible = false;
             //   SalaryIncrementRadioButtonList1.Visible = false;
             //   FacilityRadioButtonList.Visible = false;
             //   ContractPreiod.Visible = false;
             //   manualUpdateCheckBox.Visible = false;
                evgrid.Visible = false;

            }


            using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(id.ToString()))
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


            companyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            ddlCompany.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            ddlCompany_SelectedIndexChanged(null, null);
            companyDropDownList_OnSelectedIndexChanged(null, null);
            SearchEmployeeNameTextBoxTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
            lblProject.Text = dtdata.Rows[0]["ProjectName"].ToString();

            ddlEmpInfo.SelectedValue = id.ToString();
            if ((dtdata.Rows[0]["ContractEndDate"] != DBNull.Value))
            {
                ContactualEndDateHiddenField.Value = dtdata.Rows[0]["ContractEndDate"].ToString();
                lblContractEndDate.Text = string.IsNullOrEmpty(dtdata.Rows[0]["ContractEndDate"].ToString()) ? "" : Convert.ToDateTime(dtdata.Rows[0]["ContractEndDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            if ((dtdata.Rows[0]["ContractPeriod"] != DBNull.Value))
            {
                ContractPeriodHF.Value = dtdata.Rows[0]["ContractPeriod"].ToString();
            }

            lblComName.Text = dtdata.Rows[0]["CompanyName"].ToString();
            hfIsProgramContractualOP.Value = dtdata.Rows[0]["IsProgramContractual"].ToString();
            hfIsSMCFundedProjects.Value = dtdata.Rows[0]["IsSMCFundedProjects"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
            lblEmpType.Text = dtdata.Rows[0]["EmpType"].ToString();
            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = dtdata.Rows[0]["SalaryGrade"].ToString();
            lblDivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
            lblWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = dtdata.Rows[0]["SubSectionName"].ToString();

            HFDivID.Value = dtdata.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = dtdata.Rows[0]["DivisionWId"].ToString();
            HFDeptID.Value = dtdata.Rows[0]["DepartmentId"].ToString();
            HFSecID.Value = dtdata.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = dtdata.Rows[0]["SubSectionId"].ToString();

            HFEmpCode.Value = dtdata.Rows[0]["EmpMasterCode"].ToString();
            HFEmpTypeID.Value = dtdata.Rows[0]["EmpTypeId"].ToString();
            HFSalLocID.Value = dtdata.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = dtdata.Rows[0]["JobLocationId"].ToString();
            HFDesgId.Value = dtdata.Rows[0]["DesignationId"].ToString();

            hfPreviousPreoject.Value = dtdata.Rows[0]["ProjectType"].ToString();

            SGradeFF.Value = dtdata.Rows[0]["SalaryGradeId"].ToString();
            SStepHF.Value = dtdata.Rows[0]["SalaryStepId"].ToString();



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
                    NewDesignationDropDownList.SelectedValue = emp.DesignationId.ToString();
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
            DataTable aTable =
                           aContractualEmpManageDAL.DeleteValidattionForEffectiveDate(ContractualEmpManageIdHiddenField.Value.ToString());
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

        if (ContractualEmpManageIdHiddenField.Value != string.Empty)
        {

            DataTable aTable =
                          aContractualEmpManageDAL.DeleteValidattionForEffectiveDate(ContractualEmpManageIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
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


        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }

    private void Delete()
    {
        ContractualEmpManageDAO aContractualEmpManageDAO = new ContractualEmpManageDAO();

        aContractualEmpManageDAO.ContractualEmpManageId = Convert.ToInt32(ContractualEmpManageIdHiddenField.Value);
        aContractualEmpManageDAO.IsDelete = true;
        aContractualEmpManageDAO.DeleteBy = Convert.ToInt32(Session["UserId"].ToString());


        aContractualEmpManageDAO.DeleteDate = DateTime.Now;







        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractEndDate = string.IsNullOrEmpty(ContactualEndDateHiddenField.Value) ? (DateTime?)null : DateTime.Parse(ContactualEndDateHiddenField.Value).Date;
        aInfo.EmpMasterCode = HFEmpCode.Value;

        aInfo.ContractPeriod = (string.IsNullOrEmpty(ContractPeriodHF.Value) ? (int?)null : int.Parse(ContractPeriodHF.Value));
        aInfo.ContractPeriod = (string.IsNullOrEmpty(ContractPeriodHF.Value) ? (int?)null : int.Parse(ContractPeriodHF.Value));
        aInfo.EmpTypeId = (string.IsNullOrEmpty(HFEmpTypeID.Value) ? (int?)null : int.Parse(HFEmpTypeID.Value));

        aInfo.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
        // aContractualEmpManageDAL.DeleteUpdateEmployeEmpTypeID(aInfo);


        if (aContractualEmpManageDAL.DeleteInfo(aContractualEmpManageDAO))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Data Delete Successfull...');window.location ='ContractualEmpManagementView.aspx';",
                    true);
        }
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

    protected void lb_Download_OnClick(object sender, EventArgs e)
    {

    }

    protected void btnYes_OnClick(object sender, EventArgs e)
    {
        mp1.Hide();
    }

    protected void ddlEmpInfo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        gv_NewSupp.DataSource = null;
        gv_NewSupp.DataBind();
        chkIsSeparation.Checked = false;
        LoadProjectData(Convert.ToInt32(ddlEmpInfo.SelectedValue));
        LoadData(Convert.ToInt32(ddlEmpInfo.SelectedValue));
        ShowPanel.Visible = true;


        int id = Convert.ToInt32(ddlEmpInfo.SelectedValue);

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

        using (DataTable dtreporting = _commonDataLoad.GetReportingEmployee(ddlEmpInfo.SelectedValue.ToString()))
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
    protected void btnBehavioralClose_Click(object sender, EventArgs e)
    {
        MPBehavioral.Hide();
        //chkReappointment.Checked = false;
    }
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


    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        ModalPopupExtender14.Hide();


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
    protected void chkIsSeparation_OnCheckedChanged(object sender, EventArgs e)
    {

        if (chkIsSeparation.Checked)
        {

            DataTable dtadata = aEmpTransferAndRedesignationDal.LoadSuperviseEmployeeActive(ddlEmpInfo.SelectedValue.ToString());

            if (dtadata.Rows.Count > 0)
            {
                //chkIsSeparation.Checked = false;
                //showMessageBox("Please Define the reporting person!");
            }

            gv_NewSupp.DataSource = dtadata;
            gv_NewSupp.DataBind();
            for (int i = 0; i < gv_NewSupp.Rows.Count; i++)
            {
                DropDownList ddlEmpInfoList = (DropDownList)gv_NewSupp.Rows[i].Cells[0].FindControl("ddlEmpInfoList");
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

        else
        {
            gv_NewSupp.DataSource = null;
            gv_NewSupp.DataBind();
        }
        //septype.Visible = false;
        //sepDate.Visible = false;
        //if (chkIsSeparation.Checked)
        //{
        //    septype.Visible = true;
        //    sepDate.Visible = true;
        //}
    }
}