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

public partial class MasterSetup_UI_TrainingTopicEntry : System.Web.UI.Page
{
    TrainingTopicEntryDaL aVaencyEntryDaL = new TrainingTopicEntryDaL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            SetCheckBox();
           
             
            loadDropDownList();

            if (Session["SuspendReasonEntryId"] != null)
            {
                GetOneRecord(Session["SuspendReasonEntryId"].ToString());
                Session["SuspendReasonEntryId"] = null;
            }
        }
    }

    private void loadDropDownList()
    {
        aVaencyEntryDaL.GetTrainingHeadingList(TrainingHeadingDropDownList);

           ddlTrainingSerial.Items.Insert(0, new ListItem("Select One.......", "0"));
        for (int i = 1; i < 100; i++)
        {
            ddlTrainingSerial.Items.Insert(i, new ListItem(i.ToString(), i.ToString()));
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
            Response.Redirect("TrainingTopicView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            TrainingTopicIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("TrainingTopicId").ToString(CultureInfo.InvariantCulture);


            TrainingTopicTextBox.Text = dataTable.Rows[rowIndex].Field<string>("TrainingTopic");
            TrainingHeadingDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<int>("TraingingHeadingId").ToString();
         

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
            try
            {
                ddlTrainingSerial.SelectedValue = dataTable.Rows[rowIndex].Field<int>("TraingingSerial").ToString();
            }
            catch (Exception)
            {
                ddlTrainingSerial.SelectedValue = 0.ToString();
                //throw;
            }
            ddlTrainingSerial.Enabled = false;
           

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
        

        if (TrainingTopicTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            TrainingTopicTextBox.Focus();
            return false;
        }


        if (TrainingHeadingDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            TrainingHeadingDropDownList.Focus();
            return false;
        }



  
      
      

        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (!CheckAchievementsAllocateOrNot(Convert.ToInt32(ddlTrainingSerial.SelectedValue), Convert.ToInt32(TrainingHeadingDropDownList.SelectedValue)))
            {
            if (TrainingTopicIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                         "alert",
                         "alert('Data Saved Successfully...');window.location ='TrainingTopicView.aspx';",
                         true);
                         
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }
            }
            else
            {
                aShowMessage.ShowMessageBox("Trainging Sorting Order Already Exist!!! ", this);
            }

           
        }
    }
    private bool UpdateAreaInformation(TrainingTopicEntryDAO prepareDataForUpdate)
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
    private TrainingTopicEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new TrainingTopicEntryDAO();

        aVacancyEntryDao.TrainingTopicId = Convert.ToInt32(TrainingTopicIdHiddenField.Value);
        aVacancyEntryDao.TraingingSerial = Convert.ToInt32(ddlTrainingSerial.SelectedItem.Text.Trim());
        aVacancyEntryDao.TrainingTopic = TrainingTopicTextBox.Text.Trim();
        aVacancyEntryDao.TraingingHeadingId = Convert.ToInt32(TrainingHeadingDropDownList.SelectedValue);
         

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

    private bool CheckAchievementsAllocateOrNot(int MainID, int HId)
    {

        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblTrainingSetupTopics
                      where t.TraingingSerial == MainID && t.TraingingHeadingId == HId && (t.IsDelete == false || t.IsDelete == null)
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }
    private TrainingTopicEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new TrainingTopicEntryDAO();



        aVacancyEntryDao.TrainingTopic = TrainingTopicTextBox.Text.Trim();
        aVacancyEntryDao.TraingingSerial = Convert.ToInt32(ddlTrainingSerial.SelectedItem.Text.Trim());

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;
      


        aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);
        aVacancyEntryDao.TraingingHeadingId = Convert.ToInt32(TrainingHeadingDropDownList.SelectedValue);


        aVacancyEntryDao.EntryDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
       
        TrainingTopicIdHiddenField.Value = "";

        TrainingTopicTextBox.Text = "";
        TrainingHeadingDropDownList.SelectedValue = "";
        submitButton.Text = "Save";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TrainingTopicView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (TrainingTopicIdHiddenField.Value != "")
        {
            try
            {
                bool area = UpdateAreaInformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "alert",
                              "alert('Data Updated Successfully...');window.location ='TrainingTopicView.aspx';",
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
        if (TrainingTopicIdHiddenField.Value != string.Empty)
        {
            Delete();
        }
    }

    private void Delete()
    {
        TrainingTopicEntryDAO aVacancyEntryDao = new TrainingTopicEntryDAO();


        aVacancyEntryDao.TrainingTopicId = Convert.ToInt32(TrainingTopicIdHiddenField.Value);

        aVacancyEntryDao.IsDelete = true;


        aVacancyEntryDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aVacancyEntryDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aVaencyEntryDaL.DeleteVacancyEntryfoById(aVacancyEntryDao);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='TrainingTopicView.aspx';",
              true);
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}