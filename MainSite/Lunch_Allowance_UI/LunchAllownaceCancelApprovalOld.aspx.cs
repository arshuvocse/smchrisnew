using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
using DAL.COMMON_DAL;
using DAL.Lunch_Allowance_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Lunch_Allowance_UI_LunchAllownaceCancelApproval : System.Web.UI.Page
{
    LunchAllowanceCancelDAL allowanceCancelDal=new LunchAllowanceCancelDAL();
    ShowMessage aShowMessage = new ShowMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dta = DateTime.Now.AddDays(1);
            effectiveDateTextBox.Text = dta.ToString("dd-MMM-yyyy");
            txtToDate.Text = dta.ToString("dd-MMM-yyyy");

            CalendarExtender1.StartDate = DateTime.Now.AddDays(1);
            CalendarExtender2.StartDate = DateTime.Now.AddDays(1);
            //startDate="<%# DateTime.Now.AddDays(1) %>" EndDate="<%# DateTime.Now.AddDays(30) %>"

            DropDownList();
            Button1_OnClick(null, null);
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void DropDownList()
    {
        allowanceCancelDal.LoadCompany(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);

        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {

        if (validfa())
        {


            DataTable dtdata = allowanceCancelDal.GetLunchAllowCancelList( GenerateParameter(),"");
        

        if (dtdata.Rows.Count>0)
        {
       //int dateCount=     (int) dtdata.Rows[0]["DateDiffer"];



            loadGridView.DataSource = dtdata;
       loadGridView.DataBind();
              
            }
    
        }
    }

    private bool validfa()
    {
        if ((companyDropDownList.SelectedValue == "0") )
        {
            AlertMessageBoxShow("Please Select company...");
            companyDropDownList.Focus();
            return false;


        }


        if ((effectiveDateTextBox.Text == ""))
        {
            AlertMessageBoxShow("Please Select effective Date...");
            effectiveDateTextBox.Focus();
            return false;


        }

    

        return true;
    }

   

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    private string GenerateParameter()
    {
        string parameter = " ";

        if (companyDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and    alo.CompanyId = '" + companyDropDownList.SelectedValue + "'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and    alo.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and   alo.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }

        //if (ddlSection.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SectionId = '" + ddlSection.SelectedValue + "'";
        //}

        //if (ddlSubSection.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
        //}

        //if (txtSearch.Text != "")
        //{
        //    parameter = parameter + "  and (e.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        //}

        //if (NameTextBox.Text != "")
        //{
        //    parameter = parameter + "  and  ( e.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
        //}

        if (ddlDesignation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and  alo.EmpInfoId=" + ddlEmpInfo.SelectedValue + "";
        }


        //if (ddlSalaryLocation.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        //}

        //if (ddlConformationStatus.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        //}

        //if (ActiveStatusDropDownList.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.IsActive = '" + ActiveStatusDropDownList.SelectedValue + "'";
        //}

        if (effectiveDateTextBox.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND alo.EffectiveDate BETWEEN '" + effectiveDateTextBox.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (effectiveDateTextBox.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND alo.EffectiveDate BETWEEN '" + effectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (effectiveDateTextBox.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND alo.EffectiveDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }


        parameter = parameter + "  and alo.ActionStatus= 'Submitted'";

        return parameter;
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }
    protected void submitButton_OnClick(object sender, EventArgs e)
    {


        if (Valisddate())
        {

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                 SaveMethod(i);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelSelf.aspx';",
                true);

        }
    }

    private void SaveMethod(int i)
    {
        
            LunchAllownceCancelDAO allownceCancelDao = new LunchAllownceCancelDAO();

            HiddenField hfLunchAlllowCancelId = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hfLunchAlllowCancelId");
            TextBox lblDateData = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("lblDateData");
            CheckBox chkSelect = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            TextBox Remark = (TextBox)loadGridView.Rows[i].FindControl("txtApprovalRemarks");

        if (chkSelect.Checked)
        {

            if (lblDateData.Text != "") {
                bool issucc = allowanceCancelDal.ApproveLunchAllowCancel(hfLunchAlllowCancelId.Value, actionRadioButtonList.SelectedValue, Remark.Text.Trim(), lblDateData.Text);
            if (issucc)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelApproval.aspx';",
                    true);
            }
            else
            {
                ShowMessageBox("Operation Faild!!");
            }
            }
            else
            {
                ShowMessageBox("Please Select Cancel Date!!");
                lblDateData.Focus();
            }
        }
       



    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool Valisddate()
    {

        if (actionRadioButtonList.SelectedValue == "")
        {
            ShowMessageBox("Please Select Approval Status!!!");
            return false;
        }

        Int32 count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please Select at least one employee !!!");
            return false;
        }
        //if ()
        //{
        //    AlertMessageBoxShow("Please Select Year...");
        //    txtToDate.Focus();
        //    return false;


        //}

        return true;
    }

    protected void Button11_OnClick(object sender, EventArgs e)
    {
        
    }


    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            _commonDataLoad.GetSubSectionListAll(ddlSubSection, ddlDivision.SelectedValue);
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        //else
        //{
        //    txtSearch.Text = "";
        //    txtSearch.Text = "";
        //    //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];

        }
        //else
        //{
        //    NameTextBox.Text = "";
        //    NameTextBox.Text = "";
        //  //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchAllownaceCancel.aspx");
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedIndex > 0)
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(companyDropDownList.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(companyDropDownList.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(companyDropDownList.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(companyDropDownList.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(companyDropDownList.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }

            using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsActive(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlWing.Items.Clear();
                ddlDepartment.Items.Clear();
                ddlSection.Items.Clear();
                    ddlSubSection.Items.Clear();
                    ddlSalaryLocation.Items.Clear();
        }
    }

    protected void effectiveDateTextBox_OnTextChanged(object sender, EventArgs e)
    {
        Button1_OnClick(null, null);
    }
}