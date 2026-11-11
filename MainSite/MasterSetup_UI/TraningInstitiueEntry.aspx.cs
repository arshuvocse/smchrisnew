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

public partial class MasterSetup_UI_TraningInstitiueEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    TraningInstitiueEntryDAL aEntryDaL = new TraningInstitiueEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            
            ButtonVisible();


            if (Session["MId"] != null)
            {
                GetOneRecord(Session["MId"].ToString());
                Session["MId"] = null;
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
            Response.Redirect("TraningInstitiueView.aspx");
        }

    }

    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            IdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("InstitutionID").ToString(CultureInfo.InvariantCulture);


            NameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Description");
         

          

          

            
        }
    }
 
    private bool Validation()
    {


        if (NameTextBox.Text == "")
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
            if (IdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='TraningInstitiueView.aspx';",
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
    private bool Updateformation(TraningInstitiueEntryDAO prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aEntryDaL.UpdateEntryInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private TraningInstitiueEntryDAO PrepareDataForUpdate()
    {
        var aEntryDao = new TraningInstitiueEntryDAO();

        aEntryDao.InstitutionID = Convert.ToInt32(IdHiddenField.Value);

        aEntryDao.Description = NameTextBox.Text.Trim();



        aEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aEntryDao.UpdateDate = DateTime.Now;

        return aEntryDao;
    }
    private Int32 SaveVacancyEntry()
    {
        Int32 retVal;
        try
        {
            retVal = aEntryDaL.SaveEntryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private TraningInstitiueEntryDAO PrepareDataForSave()
    {
        var EntryDao = new TraningInstitiueEntryDAO();



        EntryDao.Description =  NameTextBox.Text.Trim();

    


        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
       
         IdHiddenField.Value = "";
        
        NameTextBox.Text = "";
      
       
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TraningInstitiueView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if ( IdHiddenField.Value != "")
        {
            try
            {
                 if (!CheckTrainingInstitutionAllocateOrNot(IdHiddenField.Value))
        {
                bool area = Updateformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Updated Successfully...');window.location ='TraningInstitiueView.aspx';",
                          true);
                }

        }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(),
                         "alert",
                         "alert('Can not be Updated! Already Defined in Employee Information Traning Institiue...');window.location ='TraningInstitiueView.aspx';",
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
        if (!CheckTrainingInstitutionAllocateOrNot(IdHiddenField.Value))
        {

            if (aEntryDaL.DeleteEntryfoById(IdHiddenField.Value))
            {
                Int32 departmentId = SaveInformationDEL();

                if (departmentId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfull Done...');window.location ='TraningInstitiueView.aspx';",
                        true);
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Can not be Deleted! Already Defined in Employee Information Traning Institiue...');window.location ='TraningInstitiueView.aspx';",
                true);

        }
    }

    private bool CheckTrainingInstitutionAllocateOrNot(string TrainingInstitution)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.TrainingInstitutionAllocatedOrNotEMP(TrainingInstitution);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private Int32 SaveInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aEntryDaL.SaveInfoDEL(PrepareDataForSaveDEL());

        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private TraningInstitiueEntryDAO PrepareDataForSaveDEL()
    {
        var EntryDao = new TraningInstitiueEntryDAO();



        EntryDao.Description =  NameTextBox.Text.Trim();
        EntryDao.InstitutionID = Convert.ToInt32( IdHiddenField.Value);




        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }
 

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}