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

public partial class MasterSetup_UI_DesignationStepInformation : System.Web.UI.Page
{
    DesignationStepInformationDal aDivisionInformationDal = new DesignationStepInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetCheckBox();

            if (Session["UserId"] != null)
            {
                const int manuId = 15;
                aOperationCredentials.MnageUserOperation("VIEW", Session["UserId"].ToString(), manuId, this);
            }

            if (Session["designationStepId"] != null)
            {
                GetOneRecord(Convert.ToInt32(Session["designationStepId"].ToString()));
                Session["designationStepId"] = null;
            }
        }
        else
        {

            Response.Redirect("DesignationStepView.aspx");
        }
    }

    private void GetOneRecord(Int32 designationStepId)
    {
        DataTable desigInfoById = aDivisionInformationDal.GetDesignationStepInformationById(designationStepId);

        const int rowIndex = 0;

        if (desigInfoById.Rows.Count > 0)
        {
            designatonStepHiddenField.Value =
                desigInfoById.Rows[rowIndex].Field<Int32>("DesignationStepId").ToString(CultureInfo.InvariantCulture);
            designationStepNameTextBox.Text = desigInfoById.Rows[rowIndex].Field<string>("DesignationStepName");
            remarksTextBox.Text = desigInfoById.Rows[rowIndex].Field<string>("Remarks");

            if (desigInfoById.Rows[rowIndex].Field<bool>("IsActive"))
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
        if (designationStepNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Insert Designation Step Name!!!", this);
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (designatonStepHiddenField.Value =="")
            {
                try
                {
                    Int32 designationStepId = SaveDesignationStepInformation();

                    if (designationStepId > 0)
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

            if (designatonStepHiddenField.Value != "")
            {
                try
                {
                    bool designationStep = UpdateeDesignationStepInformation();

                    if (designationStep)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Response.Redirect("DesignationStepView.aspx");
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }

    private bool UpdateeDesignationStepInformation()
    {
        bool retVal;
        try
        {
            retVal = aDivisionInformationDal.UpdateDesignationStepInfo(PrepareDataForUpdate());

        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }

    private DesignationStepInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new DesignationStepInformationDao();

        aInformationDao.DesignationStepId = Convert.ToInt32(designatonStepHiddenField.Value);
        aInformationDao.DesignationStepName = designationStepNameTextBox.Text;
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.ApprovalStatus = "Posted";
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveDesignationStepInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aDivisionInformationDal.SaveDesignationStepInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }

    private DesignationStepInformationDao PrepareDataForSave()
    {
        var aInformationDao = new DesignationStepInformationDao();

        aInformationDao.DesignationStepName = designationStepNameTextBox.Text;
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
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
        designationStepNameTextBox.Text = "";
        remarksTextBox.Text = "";
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DesignationStepView.aspx");
    }
}