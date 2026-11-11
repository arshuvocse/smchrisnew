using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_DivisionInformation : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    DivisionInformationDal aDivisionInformationDal = new DivisionInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            //if (Session["UserId"] != null)
            //{
            //    const int manuId = 10;
            //    aOperationCredentials.MnageUserOperation("VIEW",Session["UserId"].ToString(),manuId, this);
            //}

            //LoadDropDownList();
            SetCheckBox();
            LoadDropDownList();
            if (Session["divisionId"] != null)
            {
                GetOneRecord(Session["divisionId"].ToString());
                Session["divisionId"] = null;
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

            Response.Redirect("DivisionInformationView.aspx");
        }

    }


    private void GetOneRecord(string divisionId)
    {
        DataTable dataTable = aDivisionInformationDal.GetDivisionInformationById(divisionId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            divisionHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

            DivisionNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("DivisionName");
            shortNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("ShortName");


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

            descriptionTexBox.Text = dataTable.Rows[rowIndex].Field<string>("Description");
            remarksTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Remarks");

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

    private void LoadDropDownList()
    {
        aDivisionInformationDal.GetCompanyListIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
    }

    private bool Validation()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VCompany, this);
            return false;
        }

        if (DivisionNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VDivision, this);
            return false;
        }

        if (shortNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VShortName, this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (divisionHiddenField.Value == "")
            {
                try
                {
                    Int32 divisionId = SaveDivisionInformation();

                    if (divisionId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='DivisionInformationView.aspx';",
                     true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (divisionHiddenField.Value != "")
            {
                try
                {
                    bool division = UpdateDivisionInformation();

                    if (division)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }

    private bool UpdateDivisionInformation()
    {
        bool retVal;
        try
        {
            retVal = aDivisionInformationDal.UpdateDivisionInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private DivisionInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new DivisionInformationDao();

        aInformationDao.DivisionId = Convert.ToInt32(divisionHiddenField.Value);
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aInformationDao.DivisionName = DivisionNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveDivisionInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aDivisionInformationDal.SaveDivisionInfo(PrepareDataForSave());

        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 SaveDivisionInformationForDel()
    {
        Int32 retVal;
        try
        {
            retVal = aDivisionInformationDal.SaveDivisionInfoForDele(PrepareDataForSaveDel());

        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }


    private DivisionInformationDao PrepareDataForSave()
    {
        var aInformationDao = new DivisionInformationDao();

        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aInformationDao.DivisionName = DivisionNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }

    private DivisionInformationDao PrepareDataForSaveDel()
    {
        var aInformationDao = new DivisionInformationDao();
        aInformationDao.DivisionId = Convert.ToInt32(divisionHiddenField.Value);
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aInformationDao.DivisionName = DivisionNameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        companyDropDownList.SelectedValue = "";
        DivisionNameTextBox.Text = "";
        shortNameTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DivisionInformationView.aspx");
    }
    protected void ListViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DivisionInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            
            if (divisionHiddenField.Value != "")
            {
                try
                {
                    if (!CheckDivisionAllocateOrNot(divisionHiddenField.Value))
                    {

                        if (!CheckEmpDivisionInAllocateOrNot(divisionHiddenField.Value))
                        {
                            bool division = UpdateDivisionInformation();

                            if (division)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Operation Successfull Done...');window.location ='DivisionInformationView.aspx';",
                                    true);
                                Clear();
                            }
                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Can not be Deleted! Already Defined in Employee Information Department...');window.location ='DepartmentInformationView.aspx';",
                                true);

                        }
                    }


                  
              

            else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can not be Deleted! Already Defined in Division Wing...');window.location ='DepartmentInformationView.aspx';",
                            true);

                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }
    private bool CheckDivisionAllocateOrNot(string divisionId)
    {
        bool status = false;

        DataTable dataTable = aDivisionInformationDal.DivisionAllocatedOrNot(divisionId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
 
        if (!CheckDivisionAllocateOrNot(divisionHiddenField.Value))
            {

                if (!CheckEmpDivisionInAllocateOrNot(divisionHiddenField.Value))
                        {
                if (aDivisionInformationDal.DeleteDivisionInfoById(divisionHiddenField.Value))
                {
                    Int32 divisionId = SaveDivisionInformationForDel();

                    if (divisionId > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successfull Done...');window.location ='DivisionInformationView.aspx';",
                   true);
                    }
                  
                   
                    
                }
                        }


              else
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Can not be Deleted! Already Defined in Employee Information Department...');window.location ='DepartmentInformationView.aspx';",
                      true);

              }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Can not be Deleted! Already Defined in Division Wing...');window.location ='DepartmentInformationView.aspx';",
                      true);
                
            }
        
    }


    private bool CheckEmpDivisionInAllocateOrNot(string Division)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EMPDivisionAllocatedOrNotEMP(Division);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}