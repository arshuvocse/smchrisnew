using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using CrystalDecisions.CrystalReports.Engine;
using DAL.ACC_DAL;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAO.ACC_DAO;


public partial class CSTL_ACC_UI_FinancialYear : System.Web.UI.Page
{
    FinancialYearDAL aFinancialYearBLL = new FinancialYearDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownList();
            hiddenField.Value = "";
            if (Session["FinancialYear"] != null)
            {
             
                GetOneRecord(Convert.ToInt32(Session["FinancialYear"].ToString()));
                Session["FinancialYear"] = null;
            }
        }
    }
    # region Edit
    private void GetOneRecord(Int32 id)
    {
        
        using (DataTable dt = aFinancialYearBLL.FinancialYearEditLoad(id.ToString()))
        {
            submitButton.Text = "Update";
            submitButton.BackColor = Color.DodgerBlue;

            Int32 rowIndex = 0;
            hiddenField.Value = dt.Rows[rowIndex].Field<Int32>("FinancialYearId").ToString();

            yearStartDateTextBox.Text = dt.Rows[rowIndex].Field<DateTime>("StartDate").ToString("dd-MMM-yyyy");
            yearEndTextBox.Text = dt.Rows[rowIndex].Field<DateTime>("EndDate").ToString("dd-MMM-yyyy");

            if (dt.Rows[0]["Status"].ToString() == "Active")
            {
                isactiveCheckBox.Checked = true;
                Divactive.Visible = true;

                activeDateTextBox.Text = dt.Rows[rowIndex].Field<DateTime>("ActiveDate").ToString("dd-MMM-yyyy"); 
            }
            if (dt.Rows[0]["Status"].ToString() == "InActive")
            {
                DivInactive.Visible = true;
                isactiveCheckBox.Checked = false;
                inactiveDateTextBox.Text = dt.Rows[rowIndex].Field<DateTime>("InActiveDate").ToString("dd-MMM-yyyy");
                activeDateTextBox.Text = dt.Rows[rowIndex].Field<DateTime>("ActiveDate").ToString("dd-MMM-yyyy"); 
            }

            companyDropDownList.SelectedValue = dt.Rows[rowIndex].Field<Int32>("CompanyId").ToString();
        }
     
    }
    #endregion Edit
    # region View
   
   
 
  

    #endregion View
    #region Entry
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    readonly IncrementDal aIncrementDal = new IncrementDal();

    public void LoadDropdownList()
    {
        aIncrementDal.LoadCompany(companyDropDownList);
    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private void Clear()
    {
        hiddenField.Value = string.Empty;
        submitButton.Text = "Save";
        submitButton.BackColor = Color.DodgerBlue;

        companyDropDownList.SelectedValue = string.Empty;
        companyDropDownList.BackColor = Color.White;
        yearStartDateTextBox.Text = string.Empty;
        yearStartDateTextBox.BackColor = Color.White;
        yearEndTextBox.Text = string.Empty;
        yearEndTextBox.BackColor = Color.White;
        activeDateTextBox.Text = string.Empty;
        activeDateTextBox.BackColor = Color.White;
        inactiveDateTextBox.Text = string.Empty;
        inactiveDateTextBox.BackColor = Color.White;
        DivInactive.Visible = false;
    }
    private bool Validation()
    {
        int xxx = 0;
        try
        {
            xxx = Convert.ToInt32(hiddenField.Value);
        }
        catch (Exception)
        {
            hiddenField.Value = "";
            //throw;
        }
        if (hiddenField.Value != "")
        {
            isactiveCheckBox.Checked = false;
            showMessageBox("Please Select IsActive !!!");
            return false;
        }
        if (companyDropDownList.SelectedValue == "")
        {
            showMessageBox("Please fill out this field !!!");
            companyDropDownList.Focus();
            companyDropDownList.BackColor = Color.GhostWhite;
            return false;
        }
        if (yearStartDateTextBox.Text == "")
        {
            showMessageBox("Please fill out this field !!!");
            yearStartDateTextBox.Focus();
            yearStartDateTextBox.BackColor = Color.GhostWhite;
            return false;
        }
        if (yearEndTextBox.Text == "")
        {
            showMessageBox("Please fill out this field !!!");
            yearEndTextBox.Focus();
            yearEndTextBox.BackColor = Color.GhostWhite;
            return false;
        }
        if (isactiveCheckBox.Checked)
        {
            if (activeDateTextBox.Text == "")
            {
                showMessageBox("Please fill out this field !!!");
                activeDateTextBox.Focus();
                activeDateTextBox.BackColor = Color.GhostWhite;
                return false;
            }
        }
        if (isactiveCheckBox.Checked == false)
        {
            if (inactiveDateTextBox.Text == "")
            {
                showMessageBox("Please fill out this field !!!");
                inactiveDateTextBox.Focus();
                inactiveDateTextBox.BackColor = Color.GhostWhite;
                return false;
            }
        }

        return true;
    }
    private FinancialYearEntry PrepareDataForSave()
    {
        FinancialYearEntry aFinancialYearEntry = new FinancialYearEntry();
        {
            if (isactiveCheckBox.Checked)
            {
                aFinancialYearEntry.Status = "Active";
                aFinancialYearEntry.ActiveDate = Convert.ToDateTime(activeDateTextBox.Text);
            }
            if (isactiveCheckBox.Checked == false)
            {
                aFinancialYearEntry.Status = "InActive";
                aFinancialYearEntry.InActiveDate = Convert.ToDateTime(inactiveDateTextBox.Text);
            }
            aFinancialYearEntry.StartDate = Convert.ToDateTime(yearStartDateTextBox.Text);
            aFinancialYearEntry.EndDate = Convert.ToDateTime(yearEndTextBox.Text);
            aFinancialYearEntry.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

        };
        return aFinancialYearEntry;
    }
    private FinancialYearEntry PrepareDataForUpdate()
    {
        FinancialYearEntry aFinancialYearEntry = new FinancialYearEntry();
        {
            aFinancialYearEntry.FinancialYearId = Convert.ToInt32(hiddenField.Value);
            if (isactiveCheckBox.Checked)
            {
                aFinancialYearEntry.Status = "Active";
                aFinancialYearEntry.ActiveDate = Convert.ToDateTime(activeDateTextBox.Text);
            }
            if (isactiveCheckBox.Checked == false)
            {
                aFinancialYearEntry.Status = "InActive";
                aFinancialYearEntry.InActiveDate = Convert.ToDateTime(inactiveDateTextBox.Text);
                aFinancialYearEntry.ActiveDate = Convert.ToDateTime(activeDateTextBox.Text);
            }
            aFinancialYearEntry.StartDate = Convert.ToDateTime(yearStartDateTextBox.Text);
            aFinancialYearEntry.EndDate = Convert.ToDateTime(yearEndTextBox.Text);
            aFinancialYearEntry.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
        }
        return aFinancialYearEntry;
    }
    private bool UpdateChanges()
    {
        bool retVal;
        try
        {
            retVal = aFinancialYearBLL.UpdateFinancialYear(PrepareDataForUpdate());
        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }
    private Int32 SaveChanges()
    {
        Int32 retVal;
        try
        {
            retVal = aFinancialYearBLL.SaveFinancialYear(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }
    #endregion Entry
    protected void submitButton_Click(object sender, EventArgs e)
    {
        {
            if (Validation())
            {
                if (hiddenField.Value == "")
                {
                   
                    if (SaveChanges() > 0)
                    {
                        showMessageBox("Information Created Successfully...");
                        Clear();
                    }
                    else
                    {
                        showMessageBox("Information Already Created!!!");
                    }
                    
                }
                if (hiddenField.Value != "")
                {
                  
                    if (UpdateChanges())
                    {
                        showMessageBox("Information Updated Successfully...");
                        Clear();
                    }
                   

                }
               
            }
            else
            {
                showMessageBox("Error Creating Information !!!");
            }
        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Clear();
    }



    protected void yearEndTextBox_TextChanged(object sender, EventArgs e)
    {
        if (yearEndTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(yearEndTextBox.Text);
            }
            catch
            {
                yearEndTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void yearStartDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (yearStartDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(yearStartDateTextBox.Text);
                activeDateTextBox.Text = yearStartDateTextBox.Text;
            }
            catch
            {
                yearStartDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void activeDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (activeDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(activeDateTextBox.Text);
            }
            catch
            {
                activeDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void inactiveDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (inactiveDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(inactiveDateTextBox.Text);
            }
            catch
            {
                inactiveDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }
    protected void isactiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {

        Divactive.Visible = false;
        DivInactive.Visible = false;


        if (isactiveCheckBox.Checked == false)
        {
            DivInactive.Visible = true;
        }
        if (isactiveCheckBox.Checked)
        {
            Divactive.Visible = true;
           
        }
    }

    protected void HomeButton_OnClick_OnClick(object sender, EventArgs e)
    {
       
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("FinancialYearList.aspx");
       
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        
    }
}