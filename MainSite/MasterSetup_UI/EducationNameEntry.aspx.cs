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

public partial class MasterSetup_UI_EducationNameEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    EducationNameEntryDAL aEntryDaL = new EducationNameEntryDAL();
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

            Response.Redirect("EducationNameView.aspx");
        }

    }

    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            IdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("EducationNameID").ToString(CultureInfo.InvariantCulture);


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
                          "alert('Data Saved Successfully...');window.location ='EducationNameView.aspx';",
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
    private bool Updateformation(EducationNameEntryDAO prepareDataForUpdate)
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
    private EducationNameEntryDAO PrepareDataForUpdate()
    {
        var aEntryDao = new EducationNameEntryDAO();

        aEntryDao.EducationNameID = Convert.ToInt32(IdHiddenField.Value);

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
    private EducationNameEntryDAO PrepareDataForSave()
    {
        var EntryDao = new EducationNameEntryDAO();



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
        Response.Redirect("EducationNameView.aspx");
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
                if (!CheckEducationNameAllocateOrNot(IdHiddenField.Value))
                {
                    if (!CheckEmpSpouseMaxEduAllocateOrNot(IdHiddenField.Value))
                    {
                bool area = Updateformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Updated Successfully...');window.location ='EducationNameView.aspx';",
                          true);
                }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can not be Updated! Already Defined in Employee Information...');window.location ='EducationNameView.aspx';",
                            true);

                    }
                }
                  else
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Can not be Updated! Already Defined in Employee Information...');window.location ='EducationNameView.aspx';",
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


    private bool CheckEducationNameAllocateOrNot(string EducationName)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EducationNameAllocatedOrNotEMP(EducationName);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckEmpSpouseMaxEduAllocateOrNot(string EmpSpouseMax)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EmpSpouseMaxEduAllocatedOrNotEMP(EmpSpouseMax);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        
         if (!CheckEducationNameAllocateOrNot(IdHiddenField.Value))
                {
                    if (!CheckEmpSpouseMaxEduAllocateOrNot(IdHiddenField.Value))
                    {
                        Int32 departmentId = SaveInformationDEL();
        if (aEntryDaL.DeleteEntryfoById( IdHiddenField.Value))
        {
           

            if (departmentId > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='EducationNameView.aspx';",
                    true);
            }

        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can not be Deleted! Already Defined in Employee Information...');window.location ='EducationNameView.aspx';",
                            true);

                    }
                }
         else
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Can not be Deleted! Already Defined in Employee Information...');window.location ='EducationNameView.aspx';",
                 true);

         }
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
    private EducationNameEntryDAO PrepareDataForSaveDEL()
    {
        var EntryDao = new EducationNameEntryDAO();



        EntryDao.Description =  NameTextBox.Text.Trim();
        EntryDao.EducationNameID = Convert.ToInt32( IdHiddenField.Value);




        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }
 

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}