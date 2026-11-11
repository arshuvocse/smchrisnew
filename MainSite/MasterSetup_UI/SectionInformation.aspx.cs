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

public partial class MasterSetup_UI_SectionInformation : System.Web.UI.Page
{

    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();


    SectionInformationDal asSectionInformationDal = new SectionInformationDal();
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

            
            if (Session["sectionId"] != null)
            {
                GetOneRecord(Session["sectionId"].ToString());
                Session["sectionId"] = null;
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
            Response.Redirect("SectionInformationView.aspx");
        }

    }
    private void GetOneRecord(string sectionId)
    {
        DataTable dataTable = asSectionInformationDal.GetSectionInformationById(sectionId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            mainid.Visible = true;
            sectionHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SectionId").ToString(CultureInfo.InvariantCulture);

            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

            if (companyDropDownList.SelectedValue != "")
            {
                asSectionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
           }
            rootRadioButtonList.SelectedValue = dataTable.Rows[0]["Root"].ToString();
            rootRadioButtonList.Enabled = true;
            divisionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
            try
            {

            
            if (divisionDropDownList.SelectedValue != "")
            {
                asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionDropDownList.SelectedValue);
                asSectionInformationDal.GetDepartmentByDivList(departmentNameDropdownList, divisionDropDownList.SelectedValue);
                departmentNameDropdownList.SelectedValue =
                    dataTable.Rows[rowIndex].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
                divisionWingDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
                wingHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                divisionWingDropDownList.Items.Clear();
            }

            

            //if (divisionWingDropDownList.SelectedValue != "")
            //{
            //    asSectionInformationDal.GetDepartmentList(departmentNameDropdownList,
            //        divisionWingDropDownList.SelectedValue);
            
            //    deptHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    departmentNameDropdownList.Items.Clear();
            //}

            }
            catch (Exception)
            {

               
            }

            


            sectionTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SectionName");
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
            wing.Visible = false;
            dept.Visible = false;
            if (rootRadioButtonList.SelectedIndex == 1)
            {
                wing.Visible = true;
            }
            if (rootRadioButtonList.SelectedIndex == 2)
            {
                wing.Visible = true;
                dept.Visible = true;
            }
            DataTable dtgetdata = asSectionInformationDal.GetDepartmentRelaton(departmentNameDropdownList.SelectedValue, "");
            if (dtgetdata.Rows.Count > 0)
            {
                if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                {
                    wing.Visible = false;
                    divisionWingDropDownList.Items.Clear();
                    asSectionInformationDal.GetDivisionWingListAll(divisionWingDropDownList, divisionDropDownList.SelectedValue);
                    divisionWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    wingHiddenField.Value = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                else
                {
                    wing.Visible = true;
                    divisionWingDropDownList.Items.Clear();
                    asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionDropDownList.SelectedValue);
                    divisionWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    wingHiddenField.Value = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
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
        asSectionInformationDal.GetCompanyListIntoDropdown(companyDropDownList);
        asSectionInformationDal.GetDivisionListIntoDropdown(divisionDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            asSectionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
        }
    }

    protected void divisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (divisionDropDownList.SelectedValue != "")
        {
            asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionDropDownList.SelectedValue);
            asSectionInformationDal.GetDepartmentByDivList(departmentNameDropdownList, divisionDropDownList.SelectedValue);
        }
        else
        {
            divisionWingDropDownList.Items.Clear();
        }
    }

    protected void divisionWingDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (divisionWingDropDownList.SelectedValue != "")
        {
            asSectionInformationDal.GetDepartmentList(departmentNameDropdownList, divisionWingDropDownList.SelectedValue);
        }
        else
        {
            departmentNameDropdownList.Items.Clear();
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

        //if (divisionWingDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VDivision, this);
        //    return false;
        //}

        //if (departmentNameDropdownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VDepartment, this);
        //    return false;
        //}

        if (sectionTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSection, this);
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
            if (sectionHiddenField.Value == "")
            {
                try
            {
                Int32 sectionId = SaveSectionInformation();

                if (sectionId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successfull Done...');window.location ='SectionInformationView.aspx';",
                      true);
                    Clear();
                }
            }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (sectionHiddenField.Value != "")
            {
                try
                {
                    bool section = UpdateSectionInformation();

                    if (section)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }

    private bool UpdateSectionInformation()
    {
        bool retVal;
        try
        {
            retVal = asSectionInformationDal.UpdateSectionInfo(PrepareDataForUpdate());
        }
        catch (Exception ex)
        {
            retVal = false;
        }

        return retVal;
    }

    
    private DivisionWingInformationDao PrepareDataForUpdateWing()
    {
        var aInformationDao = new DivisionWingInformationDao();
        aInformationDao.DivisionWId = Convert.ToInt32(wingHiddenField.Value);
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = sectionTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;
        aInformationDao.Invisible = true;

        return aInformationDao;
    }
    private DepartmentInformationDao PrepareDataForUpdateDept()
    {
        DepartmentInformationDal aDepartmentInformationDal = new DepartmentInformationDal();
        var aInformationDao = new DepartmentInformationDao();
        aInformationDao.DepartmentId = Convert.ToInt32(deptHiddenField.Value);
        if (rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DivisionWId = Convert.ToInt32(wingHiddenField.Value);
            aDepartmentInformationDal.UpdateDivisionWingInfo(PrepareDataForUpdateWing());
        }
        else
        {
            aInformationDao.DivisionWId = Convert.ToInt32(divisionWingDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aInformationDao.DepartmentName = sectionTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;
        aInformationDao.Invisible = true;
        return aInformationDao;
    }
    private SectionInformationDao PrepareDataForUpdate()
    
    {
        var aInformationDao = new SectionInformationDao();
        aInformationDao.SectionId = Convert.ToInt32(sectionHiddenField.Value);
        if (rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DepartmentId = asSectionInformationDal.SaveDepartmentInfo(PrepareDataForSaveDept());

        }
        else
        {
            aInformationDao.DepartmentId = Convert.ToInt32(departmentNameDropdownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aInformationDao.SectionName = sectionTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;
        aInformationDao.Invisible = true;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;
        return aInformationDao;
    }
    private DivisionWingInformationDao PrepareDataForSaveWing()
    {
        var aInformationDao = new DivisionWingInformationDao();
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = sectionTextBox.Text.Trim();
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
    private DepartmentInformationDao PrepareDataForSaveDept()
    {
        DepartmentInformationDal aDepartmentInformationDal = new DepartmentInformationDal();
        var aInformationDao = new DepartmentInformationDao();
        if (rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DivisionWId = aDepartmentInformationDal.SaveDivisionWingInfo(PrepareDataForSaveWing());
        }
        else
        {
            aInformationDao.DivisionWId = Convert.ToInt32(divisionWingDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DepartmentName = sectionTextBox.Text.Trim();
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
    private SectionInformationDao PrepareDataForSave()
    {
        var aInformationDao = new SectionInformationDao();
        if (rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DepartmentId = asSectionInformationDal.SaveDepartmentInfo(PrepareDataForSaveDept());

        }
        else
        {
            aInformationDao.DepartmentId = Convert.ToInt32(departmentNameDropdownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.SectionName = sectionTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;
        aInformationDao.Invisible = true;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;
        
        return aInformationDao;
    }

    private SectionInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new SectionInformationDao();
        aInformationDao.SectionId = Convert.ToInt32(sectionHiddenField.Value);

        if (rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DepartmentId = asSectionInformationDal.SaveDepartmentInfo(PrepareDataForSaveDept());

        }
        else
        {
            aInformationDao.DepartmentId = Convert.ToInt32(departmentNameDropdownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.SectionName = sectionTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;
        aInformationDao.Invisible = true;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;

        return aInformationDao;
    }
    private Int32 SaveSectionInformation()
    {
        Int32 retVal;
        try
        {
            retVal = asSectionInformationDal.SaveSectionInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 SaveSectionInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = asSectionInformationDal.SaveSectionInfoDEL(PrepareDataForSaveDEL());

        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        mainid.Visible = true;
       // companyDropDownList.SelectedValue = "";
        divisionDropDownList.SelectedValue = "";
        divisionWingDropDownList.Items.Clear();
        departmentNameDropdownList.Items.Clear();
        sectionTextBox.Text = "";
        shortNameTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        rootRadioButtonList.Enabled = true;
        submitButton.Text = "Save";
        for (int i = 0; i < rootRadioButtonList.Items.Count; i++)
        {
            rootRadioButtonList.Items[i].Selected = false;
        }
    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SectionInformationView.aspx");
    }
    protected void rootRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        wing.Visible = false;
        dept.Visible = false;
        if (rootRadioButtonList.SelectedIndex==1)
        {
            wing.Visible = true;
            if (divisionDropDownList.SelectedIndex > 0)
            {
                asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionWingDropDownList.SelectedValue);
            }
        }
        if (rootRadioButtonList.SelectedIndex==2)
        {
            wing.Visible = true;
            dept.Visible = true;
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
            

            if (sectionHiddenField.Value != "")
            {
                try
                {
                    if (!CheckSectionAllocateOrNot(sectionHiddenField.Value))
                    {
                        if (!CheckEmpSectionAllocateOrNot(sectionHiddenField.Value))
                        {
                            bool section = UpdateSectionInformation();

                            if (section)
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Operation Successfull Done...');window.location ='SectionInformationView.aspx';",
                                    true);
                                Clear();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Can not be Deleted! Already Defined in Employee Information Section...');window.location ='SectionInformationView.aspx';",
                                true);

                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                               "alert",
                               "alert('Can not be Deleted! Already Defined in Sub-Section Information...');window.location ='SectionInformationView.aspx';",
                               true);

                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }
    private bool CheckSectionAllocateOrNot(string sectionId)
    {
        bool status = false;

        DataTable dataTable = asSectionInformationDal.SectionAllocatedOrNot(sectionId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (!CheckSectionAllocateOrNot(sectionHiddenField.Value))
        {
            if (!CheckEmpSectionAllocateOrNot(sectionHiddenField.Value))
            {
  Int32 sectionId = SaveSectionInformationDEL();
                if (asSectionInformationDal.DeleteSectionInfoById(sectionHiddenField.Value))
                {
                  

                    if (sectionId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='SectionInformationView.aspx';",
                            true);
                    }

                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Can not be Deleted! Already Defined in Employee Information Section...');window.location ='SectionInformationView.aspx';",
                    true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Can not be Deleted! Already Defined in Sub-Section Information...');window.location ='SectionInformationView.aspx';",
                   true);

        }
    }


    private bool CheckEmpSectionAllocateOrNot(string Section)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EMPSectionAllocatedOrNotEMP(Section);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void departmentNameDropdownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata = asSectionInformationDal.GetDepartmentRelaton(departmentNameDropdownList.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                divisionWingDropDownList.Items.Clear();
                asSectionInformationDal.GetDivisionWingListAll(divisionWingDropDownList, divisionDropDownList.SelectedValue);
                divisionWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                divisionWingDropDownList.Items.Clear();
                asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionDropDownList.SelectedValue);
                divisionWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
    }
}