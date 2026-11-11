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

public partial class MasterSetup_UI_EmployeeCategoryInformation : System.Web.UI.Page
{
    EmployeeCategoryInformationDal aInformationDal = new EmployeeCategoryInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ButtonVisible();
            SetCheckBox();


            if (Session["EmpCategoryId"] != null)
            {
                GetOneRecord(Session["EmpCategoryId"].ToString());
                Session["EmpCategoryId"] = null;
            }
            else
            {
                submitButton.Visible = true;
            }
        }
    }


    private void GetOneRecord(string EmpCategoryId)
    {
        DataTable dataTable = aInformationDal.GetEmpCategoryInformationById(EmpCategoryId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            EmployeeCategoryIdField.Value = dataTable.Rows[rowIndex].Field<Int32>("EmpCategoryId").ToString(CultureInfo.InvariantCulture);
            EmployeeCategoryHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("EmpCategoryName");
            EmployeeCategoryTextBox.Text = dataTable.Rows[rowIndex].Field<string>("EmpCategoryName");

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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeCategoryInformationView.aspx");
    }

    protected void EmployeeCategoryTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (EmployeeCategoryHiddenField.Value != "")
        {
            if (EmployeeCategoryTextBox.Text != EmployeeCategoryHiddenField.Value)
            {
                if (CheckLocationExistOrNot(EmployeeCategoryTextBox.Text))
                {
                    aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                    EmployeeCategoryTextBox.Text = EmployeeCategoryHiddenField.Value;
                }
            }
        }
        else
        {
            if (CheckLocationExistOrNot(EmployeeCategoryTextBox.Text))
            {
                aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                EmployeeCategoryTextBox.Text = "";
            }
        }
    }


    private bool CheckLocationExistOrNot(string EmpCat)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.CheckEmpCategoryExistOrNot(EmpCat);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (EmployeeCategoryIdField.Value == "")
            {
                try
                {
                    Int32 EmpCategoryId = SaveEmpCategoryInformation();

                    if (EmpCategoryId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='EmployeeCategoryInformationView.aspx';",
                    true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

           
        }
    }


    private EmployeeCategoryInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new EmployeeCategoryInformationDao();

        aInformationDao.EmpCategoryId = Convert.ToInt32(EmployeeCategoryIdField.Value);
        aInformationDao.EmpCategoryName = EmployeeCategoryTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }


    private bool UpdateEmpCategoryInformation(EmployeeCategoryInformationDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aInformationDal.UpdateEmpCategoryNInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private int SaveEmpCategoryInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveEmployeeCategoryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }


    private int SaveEmpCategoryInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveEmployeeCategoryInfoDEL(PrepareDataForSaveDEL());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private EmployeeCategoryInformationDao PrepareDataForSave()
    {
        var aInformationDao = new EmployeeCategoryInformationDao();

        aInformationDao.EmpCategoryName = EmployeeCategoryTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    private EmployeeCategoryInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new EmployeeCategoryInformationDao();
        aInformationDao.EmpCategoryId = Convert.ToInt32(EmployeeCategoryIdField.Value);
        aInformationDao.EmpCategoryName = EmployeeCategoryTextBox.Text.Trim();
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
        
    }

    private void Clear()
    {
        EmployeeCategoryHiddenField.Value = "";
        EmployeeCategoryIdField.Value = "";
        EmployeeCategoryTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";

        SetCheckBox();
    }


    private bool Validation()
    {

        if (EmployeeCategoryTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryLocation, this);
            return false;
        }

        return true;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            

            if (EmployeeCategoryIdField.Value != "")
            {
                try
                {
                    bool location = UpdateEmpCategoryInformation(PrepareDataForUpdate());

                    if (location)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='EmployeeCategoryInformationView.aspx';",
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

            Response.Redirect("EmployeeCategoryInformationView.aspx");
        }

    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (!CheckAreaAllocateOrNot(EmployeeCategoryIdField.Value))
        {
            if (aInformationDal.DeleteEmpCategoryInfoById(EmployeeCategoryIdField.Value))
            {
                 Int32 EmpCategoryId = SaveEmpCategoryInformationDEL();

                if (EmpCategoryId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfull Done...');window.location ='EmployeeCategoryInformationView.aspx';",
                        true);
                }

            }
        }
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }


    }

    private bool CheckAreaAllocateOrNot(string SalaryGradeId)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.SalaryGradeAllocatedOrNot(SalaryGradeId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
}