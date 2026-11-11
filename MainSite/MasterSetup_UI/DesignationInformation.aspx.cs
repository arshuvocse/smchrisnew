using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_DesignationInformation : System.Web.UI.Page
{
    DesignationInformationDal aInformationDal = new DesignationInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();
            SetCheckBox();

       

            if (Session["designationId"] != null)
            {
                GetOneRecord(Convert.ToInt32(Session["designationId"].ToString()));
                Session["designationId"] = null;
            }
            else
            {
                submitButton.Visible = true;
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

            Response.Redirect("DesignationInformationView.aspx");
        }

    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private void LoadDropDownList()
    {
        aInformationDal.GetEmpCategoryList(EmployeeCategoryDropDownList);
        //aInformationDal.LoadDesinationStep(designationStepNameDropDownList);

        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            CompanyDropDown.DataSource = dt;
            CompanyDropDown.DataValueField = "Value";
            CompanyDropDown.DataTextField = "TextField";
            CompanyDropDown.DataBind();
        }
    }

    private void GetOneRecord(Int32 designationId)
    {
        DataTable desigInfoById = aInformationDal.GetDesignationInformationById(designationId);

        const int rowIndex = 0;

        if (desigInfoById.Rows.Count > 0)
        {
            DesignationIdHiddenField.Value = desigInfoById.Rows[rowIndex].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);
            try
            {
                CompanyDropDown.SelectedValue = desigInfoById.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                
                //throw;
            }
            EmployeeCategoryDropDownList.SelectedValue = desigInfoById.Rows[rowIndex].Field<Int32>("EmpCategoryId").ToString(CultureInfo.InvariantCulture);
            if (EmployeeCategoryDropDownList.SelectedValue != "")
            {
                aInformationDal.GetSalaryGradeList(salaryGradeDropDownList, EmployeeCategoryDropDownList.SelectedValue);
            }
            salaryGradeDropDownList.SelectedValue = desigInfoById.Rows[rowIndex].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
           // salaryTypeDropDownList.SelectedValue = desigInfoById.Rows[rowIndex].Field<Int32>("SalaryTypeId").ToString(CultureInfo.InvariantCulture);

             

           
            designationTextBox.Text = desigInfoById.Rows[rowIndex].Field<string>("Designation");
            descriptionTexBox.Text = desigInfoById.Rows[rowIndex].Field<string>("Description");
            remarksTextBox.Text = desigInfoById.Rows[rowIndex].Field<string>("Remarks");

            if (desigInfoById.Rows[rowIndex].Field<bool>("IsActive"))
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

            submitButton.Text = "Update";
        }
    }

    private void SetCheckBox()
    {
        if (!isActiveCheckBox.Checked)
        {
            isActiveCheckBox.Checked = true;
        }
    }

    private bool Validation()
    {
        //if (designationStepNameDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VDesignationStep, this);
        //    return false;

//        ddlDivision
//ddlDepartment
//ddlDesignation


        //}

        //if (ddlDivision.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VSalaryType, this);
        //    return false;
        //}

        //if (salaryGradeDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VSalaryGrade, this);
        //    return false;
        //}

        //if (salaryStepDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VSalaryStep, this);
        //    return false;
        //}

        //if (designationTextBox.Text == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VDesignation, this);
        //    return false;
        //}

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (DesignationIdHiddenField.Value == "")
            {
                try
                {
                    Int32 designationId = SaveDesignationInformation();

                    if (designationId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successfull Done...');window.location ='DesignationInformationView.aspx';",
                      true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (DesignationIdHiddenField.Value != "")
            {
                try
                {
                    bool designation = UpdateeDesignationInformation();

                    if (designation)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }

    private bool UpdateeDesignationInformation()
    {
        bool retVal;
        try
        {
            retVal = aInformationDal.UpdateDesignationInfo(PrepareDataForUpdate());

        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }

    private DesignationInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new DesignationInformationDao();

        aInformationDao.DesignationId = Convert.ToInt32(DesignationIdHiddenField.Value);
   aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeDropDownList.SelectedValue);
   aInformationDao.CompanyId = Convert.ToInt32(CompanyDropDown.SelectedValue);
       // aInformationDao.SalaryStepId = Convert.ToInt32(salaryStepDropDownList.SelectedValue);
        aInformationDao.Designation = designationTextBox.Text.Trim();
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveDesignationInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveDesignationInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }
    private Int32 SaveDesignationInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveDesignationInfoDEL(PrepareDataForSaveDEL());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }
    private DesignationInformationDao PrepareDataForSave()
    {
        var aInformationDao = new DesignationInformationDao();

        aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeDropDownList.SelectedValue);
        aInformationDao.CompanyId = Convert.ToInt32(CompanyDropDown.SelectedValue);
       
        aInformationDao.Designation = designationTextBox.Text.Trim();
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    private DesignationInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new DesignationInformationDao();

        aInformationDao.DesignationId = Convert.ToInt32(DesignationIdHiddenField.Value);
        aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeDropDownList.SelectedValue);
        aInformationDao.CompanyId = Convert.ToInt32(CompanyDropDown.SelectedValue);
        aInformationDao.Designation = designationTextBox.Text.Trim();
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
         DesignationHiddenField.Value = "";
         EmployeeCategoryDropDownList.SelectedValue = "";
       
         salaryGradeDropDownList.Items.Clear();
       
        designationTextBox.Text = "";
         descriptionTexBox.Text = "";
        remarksTextBox.Text = "";

        DesignationIdHiddenField.Value = "";
         
        submitButton.Text = "Save";
        SetCheckBox();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DesignationInformationView.aspx");
    }

    protected void salaryTypeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (salaryTypeDropDownList.SelectedValue != "")
        //{
        //    aInformationDal.GetSalaryGradeList(salaryGradeDropDownList, salaryTypeDropDownList.SelectedValue);
        //}
        //else
        //{
        //    salaryGradeDropDownList.Items.Clear();
        //}
    }

    protected void salaryGradeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (salaryGradeDropDownList.SelectedValue != "")
        //{
        //    aInformationDal.GetSalaryStepList(salaryStepDropDownList, salaryGradeDropDownList.SelectedValue);
        //}
        //else
        //{
        //    salaryStepDropDownList.Items.Clear();
        //}
    }

    protected void EmployeeCategoryDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (EmployeeCategoryDropDownList.SelectedValue != "")
        {
            aInformationDal.GetSalaryGradeList(salaryGradeDropDownList, EmployeeCategoryDropDownList.SelectedValue);
        }
        else
        {
            salaryGradeDropDownList.Items.Clear();
        }
    }

    protected void designationTextBox_OnTextChanged(object sender, EventArgs e)
    {
        //if (DesignationHiddenField.Value != "")
        //{
        //    if (designationTextBox.Text != DesignationHiddenField.Value)
        //    {
        //        if (CheckGradeExistOrNot(designationTextBox.Text))
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
        //            designationTextBox.Text = DesignationHiddenField.Value;
        //        }
        //    }
        //}
        //else
        //{
        //    if (CheckGradeExistOrNot(designationTextBox.Text))
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
        //        designationTextBox.Text = "";
        //    }
        //}
    }

    private bool CheckGradeExistOrNot(string Designation)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.CheckGradeExistOrNot(Designation);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            

            if (DesignationIdHiddenField.Value != "")
            {
                try
                {
                    bool designation = UpdateeDesignationInformation();

                    if (designation)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='DesignationInformationView.aspx';",
                            true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        string designationId = dataKey[0].ToString();

        //        if (aInformationDal.DeleteDesgInfoById(designationId))
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //            LoadDesignationInfo();
        //        }
        //    }
        //}

        if (aInformationDal.DeleteDesgInfoById(DesignationIdHiddenField.Value))
            {
             Int32 designationId = SaveDesignationInformationDEL();

                if (designationId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfull Done...');window.location ='DesignationInformationView.aspx';",
                        true);
                }

            }
        
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }
}