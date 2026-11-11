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

public partial class MasterSetup_UI_OccupationEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    OccupationEntryDaL aEntryDaL = new OccupationEntryDaL();
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
            Response.Redirect("OccupationView.aspx");
        }

    }

    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            OccupationIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("OccupationID").ToString(CultureInfo.InvariantCulture);


            OccupationNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Description");
         

          

          

            
        }
    }
 
    private bool Validation()
    {


        if (OccupationNameTextBox.Text == "")
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
            if (OccupationIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='OccupationView.aspx';",
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
    private bool Updateformation(OccupationEntryDAO prepareDataForUpdate)
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
    private OccupationEntryDAO PrepareDataForUpdate()
    {
        var aEntryDao = new OccupationEntryDAO();

        aEntryDao.OccupationID = Convert.ToInt32(OccupationIdHiddenField.Value);

        aEntryDao.Description = OccupationNameTextBox.Text.Trim();



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
    private OccupationEntryDAO PrepareDataForSave()
    {
        var EntryDao = new OccupationEntryDAO();



        EntryDao.Description = OccupationNameTextBox.Text.Trim();

    


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
       
        OccupationIdHiddenField.Value = "";
        
        OccupationNameTextBox.Text = "";
      
       
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("OccupationView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (OccupationIdHiddenField.Value != "")
        {
            try
            {
                #region FatherOccupation
                if (!CheckFatherOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                {
                    #region MotherOccupation
                    if (!CheckMotherOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                    {
                        #region NomineeOccupation
                        if (!CheckNomineeOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                        {

                            #region RefOccupation
                            if (!CheckRefOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                            {
                                #region EmpSpouseOccupation
                                if (!CheckEmpSpouseOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                                {
                                    #region EmpChildrenOccupation
                                    if (!CheckEmpChildrenOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                                    {
                                        bool area = Updateformation(PrepareDataForUpdate());

                                        if (area)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                "alert",
                                                "alert('Data Updated Successfully...');window.location ='OccupationView.aspx';",
                                                true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "alert",
                                            "alert('Can not be Updated! Already Defined in Employee Information Emp Children's Occupation...');window.location ='OccupationView.aspx';",
                                            true);

                                    }
                                    #endregion

                                }
                               
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "alert",
                                        "alert('Can not be Updated! Already Defined in Employee Information Emp Spouse's Occupation...');window.location ='OccupationView.aspx';",
                                        true);

                                }
                                #endregion
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Can not be Updated! Already Defined in Employee Information Reference Occupation...');window.location ='OccupationView.aspx';",
                                    true);

                            }
                            #endregion

                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Can not be Updated! Already Defined in Employee Information Nominee's Occupation...');window.location ='OccupationView.aspx';",
                                true);

                        }
                        #endregion
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can not be Updated! Already Defined in Employee Information Mother's Occupation...');window.location ='OccupationView.aspx';",
                            true);

                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Updated! Already Defined in Employee Information Father's Occupation...');window.location ='OccupationView.aspx';",
                        true);

                }
                #endregion
            }

            catch
                (Exception ex)
            {
                aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                throw;
            }
        }
    }


    private bool CheckFatherOccupationAllocateOrNot(string FatherOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EmpFOccupationAllocatedOrNotEMP(FatherOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckMotherOccupationAllocateOrNot(string MotherOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EmpMOccupationAllocatedOrNotEMP(MotherOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckNomineeOccupationAllocateOrNot(string NomineeOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.NomNomineeOccupationAllocatedOrNotEMP(NomineeOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckRefOccupationAllocateOrNot(string RefOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.RefOccupationAllocatedOrNotEMP(RefOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckEmpSpouseOccupationAllocateOrNot(string EmpSpouseOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EmpSpouseOccupationAllocatedOrNotEMP(EmpSpouseOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    private bool CheckEmpChildrenOccupationAllocateOrNot(string EmpChildrenOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EmpChildrenOccupationAllocatedOrNotEMP(EmpChildrenOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    protected void delButton_OnClick(object sender, EventArgs e)
    {
        

         #region FatherOccupation
                if (!CheckFatherOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                {
                    #region MotherOccupation
                    if (!CheckMotherOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                    {
                        #region NomineeOccupation
                        if (!CheckNomineeOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                        {

                            #region RefOccupation
                            if (!CheckRefOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                            {
                                #region EmpSpouseOccupation
                                if (!CheckEmpSpouseOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                                {
                                    #region EmpChildrenOccupation

                                    if (!CheckEmpChildrenOccupationAllocateOrNot(OccupationIdHiddenField.Value))
                                    {

                                        if (aEntryDaL.DeleteEntryfoById(OccupationIdHiddenField.Value))
                                        {
                                            Int32 departmentId = SaveInformationDEL();

                                            if (departmentId > 0)
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                    "alert",
                                                    "alert('Operation Successfull Done...');window.location ='OccupationView.aspx';",
                                                    true);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "alert",
                                            "alert('Can not be Deleted! Already Defined in Employee Information Emp Children's Occupation...');window.location ='OccupationView.aspx';",
                                            true);

                                    }

                                    #endregion

                                }
                               
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "alert",
                                        "alert('Can not be Deleted! Already Defined in Employee Information Emp Spouse's Occupation...');window.location ='OccupationView.aspx';",
                                        true);

                                }
                                #endregion
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Can not be Deleted! Already Defined in Employee Information Reference Occupation...');window.location ='OccupationView.aspx';",
                                    true);

                            }
                            #endregion

                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Can not be Deleted! Already Defined in Employee Information Nominee's Occupation...');window.location ='OccupationView.aspx';",
                                true);

                        }
                        #endregion
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can not be Deleted! Already Defined in Employee Information Mother's Occupation...');window.location ='OccupationView.aspx';",
                            true);

                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Deleted! Already Defined in Employee Information Father's Occupation...');window.location ='OccupationView.aspx';",
                        true);

                }
                #endregion
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
    private OccupationEntryDAO PrepareDataForSaveDEL()
    {
        var EntryDao = new OccupationEntryDAO();



        EntryDao.Description = OccupationNameTextBox.Text.Trim();
        EntryDao.OccupationID = Convert.ToInt32(OccupationIdHiddenField.Value);




        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }
 

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}