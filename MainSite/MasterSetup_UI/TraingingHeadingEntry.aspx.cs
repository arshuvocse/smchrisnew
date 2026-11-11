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

public partial class MasterSetup_UI_TraingingHeadingEntry : System.Web.UI.Page
{
    private TraingingHeadingEntryDAL aVaencyEntryDaL = new TraingingHeadingEntryDAL();
    private ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    private ShowMessage aShowMessage = new ShowMessage();
    private Messages aMessages = new Messages();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDowninf();
            SetCheckBox();
            ButtonVisible();


            if (Session["VacancyCirculationId"] != null)
            {
                GetOneRecord(Session["VacancyCirculationId"].ToString());
                Session["VacancyCirculationId"] = null;
            }
        }
    }

    private void DropDowninf()
    {


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
            Response.Redirect("TraingingHeadingEntryView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("TraingingHeadingId").ToString(CultureInfo.InvariantCulture);



            VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("TraingingHeading");

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

        if (ddlTrainingSerial.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlTrainingSerial.Focus();
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



    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblTraingingHeadings
                      where t.TraingingSerial == MainID && (t.IsDelete == false || t.IsDelete == null)
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (!CheckAchievementsAllocateOrNot(Convert.ToInt32(ddlTrainingSerial.SelectedValue)))
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
                                "alert('Data Saved Successfully...');window.location ='TraingingHeadingEntryView.aspx';",
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
    private bool UpdateAreaInformation(TraingingHeadingEntryDAO prepareDataForUpdate)
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
    private TraingingHeadingEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new TraingingHeadingEntryDAO();

        aVacancyEntryDao.TraingingHeadingId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.TraingingHeading = VacancyNameTextBox.Text.Trim();

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;
        aVacancyEntryDao.TraingingSerial = Convert.ToInt32(ddlTrainingSerial.SelectedItem.Text.Trim());
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
    private TraingingHeadingEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new TraingingHeadingEntryDAO();



        aVacancyEntryDao.TraingingHeading = VacancyNameTextBox.Text.Trim();
        aVacancyEntryDao.TraingingSerial = Convert.ToInt32(ddlTrainingSerial.SelectedItem.Text.Trim());

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
        Response.Redirect("TraingingHeadingEntryView.aspx");
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
                            "alert('Data Updated Successfully...');window.location ='TraingingHeadingEntryView.aspx';",
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
        TraingingHeadingEntryDAO aVacancyEntryDao = new TraingingHeadingEntryDAO();


        aVacancyEntryDao.TraingingHeadingId = Convert.ToInt32(VacancyIdHiddenField.Value);

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
                          "alert('Data Deleted Successfully...');window.location ='TraingingHeadingEntryView.aspx';",
                          true);
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}