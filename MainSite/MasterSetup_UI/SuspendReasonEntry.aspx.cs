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

public partial class MasterSetup_UI_SuspendReasonEntry : System.Web.UI.Page
{
    SuspendReasonEntryDaL aVaencyEntryDaL = new SuspendReasonEntryDaL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            SetCheckBox();
            ButtonVisible();
            loadDropDownList();

            if (Session["SubSectionId"] != null)
            {
                GetOneRecord(Session["SubSectionId"].ToString());
                Session["SubSectionId"] = null;
            }
        }
    }

    private void loadDropDownList()
    {
        aVaencyEntryDaL.GetComapnyNameList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
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
            Response.Redirect("SuspendReasonEntryView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            SuspendReasonEntryIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SuspendReasonEntryId").ToString(CultureInfo.InvariantCulture);


            SuspendReasonEntryTextBox.Text = dataTable.Rows[rowIndex].Field<string>("SuspendReasonEntry");
            companyDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<int>("CompanyId").ToString();
         

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

            if (dataTable.Rows[rowIndex].Field<bool>("IsSuspend")==true)
            {
                rbSuSOrRelease.Items[0].Selected = true;
            }

            if (dataTable.Rows[rowIndex].Field<bool>("IsDisciplinary") == true)
            {
                rbSuSOrRelease.Items[1].Selected = true;
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
        

        if (SuspendReasonEntryTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }



        if (String.IsNullOrEmpty(rbSuSOrRelease.Text))
        {
            //Tell the user that the text provided is unacceptable.
            aShowMessage.ShowMessageBox("Please Check For  Suspend or Disciplinary", this);
            //Validation was unsuccessful.
            return false;
        }
      
      

        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (SuspendReasonEntryIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                         "alert",
                         "alert('Data Saved Successfully...');window.location ='SuspendReasonEntryView.aspx';",
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
    private bool UpdateAreaInformation(SuspendReasonEntryDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aVaencyEntryDaL.UpdateVacancyEntryInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private SuspendReasonEntryDao PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new SuspendReasonEntryDao();

        aVacancyEntryDao.SuspendReasonEntryId = Convert.ToInt32(SuspendReasonEntryIdHiddenField.Value);

        aVacancyEntryDao.SuspendReasonEntry = SuspendReasonEntryTextBox.Text.Trim();
        aVacancyEntryDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        if (rbSuSOrRelease.Items[0].Selected)
        {
            aVacancyEntryDao.IsSuspend = true;
            aVacancyEntryDao.IsDisciplinary = false;

        }

        if (rbSuSOrRelease.Items[1].Selected)
        {
            aVacancyEntryDao.IsSuspend = false;
            aVacancyEntryDao.IsDisciplinary = true;

        }

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;

        aVacancyEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aVacancyEntryDao.UpdateDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    private Int32 SaveVacancyEntry()
    {
        Int32 retVal;
        try
        {
            retVal = aVaencyEntryDaL.SaveVacancyEntryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private SuspendReasonEntryDao PrepareDataForSave()
    {
        var aVacancyEntryDao = new SuspendReasonEntryDao();



        aVacancyEntryDao.SuspendReasonEntry = SuspendReasonEntryTextBox.Text.Trim();

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;
        if (rbSuSOrRelease.Items[0].Selected)
        {
            aVacancyEntryDao.IsSuspend = true;
            aVacancyEntryDao.IsDisciplinary = false;

        }

        if (rbSuSOrRelease.Items[1].Selected)
        {
            aVacancyEntryDao.IsSuspend = false;
            aVacancyEntryDao.IsDisciplinary = true;

        }


        aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);
        aVacancyEntryDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);


        aVacancyEntryDao.EntryDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
       
        SuspendReasonEntryIdHiddenField.Value = "";
        
        SuspendReasonEntryTextBox.Text = "";
      
        submitButton.Text = "Save";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SuspendReasonEntryView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (SuspendReasonEntryIdHiddenField.Value != "")
        {
            try
            {
                bool area = UpdateAreaInformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "alert",
                              "alert('Data Updated Successfully...');window.location ='SuspendReasonEntryView.aspx';",
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
        if (SuspendReasonEntryIdHiddenField.Value != string.Empty)
        {
            Delete();
        }
    }

    private void Delete()
    {
        SuspendReasonEntryDao aVacancyEntryDao = new SuspendReasonEntryDao();


        aVacancyEntryDao.SuspendReasonEntryId = Convert.ToInt32(SuspendReasonEntryIdHiddenField.Value);

        aVacancyEntryDao.IsDelete = true;


        aVacancyEntryDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aVacancyEntryDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aVaencyEntryDaL.DeleteVacancyEntryfoById(aVacancyEntryDao);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='SuspendReasonEntryView.aspx';",
              true);
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}