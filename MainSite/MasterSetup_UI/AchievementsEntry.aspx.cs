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

public partial class MasterSetup_UI_AchievementsEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    AchievementsEntryDAL aVaencyEntryDaL = new AchievementsEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
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
            Response.Redirect("AchievementsView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("MasterAchievementsId").ToString(CultureInfo.InvariantCulture);


            VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("AchievementsName");
         

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
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            VacancyNameTextBox.Focus();
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
                          "alert('Data Saved Successfully...');window.location ='AchievementsView.aspx';",
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
    private bool UpdateAreaInformation(AchievementsEntryDAO prepareDataForUpdate)
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
    private AchievementsEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new AchievementsEntryDAO();

        aVacancyEntryDao.MasterAchievementsId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.AchievementsName = VacancyNameTextBox.Text.Trim();

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
    private AchievementsEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new AchievementsEntryDAO();



        aVacancyEntryDao.AchievementsName = VacancyNameTextBox.Text.Trim();

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
        Response.Redirect("AchievementsView.aspx");
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
                if (!CheckAchievementsAllocateOrNot(VacancyIdHiddenField.Value))
                {
                    bool area = UpdateAreaInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Data Updated Successfully...');window.location ='AchievementsView.aspx';",
                            true);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Updated! Already Defined in Employee Information Achievements...');window.location ='AchievementsView.aspx';",
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
        AchievementsEntryDAO aVacancyEntryDao = new AchievementsEntryDAO();


        if (!CheckAchievementsAllocateOrNot(VacancyIdHiddenField.Value))
        {

            aVacancyEntryDao.MasterAchievementsId = Convert.ToInt32(VacancyIdHiddenField.Value);
            aVacancyEntryDao.AchievementsName = VacancyNameTextBox.Text.Trim();

            aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;


            aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


            aVacancyEntryDao.EntryDate = DateTime.Now;
            //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
            //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
            aVaencyEntryDaL.SaveInfoDEL(aVacancyEntryDao);
            if (aVaencyEntryDaL.DeleteEntryfoById(VacancyIdHiddenField.Value))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Data Deleted Successfully...');window.location ='AchievementsView.aspx';",
                    true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Can not be Deleted! Already Defined in Employee Information Achievements...');window.location ='AchievementsView.aspx';",
                true);

        }
    }

    private bool CheckAchievementsAllocateOrNot(string Achievements)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.AchievementsAllocatedOrNotEMP(Achievements);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}