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

public partial class Survey_surveyQuestionGroupEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    surveyQuestionGroupEntryDAL aVaencyEntryDaL = new surveyQuestionGroupEntryDAL();
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
            Response.Redirect("surveyQuestionGroupView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SurveyQuestionGroupId").ToString(CultureInfo.InvariantCulture);


            VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SurveyQuestionGroup");
         

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
                          "alert('Data Saved Successfully...');window.location ='surveyQuestionGroupView.aspx';",
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
    private bool UpdateAreaInformation(surveyQuestionGroupEntryDAO prepareDataForUpdate)
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
    private surveyQuestionGroupEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new surveyQuestionGroupEntryDAO();

        aVacancyEntryDao.SurveyQuestionGroupId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.SurveyQuestionGroup = VacancyNameTextBox.Text.Trim();

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
    private surveyQuestionGroupEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new surveyQuestionGroupEntryDAO();



        aVacancyEntryDao.SurveyQuestionGroup = VacancyNameTextBox.Text.Trim();

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
        Response.Redirect("surveyQuestionGroupView.aspx");
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
                if (!CheckHobbyAllocateOrNot(VacancyIdHiddenField.Value))
                {
                    bool area = UpdateAreaInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Data Updated Successfully...');window.location ='surveyQuestionGroupView.aspx';",
                            true);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Updated! Already Defined in Survey Question Group...');window.location ='surveyQuestionGroupView.aspx';",
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
        surveyQuestionGroupEntryDAO aVacancyEntryDao = new surveyQuestionGroupEntryDAO();


        if (!CheckHobbyAllocateOrNot(VacancyIdHiddenField.Value))
        {

            aVacancyEntryDao.SurveyQuestionGroupId = Convert.ToInt32(VacancyIdHiddenField.Value);
            aVacancyEntryDao.SurveyQuestionGroup = VacancyNameTextBox.Text.Trim();
            aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;
            aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);
            aVacancyEntryDao.EntryDate = DateTime.Now;
            
            aVaencyEntryDaL.SaveInfoDEL(aVacancyEntryDao);
            if (aVaencyEntryDaL.DeleteEntryfoById(VacancyIdHiddenField.Value))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Data Deleted Successfully...');window.location ='surveyQuestionGroupView.aspx';",
                    true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Can not be Deleted! Already Defined in Survey Question Group...');window.location ='surveyQuestionGroupView.aspx';",
                true);

        }
    }

    private bool CheckHobbyAllocateOrNot(string hobbyid)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.SurveyQuestionGroupAllocatedOrNotEMP(hobbyid);

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