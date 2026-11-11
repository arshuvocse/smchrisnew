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

public partial class MasterSetup_UI_EducationSubjectGroupEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();
    EducationSubjectGroupEntryDAL aEntryDaL = new EducationSubjectGroupEntryDAL();
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

            Response.Redirect("EducationSubjectGroupView.aspx");
        }

    }

    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            IdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("EducationSubjectGroupID").ToString(CultureInfo.InvariantCulture);


            CodeTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Code");
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
                    Int32 areaId = SaveEntry();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='EducationSubjectGroupView.aspx';",
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
    private bool Updateformation(EducationSubjectGroupEntryDAO prepareDataForUpdate)
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
    private EducationSubjectGroupEntryDAO PrepareDataForUpdate()
    {
        var aEntryDao = new EducationSubjectGroupEntryDAO();

        aEntryDao.EducationSubjectGroupID = Convert.ToInt32(IdHiddenField.Value);
        aEntryDao.Description = NameTextBox.Text.Trim();
        aEntryDao.Code = CodeTextBox.Text.Trim();

        aEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aEntryDao.UpdateDate = DateTime.Now;

        return aEntryDao;
    }
    private Int32 SaveEntry()
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
    private EducationSubjectGroupEntryDAO PrepareDataForSave()
    {
        var EntryDao = new EducationSubjectGroupEntryDAO();



        EntryDao.Description = NameTextBox.Text.Trim();
        EntryDao.Code = CodeTextBox.Text.Trim();

    


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
        Response.Redirect("EducationSubjectGroupView.aspx");
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
                if (!CheckPermanentDivisionAllocateOrNot(IdHiddenField.Value))
                {
                    

                        bool area = Updateformation(PrepareDataForUpdate());

                        if (area)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Data Updated Successfully...');window.location ='EducationSubjectGroupView.aspx';",
                                true);
                        }
                    
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Can not be Updated! Already Defined in Employee Information...');window.location ='EducationSubjectGroupView.aspx';",
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
        if (!CheckPermanentDivisionAllocateOrNot(IdHiddenField.Value))
        {
            
                if (aEntryDaL.DeleteEntryfoById(IdHiddenField.Value))
                {
                    Int32 departmentId = SaveInformationDEL();

                    if (departmentId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='EducationSubjectGroupView.aspx';",
                            true);
                    }

                }
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Deleted! Already Defined in Employee Information...');window.location ='EducationSubjectGroupView.aspx';",
                        true);

        }
    }

    private bool CheckPermanentDivisionAllocateOrNot(string ParmanentDivisionId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.AllocatedOrNotEMP(ParmanentDivisionId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckPresentDivisionAllocateOrNot(string PresentDivisionId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.PresentDivisionAllocatedOrNotEMP(PresentDivisionId);

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
    private EducationSubjectGroupEntryDAO PrepareDataForSaveDEL()
    {
        var EntryDao = new EducationSubjectGroupEntryDAO();



        EntryDao.Description = NameTextBox.Text.Trim();
        EntryDao.Code = CodeTextBox.Text.Trim();

        EntryDao.EducationSubjectGroupID = Convert.ToInt32( IdHiddenField.Value);




        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }
 

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}