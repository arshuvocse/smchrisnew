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

public partial class MasterSetup_UI_EmployeeTypeEntry : System.Web.UI.Page
{
    EmployeeTypeEntryDAL aVaencyEntryDaL = new EmployeeTypeEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            aVaencyEntryDaL.GetCompanyListShortNameIntoDropdown(companyDropDownList);
            SetCheckBox();
            ButtonVisible();


            if (Session["VacancyCirculationId"] != null)
            {
                GetOneRecord(Session["VacancyCirculationId"].ToString());
                Session["VacancyCirculationId"] = null;
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
            Response.Redirect("EmployeeTypeView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("EmpTypeId").ToString(CultureInfo.InvariantCulture);


            VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("EmpType");
            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<int>("CompanyID").ToString();
         

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
        

        if (VacancyNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter This!!", this);
            VacancyNameTextBox.Focus();
            return false;
        }

        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select this!!", this);
            companyDropDownList.Focus();
            return false;
        }

     

        //if (String.IsNullOrEmpty(myTextBox.Text))
        //{
        //    //Tell the user that the text provided is unacceptable.
        //    aShowMessage.ShowMessageBox("The content of the Textbox is not valid.",this);
        //    //Validation was unsuccessful.
        //    return false;
        //}
       
      

        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (VacancyIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='EmployeeTypeView.aspx';",
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
    private bool UpdateAreaInformation(EmployeeTypeEntryDAO prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aVaencyEntryDaL.UpdateVacancyEntryInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private EmployeeTypeEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new EmployeeTypeEntryDAO();

        aVacancyEntryDao.EmpTypeId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.EmpType = VacancyNameTextBox.Text.Trim();
        aVacancyEntryDao.CompanyID = Convert.ToInt32(companyDropDownList.SelectedValue);

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;

        aVacancyEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aVacancyEntryDao.UpdateDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    private Int32 SaveVacancyEntry()
    {
        Int32 retVal;
        try
        {
            retVal = aVaencyEntryDaL.SaveVacancyEntryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private EmployeeTypeEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new EmployeeTypeEntryDAO();



        aVacancyEntryDao.EmpType = VacancyNameTextBox.Text.Trim();
        aVacancyEntryDao.CompanyID = Convert.ToInt32(companyDropDownList.SelectedValue);

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;

        
        aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        aVacancyEntryDao.EntryDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
       
        VacancyIdHiddenField.Value = "";
        
        VacancyNameTextBox.Text = "";
      
        submitButton.Text = "Save";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeTypeView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (VacancyIdHiddenField.Value != "")
        {
            try
            {
                bool area = UpdateAreaInformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Updated Successfully...');window.location ='EmployeeTypeView.aspx';",
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

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (VacancyIdHiddenField.Value != string.Empty)
        {
            Delete();
        }
    }

    private void Delete()
    {
        EmployeeTypeEntryDAO aVacancyEntryDao = new EmployeeTypeEntryDAO();


        aVacancyEntryDao.EmpTypeId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.EmpType = VacancyNameTextBox.Text.Trim();
        aVacancyEntryDao.CompanyID = Convert.ToInt32(companyDropDownList.SelectedValue);
        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;
        aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        aVacancyEntryDao.EntryDate = DateTime.Now;
    
        aVaencyEntryDaL.DelSaveVacancyEntryInfo(aVacancyEntryDao);
       
          aVaencyEntryDaL.DeleteEntryfoById(VacancyIdHiddenField.Value);
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Deleted Successfully...');window.location ='EmployeeTypeView.aspx';",
                          true);
         
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}