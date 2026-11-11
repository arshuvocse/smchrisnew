using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.MasterSetup_DAL;
using DAL.Report_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_KPIMIdyearStatusSetup : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();
    AppraisalSetupListDAL _AppraisalSetupListDAL = new AppraisalSetupListDAL();
    KPIMIdyearStatusSetupDAL aEntryDaL = new KPIMIdyearStatusSetupDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadDroDownList();
            ButtonVisible();

            RadioButtonList_SelectedIndexChanged(null, null);

            ReloadAllInfo();
            if (Session["MId"] != null)
            {
                GetOneRecord(Session["MId"].ToString());
                Session["MId"] = null;
            }
        }
    }

    private void ReloadAllInfo()
    {
        submitButton.Visible = false;
        editButton.Visible = false;
        if (companyDropDownList.SelectedIndex > 0 && financialYearDropDownList.SelectedIndex > 0)
        {
            DataTable dataTable = aEntryDaL.GetInformationById(companyDropDownList.SelectedValue, financialYearDropDownList.SelectedValue);
            if (dataTable.Rows.Count > 0)
            {
                editButton.Visible = true;

                string SelectedActionStatus = dataTable.Rows[0].Field<string>("SelectedActionStatus");

                if (SelectedActionStatus == "Ongoing")
                {
                    RadioButtonList.Items[0].Selected = true;
                }
                else
                {
                    RadioButtonList.Items[1].Selected = true;
                }

                RadioButtonList_SelectedIndexChanged(null, null);
                OccupationIdHiddenField.Value = dataTable.Rows[0].Field<Int32>("MidYearKPISetupId").ToString(CultureInfo.InvariantCulture);

                //companyDropDownList.Enabled = false;
                //financialYearDropDownList.Enabled = false;
            }
            else
            {
                lblStatus.Text = "Not Started";
                RadioButtonList.ClearSelection();
                OccupationIdHiddenField.Value = "";
                submitButton.Visible = true;
            }
        }
        else
        {
            submitButton.Visible = true;
        }
    }

    public void ButtonVisible()
    {
      
        //if (Session["Status"] != null)
        //{
        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        submitButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        editButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        delButton.Visible = true;
        //    }
        //    Session["Status"] = null;
        //}
        //else
        //{
        //    Response.Redirect("KPIMIdyearStatusSetup.aspx");
        //}

    }

    private void LoadDroDownList()
    {
        _AppraisalSetupListDAL.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    private bool CheckStartEndDateExistOrNot(DateTime Start, DateTime End)
    {
        DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();

        bool status = false;
        string COMID = companyDropDownList.SelectedValue;

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(Start, End, COMID);

        if (dataTable.Rows.Count > 0)
        {
            financialYearDropDownList.SelectedValue = dataTable.Rows[0]["FinancialYearId"].ToString();
            status = true;
        }

        return status;
    }

    protected void RadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Update the label text based on selected option

        if (RadioButtonList.Items[0].Selected == true)
        {
            lblStatus.Text = "Ongoing";
        }
        else if (RadioButtonList.Items[1].Selected == true)
        {
            lblStatus.Text = "Stop";
        }
        else
        {
            lblStatus.Text = "Not Started";
        }

    }
    EmployeeContractualReportDAL aEmployeeJobLeftEntryDAL = new EmployeeContractualReportDAL();
    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(financialYearDropDownList, companyDropDownList.SelectedValue);


            if (DateTime.Now != null)
            {


                if (CheckStartEndDateExistOrNot(DateTime.Now, DateTime.Now) == true)
                {
               
                }

            }
            //if (ddlCompany.SelectedValue=="1")
            //{
            //    FinancialYearDropDownList.SelectedValue = "9";
            //}
            //else
            //{
            //    FinancialYearDropDownList.SelectedValue = "10";

            //}
        }
        else
        {
            financialYearDropDownList.Items.Clear();
        }


        ReloadAllInfo();

    }
    private void GetOneRecord(string id)
    {
        //DataTable dataTable = aEntryDaL.GetInformationById(id);

        //const int rowIndex = 0;

        //if (dataTable.Rows.Count > 0)
        //{
        //    OccupationIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("OccupationID").ToString(CultureInfo.InvariantCulture);


        //    OccupationNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Description");
         

          

          

            
        //}
    }

    public bool Validation()
    {
        bool isValid = true;
        if (companyDropDownList.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Company Required ", this);
            companyDropDownList.Focus();
            return false;
        }

        if (financialYearDropDownList.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial Year Required ", this);
            financialYearDropDownList.Focus();
            return false;
        }

        if (RadioButtonList.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Select Action Required ", this);
            RadioButtonList.Focus();
            return false;
        }

         




        return isValid;
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
                          "alert('Data Saved Successfully...');window.location ='KPIMIdyearStatusSetup.aspx';",
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
    private bool Updateformation(MidYearKPISetupDAO prepareDataForUpdate)
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
    private MidYearKPISetupDAO PrepareDataForUpdate()
    {
         
        var EntryDao = new MidYearKPISetupDAO();
        EntryDao.MidYearKPISetupId = Convert.ToInt32(OccupationIdHiddenField.Value);
        // Get the selected CompanyId
        EntryDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        // Get the selected Financial Year Id
        EntryDao.FinancialYearId = Convert.ToInt32(financialYearDropDownList.SelectedValue);

        // Get the selected action (Start/Stop)
        EntryDao.SelectedActionStatus = RadioButtonList.SelectedValue;

        // Get the status (if it's a numeric value or text, adjust accordingly)
        EntryDao.SelectedActionStatus = lblStatus.Text.Trim(); // Assuming the label holds some status text





        EntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        EntryDao.UpdateDate = DateTime.Now;

        return EntryDao;
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
    private MidYearKPISetupDAO PrepareDataForSave()
    {
        var EntryDao = new MidYearKPISetupDAO();

        // Get the selected CompanyId
        EntryDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        // Get the selected Financial Year Id
        EntryDao.FinancialYearId = Convert.ToInt32(financialYearDropDownList.SelectedValue);

        // Get the selected action (Start/Stop)
        EntryDao.SelectedActionStatus = RadioButtonList.SelectedValue;

        // Get the status (if it's a numeric value or text, adjust accordingly)
        EntryDao.SelectedActionStatus = lblStatus.Text.Trim(); // Assuming the label holds some status text

        // Capture EntryBy user from session
        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);

        // Capture Entry Date
        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }



    protected void cancelButton_OnClick(object sender, EventArgs e)
    {

        Response.Redirect("KPIMIdyearStatusSetup.aspx");
        
    }
    private void Clear()
    {
       
        OccupationIdHiddenField.Value = "";
        
      //  OccupationNameTextBox.Text = "";
      
       
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("KPIMIdyearStatusSetup.aspx");
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
                
                                        bool area = Updateformation(PrepareDataForUpdate());

                                        if (area)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                "alert",
                                                "alert('Data Updated Successfully...');window.location ='KPIMIdyearStatusSetup.aspx';",
                                                true);
                                        }
                                    
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Updated! Already Defined in Employee Information Father's Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
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
                                                    "alert('Operation Successfull Done...');window.location ='KPIMIdyearStatusSetup.aspx';",
                                                    true);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "alert",
                                            "alert('Can not be Deleted! Already Defined in Employee Information Emp Children's Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
                                            true);

                                    }

                                    #endregion

                                }
                               
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "alert",
                                        "alert('Can not be Deleted! Already Defined in Employee Information Emp Spouse's Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
                                        true);

                                }
                                #endregion
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Can not be Deleted! Already Defined in Employee Information Reference Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
                                    true);

                            }
                            #endregion

                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Can not be Deleted! Already Defined in Employee Information Nominee's Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
                                true);

                        }
                        #endregion
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can not be Deleted! Already Defined in Employee Information Mother's Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
                            true);

                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Can not be Deleted! Already Defined in Employee Information Father's Occupation...');window.location ='KPIMIdyearStatusSetup.aspx';",
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



      //  EntryDao.Description = OccupationNameTextBox.Text.Trim();
        EntryDao.OccupationID = Convert.ToInt32(OccupationIdHiddenField.Value);




        EntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        EntryDao.EntryDate = DateTime.Now;

        return EntryDao;
    }
 

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void financialYearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReloadAllInfo();
    }
}