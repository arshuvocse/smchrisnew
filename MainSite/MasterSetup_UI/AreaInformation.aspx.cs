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

public partial class MasterSetup_UI_AreaInformation : System.Web.UI.Page
{
    AreaInformationDal areaInformation = new AreaInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            SetCheckBox();

           

            if (Session["AreaId"] != null)
            {
                GetOneRecord(Session["AreaId"].ToString());
                Session["AreaId"] = null;
            }
        }
    }

    private void LoadDropDownList()
    {
        areaInformation.LoadRegionList(regionDropDownList);
    }
    private void GetOneRecord(string areaId)
    {
        DataTable dataTable = areaInformation.GetRegionInformationById(areaId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            areaHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("AreaId").ToString(CultureInfo.InvariantCulture);
            regionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("RegionId").ToString(CultureInfo.InvariantCulture);
            areaCodeHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("AreaCode");
            areaNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("AreaName");
            areaCodeTextBox.Text = dataTable.Rows[rowIndex].Field<string>("AreaCode");

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

        if (areaNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }

        if (areaCodeTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VAreaCode, this);
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
            if (areaHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveAreaInformation();

                    if (areaId > 0)
                    {
                        aShowMessage.ShowMessageBox(aMessages.SaveSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (areaHiddenField.Value != "")
            {
                try
                {
                    bool area = UpdateAreaInformation(PrepareDataForUpdate());

                    if (area)
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
    private bool UpdateAreaInformation(AreaInformationDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = areaInformation.UpdateAreaInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private AreaInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new AreaInformationDao();

        aInformationDao.AreaId = Convert.ToInt32(areaHiddenField.Value);
        aInformationDao.RegionId = Convert.ToInt32(regionDropDownList.SelectedValue);
        aInformationDao.AreaName = areaNameTextBox.Text.Trim();
        aInformationDao.AreaCode = areaCodeTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }
    private Int32 SaveAreaInformation()
    {
        Int32 retVal;
        try
        {
            retVal = areaInformation.SaveAreaInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private AreaInformationDao PrepareDataForSave()
    {
        var aInformationDao = new AreaInformationDao();

        aInformationDao.RegionId = Convert.ToInt32(regionDropDownList.SelectedValue);
        
        aInformationDao.AreaName = areaNameTextBox.Text.Trim();
        aInformationDao.AreaCode = areaCodeTextBox.Text.Trim();
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
        areaHiddenField.Value = "";
        areaCodeHiddenField.Value = "";
        areaNameTextBox.Text = "";
        areaCodeTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (areaCodeHiddenField.Value != "")
        {
            if (areaCodeTextBox.Text != areaCodeHiddenField.Value)
            {
                if (CheckAreaCodeExistOrNot(areaCodeTextBox.Text))
                {
                    aShowMessage.ShowMessageBox(aMessages.CodeExistMessage, this);
                    areaCodeTextBox.Text = areaCodeHiddenField.Value;
                }
            }
        }
        else
        {
            if (CheckAreaCodeExistOrNot(areaCodeTextBox.Text))
            {
                aShowMessage.ShowMessageBox(aMessages.CodeExistMessage, this);
                areaCodeTextBox.Text = "";
            }
        }
    }
    private bool CheckAreaCodeExistOrNot(string areaCode)
    {
        bool status = false;

        DataTable dataTable = areaInformation.CheckAreaCodeExistOrNot(areaCode);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }
}