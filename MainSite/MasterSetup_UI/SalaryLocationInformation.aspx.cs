using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_SalaryLocationInformation : System.Web.UI.Page
{
    SalaryLocationInformationDal aInformationDal = new SalaryLocationInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
            ButtonVisible();
            SetCheckBox();
           

            if (Session["LocationId"] != null)
            {
                GetOneRecord(Session["LocationId"].ToString());
                Session["LocationId"] = null;
            }
        }
    }

    private void DropDownList()
    {
        aInformationDal.GetOfficeNameDropdown(OfficeNameDownList);
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
            Response.Redirect("SalaryLocationInformationView.aspx");
        }

    }
    private void GetOneRecord(string locationId)
    {
        DataTable dataTable = aInformationDal.GetSalaryLocationInformationById(locationId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            salaryLocationIdField.Value = dataTable.Rows[rowIndex].Field<Int32>("SalaryLoationId").ToString(CultureInfo.InvariantCulture);

            if (dataTable.Rows[0]["JoinIdSalaryLocation"] != DBNull.Value && dataTable.Rows[rowIndex].Field<Int32>("JoinIdSalaryLocation") != 0)
            
            {
                OfficeNameDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("JoinIdSalaryLocation").ToString(CultureInfo.InvariantCulture);
            }
          
            salaryLocationHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("SalaryLocation");
            salaryLocationTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SalaryLocation");

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

        if (salaryLocationTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryLocation, this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (salaryLocationIdField.Value == "")
            {
                try
                {
                    Int32 locationId = SaveSalaryLocationInformation();

                    if (locationId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='SalaryLocationInformationView.aspx';",
                    true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (salaryLocationIdField.Value != "")
            {
                try
                {
                    bool location = UpdateSalaryLocationInformation(PrepareDataForUpdate());

                    if (location)
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

    private bool UpdateSalaryLocationInformation(SalaryLocationInformationDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aInformationDal.UpdateSalaryLocationInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private SalaryLocationInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new SalaryLocationInformationDao();

        aInformationDao.SalaryLoationId = Convert.ToInt32(salaryLocationIdField.Value);
        aInformationDao.SalaryLocation = salaryLocationTextBox.Text.Trim();
        if (OfficeNameDownList.SelectedIndex > 0)
        {
            aInformationDao.JoinIdSalaryLocation = Convert.ToInt32(OfficeNameDownList.SelectedValue);
        }
        else
        {
            aInformationDao.JoinIdSalaryLocation = 0;
        }
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveSalaryLocationInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveSalaryLocationInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }


    private Int32 SaveSalaryLocationInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveSalaryLocationInfoDEL(PrepareDataForSaveDEL());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private SalaryLocationInformationDao PrepareDataForSave()
    {
        var aInformationDao = new SalaryLocationInformationDao();

        aInformationDao.SalaryLocation = salaryLocationTextBox.Text.Trim();

        if (OfficeNameDownList.SelectedIndex>0)
        {
            aInformationDao.JoinIdSalaryLocation = Convert.ToInt32(OfficeNameDownList.SelectedValue);
        }
        else
        {
            aInformationDao.JoinIdSalaryLocation = 0;
        }
      
      
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    private SalaryLocationInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new SalaryLocationInformationDao();
        aInformationDao.SalaryLoationId = Convert.ToInt32(salaryLocationIdField.Value);
        aInformationDao.SalaryLocation = salaryLocationTextBox.Text.Trim();
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
        salaryLocationHiddenField.Value = "";
        salaryLocationIdField.Value = "";
        salaryLocationTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";

        SetCheckBox();
    }
    private bool CheckLocationExistOrNot(string location)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.CheckLocationExistOrNot(location);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SalaryLocationInformationView.aspx");
    }

    protected void salaryLocationTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (salaryLocationHiddenField.Value != "")
        {
            if (salaryLocationTextBox.Text != salaryLocationHiddenField.Value)
            {
                if (CheckLocationExistOrNot(salaryLocationTextBox.Text))
                {
                    aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                    salaryLocationTextBox.Text = salaryLocationHiddenField.Value;
                }
            }
        }
        else
        {
            if (CheckLocationExistOrNot(salaryLocationTextBox.Text))
            {
                aShowMessage.ShowMessageBox(aMessages.ConflictMessage, this);
                salaryLocationTextBox.Text = "";
            }
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            

            if (salaryLocationIdField.Value != "")
            {
                try
                {
                    bool location = UpdateSalaryLocationInformation(PrepareDataForUpdate());

                    if (location)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='SalaryLocationInformationView.aspx';",
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
        if (aInformationDal.DeleteSalaryLocationInfoById(salaryLocationIdField.Value))
        {

            Int32 locationId = SaveSalaryLocationInformationDEL();

            if (locationId > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='SalaryLocationInformationView.aspx';",
                    true);
            }

        }

        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}