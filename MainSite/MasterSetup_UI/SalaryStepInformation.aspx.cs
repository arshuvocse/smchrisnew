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

public partial class MasterSetup_UI_SalaryStepInformation : System.Web.UI.Page
{
    SalaryStepInforamtionDal aInformationDal = new SalaryStepInforamtionDal();
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

         

            if (Session["SalaryStepId"] != null)
            {
                GetOneRecord(Session["SalaryStepId"].ToString());
                Session["SalaryStepId"] = null;
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
            Response.Redirect("SalaryStepInformationView.aspx");
        }

    }

    private void LoadDropDownList()
    {
        aInformationDal.GetSalaryGradeList(salaryGradeDropDownList);
    }

    private void GetOneRecord(string salaryStepId)
    {
        DataTable dataTable = aInformationDal.GetSalaryStepInformationById(salaryStepId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            salaryStepIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SalaryStepId").ToString(CultureInfo.InvariantCulture);
            salaryGradeDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
            salaryStepHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("SalaryStepName");
            //if (salaryTypeDropDownList.SelectedValue != "")
            //{
            //    aInformationDal.GetSalaryGradeList(salaryGradeDropDownList, salaryTypeDropDownList.SelectedValue);
            //    salaryGradeDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    salaryGradeDropDownList.Items.Clear();
            //}

        //    salaryStepHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("SalaryStepName");
            salaryStepTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SalaryStepName");

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


            GrossAmountTextBox.Text = Convert.ToDecimal(((dataTable.Rows[rowIndex]["GrossAmount"] == DBNull.Value) ? (decimal?)null : ((decimal)dataTable.Rows[rowIndex]["GrossAmount"]))).ToString();
            BasicAmountTextBox.Text = Convert.ToDecimal(((dataTable.Rows[rowIndex]["BasicAmount"] == DBNull.Value) ? (decimal?)null : ((decimal)dataTable.Rows[rowIndex]["BasicAmount"]))).ToString();


            
            //BasicAmountTextBox.Text = dataTable.Rows[rowIndex].Field<decimal>("BasicAmount").ToString();
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
      

        if (salaryGradeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryGrade, this);
            return false;
        }

        if (salaryStepTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryStep, this);
            return false;
        }
        if (GrossAmountTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryStep, this);
            return false;
        }
        if (BasicAmountTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryStep, this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (salaryStepIdHiddenField.Value == "")
            {
                try
                {
                    Int32 stepId = SaveSalaryStepInformation();

                    if (stepId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='SalaryStepInformationView.aspx';",
                     true);
                     
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

           
        }
    }

    private bool UpdateSalaryTypeInformation(SalaryStepInformationDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aInformationDal.UpdateSalaryStepInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private SalaryStepInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new SalaryStepInformationDao();

        aInformationDao.SalaryStepId = Convert.ToInt32(salaryStepIdHiddenField.Value);
        aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeDropDownList.SelectedValue);
        aInformationDao.SalaryStepName = salaryStepTextBox.Text.Trim();
        aInformationDao.GrossAmount = Convert.ToDecimal(GrossAmountTextBox.Text.Trim());
        aInformationDao.BasicAmount = Convert.ToDecimal(BasicAmountTextBox.Text.Trim());
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveSalaryStepInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveSalaryStepInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 SaveSalaryStepInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveSalaryStepInfoDEL(PrepareDataForSaveDEL());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private SalaryStepInformationDao PrepareDataForSave()
    {
        var aInformationDao = new SalaryStepInformationDao();

        aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeDropDownList.SelectedValue);
        aInformationDao.SalaryStepName = salaryStepTextBox.Text.Trim();
        aInformationDao.GrossAmount = Convert.ToDecimal(GrossAmountTextBox.Text.Trim());
        aInformationDao.BasicAmount = Convert.ToDecimal(BasicAmountTextBox.Text.Trim());
     
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    private SalaryStepInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new SalaryStepInformationDao();

        aInformationDao.SalaryStepId = Convert.ToInt32(salaryStepIdHiddenField.Value);
        aInformationDao.SalaryGradeId = Convert.ToInt32(salaryGradeDropDownList.SelectedValue);
        aInformationDao.SalaryStepName = salaryStepTextBox.Text.Trim();
        aInformationDao.GrossAmount = Convert.ToDecimal(GrossAmountTextBox.Text.Trim());
        aInformationDao.BasicAmount = Convert.ToDecimal(BasicAmountTextBox.Text.Trim());

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
        salaryStepIdHiddenField.Value = "";
        salaryStepHiddenField.Value = "";
       
        salaryGradeDropDownList.Items.Clear();

        salaryStepTextBox.Text = "";
        GrossAmountTextBox.Text = "";
        BasicAmountTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";

        submitButton.Text = "Save";
        SetCheckBox();
    }


    private bool CheckStepExistOrNot(string salaryStep)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.CheckStepExistOrNot(salaryStep, salaryGradeDropDownList.SelectedValue);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SalaryStepInformationView.aspx");
    }

    protected void salaryTypeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void salaryStepTextBox_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            if (salaryGradeDropDownList.SelectedValue != "")
            {
                if (salaryStepHiddenField.Value != "")
                {
                    if (salaryStepTextBox.Text.Trim() != salaryStepHiddenField.Value.Trim())
                    {

                        DataTable dataTable = aInformationDal.CheckStepExistOrNot(salaryStepTextBox.Text.Trim(), salaryGradeDropDownList.SelectedValue);

                        if (dataTable.Rows.Count>0)
                        {

                            if (dataTable.Rows.Count < 1)
                            {
                                aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                                salaryStepTextBox.Text = "";
                            }

                        }

                       
                    }
                }
                else
                {
                    if (CheckStepExistOrNot(salaryStepTextBox.Text.Trim()))
                    {
                        aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                        salaryStepTextBox.Text = "";
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Please Select Salary Grade...');",
                     true);
            }
        }
        catch (Exception)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Please Select Salary Grade...');",
                    true);
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
             
            if (salaryStepIdHiddenField.Value != "")
            {
                try
                {
                    bool type = UpdateSalaryTypeInformation(PrepareDataForUpdate());

                    if (type)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successfull Done...');window.location ='SalaryStepInformationView.aspx';",
                 true);
                        
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

        if (aInformationDal.DeleteSalaryStepInfoById(salaryStepIdHiddenField.Value))
            {

                Int32 stepId = SaveSalaryStepInformationDEL();

                if (stepId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfull Done...');window.location ='SalaryStepInformationView.aspx';",
                        true);
                }

            }
       
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}