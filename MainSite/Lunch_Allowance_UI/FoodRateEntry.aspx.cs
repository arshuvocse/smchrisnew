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

public partial class Lunch_Allowance_UI_FoodRateEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();
    private int mid = 0;
    AchievementsEntryDAL aVaencyEntryDaL = new AchievementsEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    DepartmentInformationDal aDepartmentInformationDal = new DepartmentInformationDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            SetCheckBox();
           ButtonVisible();
            LoadDropDownList();

            if (Session["VacancyCirculationId"] != null)
            {
                try
                {

                    mid = Convert.ToInt32(Session["VacancyCirculationId"].ToString());
                    (hdpk.Value) = mid.ToString();
                    if (mid > 0)
                    {

                        using (var db = new HRIS_SMC_DBEntities())
                        {
                            var Bat = (from j in db.tblFoodRates where j.FoodRateId == mid select j).FirstOrDefault();
                            companyDropDownList.SelectedValue = Bat.CompanyId.ToString();
                            companyDropDownList_OnSelectedIndexChanged(null, null);
                            ddlEmpCategory.SelectedValue = Bat.EmployeeType.ToString();

                            txtHolidayFromDate.Text = string.IsNullOrEmpty(Bat.EffectiveDate.ToString()) ? String.Empty : Bat.EffectiveDate.Value.ToString("dd-MMM-yyyy");
                           
                            txtFoodrate.Text = Bat.Rate.ToString();


                           
                        }
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

    private void LoadDropDownList()
    {
        aDepartmentInformationDal.GetCompanyListIntoDropdown(companyDropDownList);
       
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        using (DataTable dt = _commonDataLoad.GetDDLComCategory())
        {
            ddlEmpCategory.DataSource = dt;
            ddlEmpCategory.DataValueField = "Value";
            ddlEmpCategory.DataTextField = "TextField";
            ddlEmpCategory.DataBind();
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
            Response.Redirect("FoodRateView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
          //  VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("MasterAchievementsId").ToString(CultureInfo.InvariantCulture);


          //  VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("AchievementsName");
         

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


        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please select this !!!", this);
            companyDropDownList.Focus();
            return false;
        }


        if (ddlEmpCategory.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please select this !!!", this);
            ddlEmpCategory.Focus();
            return false;
        }

        if (txtHolidayFromDate.Text == "")
        {
            aShowMessage.ShowMessageBox("Please select this !!!", this);
            txtHolidayFromDate.Focus();
            return false;
        }

        if (txtFoodrate.Text == "")
        {
            aShowMessage.ShowMessageBox("Please select this !!!", this);
            txtFoodrate.Focus();
            return false;
        }



     

       
       
      

        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        SaveUpdate();
    }

    private void SaveUpdate()
    {
        if (Validation())
        {
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblFoodRate Bat = null;


            try
            {
                using (var db = new HRIS_SMC_DBEntities())
                {
                    if (mid > 0)
                    {
                        Bat = (from j in db.tblFoodRates where j.FoodRateId == mid select j).FirstOrDefault();


                        Bat.CompanyId = int.Parse(companyDropDownList.SelectedValue) > 0
                            ? int.Parse(companyDropDownList.SelectedValue)
                            : (int?)null;


                        Bat.EmployeeType = int.Parse(companyDropDownList.SelectedValue) > 0
                         ? int.Parse(ddlEmpCategory.SelectedValue)
                         : (int?)null;
                        Bat.EffectiveDate = string.IsNullOrEmpty(txtHolidayFromDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txtHolidayFromDate.Text).Date;


                        Bat.Rate = Convert.ToDecimal(txtFoodrate.Text);

                        Bat.UpdateBy = Convert.ToInt32(Session["UserId"]);
                        Bat.UpdateDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        Bat = new tblFoodRate();

                        Bat.CompanyId = int.Parse(companyDropDownList.SelectedValue) > 0
                              ? int.Parse(companyDropDownList.SelectedValue)
                              : (int?)null;


                        Bat.EmployeeType = int.Parse(companyDropDownList.SelectedValue) > 0
                         ? int.Parse(ddlEmpCategory.SelectedValue)
                         : (int?)null;
                        Bat.EffectiveDate = string.IsNullOrEmpty(txtHolidayFromDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txtHolidayFromDate.Text).Date;


                        Bat.Rate = Convert.ToDecimal(txtFoodrate.Text);
                        Bat.EntryBy = Convert.ToInt32(Session["UserId"]);
                        Bat.EntryDate = DateTime.Now;
                        db.tblFoodRates.Add(Bat);
                        db.SaveChanges();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...! ');window.location ='FoodRateView.aspx';",
                        true);
                }
            }
            catch (Exception)
            {
            }
        }
    }

    private bool UpdateAreaInformation(AchievementsEntryDAO prepareDataForUpdate)
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
    private AchievementsEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new AchievementsEntryDAO();

      //  aVacancyEntryDao.MasterAchievementsId = Convert.ToInt32(VacancyIdHiddenField.Value);

      //  aVacancyEntryDao.AchievementsName = VacancyNameTextBox.Text.Trim();

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
    private AchievementsEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new AchievementsEntryDAO();



        //aVacancyEntryDao.AchievementsName = VacancyNameTextBox.Text.Trim();

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;

        
        aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        aVacancyEntryDao.EntryDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
       
      //  VacancyIdHiddenField.Value = "";
        
      //  VacancyNameTextBox.Text = "";
      
        submitButton.Text = "Save";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("FoodRateView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        SaveUpdate();
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        if (hdpk.Value != string.Empty)
        {

            // if (!CheckEmpDepartmentAllocateOrNot(mid.ToString()))
            {
                tblFoodRate Bat = null;
                try
                {
                    using (var db = new HRIS_SMC_DBEntities())
                    {
                        if (mid > 0)
                        {

                            Bat = (from j in db.tblFoodRates where j.FoodRateId == mid select j).FirstOrDefault();









                            Bat.IsDelete = true;
                            Bat.DeleteBy = Convert.ToInt32(Session["UserId"]);
                            Bat.DeleteDate = DateTime.Now;
                            db.SaveChanges();

                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successful...! ');window.location ='YearlyHolidayView.aspx';",
                                true);
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
            //   else
            //   {
            // ScriptManager.RegisterStartupScript(this, this.GetType(),
            //     "alert",
            //    "alert('Can not be Updated & Deleted! Already Defined......');window.location ='YearlyHolidayView.aspx';",
            //      true);

            // }
        }
    }

    private void Delete()
    {
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        if (hdpk.Value != string.Empty)
        {

            // if (!CheckEmpDepartmentAllocateOrNot(mid.ToString()))
            {
                tblYearlyHoliday Bat = null;
                try
                {
                    using (var db = new HRIS_SMC_DBEntities())
                    {
                        if (mid > 0)
                        {

                            Bat = (from j in db.tblYearlyHolidays where j.YearlyHolidayId == mid select j).FirstOrDefault();









                            Bat.IsDelete = true;
                            Bat.DeleteBy = Convert.ToInt32(Session["UserId"]);
                            Bat.DeleteDate = DateTime.Now;
                            db.SaveChanges();

                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successful...! ');window.location ='FoodRateView.aspx';",
                                true);
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
            //   else
            //   {
            // ScriptManager.RegisterStartupScript(this, this.GetType(),
            //     "alert",
            //    "alert('Can not be Updated & Deleted! Already Defined......');window.location ='YearlyHolidayView.aspx';",
            //      true);

            // }
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
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        
       

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}