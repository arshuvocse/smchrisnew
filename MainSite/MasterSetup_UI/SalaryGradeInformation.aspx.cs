using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_SalaryGradeInformation : System.Web.UI.Page
{
    SalaryGradeInformationDal aInformationDal = new SalaryGradeInformationDal();
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
            if (Session["SalaryGradeId"] != null)
            {
                GetOneRecord(Session["SalaryGradeId"].ToString());
                Session["SalaryGradeId"] = null;
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
            Response.Redirect("SalaryGradeInformationView.aspx");
        }

    }

    private void LoadDropDownList()
    {
        aInformationDal.GetDesignationTypeList(DesignationTypeIdDropDownList);
        aInformationDal.GetEmpCategoryList(EmpCategoryDropDownList);
    }

    private void GetOneRecord(string salaryGradeId)
    {
        DataTable dataTable = aInformationDal.GetSalaryGradeInformationById(salaryGradeId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            salaryGradeIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
            EmpCategoryDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("EmpCategoryId").ToString(CultureInfo.InvariantCulture);
            DesignationTypeIdDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DesignationTypeId").ToString(CultureInfo.InvariantCulture);

            salaryGradeHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("GradeName");
            salaryGradeTextBox.Text = dataTable.Rows[rowIndex].Field<string>("GradeName");
            GradeCodeTextBox.Text = dataTable.Rows[rowIndex].Field<string>("GradeCode");
        

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

            descriptionTexBox.Text = dataTable.Rows[rowIndex].Field<string>("Description");
            remarksTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Remarks");

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

        if (EmpCategoryDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryType, this);
            return false;
        }

        if (GradeCodeTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryGrade, this);
            return false;
        }
 

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (salaryGradeIdHiddenField.Value == "")
            {
                try
                {
                    Int32 gradeId = SaveSalaryGradeInformation();

                    if (gradeId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successfull Done...');window.location ='SalaryGradeInformationView.aspx';",
                      true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (salaryGradeIdHiddenField.Value != "")
            {
                try
                {
                    bool type = UpdateSalaryTypeInformation(PrepareDataForUpdate());

                    if (type)
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

    private bool UpdateSalaryTypeInformation(SalaryGradeDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aInformationDal.UpdateSalaryTypeInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private SalaryGradeDao PrepareDataForUpdate()
    {
        var aInformationDao = new SalaryGradeDao();

        aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeIdHiddenField.Value);
        aInformationDao.EmpCategoryId = Convert.ToInt32(EmpCategoryDropDownList.SelectedValue);
        aInformationDao.DesignationTypeId = Convert.ToInt32(DesignationTypeIdDropDownList.SelectedValue);

        if (salaryGradeTextBox.Text.Trim()=="")
        {
             salaryGradeTextBox.Text = string.Empty;
        }
        aInformationDao.GradeName = salaryGradeTextBox.Text.Trim();
        aInformationDao.GradeCode = GradeCodeTextBox.Text.Trim();
       
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveSalaryGradeInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveSalaryGradeInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private SalaryGradeDao PrepareDataForSave()
    {
        var aInformationDao = new SalaryGradeDao();

        aInformationDao.EmpCategoryId = Convert.ToInt32(EmpCategoryDropDownList.SelectedValue);
        aInformationDao.DesignationTypeId = Convert.ToInt32(DesignationTypeIdDropDownList.SelectedValue);
        if (salaryGradeTextBox.Text.Trim() == "")
        {
            salaryGradeTextBox.Text = string.Empty;
        }
        aInformationDao.GradeName = salaryGradeTextBox.Text.Trim();
        aInformationDao.GradeCode = GradeCodeTextBox.Text.Trim();
        
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
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
        salaryGradeHiddenField.Value = "";
        salaryGradeIdHiddenField.Value = "";
        EmpCategoryDropDownList.SelectedValue = "";
        DesignationTypeIdDropDownList.SelectedValue = "";
        salaryGradeTextBox.Text = "";
        GradeCodeTextBox.Text = "";
    
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";

        submitButton.Text = "Save";
        SetCheckBox();
    }


    private bool CheckGradeExistOrNot(string salaryGrade)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.CheckGradeExistOrNot(salaryGrade);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckGradeCodeExistOrNot(string GradeCode)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.CheckGradeExistOrNot(GradeCode);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void salaryGradeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (salaryGradeHiddenField.Value != "")
        {
            if (salaryGradeTextBox.Text != salaryGradeHiddenField.Value)
            {
                if (CheckGradeExistOrNot(salaryGradeTextBox.Text))
                {
                    aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                    salaryGradeTextBox.Text = salaryGradeHiddenField.Value;
                }
            }
        }
        else
        {
            if (CheckGradeExistOrNot(salaryGradeTextBox.Text))
            {
                aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                salaryGradeTextBox.Text = "";
            }
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SalaryGradeInformationView.aspx");
    }

    protected void GradeCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (salaryGradeHiddenField.Value != "")
        {
            if (GradeCodeTextBox.Text != salaryGradeHiddenField.Value)
            {
                if (CheckGradeCodeExistOrNot(GradeCodeTextBox.Text))
                {
                    aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                    GradeCodeTextBox.Text = salaryGradeHiddenField.Value;
                }
            }
        }
        else
        {
            if (CheckGradeCodeExistOrNot(GradeCodeTextBox.Text))
            {
                aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                GradeCodeTextBox.Text = "";
            }
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
          

            if (salaryGradeIdHiddenField.Value != "")
            {
                try
                {
                    bool type = UpdateSalaryTypeInformation(PrepareDataForUpdate());

                    if (type)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successfull Done...');window.location ='SalaryGradeInformationView.aspx';",
                      true);
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

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (!CheckGradeAllocateOrNot(salaryGradeIdHiddenField.Value))
        {
            if (aInformationDal.DeleteSalaryGradeInfoById(salaryGradeIdHiddenField.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfull Done...');window.location ='SalaryGradeInformationView.aspx';",
                       true);
                Clear();

            }
        }
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }


        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var gradeId = dataKey[0].ToString();

        //        if (!CheckGradeAllocateOrNot(gradeId))
        //        {
        //            if (aInformationDal.DeleteSalaryGradeInfoById(gradeId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadSalaryGraadeInformation();
        //            }
        //        }
        //        else
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //            LoadSalaryGraadeInformation();
        //        }
        //    }
        //}
    }

    private bool CheckGradeAllocateOrNot(string gradeId)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.GradeAllocateOrNot(gradeId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
}