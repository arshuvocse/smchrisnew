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

public partial class SuspendAndDiciplinary_UI_EmployeeCauseShow : System.Web.UI.Page
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
            if (Session["suspendId"] != null)
            {
                GetOneRecord(Session["suspendId"].ToString());
                Session["suspendId"] = null;
            }
        }
    }

    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompanyDropDownList(companyDropDownList);
        aSuspendDal.EmployeeTypeList(typeDropDownList);
    }

    private void GetOneRecord(string suspendId)
    {

        DataTable aTable = aSuspendDal.EmpSuspendInformation(suspendId);

        const int rowIndex = 0;

        if (aTable.Rows.Count > 0)
        {
            effectDateTexBox.Text = aTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString(CultureInfo.InvariantCulture);
          //  EmpMasterCodeTextBox.Text = aTable.Rows[rowIndex].Field<string>("EmpMasterCode").ToString(CultureInfo.InvariantCulture);

            EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
            empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

            comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
            comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

            divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
            divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

            divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
            divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


            deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

            secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
            secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

            subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
            subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

            desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

            empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
            empGradeIdHiddenField.Value = aTable.Rows[0]["GradeId"].ToString().Trim();

            joiningDateLabel.Text = aTable.Rows[0]["JoiningDate"].ToString();

            descriptionTexBox.Text = aTable.Rows[rowIndex].Field<string>("Description").ToString(CultureInfo.InvariantCulture);
            remarksTextBox.Text = aTable.Rows[rowIndex].Field<string>("Remarks").ToString(CultureInfo.InvariantCulture);
            typeDropDownList.SelectedValue = aTable.Rows[rowIndex].Field<Int32>("TypeId").ToString(CultureInfo.InvariantCulture);

            submithButton.Text = "Update";


        }
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty((Convert.))
        //{
           DataTable aTable = aSuspendDal.LoadEmpInfo(EmployeeDropDownList.SelectedValue);

            if (aTable.Rows.Count > 0)
            {

                EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
                empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

                comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
                comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

                divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
                divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

                divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
                divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

                secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
                secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

                subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
                subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

                empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
                empGradeIdHiddenField.Value = aTable.Rows[0]["GradeId"].ToString().Trim();

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
                try
                {
                    Int32 suspendId = SaveSuspendInformation();

                    if (suspendId > 0)
                    {
                        aShowMessage.ShowMessageBox(aMessages.SaveSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (suspendHiddenField.Value != "")
            {
                try
                {
                    bool area = UpdateRegionInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
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

        aSuspend.SuspendId = Convert.ToInt32(empGradeIdHiddenField.Value);
        aSuspend.EmpInfoId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
        aSuspend.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());
        //aSuspend.Status = "Inactive";

        aSuspend.CompanyInfoId = Convert.ToInt32(comIdHiddenField.Value);
        aSuspend.DivisionWId = Convert.ToInt32(divWingIdHiddenField.Value);
        aSuspend.DivisionId = Convert.ToInt32(divitionIdHiddenField.Value);
        aSuspend.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        aSuspend.SectionId = Convert.ToInt32(secIdHiddenField.Value);
        aSuspend.DesigId = Convert.ToInt32(desigIdHiddenField.Value);
        //aSuspend.GradeId = Convert.ToInt32(empGradeIdHiddenField.Value);
        aSuspend.EmpTypeId = Convert.ToInt32(typeDropDownList.SelectedValue);

        aSuspend.EntryBy = Session["LoginName"].ToString();
        aSuspend.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        aSuspend.ActionStatus = "Posted";

        aSuspend.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aSuspend.Description = descriptionTexBox.Text.Trim();
        aSuspend.Remarks = remarksTextBox.Text.Trim();

        aSuspend.DivisionWId = Convert.ToInt32(divitionIdHiddenField.Value);
        aSuspend.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);

        //aSuspend.TypeId = Convert.ToInt32(typeDropDownList.SelectedValue);
        aSuspend.IsActive = true;

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

    private EmployeeSuspendDao PrepareDataForSave()
    {
        var aSuspend = new EmployeeSuspendDao();

        aSuspend.EmpInfoId = Convert.ToInt32(EmpInfoIdHiddenField.Value);
        aSuspend.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());
       // aSuspend.Status = "Inactive";

        aSuspend.CompanyInfoId = Convert.ToInt32(comIdHiddenField.Value);
        aSuspend.DivisionWId = Convert.ToInt32(divWingIdHiddenField.Value);
        aSuspend.DivisionId = Convert.ToInt32(divitionIdHiddenField.Value);
        aSuspend.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        aSuspend.SectionId = Convert.ToInt32(secIdHiddenField.Value);
        aSuspend.DesigId = Convert.ToInt32(desigIdHiddenField.Value);
        //aSuspend.GradeId = Convert.ToInt32(empGradeIdHiddenField.Value);
        aSuspend.EmpTypeId = Convert.ToInt32(typeDropDownList.SelectedValue);

        aSuspend.EntryBy = Session["LoginName"].ToString();
        aSuspend.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        aSuspend.ActionStatus = "Posted";

        aSuspend.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aSuspend.Description = descriptionTexBox.Text.Trim();
        aSuspend.Remarks = remarksTextBox.Text.Trim();

        aSuspend.DivisionWId = Convert.ToInt32(divitionIdHiddenField.Value);
        aSuspend.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);

        //aSuspend.TypeId = Convert.ToInt32(typeDropDownList.SelectedValue);
        aSuspend.IsActive = true;

        return aSuspend;
    }

    private bool DataValidation()
    {
        if (effectDateTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Effective date is required!!", this);
            return false;
        }

        //if (EmpMasterCodeTextBox.Text == "")
        //{
        //    aShowMessage.ShowMessageBox("Employee Code is required!!", this);
        //    return false;
        //}
        if (empNameTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Employee Name is required!!", this);
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

        EmpInfoIdHiddenField.Value = "";
        empNameTexBox.Text = "";

        comNameLabel.Text = "";
        comIdHiddenField.Value = "";

        divisionNameLabel.Text = "";
        divitionIdHiddenField.Value = "";

        divWingNameLabel.Text = "";
        divWingIdHiddenField.Value = "";


        deptNameLabel.Text = "";
        deptIdHiddenField.Value = "";

        secNameLabel.Text = "";
        secIdHiddenField.Value = "";

        subSectionLabel.Text = "";
        subSectionHiddenField.Value = "";

        desigNameLabel.Text = "";
        desigIdHiddenField.Value = "";

        empGradeLabel.Text = "";
        empGradeIdHiddenField.Value = "";

        joiningDateLabel.Text = "";

        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        typeDropDownList.SelectedValue = "";

        EmpInfoIdHiddenField.Value = "";
        suspendHiddenField.Value = "";

        submithButton.Text = "Save";

    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    protected void popupButton_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
    }

    protected void refreshButton_Click(object sender, EventArgs e)
    {
        aSuspendDal.EmployeeTypeList(typeDropDownList);
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
            DataTable aTable = aSuspendDal.LoadEmpInfo(EmployeeDropDownList.SelectedValue);

            if (aTable.Rows.Count > 0)
            {

                EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
                empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

                comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
                comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

                divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
                divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

                divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
                divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

                secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
                secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

                subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
                subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

                empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
                empGradeIdHiddenField.Value = aTable.Rows[0]["GradeId"].ToString().Trim();

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
        DataTable aTable = aSuspendDal.LoadEmpInfo(EmployeeDropDownList.SelectedValue);

        if (aTable.Rows.Count > 0)
        {

            EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
            empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

            comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
            comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

            divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
            divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

            divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
            divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


            deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

            secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
            secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

            subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
            subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

            desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

            empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
            empGradeIdHiddenField.Value = aTable.Rows[0]["GradeId"].ToString().Trim();

            joiningDateLabel.Text = aTable.Rows[0]["DateOfJoin"].ToString();

        }
        else
        {
            aShowMessage.ShowMessageBox("Employee not Active", this);
        }
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aSuspendDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
    }
}