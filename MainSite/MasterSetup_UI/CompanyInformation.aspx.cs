using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_CompanyInformation : System.Web.UI.Page
{
    CompanyInformationDal aInformationDal = new CompanyInformationDal();
    Validation aValidation = new Validation();
    ShowMessage aMessage = new ShowMessage();
    Messages aMessages = new Messages();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["companyId"] != null)
            {
                GetOneRecord(Session["companyId"].ToString());
                Session["divisionId"] = null;
            }
        }
    }

    private void GetOneRecord(string companyId)
    {
        DataTable dataTable = aInformationDal.GetCompanyInformationById(companyId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            companyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);
            companynameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("CompanyName");
            shortNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("ShortName");
            companyAddressTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Address");
            contactTextBox.Text = dataTable.Rows[rowIndex].Field<string>("ContactNo");
            faxNoTextBox.Text = dataTable.Rows[rowIndex].Field<string>("FaxNumber");
            pabxTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Pabx");
            emailTextBox.Text = dataTable.Rows[rowIndex].Field<string>("EmailAdress");
            hotlineTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Hotline");
            descriptionTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Description");
            remarksTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Remarks");

            submitButton.Text = "Update";
        }
    }

    private bool Validation()
    {
        if (companynameTextBox.Text == "")
        {
            aMessage.ShowMessageBox("Please insert Company Name!!!", this);
            return false;
        }

        if (shortNameTextBox.Text == "")
        {
            aMessage.ShowMessageBox("Please insert Company Short Name!!!", this);
            return false;
        }

        if (companyAddressTextBox.Text == "")
        {
            aMessage.ShowMessageBox("Please insert Company address!!!", this);
            return false;
        }

        if (contactTextBox.Text == "")
        {
            aMessage.ShowMessageBox("Please insert contact number!!!", this);
            return false;
        }

        return true;
    }

    

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (companyIdHiddenField.Value == "")
            {
                 try
                {
                    Int32 companyId = SaveCompanyInformation();

                    if (companyId > 0)
                    {
                        aMessage.ShowMessageBox(aMessages.SaveSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (companyIdHiddenField.Value != "")
            {
                try
                {
                    bool company = UpdateCompanyInformation();

                    if (company)
                    {
                        aMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                }
            }
        }
    }

    private bool UpdateCompanyInformation()
    {
        bool retVal;
        try
        {
            retVal = aInformationDal.UpdateComapnyInfo(PrepareDataForUpdate());

        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private CompanyInformationDao PrepareDataForUpdate()
    {
        var aInformationDao = new CompanyInformationDao();

        aInformationDao.CompanyId = Convert.ToInt32(companyIdHiddenField.Value);
        aInformationDao.CompanyName = companynameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.Address = companyAddressTextBox.Text.Trim();
        aInformationDao.ContactNo = contactTextBox.Text.Trim();
        aInformationDao.FaxNumber = faxNoTextBox.Text.Trim();
        aInformationDao.Pabx = pabxTextBox.Text.Trim();
        aInformationDao.EmailAdress = emailTextBox.Text.Trim();
        aInformationDao.Hotline = hotlineTextBox.Text.Trim();
        aInformationDao.Description = descriptionTextBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
        aInformationDao.UpdateBy = Session["LoginName"].ToString();
        aInformationDao.UpdateDate = DateTime.Now;

        return aInformationDao;
    }

    private Int32 SaveCompanyInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aInformationDal.SaveComapnyInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }

    private CompanyInformationDao PrepareDataForSave()
    {
        var aInformationDao = new CompanyInformationDao();

        aInformationDao.CompanyName = companynameTextBox.Text.Trim();
        aInformationDao.ShortName = shortNameTextBox.Text.Trim();
        aInformationDao.Address = companyAddressTextBox.Text.Trim();
        aInformationDao.ContactNo = contactTextBox.Text.Trim();
        aInformationDao.FaxNumber = faxNoTextBox.Text.Trim();
        aInformationDao.Pabx = pabxTextBox.Text.Trim();
        aInformationDao.EmailAdress = emailTextBox.Text.Trim();
        aInformationDao.Hotline = hotlineTextBox.Text.Trim();
        aInformationDao.Description = descriptionTextBox.Text.Trim();
        aInformationDao.Remarks = remarksTextBox.Text.Trim();
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
        companynameTextBox.Text = "";
        companyAddressTextBox.Text = "";
        shortNameTextBox.Text = "";
        contactTextBox.Text = "";
        faxNoTextBox.Text = "";
        pabxTextBox.Text = "";
        emailTextBox.Text = "";
        hotlineTextBox.Text = "";
        descriptionTextBox.Text = "";
        remarksTextBox.Text = "";

        submitButton.Text = "Save";
    }

    protected void emailTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (!aValidation.IsValidEmail(emailTextBox.Text))
        {
            aMessage.ShowMessageBox(aMessages.InvalidEmailMessage, this);
            emailTextBox.Text = "";
        }
    }


    protected void CompanyListImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyInformationView.aspx");

    }
}