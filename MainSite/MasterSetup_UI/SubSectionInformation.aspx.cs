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
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_SubSectionInformation : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();


    SubSectionInformationDal asSectionInformationDal = new SubSectionInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();
            SetCheckBox();

           

            if (Session["SubSectionId"] != null)
            {
                GetOneRecord(Session["SubSectionId"].ToString());
                Session["SubSectionId"] = null;
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

    }

   
    private void GetOneRecord(string subSectionId)
    {
        DataTable dataTable = asSectionInformationDal.GetSubSectionInformationById(subSectionId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            mainid.Visible = true;
            subSectionIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SubSectionId").ToString(CultureInfo.InvariantCulture);

            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

            if (companyDropDownList.SelectedValue != "")
            {
                asSectionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
            }
            rootRadioButtonList.SelectedValue = dataTable.Rows[0]["Root"].ToString();
            //rootRadioButtonList.Enabled = false;
            divisionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
            try
            {



                if (divisionDropDownList.SelectedValue != "")
                {
                    asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList,
                        divisionDropDownList.SelectedValue);
                    //asSectionInformationDal.GetDepartmentByDivList(departmentNameDropdownList, divisionDropDownList.SelectedValue);
                    asSectionInformationDal.GetSectionByDivList(sectionDropDownList, divisionDropDownList.SelectedValue);
                    sectionDropDownList.SelectedValue =
                        dataTable.Rows[rowIndex].Field<Int32>("SectionId").ToString(CultureInfo.InvariantCulture);
                    secHiddenField.Value =
                        dataTable.Rows[rowIndex].Field<Int32>("SectionId").ToString(CultureInfo.InvariantCulture);
                    divisionWingDropDownList.SelectedValue =
                        dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
                    wingHiddenField.Value =
                        dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);

                }
                else
                {
                    divisionWingDropDownList.Items.Clear();
                }
                DataTable dtgetdata1 = asSectionInformationDal.GetSectionRelaton(sectionDropDownList.SelectedValue, "");
                if (dtgetdata1.Rows.Count > 0)
                {
                    if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
                    {
                        dept.Visible = false;
                        departmentNameDropdownList.Items.Clear();
                        asSectionInformationDal.GetDepartmentByDivListAll(departmentNameDropdownList, divisionDropDownList.SelectedValue);
                        departmentNameDropdownList.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
                        deptHiddenField.Value = dtgetdata1.Rows[0]["DepartmentId"].ToString();
                    }
                    else
                    {
                        dept.Visible = true;
                        departmentNameDropdownList.Items.Clear();
                        asSectionInformationDal.GetDepartmentByDivList(departmentNameDropdownList, divisionDropDownList.SelectedValue);
                        departmentNameDropdownList.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
                        deptHiddenField.Value = dtgetdata1.Rows[0]["DepartmentId"].ToString();
                    }
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
                        wingHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        wing.Visible = true;
                        divisionWingDropDownList.Items.Clear();
                        asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionDropDownList.SelectedValue);
                        divisionWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                        wingHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);
                    }
                }


                //if (divisionWingDropDownList.SelectedValue != "")
                //{
                //    asSectionInformationDal.GetDepartmentList(departmentNameDropdownList,
                //        divisionWingDropDownList.SelectedValue);
                //    departmentNameDropdownList.SelectedValue =
                //        dataTable.Rows[rowIndex].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
                //    deptHiddenField.Value =
                //        dataTable.Rows[rowIndex].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
                //}
                //else
                //{
                //    departmentNameDropdownList.Items.Clear();
                //}



                //if (departmentNameDropdownList.SelectedValue != "")
                //{
                //    asSectionInformationDal.GetSectionList(sectionDropDownList, departmentNameDropdownList.SelectedValue);
                //    sectionDropDownList.SelectedValue =
                //        dataTable.Rows[rowIndex].Field<Int32>("SectionId").ToString(CultureInfo.InvariantCulture);
                //    secHiddenField.Value =
                //        dataTable.Rows[rowIndex].Field<Int32>("SectionId").ToString(CultureInfo.InvariantCulture);
                //}
                //else
                //{
                //    sectionDropDownList.Items.Clear();
                //}
            }
            catch (Exception)
            {

                
            }


            subSectionTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SubSectionName");
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
            sec.Visible = false;
            if (rootRadioButtonList.SelectedIndex == 1)
            {
                wing.Visible = true;
            }
            if (rootRadioButtonList.SelectedIndex == 2)
            {
                wing.Visible = true;
                dept.Visible = true;
            }
            if (rootRadioButtonList.SelectedIndex == 3)
            {
                wing.Visible = true;
                dept.Visible = true;
                sec.Visible = true;
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
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
       //asSectionInformationDal.GetDivisionListIntoDropdown(divisionDropDownList);
    }



    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            asSectionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
        }

        else
        {
            divisionDropDownList.Items.Clear();
        }
    }

    protected void divisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (divisionDropDownList.SelectedValue != "")
        {
            asSectionInformationDal.GetDivisionWingList(divisionWingDropDownList, divisionDropDownList.SelectedValue);
            asSectionInformationDal.GetDepartmentByDivList(departmentNameDropdownList, divisionDropDownList.SelectedValue);
            asSectionInformationDal.GetSectionByDivList(sectionDropDownList,divisionDropDownList.SelectedValue);
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

    protected void departmentNameDropdownList_OnTextChanged(object sender, EventArgs e)
    {
        if (departmentNameDropdownList.SelectedValue != "")
        {
            asSectionInformationDal.GetSectionList(sectionDropDownList, departmentNameDropdownList.SelectedValue);
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
        else
        {
            sectionDropDownList.Items.Clear();
        }
    }

    private bool Validation()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Company Name!!!", this);
            return false;
        }

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

        //if (sectionDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VSection, this);
        //    return false;
        //}

        if (subSectionTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VSubSection, this);
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
            if (subSectionIdHiddenField.Value == "")
            {
                try
            {
                Int32 sectionId = SaveSectionInformation();

                if (sectionId > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successfull Done...');window.location ='SubSectionInformationView.aspx';",
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

            if (subSectionIdHiddenField.Value != "")
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
                    throw;
                }
            }
        }
    }

    private bool UpdateSectionInformation()
    {
        bool retVal;
        try
        {
            retVal = asSectionInformationDal.UpdateSubSectionInfo(PrepareDataForUpdate());

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
        aInformationDao.DivisionWingName = subSectionTextBox.Text.Trim();
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
            aDepartmentInformationDal.UpdateDivisionWingInfo(PrepareDataForSaveWing());
        }
        else
        {
            aInformationDao.DivisionWId = Convert.ToInt32(divisionWingDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        aInformationDao.DepartmentName = subSectionTextBox.Text.Trim();
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
    private SectionInformationDao PrepareDataForUpdateSection()
    {
        SectionInformationDal asSectionInformationDal = new SectionInformationDal();
        var aInformationDao = new SectionInformationDao();
        aInformationDao.SectionId = Convert.ToInt32(secHiddenField.Value);
        if (rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DepartmentId = Convert.ToInt32(deptHiddenField.Value);
            asSectionInformationDal.UpdateDepartmentInfo(PrepareDataForUpdateDept());

        }
        else
        {
            aInformationDao.DepartmentId = Convert.ToInt32(departmentNameDropdownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        aInformationDao.SectionName = subSectionTextBox.Text.Trim();
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
    private SubSectionInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new SubSectionInformationDao();
        aInformationDao.SubSectionId = Convert.ToInt32(subSectionIdHiddenField.Value);
        if (rootRadioButtonList.SelectedIndex == 2 || rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.SectionId = asSectionInformationDal.SaveSectionInfo(PrepareDataForSaveSection());
        }
        else
        {
            aInformationDao.SectionId = Convert.ToInt32(sectionDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        aInformationDao.SubSectionName = subSectionTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;
        aInformationDao.Root = rootRadioButtonList.SelectedValue;

        return aInformationDao;
    }
    private Int32 SaveSectionInformation()
    {
        Int32 retVal;
        try
        {
            retVal = asSectionInformationDal.SaveSubSectionInfo(PrepareDataForSave());

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
            retVal = asSectionInformationDal.SaveSubSectionInfoDAL(PrepareDataForSaveDEL());

        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }
    private DivisionWingInformationDao PrepareDataForSaveWing()
    {
        var aInformationDao = new DivisionWingInformationDao();
        //aInformationDao.DivisionWId = Convert.ToInt32(divisionWingDropDownList.SelectedValue);
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = subSectionTextBox.Text.Trim();
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
        //aInformationDao.DepartmentId = Convert.ToInt32(departmentNameDropdownList.SelectedValue);
        if (rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.DivisionWId = aDepartmentInformationDal.SaveDivisionWingInfo(PrepareDataForSaveWing());
            //aDepartmentInformationDal.UpdateDivisionWingInfo(PrepareDataForUpdateWing());
        }
        else
        {
            aInformationDao.DivisionWId = Convert.ToInt32(divisionWingDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        aInformationDao.DepartmentName = subSectionTextBox.Text.Trim();
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
    private SectionInformationDao PrepareDataForSaveSection()
    {
        SectionInformationDal asSectionInformationDal = new SectionInformationDal();
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
        aInformationDao.SectionName = subSectionTextBox.Text.Trim();
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
    private SubSectionInformationDao PrepareDataForSave()
    {
        var aInformationDao = new SubSectionInformationDao();
        if (rootRadioButtonList.SelectedIndex == 2 || rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.SectionId = asSectionInformationDal.SaveSectionInfo(PrepareDataForSaveSection());
        }
        else
        {
            aInformationDao.SectionId = Convert.ToInt32(sectionDropDownList.SelectedValue);    
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);    
        
        aInformationDao.SubSectionName = subSectionTextBox.Text.Trim();
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
    private SubSectionInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new SubSectionInformationDao();
        aInformationDao.SubSectionId = Convert.ToInt32(subSectionIdHiddenField.Value);    

        if (rootRadioButtonList.SelectedIndex == 2 || rootRadioButtonList.SelectedIndex == 1 || rootRadioButtonList.SelectedIndex == 0)
        {
            aInformationDao.SectionId = asSectionInformationDal.SaveSectionInfo(PrepareDataForSaveSection());
        }
        else
        {
            aInformationDao.SectionId = Convert.ToInt32(sectionDropDownList.SelectedValue);
        }
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.SubSectionName = subSectionTextBox.Text.Trim();
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

    private void Clear()
    {
        rootRadioButtonList.Enabled = true;
        mainid.Visible = true;
        companyDropDownList.SelectedValue = "";
        divisionDropDownList.SelectedValue = "";
        divisionWingDropDownList.Items.Clear();
        departmentNameDropdownList.Items.Clear();
        sectionDropDownList.Items.Clear();
        subSectionTextBox.Text = "";
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
        Session["Status"] = "Add";
        Response.Redirect("SubSectionInformationView.aspx");
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            asSectionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
        }

        else
        {
            divisionDropDownList.Items.Clear();
        }
    }
    protected void rootRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        wing.Visible = false;
        dept.Visible = false;
        sec.Visible = false;
        if (rootRadioButtonList.SelectedIndex==1)
        {
            wing.Visible = true;
        }
        if (rootRadioButtonList.SelectedIndex==2)
        {
            wing.Visible = true;
            dept.Visible = true;
        }
        if (rootRadioButtonList.SelectedIndex==3)
        {
            wing.Visible = true;
            dept.Visible = true;
            sec.Visible = true;
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
            

            if (subSectionIdHiddenField.Value != "")
            {
                try
                {
                    bool section = UpdateSectionInformation();

                    if (section)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='SubSectionInformationView.aspx';",
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
        if (!CheckWingAllocateOrNot(subSectionIdHiddenField.Value))
        {
            if (!CheckEmpDivisionWinAllocateOrNot(subSectionIdHiddenField.Value))
            {
                if (asSectionInformationDal.DeleteSubSectionInfoById(subSectionIdHiddenField.Value))
                {
                    Int32 sectionId = SaveSectionInformationDEL();

                    if (sectionId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='SubSectionInformationView.aspx';",
                            true);
                    }

                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Can not be Deleted! Already Defined in Employee Information Sub-Section Information...');window.location ='SubSectionInformationView.aspx';",
                    true);

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
        //        var subSectionId = dataKey[0].ToString();

        //        //if (!CheckWingAllocateOrNot(subSectionId))
        //        //{
        //            if (aSectionInformationDal.DeleteSubSectionInfoById(subSectionId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadSubSectionInformation();
        //            }
        //        //}
        //        //else
        //        //{
        //        //    aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //        //    LoadSubSectionInformation();
        //        //}
        //    }
        //}
    }



    private bool CheckEmpDivisionWinAllocateOrNot(string SubSection)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EMPSubSectionGAllocatedOrNotEMP(SubSection);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    private bool CheckWingAllocateOrNot(string subSectionId)
    {
        bool status = false;

        //DataTable dataTable = aSectionInformationDal.SectionAllocatedOrNot(subSectionId);

        //if (dataTable.Rows.Count > 0)
        //{
        //    status = true;
        //}

        return status;
    }

    protected void sectionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata1 = asSectionInformationDal.GetSectionRelaton(sectionDropDownList.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                departmentNameDropdownList.Items.Clear();
                asSectionInformationDal.GetDepartmentByDivListAll(departmentNameDropdownList, divisionDropDownList.SelectedValue);
                departmentNameDropdownList.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                departmentNameDropdownList.Items.Clear();
                asSectionInformationDal.GetDepartmentByDivList(departmentNameDropdownList, divisionDropDownList.SelectedValue);
                departmentNameDropdownList.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
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