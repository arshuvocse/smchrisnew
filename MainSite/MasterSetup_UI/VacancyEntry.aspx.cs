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

public partial class MasterSetup_UI_VacancyEntry : System.Web.UI.Page
{
    VaencyEntryDaL aVaencyEntryDaL = new VaencyEntryDaL();
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
            Response.Redirect("VacancyEntryView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("VacancyCirculationId").ToString(CultureInfo.InvariantCulture);


            VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("CirculationWay");
         

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
                          "alert('Data Saved Successfully...');window.location ='VacancyEntryView.aspx';",
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
    private bool UpdateAreaInformation(VacancyEntryDao prepareDataForUpdate)
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
    private VacancyEntryDao PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new VacancyEntryDao();

        aVacancyEntryDao.VacancyCirculationId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.CirculationWay = VacancyNameTextBox.Text.Trim();

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
    private VacancyEntryDao PrepareDataForSave()
    {
        var aVacancyEntryDao = new VacancyEntryDao();



        aVacancyEntryDao.CirculationWay = VacancyNameTextBox.Text.Trim();

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
        Response.Redirect("VacancyEntryView.aspx");
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
                          "alert('Data Updated Successfully...');window.location ='VacancyEntryView.aspx';",
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
        VacancyEntryDao aVacancyEntryDao = new VacancyEntryDao();


        aVacancyEntryDao.VacancyCirculationId = Convert.ToInt32(VacancyIdHiddenField.Value);

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
                          "alert('Data Deleted Successfully...');window.location ='VacancyEntryView.aspx';",
                          true);
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}