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

public partial class MasterSetup_UI_VivaSetupInfoEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    AchievementsEntryDAL aVaencyEntryDaL = new AchievementsEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            SetCheckBox();
             ButtonVisible();

            LoadDropDownnList();
            if (Session["VacancyCirculationId"] != null)
            {
                try
                {

                    mid = Convert.ToInt32(Session["VacancyCirculationId"].ToString());
                    (hdpk.Value) = mid.ToString();


                    DataTable dt = _commonDataLoad.GetVivaSetupInfoById(Convert.ToInt32(mid));
                    if (mid > 0)
                    {


                        CompanyDropDown.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                        CategoryDropDownList.SelectedValue = dt.Rows[0]["Category"].ToString();

                        VIvaTextBox.Text = dt.Rows[0]["VivaName"].ToString();
                        RemarksTextBox.Text = dt.Rows[0]["Remarks"].ToString();
                        isActiveCheckBox.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());

                            submitButton.Text = "Update";

                         
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                Session["VacancyCirculationId"] = null;
            }
        }
    }

    private void LoadDropDownnList()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            CompanyDropDown.DataSource = dt;
            CompanyDropDown.DataValueField = "Value";
            CompanyDropDown.DataTextField = "TextField";
            CompanyDropDown.DataBind();
            CompanyDropDown.SelectedIndex = 1;
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
            Response.Redirect("VivaSetupInfoView.aspx");
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


        if (CompanyDropDown.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            CompanyDropDown.Focus();
            return false;
        }

        if (CategoryDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            CategoryDropDownList.Focus();
            return false;
        }

        if (VIvaTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            VIvaTextBox.Focus();
            return false;
        }
 
       
      

        return true;
    }

    private bool CheckEmpDepartmentAllocateOrNot(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.VivaSetupAllocatedOrNotEMP(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        
            if (Validation())
            {

                mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);


                VivaDao aMaster = new VivaDao();



                aMaster.VivaId = Convert.ToInt32(mid);

                aMaster.CompanyId = int.Parse(CompanyDropDown.SelectedValue) > 0 ? int.Parse(CompanyDropDown.SelectedValue) : (int?)null;
                aMaster.VivaName = string.IsNullOrEmpty(VIvaTextBox.Text) ? null : VIvaTextBox.Text;
                aMaster.Category = string.IsNullOrEmpty(CategoryDropDownList.SelectedValue) ? null : CategoryDropDownList.SelectedValue;

                aMaster.Remarks = string.IsNullOrEmpty(RemarksTextBox.Text) ? null : RemarksTextBox.Text;
                aMaster.IsActive = isActiveCheckBox.Checked;

                int pk = _commonDataLoad.SaveVivaMaster(aMaster, Session["UserId"].ToString());
                if (pk>0)
                {
                           ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Operation Successful...! ');window.location ='VivaSetupInfoView.aspx';",
              true);
                }
                else
                {
                    aShowMessage.ShowMessageBox("Operation Faild!!",this);
                }

                
                 
            }

           
        
    }
   
   
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("VivaSetupInfoEntry.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {

        if (Validation())
        {

            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            if (!CheckEmpDepartmentAllocateOrNot(mid.ToString()))
                            {

            VivaDao aMaster = new VivaDao();



            aMaster.VivaId = Convert.ToInt32(mid);

            aMaster.CompanyId = int.Parse(CompanyDropDown.SelectedValue) > 0 ? int.Parse(CompanyDropDown.SelectedValue) : (int?)null;
            aMaster.VivaName = string.IsNullOrEmpty(VIvaTextBox.Text) ? null : VIvaTextBox.Text;
            aMaster.Category = string.IsNullOrEmpty(CategoryDropDownList.SelectedValue) ? null : CategoryDropDownList.SelectedValue;

            aMaster.Remarks = string.IsNullOrEmpty(RemarksTextBox.Text) ? null : RemarksTextBox.Text;
            aMaster.IsActive = isActiveCheckBox.Checked;

            int pk = _commonDataLoad.SaveVivaMaster(aMaster, Session["UserId"].ToString());
            if (pk > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
   "alert",
   "alert('Operation Successful...! ');window.location ='VivaSetupInfoView.aspx';",
   true);
            }
            else
            {
                aShowMessage.ShowMessageBox("Operation Faild!!", this);
            }

                            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert(Can not be Updated & Deleted! Already Defined...');window.location ='VivaSetupInfoView.aspx';",
                    true);

            }

        }
         
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        if (hdpk.Value != string.Empty)
        {

            if (!CheckEmpDepartmentAllocateOrNot(mid.ToString()))
            {
               
                try
                {
                  bool b=  _commonDataLoad.UpdateEmployeeContractEndDateInfo(mid);
                    if (b)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
 "alert",
 "alert('Operation Successful...! ');window.location ='VivaSetupInfoView.aspx';",
 true);
                    }
                }
                catch (Exception)
                {


                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Can not be Updated & Deleted! Already Defined......');window.location ='VivaSetupInfoView.aspx';",
                    true);

            }
        }
    }

    

    private bool CheckAchievementsAllocateOrNot(string Achievements)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.AchievementsAllocatedOrNotEMP(Achievements);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}