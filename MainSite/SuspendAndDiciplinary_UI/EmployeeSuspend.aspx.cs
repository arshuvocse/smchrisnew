using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.SuspendAndDiciplinary_Dal;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class SuspendAndDiciplinary_UI_EmployeeSuspend : System.Web.UI.Page
{
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            effectDateTexBox.Attributes.Add("readonly", "readonly");
            effectToDateTexBox.Attributes.Add("readonly", "readonly");
            ButtonVisible();
            if (Session["suspendId"] != null)
            {
                suspendHiddenField.Value = Session["suspendId"].ToString();
                GetOneRecord(Session["suspendId"].ToString());
                Session["suspendId"] = null;
            }
        }
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submithButton.Visible = true;
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
            Response.Redirect("EmployeeSuspendView.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Session["History"] = "Suspand";
        Response.Redirect("History.aspx");
    }
    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompanyDropDownList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        //aSuspendDal.EmployeeTypeList(typeDropDownList);
    }

    private void GetOneRecord(string suspendId)
    {

        DataTable aTable = aSuspendDal.EmpSuspendInformation(suspendId);

        const int rowIndex = 0;

        if (aTable.Rows.Count > 0)
        {
            effectDateTexBox.Text = aTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString("dd-MMM-yyyy");

            try
            {
                effectToDateTexBox.Text = aTable.Rows[rowIndex].Field<DateTime>("EffectiveToDate").ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                
                //throw;
            }

            companyDropDownList.SelectedValue = aTable.Rows[0]["CompanyInfoId"].ToString();
            

            EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
            empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();
            EmployeeDropDownList.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

            if (aTable.Rows[0]["AutoProcess"] != null)
            {
                manualUpdateCheckBox.Checked = true;
            }

            manualUpdateCheckBox.Enabled = false;

            HFDivID.Value = aTable.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = aTable.Rows[0]["DivisionWId"].ToString();

            HFSecID.Value = aTable.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = aTable.Rows[0]["SubSectionId"].ToString();

            HFSalLocID.Value = aTable.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = aTable.Rows[0]["JobLocationId"].ToString();


            deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

           

            desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

            
            //empGradeIdHiddenField.Value = aTable.Rows[0]["SalaryGradeId"].ToString().Trim();

            employeeType.Text = aTable.Rows[0]["EmpType"].ToString().Trim();
            empTypeHiddenField.Value = aTable.Rows[0]["EmpTypeId"].ToString().Trim();

            joiningDateLabel.Text = Convert.ToDateTime(aTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

            descriptionTexBox.Text = aTable.Rows[rowIndex].Field<string>("Description").ToString(CultureInfo.InvariantCulture);
            remarksTextBox.Text = aTable.Rows[rowIndex].Field<string>("Remarks").ToString(CultureInfo.InvariantCulture);
           

            if (companyDropDownList.SelectedValue != "")
            {
              
                aSuspendDal.LoadActionType(actionTypeDropDownList, companyDropDownList.SelectedValue);
                aSuspendDal.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
                actionTypeDropDownList.SelectedValue = aTable.Rows[0]["ReasonId"].ToString().Trim();
                empCodeLabel.Text = aTable.Rows[0]["EmpCode"].ToString().Trim();

                  FinancialYearDropDownList.SelectedValue = aTable.Rows[0]["FinancialYearId"].ToString().Trim();
            }
            else
            {
                actionTypeDropDownList.Items.Clear();
            }

            


            //CheckBoxList1.Items[0].Selected = Convert.ToBoolean(aTable.Rows[0]["isSuspensionLetter"].ToString().Trim());
            //CheckBoxList1.Items[1].Selected = Convert.ToBoolean(aTable.Rows[0]["isWithPay"].ToString().Trim());
            //CheckBoxList1.Items[2].Selected = Convert.ToBoolean(aTable.Rows[0]["isWithoutPay"].ToString().Trim());

            submithButton.Text = "Update";


        }
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty((Convert.))
        //{
           DataTable aTable = aSuspendDal.LoadEmpInfo(EmpInfoIdHiddenField.Value);

            if (aTable.Rows.Count > 0)
            {

                EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
                empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

                //comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
                //comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

                //divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
                //divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

                //divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
                //divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

                //secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
                //secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

                //subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
                //subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

                //empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
                //empGradeIdHiddenField.Value = aTable.Rows[0]["SalaryGradeId"].ToString().Trim();

                joiningDateLabel.Text = aTable.Rows[0]["JoiningDate"].ToString();
                
            }
            else
            {
                aShowMessage.ShowMessageBox("Employee not Active", this);
            }
        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Input Employee Code", this);
        //}
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (DataValidation())
        {
            if (suspendHiddenField.Value == "")
            {
                //DataTable dtdata = aSuspendDal.GetEmployeeSuspand(EmpInfoIdHiddenField.Value);
                //if (dtdata.Rows.Count < 1)
                //{

                 DataTable aTable =
                            aSuspendDal.ValidattionForEffectiveDate(
                                  EmpInfoIdHiddenField.Value, effectDateTexBox.Text);

                if (aTable.Rows.Count > 0)
                {
                    aShowMessage.ShowMessageBox("Data Can not be Inserted", this);
                }
                else
                {
                    try
                    {
                        Int32 suspendId = SaveSuspendInformation();

                        if (suspendId > 0)
                        {

                            //For Employee Master Information update ------------------------------------------------------------------------

                            if (manualUpdateCheckBox.Checked)
                            {

                                Int32 empGenId = 0;
                                string reason = "";

                                empGenId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
                                reason = Convert.ToString(actionTypeDropDownList.SelectedItem.Text);

                                UpdateEmployeeStepId(empGenId, reason);
                            }

                            //--------------------------------------------------------------------------------------------------------------

                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation successfully done...');window.location ='EmployeeSuspendView.aspx';",
                                true);
                        }
                    }
                    catch (Exception)
                    {
                        aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                    }
                }
                //}
                //else
                //{
                //    aShowMessage.ShowMessageBox("Employee Suspand Already Exist ",this);
                //}
            }

            
        }
    }

    private void UpdateEmployeeStepId(int empGenId, string reason)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.InactiveReason = reason;
        aInfo.EmployeeStatus = "InActive";
        aInfo.EmpInfoId = empGenId;

        aSuspendDal.UpdateEmployeeExitInfo(aInfo);

    }

    private bool UpdateRegionInformation(EmployeeSuspendDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aSuspendDal.UpdateDataForEmpSuspend(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private EmployeeSuspendDao PrepareDataForUpdate()
    {
        var aSuspend = new EmployeeSuspendDao();

        aSuspend.SuspendId = Convert.ToInt32(suspendHiddenField.Value);
        aSuspend.EmpInfoId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
        aSuspend.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());
        aSuspend.EffectiveToDate = Convert.ToDateTime(effectToDateTexBox.Text.Trim());

        aSuspend.CompanyInfoId = Convert.ToInt32(companyDropDownList.SelectedValue);
        if (deptIdHiddenField.Value != "")
        {
            aSuspend.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        }
        if (desigIdHiddenField.Value != "")
        {
            aSuspend.DesigId = Convert.ToInt32(desigIdHiddenField.Value);
        }

        aSuspend.EmpTypeId = Convert.ToInt32(empTypeHiddenField.Value);

        aSuspend.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aSuspend.UpdateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        aSuspend.ActionStatus = "Posted";

        aSuspend.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aSuspend.Description = descriptionTexBox.Text.Trim();
        aSuspend.Remarks = remarksTextBox.Text.Trim();
        aSuspend.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

        aSuspend.EmpCode = empCodeLabel.Text.Trim();


        //aSuspend.DivisionWId = Convert.ToInt32(divitionIdHiddenField.Value);
        //aSuspend.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);

        // aSuspend.TypeId = Convert.ToInt32(typeDropDownList.SelectedValue);
        aSuspend.IsActive = true;
        aSuspend.ReasonId = Convert.ToInt32(actionTypeDropDownList.SelectedValue);

        if (HFDivID.Value != "")
        {
            aSuspend.DivisionId =
                Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
        }

        if (HFDivWingId.Value != "")
        {
            aSuspend.DivisionWId =
                Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
        }


        if (HFSecID.Value != "")
        {
            aSuspend.SectionId =
                Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
        }

        if (HFSubSecID.Value != "")
        {
            aSuspend.SubSectionId =
                Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
        }
        if (HFSalLocID.Value != "")
        {
            aSuspend.SalaryLoationId =
                Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
        }

        if (HFJobLocID.Value != "")
        {
            aSuspend.JobLocationId =
                Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
        }
        return aSuspend;
    }

    private Int32 SaveSuspendInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aSuspendDal.SaveDataForSuspend(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 DELSaveSuspendInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aSuspendDal.InsertDeleteSuspendInfoById(DELPrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private EmployeeSuspendDao DELPrepareDataForSave()
    {

        var aSuspend = new EmployeeSuspendDao();
        aSuspend.SuspendId = Convert.ToInt32(suspendHiddenField.Value);
        aSuspend.EmpInfoId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
        aSuspend.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());
        aSuspend.EffectiveToDate = Convert.ToDateTime(effectToDateTexBox.Text.Trim());
        aSuspend.CompanyInfoId = Convert.ToInt32(companyDropDownList.SelectedValue);
        if (deptIdHiddenField.Value != "")
        {
            aSuspend.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        }
        if (desigIdHiddenField.Value != "")
        {
            aSuspend.DesigId = Convert.ToInt32(desigIdHiddenField.Value);
        }

        aSuspend.EmpTypeId = Convert.ToInt32(empTypeHiddenField.Value);

        aSuspend.EntryBy = Session["UserId"].ToString();
        aSuspend.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        aSuspend.ActionStatus = "Posted";

        aSuspend.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aSuspend.Description = descriptionTexBox.Text.Trim();
        aSuspend.Remarks = remarksTextBox.Text.Trim();


        aSuspend.IsActive = true;
        aSuspend.ReasonId = Convert.ToInt32(actionTypeDropDownList.SelectedValue);
        aSuspend.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

        aSuspend.EmpCode = empCodeLabel.Text.Trim();

        if (manualUpdateCheckBox.Checked)
        {
            aSuspend.AutoProcess = "Manually Updated";
        }



        if (HFDivID.Value != "")
        {
            aSuspend.DivisionId =
                Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
        }

        if (HFDivWingId.Value != "")
        {
            aSuspend.DivisionWId =
                Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
        }


        if (HFSecID.Value != "")
        {
            aSuspend.SectionId =
                Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
        }

        if (HFSubSecID.Value != "")
        {
            aSuspend.SubSectionId =
                Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
        }
        if (HFSalLocID.Value != "")
        {
            aSuspend.SalaryLoationId =
                Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
        }

        if (HFJobLocID.Value != "")
        {
            aSuspend.JobLocationId =
                Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
        }

        return aSuspend;
    }

    private EmployeeSuspendDao PrepareDataForSave()
    {

        var aSuspend = new EmployeeSuspendDao();

        aSuspend.EmpInfoId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
        aSuspend.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());
        aSuspend.EffectiveToDate = Convert.ToDateTime(effectToDateTexBox.Text.Trim());
        aSuspend.CompanyInfoId = Convert.ToInt32(companyDropDownList.SelectedValue);
        if (deptIdHiddenField.Value != "")
        {
            aSuspend.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        }
        if (desigIdHiddenField.Value != "")
        {
            aSuspend.DesigId = Convert.ToInt32(desigIdHiddenField.Value);
        }

        aSuspend.EmpTypeId = Convert.ToInt32(empTypeHiddenField.Value);

        aSuspend.EntryBy = Session["UserId"].ToString();
        aSuspend.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        aSuspend.ActionStatus = "Posted";

        aSuspend.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());  
        aSuspend.Description = descriptionTexBox.Text.Trim();
        aSuspend.Remarks = remarksTextBox.Text.Trim();
       
      
        aSuspend.IsActive = true;
        aSuspend.ReasonId = Convert.ToInt32(actionTypeDropDownList.SelectedValue);
        aSuspend.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

        aSuspend.EmpCode = empCodeLabel.Text.Trim();

        if (manualUpdateCheckBox.Checked)
        {
            aSuspend.AutoProcess = "Manually Updated"; 
        }

        

        if (HFDivID.Value != "")
        {
            aSuspend.DivisionId =
                Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
        }

        if (HFDivWingId.Value != "")
        {
            aSuspend.DivisionWId =
                Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
        }

     
        if (HFSecID.Value != "")
        {
            aSuspend.SectionId =
                Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
        }

        if (HFSubSecID.Value != "")
        {
            aSuspend.SubSectionId =
                Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
        }
        if (HFSalLocID.Value != "")
        {
            aSuspend.SalaryLoationId =
                Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
        }

        if (HFJobLocID.Value != "")
        {
            aSuspend.JobLocationId =
                Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
        }

        return aSuspend;
    }

    private bool DataValidation()
    {
        if (effectDateTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Effective From date is required!!", this);
            effectDateTexBox.Focus();
            return false;
        }

        if (effectToDateTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Effective To date is required!!", this);
            effectToDateTexBox.Focus();
            return false;
        }

        if (actionTypeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please select action type!!", this);
            actionTypeDropDownList.Focus();
            return false;
        }

        if (empNameTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Employee Name is required!!", this);
            empNameTexBox.Focus();
            return false;
        }
        if (EffectDate() == false)
        {
            aShowMessage.ShowMessageBox("Please enter Valid Date!!", this);
            return false;
        }

        return true;
    }

    private bool EffectDate()
    {
        try
        {
            var aDateTime = new DateTime();
            aDateTime = Convert.ToDateTime(effectDateTexBox.Text);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }


    private void Clear()
    {
        effectDateTexBox.Text = "";
        //EmpMasterCodeTextBox.Text = "";

        companyDropDownList.SelectedValue = "";
        empTypeHiddenField.Value = "";
        employeeType.Text = "";

        EmpInfoIdHiddenField.Value = "";
        empNameTexBox.Text = "";

        //comNameLabel.Text = "";
        //comIdHiddenField.Value = "";

        //divisionNameLabel.Text = "";
        //divitionIdHiddenField.Value = "";

        //divWingNameLabel.Text = "";
        //divWingIdHiddenField.Value = "";


        deptNameLabel.Text = "";
        deptIdHiddenField.Value = "";

        //secNameLabel.Text = "";
        //secIdHiddenField.Value = "";

        //subSectionLabel.Text = "";
        //subSectionHiddenField.Value = "";

        desigNameLabel.Text = "";
        desigIdHiddenField.Value = "";

        //empGradeLabel.Text = "";
        //empGradeIdHiddenField.Value = "";

        joiningDateLabel.Text = "";

        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        //typeDropDownList.SelectedValue = "";

        EmpInfoIdHiddenField.Value = "";
        suspendHiddenField.Value = "";

        submithButton.Text = "Save";

        actionTypeDropDownList.Items.Clear();
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeSuspendView.aspx");
    }

    protected void popupButton_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
    }

    protected void refreshButton_Click(object sender, EventArgs e)
    {
        //aSuspendDal.EmployeeTypeList(typeDropDownList);
    }


    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveEmployeeType(string empName)
    {
        var aTypeDao = new EmployeeTypeDao();

        aTypeDao.EmpType = empName.Trim();
        aTypeDao.IsActive = true;
        aTypeDao.EntryBy = Session["LoginName"].ToString();
        aTypeDao.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        aSuspendDal.SaveEmployeeType(aTypeDao);
    }

    protected void searchdddddButton_Click(object sender, EventArgs e)
    {

        //if (!string.IsNullOrEmpty(EmpMasterCodeTextBox.Text.Trim()))
        //{
            DataTable aTable = aSuspendDal.LoadEmpInfo(EmpInfoIdHiddenField.Value);

            if (aTable.Rows.Count > 0)
            {

                EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
                empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

                //comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
                //comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

                //divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
                //divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

                //divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
                //divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

                //secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
                //secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

                //subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
                //subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

                //empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
                //empGradeIdHiddenField.Value = aTable.Rows[0]["GradeId"].ToString().Trim();

                joiningDateLabel.Text = Convert.ToDateTime(aTable.Rows[0]["DateOfJoin"]).ToString("yy-mmm-dd");

            }
            else
            {
                aShowMessage.ShowMessageBox("Employee not Active", this);
            }
        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Input Employee Code", this);
        //}
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetEmployeeInfo();

        DataTable aTable = aSuspendDal.LoadEmpInfoFromEmp(EmpInfoIdHiddenField.Value);

        if (aTable.Rows.Count > 0)
        {

            EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
            empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

            empCodeLabel.Text = aTable.Rows[0]["EmpMasterCode"].ToString().Trim();
         

            deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();
 

            desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

          

            employeeType.Text = aTable.Rows[0]["EmpType"].ToString().Trim();
            empTypeHiddenField.Value = aTable.Rows[0]["EmpTypeId"].ToString().Trim();




            HFDivID.Value = aTable.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = aTable.Rows[0]["DivisionWId"].ToString();

            HFSecID.Value = aTable.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = aTable.Rows[0]["SubSectionId"].ToString();

            HFSalLocID.Value = aTable.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = aTable.Rows[0]["JobLocationId"].ToString();


            joiningDateLabel.Text = Convert.ToDateTime(aTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

        }
        else
        {
            aShowMessage.ShowMessageBox("Employee not Active", this);
        }
    }

    private void SetEmployeeInfo()
    {
        string empName = EmployeeDropDownList.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            empNameTexBox.Text = emp[2];
            EmployeeDropDownList.Text = emp[2];
            EmpInfoIdHiddenField.Value = emp[0];
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();
          //  EmployeeDropDownList.Text = "";
        }
        else
        {
            EmployeeDropDownList.Text = "";
            empNameTexBox.Text = "";
            EmpInfoIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //aSuspendDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);

        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;

            aSuspendDal.LoadActionType(actionTypeDropDownList, companyDropDownList.SelectedValue);
            aSuspendDal.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
        }
        else
        {
            actionTypeDropDownList.Items.Clear();
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeSuspendView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (DataValidation())
        {
            

            if (suspendHiddenField.Value != "")
            {
                try
                {
                       DataTable aTable =
                             aSuspendDal.DeleteValidattionForEffectiveDate(suspendHiddenField.Value.ToString());
                    if (aTable.Rows.Count > 0)
                    {
                        string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                        string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                        if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                        {
                            bool area = UpdateRegionInformation(PrepareDataForUpdate());

                            if (area)
                            {
                                if (manualUpdateCheckBox.Checked)
                                {

                                    Int32 empGenId = 0;
                                    string reason = "";

                                    empGenId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
                                    reason = Convert.ToString(actionTypeDropDownList.SelectedItem.Text);

                                    UpdateEmployeeStepId(empGenId, reason);
                                }


                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Data Update Successfull...');window.location ='EmployeeSuspendView.aspx';",
                                    true);

                            }
                        }
                        else
                        {
                            aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                    throw;
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
           DataTable aTable = aSuspendDal.DeleteValidattionForEffectiveDate(suspendHiddenField.Value.ToString());
        if (aTable.Rows.Count > 0)
        {
            string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
            string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");
           
          
            if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
            {  Int32 departmentId = DELSaveSuspendInformation();
                if (departmentId > 0)
                {
                    if (aSuspendDal.DeleteSuspendInfoById(suspendHiddenField.Value))
                    {
                        ResetEmpGeneralInfo(Convert.ToInt32(suspendHiddenField.Value));

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Data Deleted ...');window.location ='EmployeeSuspendView.aspx';",
                            true);
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
            }
        }
    }

    private void ResetEmpGeneralInfo(int suspendId)
    {
        DataTable aTable = aSuspendDal.FetchEmployeeInfoById(suspendId);

        if (aTable.Rows.Count > 0)
        {
            Int32 employeeId = aTable.Rows[0].Field<Int32>("EmployeeId");

            EmpGeneralInfo aInfo = new EmpGeneralInfo();

            aInfo.InactiveReason = null;
            aInfo.EmployeeStatus = "Active";
            aInfo.EmpInfoId = employeeId;

            aSuspendDal.UpdateEmployeeExitInfo(aInfo);

        }
    }

    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        effectDateTexBox.Text = "";
    }

    protected void effectDateTexBox_Changed(object sender, EventArgs e)
    {
        if (FinancialYearDropDownList.SelectedValue != "")
        {
            if (effectDateTexBox.Text != "")
            {


                if (CheckStartEndDateExistOrNot(effectDateTexBox.Text, effectDateTexBox.Text) == true)
                {

                }
                if (CheckStartEndDateExistOrNot(effectDateTexBox.Text, effectDateTexBox.Text) == false)
                {
                    aShowMessage.ShowMessageBox("Effective From date must be within the finnancial year!!", this);
                    effectDateTexBox.Text = "";
                    effectDateTexBox.Focus();

                }
            }



        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this.", this);
            effectDateTexBox.Text = "";
            FinancialYearDropDownList.Focus();
        }
        
        
    }

    protected void effectDateTexBoxTo_Changed(object sender, EventArgs e)
    {
        if (FinancialYearDropDownList.SelectedValue != "")
        {
            if (effectToDateTexBox.Text != "")
            {


                if (CheckStartEndDateExistOrNot(effectToDateTexBox.Text, effectToDateTexBox.Text) == true)
                {

                }
                if (CheckStartEndDateExistOrNot(effectToDateTexBox.Text, effectToDateTexBox.Text) == false)
                {
                    aShowMessage.ShowMessageBox("Effective To date must be within the finnancial year!!", this);
                    effectToDateTexBox.Text = "";
                    effectToDateTexBox.Focus();

                }
            }



        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this.", this);
            effectDateTexBox.Text = "";
            FinancialYearDropDownList.Focus();
        }


    }
    private bool CheckStartEndDateExistOrNot(string Start, string End)
    {
        bool status = false;

        DataTable dataTable = aSuspendDal.CheckStartEndDateExistOrNotDAL(FinancialYearDropDownList.SelectedValue, Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

}
