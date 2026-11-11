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

public partial class MasterSetup_UI_DivisionWingInformation : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    DivisionWingInformationDal aDivisionInformationDal = new DivisionWingInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();
            SetCheckBox();



            if (Session["wingId"] != null)
            {
                GetOneRecord(Session["wingId"].ToString());
                Session["wingId"] = null;
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

            Response.Redirect("DivisionWingView.aspx");
        }

    }
    private void GetOneRecord(string wingId)
    {
        DataTable dataTable = aDivisionInformationDal.GetDivisionInformationById(wingId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            wingIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DivisionWId").ToString(CultureInfo.InvariantCulture);

            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

            if (companyDropDownList.SelectedValue != "")
            {
                aDivisionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
            }

            divisionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

            unitTextBox.Text = dataTable.Rows[rowIndex].Field<string>("DivisionWingName");
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
        companyDropDownList_OnSelectedIndexChanged(null, null);
        //aDivisionInformationDal.GetDivisionListIntoDropdown(divisionDropDownList);
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aDivisionInformationDal.GetDivisionList(divisionDropDownList, companyDropDownList.SelectedValue);
        }
    }

    private bool Validation()
    {
        //if (companyDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VCompany, this);
        //    return false;
        //}

        if (divisionDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VDivision, this);
            return false;
        }

        if (unitTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VDivisionWing, this);
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
            if (wingIdHiddenField.Value == "")
            {
                try
                {
                    Int32 divisionWingId = SaveDivisionWingInformation();

                    if (divisionWingId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='DivisionWingView.aspx';",
                     true);
                        Clear();
                    }
                }
                catch (Exception)
                {

                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);

                }
            }


            if (wingIdHiddenField.Value != "")
            {
                try
                {
                    bool divisionWing = UpdateDivisionWingInformation();

                    if (divisionWing)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception ex)
                {

                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                    throw;

                }
            }
        }
    }

    private bool UpdateDivisionWingInformation()
    {
        bool retVal;
        try
        {
            retVal = aDivisionInformationDal.UpdateDivisionWingInfo(PrepareDataForUpdate());

        }
        catch (Exception ex)
        {
            retVal = false;
        }

        return retVal;
    }

    private DivisionWingInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new DivisionWingInformationDao();

        aInformationDao.DivisionWId = Convert.ToInt32(wingIdHiddenField.Value);
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = unitTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveDivisionWingInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aDivisionInformationDal.SaveDivisionWingInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 SaveDivisionWingInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aDivisionInformationDal.SaveDivisionWingInfoDEL(PrepareDataForSaveDEL());

        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }


    private DivisionWingInformationDao PrepareDataForSave()
    {
        var aInformationDao = new DivisionWingInformationDao();
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = unitTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }


    private DivisionWingInformationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new DivisionWingInformationDao();
        aInformationDao.DivisionWId = Convert.ToInt32(wingIdHiddenField.Value);
        aInformationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        aInformationDao.DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue);
        aInformationDao.DivisionWingName = unitTextBox.Text.Trim();
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
        //companyDropDownList.SelectedValue = "";
        divisionDropDownList.SelectedValue = "";
        unitTextBox.Text = "";
        shortNameTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";

        submitButton.Text = "Save";

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DivisionWingView.aspx");
    }


    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            

            if (wingIdHiddenField.Value != "")
            {
                try
                {

                     if (!CheckWingAllocateOrNot(wingIdHiddenField.Value))
        {

            if (!CheckEmpDivisionWinAllocateOrNot(wingIdHiddenField.Value))
            {
                    bool divisionWing = UpdateDivisionWingInformation();

                    if (divisionWing)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfull Done...');window.location ='DivisionWingView.aspx';",
                       true);
                        Clear();
                    }

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Can not be Updated! Already Defined in Employee Information Division Wing...');window.location ='DivisionWingView.aspx';",
                    true);

            }
        }
                     else
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(),
                                   "alert",
                                   "alert('Can not be Updated! Already Defined in Department Information...');window.location ='DivisionWingView.aspx';",
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
    }
    private bool CheckWingAllocateOrNot(string wingId)
    {
        bool status = false;

        DataTable dataTable = aDivisionInformationDal.WingAllocatedOrNot(wingId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {

        if (!CheckWingAllocateOrNot(wingIdHiddenField.Value))
        {

            if (!CheckEmpDivisionWinAllocateOrNot(wingIdHiddenField.Value))
            {
                if (aDivisionInformationDal.DeleteDivisionWingInfoById(wingIdHiddenField.Value))
                {
                    Int32 divisionWingId = SaveDivisionWingInformationDEL();

                    if (divisionWingId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='DivisionWingView.aspx';",
                            true);

                    }

                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Can not be Deleted! Already Defined in Employee Information Division Wing...');window.location ='DivisionWingView.aspx';",
                    true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Can not be Deleted! Already Defined in Department Information...');window.location ='DivisionWingView.aspx';",
                      true);

        }
        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string wingId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (!CheckWingAllocateOrNot(wingId))
        //    {
        //        if (aDivisionInformationDal.DeleteDivisionWingInfoById(wingId))
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //            LoadDivisionWingInformation();
        //        }
        //    }
        //    else
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //        LoadDivisionWingInformation();
        //    }

        //}
    }

    private bool CheckEmpDivisionWinAllocateOrNot(string DivisionWin)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.EMPDivisionWinGAllocatedOrNotEMP(DivisionWin);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
}