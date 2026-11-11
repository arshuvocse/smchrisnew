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

public partial class MasterSetup_UI_JobLocation : System.Web.UI.Page
{
    JobLocationDal aLocationDal = new JobLocationDal();
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

         

            if (Session["LocationId"] != null)
            {
                GetOneRecord(Session["LocationId"].ToString());
                Session["LocationId"] = null;
            }
            else
            {
                submitButton.Visible = true;
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
            Response.Redirect("JobLocationView.aspx");
        }

    }

    private void LoadDropDownList()
    {
        aLocationDal.LoadOfficeList(regionDropDownList);
    }

    private void GetOneRecord(string locationId)
    {
        DataTable dataTable = aLocationDal.GetJobLocationInformationById(locationId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            locationIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("JobLocationID").ToString(CultureInfo.InvariantCulture);
            regionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("SalaryLoationId").ToString(CultureInfo.InvariantCulture);

            //if (regionDropDownList.SelectedValue != "")
            //{
            //    aLocationDal.LoadAreaList(areaDropDownList, regionDropDownList.SelectedValue);
            //    areaDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("AreaId").ToString(CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    areaDropDownList.Items.Clear();
            //}

            locationHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("Location");
            jobLocationTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Location");

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

    private bool Validation()
    {
        if (regionDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VRegion, this);
            return false;
        }

        //if (areaDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VArea, this);
        //    return false;
        //}

        if (jobLocationTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VJobLocation, this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (locationIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveJobLocationInformation();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='JobLocationView.aspx';",
                    true);
                        
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (locationIdHiddenField.Value != "")
            {
                try
                {
                    bool area = UpdateRegionInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successfull Done...');window.location ='JobLocationView.aspx';",
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

    private bool UpdateRegionInformation(JobLocationDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aLocationDal.UpdateAreaInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private JobLocationDao PrepareDataForUpdate()
    {
        var aInformationDao = new JobLocationDao();

        aInformationDao.JobLocationID = Convert.ToInt32(locationIdHiddenField.Value);
        aInformationDao.AreaId = Convert.ToInt32(regionDropDownList.SelectedValue);
        aInformationDao.Location = jobLocationTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveJobLocationInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aLocationDal.SaveJobLocationInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private Int32 SaveJobLocationInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aLocationDal.SaveJobLocationInfoDEL(PrepareDataForSaveDEL());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }


    private JobLocationDao PrepareDataForSave()
    {
        var aInformationDao = new JobLocationDao();

        aInformationDao.AreaId = Convert.ToInt32(regionDropDownList.SelectedValue);
        aInformationDao.Location = jobLocationTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.EntryBy = Session["LoginName"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }
    private JobLocationDao PrepareDataForSaveDEL()
    {
        var aInformationDao = new JobLocationDao();

        aInformationDao.JobLocationID = Convert.ToInt32(locationIdHiddenField.Value);
        aInformationDao.AreaId = Convert.ToInt32(regionDropDownList.SelectedValue);
        aInformationDao.Location = jobLocationTextBox.Text.Trim();
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
        regionDropDownList.SelectedValue = "";
        locationHiddenField.Value = "";
        locationIdHiddenField.Value = "";
        jobLocationTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";
    //    areaDropDownList.Items.Clear();
        SetCheckBox();
    }


    private bool CheckLocationExistOrNot(string location, string officelocation)
    {
        bool status = false;

        DataTable dataTable = aLocationDal.CheckLocationExistOrNot(location, officelocation);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void regionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (regionDropDownList.SelectedValue != "")
        //{
        //    aLocationDal.LoadAreaList(areaDropDownList, regionDropDownList.SelectedValue);
        //}
        //else
        //{
        //    areaDropDownList.Items.Clear();
        //}
    }

    protected void jobLocationTextBox_OnTextChanged(object sender, EventArgs e)
    {

        if (regionDropDownList.SelectedValue!="")
        {
            if (locationHiddenField.Value != "")
            {
                if (jobLocationTextBox.Text != locationHiddenField.Value)
                {
                    if (CheckLocationExistOrNot(jobLocationTextBox.Text.Trim(), regionDropDownList.SelectedValue))
                    {
                       // aShowMessage.ShowMessageBox(aMessages.LocationExistMessage, this);
                        jobLocationTextBox.Text = locationHiddenField.Value;
                    }
                }
            }
            else
            {
                if (CheckLocationExistOrNot(jobLocationTextBox.Text.Trim(), regionDropDownList.SelectedValue))
                {
                    aShowMessage.ShowMessageBox(aMessages.LocationExistMessage, this);
                    jobLocationTextBox.Text = "";
                }
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Office Location",this);
        }
      
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobLocationView.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            

            if (locationIdHiddenField.Value != "")
            {
                try
                {
                    bool area = UpdateRegionInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successfull Done...');window.location ='JobLocationView.aspx';",
                   true);
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

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (!CheckAreaAllocateOrNot(locationIdHiddenField.Value))
        {
            if (aLocationDal.DeleteJobLocationInfoById(locationIdHiddenField.Value))
            {

                Int32 areaId = SaveJobLocationInformationDEL();

                if (areaId > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfull Done...');window.location ='JobLocationView.aspx';",
                        true);
                }

            }
        }
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }


    private bool CheckAreaAllocateOrNot(string areaId)
    {
        bool status = false;

        //DataTable dataTable = aInformationDal.AreaAllocatedOrNot(areaId);

        //if (dataTable.Rows.Count > 0)
        //{
        //    status = true;
        //}

        return status;
    }
}