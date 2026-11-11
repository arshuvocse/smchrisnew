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

public partial class MasterSetup_UI_DesignationType : System.Web.UI.Page
{
    DesignationTypeDAL aInformationDal = new DesignationTypeDAL();
    
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            ButtonVisible();
            SetCheckBox();


            if (Session["DesignationTypeId"] != null)
            {
                GetOneRecord(Session["DesignationTypeId"].ToString());
                Session["DesignationTypeId"] = null;
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

            Response.Redirect("DesignationTypeView.aspx");
        }

    }
    private void GetOneRecord(string locationId)
    {
        DataTable dataTable = aInformationDal.GetSalaryLocationInformationById(locationId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            DesignationTypeIdField.Value = dataTable.Rows[rowIndex].Field<Int32>("DesignationTypeId").ToString(CultureInfo.InvariantCulture);
            DesignationTypeHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("DesigTypeName");
            DesignationTypeTextBox.Text = dataTable.Rows[rowIndex].Field<string>("DesigTypeName");

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

    private bool Validation()
    {

        if (DesignationTypeTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSalaryLocation, this);
            return false;
        }

        return true;
    }
    private  void SetCheckBox  ()
        {
            if (!isActiveCheckBox.Checked)
            {
                isActiveCheckBox.Checked = true;
            }
        }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DesignationTypeView.aspx");
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (DesignationTypeIdField.Value == "")
            {
                try
                {
                    Int32 locationId = SaveSalaryLocationInformation();

                    if (locationId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='DesignationTypeView.aspx';",
                     true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (DesignationTypeIdField.Value != "")
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

    

    private DesignationTypeDAO PrepareDataForUpdate()
    {
        var aInformationDao = new DesignationTypeDAO();

        aInformationDao.DesignationTypeId = Convert.ToInt32(DesignationTypeIdField.Value);
        aInformationDao.DesigTypeName = DesignationTypeTextBox.Text.Trim();
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

    private bool UpdateSalaryLocationInformation(DesignationTypeDAO prepareDataForUpdate)
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


    private DesignationTypeDAO PrepareDataForSave()
    {
        var aInformationDao = new DesignationTypeDAO();

        aInformationDao.DesigTypeName = DesignationTypeTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    private void Clear()
    {
        DesignationTypeHiddenField.Value = "";
        DesignationTypeIdField.Value = "";
        DesignationTypeTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";

        SetCheckBox();
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void DesignationTypeTextBox_OnTextChanged(object sender, EventArgs e)
    {
       
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            
            if (DesignationTypeIdField.Value != "")
            {
                try
                {
                    bool location = UpdateSalaryLocationInformation(PrepareDataForUpdate());

                    if (location)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successfull Done...');window.location ='DesignationTypeView.aspx';",
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
        if (!CheckRegionAllocateOrNot(DesignationTypeIdField.Value))
        {
            if (aInformationDal.DeleteSalaryLocationInfoById(DesignationTypeIdField.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='DesignationTypeView.aspx';",
                    true);
                Clear();

            }
        }
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }

    private bool CheckRegionAllocateOrNot(string SalaryGradeId)
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