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

public partial class MasterSetup_UI_DepartmentInformation : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    DepartmentInformationDal aDepartmentInformationDal = new DepartmentInformationDal();
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

           

            if (Session["departmentId"] != null)
            {
                GetOneRecord(Session["departmentId"].ToString());
                Session["departmentId"] = null;
            }
            
        }
    }

    private void GetOneRecord(string wingId)
    {
        DataTable dataTable = aDepartmentInformationDal.GetDepartmentInformationById(wingId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            mainid.Visible = true;
            rootRadioButtonList.SelectedValue = dataTable.Rows[0]["Root"].ToString();
            rootRadioButtonList.Enabled = true;
            departmentIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
            
            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

            if (companyDropDownList.SelectedValue != "")
            {
                aDepartmentInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
            }
            try
            {

            
            divisionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

            if (divisionDropDownList.SelectedValue != "")
            {
                aDepartmentInformationDal.GetDivisionWingList(wingDropDownList, divisionDropDownList.SelectedValue);
                wingDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
                wingHiddenField.Value= dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                wingDropDownList.Items.Clear();
            }
            }
            catch (Exception)
            {

                
            }
            departmentNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("DepartmentName");
            shortNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("ShortName");

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
            if (rootRadioButtonList.SelectedIndex == 0)
            {
                wing.Visible = false;
            }
            else
            {
                wing.Visible = true;
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

    private void LoadDropDownList()
    {
        aDepartmentInformationDal.GetCompanyListIntoDropdown(companyDropDownList);
        aDepartmentInformationDal.GetDivisionListIntoDropdown(divisionDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aDepartmentInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
        }
    }

    protected void divisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (divisionDropDownList.SelectedValue != "")
        {
            aDepartmentInformationDal.GetDivisionWingList(wingDropDownList, divisionDropDownList.SelectedValue);
        }
        else
        {
            wingDropDownList.Items.Clear();
        }
    }

    private bool Validation()
    {
        //if (companyDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Select Company Name!!!", this);
        //    return false;
        //}

        if (divisionDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VDivision, this);
            return false;
        }

        //if (wingDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VDivisionWing, this);
        //    return false;
        //}

        if (departmentNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VDepartment, this);
            return false;
        }

        if (shortNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VShortName, this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (departmentIdHiddenField.Value == "")
            {
                try
                {
                    Int32 departmentId = SaveDepartmentInformation();

                    if (departmentId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='DepartmentInformationView.aspx';",
                    true);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                    throw;
                }
            }

            if (departmentIdHiddenField.Value != "")
            {
                try
                {
                    bool department = UpdateDepartmentInformation();

                    if (department)
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

    private bool UpdateDepartmentInformation()
    {
        bool retVal;
        try
        {
            retVal = aDepartmentInformationDal.UpdateDepartmentInfo(PrepareDataForUpdate());

        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    
    private DivisionWingInformationDao PrepareDataForSaveWing()
    {
        var aInformationDao = new DivisionWingInformationDao();
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = departmentNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;
        aInformationDao.Invisible = true;

        return aInformationDao;
    }
    private DivisionWingInformationDao PrepareDataForUpdateWing()
    {
        var aInformationDao = new DivisionWingInformationDao();
        aInformationDao.DivisionWId = Convert.ToInt32(wingHiddenField.Value);
        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = departmentNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate= DateTime.Now;
        aInformationDao.Invisible = true;

        return aInformationDao;
    }

    private Int32 SaveDepartmentInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aDepartmentInformationDal.SaveDepartmentInfo(PrepareDataForSave());

        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 SaveDepartmentInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aDepartmentInformationDal.SaveDepartmentInfoDEL(PrepareDataForSaveDEL());

        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private DepartmentInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new DepartmentInformationDao();
        aInformationDao.DepartmentId = Convert.ToInt32(departmentIdHiddenField.Value);
        if (rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DivisionWId = 0;
                // aDepartmentInformationDal.SaveDivisionWingInfo(PrepareDataForSaveWing());
        }
        else
        {
            aInformationDao.DivisionWId = 0; //Convert.ToInt32(wingDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue); 
        aInformationDao.DepartmentName = departmentNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate= DateTime.Now;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;
        return aInformationDao;
    }
    private DepartmentInformationDao PrepareDataForSave()
    {
        var aInformationDao = new DepartmentInformationDao();
        if (rootRadioButtonList.SelectedIndex==0)
        {
            aInformationDao.DivisionWId = aDepartmentInformationDal.SaveDivisionWingInfo(PrepareDataForSaveWing());
        }
        else
        {
            aInformationDao.DivisionWId = Convert.ToInt32(wingDropDownList.SelectedValue);    
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        
        aInformationDao.DepartmentName = departmentNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;

        return aInformationDao;
    }

    private DepartmentInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new DepartmentInformationDao();

        aInformationDao.DepartmentId = Convert.ToInt32(departmentIdHiddenField.Value);
        if (rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DivisionWId = aDepartmentInformationDal.SaveDivisionWingInfo(PrepareDataForSaveWing());
        }
        else
        {
            aInformationDao.DivisionWId = Convert.ToInt32(wingDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DepartmentName = departmentNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;

        return aInformationDao;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
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

            Response.Redirect("DepartmentInformationView.aspx");
        }

    }

    private void Clear()
    {
        companyDropDownList.SelectedValue = "";
        divisionDropDownList.SelectedValue = "";
        wingDropDownList.SelectedValue = "";
        departmentNameTextBox.Text = "";
        shortNameTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        mainid.Visible = true;
        rootRadioButtonList.Enabled = true;
        for (int i = 0; i < rootRadioButtonList.Items.Count; i++)
        {
            rootRadioButtonList.Items[i].Selected = false;
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DepartmentInformationView.aspx");
    }
    protected void rootRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rootRadioButtonList.SelectedIndex==0)
        {
            wing.Visible = false;
        }
        else
        {
            wing.Visible = true; 
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
            

            if (departmentIdHiddenField.Value != "")
            {
                try
                {

                 //   if (!CheckWingAllocateOrNot(departmentIdHiddenField.Value))
                    {

                      // if (!CheckEmpDepartmentAllocateOrNot(departmentIdHiddenField.Value))
                        {
                            if (!CheckDeptNameOrNot(departmentIdHiddenField.Value, departmentNameTextBox.Text))
                        {
                            bool department = UpdateDepartmentInformation();

                            if (department)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Operation Successfull Done...');window.location ='DepartmentInformationView.aspx';",
                                    true);

                            }
                        }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Can not be Updated! Already Exist...');",
                                    true);

                            }
                        }


                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        //        "alert",
                        //        "alert('Can not be Updated! Already Defined in Employee Information...');",
                        //        true);

                        //}
                    }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //        "alert",
                    //        "alert('Can not be Updated! Already Defined in Section...');",
                    //        true);

                    //}
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
        if (!CheckWingAllocateOrNot(departmentIdHiddenField.Value))
        {
            if (!CheckEmpDepartmentAllocateOrNot(departmentIdHiddenField.Value))
            {
                if (aDepartmentInformationDal.DeleteDepartmentInfoById(departmentIdHiddenField.Value))
                {
                    Int32 departmentId = SaveDepartmentInformationDEL();

                    if (departmentId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='DepartmentInformationView.aspx';",
                            true);
                    }

                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Can not be Deleted! Already Defined in Employee Information Department...');window.location ='DepartmentInformationView.aspx';",
                    true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Can not be Deleted! Already Defined in Division Wing...');window.location ='DepartmentInformationView.aspx';",
                true);

        }
    }

    private bool CheckWingAllocateOrNot(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aDepartmentInformationDal.DepartmentAllocatedOrNot(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckEmpDepartmentAllocateOrNot(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EMPDepartmentAllocatedOrNotEMP(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckDeptNameOrNot(string departmentId, string DepartmentName)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EMPDptName(departmentId, DepartmentName);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
}