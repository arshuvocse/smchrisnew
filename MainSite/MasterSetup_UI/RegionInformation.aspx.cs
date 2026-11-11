using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_RegionInformation : System.Web.UI.Page
{
    RegionInformationDal aRegionInformationDal = new RegionInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetCheckBox();
           


            if (Session["RegionId"] != null)
            {
                GetOneRecord(Session["RegionId"].ToString());
                Session["RegionId"] = null;
            }
        }
    }

    private void GetOneRecord(string regionId)
    {
        DataTable dataTable = aRegionInformationDal.GetRegionInformationById(regionId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            regionHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("RegionId").ToString(CultureInfo.InvariantCulture);
            regionCodeHiddenField.Value = dataTable.Rows[rowIndex].Field<string>("RegionCode");
            regionNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("RegionName");
            regionCodeTextBox.Text = dataTable.Rows[rowIndex].Field<string>("RegionCode");

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

        if (regionNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VRegion, this);
            return false;
        }

        if (regionCodeTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VRegionCode, this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (regionHiddenField.Value == "")
            {
                try
                {
                    Int32 divisionId = SaveRegionInformation();

                    if (divisionId > 0)
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

            if (regionHiddenField.Value != "")
            {
                try
                {
                    bool division = UpdateRegionInformation(PrepareDataForUpdate());

                    if (division)
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

    private bool UpdateRegionInformation(RegionInformatinDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aRegionInformationDal.UpdateRegionInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private RegionInformatinDao PrepareDataForUpdate()
    {
        var aInformationDao = new RegionInformatinDao();

        aInformationDao.RegionId = Convert.ToInt32(regionHiddenField.Value);
        aInformationDao.RegionName = regionNameTextBox.Text.Trim();
        aInformationDao.RegionCode = regionCodeTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Description = descriptionTexBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveRegionInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aRegionInformationDal.SaveRegionInfo(PrepareDataForSave());        
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    

    private RegionInformatinDao PrepareDataForSave()
    {
        var aInformationDao = new RegionInformatinDao();

        aInformationDao.RegionName = regionNameTextBox.Text.Trim();
        aInformationDao.RegionCode = regionCodeTextBox.Text.Trim();
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
        regionHiddenField.Value = "";
        regionCodeHiddenField.Value = "";
        regionNameTextBox.Text = "";
        regionCodeTextBox.Text = "";
        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        submitButton.Text = "Save";

        SetCheckBox();
    }

    protected void regionCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (regionCodeHiddenField.Value != "")
        {
            if (regionCodeTextBox.Text != regionCodeHiddenField.Value)
            {
                if (CheckRegionCodeExistOrNot(regionCodeTextBox.Text))
                {
                    aShowMessage.ShowMessageBox(aMessages.CodeExistMessage, this);
                    regionCodeTextBox.Text = regionCodeHiddenField.Value;
                }
            }
        }
        else
        {
            if (CheckRegionCodeExistOrNot(regionCodeTextBox.Text))
            {
                aShowMessage.ShowMessageBox(aMessages.CodeExistMessage, this);
                regionCodeTextBox.Text = "";
            }
        }
    }


    private bool CheckRegionCodeExistOrNot(string regionCode)
    {
        bool status = false;

        DataTable dataTable = aRegionInformationDal.CheckRegionCodeExistOrNot(regionCode);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("RegionInformationView.aspx");
    }
}