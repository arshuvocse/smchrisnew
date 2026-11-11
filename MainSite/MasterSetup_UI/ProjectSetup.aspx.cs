using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_ProjectSetup : System.Web.UI.Page
{
    ProjectSetupDal aProjectDal = new ProjectSetupDal();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetValue();
            ButtonVisible();
            LoadDropDownList();

            if (Session["ProjectId"] != null)
            {
                GetOneRecord(Session["ProjectId"].ToString());
                Session["ProjectId"] = null;
            }
            
        }
    }

    private void GetValue()
    {
        projectStartDate.Attributes.Add("readonly", "readonly");
        projectEndDate.Attributes.Add("readonly", "readonly");
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                btnSave.Visible = true;
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
            Response.Redirect("ProjectSetupView.aspx");
        }

    }
    private void GetOneRecord(string projectId)
    {
        DataTable dataTable = aProjectDal.GetProjectSetupById(projectId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            projectSetupIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("ProjectId").ToString(CultureInfo.InvariantCulture);
            CompanyDropDown.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);
            projectNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("ProjectName");
            projectStartDate.Text = dataTable.Rows[rowIndex].Field<DateTime>("ProjectStartDate").ToString("dd-MMM-yyyy");

            if (dataTable.Rows[rowIndex].Field<bool>("Parmanent"))
            {
                projectNameRadioButtonList.Items[0].Selected = true;
            }

            if (dataTable.Rows[rowIndex].Field<bool>("Temporary"))
            {
                projectNameRadioButtonList.Items[1].Selected=true;
                projectDate.Visible = true;
                projectEndDate.Text = dataTable.Rows[rowIndex].Field<DateTime>("ProjectEndDate").ToString("dd-MMM-yyyy");
            }

            projectDescriptionTextBox.Text = dataTable.Rows[rowIndex].Field<string>("ProjectDescription");
            isActiveCheckBox.Checked = dataTable.Rows[rowIndex].Field<bool>("IsActive");
            remarksTextBox.Text = dataTable.Rows[rowIndex].Field<string>("Remarks");


            try
            {
                chkIsOtherProject.Checked = dataTable.Rows[rowIndex].Field<bool>("IsOtherProject");
            }
            catch (Exception)
            {
                chkIsOtherProject.Checked = false;
                //throw;
            }


            try
            {
                FundedProjectsCheckBox1.Checked = dataTable.Rows[rowIndex].Field<bool>("IsSMCFundedProjects");
            }
            catch (Exception)
            {
                FundedProjectsCheckBox1.Checked = false;
                //throw;
            }


            try
            {
                chkSmcContract.Checked = dataTable.Rows[rowIndex].Field<bool>("IsSMCContract");
            }
            catch (Exception)
            {
                chkSmcContract.Checked = false;
                //throw;
            }
            try
            {
                chkIsCompanyDirector.Checked = dataTable.Rows[rowIndex].Field<bool>("IsCompanyDirector");
            }
            catch (Exception)
            {
                chkIsCompanyDirector.Checked = false;
                //throw;
            }

            //chkIsOtherProject.Checked = true;
            btnSave.Text = "Update";
        }
    }
    private void LoadDropDownList()
    {
        aProjectDal.GetCompanyListIntoDropdown(CompanyDropDown);
        CompanyDropDown.SelectedIndex = 1;
    }
    private bool Validation()
    {
        if (CompanyDropDown.SelectedValue == "")
        {
            ShowMessageBox("Please insert Company!!!");
            return false;
        }
        if (projectNameTextBox.Text == "")
        {
            ShowMessageBox("Please inser Project Name!!!");
            return false;
        }

        if (projectStartDate.Text == "")
        {
            ShowMessageBox("Please inser Project Start Date");
            return false;
        }


        if (projectNameRadioButtonList.Items[0].Selected == false && projectNameRadioButtonList.Items[1].Selected == false)
        {
                ShowMessageBox("Please click Any one Redio Button!!!");
                return false;
        }

        if (projectNameRadioButtonList.SelectedValue == "temporary")
        {
            if (projectEndDate.Text == "")
            {
                ShowMessageBox("Please inser Project End Date");
                return false;
            }
        }
     
        if (projectDescriptionTextBox.Text == "")
        {
            ShowMessageBox("Please inser Description");
            return false;
        }


        int count = 0;
 

        if (chkIsOtherProject.Checked==false)
        {

            count++;

        }
        if (FundedProjectsCheckBox1.Checked == false)
        {
            count++;


        }
        if (chkSmcContract.Checked == false)
        {

            count++;

        }
        if (chkIsCompanyDirector.Checked == false)
        {


            count++;
        }


        if (count==4)
        {
            ShowMessageBox("Please Select One Checkbox from here");
            return false;
        }

        return true;
    }
    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void projectNameRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (projectNameRadioButtonList.SelectedValue == "temporary")
        {
           projectDate.Visible = true;
            
        }
        else
        {
            projectDate.Visible = false;
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (projectSetupIdHiddenField.Value == "")
            {
                try
                {
                    Int32 ProjectId = SaveProjectSetup();

                    if (ProjectId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successfull Done...');window.location ='ProjectSetupView.aspx';",
                     true);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                    throw;
                }
            }

             
            if (projectSetupIdHiddenField.Value != "")
            {
                try
                {
                    bool project = UpdateProjectSetup();

                    if (project)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    //aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                    throw;
                }
            }
        }
    }

    private bool UpdateProjectSetup()
    {
        bool retVal;
        try
        {
            retVal = aProjectDal.UpdateProjectSetup(PrepareDataForUpdate());
        }
        catch (Exception ex)
        {
            retVal = false;
        }
        return retVal;
    }

    private ProjectSetupDao PrepareDataForUpdate()
    {
        var aProjectDao = new ProjectSetupDao();

        aProjectDao.ProjectId = Convert.ToInt32(projectSetupIdHiddenField.Value);

        aProjectDao.CompanyId = Convert.ToInt32(CompanyDropDown.SelectedValue);
        aProjectDao.ProjectName = projectNameTextBox.Text.Trim();
        aProjectDao.ProjectStartDate = Convert.ToDateTime(projectStartDate.Text);


        if (projectNameRadioButtonList.SelectedValue == "parmanent")
        {
            aProjectDao.Parmanent = true;
        }
        else
        {
            aProjectDao.Parmanent = false;
        }

        if (projectNameRadioButtonList.SelectedValue == "temporary")
        {
            aProjectDao.Temporary = true;
            aProjectDao.ProjectEndDate = Convert.ToDateTime(projectEndDate.Text);
        }
        else
        {
            aProjectDao.Temporary = false;

        }

        aProjectDao.IsOtherProject = chkIsOtherProject.Checked;
        aProjectDao.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;
        aProjectDao.IsSMCContract = chkSmcContract.Checked;
        aProjectDao.IsCompanyDirector = chkIsCompanyDirector.Checked;
        aProjectDao.ProjectDescription = projectDescriptionTextBox.Text.Trim();
        aProjectDao.Remarks = remarksTextBox.Text.Trim();
    
        aProjectDao.IsActive = isActiveCheckBox.Checked;
        aProjectDao.UpdateBy = Session["LoginName"].ToString();
        aProjectDao.UpdateDate = DateTime.Now;
       
        return aProjectDao;
    }

    private Int32 SaveProjectSetup()
    {
        Int32 retVal;
        try
        {
            retVal = aProjectDal.SaveProjectSet(PrepareDataForSave());
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }
        return retVal;
    }

    private Int32 SaveProjectSetupDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aProjectDal.SaveProjectSetDEL(PrepareDataForSaveDEL());
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }
        return retVal;
    }
    private ProjectSetupDao PrepareDataForSave()
    {
        var aProjectDao = new ProjectSetupDao();

        aProjectDao.CompanyId = Convert.ToInt32(CompanyDropDown.SelectedValue);
        aProjectDao.ProjectName =projectNameTextBox.Text.Trim();
        aProjectDao.ProjectStartDate = Convert.ToDateTime(projectStartDate.Text);


        if (projectNameRadioButtonList.SelectedValue == "parmanent")
        {
            aProjectDao.Parmanent = true;
        }
        else
        {
            aProjectDao.Parmanent = false;
        }

        if (projectNameRadioButtonList.SelectedValue == "temporary")
        {
            aProjectDao.Temporary = true;
            aProjectDao.ProjectEndDate = Convert.ToDateTime(projectEndDate.Text);
        }
        else
        {
            aProjectDao.Temporary = false;
          
        }

        aProjectDao.IsOtherProject = chkIsOtherProject.Checked;
        aProjectDao.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;
        aProjectDao.IsSMCContract = chkSmcContract.Checked;
        aProjectDao.IsCompanyDirector = chkIsCompanyDirector.Checked;

        aProjectDao.ProjectDescription =projectDescriptionTextBox.Text.Trim();
        aProjectDao.Remarks = remarksTextBox.Text.Trim();
        aProjectDao.EntryBy = Session["LoginName"].ToString();
        aProjectDao.EntryDate = DateTime.Now;
        aProjectDao.IsActive = isActiveCheckBox.Checked;
        return aProjectDao;
    }
    private ProjectSetupDao PrepareDataForSaveDEL()
    {
        var aProjectDao = new ProjectSetupDao();
        aProjectDao.ProjectId = Convert.ToInt32(projectSetupIdHiddenField.Value);
        aProjectDao.CompanyId = Convert.ToInt32(CompanyDropDown.SelectedValue);
        aProjectDao.ProjectName = projectNameTextBox.Text.Trim();
        aProjectDao.ProjectStartDate = Convert.ToDateTime(projectStartDate.Text);


        if (projectNameRadioButtonList.SelectedValue == "parmanent")
        {
            aProjectDao.Parmanent = true;
        }
        else
        {
            aProjectDao.Parmanent = false;
        }

        if (projectNameRadioButtonList.SelectedValue == "temporary")
        {
            aProjectDao.Temporary = true;
            aProjectDao.ProjectEndDate = Convert.ToDateTime(projectEndDate.Text);
        }
        else
        {
            aProjectDao.Temporary = false;

        }

        aProjectDao.IsOtherProject = chkIsOtherProject.Checked;
        aProjectDao.IsSMCFundedProjects = FundedProjectsCheckBox1.Checked;
        aProjectDao.IsSMCContract = chkSmcContract.Checked;
        aProjectDao.IsCompanyDirector = chkIsCompanyDirector.Checked;
        aProjectDao.ProjectDescription = projectDescriptionTextBox.Text.Trim();
        aProjectDao.Remarks = remarksTextBox.Text.Trim();
        aProjectDao.EntryBy = Session["LoginName"].ToString();
        aProjectDao.EntryDate = DateTime.Now;
        aProjectDao.IsActive = isActiveCheckBox.Checked;
        return aProjectDao;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        CompanyDropDown.SelectedValue = "";
        projectNameTextBox.Text = "";
        projectStartDate.Text = "";
        projectNameRadioButtonList.Items[0].Selected = false;
        projectNameRadioButtonList.Items[1].Selected = false;
        projectEndDate.Text = "";
        projectDescriptionTextBox.Text = "";
        remarksTextBox.Text = "";
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ProjectSetupView.aspx");
    }

    

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            


            if (projectSetupIdHiddenField.Value != "")
            {
                try
                {
                    bool project = UpdateProjectSetup();

                    if (project)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successfull Done...');window.location ='ProjectSetupView.aspx';",
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
       
            if (aProjectDal.DeleteProjectSetupById(projectSetupIdHiddenField.Value))
            {
                 Int32 ProjectId = SaveProjectSetupDEL();

                if (ProjectId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfull Done...');window.location ='ProjectSetupView.aspx';",
                        true);
                }

            }
        
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}